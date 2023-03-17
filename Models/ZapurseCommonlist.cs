using BO.Models;
using Newtonsoft.Json;
using NewZapures_V2.Controllers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Device.Location;
using static NewZapures_V2.Models.TrusteeBO;
using RestSharp.Serializers;
using System.Web.Script.Serialization;

namespace NewZapures_V2.Models
{
    public class ZapurseCommonlist
    {
        static JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        //private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
        //private string Latitude = "";
        //private string Longitude = "";

        public static List<GlobalClass> GetCategoryTypeList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Department"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Services"

            });


            return Lst;

        }
        public static List<GlobalClass> GetMode()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "TNOC",
                label = "radio-danger"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "PNOC",
                label = "radio-primary"

            });
            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Both",
                label = "radio-success"

            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Extension TNOC",
                label = "radio-success"

            });
            return Lst;

        }
        public static List<GlobalClass> GetServicesRate()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "GST"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "SSTC"

            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "AMC"

            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Testing Rate"

            });
            return Lst;
        }


        public static List<GetservicesetailsAndroidNew> GetServicesAllDetails()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCategoryAllInformation?Type=Service");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<GetservicesetailsAndroidNew> serviceDetails = new List<GetservicesetailsAndroidNew>();
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    serviceDetails = JsonConvert.DeserializeObject<List<GetservicesetailsAndroidNew>>(objResponse.Data.ToString());
            }
            return serviceDetails;
        }
        public static List<GetservicesetailsAndroidNew> GetHardwaresAllDetails()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCategoryAllInformation?Type=Hardware");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<GetservicesetailsAndroidNew> serviceDetails = new List<GetservicesetailsAndroidNew>();
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    serviceDetails = JsonConvert.DeserializeObject<List<GetservicesetailsAndroidNew>>(objResponse.Data.ToString());
            }
            return serviceDetails;
        }
        public static List<GlobalClass> GetHardwarelist()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Card swipe machie",
                strId = "card-swipe-machine-500x500.jpg"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Thumb machine",
                strId = "ThumbMachine.jpg"

            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "AMC",
                strId = "ThumbMachine.jpg"

            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Testing Rate",
                strId = "ThumbMachine.jpg"

            });
            return Lst;
        }
        public static List<GlobalClass> GetTypelist()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "White Label"
            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Stockist"

            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Distributer"

            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Retailer"
            });
            Lst.Add(new GlobalClass
            {
                Id = 5,
                Text = "User"
            });
            return Lst;
        }
        public static List<DropdownDeptImages> GetDepartmentlist()
        {
            List<DropdownDeptImages> Lst = new List<DropdownDeptImages>();

            AdminController controller = new AdminController();

            Lst = controller.GetDepartments().Select(s => new DropdownDeptImages { Id = s.DepartmentID.ToString(), Text = s.DepartmentName }).Distinct().ToList();

            return Lst;
        }
        public static List<Dropdown> GetRoles()
        {
            List<Dropdown> Lst = new List<Dropdown>();

            AdminController controller = new AdminController();

            Lst = controller.GetRoles();

            return Lst;
        }
        public static List<Dropdown> GetCircle()
        {
            List<Dropdown> Lst = new List<Dropdown>();

            Lst.Add(new Dropdown
            {
                Text = "Andhra Pradesh Telangana"
            });
            Lst.Add(new Dropdown
            {
                Text = "Assam"
            });
            Lst.Add(new Dropdown
            {
                Text = "Bihar Jharkhand"
            });
            Lst.Add(new Dropdown
            {
                Text = "Chennai"
            });
            Lst.Add(new Dropdown
            {
                Text = "Delhi NCR"
            });
            Lst.Add(new Dropdown
            {
                Text = "Gujarat"
            });
            Lst.Add(new Dropdown
            {
                Text = "Haryana"
            });
            Lst.Add(new Dropdown
            {
                Text = "Himachal Pradesh"
            });
            Lst.Add(new Dropdown
            {
                Text = "Jammu Kashmir"
            });
            Lst.Add(new Dropdown
            {
                Text = "Karnataka"
            });
            Lst.Add(new Dropdown
            {
                Text = "Kerala"
            });
            Lst.Add(new Dropdown
            {
                Text = "Kolkata"
            }); Lst.Add(new Dropdown
            {
                Text = "Madhya Pradesh Chhattisgarh"
            }); Lst.Add(new Dropdown
            {
                Text = "Maharashtra Goa"
            }); Lst.Add(new Dropdown
            {
                Text = "Mumbai"
            }); Lst.Add(new Dropdown
            {
                Text = "North East"
            }); Lst.Add(new Dropdown
            {
                Text = "Orissa"
            }); Lst.Add(new Dropdown
            {
                Text = "Punjab"
            }); Lst.Add(new Dropdown
            {
                Text = "Rajasthan"
            });
            Lst.Add(new Dropdown
            {
                Text = "Tamil Nadu"
            });
            Lst.Add(new Dropdown
            {
                Text = "UP East"
            });
            Lst.Add(new Dropdown
            {
                Text = "UP West"
            });
            Lst.Add(new Dropdown
            {
                Text = "West Bengal"
            });
            return Lst.OrderBy(o => o.Text).ToList();
        }
        public static List<OperatorsList> GetMobileOperator()
        {
            List<OperatorsList> Lst = new List<OperatorsList>();

            Lst.Add(new OperatorsList
            {
                OperatorId = "1",
                OperatorName = "Airtel",
                OperatorImage = "Airtel.png",
                Type = "Mobile"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "2",
                OperatorName = "Idea",
                OperatorImage = "Idea.png",
                Type = "Mobile"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "3",
                OperatorName = "BSNL (Topup)",
                OperatorImage = "BSNL.png",
                Type = "Mobile"

            });

            Lst.Add(new OperatorsList
            {
                OperatorId = "4",
                OperatorName = "BSNL (Special)",
                OperatorImage = "BSNL.png",
                Type = "Mobile"

            });

            Lst.Add(new OperatorsList
            {
                OperatorId = "5",
                OperatorName = "JIO",
                OperatorImage = "RelianceJIO.png",
                Type = "Mobile"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "6",
                OperatorName = "Vodafone",
                OperatorImage = "Vodafone.png",
                Type = "Mobile"

            });


            return Lst;
        }
        public static List<OperatorsList> GetDTHOperator()
        {
            List<OperatorsList> Lst = new List<OperatorsList>();

            Lst.Add(new OperatorsList
            {
                OperatorId = "7",
                OperatorName = "Airtel DTH",
                OperatorImage = "AirtelDTH.png",
                Type = "DTH"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "8",
                OperatorName = "Dish TV",
                OperatorImage = "DishTV.png",
                Type = "DTH"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "11",
                OperatorName = "Tata Sky",
                OperatorImage = "TataSky.png",
                Type = "DTH"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "10",
                OperatorName = "Sun Direct",
                OperatorImage = "SunDirect.png",
                Type = "DTH"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "12",
                OperatorName = "Videocon D2H",
                OperatorImage = "Videocon.png",
                Type = "DTH"

            });


            return Lst;
        }
        public static List<Dropdown> GetUserTypes()
        {
            List<Dropdown> dropdowns = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetUserType");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse != null)
                    dropdowns = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return dropdowns;
        }
        public static List<Dropdown> GetMenusList(string Type = "MenuList", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<Dropdown> GetUniversities(string MenuId = "0", string Type = "University")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }
        public static List<Dropdown> GetNotificationTypeMaster(string MenuId = "0", string Type = "NotificationTypeMaster")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }
        public static List<Dropdown> GetNotificationDirectionFlow(string MenuId = "0", string Type = "NotificationFlowDirection")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }
        public static List<Dropdown> GetServideProviderList(string Type = "ServiceProvider")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> departments = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                departments = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return departments;
        }
        public static List<WalletLeft> GetWalletAmount(string partyID, string Type = "GetWalletAmount")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletLeft> wallet = new List<WalletLeft>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                wallet = JsonConvert.DeserializeObject<List<WalletLeft>>(objResponse.Data.ToString());
            }
            return wallet;
        }
        public static List<AadhaarDetails> GetAadhaarDetails(string partyID)
        {

            AdminController controller = new AdminController();

            List<AadhaarDetails> details = controller.GetAadhaarDetails(partyID);

            return details;
        }
        public static List<NotificationMaster> GetNotificationMaster()
        //public static List<NotificationMaster> GeNotificationMaster(sting serviceTypeId, int descriptionId)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetNotificationMaster");
            //var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetNotificationMaster?serviceTypeId=" + serviceTypeId + "&descriptionId=" + descriptionId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<NotificationMaster> notificationsMaster = new List<NotificationMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                var responseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (responseData.Data != null)
                    notificationsMaster = JsonConvert.DeserializeObject<List<NotificationMaster>>(responseData.Data.ToString());
            }
            return notificationsMaster;
        }
        public static ResponseData SaveNotificationTransactionData(NotificationTransectionUserListRequest requestData)
        {
            var json = JsonConvert.SerializeObject(requestData);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/SaveNotificationTransactionAndUserList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            ResponseData requestResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

            }
            return requestResponse;
        }
        public static List<NotificationOperationData> NotificationOperation(NotificationOperationRequest notificationOperation)
        {
            var json = JsonConvert.SerializeObject(notificationOperation);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/NotificationOperation");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            ResponseData requestResponse = new ResponseData();
            List<NotificationOperationData> notificationData = new List<NotificationOperationData>();
            if (response.StatusCode.ToString() == "OK")
            {
                requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            if (notificationOperation.Type == "MarkasRead" || notificationOperation.Type == "Delete")
            {
                requestResponse.Data = null;
                notificationData = new List<NotificationOperationData>();
            }
            else
            {
                if (requestResponse.Data != null)
                {
                    notificationData = JsonConvert.DeserializeObject<List<NotificationOperationData>>(requestResponse.Data.ToString());
                }
                else
                {
                    notificationData = new List<NotificationOperationData>();
                }
            }
            return notificationData;
        }
        public static List<CommissionRates> GetCommissionRatesList(string partyID, int PartyType, int operatorId, int serviceId, int serviceProviderId)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCommissionRates?partyID=" + partyID + "&PartyType=" + PartyType + "&operatorId=" + operatorId + "&serviceId=" + serviceId + "&serviceProviderId=" + serviceProviderId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CommissionRates> commissionRates = new List<CommissionRates>();
            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    commissionRates = JsonConvert.DeserializeObject<List<CommissionRates>>(objResponse.Data.ToString());
                else
                    commissionRates = new List<CommissionRates>();

            }
            return commissionRates;
        }

        #region Transection Table Data
        public static List<CommissionDistributionMaster> GetCommissionDistribution()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCommissionDistribution");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CommissionDistributionMaster> commissionRates = new List<CommissionDistributionMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                commissionRates = JsonConvert.DeserializeObject<List<CommissionDistributionMaster>>(objResponse.Data.ToString());
            }
            return commissionRates;
        }
        public static List<TransectionMaster> GetTransections()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetTransections");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TransectionMaster> commissionRates = new List<TransectionMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                commissionRates = JsonConvert.DeserializeObject<List<TransectionMaster>>(objResponse.Data.ToString());
            }
            return commissionRates;
        }
        #endregion

        public static Dictionary<string, string> GetIPAddress()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST

            // Get the IP

            var etc = Dns.GetHostEntry(hostName).AddressList;
            var IPv6Address = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            var IPv4Address = Dns.GetHostEntry(hostName).AddressList[etc.Length - 1].ToString();

            Dictionary<string, string> IpAddressCollection = new Dictionary<string, string>();
            IpAddressCollection.Add("IPv6_Address", IPv6Address);
            IpAddressCollection.Add("IPv4_Address", IPv4Address);
            IpAddressCollection.Add("Hostname", hostName);

            //watcher = new GeoCoordinateWatcher();
            //// Catch the StatusChanged event.  
            //watcher.StatusChanged += Watcher_StatusChanged;
            //// Start the watcher.  
            //watcher.Start();

            return IpAddressCollection;
        }


        //private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) // Find GeoLocation of Device  
        //{
        //    try
        //    {
        //        if (e.Status == GeoPositionStatus.Ready)
        //        {
        //            // Display the latitude and longitude.  
        //            if (watcher.Position.Location.IsUnknown)
        //            {
        //                Latitude = "0";
        //                Longitude = "0";
        //            }
        //            else
        //            {
        //                Latitude = watcher.Position.Location.Latitude.ToString();
        //                Longitude = watcher.Position.Location.Longitude.ToString();
        //            }
        //        }
        //        else
        //        {
        //            Latitude = "0";
        //            Longitude = "0";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Latitude = "0";
        //        Longitude = "0";
        //    }
        //}

        public static List<Dropdown> GetCollegeType(string Type = "CollegeType", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<Dropdown> GetDistrict(string Type = "District", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetTehsil(string MenuId, string Type = "Tehsil")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetProjectSource(string Type = "ProjectSource", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<BankMaster> GetBankDetails(string partyID = "", string Type = "BankDetails")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<BankMaster> bankDetailsList = new List<BankMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                bankDetailsList = JsonConvert.DeserializeObject<List<BankMaster>>(objResponse.Data.ToString());
            }
            return bankDetailsList;
        }


        public static List<Dropdown> GetFeeType(string MenuId = "0", string Type = "FeeType")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }

        public static List<TrusteeBO.TrusteeMember> GetTrusteeMember(int MenuId, string Type = "TrusteeMember")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TrusteeBO.TrusteeMember> trusteeList = new List<TrusteeBO.TrusteeMember>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<TrusteeBO.TrusteeMember>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        public static List<LandInfoBO> GetLandBuildingInfo(string MenuId, string Type = "LandInfo")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<LandInfoBO> trusteeList = new List<LandInfoBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<LandInfoBO>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        public static List<AcdmcTableData> GetAcdmcData(string GUIID)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetAcdmcData?GUIID=" + GUIID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AcdmcTableData> data = new List<AcdmcTableData>();
            var objResponse = new ResponseData();
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
        public static List<AddCourseBO> GetSubjectList(string ApplGUID)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/GetSubjectList?Guid=" + ApplGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddCourseBO> data = new List<AddCourseBO>();
            var objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    data = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());
                }
            }
            return data;
        }

        public static List<DraftApplication> GetDraftApplication(string applGUID = "", string trustID = "")
        {
            trustID = SessionModel.TrustId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetDarftApplications?applGUID=" + applGUID + "&trustID=" + trustID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<DraftApplication> draftApplication = new List<DraftApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    draftApplication = JsonConvert.DeserializeObject<List<DraftApplication>>(requestResponse.Data.ToString());
            }
            return draftApplication;
        }

        public static List<DraftApplication> GetApplicationsToUploadReceipt(string applGUID = "")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetApplicationsToUploadReceipt?applGUID=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<DraftApplication> draftApplication = new List<DraftApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    draftApplication = JsonConvert.DeserializeObject<List<DraftApplication>>(requestResponse.Data.ToString());
            }
            return draftApplication;
        }
        public static List<LandInfoBO> GetLandData(string applGUID = "")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "LandDetails/GetLandData?APPGUID=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<LandInfoBO> draftApplication = new List<LandInfoBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    draftApplication = JsonConvert.DeserializeObject<List<LandInfoBO>>(requestResponse.Data.ToString());
            }
            return draftApplication;
        }

        public static List<Dropdown> GetCourseForDept(int MenuId, string Type = "Course")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public enum ModalSize
        {
            Small,
            Medium,
            Large
        }

        public static List<StaffBO.Staff> GetStaffList(string ApplGUID)
        {

            List<StaffBO.Staff> _result = new List<StaffBO.Staff>();
            #region List Staff
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/StaffList?Guid=" + ApplGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = JsonConvert.DeserializeObject<List<StaffBO.Staff>>(response.Content);
                if (_result != null)
                {
                    return _result;
                    //return RedirectToAction("Index");
                }
            }
            return _result;
            #endregion
        }

        public static CollageFacility GetCollageFacility(string ApplGUID)
        {
            CollageFacility modal = new CollageFacility();
            modal.Guid = ApplGUID;
            CollageFacility _result = new CollageFacility();
            var json = JsonConvert.SerializeObject(modal);
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetCollageFacilityList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //_JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = JsonConvert.DeserializeObject<TrusteeBO.CollageFacility>(response.Content);
                if (_result != null)
                {
                    return _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return _result;
        }

        public static CollageFeeMst GetCollageFeeList(string ApplGUID)
        {
            CollageFeeMst obj = new CollageFeeMst();
            obj.Guid = ApplGUID;
            var json = JsonConvert.SerializeObject(obj);
            CollageFeeMst _result = new CollageFeeMst();
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetFeeDetailsList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<TrusteeBO.CollageFeeMst>(response.Content);
                return _result;
            }
            #endregion
            return _result;
        }

        public static List<DraftApplication> GetAdminApplication(string applGUID = "")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetAdminApplication?applGUID=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<DraftApplication> draftApplication = new List<DraftApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    draftApplication = JsonConvert.DeserializeObject<List<DraftApplication>>(requestResponse.Data.ToString());
            }
            return draftApplication;
        }

        public static List<ApplyNOCApplication> GETNOCApplicationList(int deptID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GETNOCApplicationList?deptID=" + deptID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<ApplyNOCApplication> nocApplication = new List<ApplyNOCApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    nocApplication = JsonConvert.DeserializeObject<List<ApplyNOCApplication>>(requestResponse.Data.ToString());
            }
            return nocApplication;
        }
        public static List<ApplyNOCCLGApplication> GETNOCApplicationClgList(int deptID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GETNOCApplicationClgList?deptID=" + deptID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<ApplyNOCCLGApplication> nocApplication = new List<ApplyNOCCLGApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    nocApplication = JsonConvert.DeserializeObject<List<ApplyNOCCLGApplication>>(requestResponse.Data.ToString());
            }
            return nocApplication;
        }


        #region Committee

        public static List<Dropdown> getCommitteeList(int MenuId)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Committee/getCommitteeList?deptId=" + MenuId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> committees = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    committees = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return committees;
        }

        public static List<CommitteeMembers> getCommitteeMembersList(int MenuId)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Committee/getCommitteeMembersList?committeeID=" + MenuId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CommitteeMembers> committeeMembers = new List<CommitteeMembers>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    committeeMembers = JsonConvert.DeserializeObject<List<CommitteeMembers>>(objResponse.Data.ToString());
                }
            }
            return committeeMembers;
        }
        public static List<Committee> GetExistingCommitteeAsignment(string applicationNumber)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Committee/GetExistingCommitteeAsignment?applicationNumber=" + applicationNumber);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Committee> committeeMembers = new List<Committee>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    committeeMembers = JsonConvert.DeserializeObject<List<Committee>>(objResponse.Data.ToString());
                }
            }
            return committeeMembers;
        }
        #endregion

        #region AddComments by Vivek
        public static ResponseData AddComments(InspectionComments comments)
        {
            var json = JsonConvert.SerializeObject(comments);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddComments");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            List<InspectionComments> commentsList = new List<InspectionComments>();
            if (response.StatusCode.ToString() == "OK")
            {

                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (comments.type == "Select" && objResponse.Data != null)
                {
                    commentsList = JsonConvert.DeserializeObject<List<InspectionComments>>(objResponse.Data.ToString());
                    objResponse.Data = null;
                    objResponse.Data = commentsList;
                }
            }

            return objResponse;
        }
        #endregion


        public static List<Dropdown> GetClgListForDepartment(string MenuId, string Type = "", string partyId = "")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + partyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }

        public static List<Dropdown> GetAvailableNOC(string MenuId, string Type = "AvailableNOC", string partyId = "")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + partyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }

        public static List<Dropdown> GetCourseForCollege(string MenuId, string Type)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }

        public static List<NOCExistingData> GetExistingNOCForCollege(string MenuId, string Type = "GetExistingNOCForCollege")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<NOCExistingData> trusteeList = new List<NOCExistingData>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<NOCExistingData>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }


        public static List<Dropdown> GetSubjectForCourseOldNOC1(string MenuId, string Type, string partyID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&partyID=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }

        public static List<Dropdown> GetSubjectForCourse(string MenuId, string Type, string CollegeId)
        {
            var trusID = Convert.ToInt32(SessionModel.TrustId);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + CollegeId+ "&trustid="+ trusID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        public static List<Dropdown> GetCourseAndSubject(string MenuId, string Type)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }

        public static List<Dropdown> GetApplicableCourseForCollege(string MenuId, string Type = "CourseForClgFromMST")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        public static List<Dropdown> GetApplicableSubjectForCollege(string MenuId, string Type = "SubjectForClgFromMST")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }

        public static List<Dropdown> AmmendmentDetailsForCollege(string MenuId, string Type = "AmmendmentDetailsForCollege")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        public static List<Dropdown> TNOCPNOCExtentionDetails(string MenuId, string Type = "TNOC_PNOCExtention")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        public static List<Dropdown> GetFee(string MenuId, string Type = "GetFeeForApplication")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }


        public static List<Dropdown> GetChangeInName(int MenuId, int clgid, string Type = "CollegeAmdmnt_NameChange")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + clgid);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetChangeInPlace(int MenuId, int clgid, string Type = "CollegeAmdmnt_PlaceChange")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + clgid);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetChangeInSociety(int MenuId, int clgid, string Type = "CollegeAmdmnt_SocietyChange")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + clgid);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetInspectionFee(string MenuId = "", string Type = "InspectionFee")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetColleges(int MenuId = 0, string Type = "GetMyColleges")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<TrusteeBO.OldNOCData> GetOldNOCData(int MenuId = 0, string Type = "GetOLDNOCData")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TrusteeBO.OldNOCData> MenusList = new List<TrusteeBO.OldNOCData>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<TrusteeBO.OldNOCData>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<TrusteeBO.OldNOCData> GetOldNOCDataForCLG(string MenuId, string Type = "GetOLDNOCDataForCLG")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TrusteeBO.OldNOCData> MenusList = new List<TrusteeBO.OldNOCData>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<TrusteeBO.OldNOCData>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<Dropdown> GetCoursesForCollege(int MenuId, string Type = "GetCoursesForCollege")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetCoursesForCollegeOLDNOC1(int MenuId, string Type = "GetCoursesForCollegeOLDNOC1")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetSessionYears(int MenuId = 0, string Type = "GetSessionYears")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<Dropdown> GetUserSendbackForward(string MenuId, string partyID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetUserSendbackForward?MenuId=" + MenuId + "&PartyId=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }

        public static List<CustomMaster> GetSubjectListDropDown(string Guid)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetSubjectList?Guid=" + Guid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CustomMaster> MenusList = new List<CustomMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(objResponse.Data.ToString());
                MenusList = objResponseData.ListRequest;
            }
            return MenusList;
        }

        public static List<StaffBO.Staff> ExistingNOCStaffDetails(ExistingNOCRequest obj)
        {
            var objResponse = new ResponseData();
            List<StaffBO.Staff> _result = new List<StaffBO.Staff>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Staff/ExistingNOCStaffDetails");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = JsonConvert.DeserializeObject<List<StaffBO.Staff>>(response.Content);
                if (_result != null)
                {
                    return _result;
                    //return RedirectToAction("Index");
                }
            }
            return _result;
        }

        public static List<AddCourseBO> ExistingNOCGetSubjectList(ExistingNOCRequest obj)
        {
            var objResponse = new ResponseData();
            List<AddCourseBO> data = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/ExistingNOCGetSubjectList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                data = JsonConvert.DeserializeObject<List<AddCourseBO>>(response.Content);
                //if (objResponse.Data != null)
                //{
                //    data = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());
                //}
            }
            return data;
        }


        public static List<DraftApplication> DepartmentapplicationList(string applGUID = "")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/DepartmentapplicationList?applGUID=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<DraftApplication> draftApplication = new List<DraftApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    draftApplication = JsonConvert.DeserializeObject<List<DraftApplication>>(requestResponse.Data.ToString());
            }
            return draftApplication;
        }

        public static List<AcdmcTableData> GetAcdmcData()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetAcdmcData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AcdmcTableData> data = new List<AcdmcTableData>();
            var objResponse = new ResponseData();
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

        public static List<CustomMaster> GetSubjectListDropDown(string Guid, string Course)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetSubjectList?Guid=" + Guid + "&CourseId=" + Course);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CustomMaster> MenusList = new List<CustomMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(objResponse.Data.ToString());
                MenusList = objResponseData.ListRequest;
            }
            return MenusList;
        }

        public static List<CustomMaster> GetCourseListDropDown(string Guid)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetCourseList?Guid=" + Guid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CustomMaster> MenusList = new List<CustomMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(objResponse.Data.ToString());
                MenusList = objResponseData.ListRequest;
            }
            return MenusList;
        }
        public static List<Dropdown> GetFeeForType(int MenuId, string Type = "GetFeeForType")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<TrusteeBO.DeptMasterApplication> GetDeptMasterApplication(int MenuId = 0, string Type = "MstDeptApplicationList")
        {
            var trusID = SessionModel.TrustId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + trusID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TrusteeBO.DeptMasterApplication> MenusList = new List<TrusteeBO.DeptMasterApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    MenusList = JsonConvert.DeserializeObject<List<TrusteeBO.DeptMasterApplication>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static string GetGuid(string Type, string TrustId, string ColgId, string DeptId, string CourseId)
        {
            string Guid = string.Empty;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetGUID?Type=" + Type + "&TrustId=" + TrustId + "&ColgId=" + ColgId + "&DeptId=" + DeptId + "&CourseId=" + CourseId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                Guid = JsonConvert.DeserializeObject<string>(response.Content);
            }
            return Guid;
        }

        public static List<Dropdown> GetAnimalList(int MenuId = 0, string Type = "GetAnimalList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        //Changes for College Create Page
        public static List<Dropdown> GetUniversityForDept(int MenuId, string Type = "GetUniversityForDEPT")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }


        //Changes for Add Course
        public static List<Dropdown> GetCoursesForDept(int MenuId, string Type = "GetCourseForClg")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        //Update ApplicationStatus
        public static string UpdateApplicationStatus(string MenuId, string Type = "UpdateApplicationStatus")
        {
            var status = "";
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    status = "Status Updated Successfully...";
            }
            return status;
        }


        public static List<ApplicationSubmissionCheck> ValidateApplicationSubmission(string MenuId, string Type = "validateApplicationSubmission")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<ApplicationSubmissionCheck> MenusList = new List<ApplicationSubmissionCheck>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    MenusList = JsonConvert.DeserializeObject<List<ApplicationSubmissionCheck>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<BuildingDetails> GetBuildingInfo(int MenuId, string Type = "BuildingDetails")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<BuildingDetails> trusteeList = new List<BuildingDetails>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<BuildingDetails>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }

        public static List<Architecturesave> BindTable(string AppGuid, string Type)
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
            }
            return departments;
        }

        public static List<AddCourseBO> GetDetailsList(string applGUID)
        {
            //string applGUID = SessionModel.ApplicantGuid;
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

        public static List<AcdmcTableData> GetAcdmcDataNew(string GUIID, int clgID)
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
            var objResponse = new ResponseData();
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


        #region ApplyNOC New page conditions
        public static List<Dropdown> ApplyNOC_NOCCategory(string MenuId, string Type = "ApplyNOC_NOCCategory")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        public static List<Dropdown> ApplyNOC_NOCForms(string MenuId, string Type = "", string partyID = null)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        
        public static List<TNOC_PNOC> ApplyNOC_TNOCExtentionData(string MenuId, string Type = "")
        {
            var trusID = SessionModel.TrustId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + trusID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TNOC_PNOC> trusteeList = new List<TNOC_PNOC>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<TNOC_PNOC>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        
        public static List<TNOC_PNOC> ApplyNOC_PNOC(string MenuId, string Type = "")
        {
            var trusID = SessionModel.TrustId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId + "&PartyId=" + trusID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TNOC_PNOC> trusteeList = new List<TNOC_PNOC>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<TNOC_PNOC>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        #endregion

        public static List<Dropdown> GetOldData(int clgID, string type )
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetOldData?clgID=" + clgID + "&type=" + type);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> trusteeList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    trusteeList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return trusteeList;
        }
        
        public static CollegeDetailsForPreview GetCollegeDetailsForPreview(int clgID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetCollegeDetailsForPreview?clgID=" + clgID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            CollegeDetailsForPreview details = new CollegeDetailsForPreview();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    details = JsonConvert.DeserializeObject<CollegeDetailsForPreview>(objResponse.Data.ToString());
                }
            }
            return details;
        }
    }
}