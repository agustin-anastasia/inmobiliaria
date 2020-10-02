using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Inmobiliaria
{
    public partial class FormPrincipal : Form
    {
        /// <summary>
        /// Form principal de la aplicacion
        /// </summary>
        ManejadorInmuebles m = ManejadorInmuebles.GetInstancia();
        List<Inmueble> lista = new List<Inmueble>();
        public FormPrincipal()
        {
            InitializeComponent();
            dataGridView1.DataSource = null;
            comboBoxCiudades.DataSource = null;
        }
        

        /// <summary>
        /// Botón que filtra del archivo según los radioButton: Apartamento/Casa, Alquiler/Compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRadiobtnAceptar_Click(object sender, EventArgs e)
        {
            List<Inmueble> ListaInmCiudad = new List<Inmueble>();
            dataGridView1.DataSource = null;
            lista.Clear();
            // La primera Flag indica: "A" Apartamento, "C" Casa
            // La segunda Flag indica: "A" Alquiler, "V" Venta
            if (radioBtnApto.Checked && radioBtnAlquilar.Checked)
            {
                lista = m.Filtro_AlquilerVenta_AptoCasa("A","A");
                comboBoxCiudades.DataSource = null;
            }
            else if (radioBtnCasa.Checked && radioBtnAlquilar.Checked)
            {
                lista = m.Filtro_AlquilerVenta_AptoCasa("C","A");
                comboBoxCiudades.DataSource = null;
            }
            else if (radioBtnApto.Checked && radioBtnComprar.Checked)
            {
                lista = m.Filtro_AlquilerVenta_AptoCasa("A","V");
                comboBoxCiudades.DataSource = null;
            }
            else if (radioBtnCasa.Checked && radioBtnComprar.Checked)
            {
                lista = m.Filtro_AlquilerVenta_AptoCasa("C","V");
                comboBoxCiudades.DataSource = null;
            }
            if (!radioBtnCasa.Checked && !radioBtnApto.Checked || !radioBtnAlquilar.Checked && !radioBtnComprar.Checked)
            {
                MessageBox.Show("Falta data");
                comboBoxCiudades.DataSource = null;
            }
            if (txtPrecioMin.Text != "" || txtPrecioMax.Text != "")
            {
                Int32 min = 0;
                Int32 max = 0;
                // Como entra a este if, sólo si el usuario ingresó minimo Ó máximo. 
                // Se valida que, si no puso mínimo, el mínimo sea 0,
                // Si no hay máximo, el máximo es un número muy alto para que entren todos los inmuebles.
                // De esta forma se valida por ej, que si sólo se buscan casas hasta 300.000 dólares (mínimo no completado), entonces, el mínimo es cero.
                if (txtPrecioMin.Text.Length == 0 && txtPrecioMax.Text.Length > 0)
                {
                    min = 0;
                    max = Convert.ToInt32(txtPrecioMax.Text);
                }
                if (txtPrecioMin.Text.Length > 0 && txtPrecioMax.Text.Length == 0)
                {
                    min += Convert.ToInt32(txtPrecioMin.Text);
                    max = 150000000;
                }
                if (txtPrecioMin.Text.Length > 0 && txtPrecioMax.Text.Length > 0)
                {
                    min += Convert.ToInt32(txtPrecioMin.Text);
                    max = Convert.ToInt32(txtPrecioMax.Text);
                }
                // La función, BuscarPorPrecio, requiere que se le pase una lista para que la filtre según
                // Las condiciones de min y máx. A su vez de dichas condiciones, pueden utilizarse ambas, 
                // ninguna o sólo una.
                lista = m.BuscarPorPrecio(min, max, lista);
            }
            // Se carga el comboBox, mostrando sólamente las ciudades a las que pertenecen 
            // los apartamentos de la lista filtrada. 
            comboBoxCiudades.DataSource = m.Ciudades();
            dataGridView1.DataSource = lista;
        }

        /// <summary>
        /// Evento de hacer click en una celda del datagrid. Abre una ventana con la ficha del inmueble clickeado. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                // Se pasa por parámetro la linea y el inmueble de la misma.
                Propiedad p = new Propiedad(lista[e.RowIndex]);
                p.Show();
                p.Focus();
            }
        }
        /// <summary>
        /// Agregar apartamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarApartamento_Click_1(object sender, EventArgs e)
        {
            groupBoxApto.Visible = true;
            groupBoxCasa.Visible = false;
            groupBoxPropietario.Visible = true;
        }

        /// <summary>
        /// Agregar casa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarCasa_Click(object sender, EventArgs e)
        {
            groupBoxApto.Visible = false;
            groupBoxCasa.Visible = true;
            groupBoxPropietario.Visible = true;
        }

        /// <summary>
        /// Evento de boton confirmar para agregar un apartamento al sistema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmarApto_Click(object sender, EventArgs e)
        {
            if (txtPrecioApto.Text == "" || comboBoxDormitoriosApto.SelectedItem.Equals("") || comboBoxBañosApto.SelectedItem.Equals("")
              || txtAñoApto.Text == "" || txtMetrosApto.Text == "" || txtCiudadApto.Text == "" || txtbarrioApto.Text == ""
              || comboBoxEstadoApto.SelectedItem.Equals("") || txtGastosApto.Text == "" || comboBoxAptoGarages.SelectedItem.Equals("")
              || txtUbicacionApto.Text == "" || txtPisoApto.Text == "" || txtPlanta.Text == "" || nomProp.Text == ""
              || apeProp.Text == "" || ciProp.Text == "" || telProp.Text == "" || mailProp.Text == "" || dirProp.Text == "" || txtComentario.Text == "")
            {
                MessageBox.Show("Faltan llenar campos");
            }
            else
            {
                // Se confecciona el Id, según las operaciones. id = "A-X-000X"
                String id = "A-";
                if (comboBoxVentaAlquilerApto.Text == "Alquiler")
                {
                    id += "A-";
                }
                if (comboBoxVentaAlquilerApto.Text == "Venta")
                {
                    id += "V-";
                }
                if (comboBoxVentaAlquilerApto.Text == "")
                {
                    MessageBox.Show("Falta seleccionar la operación. (Venta o alquiler)");
                }
                // Se crea el apartamento para verificar la validez y existencia de cada parámetro.
                Apartamento inm = m.CrearApartamento(id, Convert.ToInt32(txtPrecioApto.Text), Convert.ToInt32(comboBoxDormitoriosApto.SelectedItem),
                    Convert.ToInt32(comboBoxBañosApto.SelectedItem), Convert.ToInt32(txtAñoApto.Text), Convert.ToInt32(txtMetrosApto.Text),
                    txtCiudadApto.Text, txtbarrioApto.Text, comboBoxEstadoApto.SelectedItem.ToString(), Convert.ToInt32(txtGastosApto.Text),
                    Convert.ToInt32(comboBoxAptoGarages.SelectedItem), txtUbicacionApto.Text, txtComentario.Text, Convert.ToInt32(txtPisoApto.Text),
                    Convert.ToInt32(txtPlanta.Text));
                
                // Se llama al creador de Id, para modificarlo en el inmueble ya existente.
                Apartamento conId = ManejadorInmuebles.CrearIdA(inm);
                // Cuando se crea un Inmueble, se crea la carpeta donde se guardan sus fotos, con el mismo nombre de su ID.
                
                // Luego la línea que se escribirá en el archivo.
                String linea = conId.Id + "|" + inm.Precio + "|" + inm.CantidadDormitorios + "|" + inm.CantidadBaños + "|" + 
                    inm.AñoConstruccion + "|" + inm.MetrosEdificados + "|" + inm.Ciudad + "|" + inm.Barrio + "|" + 
                    inm.Estado + "|" + inm.GastosComunes + "|" + inm.CantidadGarages + "|" + inm.Ubicacion + "|" + 
                    inm.Comentarios + "|" + inm.Piso + "|" + inm.Plantas;
                m.Guardar(System.IO.Directory.GetCurrentDirectory() + "\\DB.txt", linea);
                // Se agrega el propietario
                Propietario p = new Propietario(nomProp.Text, apeProp.Text, ciProp.Text,
                                Convert.ToInt32(telProp.Text), mailProp.Text, conId.Id, dirProp.Text);
                String lineaP = nomProp.Text + "|" + apeProp.Text + "|" + ciProp.Text + "|" + Convert.ToInt32(telProp.Text) + "|" +
                                 mailProp.Text + "|" + dirProp.Text + "|" + conId.Id;
                MessageBox.Show("Inmueble agregado. \nSeleccionar las imágenes a agregar del inmueble.");
                m.Guardar(System.IO.Directory.GetCurrentDirectory() + "\\Propietarios.txt", lineaP);
                ManejadorImagenes.AgregarImagen(conId);
                // Se llama a la función estática de la clase Limpar, para limpiar los campos.
                Limpiar.BorrarCampos(this,groupBoxApto);
                Limpiar.BorrarCampos(this,groupBoxPropietario);
            }
        }
        /// <summary>
        /// Evento del boton confirmar casa para agregar una casa al sistema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmarCasa_Click(object sender, EventArgs e)
        {
            if (txtPrecioCasa.Text == "" || comboDormitoriosCasa.SelectedItem.Equals("") || comboBoxBañosCasa.SelectedItem.Equals("")
             || txtAñoConstruCasa.Text == "" || txtMetrosCasa.Text == "" || txtCiudadCasa.Text == "" || txtBarrioCasa.Text == ""
             || comboBoxEstadoCasa.SelectedItem.Equals("") || comboBoxGaragesCasa.SelectedItem.Equals("") || txtUbicacionCasa.Text == ""
             || txtJardinCasa.Text == "" || txtParrilleroCasa.Text == "" || nomProp.Text == "" || apeProp.Text == "" || ciProp.Text == ""
             || telProp.Text == "" || mailProp.Text == "" || dirProp.Text == "" || txtComentario.Text == "")
            {
                MessageBox.Show("Faltan llenar campos.");
            }
            else
            {
                // Se confecciona el Id, según las operaciones. id = "C-X-000X"
                String id = "C-";
                if (comboBoxVentaAlquilerCasa.Text == "Alquiler")
                {
                    id += "A-";
                }
                if (comboBoxVentaAlquilerCasa.Text == "Venta")
                {
                    id += "V-";
                }
                if (comboBoxVentaAlquilerCasa.Text == "")
                {
                    MessageBox.Show("Falta seleccionar la operación. (Venta o alquiler)");
                }
                // Se crea la casa para verificar la validez y existencia de cada parámetro.
                Casa inm = m.CrearCasa(id, Convert.ToInt32(txtPrecioCasa.Text), Convert.ToInt32(comboDormitoriosCasa.SelectedItem),
                    Convert.ToInt32(comboBoxBañosCasa.SelectedItem), Convert.ToInt32(txtAñoConstruCasa.Text),
                    Convert.ToInt32(txtMetrosCasa.Text), txtCiudadCasa.Text, txtBarrioCasa.Text,
                    comboBoxEstadoCasa.SelectedItem.ToString(), Convert.ToInt32(comboBoxGaragesCasa.SelectedItem),
                    txtUbicacionCasa.Text, txtComentario.Text, Convert.ToInt32(txtJardinCasa.Text),
                    Convert.ToInt32(txtParrilleroCasa.Text));

                // Se llama al creador de Id, para modificarlo en el inmueble ya existente.
                Casa conId = ManejadorInmuebles.CrearIdC(inm);
                // Cuando se crea un Inmueble, se crea la carpeta donde se guardan sus fotos, con el mismo nombre de su ID.

                // Luego la línea que se escribirá en el archivo.
                String linea = conId.Id + "|" + inm.Precio + "|" + inm.CantidadDormitorios + "|" + inm.CantidadBaños + "|" +
                    inm.AñoConstruccion + "|" + inm.MetrosEdificados + "|" + inm.Ciudad + "|" + inm.Barrio + "|" +
                    inm.Estado + "|" + inm.CantidadGarages + "|" + inm.Ubicacion + "|" + inm.Comentarios + "|" +
                    inm.Jardin + "|" + inm.Parrillero;
                m.Guardar(System.IO.Directory.GetCurrentDirectory() + "\\DB.txt", linea);
                // Se agrega el propietario
                Propietario p = new Propietario(nomProp.Text, apeProp.Text, ciProp.Text,
                                Convert.ToInt32(telProp.Text), mailProp.Text, dirProp.Text, conId.Id);
                String lineaP = nomProp.Text + "|" + apeProp.Text + "|" + ciProp.Text + "|" + Convert.ToInt32(telProp.Text) + "|" +
                    mailProp.Text + "|" + dirProp.Text + "|" + conId.Id;
                MessageBox.Show("Inmueble agregado. \nSeleccionar las imágenes a agregar del inmueble.");
                m.Guardar(System.IO.Directory.GetCurrentDirectory() + "\\Propietarios.txt", lineaP);
                ManejadorImagenes.AgregarImagen(conId);
                // Se llama a la función estática de la clase Limpar, para limpiar los campos.
                Limpiar.BorrarCampos(this, groupBoxCasa);
                Limpiar.BorrarCampos(this, groupBoxPropietario);
            }
        }
        /// <summary>
        /// Validar entrada del teclado en los campos para control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }
        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }
        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }
        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }
        /// <summary>
        /// Busca y filtra los inmuebles por ciudad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscarXCiudad_Click(object sender, EventArgs e)
        {
            comboBoxCiudades.DataSource = null;
            comboBoxCiudades.DataSource = m.Ciudades();

            List<Inmueble> lista = new List<Inmueble>();
            lista.Clear();

            if (comboBoxCiudades.SelectedItem != null)
            {
                lista = m.BuscarPorCiudad(comboBoxCiudades.SelectedItem.ToString(), m.ListaInmuebles);
                dataGridView1.DataSource = lista;
            }
        }

        /// <summary>
        /// Borra los campos de combobox y textbox de la pestaña Inmobiliaria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            radioBtnAlquilar.Checked = false;
            radioBtnApto.Checked = false;
            radioBtnCasa.Checked = false;
            radioBtnComprar.Checked = false;
            comboBoxCiudades.SelectedIndex = -1;
            txtPrecioMin.Text = "";
            txtPrecioMax.Text = "";
        }

        private void txtPrecioMin_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtPrecioMax_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtPrecioApto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtbarrioApto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtEstadoApto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtGastosApto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtAñoApto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void metrosApto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void ciudadApto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtPiso_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtCiudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtBarrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtJardin_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtParrillero_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtAñoConstru_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtGastos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtMetros_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtNombre_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtApellido_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtCI_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void txtTel_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void nomProp_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void ciProp_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void telProp_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void apeProp_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }
        // Boton autocompletar para facilitar el testeo de las funciones del programa.
        private void button1_Click_1(object sender, EventArgs e)
        {
            // Formulario Casa
            txtPrecioCasa.Text = "123";
            comboDormitoriosCasa.SelectedItem = "1";
            comboBoxBañosCasa.SelectedItem = "1";
            txtAñoConstruCasa.Text = "1930";
            txtMetrosCasa.Text = "80";
            txtCiudadCasa.Text = "Montevideo";
            txtBarrioCasa.Text = "Buceo";
            comboBoxEstadoCasa.SelectedItem = "Reciclado";
            comboBoxGaragesCasa.SelectedItem = "1";
            txtUbicacionCasa.Text = "Calle 1 n° 1002";
            txtJardinCasa.Text = "15";
            txtParrilleroCasa.Text = "30";
            txtComentario.Text = "El comentario";
            nomProp.Text = "Carlos";
            apeProp.Text = "Carlin";
            ciProp.Text = "12332126";
            telProp.Text = "092323132";
            mailProp.Text = "Carlin58@gmail.com";
            dirProp.Text = "Calle 123, casa 3321";
            // Formulario Apartamento
            txtPrecioApto.Text = "10000";
            txtbarrioApto.Text = "Las Acacias";
            txtPlanta.Text = "3";
            comboBoxDormitoriosApto.SelectedItem = "1";
            comboBoxBañosApto.SelectedItem = "1";
            comboBoxEstadoApto.SelectedItem = "Reciclado";
            comboBoxPorteria.SelectedItem = "No";
            txtGastosApto.Text = "1700";
            txtAñoApto.Text = "1930";
            comboBoxAptoGarages.SelectedItem = "1";
            txtMetrosApto.Text = "35";
            txtUbicacionApto.Text = "Calle n, 5003, apto 303";
            txtPisoApto.Text = "3";
            txtCiudadApto.Text = "Montevideo";
        }

        /// <summary>
        /// Boton que confirma cuando se agrega una visita
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmarVisita_Click(object sender, EventArgs e)
        {

            if (nomCliente.Text == "" || apeCliente.Text == "" || ciCliente.Text == "" || telCliente.Text == ""
                || mailCliente.Text == "" || txtComentariosVisita.Text == "" || comboBoxHoraAgenda.SelectedItem.Equals("")) 
            {
                MessageBox.Show("Faltan llenar datos del cliente.");
            }
            else
            {
                Cliente c = new Cliente(nomCliente.Text, apeCliente.Text, ciCliente.Text, Convert.ToInt32(telCliente.Text),
                    mailCliente.Text);
                String FechaHora = monthCalendarVisita.SelectionRange.Start.ToString("dd-MM-yyyy") + "  " + comboBoxHoraAgenda.SelectedItem.ToString();
                String linea = nomCliente.Text + "|" + apeCliente.Text + "|" + ciCliente.Text + "|" + telCliente.Text + "|" +
                    mailCliente.Text + "|" + txtComentariosVisita.Text + FechaHora;

                m.Guardar(System.IO.Directory.GetCurrentDirectory() + "\\Visitas.txt", linea);

                MessageBox.Show("Su visita quedo agendada para el dia " + monthCalendarVisita.SelectionRange.Start.ToString("dd-MM-yyyy")
                    + "   a las  " + comboBoxHoraAgenda.SelectedItem.ToString() + " hs");

                comboBoxHoraAgenda.SelectedIndex = -1;
                nomCliente.Clear();
                apeCliente.Clear();
                ciCliente.Clear();
                mailCliente.Clear();
                telCliente.Clear();
            }
        }

        private void nomCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void apeCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void ciCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void telCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        /// <summary>
        /// DataGrid muestra todos los inmuebles 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMostrarInmueblesVisita_Click(object sender, EventArgs e)
        {
            dataGridInmueblesVisita.Visible = true;
            dataGridInmueblesVisita.DataSource = null;
            dataGridInmueblesVisita.DataSource = m.ListaInmuebles;
            
        }
        
    }
}