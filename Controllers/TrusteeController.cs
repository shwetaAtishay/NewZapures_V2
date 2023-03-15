using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using static NewZapures_V2.Models.TrusteeBO;

namespace NewZapures_V2.Controllers
{
    public class TrusteeController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        ResponseData objResponse;
        // GET: Trustee
        public ActionResult Index()
        {
            List<CustomMaster> TrustList = new List<CustomMaster>();

            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;


            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(29);
            ViewBag.RoleType = RoleType;

            List<CustomMaster> Occupation = new List<CustomMaster>();
            Occupation = Common.GetCustomMastersList(49);
            ViewBag.Occupation = Occupation;



            #region List Collage Apply List
            var clients = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/CollageListApply?TrustId=" + SessionModel.TrustId);
            var requests = new RestRequest(Method.GET);
            requests.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            requests.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = clients.Execute(requests);
            List<TrusteeBO.CollageList> result = new List<CollageList>();

            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.CollageList> _result = _JsonSerializer.Deserialize<List<TrusteeBO.CollageList>>(response.Content);
                if (_result != null)
                {
                    ViewBag.collegelist = _result;
                    //return RedirectToAction("Index");
                }

            }
            #endregion


            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrusteeList?TrustId=" + SessionModel.TrustId.ToString());
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            IRestResponse responses = client.Execute(request);
            if (responses.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.Trustee> _result = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(responses.Content);
                if (_result != null)
                {
                    ViewBag.TrusteeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        [HttpPost]
        public ActionResult Index(TrusteeBO.Trustee obj, HttpPostedFileBase aadhaarfile, HttpPostedFileBase panfile, HttpPostedFileBase profilefile, HttpPostedFileBase Authfile,
            HttpPostedFileBase Educationfile, HttpPostedFileBase Letterfile, HttpPostedFileBase signaturefile)
        {
            obj.TrustInfoId = SessionModel.TrustId;
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Aadhaar
            if (aadhaarfile != null)
            {
                extension = Path.GetExtension(aadhaarfile.FileName);
                ContentType = aadhaarfile.ContentType;
                using (Stream inputStream = aadhaarfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Aadhaar = Convert.ToBase64String(Documentbyte);
                    obj.AadhaarExtension = extension;
                    obj.AadhaarContentType = ContentType;
                }
            }
            #endregion
            #region Pan
            if (panfile != null)
            {
                extension = Path.GetExtension(panfile.FileName);
                ContentType = panfile.ContentType;
                using (Stream inputStream = panfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Pan = Convert.ToBase64String(Documentbyte);
                    obj.PanExtension = extension;
                    obj.PanContentType = ContentType;
                }
            }
            #endregion
            #region Profile
            if (profilefile != null)
            {
                extension = Path.GetExtension(profilefile.FileName);
                ContentType = profilefile.ContentType;
                using (Stream inputStream = profilefile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.ProfileImg = Convert.ToBase64String(Documentbyte);
                    obj.ProfileExtension = extension;
                    obj.ProfileContentType = ContentType;
                }
            }
            #endregion
            #region Authfile
            if (Authfile != null)
            {
                extension = Path.GetExtension(Authfile.FileName);
                ContentType = aadhaarfile.ContentType;
                using (Stream inputStream = Authfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Authorized = Convert.ToBase64String(Documentbyte);
                    obj.AuthorizedExtension = extension;
                    obj.AuthorizedContentType = ContentType;
                }
            }
            #endregion

            #region Eductionfile
            if (Educationfile != null)
            {
                extension = Path.GetExtension(Educationfile.FileName);
                ContentType = Educationfile.ContentType;
                using (Stream inputStream = Educationfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Educationfile = Convert.ToBase64String(Documentbyte);
                    obj.EducationfileExtension = extension;
                    obj.EducationfileContentType = ContentType;
                }
            }
            #endregion

            #region Laterfile
            if (Letterfile != null)
            {
                extension = Path.GetExtension(Letterfile.FileName);
                ContentType = Letterfile.ContentType;
                using (Stream inputStream = Letterfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Letterfile = Convert.ToBase64String(Documentbyte);
                    obj.LetterfileExtension = extension;
                    obj.LetterfileContentType = ContentType;
                }
            }
            #endregion

            #region Signaturefile
            if (signaturefile != null)
            {
                extension = Path.GetExtension(signaturefile.FileName);
                ContentType = signaturefile.ContentType;
                using (Stream inputStream = signaturefile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.signaturefile = Convert.ToBase64String(Documentbyte);
                    obj.signaturefileExtension = extension;
                    obj.signaturefileContentType = ContentType;
                }
            }
            #endregion

            #region Add Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddTrustee");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    if (objResponseData.Messsage == "Alreday exists iIsPrimary")
                    {
                        TempData["isSavedexists"] = 1;
                        TempData["Messageexists"] = "Alreday exists iIsPrimary!";

                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = "Data saved sussessfully!";
                        TempData["SwalTitleMsg"] = "Success...!";
                    }

                    //return RedirectToAction("Index");
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    //return RedirectToAction("Index");
                }
            }
            #endregion

            #region List Trustee
            client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrusteeList?TrustId=" + obj.TrustInfoId);
            request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.Trustee> _result = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(response.Content);
                if (_result != null)
                {
                    ViewBag.TrusteeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTrustMemeber(int Id)
        {
            #region Delete Trust Memeber
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/DeleteTrustMemeber?Id=" + Id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
            if (objResponseData.ResponseCode == "1")
            {
                TempData["SwalStatusMsg"] = "success";
                TempData["SwalMessage"] = "Deleted Successfully!!";
                TempData["SwalTitleMsg"] = "Success...!";
                //return RedirectToAction("Index");
            }
            else
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                //return RedirectToAction("Index");
            }
            #endregion
            return RedirectToAction("Index");
        }
        public ActionResult DownloadDocuments(int id)
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/DocumentDetail?Id=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                TrusteeBO.Trustee _result = _JsonSerializer.Deserialize<TrusteeBO.Trustee>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.AadhaarContentType.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.AadhaarContentType;
                    Response.AppendHeader("content-disposition", "attachment; filename=Documents." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.Aadhaar);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            return View();
            #endregion
            //m_RequestMPRInternDetail item = Context.m_RequestMPRInternDetail.Where(s => s.RequestMPRInternDetailId == id).FirstOrDefault();

        }
        public ActionResult DraftApplication()
        {
            var trusID = SessionModel.TrustId;
            var draftApplications = ZapurseCommonlist.GetDraftApplication("", trusID);
            ViewBag.draftApplication = draftApplications;
            return View();
        }

        //Added For Saving the Darft application by Vivek 10.02.2023
        public ActionResult CollegeDetails()
        {
            var trusID = SessionModel.TrustId;
            var CollegeList = ZapurseCommonlist.GetClgListForDepartment("8", "GetCollegeForDepartment", trusID);
            ViewBag.clgList = CollegeList;
            return View();
        }

