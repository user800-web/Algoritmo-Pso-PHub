using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo_PSO_Problema_PHUB
{
    class Phub
    {
        public int NUM_CLIENTES;
        public int NUM_HUBS;
        public int CAPACIDAD_HUB;
        public List<Cliente> CLIENTES;

        public const int PENALIZACION = 1000;

        public Phub()
        {
            CLIENTES = new List<Cliente>();
        }

        public bool LeerDatos(string path)
        {
            string[] lines = File.ReadAllLines(path);

            if (lines.Length == 0) return false;

            // Leer la primera línea (encabezado)
            string[] encabezado = lines[0].Trim().Split(' ');

            // Debe contener tres partes (num de clientes, hubs y la capacidad de los hubs)
            if (encabezado.Length < 3) return false;

            if (!int.TryParse(encabezado[0], out NUM_CLIENTES)) return false;
            if (!int.TryParse(encabezado[1], out NUM_HUBS)) return false;
            if (!int.TryParse(encabezado[2], out CAPACIDAD_HUB)) return false;

            CLIENTES = new List<Cliente>(NUM_CLIENTES);

            for (int i = 0; i < NUM_CLIENTES; i++)
            {
                string [] linea= lines[i + 1].Trim().Split(' ');

                // Cada línea posterior al encabezado debe contener cuatro partes
                // (id o número de línea, coordenada x, coordenada y, demanda requerida)
                if (linea.Length < 4) return false;

                // Crear el objeto Cliente y agregarlo a la lista
                int id = 0;
                int x = 0;
                int y = 0;
                int demanda = 0;

                if (!int.TryParse(linea[0], out id)) return false;
                if (!int.TryParse(linea[1], out x)) return false;
                if (!int.TryParse(linea[2], out y)) return false;
                if (!int.TryParse(linea[3], out demanda)) return false;

                // Crear e inicializar el objeto y agregarlo a la lista
                Cliente cl = new Cliente(id, x, y, demanda);
                CLIENTES.Add(cl);                
            }

            return true;
        }

        public double dist(Cliente c1, Cliente c2)
        {
            // Calcular la distancia euclidiana
            return Math.Sqrt(Math.Pow(c1.X - c2.X, 2) + Math.Pow(c1.Y - c2.Y, 2));
        }

        public (List<Hub> hubs, double d) ObjectiveFunction(List<int> hubIndices)
        {
            // Transformar los clientes en objetos de tipo hub
            var hubs = hubIndices.Select(index => new Hub(CLIENTES[index])).ToList();
            double costoTotal = 0;

            foreach (var cliente in CLIENTES)
            {
                // Evaluar el cliente actual para cada hub:
                // filtrando los hubs donde la distancia acumulada más la demanda del cliente es menor que la capacidad máxima
                // y ordenarlo de formas ascendente según la distancia calculada
                var opcionesValidas = hubs
                    .Select((hub, idx) => new { hub, idx, distancia = dist(cliente, hub.Cliente) })
                    .Where(opcion => opcion.hub.Demanda_acumulada + cliente.Demanda <= CAPACIDAD_HUB)
                    .OrderBy(opcion => opcion.distancia)
                    .ToList();

                // Si existen clientes, entonces agregarlos al hub que es la mejor opción
                if (opcionesValidas.Any())
                {
                    // Obtener la primera opción, puesto que es la que representa la menor distancia
                    var mejorOpcion = opcionesValidas.First();
                    mejorOpcion.hub.Demanda_acumulada += cliente.Demanda;

                    // Almacenar los clientes asociados al hub y su distancia
                    mejorOpcion.hub.Clientes.Add(cliente);
                    mejorOpcion.hub.Distancias.Add(mejorOpcion.distancia);

                    costoTotal += mejorOpcion.distancia;
                }
                else
                {
                    // Si no se puede asignar a ningún hub (por exceso de demanda), se penaliza
                    costoTotal += PENALIZACION;
                }
            }
                        
            return (hubs, costoTotal);
        }

        
    }
}
