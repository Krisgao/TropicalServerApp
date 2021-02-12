using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalServerApp.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace TropicalServerApp.Controllers
{
    public class LoginController : Controller
    {
        // static user object 
        // GET: Login
        
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = ConfigurationManager.AppSettings["TropicalServerConnectionString"];
        }
        [HttpPost]
        public ActionResult Submit(Login lo)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from tblUserLogin where UserID = '"+lo.loginID+"' and Password = '"+lo.ThisPassword+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return RedirectToAction("Orders", "Product");
            }
            else
            {
                con.Close();
                //return View("Error");
                
                return View("Login");
            }

            
        }

        [HttpGet]
        public ActionResult GetPassword() {
            return View("GetPassword");
        }
        [HttpPost]
        public ActionResult verify(GetInfo getp)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from tblUserLogin where UserID = '" + getp.Username + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                var sender = new MailAddress("galaboompow@gmail.com", "galaboompow@gmail.com");
                var receiver = new MailAddress("supervenmo@gmail.com", "Receiver");
                var password = "Zzz1234567890!";
                var sub = "Your Login Info";
                var body = "Your Login Password is: " + dr["Password"];
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sender.Address, password)

                };
                using (var mess = new MailMessage(sender, receiver)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                con.Close();
                return View("Sent");
            }
            else
            {
                con.Close();
                return View("Error");
            }
            
        }
        [HttpGet]
        public ActionResult GetID()
        {
            return View("GetID");
        }
        [HttpPost]
        public ActionResult check(GetName getN)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select UserID from tblUserLogin where EmailID = '" + getN.Accountemail + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                
                var senderEmail = new MailAddress("galaboompow@gmail.com", "galaboompow@gmail.com");
                var receiverEmail = new MailAddress("supervenmo@gmail.com", "Receiver");
                var password = "Zzz1234567890!";
                var sub = "Your Login Info";
                var body = "Your Login ID is: " + dr["UserID"];
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                    
            };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                { 
                    smtp.Send(mess);
                }
                con.Close();
                return View("Sent");
            }
            else
            {
                con.Close();
                return View("Error");
            }

        }
    }
}