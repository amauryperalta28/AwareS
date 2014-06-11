using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AwareswebApp.Models;

namespace AwareswebApp.Controllers
{
    public class ConsumosController : Controller
    {
        private DbModels db = new DbModels();

        // GET: Consumos
        public string Receive(int idColaborador, string lecturaConsumo)
        {
            Consumo consumo = new Consumo(idColaborador, lecturaConsumo);

            db.Consumos.Add(consumo);
            db.SaveChanges();

            return "1";
        }

        public ActionResult Menu()
        {
            return View();
        }
        // GET: Consumos
        public ActionResult Index()
        {
            return View(db.Consumos.ToList());
        }

        // GET: Consumos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumo consumo = db.Consumos.Find(id);
            if (consumo == null)
            {
                return HttpNotFound();
            }
            return View(consumo);
        }

        // GET: Consumos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consumos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConsumo,idColaborador,tipoConsumo,lectura,fechaCreacion")] Consumo consumo)
        {
            if (ModelState.IsValid)
            {
                db.Consumos.Add(consumo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consumo);
        }

        // GET: Consumos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumo consumo = db.Consumos.Find(id);
            if (consumo == null)
            {
                return HttpNotFound();
            }
            return View(consumo);
        }

        // POST: Consumos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConsumo,idColaborador,tipoConsumo,lectura,fechaCreacion")] Consumo consumo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consumo);
        }

        // GET: Consumos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumo consumo = db.Consumos.Find(id);
            if (consumo == null)
            {
                return HttpNotFound();
            }
            return View(consumo);
        }

        // POST: Consumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consumo consumo = db.Consumos.Find(id);
            db.Consumos.Remove(consumo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
