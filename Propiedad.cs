using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inmobiliaria
{
    public partial class Propiedad : Form
    {
        /// <summary>
        /// Form propiedad, ventana que aparece para ver informacion del inmueble y sus fotos
        /// </summary>
        ManejadorImagenes mi = new ManejadorImagenes();
        Inmueble iInterno = null;
        public Propiedad(Inmueble i)
        {
            iInterno = i;
            // A través del inmueble que se pasa por parámetro, se cargan los label con sus propiedades.
            InitializeComponent();
            lblID.Text = i.Id;
            lblUbicacion.Text = i.Ubicacion;
            lblPrecio.Text = i.Precio.ToString();
            lblCiudad.Text = i.Ciudad;
            lblBarrio.Text = i.Barrio;
            lblDormitorios.Text = i.CantidadDormitorios.ToString();
            lblGarages.Text = i.CantidadGarages.ToString();
            lblBaños.Text = i.CantidadBaños.ToString();
            lblMetrosEdificados.Text = i.MetrosEdificados.ToString();
            txtBoxComentarios.Text = i.Comentarios.ToString();

            // Se le asigna el nombre al botón, según la operación del inmueble
            if (i.Id.Split('-')[1] == "V")
            {
                btnComprarAlquilar.Text = "Comprar";
            }
            if (i.Id.Split('-')[1] == "A")
            {
                btnComprarAlquilar.Text = "Alquilar";
            }

            // Se cargan las imágenes.
            foreach (String nombre in mi.LeerImagenes(i.Id))
            {
                Image imagen = new Bitmap(nombre);
                imageListFoto.Images.Add(imagen);
            }
            lblFoto.ImageList = imageListFoto;
            lblFoto.ImageIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Siguiente Imagen
            lblFoto.ImageIndex += 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Anterior Imagen
            if (lblFoto.ImageIndex > 0)
            {
                lblFoto.ImageIndex -= 1;
            }
        }

        /// <summary>
        /// Borra de la carpeta DB el inmueble y elimina los directory de las imagenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            String path = System.IO.Directory.GetCurrentDirectory() + "\\DB.txt";

            // Se lee el contenido del archivo
            String[] leerTodoElArchivo = File.ReadAllLines(path);

            // Se vacía el mismo
            File.WriteAllText(path, String.Empty);

            // Se llena de vuelta sin la línea deseada.
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (String s in leerTodoElArchivo)
                {
                    // Se recorren todas las líneas del archivo, si el ID de la línea y el del inmueble que 
                    // Ábrió esta ventana, son distintos, entonces graba la línea. Dejándo afuera el coincidente.
                    if(s.Split('|')[0] != iInterno.Id)
                    {
                        writer.WriteLine(s);
                    }
                }
                writer.Close();
            }
            // Se elimina la carpeta de imágenes, con la path del ID del inmueble.
            System.IO.Directory.Delete(System.IO.Directory.GetCurrentDirectory() + "\\img\\" + iInterno.Id, true);
            MessageBox.Show("Operación concretada con éxito! \nEl inmueble será eliminado de la lista.");
            this.Hide();
        }
    }
}