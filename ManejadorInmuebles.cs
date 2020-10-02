using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Inmobiliaria
{
    public class ManejadorInmuebles : ManejadorArchivos
    {
        /// <summary>
        /// Manejador Inmuebles
        /// </summary>
        public List<Inmueble> ListaInmuebles { get; set; }
        public ArrayList ListaCiudades { get; set; }
        public ArrayList ListaBarrios { get; set; }
        public List<Inmueble> ListaFiltrada { get; set; }

        //SINGLETON
        /// <summary>
        /// Singleton del manejador
        /// </summary>
        private static ManejadorInmuebles instancia;
        public static ManejadorInmuebles GetInstancia() {
            if (instancia == null) {
                instancia = new ManejadorInmuebles(); }
            return instancia; }
        /// <summary>
        /// Constructor
        /// </summary>
        public ManejadorInmuebles() {
            ListaCiudades = new ArrayList();
            ListaInmuebles = new List<Inmueble>();
        }
        /// <summary>
        /// Metodo leer. Lee el archivo "DB.txt" y obtiene los datos necesarios para crear un inmueble
        /// </summary>
        /// <returns></returns>
        public List<Inmueble> Leer() {
            ListaInmuebles.Clear();
            StreamReader reader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\DB.txt");
            while (reader.Peek() > -1)
            {
                String linea = reader.ReadLine();
                String[] lineaSeparada = linea.Split('|');
                String prefijoAlquilerVenta = lineaSeparada[0].Split('-')[1];
                String prefijoCasaApartamento = lineaSeparada[0].Split('-')[0];
                if (prefijoCasaApartamento.Equals("A"))
                {
                    Apartamento i = CrearApartamento(lineaSeparada[0], Convert.ToInt32(lineaSeparada[1]),
                        Convert.ToInt32(lineaSeparada[2]), Convert.ToInt32(lineaSeparada[3]), Convert.ToInt32(lineaSeparada[4]),
                        Convert.ToInt32(lineaSeparada[5]), lineaSeparada[6], lineaSeparada[7], lineaSeparada[8],
                        Convert.ToInt32(lineaSeparada[9]), Convert.ToInt32(lineaSeparada[10]), lineaSeparada[11], lineaSeparada[12],
                        Convert.ToInt32(lineaSeparada[13]), Convert.ToInt32(lineaSeparada[14]));
                    ListaInmuebles.Add(i);
                }
                if (prefijoCasaApartamento.Equals("C"))
                {
                    Casa i = CrearCasa(lineaSeparada[0], Convert.ToInt32(lineaSeparada[1]), Convert.ToInt32(lineaSeparada[2]),
                        Convert.ToInt32(lineaSeparada[3]), Convert.ToInt32(lineaSeparada[4]), Convert.ToInt32(lineaSeparada[5]), lineaSeparada[6],
                        lineaSeparada[7], lineaSeparada[8], Convert.ToInt32(lineaSeparada[9]), lineaSeparada[10], lineaSeparada[11],
                        Convert.ToInt32(lineaSeparada[12]), Convert.ToInt32(lineaSeparada[13]));
                    ListaInmuebles.Add(i);
                }
            }
            reader.Close();
            return ListaInmuebles; }

        /// <summary>
        /// Retorna una lista filtrada segun las caracteristicas del inmueble
        /// </summary>
        /// <param name="flagAptoCasa"></param>
        /// <param name="flagAlquilerVenta"></param>
        /// <returns></returns>
        public List<Inmueble> Filtro_AlquilerVenta_AptoCasa(String flagAptoCasa, String flagAlquilerVenta)
        { // Las distintas flag, se marcan según el filtro en la aplicación. Ej: radiobutton Alquiler o Venta, Casa o Apartamento.
            List<Inmueble> lista = Leer(); // Lista de todos los inmuebles en el archivo.
            List<Inmueble> listaFiltrada = new List<Inmueble>(); // Nueva lista donde se agregan los inmuebles que cumplen las condiciones.
            foreach (Inmueble i in lista)
            {
                // Dado un id de un inmueble Ej: A-A-0001, se divide la cadena por "-" separando en 3 partes: [A,A,0001]
                // la posición 0, marca si es Apartamento o Casa.
                // la posición 1, si es Alquiler o venta.
                // Si cumple ambas condiciones dadas por parámetro, se agrega a la lista.
                if (i.Id.Split('-')[0].Equals(flagAptoCasa) && i.Id.Split('-')[1].Equals(flagAlquilerVenta))
                {
                    listaFiltrada.Add(i);
                }
            }
            return listaFiltrada;
        }

        /// <summary>
        /// Retorna una lista con las ciudades cargadas luego de seleccionar los radio buttons (casa/apartamento y alquiler/compra).
        /// </summary>
        /// <returns></returns>
        public ArrayList Ciudades() {
            ListaCiudades.Clear();
            foreach (Inmueble i in ListaInmuebles) {
                if (!ListaCiudades.Contains(i.Ciudad)) {
                    ListaCiudades.Add(i.Ciudad); } }
            return ListaCiudades; }
        /// <summary>
        /// Metodo que retorna los inmuebles que tengan la misma ciudad que el usuario busca
        /// </summary>
        /// <param name="ciudad"></param>
        /// <param name="Lista"></param>
        /// <returns></returns>
        public List<Inmueble> BuscarPorCiudad(String ciudad, List<Inmueble> Lista)
        {
            List<Inmueble> listainm = new List<Inmueble>();

            for (int i = 0; i < Lista.Count; i++)
            {
                Inmueble inm = Lista[i];
                if (inm.Ciudad.Equals(ciudad))
                {
                    listainm.Add(inm);
                }
            }
            return listainm;
        }

        /// <summary>
        /// Filtro para buscar por rango de precios el inmueble
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="lista"></param>
        /// <returns></returns>
        public List<Inmueble> BuscarPorPrecio(Int32 min, Int32 max, List<Inmueble> lista)
        {
            List<Inmueble> salida = new List<Inmueble>();
            salida.Clear();
            foreach (Inmueble i in lista) {
                if (i.Precio > min && i.Precio < max) {
                    salida.Add(i); } }
            return salida;
        }

        /// <summary>
        /// Guarda en el archivo indicado
        /// </summary>
        /// <param name="nomArchivo"></param>
        /// <param name="texto"></param>
        
        public override void Guardar(String nomArchivo, String texto)
        {
            StreamWriter writer = new StreamWriter(nomArchivo, true);
            writer.WriteLine(texto);
            writer.Close();
        }
        /// <summary>
        /// Metodo para crear un apartamento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="precio"></param>
        /// <param name="cDormitorios"></param>
        /// <param name="cBaños"></param>
        /// <param name="añoCons"></param>
        /// <param name="metrosEdificados"></param>
        /// <param name="ciudad"></param>
        /// <param name="barrio"></param>
        /// <param name="estado"></param>
        /// <param name="gc"></param>
        /// <param name="cGarages"></param>
        /// <param name="ubicacion"></param>
        /// <param name="comentarios"></param>
        /// <param name="piso"></param>
        /// <param name="plantas"></param>
        /// <returns></returns>
        public Apartamento CrearApartamento(String id, Int32 precio, Int32 cDormitorios, Int32 cBaños, Int32 añoCons, Int32 metrosEdificados,
            String ciudad, String barrio, String estado, Int32 gc, Int32 cGarages, String ubicacion, String comentarios, Int32 piso,
            Int32 plantas)
        {
            Apartamento i = new Apartamento(id, precio, cDormitorios, cBaños, añoCons, metrosEdificados, ciudad, barrio, estado, gc, cGarages, ubicacion,
                comentarios, piso, plantas);
            return i;
        }
        /// <summary>
        /// Metodo para crear casa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="precio"></param>
        /// <param name="cDormitorios"></param>
        /// <param name="cBaños"></param>
        /// <param name="añoCons"></param>
        /// <param name="metrosEdificados"></param>
        /// <param name="ciudad"></param>
        /// <param name="barrio"></param>
        /// <param name="estado"></param>
        /// <param name="cGarages"></param>
        /// <param name="ubicacion"></param>
        /// <param name="comentarios"></param>
        /// <param name="jardin"></param>
        /// <param name="parrillero"></param>
        /// <returns></returns>
        public Casa CrearCasa(String id, Int32 precio, Int32 cDormitorios, Int32 cBaños, Int32 añoCons, Int32 metrosEdificados,
            String ciudad, String barrio, String estado, Int32 cGarages, String ubicacion,
            String comentarios, Int32 jardin, Int32 parrillero)
        {
            Casa i = new Casa(id, precio, cDormitorios, cBaños, añoCons, metrosEdificados, ciudad, barrio, estado, cGarages, ubicacion,
                comentarios, jardin, parrillero);
            return i;
        }

        /// <summary>
        /// Para asignar un ID a cada apartamento nuevo, esta función lee todo el archivo de DB.txt
        /// Compara los IDs numericos de los inmuebles de iguales condiciones. por ej, de los aptos que estén a la venta.
        /// Toma el mayor y le agrega 1, para asignar el siguiente disponible.
        /// </summary>
        /// <param name="i">Apartamento</param>
        /// <returns></returns>
        public static Apartamento CrearIdA(Apartamento i)
        {
            // El inmueble viene con un ID sin el numerio. Ej: Casa a la Venta, "C-A-" o Apartamento en Alquiler "A-A-".
            String prefijoCasaApartamentoInmueble = i.Id.Split('-')[0];  // De ese id, el primer valor es "C"
            String prefijoAlquilerVentaInmueble = i.Id.Split('-')[1];    // De ese id, el segundo valor es "A"

            StreamReader reader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\DB.txt");
            int idNumerico = 1;
            while (reader.Peek() > -1)
            {
                String linea = reader.ReadLine();
                String[] lineaSeparada = linea.Split('|');
                String prefijoCasaApartamentoLinea = lineaSeparada[0].Split('-')[0];
                String prefijoAlquilerVentaLinea = lineaSeparada[0].Split('-')[1];
                Int32 idNumericoLinea = Convert.ToInt32(lineaSeparada[0].Split('-')[2]);
                // Verifica sólo el id de las lineas que tengan el mismo prefijo que el inmueble.
                if (prefijoCasaApartamentoLinea.Equals(prefijoCasaApartamentoInmueble) && prefijoAlquilerVentaLinea.Equals(prefijoAlquilerVentaInmueble))
                {
                    if (idNumericoLinea > idNumerico)
                    {
                        idNumerico = idNumericoLinea;
                    }
                }
            }
            reader.Close();
            i.Id += idNumerico + 1;
            // Cuando se crea un id nuevo, se crea tambien la carpeta correspondiente para las fotos del inmueble.
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\img\\" + i.Id);
            return i;
        }
        /// <summary>
        /// Para asignar un ID a cada casa nueva, esta función lee todo el archivo de DB.txt
        /// Compara los IDs numericos de los inmuebles de iguales condiciones. por ej, de las casas que estén a la venta.
        /// Toma el mayor y le agrega 1, para asignar el siguiente disponible.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Casa CrearIdC(Casa i)
        {
            // El inmueble viene con un ID sin el numerio. Ej: Casa a la Venta, "C-A-" o Apartamento en Alquiler "A-A-".
            String prefijoCasaApartamentoInmueble = i.Id.Split('-')[0];  // De ese id, el primer valor es "C"
            String prefijoAlquilerVentaInmueble = i.Id.Split('-')[1];    // De ese id, el segundo valor es "A"

            StreamReader reader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\DB.txt");
            int idNumerico = 1;
            while (reader.Peek() > -1)
            {
                String linea = reader.ReadLine();
                String[] lineaSeparada = linea.Split('|');
                String prefijoCasaApartamentoLinea = lineaSeparada[0].Split('-')[0];
                String prefijoAlquilerVentaLinea = lineaSeparada[0].Split('-')[1];
                Int32 idNumericoLinea = Convert.ToInt32(lineaSeparada[0].Split('-')[2]);
                // Verifica sólo el id de las lineas que tengan el mismo prefijo que el inmueble.
                if (prefijoCasaApartamentoLinea.Equals(prefijoCasaApartamentoInmueble) && prefijoAlquilerVentaLinea.Equals(prefijoAlquilerVentaInmueble))
                {
                    if (idNumericoLinea > idNumerico)
                    {
                        idNumerico = idNumericoLinea;
                    }
                }
            }
            reader.Close();
            i.Id += idNumerico + 1;
            // Cuando se crea un id nuevo, se crea tambien la carpeta correspondiente para las fotos del inmueble.
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\img\\" + i.Id);
            return i;
        }

    }
}