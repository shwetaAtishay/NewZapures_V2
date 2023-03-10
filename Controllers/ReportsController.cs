using NewZapures_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NewZapures_V2.Models.Common;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using RestSharp;
using static NewZapures_V2.Models.TrusteeBO;
using System.Configuration;

namespace NewZapures_V2.Controllers
{
    public class ReportsController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        // GET: Reports
        public ActionResult MyAppliedApplication(string applGUID)
        {
             applGUID = SessionModel.ApplicantGuid;
              ViewBag.applGUID = applGUID;
            //var recentApplicationList = ZapurseCommonlist.GetAdminApplication(applGUID);
            var recentApplicationList = GetReportUserApplication(applGUID);
            ViewBag.applicationDetails = recentApplicationList;

            var applicationtrack = Applicationtrack(applGUID);
            ViewBag.Applicationtrack = applicationtrack;
            return View();
        }

        //Report Data
        public static List<DraftApplication> GetReportUserApplication(string applGUID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetReportApplication?applGUID=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<DraftApplication> draftApplication = new List<DraftApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    draftApplication = JsonConvert.DeserializeObject<List<DraftApplication>>(requestResponse.Data.ToString());
            }
            return draftApplication;
        }
        //Application Tracking
        public ActionResult Applicationtrack(string applGUID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetApplicationtracking?applGUID=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<ApplicationTrack> trackingApplication = new List<ApplicationTrack>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    trackingApplication = JsonConvert.DeserializeObject<List<ApplicationTrack>>(requestResponse.Data.ToString());
                //ViewBag.Applicationtrack = trackingApplication;
            }
           // var applicationtrack = Applicationtrack(applGUID);
            ViewBag.Applicationtrack = trackingApplication;
            return View();
        }
    }
}