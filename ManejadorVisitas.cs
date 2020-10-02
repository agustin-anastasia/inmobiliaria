using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria
{
    public class ManejadorVisitas
    {
        public String Comentarios;

        public List<Inmueble> listaPopiedadesVisitadas;

        public ManejadorVisitas(string com)
        {
            listaPopiedadesVisitadas = new List<Inmueble>();
            Comentarios = com;
        }
        public void Agregar(Cliente v, String archivo)
        {
    //        listaPopiedadesVisitadas.Add(v);
        }
    }
}
