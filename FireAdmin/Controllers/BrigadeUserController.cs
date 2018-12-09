using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireAdmin.Controllers
{
    public class BrigadeUserController : Controller
    {
        // GET: BrigadeUser
        public ActionResult Index()
        {
            return View();
        }

        // GET: BrigadeUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrigadeUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrigadeUser/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BrigadeUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BrigadeUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BrigadeUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrigadeUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
