using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Inmobiliaria
{
    public class ManejadorImagenes
    {
        /// <summary>
        /// Manejador imagenes
        /// </summary>
        private static ManejadorImagenes instancia;
        /// <summary>
        /// Singleton del manejador de Imagenes
        /// </summary>
        /// <returns></returns>
        public static ManejadorImagenes GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new ManejadorImagenes();
            }
            return instancia;
        }

        /// <summary>
        /// Leer imagenes de la carpeta
        /// </summary>
        /// <param name="carpeta"></param>
        /// <returns></returns>
        public List<String> LeerImagenes(String carpeta)
        {
            String rutaCompleta = System.IO.Directory.GetCurrentDirectory() + "\\img\\" + carpeta + "\\";
            return Directory.GetFiles(rutaCompleta, "*.jpg", SearchOption.AllDirectories).ToList();
        }
        /// <summary>
        /// Agrega las imagenes a la carpeta del inmueble
        /// </summary>
        /// <param name="i"></param>
        public static void AgregarImagen(Inmueble i)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\img\\" + i.Id;
            ofd.ShowDialog();
        }

    }
}