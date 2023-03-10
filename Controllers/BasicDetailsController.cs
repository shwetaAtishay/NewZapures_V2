using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NewZapures_V2.Models.Common;
using System.Web.Script.Serialization;
using System.IO;

namespace NewZapures_V2.Controllers
{
    public class BasicDetailsController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: BasicDetails
        public ActionResult CreateDetails(int Id = 0)
        {
            if (Id > 0)
            {
                //var universityList = ZapurseCommonlist.GetUniversities();
                var collegeType = ZapurseCommonlist.GetCollegeType();

                //ViewBag.universityCollection = universityList;
                ViewBag.collegeTypeList = collegeType;

                #region Get Division
                List<Dropdown> Division = new List<Dropdown>();
                Division = Common.GetLocationDropDown("Division");
                ViewBag.Division = Division;
                #endregion

                #region Get District
                List<Dropdown> DistrictList = new List<Dropdown>();
                DistrictList = Common.GetLocationDropDown("District");
                ViewBag.DistrictList = DistrictList;
                #endregion

                ViewBag.Department = GetDept();

                #region List Collage Apply List
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetCollageDetails?Id=" + Id);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                _JsonSerializer.MaxJsonLength = Int32.MaxValue;
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    BasicDetailsBO _result = _JsonSerializer.Deserialize<BasicDetailsBO>(response.Content);
                    if (_result != null)
                    {
                        ViewBag.CollageDetails = _result;
                        //return RedirectToAction("Index");
                        return View();
                    }
                }
                #endregion
            }
            else
            {
                var universityList = ZapurseCommonlist.GetUniversities();
                var collegeType = ZapurseCommonlist.GetCollegeType();

                ViewBag.universityCollection = universityList;
                ViewBag.collegeTypeList = collegeType;

                #region Get Division
                List<Dropdown> Division = new List<Dropdown>();
                Division = Common.GetLocationDropDown("Division");
                ViewBag.Division = Division;
                ViewBag.Department = GetDept();
                #endregion
                #region Get District
                List<Dropdown> DistrictList = new List<Dropdown>();
                DistrictList = Common.GetLocationDropDown("District");
                ViewBag.DistrictList = DistrictList;

                ViewBag.CollageDetails = new BasicDetailsBO();
                #endregion
            }
            return View();
        }
        public ActionResult ShowDetails()
        {
            return View();
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

        [HttpPost]
        public ActionResult SaveDetails(BasicDetailsBO trg, HttpPostedFileBase collageLogo)
        {
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Collage Logo
            if (collageLogo != null)
            {
                extension = Path.GetExtension(collageLogo.FileName);
                ContentType = collageLogo.ContentType;
                using (Stream inputStream = collageLogo.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    trg.CollageLogo = Convert.ToBase64String(Documentbyte);
                    trg.CollageLogoExtension = extension;
                    trg.CollageLogoContenttype = ContentType;
                }
            }
            #endregion

            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/BasicDetailConfigure");
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
                    TempData["msg"] = " Details Saved...";
                    return RedirectToAction("CreateDetails", "BasicDetails");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("CreateDetails", "BasicDetails");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return RedirectToAction("CreateDetails");
        }

        [HttpPost]
        public JsonResult ContactSaveDetails(BasicDetailsBO trg)
        {
            try
            {
                if (trg.Id > 0)
                {
                    trg.TrustInfoId = SessionModel.TrustId;
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
                else
                {
                    trg.TrustInfoId = SessionModel.TrustId;
                    var userdetailsSession = (UserModelSession)Session["UserDetails"];
                    //party.ParentId = userdetailsSession.PartyId;
                    var json = JsonConvert.SerializeObject(trg);
                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/BasicDetailConfigure");
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult CollageList()
        {
            #region List Collage Apply List
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/CollageListApply?TrustId=" + SessionModel.TrustId);
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
                    ViewBag.collagelist = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        //[HttpGet]
        //public ActionResult ContactList()
        //{
        //    #region List Contact Apply List
        //    var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "BasicDataDetails/ContactDetailConfigure");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");
        //    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    request.AddParameter("application/json", "", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    if (response.StatusCode.ToString() == "OK")
        //    {
        //        List<BasicDetailsBO> _result = _JsonSerializer.Deserialize<List<BasicDetailsBO>>(response.Content);
        //        if (_result != null)
        //        {
        //            ViewBag.ContactList = _result;
        //            //return RedirectToAction("Index");
        //        }
        //    }
        //    #endregion
        //    return View();
        //}

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

        public JsonResult GetPaliamentAreaList(string DistrictId)
        {
            try
            {
                var id = Convert.ToInt32(DistrictId);
                List<Dropdown> PaliamentAreaList = new List<Dropdown>();
                PaliamentAreaList = Common.GetLocationDropDown("PaliamentArea", id);
                var List = PaliamentAreaList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public JsonResult GetLegislativeList(string DistrictId)
        {
            try
            {
                var id = Convert.ToInt32(DistrictId);
                List<Dropdown> LegislativeAreaList = new List<Dropdown>();
                LegislativeAreaList = Common.GetLocationDropDown("LegislativeArea", id);
                var List = LegislativeAreaList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public JsonResult GetCityList(string Id)
        {
            try
            {
                var id = Convert.ToInt32(Id);
                List<Dropdown> DistrictList = new List<Dropdown>();
                DistrictList = Common.GetLocationDropDown("City",id);
                var List= DistrictList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();
                
                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {                
                throw;
            }
        }

        public JsonResult GetBlockList(string Id)
        {
            try
            {
                var id = Convert.ToInt32(Id);
                List<Dropdown> BlockList = new List<Dropdown>();
                BlockList = Common.GetLocationDropDown("Block", id);
                var List = BlockList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
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

        public JsonResult GetDistrictList(string DivisionId)
        {
            try
            {
                var id = Convert.ToInt32(DivisionId);
                List<Dropdown> DivisionList = new List<Dropdown>();
                DivisionList = Common.GetLocationDropDown("District", id);
                var List = DivisionList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public JsonResult GetSUBDIVISIONList(string Districtid)
        {
            try
            {
                var id = Convert.ToInt32(Districtid);
                List<Dropdown> DIVISIONList = new List<Dropdown>();
                DIVISIONList = Common.GetLocationDropDown("SUBDIVISION", id);
                var List = DIVISIONList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public JsonResult GetUniversityForDept(int deptID)
        {
            var clgList = ZapurseCommonlist.GetUniversityForDept(deptID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = clgList, Failure = false, Message = "University List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}