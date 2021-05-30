using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgressTestWeb.db;

namespace ProgressTestWeb.Controllers
{
    public class ProgressController : Controller
    {
        // GET: Progress
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAjaxTest1(string param)
        {
            try
            {
                var db = new webtestEntities();
                var q = from b in db.progress_test where b.no == param select b.count;
                var cnt = q.Single();

                return Json(new { donePercent = cnt.ToString(), finish = (cnt == 100) });
            }
            catch
            {

            }
            return Json("");
        }

        public ActionResult StartProgress()
        {
            var psi = new ProcessStartInfo();
            psi.FileName = "ProgressTestExe";
            var guid = Guid.NewGuid();
            string new_id = guid.ToString("N");
            psi.UseShellExecute = false;

            psi.Arguments = new_id;
            var p = Process.Start(psi);

            ViewBag.ProgressID = new_id;

            return View();
        }

        public ActionResult Download(string id)
        {
#if DEBUG
            string path = @"C:\Users\ono\HW\web.config";
#else
            string path = @"C:\ProgressTest\" + id + ".txt";
#endif
            return File(path, "application/pdf", "test.txt");
        }
    }
}
