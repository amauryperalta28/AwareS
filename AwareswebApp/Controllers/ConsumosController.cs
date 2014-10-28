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
        public void Receive(string userNameColaborador, string lecturaConsumo)
        {
            Consumo consumo = new Consumo(userNameColaborador, lecturaConsumo);

            // Se recibe las lecturas
            String l = lecturaConsumo;

            // Se separan las lecturas por usuario
            String[] lecturas = l.Split('-');

            
            //Se recorren las lecturas
            for (int i = 0; i < lecturas.Length; i++)
            {
                // Se separan los datos usuario y lectura
                String[] datos = lecturas[i].Split(',');

                Consumo c = new Consumo(datos[1], datos[0]);
                db.Consumos.Add(c);
                
            }
            
            db.SaveChanges();
            
        }

        public ActionResult Menu()
        {
            return View();
        }

        /**
         * Envia una lista de reportes de un usuario especificado
         * 
         * @return Lista de reportes no resueltos
         */
        [Route("Consumos/getConsumesUser/{username}/{contrasena}")]
        public JsonResult getConsumesUser(string userName, string contrasena)
        {

            //Verifico si el usuario y contrasena son validos
            int usuario = (from a in db.Colaboradores
                           where a.Password == contrasena &&
                                 a.nombreUsuario == userName
                           select a).ToList().Count;
            //Verifico si el usuario y contrasena son validos
            if (usuario == 1)
            {
                var rep = from a in db.Colaboradores
                          where a.nombreUsuario == userName
                          select a;
                return Json(rep, JsonRequestBehavior.AllowGet);
            }


            return Json(0, JsonRequestBehavior.AllowGet);

        }
        // GET: Consumos
        public ActionResult Index(string colabFilter)
        {
            // Creo una lista para guardar cadenas
            var ColabLst = new List<string>();
            //Hago query en la tabla consumos en donde obtengo los usuarios que han realizado consumos y los guardo en una variable
            var ColabQry = from a in db.Consumos
                           select a.UsernameColaborador;
           
            // Relleno la lista con los usuarios no repetidos que han hecho consumos
            ColabLst.AddRange(ColabQry.Distinct());
            ViewBag.colabFilter = new SelectList(ColabLst);
            // Guardo en variable los consumos hechos por el usuario indicado, si se indico, si no se indico no desplego nada.
            var colab = from a in db.Consumos
                        select a;

            if (!string.IsNullOrEmpty(colabFilter))
            {
                colab = colab.Where(x => x.UsernameColaborador == colabFilter);
            }

            return View(colab);
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
        public ActionResult Create([Bind(Include = "idConsumo,userNameColaborador,tipoConsumo,lectura,fechaCreacion")] Consumo consumo)
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
        public ActionResult Edit([Bind(Include = "idConsumo,userNameColaborador,tipoConsumo,lectura,fechaCreacion")] Consumo consumo)
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
