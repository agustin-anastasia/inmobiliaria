using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Inmobiliaria
{
    /// <summary>
    /// Clase abstracta Inmueble
    /// </summary>
    public abstract class Inmueble
    {
        
        public String Id { get; set; }
        public Int32 Precio { get; set; }
        public Int32 CantidadDormitorios { get; set; }
        public Int32 CantidadBaños { get; set; }
        public Int32 AñoConstruccion { get; set; }
        public Int32 MetrosEdificados { get; set; }
        public String Ciudad { get; set; }
        public String Barrio { get; set; }
        public String Estado { get; set; }
        public Int32 CantidadGarages { get; set; }
        public String Ubicacion { get; set; }
        public ArrayList ConjuntoFotos { get; set; }
        public String Comentarios { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id inmueble</param>
        /// <param name="precio">precio inmueble</param>
        /// <param name="cDormitorios"> dormitorios del inmueble</param>
        /// <param name="cBaños"> baños del inmueble</param>
        /// <param name="añoCons"> año construccion del inmueble</param>
        /// <param name="metrosEdificados">metros edificados del inmueble</param>
        /// <param name="ciudad">ciudad del inmueble</param>
        /// <param name="barrio"> barrio del inmueble</param>
        /// <param name="estado">estado del inmueble</param>
        /// <param name="cGarages">garages del inmueble</param>
        /// <param name="ubicacion">ubiacion del inmueble</param>
        /// <param name="comentarios">comentarios del inmueble</param>
        public Inmueble(String id, Int32 precio, Int32 cDormitorios, Int32 cBaños, Int32 añoCons, Int32 metrosEdificados,
            String ciudad, String barrio, String estado, Int32 cGarages, String ubicacion, String comentarios)
        {
            Id = id;
            Precio = precio;
            CantidadDormitorios = cDormitorios;
            CantidadBaños = cBaños;
            AñoConstruccion = añoCons;
            MetrosEdificados = metrosEdificados;
            Ciudad = ciudad;
            Barrio = barrio;
            Estado = estado;
            CantidadGarages = cGarages;
            Ubicacion = ubicacion;
            Comentarios = comentarios;
        }
    }
}