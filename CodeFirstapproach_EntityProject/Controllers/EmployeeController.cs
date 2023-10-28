using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirstapproach_EntityProject.Models;
using Newtonsoft.Json;

namespace CodeFirstapproach_EntityProject.Controllers
{
    public class EmployeeController : Controller
    {
        DB65_001Entities2 PP = new DB65_001Entities2();

        // sqlconnection con =  new sqconnection("data source=;inte")
        //con.Open() con.close()
        public ActionResult EmployeeIndex()
        {
            return View();
        }

        public void EmployeeUpdate(tblemployee obj)
        {

            PP.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            PP.SaveChanges();
        
        }

        public void EmployeeInsert(tblemployee obj)
        {

            PP.tblemployees.Add(obj);
            PP.SaveChanges();

        }

        public JsonResult EmployeeShow()
        {
            var data =( from a in PP.tblemployees 
                        join w in PP.tblcountries on a.country equals w.cid
                        join z in PP.tblstates on a.state equals z.sid
                        select new {a.id,a.name,a.gender,w.cname,z.sname}).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
         
        //??select * from tblemployee join tblcountry on country = cid join  tblstate on state = sid

        
        // from e in PP.tblemployees select e



        public JsonResult EmployeeCountry()
        {
            var data = PP.tblcountries.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        
        }

        public JsonResult EmployeeState(int A)
        {
            var data =(from a in PP.tblstates select a).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmployeeEdit(int A)
        {
            var data = (from a in PP.tblemployees where a.id==A select a).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);        
        }

        public void EmployeeDelete(int A)
        {
          var data = PP.tblemployees.Find(A);
            PP.tblemployees.Remove(data);
            PP.SaveChanges();
                
        }
    }
}