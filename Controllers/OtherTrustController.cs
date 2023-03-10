using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NewZapures_V2.Models.TrusteeBO;
using System.Web.Script.Serialization;

namespace NewZapures_V2.Controllers
{
    public class OtherTrustController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        // GET: OtherTrust
        public ActionResult TrustDetails()
        {

            List<CustomMaster> TrusteeType = new List<CustomMaster>();
            TrusteeType = Common.GetCustomMastersList(44);
            ViewBag.TrusteeType = TrusteeType;
            return View();
        }

        [HttpPost]
        public JsonResult TrustDetails(TrusteeInfo obj)
        {
            List<CustomMaster> TrusteeType = new List<CustomMaster>();
            TrusteeType = Common.GetCustomMastersList(31);
            ViewBag.TrusteeType = TrusteeType;

            #region Add Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/SaveOtherTrustDetails");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("LoginId", "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    SessionModel.TrustId = objResponseData.Id;
                    SessionModel.TrustName = objResponseData.Name;
                    return new JsonResult
                    {
                        Data = new { StatusCode = objResponseData.ResponseCode, Message = objResponseData.Messsage, Failure = true },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { StatusCode = objResponseData.ResponseCode, Message = objResponseData.Messsage, Failure = false },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            #endregion

            return new JsonResult
            {
                Data = new { StatusCode = 0, Message = "Failed", Failure = false },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return View();
        }
    }
}