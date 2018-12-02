using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSharpDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CSharpDashboard.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return View();
            }
            return RedirectToAction("Dashboard");
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return View();
            }
            return RedirectToAction("Dashboard");
        }
        [HttpPost("register")]
        public IActionResult Registering(User register)
        {
            if (ModelState.IsValid)
            {
                if(!dbContext.Users.Any(u => u.Email == register.Email))
                {
                    var hasher = new PasswordHasher<User>();
                    register.Password = hasher.HashPassword(register, register.Password);
                    User newUser = new User
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Email = register.Email,
                        Password = register.Password,
                        Level = 1,
                        CreatedAt = register.CreatedAt,
                        UpdatedAt = register.UpdatedAt,
                    };
                    if (dbContext.Users.Where(u => u.Level == 9).Count() == 0)
                    {
                        newUser.Level = 9;
                    }
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    var new_user = dbContext.Users.SingleOrDefault(u => u.Email == register.Email);
                    HttpContext.Session.SetInt32("user_id", new_user.UserId);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("Email", "This Email is already in use.");
                }
            }
            return View("Register");
        }
        [HttpPost("logging")]
        public IActionResult Logging(LoginUser login)
        {
            if (ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == login.Email);
                if(userInDb != null)
                {
                    var hasher = new PasswordHasher<LoginUser>();
                    if(hasher.VerifyHashedPassword(login, userInDb.Password, login.Password) != 0)
                    {
                        HttpContext.Session.SetInt32("user_id", userInDb.UserId);
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Your Password does not match the Email you provided.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "No matching Email in database.");
                }
            }
            return View("Login");
        }
        [HttpGet("/wall/{user_id}")]
        public IActionResult Wall(int user_id)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == HttpContext.Session.GetInt32("user_id")))
                {
                    if (dbContext.Users.Any(u => u.UserId == user_id))
                    {
                        ViewBag.Messages = dbContext.Messages.Include(m => m.User).Where(m=> m.ReceiverId == user_id).OrderByDescending(m => m.CreatedAt).ToList();
                        ViewBag.Comments = dbContext.Comments.Include(c => c.Creator).OrderByDescending(c => c.CreatedAt).ToList();
                        ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == user_id);
                        TempData["receiver_id"] = dbContext.Users.FirstOrDefault(u => u.UserId == user_id).UserId;
                        return View();
                    }
                    return RedirectToAction("Dashboard");
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpPost("/createmessage")]
        public IActionResult CreateMessage(Messages newMessage)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == HttpContext.Session.GetInt32("user_id")))
                {
                    if (ModelState.IsValid)
                    {
                        Messages new_message = new Messages
                        {
                            Message = newMessage.Message,
                            UserId = (int)HttpContext.Session.GetInt32("user_id"),
                            ReceiverId = newMessage.ReceiverId,
                        };
                        dbContext.Add(new_message);
                        dbContext.SaveChanges();
                        return RedirectToAction("wall", new {user_id = newMessage.ReceiverId});
                    }    
                    ViewBag.Messages = dbContext.Messages.Include(m => m.User).Where(m=> m.ReceiverId == newMessage.ReceiverId).OrderByDescending(m => m.CreatedAt).ToList();
                    ViewBag.Comments = dbContext.Comments.Include(c => c.Creator).OrderByDescending(c => c.CreatedAt).ToList();
                    ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == newMessage.ReceiverId);
                    return View("Wall");
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpPost("/createcomment/{user_id}")]
        public IActionResult CreateComment(Comments newComment, int user_id)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == HttpContext.Session.GetInt32("user_id")))
                {
                    if (dbContext.Messages.Any(m => m.MessageId == newComment.MessageId))
                    {
                        if (ModelState.IsValid)
                        {
                            Comments new_comment= new Comments
                            {
                                MessageId = newComment.MessageId,
                                Comment = newComment.Comment,
                                UserId = (int)HttpContext.Session.GetInt32("user_id"),
                            };
                            dbContext.Add(new_comment);
                            dbContext.SaveChanges();
                            return RedirectToAction("Wall", new {user_id = user_id});
                        }
                    }
                    ViewBag.Messages = dbContext.Messages.Include(m => m.User).Where(m=> m.ReceiverId == user_id).OrderByDescending(m => m.CreatedAt).ToList();
                    ViewBag.Comments = dbContext.Comments.Include(c => c.Creator).OrderByDescending(c => c.CreatedAt).ToList();
                    ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == user_id);
                    return View("Wall");
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    ViewBag.Users = dbContext.Users;
                    ViewBag.Level = dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("user_id")).Level;
                    return View();
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpGet("/selfedit")]
        public IActionResult SelfEdit()
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id"));
                    return View();
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpGet("/edit/{user_id}")]
        public IActionResult Edit(int user_id)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    if(dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")).Level == 9)
                    {
                        if(dbContext.Users.Any(u => u.UserId == user_id))
                        {
                            if(HttpContext.Session.GetInt32("user_id") != user_id)
                            {
                                ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == user_id);
                                return View();
                            }
                            return RedirectToAction("SelfEdit");
                        }
                    }
                    return RedirectToAction("Dashboard");
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpPost("/editing/{user_id}")]
        public IActionResult Editing(EditUser editing, int user_id)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    if((int)HttpContext.Session.GetInt32("user_id") == user_id)
                    {
                        if(ModelState.IsValid)
                        {
                            if (!dbContext.Users.Where(u => u.UserId != user_id).Any(u => u.Email == editing.Email))
                            {
                                var edit = dbContext.Users.FirstOrDefault(u => u.UserId == user_id);
                                edit.Email = editing.Email;
                                edit.FirstName = editing.FirstName;
                                edit.LastName = editing.LastName;
                                dbContext.SaveChanges();
                                return RedirectToAction("SelfEdit");
                            }
                            ModelState.AddModelError("Email", "This email is already in use.");
                        }
                        ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id"));
                        return View("SelfEdit");
                    }
                    else
                    {
                        if(dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")).Level == 9)
                        {
                            if(ModelState.IsValid)
                            {
                                if (!dbContext.Users.Where(u => u.UserId != user_id).Any(u => u.Email == editing.Email))
                                {
                                    var edit = dbContext.Users.FirstOrDefault(u => u.UserId == user_id);
                                    edit.Level = editing.Level;
                                    edit.Email = editing.Email;
                                    edit.FirstName = editing.FirstName;
                                    edit.LastName = editing.LastName;
                                    dbContext.SaveChanges();
                                    return RedirectToAction("Edit", user_id);
                                }
                                ModelState.AddModelError("Email", "This email is already in use.");
                            }
                            ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == user_id);
                            return View("Edit");
                        }
                        return RedirectToAction("Dashboard");
                    }
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpPost("editdesc")]
        public IActionResult EditDesc(EditUserDescription newDesc)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    if(ModelState.IsValid)
                    {
                        dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")).Description = newDesc.Description;
                        dbContext.SaveChanges();
                        return RedirectToAction("SelfEdit");
                    }
                    ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id"));
                    return View("SelfEdit");
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpPost("editpassword/{user_id}")]
        public IActionResult EditPassword(EditUserPassword editPassword, int user_id)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    if((int)HttpContext.Session.GetInt32("user_id") == user_id)
                    {
                        if(ModelState.IsValid)
                        {
                            var hasher = new PasswordHasher<EditUserPassword>();
                            editPassword.Password = hasher.HashPassword(editPassword, editPassword.Password);
                            dbContext.Users.FirstOrDefault(u => u.UserId == user_id).Password = editPassword.Password;
                            dbContext.SaveChanges();
                            return RedirectToAction("SelfEdit");
                        }
                        ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == user_id);
                        return View("SelfEdit");
                    }
                    else
                    {
                        if(dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")).Level == 9)
                        {
                            if(ModelState.IsValid)
                            {
                                var hasher = new PasswordHasher<EditUserPassword>();
                                editPassword.Password = hasher.HashPassword(editPassword, editPassword.Password);
                                dbContext.Users.FirstOrDefault(u => u.UserId == user_id).Password = editPassword.Password;
                                dbContext.SaveChanges();
                                return RedirectToAction("Edit", new { user_id = user_id});
                            }
                            ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == user_id);
                            return View("Edit");
                        }
                        return RedirectToAction("Dashboard");
                    }
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpGet("New")]
        public IActionResult New()
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    if(dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")).Level == 9)
                    {
                        return View();
                    }
                    return RedirectToAction("Dashboard");
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpPost("Add")]
        public IActionResult Add(User register)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    if(dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")).Level == 9)
                    {
                        if(ModelState.IsValid)
                        {
                            var hasher = new PasswordHasher<User>();
                            register.Password = hasher.HashPassword(register, register.Password);
                            User newUser = new User
                            {
                                FirstName = register.FirstName,
                                LastName = register.LastName,
                                Email = register.Email,
                                Password = register.Password,
                                Level = 1,
                                CreatedAt = register.CreatedAt,
                                UpdatedAt = register.UpdatedAt,
                            };
                            dbContext.Add(newUser);
                            dbContext.SaveChanges();
                            var new_user = dbContext.Users.SingleOrDefault(u => u.Email == register.Email);
                            return RedirectToAction("Dashboard");
                        }
                        return View("New");
                    }
                    return RedirectToAction("Dashboard");
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [HttpGet("delete/{user_id}")]
        public IActionResult Delete(int user_id)
        {
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                if (dbContext.Users.Any(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")))
                {
                    if(dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id")).Level == 9)
                    {
                        dbContext.Users.Remove(dbContext.Users.FirstOrDefault(u => u.UserId == user_id));
                        dbContext.SaveChanges();
                        return RedirectToAction("Dashboard");
                    }
                    ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("user_id"));
                    return View("SelfEdit");
                }
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}