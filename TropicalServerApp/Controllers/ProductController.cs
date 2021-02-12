using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalServerApp.Models;
using System.Configuration;
using System.Data;

namespace TropicalServerApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        //string connString = Convert.ToString(ConfigurationManager.AppSettings["TropicalServerConnectionString"]);

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataAdapter dr;
        SqlDataReader dd;
        
        
        public ActionResult Product()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = ConfigurationManager.AppSettings["TropicalServerConnectionString"];
        }
        [HttpGet]
        public ActionResult Orders()
        {
            return View();
            //DataSet ds = new DataSet();
            //connectionString();
            //con.Open();
            //com.Connection = con;
            //com.CommandText = "select tblOrder.OrderTrackingNumber, tblOrder.OrderDate, tblCustomer.CustNumber, tblCustomer.CustName, tblCustomer.CustOfficeAddress1, tblCustomer.CustRouteNumber from tblOrder inner join  tblCustomer on tblOrder.OrderCustomerNumber = tblCustomer.CustNumber where OrderTrackingNumber is not null";
            //dr = new SqlDataAdapter(com);
            //dr.Fill(ds);
            //con.Close();
            //return View(ds);
        }
        public JsonResult Get_data()
        {
            DataSet ds = new DataSet();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select DISTINCT tblOrder.OrderTrackingNumber, CONVERT(Date, tblOrder.OrderDate) OrderDate, tblCustomer.CustNumber, tblCustomer.CustName, tblCustomer.CustOfficeAddress1, tblCustomer.CustRouteNumber from tblOrder inner join  tblCustomer on tblOrder.OrderCustomerNumber = tblCustomer.CustNumber where OrderTrackingNumber is not null";
            dr = new SqlDataAdapter(com);
            dr.Fill(ds);
            con.Close();
            List<OrderTable> list = new List<OrderTable>();
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                list.Add(new OrderTable
                {
                    TrackingNum = r["OrderTrackingNumber"].ToString(),
                    OrderDate = r["OrderDate"].ToString(),
                    CustomerID = r["CustNumber"].ToString(),
                    CustomerName = r["CustName"].ToString(),
                    Address = r["CustOfficeAddress1"].ToString(),
                    RouteNum = r["CustRouteNumber"].ToString()
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
            
        
        //[HttpPost]
        //public ActionResult CustNum()
        //{
        //    DataSet ds = new DataSet();
        //    connectionString();
        //    con.Open();
        //    com.Connection = con;
        //    string id = Request.Form["CustomerID"];
        //    com.CommandText = "select tblOrder.OrderTrackingNumber, tblOrder.OrderDate, tblCustomer.CustNumber, tblCustomer.CustName, tblCustomer.CustOfficeAddress1, tblCustomer.CustRouteNumber from tblOrder inner join  tblCustomer on tblOrder.OrderCustomerNumber = tblCustomer.CustNumber where OrderTrackingNumber is not null and tblCustomer.CustNumber = '"+ id +"'";
        //    dr = new SqlDataAdapter(com);
        //    dr.Fill(ds);
        //    con.Close();
        //    return View(ds);
        //}
        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Clear();
            return View("~/Views/Login/Login.cshtml");
        }
        List<Reads> list1 = new List<Reads>();
        public ActionResult Read(string s)
        {
            
            //foreach (var val in list1)
            //{
            //    r.TrackingNums = val.TrackingNums;
            //    r.OrderDates = val.OrderDates;
            //    r.RouteNums = val.RouteNums;
            //    r.theAddress = val.theAddress;
            //    r.CustomerIDs = val.CustomerIDs;
            //    r.CustomerNames = val.CustomerNames;
            //}
            return View();
        }
        //[HttpPost]
        //public ActionResult Read(string s)
        //{
        //    DataSet ds = new DataSet();
        //    connectionString();
        //    con.Open();
        //    com.Connection = con;
        //    com.CommandText = "select DISTINCT tblOrder.OrderTrackingNumber, CONVERT(Date, tblOrder.OrderDate) OrderDate, tblCustomer.CustNumber, tblCustomer.CustName, tblCustomer.CustOfficeAddress1, tblCustomer.CustRouteNumber from tblOrder inner join  tblCustomer on tblOrder.OrderCustomerNumber = tblCustomer.CustNumber where OrderTrackingNumber is not null and OrderTrackingNumber = '"+ s +"'";
        //    dr = new SqlDataAdapter(com);
        //    dr.Fill(ds);
        //    con.Close();
        //    //if (dd.Read())
        //    //{
                
        //    //    r.TrackingNums = dd["OrderTrackingNumber"].ToString();
        //    //    r.OrderDates = dd["OrderDate"].ToString();
        //    //    r.CustomerIDs = dd["CustNumber"].ToString();
        //    //    r.CustomerNames = dd["CustName"].ToString();
        //    //    r.theAddress = dd["CustOfficeAddress1"].ToString();
        //    //    r.RouteNums = dd["CustRouteNumber"].ToString();
        //    //}
        //    foreach (DataRow ss in ds.Tables[0].Rows)
        //    {
        //        list1.Add(new Reads
        //        {
        //            TrackingNums = ss["OrderTrackingNumber"].ToString(),
        //            OrderDates = ss["OrderDate"].ToString(),
        //            CustomerIDs = ss["CustNumber"].ToString(),
        //            CustomerNames = ss["CustName"].ToString(),
        //            theAddress = ss["CustOfficeAddress1"].ToString(),
        //            RouteNums = ss["CustRouteNumber"].ToString()
        //        });
        //    }
        //    return View(list1);


        //}
    }
}