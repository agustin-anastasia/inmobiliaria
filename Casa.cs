using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Inmobiliaria
{
    public class Casa : Inmueble
    {
        public Int32 Jardin { get; set; }
        public Int32 Parrillero { get; set; }
        public Casa(String id, Int32 precio, Int32 cDormitorios, Int32 cBaños, Int32 añoCons, Int32 metrosEdificados,
            String ciudad, String barrio, String estado, Int32 cGarages, String ubicacion, 
            String comentarios, Int32 jardin, Int32 parrillero) : base(id, precio, cDormitorios, cBaños, añoCons, metrosEdificados,
            ciudad, barrio, estado, cGarages, ubicacion, comentarios)
        {
            Jardin = jardin;
            Parrillero = parrillero;
        }
    }
}
