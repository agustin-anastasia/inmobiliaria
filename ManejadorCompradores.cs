using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Inmobiliaria
{
    public class ManejadorCompradores: ManejadorArchivos
    {
        /// <summary>
        /// Manejador compradores
        /// </summary>
        public List<Persona> listaCompradores;
        public override void Guardar(String nomArchivo, String texto)
        {
            StreamWriter writer = new StreamWriter(nomArchivo, true);
            writer.WriteLine(texto);
            writer.Close();
        }

        /// <summary>
        /// Metodo leer archivo compradores
        /// </summary>
        /// <param name="nomArchivo"></param>
        /// <returns></returns>
        public List<Persona> Leer(String nomArchivo)
        {
            StreamReader reader = new StreamReader(nomArchivo);
            while (reader.Peek() > -1)
            {
                String s = reader.ReadLine();
                String[] linea = s.Split(',');
                Persona c = new Cliente(linea[0], linea[1], linea[3], Convert.ToInt32(linea[4]), linea[5]);
                listaCompradores.Add(c);
            }
            reader.Close();
            return listaCompradores;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ManejadorCompradores()
        {
            listaCompradores = new List<Persona>();
        }
        /// <summary>
        /// Metodo agregar cliente a la lista de compradores
        /// </summary>
        /// <param name="c"></param>
        public void Agregar(Cliente c)
        {
            listaCompradores.Add(c);
        }
    }
}