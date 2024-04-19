using MVCPais.Models.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPais.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Pais pa = new Pais();
            return View(pa.ConsultarPaises());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            Pais pais = new Pais();
            pais = pais.ConsultaIndividualPais(id);
            return View(pais);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Pais pai = new Pais();
                pai.paisdesc = collection["paisdesc"];
                int id = pai.AgregaPais();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            Pais pa = new Pais();
            pa = pa.ConsultaIndividualPais(id);
            return View(pa);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Pais pa = new Pais();
                pa.paisdesc = collection["paisdesc"].ToString();
                pa.ActualizaPais(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            Pais pa = new Pais();
            pa = pa.ConsultaIndividualPais(id);

            return View(pa);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pais pa = new Pais();
                pa.EliminarPais(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
