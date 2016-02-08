using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimplexUserAdmin.Models;

namespace SimplexUserAdmin.Controllers
{
    public class UserAdminController : Controller
    {


        public UserAdminController()
        {

        }

        // GET: UserAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageUser(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                ViewBag.Message = "Add user to role";
            }
            else
                ViewBag.Message = message;

            return View();
        }


        public ActionResult RoleAdmin(string message)
        {
            if (string.IsNullOrEmpty(message))
                ViewBag.Message = "Type role name and click add.";
            else
            {
                ViewBag.Message = message;
            }

            return View();
        }

        // GET: UserAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult AddUserToRole(string userName, string roleName)
        {
            string userMessage = "User added to role";
            try
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                IdentityResult roleAddResult;

                if (roleManager.RoleExists(roleName))
                {
                    var _context = new ApplicationDbContext();
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

                    //Check if user is in this role
                    var userFromDb = _context.Users.ToList().Where(x => x.UserName.Equals(userName)).ElementAtOrDefault(0);
                    if(userFromDb != null)
                    {
                        userManager.AddToRole(userFromDb.Id, roleName);
                    }
                    
                }
                else
                {
                    userMessage = string.Format("Role {0} does not exists", roleName);
                }

            }
            catch (Exception ex)
            {
                userMessage = ex.Message;
            }

            return RedirectToAction("ManageUser", new RouteValueDictionary(
                                                new { controller = "UserAdmin", action = "ManageUser", message = userMessage }));
        }

        [HttpPost]
        public ActionResult AddRole(string roleName)
        {
            string userMessage = "Role created";
            try {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                IdentityResult roleAddResult;

                if (!roleManager.RoleExists(roleName))
                {
                    roleAddResult = roleManager.Create(new IdentityRole(roleName));
                }
                else
                {
                    userMessage = string.Format("Role {0} exists already", roleName);
                }

            }
            catch(Exception ex)
            {
                userMessage = ex.Message;
            }

            return RedirectToAction("RoleAdmin", new RouteValueDictionary(
                                                new { controller = "UserAdmin", action = "RoleAdmin", message = userMessage}));
        }

    }
}
