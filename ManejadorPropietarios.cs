using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria
{
    public class ManejadorPropietarios
    {
        public List<Persona> listaPopietarios;

        public ManejadorPropietarios()
        {
            listaPopietarios = new List<Persona>();
        }
        public void Agregar(Propietario p)
        {
            listaPopietarios.Add(p);
        }

    }
}
