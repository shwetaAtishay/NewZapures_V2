using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NewZapures_V2.Controllers
{
    public class SupportTicketController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        ResponseData objResponse;

        // GET: SupportTicket
        public ActionResult Index()
        {
            ViewBag.SupportList = GetDetailsList();
            ViewBag.TicketList = GetModule();
            
            return View();
        }

        public List<Dropdown> GetModule()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "SupportTicketIssue/GetTicketsList");
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

        public List<Dropdown> GetFunctionality()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetFunctionality");
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

        public List<Dropdown> GetTicket()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetTicket");
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

        public JsonResult SaveData(SupportTicket mapping)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            var json = JsonConvert.SerializeObject(mapping);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveSupportTicket");
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


        [HttpPost]
        public JsonResult SupportSaveDetails(SupportIssue trg)
        {
            try
            {
                if (trg.Id > 0)
                {
                    //trg.TrustInfoId = SessionModel.TrustId;
                    var userdetailsSession = (UserModelSession)Session["UserDetails"];
                    //party.ParentId = userdetailsSession.PartyId;
                    var json = JsonConvert.SerializeObject(trg);
                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/UpdateCollageDetails");
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
                            Data = new { StatusCode = objResponse.statusCode, Failure = false, },
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
                else
                {
                   
                    var userdetailsSession = (UserModelSession)Session["UserDetails"];
                    //party.ParentId = userdetailsSession.PartyId;
                    var json = JsonConvert.SerializeObject(trg);
                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "SupportTicketIssue/SupportDetail");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SupportIssue> GetDetailsList()
        {
            List<SupportIssue> objUsermaster = new List<SupportIssue>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "SupportTicketIssue/GetSupportData");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<SupportIssue> objResponseData = new List<SupportIssue>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponseData = JsonConvert.DeserializeObject<List<SupportIssue>>(response.Content);
                //objResponseData = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());

                objUsermaster = objResponseData;
            }

            return objUsermaster;
        }

        public JsonResult DeleteTickets(int id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SupportTicketIssue/DeleteTickets?id=" + id);
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

        public ActionResult DownloadTicketsDocuments(int id)
        {

            #region List Tickets
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "SupportTicketIssue/DocumentTicketsDetail?Identity=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
               
                SupportIssue _result = _JsonSerializer.Deserialize<SupportIssue>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.TicketFileContentType.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.TicketFileContentType;
                    Response.AppendHeader("content-disposition", "attachment; filename=Documents." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.TicketFile);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                   
                }
            }
            return View();
            #endregion
            

        }
    }
}