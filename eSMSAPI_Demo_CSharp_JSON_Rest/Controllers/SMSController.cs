using eSMSAPI_Demo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace eSMSAPI_Demo.Controllers
{
  

    public class SMSController : Controller
    {

        const string APIKey = "A302FE6CA0153C0FCFAEB0765A5C77";//Login to eSMS.vn to get this";//Dang ky tai khoan tai esms.vn de lay key//Register account at esms.vn to get key
        const string SecretKey = "773C171D062FACF6878425F8714FEB";//Login to eSMS.vn to get this";
        public ActionResult Index()
        {
            SendPostRequest("http://api.esms.vn/MainService.svc/xml/GetSmsSentData");
            return View();
          
        }

        //Send SMS with Sender is a number
        public ActionResult SendJson(string phone,string message)
        {
            //Sample Request
            //http://rest.esms.vn/SendMultipleMessage_V4_get?Phone={Phone}&Content={Content}&ApiKey={ApiKey}&SecretKey={SecretKey}&IsUnicode={IsUnicode}&Brandname={Brandname}&SmsType={SmsType}&Sandbox={Sandbox}&Priority={Priority}&RequestId={RequestId}&SendDate={SendDate}
            
            // Create URL, method 1:
            string URL = "http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone=" + phone + "&Content=" + message + "&ApiKey=" + APIKey + "&SecretKey=" + SecretKey + "&IsUnicode=0&SmsType=4";
            //-----------------------------------
           
            //-----------------------------------
            string result = SendGetRequest(URL);
            JObject ojb = JObject.Parse(result);
            int CodeResult = (int)ojb["CodeResult"];//100 is successfull
           
            string SMSID = (string)ojb["SMSID"];//id of SMS
            return RedirectToAction("Index", "SMS");
        }

        private string SendPostRequest(string RequestUrl, string XMLPostData)
        {

            Uri address = new Uri(RequestUrl);
            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;

           // string strXMLPost = "<ROOT><RQST><APIKEY>A302FE6CA0153C0FCFAEB0765A5C77</APIKEY><SECRETKEY>773C171D062FACF6878425F8714FEB</SECRETKEY><FROM>14/05/2010</FROM><TO>19/05/20</TO></RQST></ROOT>";

            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] bytes;
            bytes = encoding.GetBytes(XMLPostData);
           
            
            if (address == null) { throw new ArgumentNullException("address"); }
            try
            {
                request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "text";
                request.Accept = "aplication/xml";
                request.ContentLength = bytes.Length;
                
               
                Stream stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                response = request.GetResponse() as HttpWebResponse;
                if (request.HaveResponse == true && response != null)
                {
                    reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    result = result.Replace("</string>", "");
                    return result;
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                        Console.WriteLine(
                            "The server returned '{0}' with the status code {1} ({2:d}).",
                            errorResponse.StatusDescription, errorResponse.StatusCode,
                            errorResponse.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }
            return null;


           
        }

        private string SendGetRequest(string RequestUrl)
        {
            Uri address = new Uri(RequestUrl);
            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;
            if (address == null) { throw new ArgumentNullException("address"); }
            try
            {
                request = WebRequest.Create(address) as HttpWebRequest;
                request.UserAgent = ".NET Sample";
                request.KeepAlive = false;
                request.Timeout = 15 * 1000;
                response = request.GetResponse() as HttpWebResponse;
                if (request.HaveResponse == true && response != null)
                {
                    reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    result = result.Replace("</string>", "");
                    return result;
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                        Console.WriteLine(
                            "The server returned '{0}' with the status code {1} ({2:d}).",
                            errorResponse.StatusDescription, errorResponse.StatusCode,
                            errorResponse.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }
            return null;
        }

        //Send SMS with Alpha Sender
        public ActionResult SendBrandnameJson(string phone, string message,string brandname)
        {
            //http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone={Phone}&Content={Content}&BrandnameCode={BrandnameCode}&ApiKey={ApiKey}&SecretKey={SecretKey}&SmsType={SmsType}&SendDate={SendDate}&SandBox={SandBox}
            //url vi du
            // sử dụng cách 1:
            string URL = "http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone=" + phone + "&Content=" + message + "&Brandname=" + brandname + "&ApiKey=" + APIKey + "&SecretKey=" + SecretKey + "&IsUnicode=0&SmsType=2";
           
            string result = SendGetRequest(URL);
            JObject ojb = JObject.Parse(result);
            int CodeResult = (int)ojb["CodeResult"];//trả về 100 là thành công
            int CountRegenerate = (int)ojb["CountRegenerate"];
            string SMSID = (string)ojb["SMSID"];//id của tin nhắn
            return RedirectToAction("Index", "SMS");
        }
       
        //Get Account Balance - Lay so du tai khoan
        public ActionResult GetBalance()
        {
            string data = "http://rest.esms.vn/MainService.svc/json/GetBalance/" + APIKey + "/" + SecretKey + "";
            string result = SendGetRequest(data);
            JObject ojb = JObject.Parse(result);
            int CodeResult = (int)ojb["CodeResponse"];//trả về 100 là thành công
            int UserID = (int)ojb["UserID"];//id tài khoản
            long Balance = (long)ojb["Balance"];//tiền trong tài khoản

            BalanceModel balaModel = new BalanceModel();
            balaModel.UserID = UserID;
            balaModel.Balance = Balance;
            return View(balaModel);
        }

        public ActionResult GetBrands()
        {

            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
          //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "sourav:kayal");
           

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(new Uri("http://localhost:19878/api/brand/GetBrands")).Result;

            List<BrandModel> bras = new List<BrandModel>();
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body
                var dataObjects = response.Content.ReadAsAsync<List<BrandModel>>().Result;

                foreach (var d in dataObjects)
                {
                    bras.Add(d as BrandModel);
                }
            }
            else
            {
                return Content("{0} ({1})", (int)response.StatusCode + response.ReasonPhrase);              
            }  

            return View();

        }
        
        
        
        
    }
}
