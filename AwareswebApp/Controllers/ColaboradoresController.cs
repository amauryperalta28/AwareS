﻿using System;
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
    public class ColaboradoresController : Controller
    {
        private DbModels db = new DbModels();

        public ActionResult Menu()
        {
            return View();
        }
        // GET: Colaboradores
        public ActionResult Index()
        {
            return View(db.Colaboradores.ToList());
        }

        // GET: Colaboradores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaborador colaborador = db.Colaboradores.Find(id);
            if (colaborador == null)
            {
                return HttpNotFound();
            }
            return View(colaborador);
        }
        // GET: Colaboradores/Create
        public string Crear(string usuario, string email, string password)
        {
            Colaborador colab = new Colaborador(usuario, email, password);
            db.Colaboradores.Add(colab);
            db.SaveChanges();

            return "1";
        }

        // GET: Colaboradores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colaboradores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombreUsuario,Email,Password")] Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                db.Colaboradores.Add(colaborador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(colaborador);
        }

        // GET: Colaboradores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaborador colaborador = db.Colaboradores.Find(id);
            if (colaborador == null)
            {
                return HttpNotFound();
            }
            return View(colaborador);
        }

        // POST: Colaboradores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idColaborador,nombreUsuario,Email,Password,sector,localidad,tipoUsuario,fechaCreacion")] Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colaborador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colaborador);
        }

        // GET: Colaboradores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaborador colaborador = db.Colaboradores.Find(id);
            if (colaborador == null)
            {
                return HttpNotFound();
            }
            return View(colaborador);
        }

        // POST: Colaboradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Colaborador colaborador = db.Colaboradores.Find(id);
            db.Colaboradores.Remove(colaborador);
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
