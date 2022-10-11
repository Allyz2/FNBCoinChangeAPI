using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        string baseUrl = "http://localhost/";


        public ActionResult GeChange()
        {
            return View();
        }



        [HttpPost]
        public JsonResult GePayoutChange(FormCollection frm)
        {
            var _v1 = int.Parse(frm["v1"]);
            var _v2 = int.Parse(frm["v2"]);
            var _v3 = int.Parse(frm["v3"]);
            var _Amount = int.Parse(frm["Amount"]);

            int[] _arraay = new int[] { _v1,_v2,_v3};

            string _testres = "" ;


           // string ApiMethodNameParam = "svc?Coins=" +_arraay  + "&amount=" + _Amount;

            //Try call the coin change  api hosted locally on iis
             string ApiMethodNameParam ="svc?Coins=" + _v1 + "&Coins=" + _v2 + "&Coins="+ _v3 + "&amount=" + _Amount;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/");
                //HTTP GET
                var responseTask = client.GetAsync(ApiMethodNameParam);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    // var readTask = result.Content();
                     readTask.Wait();
                    _testres = readTask.Result;
                }

            }
            //return string json result
            return Json(_testres, JsonRequestBehavior.AllowGet);
        }





        public ActionResult Index()
        {
            //redirect user to the change coin page
            return RedirectToAction("GeChange", "Home");
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}