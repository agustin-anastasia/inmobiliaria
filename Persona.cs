using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria
{
    /// <summary>
    /// Clase abstracta persona
    /// </summary>
    public abstract class Persona
    {
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Cedula { get; set; }
        public Int32 Telefono { get; set; }
        public String Correo { get; set; }
        public Persona(string n, string a, string c, int t, string correo)
        {
            Nombre = n;
            Apellido = a;
            Cedula = c;
            Telefono = t;
            Correo = correo;
        }
    }
}
