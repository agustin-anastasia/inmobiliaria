using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Inmobiliaria
{
    public class Apartamento : Inmueble
    {
        public Int32 Piso { get; set; }
        public Int32 Plantas { get; set; }
        public Int32 GastosComunes { get; set; }
        public Apartamento(String id, Int32 precio, Int32 cDormitorios, Int32 cBaños, Int32 añoCons, Int32 metrosEdificados,
            String ciudad, String barrio, String estado, Int32 gc, Int32 cGarages, String ubicacion,
            String comentarios, Int32 piso, Int32 plantas) : base(id, precio, cDormitorios, cBaños, añoCons, metrosEdificados,
            ciudad, barrio, estado, cGarages, ubicacion, comentarios)
        {
            Piso = piso;
            Plantas = plantas;
            GastosComunes = gc;
        }
    }
}
