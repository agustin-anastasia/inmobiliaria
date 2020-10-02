using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria
{
    public class Propietario : Persona
    {
        public String Id { get; set; }
        public String Direccion { get; set; }
        public Propietario(string n, string a, string c, int t, string correo, String id, string dir) : base(n, a, c, t, correo)
        {
            Id = id;
            Direccion = dir;
        }
    }
}
