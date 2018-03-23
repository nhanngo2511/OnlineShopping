using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ONLINE_SHOPPING.BO.Models;

namespace ONLINE_SHOPPING.BO.Controllers
{
    public class CustomerController : Controller
    {
        const string APIKey = "A302FE6CA0153C0FCFAEB0765A5C77";
        const string SecretKey = "773C171D062FACF6878425F8714FEB";

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
                request.Accept = "aplication/json";
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

        [HttpPost]
        public ActionResult SendSMS(string phone, string message)
        {

            string URL = "http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone=" + phone + "&Content=" + message + "&ApiKey=" + APIKey + "&SecretKey=" + SecretKey + "&IsUnicode=0&SmsType=4";

            string result = SendGetRequest(URL);
            JObject ojb = JObject.Parse(result);
            int CodeResult = (int)ojb["CodeResult"];//100 is successfull

            return RedirectToAction("SMSSentList");
        }

        [Authorize]
        public ActionResult SendSMS()
        {
            return View();
        }

        private static Dictionary<string, object> GetXmlData(XElement xml)
        {
            var attr = xml.Attributes().ToDictionary(d => d.Name.LocalName, d => (object)d.Value);
            if (xml.HasElements) attr.Add("_value", xml.Elements().Select(e => GetXmlData(e)));
            else if (!xml.IsEmpty) attr.Add("_value", xml.Value);

            return new Dictionary<string, object> { { xml.Name.LocalName, attr } };
        }

        [HttpPost]
        public ActionResult SMSSentList(DateTime StartDate, DateTime EndDate)
        {
            return RedirectToAction("SMSSentList", new { StartDate = StartDate, EndDate = EndDate });
        }

        [Authorize]
        public ActionResult SMSSentList(DateTime? StartDate, DateTime? EndDate)
        {
         
           
            if (StartDate == null)
            {
                StartDate = new DateTime(2015, 1, 1);
                EndDate = DateTime.Now;
            }

            string URL = "http://api.esms.vn/MainService.svc/xml/GetSmsSentData";

            string XMLPost = "<ROOT><RQST><APIKEY>A302FE6CA0153C0FCFAEB0765A5C77</APIKEY><SECRETKEY>773C171D062FACF6878425F8714FEB</SECRETKEY><FROM>" + StartDate.Value.ToString("dd/MM/yyyy") + "</FROM><TO>" + EndDate.Value.AddDays(1).ToString("dd/MM/yyyy") + "</TO></RQST></ROOT>";

            string result = SendPostRequest(URL, XMLPost);

            List<SmsSentDataModel> ListSmsSentDataModel = XMLToObject(result);

            return View(ListSmsSentDataModel);
        }

        private List<SmsSentDataModel> XMLToObject(string xml)
        {
            var XMLDoc = XDocument.Parse(xml);
            List<SmsSentDataModel> ListSmsSentData = XMLDoc.Descendants("SmsSentData").Select(x => new SmsSentDataModel
            {
                Phone = x.Element("Phone").Value,
                Content = x.Element("Content").Value,
                SentStatus = Boolean.Parse(x.Element("SentStatus").Value),
                SentTime = DateTime.Parse(x.Element("SentTime").Value),
                SmsType = Int32.Parse(x.Element("SmsType").Value)
            }).OrderByDescending(x => x.SentTime).ToList();

            return ListSmsSentData;
        }

    }


}
