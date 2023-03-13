using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using static NewZapures_V2.Models.Common;

namespace NewZapures_V2.Controllers
{
    public class CollegeAmendmentController : Controller
    {
        // GET: CollegeAmendment
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        //public static IConfiguration _configuration;
        //string connectionString = DBCS.GetConnectionString();
        public ActionResult DetailList(int id = 0)
        {
            if (id > 0)
            {
                List<CustomMaster> TrustList = new List<CustomMaster>();
                TrustList = GetTrustDropDownList(28);
                ViewBag.TrustList = TrustList;
                string type = Request.QueryString["CollegeType"].ToString();

                TempData["clgID"] = id;
                TempData["CollegeType"] = type;
                if (type == "General Co-ed")
                {
                    ViewBag.type = "General Girls";
                }
                else if (type == "General Girls")
                {
                    ViewBag.type = "General Co-ed";
                }
                else if (type == "Law Co-ed")
                {
                    ViewBag.type = "Law Co-ed";
                }
                #region Get District
                List<Dropdown> DistrictList = new List<Dropdown>();
                DistrictList = Common.GetLocationDropDown("AmendmentDistrict");
                ViewBag.DistrictList = DistrictList;
                #endregion
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetEdit?Id=" + id);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                _JsonSerializer.MaxJsonLength = Int32.MaxValue;
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    List<CollegeAmendmentListEdit> _result = _JsonSerializer.Deserialize<List<CollegeAmendmentListEdit>>(response.Content);
                    //CollegeAmendmentListEdit _result = _JsonSerializer.Deserialize<CollegeAmendmentListEdit>(response.Content);
                    if (_result != null)
                    {

                        ViewBag.amendmentlistedit = _result;
                        //return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                string applGUID = SessionModel.ApplicantGuid;
                ViewBag.applGUID = applGUID;
                List<Dropdown> DistrictList = new List<Dropdown>();
                DistrictList = Common.GetLocationDropDown("AmendmentDistrict");
                ViewBag.DistrictList = DistrictList;

                ViewBag.CollageDetails = new BasicDetailsBO();
                //CollageList();
                //ViewBag.CollegeId = Index(applGUID);
            }
            AmdmentList(id);
            AmdmentNameOfCollegeList(id);
            AmdmentCollegePlaceChangeList(id);
            AmdmentManagementCollegeList(id);
            AmdmentMergerDetailsList(id);
            return View();
        }

        public JsonResult GetTehsilList(string DistrictId)
        {
            try
            {
                var id = Convert.ToInt32(DistrictId);
                List<Dropdown> TehsilList = new List<Dropdown>();
                TehsilList = Common.GetLocationDropDown("Tehsil", id);
                var List = TehsilList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
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


        // College List Data
        #region CollegeListData
        [HttpGet]
        public ActionResult Index(string applGUID)
        {
            applGUID = SessionModel.ApplicantGuid;
            ViewBag.applGUID = applGUID;
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            CollegeAmendmentList result = new CollegeAmendmentList();
            //result.PartyId = RegNo.;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/AmendmentCollege?GUID=" + applGUID);
            //  var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/AmendmentCollege");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<CollegeAmendmentList> _result = _JsonSerializer.Deserialize<List<CollegeAmendmentList>>(response.Content);

                // CollegeAmendmentList _result = _JsonSerializer.Deserialize<CollegeAmendmentList>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollegeAmendmentList = _result;
                    //ViewBag.CollegeId = _result[0].iPk_ClgID;
                    //ViewBag.CollegeAmendmentList = objResponse;
                    //return RedirectToAction("Index");
                }

            }
            return View();
        }
        #endregion

        #region Attchment
        [HttpPost]
        public JsonResult SaveDetails(AmendmentBO trg)
        {
            var clgID = TempData["clgID"];
            int idclgID = Convert.ToInt32(clgID);
            try
            {
                //var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                trg.trusID = SessionModel.TrustId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/UploadDoc");
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
                        Data = new { StatusCode = objResponse.statusCode, Failure = true, Message = objResponse.Message },
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

        [HttpPost]
        public JsonResult NameCollege(AmendmentBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                trg.trusID = SessionModel.TrustId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/UploadNameCollege");
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
                        Data = new { StatusCode = objResponse.statusCode, Failure = true, Message = objResponse.Message },
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
        //Managment Changes

        [HttpPost]
        public JsonResult Managment(AmendmentBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                trg.trusID = SessionModel.TrustId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/ManagmentDocDetails");
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
                        Data = new { StatusCode = objResponse.statusCode, Failure = true, Message = objResponse.Message },
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
        //Merger Applicant College Information

        [HttpPost]
        public JsonResult MergerDetailsPlace(AmendmentBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                trg.trusID = SessionModel.TrustId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/MergerDocPlace");
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
                        Data = new { StatusCode = objResponse.statusCode, Failure = true, Message = objResponse.Message },
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


        //Merger collegeinformation
        [HttpPost]
        public JsonResult MergerDetails(AmendmentBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //trg.PartyId = userdetailsSession.PartyId;
                trg.trusID = SessionModel.TrustId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/MergerDocDetails");
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
                        Data = new { StatusCode = objResponse.statusCode, Failure = true, Message = objResponse.Message },
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
        #endregion

        //For Grid View
        public ActionResult ViewDetail()
        {
            var clgID = TempData["clgID"];
            int idclgID = Convert.ToInt32(clgID);
            ViewBag.idclgID = idclgID;
            var CollegeType = TempData["CollegeType"];
            ViewBag.CollegeType = CollegeType;
            AmdmentList(idclgID);
            AmdmentNameOfCollegeList(idclgID);
            AmdmentCollegePlaceChangeList(idclgID);
            AmdmentManagementCollegeList(idclgID);
            AmdmentMergerDetailsList(idclgID);
            return View();
        }
        //Woman Co-education
        [HttpPost]
        public List<AmendmentBO> AmdmentList(int idclgID)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/AmdList?idclgID=" + idclgID);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                List<AmendmentBO> obj = new List<AmendmentBO>();
                if (response.StatusCode.ToString() == "OK")
                {
                    List<AmendmentBO> _result = _JsonSerializer.Deserialize<List<AmendmentBO>>(response.Content);
                    if (_result != null)
                    {
                        //obj = JsonConvert.DeserializeObject<List<AmendmentBO>>(objResponse.Data.ToString());
                        ViewBag.CoEduDataList = _result;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get Details NameOf College
        [HttpPost]
        public List<AmendmentBO> AmdmentNameOfCollegeList(int idclgID)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/NameCollegeList?idclgID=" + idclgID);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                List<AmendmentBO> obj = new List<AmendmentBO>();
                if (response.StatusCode.ToString() == "OK")
                {
                    List<AmendmentBO> _result = _JsonSerializer.Deserialize<List<AmendmentBO>>(response.Content);
                    if (_result != null)
                    {
                        //obj = JsonConvert.DeserializeObject<List<AmendmentBO>>(objResponse.Data.ToString());
                        ViewBag.NameOfCollegeList = _result;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get Details College place Chnage
        [HttpPost]
        public List<AmendmentBO> AmdmentCollegePlaceChangeList(int idclgID)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/CollegePlaceList?idclgID=" + idclgID);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                List<AmendmentBO> obj = new List<AmendmentBO>();
                if (response.StatusCode.ToString() == "OK")
                {
                    List<AmendmentBO> _result = _JsonSerializer.Deserialize<List<AmendmentBO>>(response.Content);
                    if (_result != null)
                    {
                        //obj = JsonConvert.DeserializeObject<List<AmendmentBO>>(objResponse.Data.ToString());
                        ViewBag.CollegePlaceChnageList = _result;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get Details Managemnt College
        [HttpPost]
        public List<AmendmentBO> AmdmentManagementCollegeList(int idclgID)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/ManagementCollegeList?idclgID=" + idclgID);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                List<AmendmentBO> obj = new List<AmendmentBO>();
                if (response.StatusCode.ToString() == "OK")
                {
                    List<AmendmentBO> _result = _JsonSerializer.Deserialize<List<AmendmentBO>>(response.Content);
                    if (_result != null)
                    {
                        //obj = JsonConvert.DeserializeObject<List<AmendmentBO>>(objResponse.Data.ToString());
                        ViewBag.ManagementCollegeList = _result;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get Details Merger College
        [HttpPost]
        public List<AmendmentBO> AmdmentMergerDetailsList(int idclgID)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/MergerDetailsList?idclgID=" + idclgID);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                List<AmendmentBO> obj = new List<AmendmentBO>();
                if (response.StatusCode.ToString() == "OK")
                {
                    List<AmendmentBO> _result = _JsonSerializer.Deserialize<List<AmendmentBO>>(response.Content);
                    if (_result != null)
                    {
                        //obj = JsonConvert.DeserializeObject<List<AmendmentBO>>(objResponse.Data.ToString());
                        ViewBag.MergerDetailsList = _result;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Download Doc
        //Woman Co-Education
        public ActionResult DownloadDocuments(int id)
        {
            //string applGUID = SessionModel.ApplicantGuid;            
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/DocumentDetail?Id=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                //AddCourseBO _result = _JsonSerializer.Deserialize<>(response.Content);
                AmendmentBO _result = _JsonSerializer.Deserialize<AmendmentBO>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.DocumentContent.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.DocumentContent;
                    Response.AppendHeader("content-disposition", "attachment; filename=Documents." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.docFile);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            return View();
            //m_RequestMPRInternDetail item = Context.m_RequestMPRInternDetail.Where(s => s.RequestMPRInternDetailId == id).FirstOrDefault();

        }
        //Change in Name of College
        public ActionResult DownloadNameOfCollege(int id)
        {
            //string applGUID = SessionModel.ApplicantGuid;            
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/NameCollegeDetail?Id=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                //AddCourseBO _result = _JsonSerializer.Deserialize<>(response.Content);
                AmendmentBO _result = _JsonSerializer.Deserialize<AmendmentBO>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.DocumentContent.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.DocumentContent;
                    Response.AppendHeader("content-disposition", "attachment; filename=Documents." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.docFile);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            return View();
            //m_RequestMPRInternDetail item = Context.m_RequestMPRInternDetail.Where(s => s.RequestMPRInternDetailId == id).FirstOrDefault();

        }
        //College place chnage Name
        public ActionResult DownloadCollegePlaceChnageDetail(int id)
        {
            //string applGUID = SessionModel.ApplicantGuid;            
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/CollegePlaceChnageDetail?Id=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                //AddCourseBO _result = _JsonSerializer.Deserialize<>(response.Content);
                AmendmentBO _result = _JsonSerializer.Deserialize<AmendmentBO>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.DocumentContent.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.DocumentContent;
                    Response.AppendHeader("content-disposition", "attachment; filename=Documents." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.docFile);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            return View();
            //m_RequestMPRInternDetail item = Context.m_RequestMPRInternDetail.Where(s => s.RequestMPRInternDetailId == id).FirstOrDefault();

        }
        //CollegeMangement
        public ActionResult DownloadCollegeManagment(int id)
        {
            //string applGUID = SessionModel.ApplicantGuid;            
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/ManagementCollegeDetail?Id=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                //AddCourseBO _result = _JsonSerializer.Deserialize<>(response.Content);
                AmendmentBO _result = _JsonSerializer.Deserialize<AmendmentBO>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.DocumentContent.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.DocumentContent;
                    Response.AppendHeader("content-disposition", "attachment; filename=Documents." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.docFile);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            return View();
            //m_RequestMPRInternDetail item = Context.m_RequestMPRInternDetail.Where(s => s.RequestMPRInternDetailId == id).FirstOrDefault();

        }
        //Merger Information
        public ActionResult DownloadMergerDetails(int id)
        {
            //string applGUID = SessionModel.ApplicantGuid;            
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/MergerApplicantDetail?Id=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                //AddCourseBO _result = _JsonSerializer.Deserialize<>(response.Content);
                AmendmentBO _result = _JsonSerializer.Deserialize<AmendmentBO>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.DocumentContent.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.DocumentContent;
                    Response.AppendHeader("content-disposition", "attachment; filename=Documents." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.docFile);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            return View();
            //m_RequestMPRInternDetail item = Context.m_RequestMPRInternDetail.Where(s => s.RequestMPRInternDetailId == id).FirstOrDefault();

        }

        #region updateDocument
        [HttpPost]
        public JsonResult UpdateDetails(AmendmentBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/UpdateDocDetail");
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


        }
        #endregion

        #region update Merger Document
        [HttpPost]
        public JsonResult UpdateMergerDetails(AmendmentBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CollegeAmendmentDetails/UpdateDocDetailMerger");
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
                        Data = new { StatusCode = objResponse.statusCode, Failure = true, Message = objResponse.Message },
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


        }
        #endregion


    }
}