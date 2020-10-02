using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria
{
    public class Visita
    {
        public List<Visita> listaVisitas;
        public DateTime DiaYHora { get; set; }
        public Inmueble Propiedad { get; set; }
        public Cliente Visitante { get; set; }
        public String Comentarios { get; set; }

        public Visita(DateTime d, Inmueble i, Cliente v, String c)
        {
            DiaYHora = d;
            Propiedad = i;
            Visitante = v;
            Comentarios = c;
            listaVisitas = new List<Visita>();
        }
    }
}
