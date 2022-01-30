using qproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qproject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var employees = dc.Employees.OrderBy(a => a.Email).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Employees.Where(a => a.Id == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        public ActionResult Save(Employee emp)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    if (emp.Id > 0)
                    {
                        //Edit
                        var v = dc.Employees.Where(a => a.Id == emp.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v.Email = emp.Email;
                            v.Name = emp.Name;
                            v.Image = emp.Image;
                        }
                    }
                    else
                    {
                        //save
                        dc.Employees.Add(emp);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Employees.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
    
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (MyDatabaseEntities dc=new MyDatabaseEntities())
            {
                var v = dc.Employees.Where(a => a.Id == id).FirstOrDefault();
                if(v!=null)
                {
                    dc.Employees.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new {status=status}  };
        }
    }
}