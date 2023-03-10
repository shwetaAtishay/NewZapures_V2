using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using static NewZapures_V2.Models.Common;
using System.Web.Mvc;
using System.Web;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    public class AddCourseController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: AddCourse
        public ActionResult CreateDetails()
        {

            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;

            //List<CustomMaster> DegreeList = new List<CustomMaster>();
            //DegreeList = Common.GetCustomMastersList(33);

            ViewBag.Department = GetDept();

            List<CustomMaster> CourseList = new List<CustomMaster>();
            CourseList = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.Course));
            ViewBag.CourseList = CourseList;

            ViewBag.AddCourseList = GetDetailsList(Convert.ToInt32(SessionModel.TrustId));
            ViewBag.Trustid = SessionModel.TrustId;
            return View();
        }
        public List<AddCourseBO> GetDetailsList(int Id)
        {
            List<AddCourseBO> objUsermaster = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/GetDetails?TrustId=" + Id);
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

        //public JsonResult GetCourse(int departID)
        //{

        //    var courses = ZapurseCommonlist.GetCourseForDept(departID);


        //    return new JsonResult
        //    {
        //        Data = new { StatusCode = 1, Data = courses, Failure = false, Message = "CourseList" },
        //        ContentEncoding = System.Text.Encoding.UTF8,
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };
        //}
        public JsonResult GetCourse(int departID, string DataType)
        {

            var courses = ZapurseCommonlist.GetCourseForDept(departID, DataType);


            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = courses, Failure = false, Message = "CourseList" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public List<CustomMaster> GetCourseDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetTrustDropDownList");
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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetTrustDropDownList");
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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetTrustDropDownList");
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
        //public JsonResult GetCollegeDropDownList(int TrustInfoId)
        //{
        //    List<CustomMaster> objUsermaster = new List<CustomMaster>();
        //    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");
        //    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    request.AddParameter("application/json", "", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    if (response.StatusCode.ToString() == "OK")
        //    {
        //        objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
        //        if (objResponse.Data != null)
        //            objUsermaster = JsonConvert.DeserializeObject<List<CustomMaster>>(objResponse.Data.ToString());
        //    }
        //    return new JsonResult
        //    {
        //        Data = new { StatusCode = objResponse.statusCode, Data = objUsermaster, Failure = false, msg = objResponse.Message },
        //        ContentEncoding = System.Text.Encoding.UTF8,
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };
        //}
        public JsonResult GetCollegeDropDownList(int Id, int trustid)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "BasicDataDetails/GetCollageDropDownListbytrustId?Id=" + Id + "&trustid=" + trustid);
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
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objUsermaster, Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //[HttpPost]
        //public ActionResult SaveDetails(AddCourseBO trg)
        //{
        //    try
        //    {
        //        var userdetailsSession = (UserModelSession)Session["UserDetails"];
        //        party.ParentId = userdetailsSession.PartyId;
        //        var json = JsonConvert.SerializeObject(trg);
        //        var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/CourseDetailConfigure");
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("cache-control", "no-cache");
        //        request.AddParameter("application/json", json, ParameterType.RequestBody);
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddHeader("Accept", "application/json");
        //        IRestResponse response = client.Execute(request);
        //        if (response.StatusCode.ToString() == "OK")
        //        {
        //            var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
        //            TempData["isSaved"] = 1;
        //            TempData["msg"] = " Details Saved...";
        //            return RedirectToAction("CreateDetails", "AddCourse");
        //        }
        //        else
        //        {
        //            TempData["isSaved"] = 0;
        //            TempData["msg"] = " Details Not Saved...";
        //            return RedirectToAction("CreateDetails", "AddCourse");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return RedirectToAction("CreateDetails");
        //}

        public ActionResult SaveDetails(AddCourseBO trg)  // both insert and update 
        {
            trg.SubjectName = Request["TagSubject"];

            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];

                //int? lat = string.IsNullOrEmpty(Request.Form["iPk_AddCourseId"])
                //? (int?)null : Convert.ToInt32(Request.Form["iPk_AddCourseId"]);
                int lat = Convert.ToInt32(Request["Id"]);
                if (lat != 0)
                {
                    trg.iPk_AddCourseId = lat;
                    var json = JsonConvert.SerializeObject(trg);

                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseDat/UpdateEditDetails?id=" + trg.iPk_AddCourseId);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddParameter("application/json", json, ParameterType.RequestBody);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                        TempData["isSaved"] = 1;
                        TempData["msg"] = "Update Details Saved...";
                        return RedirectToAction("CreateDetails", "AddCourse");
                    }
                    else
                    {
                        TempData["isSaved"] = 0;
                        TempData["msg"] = "Update Details Not Saved...";
                        return RedirectToAction("CreateDetails", "AddCourse");
                    }
                }
                else
                {
                    var json = JsonConvert.SerializeObject(trg);

                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/CourseDetailConfigure");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddParameter("application/json", json, ParameterType.RequestBody);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                        if (objResponse.Message == "Alreday exists Course")
                        {
                            TempData["isSaved"] = 0;
                            TempData["msg"] = "Alreday exists Course...";
                            return RedirectToAction("CreateDetails", "AddCourse");
                        }
                        else
                        {
                            TempData["isSaved"] = 1;
                            TempData["msg"] = " Details Saved...";
                            return RedirectToAction("CreateDetails", "AddCourse");
                        }
                    }
                    else
                    {
                        TempData["isSaved"] = 0;
                        TempData["msg"] = " Details Not Saved...";
                        return RedirectToAction("CreateDetails", "AddCourse");
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("CreateDetails");
        }

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
        }

        public JsonResult DeleteCour_Details(int id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "AddCourseDat/DeleteCourse?id=" + id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var Subjectlisting = new List<ResponseData>();
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                //if (d.Data != null)
                //    Subjectlisting = JsonConvert.DeserializeObject< ResponseData> (d.Data.ToString());

                return new JsonResult
                {
                    Data = new { StatusCode = d.statusCode, Data = "", Failure = false, Message = "Delete Row Successfully!" },
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

        public JsonResult GetEditDetails(int id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "AddCourseDat/GetEdit?id=" + id);
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

                return new JsonResult
                {
                    Data = new { StatusCode = d.statusCode, Data = Subjectlisting, Failure = false, Message = d.Message },
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

        public JsonResult GetSubjectetails(int id)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "AddCourseData/GetSubjectCourse?id=" + id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<CustomMaster> objResponseData = new List<CustomMaster>();
            if (response.StatusCode.ToString() == "OK")
            {


                objResponseData = JsonConvert.DeserializeObject<List<CustomMaster>>(response.Content);

                //var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                //if (d.Data != null)
                //    //var objResponseData = JsonConvert.DeserializeObject<List<ListCustom>>(d.Data.ToString());
                //var objResponseData = JsonConvert.DeserializeObject<ListCustom>(d.Data.ToString());

                objUsermaster = objResponseData;

                return new JsonResult
                {
                    Data = new { StatusCode = "1", Data = objUsermaster.ToArray(), Failure = false, Message = "suucess" },
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


        public ActionResult UserDetails()
        {
            return View();
        }
        //Subject LIst
        public List<CustomMaster> GetSubjectDropDownList()
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "AddCourseData/GetSubjectDropDownList");
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


        public JsonResult GetCoursesForDept(int deptID, string Type = "Course")
        {
            var courseList = ZapurseCommonlist.GetCoursesForDept(deptID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = courseList, Failure = false, Message = "Course List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        public JsonResult GetNewCourse(int colId, int couId, string DataType, string subjectlist = null)
        {
            List<Dropdown> objUsermaster = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "BasicDataDetails/GetSUbject?colId=" + colId + "&couId=" + couId + "&DataType=" + DataType + "&subjectlist=" + subjectlist);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    objUsermaster = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objUsermaster, Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
