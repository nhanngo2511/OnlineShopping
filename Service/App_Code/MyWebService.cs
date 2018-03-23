using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

/// <summary>
/// Summary description for MyWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MyWebService : System.Web.Services.WebService {

    public MyWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    OnlineShoppingDB AccessDB = new OnlineShoppingDB();


    [WebMethod]
    public float ChangeMoneyToUSD(float CurrentMoney)
    {
        float ExchangeRate = 0;
        float ResultMonney = 0;

        XmlDocument doc = new XmlDocument();
        doc.Load("http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx");

        foreach (XmlNode node in doc.SelectNodes("//ExrateList/Exrate"))
        {
            if (node.Attributes["CurrencyCode"].Value == "USD")
            {
                ExchangeRate = Int32.Parse(node.Attributes["Transfer"].Value);
            }
        }

        ResultMonney = CurrentMoney / ExchangeRate;

        return ResultMonney;
    }

    [WebMethod]
    public float ChangeMoneyToVND(float CurrentMoney)
    {
        float ExchangeRate = 0;
        float ResultMonney = 0;

        XmlDocument doc = new XmlDocument();
        doc.Load("http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx");

        foreach (XmlNode node in doc.SelectNodes("//ExrateList/Exrate"))
        {
            if (node.Attributes["CurrencyCode"].Value == "USD")
            {
                ExchangeRate = Int32.Parse(node.Attributes["Transfer"].Value);
            }
        }

        ResultMonney = CurrentMoney * ExchangeRate;

        return ResultMonney;
    }

  
    [WebMethod]
    public DataSet GetBrands()
    {
        return AccessDB.GetBrandsDataAccess();
    }
   

    [WebMethod]
    public void CreateBrand(string Name)
    {
        AccessDB.CreateBrandDataAccess(Name);
    }



}
