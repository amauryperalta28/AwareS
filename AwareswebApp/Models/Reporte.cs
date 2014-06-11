﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AwareswebApp.Models
{
    public class Reporte
    {
        
        [Key]
        public int numReporte { get; set; }
        public int numReporteUsr { get; set; }
        public int idUsuario { get; set; }

        public string Descripcion { get; set; }
        public string situacion { get; set; }
        public string ubicacion { get; set; }
        public double longitud { get; set; }
        public double latitud { get; set; }
        public string FotoUrl { get; set; }

        public string Comentarios { get; set; }        
        public string estatus { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaCorreccion { get; set; }

        public Reporte(int numReporteUsr, int idUsuario, string situacion, double longitud, double latitud)
        {
            this.numReporteUsr = numReporteUsr;
            this.idUsuario = idUsuario;
            this.situacion = situacion;
            this.longitud = longitud;
            this.latitud = latitud;
            fechaCorreccion = DateTime.Now.Add(new TimeSpan(7));
            fechaCreacion = DateTime.Now;
            estatus = "No resuelto";
            Comentarios = " ";
            ubicacion = "";
            Descripcion = "";
            FotoUrl = "";

        }

        public Reporte ()
	    {
            fechaCorreccion = DateTime.Now.Add(new TimeSpan(7));
            fechaCreacion = DateTime.Now;
            estatus = "No resuelto";
            Comentarios = " ";
            ubicacion = "";
            Descripcion = "";
            FotoUrl = "";
            situacion = "";
	    }
    }
}