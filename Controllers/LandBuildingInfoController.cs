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

namespace NewZapures_V2.Controllers
{
    public class LandBuildingInfoController : Controller
    {
        // GET: LandBuildingInfo
        public ActionResult Index(string guid, int clgId = 0)
        {
            //var draftApplications = ZapurseCommonlist.GetDraftApplication(guid);
            //var courses = ZapurseCommonlist.GetCourseForDept(draftApplications[0].iFKDEPT_ID);
            var LandData = ZapurseCommonlist.GetLandBuildingInfo(guid);
            ViewBag.CourseList = ZapurseCommonlist.GetCourseListDropDown(guid);
            ViewBag.draftApplication = guid;
            ViewBag.landDataList = LandData;
            ViewBag.collegeID = clgId;
            return View();
        }

        public ActionResult Building(string guid = "", int clgId = 0)
        {
            //var draftApplications = ZapurseCommonlist.GetDraftApplication(guid);
            //var courses = ZapurseCommonlist.GetCourseForDept(draftApplications[0].iFKDEPT_ID);
            var buildingData = ZapurseCommonlist.GetBuildingInfo(clgId);
            //ViewBag.CourseList = ZapurseCommonlist.GetCourseListDropDown(guid);
            ViewBag.draftApplication = guid;
            ViewBag.buildingData = buildingData;
            //ViewBag.collegeID = clgId;
            return View();
        }
        public ActionResult LandAreaDetails(string guid)
        {
            var userDetails = (UserModelSession)Session["UserDetails"];
            var LandData = ZapurseCommonlist.GetLandBuildingInfo(guid);
            ViewBag.landDataList = LandData;
            ViewBag.UserID = userDetails.PartyId;
            ViewBag.applGuid = guid;

            return View();
        }


