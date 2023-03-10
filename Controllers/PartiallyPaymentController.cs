using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NewZapures_V2.Models.Common;
using System.Web.Script.Serialization;

namespace NewZapures_V2.Controllers
{
    public class PartiallyPaymentController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: PartiallyPayment
        public ActionResult PaymentDetails()
        {
            string applGUID = SessionModel.ApplicantGuid;
            //ViewBag.applGUID=applGUID;
            ViewBag.paymentList = GetPaymentData();
            return View();
        }
        #region save details

        [HttpPost]
        public JsonResult SaveDetails(ParticalPaymentBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "PaymentUploadRecpet/PayRecept");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    return new JsonResult
                    {
                        Data = new { StatusCode = objResponse.statusCode, Failure = true },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { StatusCode = objResponse.statusCode, Failure = false },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return RedirectToAction("PaymentDetails");
        }

        public List<ParticalPaymentBO> GetPaymentData(string Type = "")
        {
            //List<AddCourseBO> res = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "PaymentUploadRecpet/GetReceptData?Type=" + Type);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<ParticalPaymentBO> paymentresult = new List<ParticalPaymentBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                paymentresult = JsonConvert.DeserializeObject<List<ParticalPaymentBO>>(response.Content);
                ViewBag.paymentList = paymentresult;

            }
            return paymentresult;

        }
        #endregion

        #region OLD NOC DETAILS
        //public ActionResult OldNOCDetails()
        //{
        //    return View();
        //}
        #endregion

        #region OLDNOCCount
        public ActionResult CountOldNocDetails()
        {
            GetOLDDetails();
            return View();
        }
        public List<OLDNOCBO> GetOLDDetails()
        {
            //List<AddCourseBO> res = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "PaymentUploadRecpet/GetOLDNOCData");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<OLDNOCBO> Listresult = new List<OLDNOCBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                Listresult = JsonConvert.DeserializeObject<List<OLDNOCBO>>(response.Content);
                ViewBag.Listresult = Listresult;
                ViewBag.ListresultOne = Listresult;
                ViewBag.ListresultTwo = Listresult;
                ViewBag.ListresultThree = Listresult;
                ViewBag.ListresultFour = Listresult;
            }
            return Listresult;

        }
        #endregion

    }
}