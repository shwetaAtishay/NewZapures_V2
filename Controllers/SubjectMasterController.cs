﻿using Newtonsoft.Json;
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
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    public class SubjectMasterController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: SubjectMaster
        public ActionResult CreateData()
        {

            string applGUID = SessionModel.ApplicantGuid;
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;

            //List<CustomMaster> DegreeList = new List<CustomMaster>();
            //DegreeList = Common.GetCustomMastersList(33);
            ViewBag.DegreeList = GetDept();

            //List<CustomMaster> CourseList = new List<CustomMaster>();
            //CourseList = Common.GetCustomMastersList(30);
            //ViewBag.CourseList = CourseList;

            ViewBag.AddCourseList = GetDetailsList();

            List<CustomEnum> collegeListData = new List<CustomEnum>();
            collegeListData = GetCollegeList();
            ViewBag.collegeListData = collegeListData;

            ViewBag.applNumber = applGUID;
            var EditdraftedApplications = ZapurseCommonlist.GetDraftApplication(applGUID);
            ViewBag.applicationDetails = EditdraftedApplications[0];

            var subjects = GetSubjectDataList(applGUID);
            ViewBag.SubjectDataList = subjects;

            ViewBag.PageListData = GetSubjectPageDataList(applGUID);

            return View();
        }

        public List<Dropdown> GetDept()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetDept");
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

        public JsonResult GetCourse(int departID)
        {

            var courses = ZapurseCommonlist.GetCourseForDept(departID);


            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = courses, Failure = false, Message = "CourseList" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        //For pagelist
        public List<AddCourseBO> GetSubjectPageDataList(string applGUID)
        {
            //List<AddCourseBO> res = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "AddCourseData/GetSubjectPageList?Guid=" + SessionModel.ApplicantGuid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<AddCourseBO> _result = new List<AddCourseBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    _result = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());
            }
            return _result;
            //return View(_result);
            // return RedirectToAction("EditApplication", "SubjectMaster", new { applGUID = SessionModel.ApplicantGuid });
        }
        public List<AddCourseBO> GetSubjectDataList(string applGUID)
        {
            //List<AddCourseBO> res = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "AddCourseData/GetSubjectList?Guid=" + SessionModel.ApplicantGuid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<AddCourseBO> _result = new List<AddCourseBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    _result = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());
            }
            return _result;
            //return View(_result);
            // return RedirectToAction("EditApplication", "SubjectMaster", new { applGUID = SessionModel.ApplicantGuid });
        }
        public List<CustomMaster> GetCourseDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/GetTrustDropDownList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(d.Data.ToString());

                objUsermaster = objResponseData.ListRequest;
            }
            return objUsermaster;
        }
        public List<CustomMaster> GetDegreeDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/GetTrustDropDownList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(d.Data.ToString());

                objUsermaster = objResponseData.ListRequest;
            }
            return objUsermaster;
        }
        public List<CustomMaster> GetTrustDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/GetTrustDropDownList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(d.Data.ToString());

                objUsermaster = objResponseData.ListRequest;
            }
            return objUsermaster;
        }
        public JsonResult GetCollegeDropDownList(int TrustInfoId)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    objUsermaster = JsonConvert.DeserializeObject<List<CustomMaster>>(objResponse.Data.ToString());
                ViewBag.CollegeList = objUsermaster;
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objUsermaster, Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        public List<CustomEnum> GetCollegeList()
        {
            List<CustomEnum> objUsermaster = new List<CustomEnum>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "AddCourseData/GetCollegeList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<CustomEnum> _result = _JsonSerializer.Deserialize<List<CustomEnum>>(response.Content);
                if (_result != null)
                {
                    objUsermaster = _result;

                }
            }
            return objUsermaster;
        }

        [HttpPost]
        public JsonResult SaveDetails(List<AddCourseBO> trg)
        {
            //try
            //{
            string applGUID = SessionModel.ApplicantGuid;
            List<AddCourseBO> _ListSubject = new List<AddCourseBO>();
            AddCourseBO obj = new AddCourseBO();
            obj = trg.FirstOrDefault();
            obj.applicationNumber = SessionModel.ApplicantGuid;
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            //party.ParentId = userdetailsSession.PartyId;
            var json = JsonConvert.SerializeObject(obj);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/SubjectDetail");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response2 = client.Execute(request);
            if (response2.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    _ListSubject = GetSubjectDataList(applGUID);
                    return new JsonResult
                    {
                        Data = new { Data = _ListSubject, failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    _ListSubject = GetSubjectDataList(applGUID);
                    return new JsonResult
                    {
                        Data = new { Data = _ListSubject, failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = _ListSubject, failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return RedirectToAction("CreateData");
        }
        //For Course List
        public List<AddCourseBO> GetDetailsList()
        {
            List<AddCourseBO> objUsermaster = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/GetDetails");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddCourseBO> objResponseData = new List<AddCourseBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponseData = JsonConvert.DeserializeObject<List<AddCourseBO>>(response.Content);
                //objResponseData = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());
                objUsermaster = objResponseData;
            }
            return objUsermaster;

            //return RedirectToAction("GetDetailsList", "SubjectMaster", new { applGUID = guid });
        }


        public JsonResult EditSubject(string type, int iPK_SubId, string sub)
        {
            string applGUID = SessionModel.ApplicantGuid;
            List<AddCourseBO> _ListSubject = new List<AddCourseBO>();


            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "AddCourseData/GetSubjectListById?iPK_SubId=" + iPK_SubId + "&type=" + type + "&SubjectName=" + sub);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var Subjectlisting = new List<AddCourseBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                    Subjectlisting = JsonConvert.DeserializeObject<List<AddCourseBO>>(d.Data.ToString());
                if (type == "Get")
                {
                    return new JsonResult
                    {
                        Data = new { StatusCode = d.statusCode, Data = Subjectlisting, Failure = false, Message = d.Message },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    if (type == "Update")
                    {
                        _ListSubject = GetSubjectDataList(applGUID);
                    }
                    return new JsonResult
                    {
                        Data = new { StatusCode = d.statusCode, Data = _ListSubject, Failure = false, Message = d.Message },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
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