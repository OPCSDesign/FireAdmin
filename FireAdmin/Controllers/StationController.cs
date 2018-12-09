using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using FireAdmin.Models;
using System.Data;
using System.Data.SqlClient;

namespace FireAdmin.Controllers
{
	public class StationController : Controller
	{
		readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

		// GET: Station
		public ActionResult Index()
		{
			var oLst = new List<Station>();
			var oDt = new DataTable();
			try
			{
				using (SqlConnection oConn = new SqlConnection(_connString))
				{
					string sqlString = "SELECT * FROM Stations WHERE IsDeleted Is Null OR IsDeleted <> 'True' ORDER BY Station ASC";
					using (SqlCommand oCmd = new SqlCommand(sqlString, oConn))
					{
						oCmd.Connection.Open();
						oCmd.CommandType = CommandType.Text;
						using (SqlDataAdapter oDa = new SqlDataAdapter(oCmd))
						{
							oDa.Fill(oDt);
							foreach (DataRow oDr in oDt.Rows)
							{
								oLst.Add(new Station
								{
									StationId = new Guid(oDr["StationId"].ToString()),
									StationName = oDr["Station"].ToString()
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

		// GET: Station/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Station/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Station/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here
				return RedirectToAction(actionName: "Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Station/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Station/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction(actionName: "Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Station/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Station/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction(actionName: "Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
