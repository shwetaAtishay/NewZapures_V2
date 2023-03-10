using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Metrica.Controllers
{
    public class DashboardController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        private const string TrustVerify_REQEUST_OTP = "TrustVerifyReqeustOTP";
        public ActionResult Index()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }

        public ActionResult DashboardUser()
        {
            ViewBag.DashboardReport = StatusCountDashboard();
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
        public ActionResult WelcomeNoc()
        {
            return View();
        }
        public ActionResult WelcomeNocNew()
        {
            return View();
        }

        [ActionName("crypto-index")]
        public ActionResult CryptoIndex() => View();


        [ActionName("crm-index")]
        public ActionResult CrmIndex() => View();

        [ActionName("project-index")]
        public ActionResult ProjectIndex() => View();


        [ActionName("ecommerce-index")]
        public ActionResult EcommerceIndex() => View();


        [ActionName("helpdesk-index")]
        public ActionResult HelpdeskIndex() => View();

        [ActionName("hospital-index")]
        public ActionResult hospitalIndex() => View();

        public JsonResult TrustVerification(TrusteeBO.TrusteeInfo modal)
        {
            TrustRoot _trustapi = new TrustRoot();
            //modal.RegistrationNo = "COOP/2019/ALWAR/100658";
            #region Trust API
            var client = new RestClient("https://rajsahakarapp.rajasthan.gov.in/api/EntireSocietyDetail/GetSocietyDetailsByRegistrationNO?Reg_no=" + modal.RegistrationNo);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _trustapi = _JsonSerializer.Deserialize<TrustRoot>(response.Content);
                if (_trustapi.Status == "200" && _trustapi.Message == "Success")
                {
                    SessionModel.TrustRegNo = modal.RegistrationNo;
                    //ErrorBO _ress = Verificationdata(_trustapi);
                    return new JsonResult
                    {
                        Data = new { Success = true, Message = "Trust Information Get Successfully!!", res = _trustapi.Data },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
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
            //Console.WriteLine(response.Content);
            #endregion





            return new JsonResult
            {
                Data = new { Success = false, Message = "Error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ErrorBO Verificationdata(TrustRoot modal)
        {
            ErrorBO _res = new ErrorBO();
            #region VerifyDetails
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrustVerification");
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
        public JsonResult SetSession(string name)
        {
            Session["TrustTypeName"] = name;
            return null;
        }

        #region OTP Varification
        [HttpGet]
        public ActionResult SendReqeustOTP(string RegNo)
        {
            SmsResponseModel _res = new SmsResponseModel();
            try
            {
                //int otp = new Random().Next(1000, 9999);
                int otp = 9876;
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SmsMail/SendSMsTrustVerificationOTP?RegNo=" + RegNo + "&otp=" + otp);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                //_JsonSerializer.MaxJsonLength = Int32.MaxValue;
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    _res = _JsonSerializer.Deserialize<SmsResponseModel>(response.Content);
                    if (_res != null && _res.ResponseCode != null && _res.ResponseCode.Equals("200"))
                    {
                        Session[TrustVerify_REQEUST_OTP] = otp.ToString();
                        SessionModel.TrustRegNo = RegNo;
                        SessionModel.TrustTypeName = _res.TrustType;                       
                        //return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = string.Format("OTP was sent on mobile no. {0}", base.CurrentUser.Mobile) });
                        return new JsonResult
                        {
                            Data = new { Success = true, Message = string.Format("A OTP (one time password) has been sent to {0}", _res.ResponseID) },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }

                }

                return new JsonResult
                {
                    Data = new { Success = false, MobileNo = "false" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { Success = false, MobileNo = "error" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                //base.HandleException(ex);
                //return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult VerifyOTP(string OTP, string RegistrationNo)
        {
            try
            {
                string validationMessage = string.Empty;
                bool validationResult = ValdiateOTPData(OTP, ref validationMessage);
                if (!validationResult)
                {
                    return new JsonResult
                    {
                        Data = new { Success = false, Message = validationMessage },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    //return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = validationMessage });
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { Success = true, Message = validationMessage },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            catch (Exception ex)
            {
            }
            return new JsonResult
            {
                Data = new { Success = false, Message = "" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = Resource.ResourceValidation.InvalidRequest });
        }

        private bool ValdiateOTPData(string Otp, ref string validationMessage)
        {
            bool validationResult = true;
            List<string> validationMessages = new List<string>();
            if (Otp == null || Otp == "")
            {
                validationMessages.Add("OTP is required");
                validationResult = false;
            }
            else if (!Session[TrustVerify_REQEUST_OTP].ToString().Equals(Otp))
            {
                validationMessages.Add("OTP is not valid");
                validationResult = false;
            }
            return validationResult;
        }
        #endregion


        #region Form Status Application

        public List<AllReportBO> StatusCountDashboard()
        {
            string iFKTst_ID = SessionModel.TrustId;

            List<AllReportBO> objUsermaster = new List<AllReportBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Reports/AllReport?iFKTst_ID=" + iFKTst_ID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AllReportBO> objResponseData = new List<AllReportBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponseData = JsonConvert.DeserializeObject<List<AllReportBO>>(response.Content);
                //objResponseData = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());

                objUsermaster = objResponseData;
                ViewBag.DashboardReport = objUsermaster;
            }

            return objUsermaster;
        }
        #endregion


    }
}