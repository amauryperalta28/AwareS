using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AwareswebApp.Models
{
    public class Consumo
    {

        [Key]
        public int idConsumo { get; set; }
        public int idColaborador { get; set; }
        public string tipoConsumo { get; set; } //{diario, semanal, hora}
        public string lectura { get; set; }
        
        public DateTime fechaCreacion { get; set; }

        public Consumo(int id_Colaborador, string lectura_Consumo)
        {
            idColaborador = id_Colaborador;
            lectura = lectura_Consumo;
            fechaCreacion = DateTime.Now;
            tipoConsumo = "Mensual";

        }
        public Consumo()
        {
            fechaCreacion = DateTime.Now;
            tipoConsumo = "Mensual";

        }
        

    }
}