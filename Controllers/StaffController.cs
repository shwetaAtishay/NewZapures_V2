using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.TrusteeBO;

namespace NewZapures_V2.Controllers
{
    public class StaffController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        // GET: Trustee
        public ActionResult Index(string guid)
        {
            //guid = "s001";
            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(28);
            ViewBag.RoleType = RoleType;

            List<CustomMaster> CourseList = new List<CustomMaster>();
            CourseList = ZapurseCommonlist.GetCourseListDropDown(guid);
            ViewBag.CourseList = CourseList;

            //List<CustomMaster> SubjectList = new List<CustomMaster>();
            //SubjectList = ZapurseCommonlist.GetSubjectListDropDown(guid);
            //ViewBag.SubjectList = SubjectList;

            List<StaffBO.Staff> _result = new List<StaffBO.Staff>();
            #region List Staff
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/StaffList?Guid=" + SessionModel.ApplicantGuid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<StaffBO.Staff>>(response.Content);
                if (_result != null)
                {
                    ViewBag.StaffList = _result;
                    //return RedirectToAction("Index");
                }
            }
            ViewBag.StaffList = _result;
            ViewBag.guid = guid;
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult Index(StaffBO.Staff obj, HttpPostedFileBase aadhaarfile, HttpPostedFileBase panfile, HttpPostedFileBase profilefile, HttpPostedFileBase experiencefile, string guid)
        {
            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(28);
            ViewBag.RoleType = RoleType;

            obj.Guid = SessionModel.ApplicantGuid;
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Aadhaar
            if (aadhaarfile != null)
            {
                byte[] AadharDocumentbyte;
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
                    obj.Profile = Convert.ToBase64String(Documentbyte);
                    obj.ProfileExtension = extension;
                    obj.ProfileContentType = ContentType;
                }
            }
            #endregion

            #region experiencefile
            if (experiencefile != null)
            {
                extension = Path.GetExtension(experiencefile.FileName);
                ContentType = experiencefile.ContentType;
                using (Stream inputStream = experiencefile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Experience = Convert.ToBase64String(Documentbyte);
                    obj.ExperienceExtension = extension;
                    obj.ExperienceContentType = ContentType;
                }
            }
            #endregion

            #region Add Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/AddStaff");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = "Data saved sussessfully!";
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
            }
            #endregion
            #region List Trustee
            client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/StaffList");
            request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<StaffBO.Staff> _result = _JsonSerializer.Deserialize<List<StaffBO.Staff>>(response.Content);
                if (_result != null)
                {
                    ViewBag.TrusteeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion

            //return View();
            return RedirectToAction("EditApplication", "Trustee", new { applGUID = SessionModel.ApplicantGuid });
            //return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            #region List Staff
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/StaffDelete?Id=" + Id);
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

        [HttpPost]
        public JsonResult SaveDetails(StaffBO.Staff obj)
        {
            //var JSON = _JsonSerializer.Serialize(obj);           

            try
            {
                #region Add Trustee
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/AddStaff");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                _JsonSerializer.MaxJsonLength = Int32.MaxValue;
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
                        return new JsonResult
                        {
                            Data = new { StatusCode = objResponseData.ResponseCode, Failure = false },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JsonResult
            {
                Data = new { StatusCode = 0, Failure = false },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public JsonResult GetQualification(int Id)
        {
            List<StaffBO.QualificationDetails> _result = new List<StaffBO.QualificationDetails>();
            #region List Staff
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/GetQualificationDetails?Guid=" + SessionModel.ApplicantGuid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<StaffBO.QualificationDetails>>(response.Content);
                if (_result != null)
                {
                    ViewBag.StaffList = _result;
                    return new JsonResult
                    {
                        Data = new { StatusCode = 1, datalist = _result, Failure = true },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    //return RedirectToAction("Index");
                }
            }
            ViewBag.StaffList = _result;
            #endregion
            return new JsonResult
            {
                Data = new { StatusCode = 0, Failure = false },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult DataDelete(string Id)
        {
            #region List Staff
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/StaffDelete?Id=" + Id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
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
                return new JsonResult
                {
                    Data = new { StatusCode = objResponseData.ResponseCode, Failure = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            #endregion
        }

        public JsonResult GetSubjectList(string guid, string Course)
        {
            try
            {

                List<CustomMaster> SubjectList = new List<CustomMaster>();
                SubjectList = ZapurseCommonlist.GetSubjectListDropDown(guid, Course);
                var List = SubjectList.Select(x => new { x.text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}