        public ActionResult EditDetails(string guid = "", int clgId = 0, int deptID = 0, string clgStatus = "")
        {
            SessionModel.ApplicantGuid = guid;
            ViewBag.guid = guid;
            ViewBag.clgId = clgId;
            ViewBag.departmentID = deptID;
            ViewBag.clgStatus = clgStatus;

            ViewData["DeptId"] = deptID;
            return View();
        }
        public ActionResult DeptMasterApplicationList()
        {

            var departmentMstApplicationList = ZapurseCommonlist.GetDeptMasterApplication();
            ViewBag.deptMstList = departmentMstApplicationList;
            return View();
        }

        [HttpPost]
        public JsonResult SaveDeptMst(List<DeptMasterApp> masterApp)
        {

            //var trusID = SessionModel.TrustId;
            //var trusName = SessionModel.TrustName;

            //foreach(var app in masterApp)
            //{
            //    app.trustID = trusID;
            //    app.trustName = trusName;
            //}

            var json = JsonConvert.SerializeObject(masterApp);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/AddDeptMSTAppData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SaveDeptMstNewCollege(int clgID, string clgName, string appType)
        {

            //var trusID = SessionModel.TrustId;
            //var trusName = SessionModel.TrustName;

            //foreach(var app in masterApp)
            //{
            //    app.trustID = trusID;
            //    app.trustName = trusName;
            //}

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/AddDeptMSTAppDataNewCollege?clgID=" + clgID + "&clgName=" + clgName + "&appType=" + appType);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult getNewCollegeDetails(int clgID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetNewCollegeDetails?clgID=" + clgID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            NewCollegeDetails newCollege = new NewCollegeDetails();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                if (objResponse.Data != null)
                {
                    var data = JsonConvert.SerializeObject(objResponse.Data);
                    newCollege = JsonConvert.DeserializeObject<NewCollegeDetails>(data);
                }
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = newCollege, Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult UploadFeeRecipt()
        {
            var draftApplications = ZapurseCommonlist.GetApplicationsToUploadReceipt();
            ViewBag.draftApplication = draftApplications;
            return View();
        }
        public JsonResult UploadReceipt(UploadReceipt receipt)
        {
            var json = JsonConvert.SerializeObject(receipt);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/UploadReceipt");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult CancelDraftApplication(string applGUID, string Creason)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/CancleDarftApplications?applGUID=" + applGUID + "&Creason=" + Creason);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        //public ActionResult EditApplication(string applicationNo, string trustName, int trustID, string clgName, string dptname, string cours, int deptID, int courseID,int clgID)
        public ActionResult EditApplication(string applGUID)
        {
            var EditdraftedApplications = ZapurseCommonlist.GetDraftApplication(applGUID);
            ViewBag.applicationDetails = EditdraftedApplications[0];
            var trusteeMember = ZapurseCommonlist.GetTrusteeMember(EditdraftedApplications[0].iFKTst_ID);
            //var LandData = ZapurseCommonlist.GetLandData(EditdraftedApplications[0].ApplGuid);
            //ViewBag.LandDetails = LandData;
            ViewBag.trusteeMember = trusteeMember;
            SessionModel.ApplicantGuid = applGUID;
            return View();
        }

        public ActionResult ApplicationPreview(string applGUID)
        {
            var EditdraftedApplications = ZapurseCommonlist.GetDraftApplication(applGUID);
            ViewBag.applicationDetails = EditdraftedApplications[0];
            //Data To Preview

            var trusteeMember = ZapurseCommonlist.GetTrusteeMember(EditdraftedApplications[0].iFKTst_ID);
            var LandData = ZapurseCommonlist.GetLandBuildingInfo(applGUID);
            var AcadmicData = ZapurseCommonlist.GetAcdmcData(applGUID);
            var subjectData = ZapurseCommonlist.GetSubjectList(applGUID);

            ViewBag.trusteeMember = trusteeMember;
            ViewBag.landDataList = LandData;
            ViewBag.AcadmicDataList = AcadmicData;
            ViewBag.subjectDataList = subjectData;
            return View();
        }

        public ActionResult Draftinspaction(string applGUID)
        {
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var EditdraftedApplications = ZapurseCommonlist.GetAdminApplication(applGUID);
            ViewBag.applicationDetails = EditdraftedApplications[0];
            var trusteeMember = ZapurseCommonlist.GetTrusteeMember(EditdraftedApplications[0].iFKTst_ID);
            //var LandData = ZapurseCommonlist.GetLandData(EditdraftedApplications[0].ApplGuid);
            //ViewBag.LandDetails = LandData;
            SessionModel.ApplicantGuid = applGUID;
            ViewBag.trusteeMember = trusteeMember;
            ViewBag.usrid = servicesCollectiondata.PartyId;
            SessionModel.ApplicantGuid = applGUID;
            return View();
        }
        public ActionResult ApplyNOCApplicationNew1()
        {
            var departmentList = ZapurseCommonlist.GetDepartmentlist();
            ViewBag.departments = departmentList;
            return View();
        }
        public ActionResult ApplyNOCApplicationNew()
        {
            var departmentList = ZapurseCommonlist.GetDepartmentlist();
            ViewBag.departments = departmentList;
            return View();
        }
        public ActionResult ApplyNOCApplication()
        {
            var departmentList = ZapurseCommonlist.GetDepartmentlist();
            ViewBag.departments = departmentList;
            return View();
        }

        //public ActionResult CollageAttachment()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult TrusteeGeneralInfo()
        {
            string RegNo = SessionModel.TrustRegNo;
            bool status = false;
            #region Get Trust Information
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetTrustInfo?TrustId=" + RegNo);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                TrusteeBO.TrusteeInfo _result = _JsonSerializer.Deserialize<TrusteeBO.TrusteeInfo>(response.Content);
                if (_result != null)
                {
                    if (_result.RegistrationNo != null)
                    {
                        status = true;
                        _result.SSOID = SessionModel.SSOUserDetail.SSOID;
                        ViewBag.TrustDetails = _result;
                        ViewData["RegDate"] = _result.RegistrationDate;
                        ViewData["ElectionDate"] = _result.ElectionDate;
                        SessionModel.TrustId = _result.TrusteeInfoId;
                        SessionModel.TrustName = _result.Name;
                    }
                }
            }
            #endregion

            TrustRoot _trustapi = new TrustRoot();
            //modal.RegistrationNo = "COOP/2019/ALWAR/100658";
            if (!status)
            {
                #region Trust API
                client = new RestClient("https://rajsahakarapp.rajasthan.gov.in/api/EntireSocietyDetail/GetSocietyDetailsByRegistrationNO?Reg_no=" + RegNo);
                client.Timeout = -1;
                request = new RestRequest(Method.GET);
                response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    _trustapi = _JsonSerializer.Deserialize<TrustRoot>(response.Content);
                    if (_trustapi.Status == "200" && _trustapi.Message == "Success")
                    {
                        ErrorBO _ress = Verificationdata(_trustapi);
                        if (_ress.ResponseCode == "1")
                        {
                            #region List Trustee
                            client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetTrustInfo?TrustId=" + _trustapi.Data.RegistrationNo);
                            request = new RestRequest(Method.GET);
                            request.AddHeader("cache-control", "no-cache");
                            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                            request.AddParameter("application/json", "", ParameterType.RequestBody);
                            response = client.Execute(request);
                            if (response.StatusCode.ToString() == "OK")
                            {
                                TrusteeBO.TrusteeInfo _result = _JsonSerializer.Deserialize<TrusteeBO.TrusteeInfo>(response.Content);
                                if (_result != null)
                                {
                                    _result.SSOID = SessionModel.SSOUserDetail.SSOID;
                                    ViewBag.TrustDetails = _result;
                                    ViewData["RegDate"] = _result.RegistrationDate;
                                    ViewData["ElectionDate"] = _result.ElectionDate;
                                    SessionModel.TrustId = _result.TrusteeInfoId;
                                    SessionModel.TrustName = _result.Name;
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        return new JsonResult
                        {
                            Data = new { Success = false, Message = "Enter Correct Registration Number", res = _trustapi },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
                #endregion
            }
            return View();
        }
        public List<CustomMaster> GetTrustDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetTrustDropDownList");
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

        [HttpPost]
        public JsonResult TrusteeGeneralInfo(TrusteeBO.TrusteeInfo obj)
        {

            obj.TrusteeInfoId = SessionModel.TrustId;
            #region Add Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddTrusteeInfo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    return new JsonResult
                    {
                        Data = new { StatusCode = objResponseData.ResponseCode, Failure = true },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    //TempData["SwalStatusMsg"] = "error";
                    //TempData["SwalMessage"] = "Something wrong";
                    //TempData["SwalTitleMsg"] = "error!";
                    //return RedirectToAction("Index");
                    return new JsonResult
                    {
                        Data = new { StatusCode = objResponseData.ResponseCode, Failure = false },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            #endregion

            List<CustomMaster> TrusteeType = new List<CustomMaster>();
            TrusteeType = Common.GetCustomMastersList(31);
            ViewBag.TrusteeType = TrusteeType;
            return new JsonResult
            {
                Data = new { StatusCode = 0, Failure = false },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return View();
        }
        [HttpGet]
        public ActionResult TrustList()
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrustInfoList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.TrusteeInfo> _result = _JsonSerializer.Deserialize<List<TrusteeBO.TrusteeInfo>>(response.Content);
                if (_result != null)
                {
                    ViewBag.TrusteeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        [HttpGet]
        public ActionResult CollageListForApply()
        {
            #region List Collage Apply List
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/CollageListApply?TrustId=" + SessionModel.TrustId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.CollageList> _result = _JsonSerializer.Deserialize<List<TrusteeBO.CollageList>>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageApplyList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        [HttpGet]
        public ActionResult CollageFacilitys()
        {
            TrusteeBO.CollageFacility modal = new TrusteeBO.CollageFacility();
            modal.Guid = SessionModel.ApplicantGuid;
            //ViewBag.Guid = eGuid;
            List<CustomMaster> CourseList = new List<CustomMaster>();
            CourseList = ZapurseCommonlist.GetCourseListDropDown(modal.Guid);
            ViewBag.CourseList = CourseList;
            ViewBag.Facilites = GetDept();
            ViewBag.FeeDetails = GetDetailsList();
            //ViewBag.Guid = eGuid;
            List<TrusteeBO.Trustee> trustees = new List<TrusteeBO.Trustee>();
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetCollageFacilityList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                TrusteeBO.CollageFacility _result = _JsonSerializer.Deserialize<TrusteeBO.CollageFacility>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageFacilityMster = _result;
                    //return RedirectToAction("Index");
                }
            }
            ViewBag.TrusteeList = trustees;
            #endregion

            //Collage Faciliy Master from Enum
            //List<CustomMaster> CollageFacilityMster = new List<CustomMaster>();
            //CollageFacilityMster = Common.GetCustomMastersList(35);
            //ViewBag.CollageFacilityMster = CollageFacilityMster;
            //ViewData["TrustId"] = "0";
            //ViewData["CollageId"] = "0";

            //ViewBag.CollageFacilityMster = new List<CustomMaster>();

            //Trust List 
            //List<CustomMaster> TrustList = new List<CustomMaster>();
            //TrustList = GetTrustDropDownList(28);
            //ViewBag.TrustList = TrustList;


            //List<CustomMaster> RoleType = new List<CustomMaster>();
            //RoleType = Common.GetCustomMastersList(29);
            //ViewBag.RoleType = RoleType;

            //#region List Trustee
            //var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrusteeList");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("cache-control", "no-cache");
            ////request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //request.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //if (response.StatusCode.ToString() == "OK")
            //{
            //    List<TrusteeBO.Trustee> _result = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(response.Content);
            //    if (_result != null)
            //    {
            //        ViewBag.TrusteeList = _result;
            //        //return RedirectToAction("Index");
            //    }
            //}
            //#endregion
            return View();
        }

        [HttpPost]
        public ActionResult CollageFacilitys(TrusteeBO.CollageFacility modal)
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetCollageFacilityList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                TrusteeBO.CollageFacility _result = _JsonSerializer.Deserialize<TrusteeBO.CollageFacility>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageFacilityMster = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            //Collage Faciliy Master from Enum
            //List<CustomMaster> CollageFacilityMster = new List<CustomMaster>();
            //CollageFacilityMster = Common.GetCustomMastersList(35);
            //ViewBag.CollageFacilityMster = CollageFacilityMster;

            ViewData["TrustId"] = 0; //modal.TrustId;
            ViewData["CollageId"] = 0; //modal.CollageId;
            //Trust List 
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;


            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(29);
            ViewBag.RoleType = RoleType;



            List<TrusteeBO.Trustee> trustees = new List<TrusteeBO.Trustee>();
            #region List Trustee
            client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrusteeList");
            request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                trustees = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(response.Content);
                //if (_result != null)
                //{
                //    ViewBag.TrusteeList = _result;
                //    //return RedirectToAction("Index");
                //}
            }
            ViewBag.TrusteeList = trustees;
            #endregion
            return View();
        }
        [HttpPost]
        public JsonResult CollageFacilitysAdd(TrusteeBO.CollageFacility modal)
        {
            modal.Guid = SessionModel.ApplicantGuid;
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddCollageFacility");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    //TempData["SwalStatusMsg"] = "success";
                    //TempData["SwalMessage"] = "Data saved sussessfully!";
                    //TempData["SwalTitleMsg"] = "Success...!";
                    return new JsonResult
                    {
                        Data = new { failure = true, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { failure = false, msg = "Failed" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            #endregion
            return new JsonResult
            {
                Data = new { failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetCollegeDropDownList(int TrustInfoId)
        {
            ResponseData objResponse = new ResponseData();
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
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

        public JsonResult GetCollageFaciltyList(int TrustInfoId)
        {
            ResponseData objResponse = new ResponseData();
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
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
        public JsonResult GetLandDetails(string AppGUID)
        {
            var LandData = ZapurseCommonlist.GetLandData(AppGUID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = LandData, Failure = false, msg = "Land Details" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult CollageAttachment(string guid)
        {
            List<CustomMaster> CourseList = new List<CustomMaster>();
            CourseList = ZapurseCommonlist.GetCourseListDropDown(SessionModel.ApplicantGuid);
            ViewBag.CourseList = CourseList;
            var draftedApplication = ZapurseCommonlist.GetDraftApplication(SessionModel.ApplicantGuid);

            ViewData["TrustId"] = draftedApplication[0].iFKTst_ID;
            ViewData["CollageId"] = draftedApplication[0].iFKCLG_ID;
            ViewData["CourseId"] = draftedApplication[0].iFK_CORS_ID;
            ViewData["DeptId"] = draftedApplication[0].iFKDEPT_ID;
            ViewData["clgStatus"] = draftedApplication[0].clgStatus;

            List<CollageAttachmentList> _result = new List<CollageAttachmentList>();
            #region List College Attachement List
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetCollageAttachment?Guid=" + SessionModel.ApplicantGuid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<CollageAttachmentList>>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollegeAttachmentList = _result;
                    //return RedirectToAction("Index");
                }
            }
            ViewBag.StaffList = _result;
            ViewBag.guid = guid;
            #endregion
            return View();
        }

        [HttpPost]
        public ActionResult CollageAttachment(TrusteeBO.CollageAttachment obj)
        {
            List<TrusteeBO.DocumentsDetails> _DOCList = new List<TrusteeBO.DocumentsDetails>();

            TrusteeBO.DocumentsDetails _DocDetails = getDocdetails(obj.affidavit, "affidavit", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.SalaryPayment, "SalaryPayment", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.Bankstatement, "Bankstatement", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.Annexure, "Annexure", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.EsiDoc, "EsiDoc", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsCnnctUnvrctyDrctnfile, "bIsCnnctUnvrctyDrctnfile", obj.bIsCnnctUnvrctyDrctn, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsTimeFrmfile, "bIsTimeFrm", obj.bIsTimeFrm, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsLadDwnfile, "bIsLadDwn", obj.bIsLadDwn, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsSffcentLandfile, "bIsSffcentLand", obj.bIsSffcentLand, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsAffidvtAsprGuidfile, "bIsAffidvtAsprGuid", obj.bIsAffidvtAsprGuid, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);


            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsOtherDocfile, "bIsOtherDoc", obj.bIsOtherDoc, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            TrusteeBO.CollageattachmentAPI _rendmodal = new TrusteeBO.CollageattachmentAPI();
            _rendmodal.bIsAffidvtAsprGuid = obj.bIsAffidvtAsprGuid;
            _rendmodal.bIsCnnctUnvrctyDrctn = obj.bIsCnnctUnvrctyDrctn;
            _rendmodal.bIsLadDwn = obj.bIsLadDwn;
            _rendmodal.bIsOtherDoc = obj.bIsOtherDoc;
            _rendmodal.bIsSffcentLand = obj.bIsSffcentLand;
            _rendmodal.bIsTimeFrm = obj.bIsTimeFrm;
            _rendmodal.iFk_TrstId = obj.iFk_TrstId;
            _rendmodal.iFk_ClgId = obj.iFk_ClgId;
            _rendmodal.iFk_CourseId = obj.iFk_CourseId;
            _rendmodal.sSSOID = obj.sSSOID;
            _rendmodal.Guid = SessionModel.ApplicantGuid;

            #region Adddata
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddCollageAttachementMain");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            //var json = _JsonSerializer.Serialize(obj);
            request.AddParameter("application/json", _JsonSerializer.Serialize(_rendmodal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = _JsonSerializer.Deserialize<ResponseData>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    if (_DOCList != null)
                    {
                        foreach (TrusteeBO.DocumentsDetails item in _DOCList)
                        {
                            item.Id = objResponseData.ID;
                            item.EnumNo = 40;
                            client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddCollageAttachementFiles");
                            request = new RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                            _JsonSerializer.MaxJsonLength = Int32.MaxValue;

                            request.AddParameter("application/json", _JsonSerializer.Serialize(item), ParameterType.RequestBody);
                            response = client.Execute(request);
                            if (response.StatusCode.ToString() == "OK")
                            {
                            }
                        }
                    }
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = "Data saved sussessfully!";
                    TempData["SwalTitleMsg"] = "Success...!";
                    return RedirectToAction("EditDetails", "Trustee", new { guid = SessionModel.ApplicantGuid, clgId = obj.iFk_ClgId, deptID = obj.iFk_DeptId, clgStatus = obj.clgStatus });
                    //return RedirectToAction("CollageAttachment",new { TrustId=obj.iFk_TrstId, CollageId=obj.iFk_ClgId,CourseId=obj.iFk_CourseId });
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    //return RedirectToAction("Index");
                }
            }
            #endregion

            return RedirectToAction("EditDetails", "Trustee", new { guid = SessionModel.ApplicantGuid });

            //return View();
        }


        public JsonResult SaveApplicationDetails(TrusteeBO.SaveApplicationModal applicationModal)
        {
            var json = JsonConvert.SerializeObject(applicationModal);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/SaveMultipleNOC");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
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

        public JsonResult ApplyApplicationDetails(TrusteeBO.SaveApplicationModal applicationModal)
        {
            var json = JsonConvert.SerializeObject(applicationModal);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/SaveApplication");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
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
        public TrusteeBO.DocumentsDetails getDocdetails(HttpPostedFileBase file, string Filetype, int Isyes, int TrustId, int CollageId, int CourseId)
        {
            TrusteeBO.DocumentsDetails _result = new TrusteeBO.DocumentsDetails();
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Certified Document
            if (file != null)
            {
                extension = Path.GetExtension(file.FileName);
                ContentType = file.ContentType;
                using (Stream inputStream = file.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    _result.file = Convert.ToBase64String(Documentbyte);
                    _result.Extension = extension;
                    _result.Contenttype = ContentType;
                    _result.Filetype = Filetype;
                    _result.Isyes = Isyes;
                    _result.TrustId = TrustId;
                    _result.CollageId = CollageId;
                    _result.CourseId = CourseId;
                }
            }
            #endregion
            return _result;
        }

        public ErrorBO Verificationdata(TrustRoot modal)
        {
            ErrorBO _res = new ErrorBO();
            #region VerifyDetails
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrustVerificationAPI");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _res = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (_res.ResponseCode == "1")
                {
                    //TempData["SwalStatusMsg"] = "success";
                    //TempData["SwalMessage"] = "Data saved sussessfully!";
                    //TempData["SwalTitleMsg"] = "Success...!";
                    SessionModel.TrustId = _res.Id;
                    //return new JsonResult
                    //{
                    //    Data = new { Success = true, Message = objResponseData.Messsage },
                    //    ContentEncoding = System.Text.Encoding.UTF8,
                    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    //};
                }
                else
                {
                    //TempData["SwalStatusMsg"] = "error";
                    //TempData["SwalMessage"] = "Something wrong";
                    //TempData["SwalTitleMsg"] = "error!";
                    //return new JsonResult
                    //{
                    //    Data = new { Success = false, Message = objResponseData.Messsage },
                    //    ContentEncoding = System.Text.Encoding.UTF8,
                    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    //};
                }
            }
            #endregion
            return _res;
        }

        public ActionResult testpage()
        {
            return View();
        }

        public JsonResult GetNOCApplicationList(int departID)
        {
            var nocList = ZapurseCommonlist.GETNOCApplicationList(departID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GETNOCApplicationClgList(int departID)
        {
            var nocList = ZapurseCommonlist.GETNOCApplicationClgList(departID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetCollegeListForDepartment(string departID)
        {
            var trusID = SessionModel.TrustId;
            var clgList = ZapurseCommonlist.GetClgListForDepartment(departID, "GetCollegeForDepartmentNOCApply", trusID); // gets only those colleges whose entry is there in MST_APLN table
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = clgList, Failure = false, Message = "College List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetAvailableNOC(string applicationType, string deptID)
        {
            var clgList = ZapurseCommonlist.GetAvailableNOC(applicationType, "AvailableNOC", deptID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = clgList, Failure = false, Message = "Available NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetCourseForCollege(string collegeID, string type)
        {
            var nocList = ZapurseCommonlist.GetCourseForCollege(collegeID, type);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetExistingNOCForCollege(string collegeID)
        {
            var nocList = ZapurseCommonlist.GetExistingNOCForCollege(collegeID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetSubjectForCourse(string CourseID, string type, string CollegeId)
        {

            var nocList = ZapurseCommonlist.GetSubjectForCourse(CourseID, type, CollegeId);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetFee(string ApplicationId)
        {
            var nocList = ZapurseCommonlist.GetFee(ApplicationId);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetInspectionFee()
        {
            var nocList = ZapurseCommonlist.GetInspectionFee();
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetUserSendbackForward(string MenuId, string PartyId)
        {
            var rolIst = ZapurseCommonlist.GetUserSendbackForward(MenuId, PartyId);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = rolIst, Failure = false, Message = "Role List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult AddTrackingData(TrackingData track)
        {
            var json = JsonConvert.SerializeObject(track);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/AddTrackingData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
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


        [HttpPost]
        public JsonResult SaveCheckValidation()
        {
            bool result = false;
            string message = "";
            string applGUID = SessionModel.ApplicantGuid;

            List<AcdmcTableData> AcadmicData = ZapurseCommonlist.GetAcdmcData();
            List<AddCourseBO> subjectData = ZapurseCommonlist.GetSubjectList(applGUID);
            List<StaffBO.Staff> StaffData = ZapurseCommonlist.GetStaffList(applGUID);

            CollageFeeMst CollageFeeMst = ZapurseCommonlist.GetCollageFeeList(applGUID);
            var res = CollageFeeMst.rateLists.Where(x => x.IsSelect == true).ToList();
            if (AcadmicData.Count <= 0)
            {
                message = "Complete Your Profile";
                result = false;
            }
            else if (subjectData.Count <= 0)
            {
                message = "Complete Your Profile";
                result = false;
            }
            else if (StaffData.Count <= 0)
            {
                message = "Complete Your Profile";
                result = false;
            }

            else if (res.Count <= 0)
            {
                message = "Complete Your Profile";
                result = false;
            }
            else if (subjectData.Count > 0)
            {
                foreach (var itr in subjectData)
                {
                    int co = StaffData.Where(x => x.Subject == itr.SubjectName).Count();
                    if (co <= 0)
                    {
                        message = "Complete Your Profile";
                        result = false;
                    }
                }
            }
            else
            {
                message = "";
                result = true;
            }
            return new JsonResult
            {
                Data = new { StatusCode = 1, Failure = result, Message = message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public ActionResult OtherDetailsShow(string CollageId, string CourseId, string DeptId, string TrustId)
        {
            string applGUID = "";
            ExistingNOCRequest obj = new ExistingNOCRequest();
            obj.CollageId = CollageId;
            obj.CourseId = CourseId;
            obj.DepartmentId = DeptId;
            obj.TrustId = TrustId;

            var EditdraftedApplications = ZapurseCommonlist.GetDraftApplication(applGUID);
            if (EditdraftedApplications.Count > 0)
                ViewBag.applicationDetails = EditdraftedApplications[0];
            else
                ViewBag.applicationDetails = new TrusteeBO.DraftApplication();
            //Data To Preview
            var StaffData = ZapurseCommonlist.ExistingNOCStaffDetails(obj);
            var subjectData = ZapurseCommonlist.ExistingNOCGetSubjectList(obj);
            List<TrusteeMember> trusteeMember = new List<TrusteeMember>();
            if (EditdraftedApplications.Count > 0)
                trusteeMember = ZapurseCommonlist.GetTrusteeMember(EditdraftedApplications[0].iFKTst_ID);
            var LandData = ZapurseCommonlist.GetLandBuildingInfo(applGUID);
            var AcadmicData = ZapurseCommonlist.GetAcdmcData();
            //var subjectData = ZapurseCommonlist.GetStaffList(applGUID);
            //var StaffData = ZapurseCommonlist.GetStaffList(applGUID);
            var CollageFacility = ZapurseCommonlist.GetCollageFacility(applGUID);
            var CollageFeeMst = ZapurseCommonlist.GetCollageFeeList(applGUID);

            ViewBag.trusteeMember = trusteeMember;
            ViewBag.landDataList = LandData;
            ViewBag.AcadmicDataList = AcadmicData;
            ViewBag.subjectDataList = subjectData;
            ViewBag.StaffData = StaffData;
            ViewBag.CollageFacility = CollageFacility;
            ViewBag.CollageFeeList = CollageFeeMst;
            return View();
        }
        public ActionResult Departmentapplication()
        {
            var draftApplications = ZapurseCommonlist.DepartmentapplicationList();
            ViewBag.draftApplication = draftApplications;
            return View();
        }
        public ActionResult ArchitecDetails1(string AppGuid)
        {
            ViewBag.AppGuid = AppGuid;
            return View();
        }
        //public JsonResult BindCourse(string AppGuid, int CourseId = 0)
        //{
        //    List<Dropdown> departments = new List<Dropdown>();
        //    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetApplicationCourse?AppGuid=" + AppGuid + "&CourseId=" + CourseId);
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");
        //    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    request.AddParameter("application/json", "", ParameterType.RequestBody);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "application/json");
        //    IRestResponse response = client.Execute(request);
        //    if (response.StatusCode.ToString() == "OK")
        //    {

        //        ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
        //        if (objResponse.Data != null)
        //        {
        //            departments = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
        //        }
        //        if (objResponse.ResponseCode == "001")
        //        {
        //            return new JsonResult
        //            {
        //                Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
        //                ContentEncoding = System.Text.Encoding.UTF8,
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //        }
        //        else if (objResponse.ResponseCode == "000" && objResponse.statusCode == 1)
        //        {
        //            return new JsonResult
        //            {
        //                Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
        //                ContentEncoding = System.Text.Encoding.UTF8,
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };

        //        }
        //    }
        //    return new JsonResult
        //    {
        //        Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
        //        ContentEncoding = System.Text.Encoding.UTF8,
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };
        //}

        //public JsonResult InsertArchitechDetaile(List<Architecturesave> datalist)
        //{

        //    var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/InsertArchitechDetaile");
        //    var request2 = new RestRequest(Method.POST);
        //    request2.AddHeader("cache-control", "no-cache");
        //    // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    request2.AddParameter("application/json", _JsonSerializer.Serialize(datalist), ParameterType.RequestBody);
        //    IRestResponse response2 = client2.Execute(request2);
        //    if (response2.StatusCode.ToString() == "OK")
        //    {
        //        ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
        //        if (objResponseData.ResponseCode == "001")
        //        {
        //            return new JsonResult
        //            {
        //                Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
        //                ContentEncoding = System.Text.Encoding.UTF8,
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //        }
        //        else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
        //        {
        //            return new JsonResult
        //            {
        //                Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
        //                ContentEncoding = System.Text.Encoding.UTF8,
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };

        //        }
        //    }


        //    return new JsonResult
        //    {
        //        Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
        //        ContentEncoding = System.Text.Encoding.UTF8,
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };
        //}
        public JsonResult GetChangeInName(int formID, int clgid)
        {
            var nocList = ZapurseCommonlist.GetChangeInName(formID, clgid);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetChangeInPlace(int formID, int clgid)
        {
            var nocList = ZapurseCommonlist.GetChangeInPlace(formID, clgid);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetChangeInSociety(int formID, int clgid)
        {
            var nocList = ZapurseCommonlist.GetChangeInSociety(formID, clgid);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetFeeForType(int ApplicationType)
        {
            var nocList = ZapurseCommonlist.GetFeeForType(ApplicationType);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult updateFeeForApplication(string applNo, decimal totalFee)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/UpdateFeeForApplication?applicationNumber=" + applNo + "&totalFee=" + totalFee);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

            }
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetPreViewDetails(string ColgId, string DeptId, string TypeOfNOC, string CourseId)
        {
            string TrustId = SessionModel.TrustId;
            string Type = string.Empty;
            if (TypeOfNOC == "28")
            {
                Type = "ByNewCollage";
            }
            else
            {
                Type = "ByNewCourse";
            }
            string applGUID = ZapurseCommonlist.GetGuid(Type, TrustId, ColgId, DeptId, CourseId);
            //string applGUID = "";
            var EditdraftedApplications = ZapurseCommonlist.GetDraftApplication(applGUID);
            ViewBag.applicationDetails = EditdraftedApplications[0];
            //Data To Preview

            var trusteeMember = ZapurseCommonlist.GetTrusteeMember(EditdraftedApplications[0].iFKTst_ID);
            var LandData = ZapurseCommonlist.GetLandBuildingInfo(applGUID);
            var roomdeatils = ZapurseCommonlist.BindTable(applGUID, "1");
            var OtherDetails = ZapurseCommonlist.BindTable(applGUID, "2");
            //var AcdData = new List<AcdmcTableData>();
            var AcdData = ZapurseCommonlist.GetAcdmcDataNew(applGUID, int.Parse(ColgId));
            var subjectData = ZapurseCommonlist.GetSubjectList(applGUID);
            var StaffData = ZapurseCommonlist.GetStaffList(applGUID);
            var OtherFac = ZapurseCommonlist.GetDetailsList(applGUID);
            var ColgFee = ZapurseCommonlist.GetCollageFeeList(applGUID);


            #region LandDetail
            string Landhtml = string.Empty;
            int sr = 1;

            foreach (var item in LandData)
            {
                var landConvertedURL = "";
                var landTitleURL = "";
                var landDocumentURL = "";
                //var landConvertedURL = (item.UploadConvertedDocument == null || item.UploadConvertedDocument == "") ? "NA" : "<a href="+item.UploadConvertedDocument+" download = '"+item.sKhasaraNo+"_LandConvert"' target = "_blank" alt = "Red dot" >< i class="fa fa-download"></i></a>";
                //var landTitleURL = (item.UploadLandTitleDoc == null || item.UploadLandTitleDoc == '') ? "NA" : "<a href = "${item.UploadLandTitleDoc}" download="${item.sKhasaraNo}_landTitle" target="_blank" alt="Red dot"><i class="fa fa-download"></i></a>";
                //var landDocumentURL = (item.UploadLandDoc == null || item.UploadLandDoc == '') ? "NA" : "<a href = "${item.UploadLandDoc}" download="${item.sKhasaraNo}_LandDoc" target="_blank" alt="Red dot"><i class="fa fa-download"></i></a>";

                Landhtml += "<tr><td>" + sr + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Course + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.sLndArea + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.sLndDocType + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.sIsLndCnvrted + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.sOrdrNo + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.dtOrdrDate + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.sLndTyp + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.sKhasaraNo + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.slndAccureTyp + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.dLndArea + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.dTotalArea + "</td>" +
                                            "<td class='text-center' style='border-right: solid 1px #000; border-bottom: solid 1px #000;'><span>" + landConvertedURL + "</span></td>" +
                                            "<td class='text-center' style='border-right: solid 1px #000; border-bottom: solid 1px #000;'><span>" + landTitleURL + "</span></td>" +
                                            "<td class='text-center' style='border-bottom: solid 1px #000;'><span>" + landDocumentURL + "</span></td>" +
                                            "</tr>";
            }
            #endregion

            #region AcdDetails
            string Acdhtml = string.Empty;
            int AcdSr = 1;

            foreach (var item in AcdData)
            {
                Acdhtml += "<tr><td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + AcdSr + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.FromYear + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Course + "</td>" +
                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.TotalStudent + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.NoOfStudent + "</td>" +
                                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Result + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.NoOfStudentPass + "</td>" +
                           "<td style=' border-bottom: solid 1px #000;'>" + item.Percentage + "</td></tr>";
                AcdSr = 1 + AcdSr;
            }
            #endregion

            #region Staff Det
            string Staffhtml = string.Empty;
            int StaffSr = 1;

            foreach (var item in StaffData)
            {
                Staffhtml += "<tr><td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + StaffSr + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Name + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Course + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Subject + "" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Role + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Mobile + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Email + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Qualification + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Type + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'><a href='/Trustee/DownloadDocuments?id=" + item.Aadhaar + "' target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a></td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'><a href='/Trustee/DownloadDocuments?id=" + item.Pan + "' target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a></td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'><a href='/Trustee/DownloadDocuments?id=" + item.Profile + "' target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a></td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'><a href='/Trustee/DownloadDocuments?id=" + item.Experience + "' target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a></td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.DOB + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.DOJ + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.DOA + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Salary + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.StaffStatus + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.ResearchGuide + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.PFDeduction + "</td>" +
                                            "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.PFNumber + "</td>" +
                                            "<td style='border-bottom: solid 1px #000;'>" + item.Specialization + "</td></tr>";
            }

            #endregion

            #region SubjectDetails
            string Subhtml = string.Empty;
            int SubSr = 1;

            foreach (var item in subjectData)
            {
                Subhtml += "<tr><td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + SubSr + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.SubjectName + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.TagDegrees + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.TagCourse + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.CollegeName + "</td></tr>";

                SubSr = 1 + SubSr;
            }
            #endregion

            #region Proof Of Doc            
            List<CollageAttachmentList> _result = new List<CollageAttachmentList>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetCollageAttachment?Guid=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<CollageAttachmentList>>(response.Content);
            }

            string Proofhtml = string.Empty;
            int ProofSr = 1;

            foreach (var item in _result)
            {
                var affidavitfile = (item.affidavitfile == "") ? "NA" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.affidavitfile} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var SalaryPaymentfile = (item.SalaryPaymentfile == "") ? "NA" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.SalaryPaymentfile} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var Bankstatementfile = (item.Bankstatementfile == "") ? "NA" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.Bankstatementfile} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var Annexurefile = (item.Annexurefile == "") ? "NA" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.Annexurefile} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var EsiDocfile = (item.EsiDocfile == "") ? "NA" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.EsiDocfile} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";

                var bIsCnnctUnvrctyDrctfiles = (item.bIsCnnctUnvrctyDrctfiles == "") ? "" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.bIsCnnctUnvrctyDrctfiles} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var bIsCnnctUnvrctyDrctn = (item.bIsCnnctUnvrctyDrctn == 1) ? "Yes" : "No";

                var bIsTimeFrm = (item.bIsTimeFrm == 1) ? "Yes" : "No";
                var bIsTimeFrmfiles = (item.bIsTimeFrmfiles == "") ? "NA" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.bIsTimeFrmfiles} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";

                var bIsLadDwnfiles = (item.bIsLadDwnfiles == "") ? "" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.bIsLadDwnfiles} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var bIsLadDwn = (item.bIsLadDwn == 1) ? "Yes" : "No";

                var bIsSffcentLandfiles = (item.bIsSffcentLandfiles == "") ? "" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.bIsSffcentLandfiles} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var bIsSffcentLand = (item.bIsSffcentLand == 1) ? "Yes" : "No";

                var bIsAffidvtAsprGuidfiles = (item.bIsAffidvtAsprGuidfiles == "") ? "" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.bIsAffidvtAsprGuidfiles} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var bIsAffidvtAsprGuid = (item.bIsAffidvtAsprGuid == 1) ? "Yes" : "No";

                var bIsOtherDocfiles = (item.bIsOtherDocfiles == "") ? "" : "<a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=${item.bIsOtherDocfiles} target ='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a>";
                var bIsOtherDoc = (item.bIsOtherDoc == 1) ? "Yes" : "No";

                Proofhtml += "<tr><td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + ProofSr + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Course + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'>" + affidavitfile + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'>" + SalaryPaymentfile + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'>" + Bankstatementfile + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'>" + Annexurefile + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;text-align:center'>" + EsiDocfile + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + bIsCnnctUnvrctyDrctn + "&nbsp;&nbsp;" + bIsCnnctUnvrctyDrctfiles + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + bIsTimeFrm + "&nbsp;&nbsp;" + bIsTimeFrmfiles + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + bIsLadDwn + "&nbsp;&nbsp;" + bIsLadDwnfiles + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + bIsSffcentLand + "&nbsp;&nbsp;" + bIsSffcentLandfiles + "</td>" +
                          "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + bIsAffidvtAsprGuid + "&nbsp;&nbsp;" + bIsAffidvtAsprGuidfiles + "</td>" +
                          "<td style='border-bottom: solid 1px #000;'>" + bIsOtherDoc + "&nbsp;&nbsp;" + bIsOtherDocfiles + "</td></tr>";

                ProofSr = 1 + ProofSr;
            }
            #endregion

            #region collage Facility
            string ColgFacilityhtml = string.Empty;
            int ColgFacilitySr = 1;

            foreach (var item in OtherFac)
            {

                ColgFacilityhtml += "<tr><td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + ColgFacilitySr + "</td>" +
                         "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Facility + "</td>" +
                         "<td style='border-bottom: solid 1px #000;text-align:center'><a href=${GetglobalDomain()}/Trustee/DownloadDocuments?id=" + item.iPk_AddCourseId + "' target='_blank' alt = 'Red dot'><i class='fa fa-download'></i></a></td>" +
                         "</tr>";


                ColgFacilitySr = 1 + ColgFacilitySr;
            }
            #endregion

            #region Collage Fee Structure
            string ColgFeehtml = string.Empty;
            int ColgFeeySr = 1;

            foreach (var item in ColgFee.rateLists)
            {
                if (item.IsSelect == true)
                {
                    ColgFeehtml += "<tr><td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + ColgFeeySr + "</td>" +
                                 "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Rate + "</td>" +
                                 "<td style='border-bottom: solid 1px #000;'>" + item.Rate + "</td>" +
                                 "</tr>";

                    ColgFeeySr = 1 + ColgFeeySr;
                }
            }
            #endregion

            #region room deatils
            string roomhtml = string.Empty;
            int roomSr = 1;

            foreach (var item in roomdeatils)
            {
                roomhtml += "<tr><td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + roomSr + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'><img src=" + item.filedata + " alt='profile - user' class='rounded - circle' width='50' height='50' />" + item.coursetext + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.length + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Width + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Qty + "</td>";


                roomSr = 1 + roomSr;

            }
            #endregion

            #region other deatils
            string otherhtml = string.Empty;
            int otherSr = 1;

            foreach (var item in OtherDetails)
            {
                otherhtml += "<tr><td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + otherSr + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.coursetext + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.Width + "</td>" +
                           "<td style='border-right: solid 1px #000; border-bottom: solid 1px #000;'>" + item.length + "</td>";



                otherSr = 1 + otherSr;

            }
            #endregion

            return new JsonResult
            {
                Data = new { StatusCode = 1, Landhtml = Landhtml, Acdhtml = Acdhtml, Staffhtml = Staffhtml, Subhtml = Subhtml, Proofhtml = Proofhtml, ColgFacilityhtml = ColgFacilityhtml, ColgFeehtml = ColgFeehtml, roomdeatils = roomhtml, OtherDetails = otherhtml, Failure = true },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return View();
        }


        public ActionResult ArchitecDetails(string AppGuid)
        {
            List<CourserBind> lstcourse = new List<CourserBind>();
            lstcourse = UpdateCoursebind(AppGuid);
            ViewBag.lstcourse = lstcourse;
            ViewBag.AppGuid = AppGuid;
            return View();
        }
        public ActionResult OtherDetails(string AppGuid)
        {
            List<CourserBind> lstother = new List<CourserBind>();
            lstother = Updateotherbind(AppGuid);
            ViewBag.lstother = lstother;
            ViewBag.AppGuid = AppGuid;
            return View();
        }
        public List<CourserBind> UpdateCoursebind(string AppGuid, int CourseId = 0)
        {
            List<CourserBind> departments = new List<CourserBind>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetApplicationCourse?AppGuid=" + AppGuid + "&CourseId=" + CourseId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<CourserBind>>(objResponse.Data.ToString());
                }
            }
            return departments;
        }

        public List<CourserBind> Updateotherbind(string AppGuid)
        {
            List<CourserBind> departments = new List<CourserBind>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetOtherBind?AppGuid=" + AppGuid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<CourserBind>>(objResponse.Data.ToString());
                }
            }
            return departments;
        }
        public JsonResult InsertArchitechDetaile(List<Architecturesave> datalist)
        {

            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/InsertArchitechDetaile");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(datalist), ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }


            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult BindTable(string AppGuid, int Type)
        {
            List<Architecturesave> departments = new List<Architecturesave>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetArchitectureData?AppGuid=" + AppGuid + "&Type=" + Type);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    var atcData = JsonConvert.SerializeObject(objResponse.Data);

                    departments = JsonConvert.DeserializeObject<List<Architecturesave>>(atcData);
                }
                if (objResponse.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponse.ResponseCode == "000" && objResponse.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public List<CustomMaster> GetDept()
        {


            ViewBag.GuidDetail = GetDetails();

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Admin/Facilites");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CustomMaster> data = new List<CustomMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    data = JsonConvert.DeserializeObject<List<CustomMaster>>(objResponse.Data.ToString());
                    ViewBag.Facilites = data;
                }
            }
            return data;
        }

        public List<AddCourseBO> GetDetails()
        {
            string applGUID = SessionModel.ApplicantGuid;
            List<AddCourseBO> objUsermaster = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Admin/GetDetailsId?GUID=" + applGUID);
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


                ViewBag.GuidDetail = objUsermaster;

            }
            return objUsermaster;
        }
        public List<AddCourseBO> GetDetailsList()
        {

            string applGUID = SessionModel.ApplicantGuid;
            List<AddCourseBO> objUsermaster = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Admin/GetFacilityDetails?GUID=" + applGUID);
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

        public JsonResult DeleteFacility(int id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/DeleteFacilityCourse?id=" + id);
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


        public JsonResult UpdateApplicationStatus(string AppGUID)
        {
            var nocList = ZapurseCommonlist.UpdateApplicationStatus(AppGUID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "Application Status" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public JsonResult GetApplicableCourseForCollege(string clgID)
        {
            var nocList = ZapurseCommonlist.GetApplicableCourseForCollege(clgID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "Courses List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetApplicableSubjectForCollege(string clgID)
        {
            var nocList = ZapurseCommonlist.GetApplicableSubjectForCollege(clgID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "Subject List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AmmendmentDetailsForCollege(string clgID)
        {
            var nocList = ZapurseCommonlist.AmmendmentDetailsForCollege(clgID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "Subject List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult TNOCPNOCExtentionDetails(string clgID)
        {
            var nocList = ZapurseCommonlist.TNOCPNOCExtentionDetails(clgID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "Subject List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult ValidateApplicationSubmission(string clgID)
        {
            var nocList = ZapurseCommonlist.ValidateApplicationSubmission(clgID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "Subject List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UploadMasterPaymentReceipt(UploadMasterReceipt receipt)
        {
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];

            receipt.paidBy = servicesCollectiondata.PartyId;

            var json = JsonConvert.SerializeObject(receipt);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/UploadMasterPaymentReceipt");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult PaymentHistoryForApplication(string ApplicationNumber)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=paymentHistory" + "&MenuId=" + ApplicationNumber);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<paymentHistory> trusteeList = new List<paymentHistory>();
            var daa = "";
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    daa = JsonConvert.SerializeObject(objResponse.Data);
                    //trusteeList = JsonConvert.DeserializeObject<List<paymentHistory>>(d);
                }
            }

            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = daa, Failure = false, Message = "Payment History" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult DownloadFacilityDocuments(int id)
        {
            string applGUID = SessionModel.ApplicantGuid;
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/DocumentFacilityDetail?GUID=" + applGUID + "&Identity=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                //AddCourseBO _result = _JsonSerializer.Deserialize<>(response.Content);
                TrusteeBO.Trustee _result = _JsonSerializer.Deserialize<TrusteeBO.Trustee>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.AadhaarContentType.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.AadhaarContentType;
                    Response.AppendHeader("content-disposition", "attachment; filename=Documents." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.Aadhaar);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            return View();
            #endregion
            //m_RequestMPRInternDetail item = Context.m_RequestMPRInternDetail.Where(s => s.RequestMPRInternDetailId == id).FirstOrDefault();

        }


        public JsonResult CheckDraftValidationForEntry(int clgID, string courses, string subjects)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/CheckDraftValidationForEntry?clgID=" + clgID + "&courses=" + courses + "&subjects=" + subjects);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Course = objResponse.Message, Subject = objResponse.ResponseCode },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #region Apply NOC New Page Conditions
        public JsonResult ApplyNOC_CollegeListForDepartment(string departID)
        {
            var trusID = "599";//SessionModel.TrustId;
            var clgList = ZapurseCommonlist.GetClgListForDepartment(departID, "ApplyNOC_CollegeForDepartment", trusID); // gets only those colleges whose entry is there in MST_APLN table
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = clgList, Failure = false, Message = "College List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult ApplyNOC_NOCCategory(string departID)
        {
            var clgList = ZapurseCommonlist.ApplyNOC_NOCCategory(departID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = clgList, Failure = false, Message = "NOC Category List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult ApplyNOC_NOCForms(string departID, string categoryID)
        {
            var clgList = ZapurseCommonlist.ApplyNOC_NOCForms(departID, "ApplyNOC_NOCForms", categoryID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = clgList, Failure = false, Message = "NOC Forms List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        public JsonResult ApplyNOC_TNOCExtentionData(string collegeID)
        {
            var clgList = ZapurseCommonlist.ApplyNOC_TNOCExtentionData(collegeID, "ApplyNOC_TNOCExtention");
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = clgList, Failure = false, Message = "NOC TNOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        public JsonResult ApplyNOC_PNOC(string collegeID)
        {
            var clgList = ZapurseCommonlist.ApplyNOC_PNOC(collegeID, "ApplyNOC_PNOC");
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = clgList, Failure = false, Message = "NOC PNOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }




        #endregion
    }
}