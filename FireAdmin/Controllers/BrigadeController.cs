using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNet.Identity;
using FireAdmin.Models;

namespace FireAdmin.Controllers
{
	public class BrigadeController : Controller
	{
		readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

		[HttpGet]
        [Authorize(Roles = "OPCS")]
        public ActionResult Index()
		{
			var oLst = new List<Brigade>();
			var oDt = new DataTable();
			try
			{
				using (SqlConnection oConn = new SqlConnection(_connString))
				{
					string sqlString = "SELECT * FROM Brigades WHERE IsDeleted Is Null OR IsDeleted <> 'True' ORDER BY Brigade ASC";
					using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
					{
						oCmd.Connection.Open();
						oCmd.CommandType = CommandType.Text;
						using (SqlDataAdapter oDa = new SqlDataAdapter(oCmd))
						{
							oDa.Fill(oDt);
							foreach (DataRow oDr in oDt.Rows)
							{
								oLst.Add(new Brigade
								{
									Id = new Guid(oDr["BrigadeId"].ToString()),
									Name = oDr["Brigade"].ToString()
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
        [Authorize(Roles = "OPCS")]
        public ActionResult Create()
		{
			return View(new Brigade());
		}

		[HttpPost]
        [Authorize(Roles = "OPCS")]
        public ActionResult Create(Brigade brigade)
		{
			try
			{
				if (BrigadeExists(brigade))
				{
					ViewBag.Error = string.Format(format: "A record for Brigade '{0}' already exists!", arg0: brigade.Name);
					return View();
				}
				else
				{
					using (SqlConnection oConn = new SqlConnection(_connString))
					{
						string sqlString = "INSERT INTO Brigades (BrigadeId, Brigade) VALUES (@BrigadeId, @Brigade)";
						using (SqlCommand oCmd = new SqlCommand(sqlString))
						{
							oCmd.Connection = oConn;
							var oPrms = new SqlParameter[2];
							oPrms[0] = new SqlParameter(parameterName: "@BrigadeId", dbType: SqlDbType.UniqueIdentifier, size: 36);
							oPrms[0].Value = Guid.NewGuid();
							oPrms[1] = new SqlParameter(parameterName: "@Brigade", dbType: SqlDbType.VarChar, size: 128);
							oPrms[1].Value = brigade.Name;
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

		[HttpGet]
        [Authorize(Roles = "OPCS")]
        public ActionResult Edit(Guid id)
		{
			var brigade = new Brigade();
			try
			{
				using (SqlConnection oConn = new SqlConnection(_connString))
				{
					string sqlString = "SELECT * FROM Brigades WHERE BrigadeId = @BrigadeId";
					using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
					{
						if (ModelState.IsValid)
						{
							oCmd.Connection.Open();
							oCmd.CommandType = CommandType.Text;
							var oPrms = new SqlParameter[1];
							oPrms[0] = new SqlParameter(parameterName: "@BrigadeId", dbType: SqlDbType.UniqueIdentifier, size: 128);
							oPrms[0].Value = id;
							oCmd.Parameters.AddRange(oPrms);
							using (SqlDataReader oRdr = oCmd.ExecuteReader())
							{
								while (oRdr.Read())
								{
									brigade.Id = new Guid(oRdr["BrigadeId"].ToString());
									brigade.Name = oRdr["Brigade"].ToString();
								}
							}
						}
						return View(brigade);
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Error = string.Format(format: "Error: {0}", arg0: ex.Message);
				return View();
			}
		}

		[HttpPost]
        [Authorize(Roles = "OPCS")]
        public ActionResult Edit(Brigade brigade, Guid id)
		{
			try
			{
				using (SqlConnection oConn = new SqlConnection(_connString))
				{
					string sqlString = "UPDATE Brigades SET Brigade=@Brigade WHERE BrigadeId=@BrigadeId";
					using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
					{
						//oCmd.CommandText = sqlString;
						oCmd.CommandType = CommandType.Text;
						var oPrms = new SqlParameter[2];
						oPrms[0] = new SqlParameter(parameterName: "@Brigade", dbType: SqlDbType.VarChar, size: 128);
						oPrms[0].Value = brigade.Name;
						oPrms[1] = new SqlParameter(parameterName: "@BrigadeId", dbType: SqlDbType.UniqueIdentifier, size: 128);
						oPrms[1].Value = id;
						oCmd.Parameters.AddRange(oPrms);
						oConn.Open();
						oCmd.ExecuteNonQuery();
						oConn.Close();
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Error = string.Format(format: "Error: {0}", arg0: ex.Message);
				return View();
			}
			return RedirectToAction(actionName: "Index");
		}

		[HttpGet]
        [Authorize(Roles = "OPCS")]
        public ActionResult Delete(Guid id)
		{
			var brigade = new Brigade();
			try
			{
				using (SqlConnection oConn = new SqlConnection(_connString))
				{
					string sqlString = "SELECT * FROM Brigades WHERE BrigadeId = @BrigadeId";
					using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
					{
						if (ModelState.IsValid)
						{
							oCmd.Connection.Open();
							oCmd.CommandType = CommandType.Text;
							var oPrms = new SqlParameter[1];
							oPrms[0] = new SqlParameter(parameterName: "@BrigadeId", dbType: SqlDbType.UniqueIdentifier, size: 128);
							oPrms[0].Value = id;
							oCmd.Parameters.AddRange(oPrms);
							using (SqlDataReader oRdr = oCmd.ExecuteReader())
							{
								while (oRdr.Read())
								{
									brigade.Id = new Guid(oRdr["BrigadeId"].ToString());
									brigade.Name = oRdr["Brigade"].ToString();
								}
							}
						}
						return View(brigade);
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Error = string.Format(format: "Error: {0}", arg0: ex.Message);
				return View();
			}
		}

		[HttpPost]
        [Authorize(Roles = "OPCS")]
        public ActionResult Delete(Brigade brigade, Guid id)
		{
			try
			{
				using (SqlConnection oConn = new SqlConnection(_connString))
				{
					string sqlString = "UPDATE Brigades SET IsDeleted=@IsDeleted, DeletedBy=@DeletedBy, DateDeleted=@DateDeleted WHERE BrigadeId=@BrigadeId";
					using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
					{
						oCmd.CommandType = CommandType.Text;
						var oPrms = new SqlParameter[4];
						oPrms[0] = new SqlParameter(parameterName: "@IsDeleted", dbType: SqlDbType.Bit);
						oPrms[0].Value = true;
						oPrms[1] = new SqlParameter(parameterName: "@DeletedBy", dbType: SqlDbType.UniqueIdentifier, size: 128);
						oPrms[1].Value = new Guid(User.Identity.GetUserId());
						oPrms[2] = new SqlParameter(parameterName: "@DateDeleted", dbType: SqlDbType.Date, size: 10);
						oPrms[2].Value = DateTime.Today;
						oPrms[3] = new SqlParameter(parameterName: "@BrigadeId", dbType: SqlDbType.UniqueIdentifier, size: 128);
						oPrms[3].Value = id;
						oCmd.Parameters.AddRange(oPrms);
						oConn.Open();
						oCmd.ExecuteNonQuery();
						oConn.Close();
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Error = string.Format(format: "Error: {0}", arg0: ex.Message);
				return View();
			}
			return RedirectToAction(actionName: "Index");
		}

		protected bool BrigadeExists(Brigade brigade)
		{
			// get the connection
			using (SqlConnection oConn = new SqlConnection(_connString))
			{
				// write the sql statement to execute
				string sqlString = "SELECT Brigade FROM Brigades WHERE Brigade=@Brigade";
				// instantiate the command object to fire
				using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
				{
					// attach the parameter to pass, if no parameter is in the sql no need to attach
					var oPrms = new SqlParameter[1];
					oPrms[0] = new SqlParameter(parameterName: "@Brigade", dbType: SqlDbType.NVarChar, size: 128);
					oPrms[0].Value = brigade.Name;
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