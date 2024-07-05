using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PracticeMVCProject.Models;
using PracticeMVCProject.Security;

namespace PracticeMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly MvcfirstProjectDatabaseContext _context;
        private readonly IDataProtector _protector;



        public HomeController(MvcfirstProjectDatabaseContext context, DataSecurityProvider provider, IDataProtectionProvider protectionProvider) { 
        _context=context;
        _protector = protectionProvider.CreateProtector(provider.dataKey);

        }
        public IActionResult Index()
        {
            var users=_context.UserLists.ToList();
            var u = users.Select(e =>
            {
                e.EncrptedId = _protector.Protect(Convert.ToString(e.UserId));
                return e;
            });
            return View(u);
        }
        public IActionResult GetUsers()
        {
            List<UserList> users = _context.UserLists.ToList();
            var u = users.Select(e =>
            {
                e.EncrptedId = _protector.Protect(Convert.ToString(e.UserId));
                return e;
            });
            return PartialView("_GetUsers", u);
        }
        public IActionResult Details(string id)
        {
            int UserId = Convert.ToInt32(_protector.Unprotect(id));
            var u = _context.UserLists.Where(x => x.UserId == UserId).First();
            return View(u);
        }
        public IActionResult Contact()
        {
            var users = _context.UserLists.ToList();
            /*var u = users.Select(e =>
            {
                e.EncrptedId = _protector.Protect(Convert.ToString(e.UserId));
                return e;
            });*/
            // List<UserList> users = new List<UserList>()
            // {
            //     new UserList {UserId=1, UserName="Hari", UserPassword="23456",Email="asdfb@gmail.com"}
            // };
            //return Json(users);
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(UserListEdit u)
        {
            if (ModelState.IsValid)
            {
                UserList user = new()
                {
                    Email = u.Email,
                    UserName = u.UserName,
                    UserPassword = u.UserPassword,
                };
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
            }

            return View(u);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            int UserId = Convert.ToInt32(_protector.Unprotect(id));
            var user = _context.UserLists.Find(UserId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserListEdit
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserPassword = user.UserPassword,
                Email = user.Email
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserListEdit u)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserLists.Find(u.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = u.UserName;
                user.UserPassword = u.UserPassword;
                user.Email = u.Email;

                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(u);

        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            int UserId = Convert.ToInt32(_protector.Unprotect(id));
            var user = _context.UserLists.Find(UserId);
            if (user == null)
            {
                return NotFound();
            }
            var u = _context.UserLists.Where(x => x.UserId == UserId).FirstOrDefault();
            _context.UserLists.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
