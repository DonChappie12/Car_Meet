using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using car_meet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace car_meet.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context;
        public HomeController(UserContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(ValidateUser user)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<ValidateUser> Hasher = new PasswordHasher<ValidateUser>();
                user.Password = Hasher.HashPassword(user, user.Password);
                User newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password
                };
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("user_id",newUser.UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("login")]
        public IActionResult LoginIn(string Email, string Password)
        {
            var user = _context.User.Where(u=> u.Email == Email).FirstOrDefault();
            if(user != null && Password != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, Password))
                {
                    HttpContext.Session.SetInt32("user_id", user.UserId);
                    return RedirectToAction("Dashboard");
                }
            }
            ViewBag.error="Email and/or Password dont match";
            return View("Login");
        }

        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            User currUser = _context.User.SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
            if (currUser!=null)
            {
                User user = _context.User.Include(x => x.Going).Include(e => e.Event).SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
                List <Events> events = _context.Event.Include(x => x.user).Include(join => join.Going).ToList();
                ViewBag.Event = events;
                ViewBag.Id = currUser.UserId;
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("newevent")]
        public IActionResult NewEvent()
        {
           User currUser = _context.User.SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
            if (currUser!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost("createevent")]
        public IActionResult CreateEvent(Events Events)
        {
            User currUser = _context.User.SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
            if (currUser!=null)
            {
                if(ModelState.IsValid)
                {
                    if(Events.Date > DateTime.Now)
                    {
                        Events newEvent = new Events
                        {
                            Title = Events.Title,
                            City = Events.City,
                            Time = Events.Time,
                            TimeAmt = Events.TimeAmt,
                            Description = Events.Description,
                            Adress = Events.Adress,
                            Date = Events.Date,
                            User_Id = currUser.UserId
                        };
                        _context.Add(newEvent);
                        _context.SaveChanges();
                        return Redirect("/details/"+newEvent.Id);
                    }
                    ViewBag.timetravel = "You can't go back in time";
                    return View("NewEvent");
                }
                return View("NewEvent");
            }
            return RedirectToAction("Index");
        }

        [Route("/details/{Id}")]
        public IActionResult Details(int Id)
        {
            User currUser = _context.User.SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
            if(currUser!=null)
            {
                Events thisEvent = _context.Event.Include(x => x.user).SingleOrDefault(x => x.Id == Id);
                return View(thisEvent);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("going/{Id}")]
        public IActionResult Going(int Id)
        {
            User currUser = _context.User.SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
            if( currUser != null)
            {
                Events thisEvent = _context.Event.Include(x => x.Going).ThenInclude(x => x.user).SingleOrDefault(x => x.Id == Id);
                Going newCommer = new Going()
                {
                    User_Id = currUser.UserId,
                    Event_Id = Id
                };
                _context.Going.Add(newCommer);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("notgoing/{Id}")]
        public IActionResult NotGoing(int Id)
        {
            User currUser = _context.User.SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
            if(currUser != null)
            {
                Going notGoing = _context.Going.SingleOrDefault(x => x.Event_Id == Id && x.User_Id == currUser.UserId);
                if(notGoing!=null)
                {
                    _context.Going.Remove(notGoing);
                    _context.SaveChanges();
                }
                return RedirectToAction("Dashboard");
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            User currUser = _context.User.SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
            if(currUser!=null)
            {
                Events thisEvent = _context.Event.Include(x => x.Going).SingleOrDefault(x => x.Id == Id);
                foreach(var at in thisEvent.Going)
                {
                    _context.Remove(at);
                }
                _context.Remove(thisEvent);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                return Redirect("/");
            }
        }

        [Route("User/{Id}")]
        public IActionResult UserDetails(int Id)
        {
            User currUser = _context.User.SingleOrDefault(c => c.UserId == HttpContext.Session.GetInt32("user_id"));
            if(currUser!=null)
            {
                User thisUser = _context.User.SingleOrDefault(x => x.UserId == Id);
                return View(thisUser);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
