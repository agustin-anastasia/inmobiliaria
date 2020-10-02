using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria
{
    public class Cliente : Persona
    {
        public List<Inmueble> listaPopiedadesVisitadas;

        public Cliente(string n, string a, string c, int t, string correo) : base(n, a, c, t, correo)
        {
            listaPopiedadesVisitadas = new List<Inmueble>();

        }
    }
}
