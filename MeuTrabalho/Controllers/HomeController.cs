using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeuTrabalho.Models;
using System.Data.SqlClient;
using Dapper;

namespace MeuTrabalho.Controllers
{
    public class HomeController : Controller, IDisposable
    {
        SqlConnection connection;

        public HomeController()
        {
            this.connection = new SqlConnection("Server=.;Database=DB01;User=user1;Password=user1");
        }

        public IActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Account");
        }

        public IActionResult Dashboard(string name)
        {
            if( name == null )
            {
                throw new ArgumentNullException(name);
            }

            ViewBag.Name = name;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            try
            {
                this.connection.Open();

                SqlCommand sql = new SqlCommand("INSERT tbLog VALUES ('about')", this.connection);
                sql.ExecuteReader();                
            }
            catch(Exception ex)
            {
                ViewData["Message"] = "ERROR ABOUT";
            }

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            try
            {
                SqlConnection conn1 = this.connection;

                // conn1.Open();

                //SqlCommand sql = new SqlCommand("INSERT tbLog VALUES ('contact')");
                //sql.Connection = conn1;

                //sql.ExecuteScalar();

                conn1.Execute("INSERT tbLog VALUES (@texto)", new { texto = "contact" });

            }
            catch(Exception ex)
            {
                return RedirectToAction("Error");
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
