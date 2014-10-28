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
        public ActionResult Index(string tipoSituacion, string sector)
        {
            // Creo una lista1, lista2 para guardar cadenas
            var tipoSitLst = new List<string>();
            var sectorLst = new List<string>();

            //Hago query en la tabla reportes en donde obtengo los tipos de situaciones reportados y los guardo en una variable
            var tipoSitQry = from a in db.Reportes
                             select a.situacion;
            //Hago query en la tabla reportes en donde obtengo los sectores en los que se han reportado situaciones y los guardo en una variable
            var sectorQry = from a in db.Reportes
                            select a.ubicacion;
            // Relleno la lista con los tipos de situaciones no repetidas de los que se han hecho reportes
            tipoSitLst.AddRange(tipoSitQry.Distinct());
            ViewBag.tipoSituacion = new SelectList(tipoSitLst);

            // Relleno la lista con los sectores no repetidos en los que se han hecho reportes
            sectorLst.AddRange(sectorQry.Distinct());
            ViewBag.sector = new SelectList(sectorLst);
            // Guardo en variable los reportes del tipo y sector indicados, si se indico. Si no se indico retorno todo los reportes no resueltos

            var listaReportes = from a in db.Reportes
                                where a.estatus == "1"
                                select a;    

            if (!String.IsNullOrEmpty(tipoSituacion) && !String.IsNullOrEmpty(sector))
            {
                listaReportes = from a in db.Reportes
                                where a.situacion == tipoSituacion &&
                                      a.ubicacion == sector        &&
                                      a.estatus == "1"
                                select a;

            }
            else if (!String.IsNullOrEmpty(tipoSituacion))
            {
                 listaReportes = from a in db.Reportes
                                where a.situacion == tipoSituacion &&
                                      a.estatus == "1"
                                select a;

            }
            else if (!String.IsNullOrEmpty(sector))
            {
                listaReportes = from  a in db.Reportes
                                where a.ubicacion == sector &&
                                      a.estatus == "1"
                                     select a;

            }
            
            ViewBag.Latitud = 18.523471;
            ViewBag.Longitud = -69.8746229;
            ViewBag.coordenadas = listaReportes;

                         
            return View(listaReportes);
        }

        public ActionResult ZonasMasAfectadas(string sector)
        {
            // Creo una lista1, lista2 para guardar cadenas
            
            var sectorLst = new List<string>();

            //Hago query en la tabla reportes en donde obtengo los sectores en los que se han reportado situaciones y los guardo en una variable
            var sectorQry = from a in db.Reportes
                            select a.ubicacion;
         
            // Relleno la lista con los sectores no repetidos en los que se han hecho reportes
            sectorLst.AddRange(sectorQry.Distinct());
            ViewBag.sector = new SelectList(sectorLst);
            // Guardo en variable los reportes del tipo y sector indicados, si se indico. Si no se indico retorno todo los reportes no resueltos

            //var listaReportes = from a in db.Reportes
            //                    where a.estatus == "1"
            //                    select a;

            List<Reporte> listaReportes = (from a in db.Reportes
                                           where a.estatus == "1"
                                           select a).ToList();


       

            // if (!String.IsNullOrEmpty(sector))
            //{
            //    listaReportes = (from  a in db.Reportes
            //                    where a.ubicacion == sector &&
            //                          a.estatus == "1"
            //                         select a).ToList();

            //}
            
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
        
        /**
         * Envia una lista de reportes no resueltos en formato Json
         * 
         * @return Lista de reportes no resueltos
         */
        public JsonResult getReports()
        {
            // Obtengo los reportes que no han sido resueltos
            var n = from a in db.Reportes
                    where a.estatus == "1"
                    select a;

            // Envio lista de reportes

            return Json(n, JsonRequestBehavior.AllowGet);

        }
        /**
         * Envia una lista de reportes de un usuario especificado
         * 
         * @return Lista de reportes no resueltos
         */
        [Route("Reportes/getReportsUser/{username}/{contrasena}")]
        public JsonResult getReportsUser(string userName, string contrasena)
        {
            
            //Verifico si el usuario y contrasena son validos
            int usuario = (from a in db.Colaboradores
                          where a.Password == contrasena &&
                                a.nombreUsuario == userName
                          select a).ToList().Count;
            //Verifico si el usuario y contrasena son validos
            if (usuario == 1)
            {
                var rep = from a in db.Reportes
                               where a.userName == userName
                               select a;
                return Json(rep, JsonRequestBehavior.AllowGet);
            }


            return Json(0, JsonRequestBehavior.AllowGet);
            
        }
        
        public JsonResult Crear(int numReporteUsr, string userName, string situacion, double longitud, double latitud, string sector)
        {
            //Tomo el username y verifico si el colaborador existe

            var colabExiste = (from a in db.Colaboradores
                              where a.nombreUsuario == userName
                              select a).ToList().Count;

            if (colabExiste == 1)
            {
                Reporte report = new Reporte(numReporteUsr, userName, situacion, longitud, latitud, sector);
                db.Reportes.Add(report);
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            
           
                
            
            

            
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
