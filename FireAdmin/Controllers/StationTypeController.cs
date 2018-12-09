using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using FireAdmin.Models;
using System.Data.SqlClient;
using System.Data;

namespace FireAdmin.Controllers
{
    public class StationTypeController : Controller
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var oLst = new List<StationType>();
            var oDt = new DataTable();
            try
            {
                using (SqlConnection oConn = new SqlConnection(_connString))
                {
                    string sqlString = "SELECT * FROM StationTypes WHERE IsDeleted Is Null OR IsDeleted <> 'True' ORDER BY StationType ASC";
                    using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
                    {
                        oCmd.Connection.Open();
                        oCmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter oDa = new SqlDataAdapter(oCmd))
                        {
                            oDa.Fill(oDt);
                            foreach (DataRow oDr in oDt.Rows)
                            {
                                oLst.Add(new StationType
                                {
                                    Id = new Guid(oDr["StationTypeId"].ToString()),
                                    Type = oDr["StationType"].ToString()
                                });
                            }
                        }
                        return View(oLst);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = string.Format(format: "Error: {0}", arg0: ex.Message);
                return View();
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new StationType());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(StationType stationType)
        {
            try
            {
                if (StationTypeExists(stationType))
                {
                    ViewBag.Error = string.Format(format: "A record for Station Type '{0}' already exists!", arg0: stationType.Type);
                    return View();
                }
                else
                {
                    using (SqlConnection oConn = new SqlConnection(_connString))
                    {
                        string sqlString = "INSERT INTO StationTypes (StationTypeId, StationType) VALUES (@StationTypeId, @StationType)";
                        using (SqlCommand oCmd = new SqlCommand(sqlString))
                        {
                            oCmd.Connection = oConn;
                            var oPrms = new SqlParameter[2];
                            oPrms[0] = new SqlParameter(parameterName: "@StationTypeId", dbType: SqlDbType.UniqueIdentifier, size: 128);
                            oPrms[0].Value = Guid.NewGuid();
                            oPrms[1] = new SqlParameter(parameterName: "@StationType", dbType: SqlDbType.VarChar, size: 128);
                            oPrms[1].Value = stationType.Type;
                            oCmd.Parameters.AddRange(oPrms);
                            oConn.Open();
                            oCmd.ExecuteScalar();
                            oConn.Close();
                        }
                    }
                    return RedirectToAction(actionName: "Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = string.Format(format: "Error: {0}", arg0: ex.Message);
                return View();
            }
        }
                
        protected bool StationTypeExists(StationType stationType)
        {
            // get the connection
            using (SqlConnection oConn = new SqlConnection(_connString))
            {
                // write the sql statement to execute
                string sqlString = "SELECT StationType FROM StationTypes WHERE StationType=@StationType";
                // instantiate the command object to fire
                using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
                {
                    // attach the parameter to pass, if no parameter is in the sql no need to attach
                    var oPrms = new SqlParameter[1];
                    oPrms[0] = new SqlParameter(parameterName: "@StationType", dbType: SqlDbType.NVarChar, size: 128);
                    oPrms[0].Value = stationType.Type;
                    oCmd.Parameters.AddRange(oPrms);
                    oConn.Open();
                    object oObj = oCmd.ExecuteScalar();
                    oConn.Close();

                    if (oObj != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
