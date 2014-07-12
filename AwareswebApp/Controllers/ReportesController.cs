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
    public class ReportesController : Controller
    {
        private DbModels db = new DbModels();

        public ActionResult Menu()
        {
            return View();
        }
        // GET: Reportes
        public ActionResult Index()
        {
            var listaReportes = (db.Reportes.ToList());

            ViewBag.Latitud = 18.523471;
            ViewBag.Longitud = -69.8746229;
            ViewBag.coordenadas = listaReportes;

                         
            return View(listaReportes);
        }

        // GET: Reportes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reporte reporte = db.Reportes.Find(id);
            if (reporte == null)
            {
                return HttpNotFound();
            }
            return View(reporte);
        }

        public void Crear(int numReporteUsr, string userName, string situacion, double longitud, double latitud, string sector)
        {
            /* Se verifica si hay un reporte del usuario con 
             * el numReporteUsr que se quiere agegar al reporte*/
            var reporte = (from n in db.Reportes
                          where n.userName == userName &&
                                n.numReporteUsr == numReporteUsr
                          select n);
           /**
            * Se verifica si el colaborador que hace el reporte existe
            */
            //var colab = (from n in db.Colaboradores
            //             where n.idColaborador == idUsuario
            //             select n);
            /**
             *  Si no se encontro el reporte y el colaborador existe, crea uno nuevo
             */
            //if (reporte.Count() == 0 && colab.Count()>0 )
            //{
                Reporte report = new Reporte(numReporteUsr, userName, situacion, longitud, latitud,sector);
                db.Reportes.Add(report);
                db.SaveChanges();
            //}
            

            
        }
        // GET: Reportes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reportes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "numReporte,numReporteUsr,userName,situacion,longitud,latitud")] Reporte reporte)
        {
            if (ModelState.IsValid)
            {
                db.Reportes.Add(reporte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reporte);
        }

        // GET: Reportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reporte reporte = db.Reportes.Find(id);
            if (reporte == null)
            {
                return HttpNotFound();
            }
            return View(reporte);
        }

        // POST: Reportes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "numReporte,numReporteUsr,userName,Descripcion,situacion,ubicacion,longitud,latitud,FotoUrl,Comentarios,estatus,fechaCreacion,fechaCorreccion")] Reporte reporte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reporte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reporte);
        }

        // GET: Reportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reporte reporte = db.Reportes.Find(id);
            if (reporte == null)
            {
                return HttpNotFound();
            }
            return View(reporte);
        }

        // POST: Reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reporte reporte = db.Reportes.Find(id);
            db.Reportes.Remove(reporte);
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
