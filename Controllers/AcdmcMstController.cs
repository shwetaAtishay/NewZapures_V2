using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewZapures_V2.Controllers
{
    public class AcdmcMstController : Controller
    {

        ResponseData objResponse;
        // GET: AcdmcMst
        public ActionResult Index(string guid = "", int clgId = 0)
        {
            List<CustomMaster> Course = new List<CustomMaster>();
            Course = ZapurseCommonlist.GetCourseListDropDown(SessionModel.ApplicantGuid);
            ViewBag.Course = Course;

            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {

                var ddlYear = GetYear();
                var result = GetResult();
                var AllData = GetAcdmcData(guid,clgId);
                var ddlCourse = GetCourseData(userdetailsSession.DepartmentId);
                ViewBag.FromYear = ddlYear;
                ViewBag.ToYear = ddlYear;
                ViewBag.Result = result;
                //ViewBag.Course = ddlCourse;
                ViewBag.AllData = AllData;
                ViewBag.GUIId = guid;
                ViewBag.clgID = clgId;
            }

            return View();
        }
        public List<Dropdown> GetYear()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetYear");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> data = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    data = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return data;
        }

        public List<Dropdown> GetResult()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetResult");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> data = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    data = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return data;
        }

        public JsonResult SaveData(AcdmcMaster mapping)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            var json = JsonConvert.SerializeObject(mapping);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveAcdmcMst");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public List<AcdmcTableData> GetAcdmcData(string GUIID, int clgID)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetAcdmcData?GUIID=" + GUIID + "&clgID=" + clgID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AcdmcTableData> data = new List<AcdmcTableData>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    data = JsonConvert.DeserializeObject<List<AcdmcTableData>>(objResponse.Data.ToString());
                }
            }
            return data;
        }
        public List<Dropdown> GetCourseData(int departmentID)
        {
            var data = ZapurseCommonlist.GetCourseForDept(departmentID);
            //var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCourseData");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("cache-control", "no-cache");
            ////request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //request.AddParameter("application/json", "", ParameterType.RequestBody);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Accept", "application/json");
            //IRestResponse response = client.Execute(request);
            //List<Dropdown> data = new List<Dropdown>();
            //if (response.StatusCode.ToString() == "OK")
            //{
            //    objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            //    if (objResponse.Data != null)
            //    {
            //        data = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            //    }
            //}
            return data;
        }


        public JsonResult EditAcdmcMSt(int AcdmcId)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetACDMCDataEdit?AcdmcId=" + AcdmcId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var Department = JsonConvert.DeserializeObject<List<AcdmcMaster>>(d.Data.ToString());

                return new JsonResult
                {
                    Data = new { StatusCode = d.statusCode, Data = Department, Failure = false, Message = d.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetACDMCDataDelete(int AcdmcId)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetACDMCDataDelete?AcdmcId=" + AcdmcId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var Department = JsonConvert.DeserializeObject<List<AcdmcMaster>>(d.Data.ToString());

                return new JsonResult
                {
                    Data = new { StatusCode = d.statusCode, Data = Department, Failure = false, Message = d.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}