        public JsonResult AddComments(InspectionComments comments)
        {
            var objResponse = ZapurseCommonlist.AddComments(comments);

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        [HttpPost]
        public JsonResult SaveDetails(List<LandInfoBO> trg)
        {
            //byte[] Documentbyte;
            //string extension = string.Empty;
            //string ContentType = string.Empty;
            //#region Land Area Proof
            //if (LandAreaProof != null)
            //{
            //    extension = Path.GetExtension(LandAreaProof.FileName);
            //    ContentType = LandAreaProof.ContentType;
            //    using (Stream inputStream = LandAreaProof.InputStream)
            //    {
            //        MemoryStream memoryStream = inputStream as MemoryStream;
            //        if (memoryStream == null)
            //        {
            //            memoryStream = new MemoryStream();
            //            inputStream.CopyTo(memoryStream);
            //        }
            //        Documentbyte = memoryStream.ToArray();
            //        trg.LandAreaProof = Convert.ToBase64String(Documentbyte);
            //        trg.LandAreaProofExtension = extension;
            //        trg.LandAreaProofDocumentContent = ContentType;
            //    }
            //}
            //#endregion

            //#region Upload Document 
            //if (LandConvertProof != null)
            //{
            //    extension = Path.GetExtension(LandConvertProof.FileName);
            //    ContentType = LandConvertProof.ContentType;
            //    using (Stream inputStream = LandConvertProof.InputStream)
            //    {
            //        MemoryStream memoryStream = inputStream as MemoryStream;
            //        if (memoryStream == null)
            //        {
            //            memoryStream = new MemoryStream();
            //            inputStream.CopyTo(memoryStream);
            //        }
            //        Documentbyte = memoryStream.ToArray();
            //        trg.LandConvertProof = Convert.ToBase64String(Documentbyte);
            //        trg.LandConvertProofExtension = extension;
            //        trg.LandConvertProofDocumentContent = ContentType;
            //    }
            //}
            //#endregion

            //#region Certificate Doc
            //if (OwnBuildingProof != null)
            //{
            //    extension = Path.GetExtension(OwnBuildingProof.FileName);
            //    ContentType = OwnBuildingProof.ContentType;
            //    using (Stream inputStream = OwnBuildingProof.InputStream)
            //    {
            //        MemoryStream memoryStream = inputStream as MemoryStream;
            //        if (memoryStream == null)
            //        {
            //            memoryStream = new MemoryStream();
            //            inputStream.CopyTo(memoryStream);
            //        }
            //        Documentbyte = memoryStream.ToArray();
            //        trg.OwnBuildingProof = Convert.ToBase64String(Documentbyte);
            //        trg.OwnBuildingProofExtension = extension;
            //        trg.OwnBuildingProofDocumentContent = ContentType;
            //    }
            //}
            //#endregion
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "LandDetails/AddLandInfo");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                ResponseData objResponse = new ResponseData();
                if (response.StatusCode.ToString() == "OK")
                {
                    objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    //TempData["isSaved"] = 1;
                    //TempData["msg"] = " Details Saved...";
                    //return RedirectToAction("Index", "LandBuildingInfo");
                }
                else
                {
                    //TempData["isSaved"] = 0;
                    //TempData["msg"] = " Details Not Saved...";
                    //return RedirectToAction("Index", "LandBuildingInfo");
                }

                return new JsonResult
                {
                    Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return RedirectToAction("CreateDetails");
        }

        [HttpPost]
        public JsonResult AddBuildingDetails(BuildingDetails trg)
        {
            //byte[] Documentbyte;
            //string extension = string.Empty;
            //string ContentType = string.Empty;
            //#region Land Area Proof
            //if (LandAreaProof != null)
            //{
            //    extension = Path.GetExtension(LandAreaProof.FileName);
            //    ContentType = LandAreaProof.ContentType;
            //    using (Stream inputStream = LandAreaProof.InputStream)
            //    {
            //        MemoryStream memoryStream = inputStream as MemoryStream;
            //        if (memoryStream == null)
            //        {
            //            memoryStream = new MemoryStream();
            //            inputStream.CopyTo(memoryStream);
            //        }
            //        Documentbyte = memoryStream.ToArray();
            //        trg.LandAreaProof = Convert.ToBase64String(Documentbyte);
            //        trg.LandAreaProofExtension = extension;
            //        trg.LandAreaProofDocumentContent = ContentType;
            //    }
            //}
            //#endregion

            //#region Upload Document 
            //if (LandConvertProof != null)
            //{
            //    extension = Path.GetExtension(LandConvertProof.FileName);
            //    ContentType = LandConvertProof.ContentType;
            //    using (Stream inputStream = LandConvertProof.InputStream)
            //    {
            //        MemoryStream memoryStream = inputStream as MemoryStream;
            //        if (memoryStream == null)
            //        {
            //            memoryStream = new MemoryStream();
            //            inputStream.CopyTo(memoryStream);
            //        }
            //        Documentbyte = memoryStream.ToArray();
            //        trg.LandConvertProof = Convert.ToBase64String(Documentbyte);
            //        trg.LandConvertProofExtension = extension;
            //        trg.LandConvertProofDocumentContent = ContentType;
            //    }
            //}
            //#endregion

            //#region Certificate Doc
            //if (OwnBuildingProof != null)
            //{
            //    extension = Path.GetExtension(OwnBuildingProof.FileName);
            //    ContentType = OwnBuildingProof.ContentType;
            //    using (Stream inputStream = OwnBuildingProof.InputStream)
            //    {
            //        MemoryStream memoryStream = inputStream as MemoryStream;
            //        if (memoryStream == null)
            //        {
            //            memoryStream = new MemoryStream();
            //            inputStream.CopyTo(memoryStream);
            //        }
            //        Documentbyte = memoryStream.ToArray();
            //        trg.OwnBuildingProof = Convert.ToBase64String(Documentbyte);
            //        trg.OwnBuildingProofExtension = extension;
            //        trg.OwnBuildingProofDocumentContent = ContentType;
            //    }
            //}
            //#endregion
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                trg.dtCrtdBy = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "LandDetails/AddBuildingInfo");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                ResponseData objResponse = new ResponseData();
                if (response.StatusCode.ToString() == "OK")
                {
                    objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    //TempData["isSaved"] = 1;
                    //TempData["msg"] = " Details Saved...";
                    //return RedirectToAction("Index", "LandBuildingInfo");
                }
                else
                {
                    //TempData["isSaved"] = 0;
                    //TempData["msg"] = " Details Not Saved...";
                    //return RedirectToAction("Index", "LandBuildingInfo");
                }

                return new JsonResult
                {
                    Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return RedirectToAction("CreateDetails");
        }
    }
}