﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MapDrawRouteTool.Controllers
{
    public class MapController : Controller
    {
        //
        // GET: /Map/

        public ActionResult Index()
        {
            return View();
        }

        public void Save(string s)
        {
            string[] t = s.Split(',');

            Debug.WriteLine(t.Length.ToString());

            for (int i = 0; i < t.Length; i = i + 2)
            {
                var mystring =
                    "Persist Security Info=False;database=trackabus_dk_db;server=mysql23.unoeuro.com;Connect Timeout=30;user id=trackabus_dk; pwd=1083209421";
                using (var connection = new MySqlConnection(mystring))
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        try
                        {
                            t[i] = t[i].TrimEnd(')');
                            t[i] = t[i].TrimStart('(');
                            t[i + 1] = t[i + 1].TrimEnd(')');
                            t[i + 1] = t[i + 1].TrimStart('(');

                            connection.Open();
                            Debug.WriteLine(t[i] + "   " + t[i + 1]);
                            cmd.CommandText = "INSERT INTO BusRoutePoints (bus_lat, bus_lon) VALUES(?Lat, ?lng);";
                            cmd.Parameters.Add("?Lat", MySqlDbType.Decimal).Value = decimal.Parse(t[i]);
                            cmd.Parameters.Add("?lng", MySqlDbType.Decimal).Value = decimal.Parse(t[i + 1]);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.ToString());
                        }

                    }
                }

            }
        }
    }
}
