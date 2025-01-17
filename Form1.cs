using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Algoritmo_PSO_Problema_PHUB;
using static Algoritmo_PSO_Problema_PHUB.Form1;

namespace Algoritmo_PSO_Problema_PHUB
{
    public partial class Form1 : Form
    {
        #region
        // Definición de las variables del problema
        static int n; // número de clientes
        static int numHubs; // número de hubs, p
        static int capacidadHub; // capacidad de los hubs, Q
        static int[,] clientes; // matriz de clientes: [id, x, y, demanda]        

        // Definición de las variables del algoritmo PSO
        static int iteraciones;
        static double w = 0.7; // Inercia
        static double c1 = 1.5; // Constante cognitiva
        static double c2 = 1.5; // Constante social
        #endregion            
        ///-----------------------------------Variables globales-------------------------------------///
        static List<Cliente> clientesv6 = new List<Cliente>();            
        static int swarmSize = 50;
        static string soluciones = "";
        static List<(double X, double Y)> mejoresHubsV6 = new List<(double X, double Y)>();
        static Dictionary<int, List<(double X, double Y)>> asignacionClientesV6 = new Dictionary<int, List<(double X, double Y)>>();
        static StringBuilder sbsoluciones = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_SelecciónDatos_Click(object sender, EventArgs e)
        {
            // Crear un OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Establecer las propiedades del OpenFileDialog
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            // Mostrar el cuadro de diálogo para abrir archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta del archivo seleccionado
                string filePath = openFileDialog.FileName;

                // Enviar la ruta del archivo al método LeerDatos
                LeerDatos(filePath);
                lbcapacidad.Text = capacidadHub.ToString();
                CargarDemandasEnDataGridView();
            }
        }
        
        private void Btn_GenerarPso_Click(object sender, EventArgs e)
        {
            // Obtener el número de iteraciones desde Txt_NumeroIteraciones
            if (!int.TryParse(Txt_NumeroIteraciones.Text, out iteraciones))
            {
                MessageBox.Show("Por favor, ingresa un número válido de iteraciones.");
                return;
            }
            txtSoluciones.Text = "";
            Txt_MejorSolucion.Text = "";

            /// Codigo v6----------------------///
            /// 
            soluciones = "";
            string bestsolucion=ejecutar();
            txtSoluciones.Text = soluciones;
            Txt_MejorSolucion.Text = bestsolucion;
            ///--------------------------------///
            Pbx_Nodos.Invalidate();                        
        }

        static void LeerDatos(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] firstLine = lines[0].Trim().Split(' '); // Trim() elimina los espacios en blanco al principio y al final de la cadena
            n = int.Parse(firstLine[0]);
            numHubs = int.Parse(firstLine[1]);
            capacidadHub = int.Parse(firstLine[2]);
            
            clientes = new int[n, 4];//se usará para el DataGridView
            clientesv6=new List<Cliente>();
            for (int i = 0; i < n; i++)
            {
                string line = lines[i + 1].Trim();

                // Verificar si la línea contiene al menos un número
                if (line.Split(' ').Any(s => int.TryParse(s, out _)))
                {
                    string[] parts = line.Split(' ');
                    for (int j = 0; j < 4; j++)
                    {
                        clientes[i, j] = int.Parse(parts[j]);
                    }

                    // Crear el objeto Cliente y agregarlo a la lista
                    int id = int.Parse(parts[0]);
                    int x = int.Parse(parts[1]);
                    int y = int.Parse(parts[2]);
                    int demanda = int.Parse(parts[3]);
                    Cliente cl = new Cliente(x, y, demanda);
                    clientesv6.Add(cl);
                }
            }
        }

        private void CargarDemandasEnDataGridView()
        {
            // Limpiar el DataGridView antes de agregar nuevos datos
            dtdemandas.Rows.Clear();

            // Verificar si el DataGridView tiene las columnas necesarias
            if (dtdemandas.Columns.Count == 0)
            {
                dtdemandas.Columns.Add("id", "ID");
                dtdemandas.Columns.Add("demanda", "Demanda");
            }

            // Recorrer la matriz 'clientes' para cargar los datos en el DataGridView
            for (int i = 0; i < n; i++)
            {
                int id = clientes[i, 0]; // ID del cliente
                int demanda = clientes[i, 3]; // Demanda del cliente

                // Agregar una fila al DataGridView con el ID y la demanda
                dtdemandas.Rows.Add(id, demanda);
            }
        }

        private Color ObtenerColorHub(int index)
        {
            // Colores predeterminados para los primeros hubs
            Color[] coloresPredeterminados = { Color.Red, Color.Green, Color.Black, Color.Brown };

            if (index < coloresPredeterminados.Length)
            {
                return coloresPredeterminados[index];
            }
            else
            {
                // Si se acaban los colores predeterminados, genera un color aleatorio
                Random random = new Random(index); // Semilla con el índice para que cada hub tenga un color constante
                return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }
        }

        private void Pbx_Nodos_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Pbx_Nodos.BackColor); // Limpia el área de dibujo con el color de fondo del PictureBox

            Pen pen = new Pen(Color.Black);

            if (mejoresHubsV6.Count == 0 || asignacionClientesV6.Count == 0) return; // Si no hay datos, no se grafica nada

            // Obtener los límites de las coordenadas
            double minX = Math.Min(mejoresHubsV6.Min(h => h.X), asignacionClientesV6.Values.SelectMany(c => c).Min(c => c.X));
            double maxX = Math.Max(mejoresHubsV6.Max(h => h.X), asignacionClientesV6.Values.SelectMany(c => c).Max(c => c.X));
            double minY = Math.Min(mejoresHubsV6.Min(h => h.Y), asignacionClientesV6.Values.SelectMany(c => c).Min(c => c.Y));
            double maxY = Math.Max(mejoresHubsV6.Max(h => h.Y), asignacionClientesV6.Values.SelectMany(c => c).Max(c => c.Y));

            // Calcular el factor de escala
            double scaleX = Pbx_Nodos.Width / (maxX - minX + 20);
            double scaleY = Pbx_Nodos.Height / (maxY - minY + 20);
            float scale = (float)Math.Min(scaleX, scaleY);

            // Tamaño relativo de los nodos
            float hubSize = 20 / scale;
            float clientSize = 10 / scale;

            pen = new Pen(Color.Black, 3 / scale);

            // Desplazamiento para centrar el contenido
            int offsetX = (int)((Pbx_Nodos.Width - (maxX - minX) * scale) / 2 - minX * scale);
            int offsetY = (int)((Pbx_Nodos.Height - (maxY - minY) * scale) / 2 - minY * scale);

            // Aplicar la transformación
            g.TranslateTransform(offsetX, offsetY);
            g.ScaleTransform(scale, scale);

            Font font = new Font(Font.FontFamily, Font.Size / scale);

            // Dibujar los hubs
            for (int i = 0; i < mejoresHubsV6.Count; i++)
            {
                float x = float.Parse(mejoresHubsV6[i].X.ToString());
                float y = float.Parse(mejoresHubsV6[i].Y.ToString());
                Color colorHub = ObtenerColorHub(i);
                using (Brush brushHub = new SolidBrush(colorHub))
                {
                    g.FillEllipse(brushHub, x - hubSize / 2, y - hubSize / 2, hubSize, hubSize);
                }
                g.DrawString($"Hub {i + 1}", font, Brushes.Black, x + 5, y + 5);
            }

            // Dibujar los clientes y las líneas a los hubs correspondientes
            foreach (var kvp in asignacionClientesV6)
            {
                int hubIndex = kvp.Key;
                int xHub = int.Parse(mejoresHubsV6[hubIndex].X.ToString());
                int yHub = int.Parse(mejoresHubsV6[hubIndex].Y.ToString());

                foreach (var cliente in kvp.Value)
                {
                    int xCliente = int.Parse(cliente.X.ToString());
                    int yCliente = int.Parse(cliente.Y.ToString());

                    // Encontrar la posición del cliente en ClienteV6
                    int posicionCliente = clientesv6.FindIndex(c => c.X == cliente.X && c.Y == cliente.Y) + 1;
                    int Valor = posicionCliente;

                    // Dibujar línea al hub correspondiente
                    g.DrawLine(pen, xCliente, yCliente, xHub, yHub);

                    // Dibujar el cliente con el valor de posición
                    g.FillEllipse(Brushes.Blue, xCliente - clientSize / 2, yCliente - clientSize / 2, clientSize, clientSize);
                    g.DrawString($"Cliente {Valor} ({cliente.X},{cliente.Y})", font, Brushes.Black, xCliente + 5, yCliente + 5);
                }
            }
        }

        static double Distancia(Cliente c1, Cliente c2)
        {
            return Math.Sqrt(Math.Pow(c1.X - c2.X, 2) + Math.Pow(c1.Y - c2.Y, 2));
        }
        
        /// Métodos////
        public string ejecutar()
        {
            //asignacionClientesV6 = new Dictionary<int, List<(double X, double Y)>>();
            PSOHubOptimization pso = new PSOHubOptimization(clientesv6, capacidadHub, numHubs);
            var (bestIndices, bestCost, costoReal, BestAsignacionHubs) = pso.RunPSO(swarmSize, iteraciones);
            //MessageBox.Show("Respuesta: " + BestAsignacionHubs.Count);

            StringBuilder sb = new StringBuilder();
            // Limpiar datos anteriores
            mejoresHubsV6.Clear();
            asignacionClientesV6.Clear();
            sb.Clear();
            sb.AppendLine("Mejor solución encontrada:");
            sb.AppendLine("Clientes seleccionados como hubs:");
            sb.AppendLine("...............................................................................");
            foreach (int index in bestIndices)
            {
                var hub = clientesv6[index];
                mejoresHubsV6.Add((hub.X, hub.Y));
                
                sb.AppendLine($"Hub: (x = {hub.X}, y = {hub.Y}), Demanda = {hub.Demanda}");
            }

            sb.AppendLine($"\nCosto total de distancia mínima calculada: {costoReal:F2}");

            sb.AppendLine("\nAsignación de clientes a hubs y distancias:");
            //MessageBox.Show("A ver esta variable"+BestAsignacionHubs.Count.ToString());
            if (BestAsignacionHubs.Count == 0)
            {
                BestAsignacionHubs = pso.ObtenerAsignacionHubs(bestIndices);
            }
            foreach (var hubIndex in BestAsignacionHubs.Keys)
            {
                var hub = clientesv6[bestIndices[hubIndex]];
                // Agregar lista de clientes asignados a este hub
                asignacionClientesV6[hubIndex] = new List<(double X, double Y)>();
                sb.AppendLine($"\nHub {hubIndex + 1} (x = {hub.X}, y = {hub.Y}): Demanda acumulada = {pso.bestDemandaHubs[hubIndex]}");

                foreach (var (cliente, distancia) in BestAsignacionHubs[hubIndex])
                {
                    sb.AppendLine($"  Cliente en (x = {cliente.X}, y = {cliente.Y}), demanda = {cliente.Demanda}, distancia = {Distancia(hub, cliente):F2}");
                    asignacionClientesV6[hubIndex].Add((cliente.X, cliente.Y));

                }
            }
            return sb.ToString();
            //MessageBox.Show("1: "+ asignacionClientesV6.Count);
        }
        /// </summary>----------------------///
        ///
        public class Cliente
        {
            public double X { get; }
            public double Y { get; }
            public int Demanda { get; }

            public Cliente(double x, double y, int demanda)
            {
                X = x;
                Y = y;
                Demanda = demanda;
            }
        }

        public class Particle
        {
            public List<int> Position { get; set; }
            public List<int> BestPosition { get; set; }
            public List<double> Velocity { get; set; }
            public double BestCost { get; set; }
        }

        public class PSOHubOptimization
        {
            private static Random random = new Random();
            private List<Cliente> clientes;
            private int capacidadHub;
            private int numHubs;
            public Dictionary<int, List<(Cliente, double)>> currentAsignacionHubs=new Dictionary<int, List<(Cliente, double)>>();
            public Dictionary<int, int> currentDemandaHubs;
            public Dictionary<int, int> bestDemandaHubs;
            public PSOHubOptimization(List<Cliente> clientes, int capacidadHub, int numHubs)
            {
                this.clientes = clientes;
                this.capacidadHub = capacidadHub;
                this.numHubs = numHubs;

                //MessageBox.Show("Primer valor de currentAsignacionHubs" + currentAsignacionHubs.Count.ToString());
            }

            public double Distancia(Cliente c1, Cliente c2)
            {
                return Math.Sqrt(Math.Pow(c1.X - c2.X, 2) + Math.Pow(c1.Y - c2.Y, 2));
            }

            public Dictionary<int, List<(Cliente, double)>> ObtenerAsignacionHubs(List<int> hubIndices)
            {
                List<Cliente> hubs = hubIndices.Select(index => clientes[index]).ToList();
                var demandaHubs = new Dictionary<int, int>();
                Dictionary<int, List<(Cliente, double)>> asignacionHubs = new Dictionary<int, List<(Cliente, double)>>();

                // Inicializar las asignaciones y demandas por hub
                for (int i = 0; i < numHubs; i++)
                {
                    demandaHubs[i] = 0;
                    asignacionHubs[i] = new List<(Cliente, double)>();
                }

                // Asignar clientes a hubs y calcular las distancias
                foreach (var cliente in clientes)
                {
                    var distanciasValidas = hubs.Select((hub, idx) => (idx, Distancia(cliente, hub)))
                                                .Where(d => demandaHubs[d.idx] + cliente.Demanda <= capacidadHub)
                                                .ToList();

                    if (distanciasValidas.Count > 0)
                    {
                        // Seleccionamos el hub más cercano
                        var (hubMasCercano, minDistancia) = distanciasValidas.OrderBy(d => d.Item2).First();

                        // Actualizar la demanda del hub y añadir al cliente
                        demandaHubs[hubMasCercano] += cliente.Demanda;
                        asignacionHubs[hubMasCercano].Add((cliente, minDistancia));
                    }
                    else
                    {
                        // Si no se puede asignar a ningún hub (por exceso de demanda), no se asigna el cliente
                    }
                }

                // Retorna solo la asignación de hubs sin calcular la distancia total
                return asignacionHubs;
            }

            public double ObjectiveFunction(List<int> hubIndices)
            {
                int control = 0;
                List<Cliente> hubs = hubIndices.Select(index => clientes[index]).ToList();
                double totalDistancia = 0;
                var demandaHubs = new Dictionary<int, int>();
                Dictionary<int, List<(Cliente, double)>> asignacionHubs = new Dictionary<int, List<(Cliente, double)>>();
                
                // Inicializar las asignaciones y demandas por hub
                for (int i = 0; i < numHubs; i++)
                {
                    demandaHubs[i] = 0;
                    asignacionHubs[i] = new List<(Cliente, double)>();
                }

                // Asignar clientes a hubs y calcular las distancias
                double distanciareal = 0;
                foreach (var cliente in clientes)
                {
                    var distanciasValidas = hubs.Select((hub, idx) => (idx, Distancia(cliente, hub)))
                                                .Where(d => demandaHubs[d.idx] + cliente.Demanda <= capacidadHub) // Verifica que no exceda la capacidad del hub
                                                .ToList();


                    if (distanciasValidas.Count > 0)
                    {
                        // Seleccionamos el hub más cercano
                        var (hubMasCercano, minDistancia) = distanciasValidas.OrderBy(d => d.Item2).First();

                        // Actualizar la demanda del hub y añadir al cliente
                        demandaHubs[hubMasCercano] += cliente.Demanda;
                        totalDistancia += minDistancia;
                        asignacionHubs[hubMasCercano].Add((cliente, minDistancia));
                    }
                    else
                    {
                        // Si no se puede asignar a ningún hub (por exceso de demanda), asignamos un costo muy alto
                        totalDistancia += 1000;
                        distanciareal += 1000;
                    }
                }
                // Guarda la asignación actual para poder mostrarla después de la optimización
                control += asignacionHubs.Count();
                this.currentAsignacionHubs = asignacionHubs.ToDictionary(entry => entry.Key, entry => new List<(Cliente, double)>(entry.Value));


                this.currentDemandaHubs = demandaHubs;

                // Imprimir la asignación actual de clientes a hubs y las distancias
                sbsoluciones.AppendLine("\nAsignación actual de clientes a hubs y distancias:");
                foreach (var hubIndex in asignacionHubs.Keys)
                {
                    var hub = hubs[hubIndex];
                    sbsoluciones.AppendLine($"\nHub {hubIndex + 1} (x = {hub.X}, y = {hub.Y}): Demanda acumulada = {demandaHubs[hubIndex]}");

                    foreach (var (cliente, distancia) in asignacionHubs[hubIndex])
                    {
                        sbsoluciones.AppendLine($"  Cliente en (x = {cliente.X}, y = {cliente.Y}), demanda = {cliente.Demanda}, distancia = {distancia:F2}");
                        distanciareal += distancia;
                    }
                }
                sbsoluciones.AppendLine("\nCosto total:" + distanciareal);
                if (distanciareal < BestCost)
                {
                    BestCost = distanciareal;
                    this.bestDemandaHubs = demandaHubs;
                }
                return distanciareal;
            }

            static double BestCost;
            static Particle Bestparticle = null;
            static Dictionary<int, List<(Cliente, double)>> BestAsignacionHubs=new Dictionary<int, List<(Cliente, double)>>();

            public (List<int>, double, double, Dictionary<int, List<(Cliente, double)>>) RunPSO(int swarmSize, int maxIterations)
            {
                BestCost=double.MaxValue;
                BestAsignacionHubs = new Dictionary<int, List<(Cliente, double)>>();
                List<Particle> particles = new List<Particle>();
                List<int> globalBestPosition = new List<int>();
                double globalBestCost = double.MaxValue;

                for (int i = 0; i < swarmSize; i++)
                {
                    Particle particle = new Particle
                    {
                        Position = new List<int>(),
                        Velocity = new List<double>(),
                        BestPosition = new List<int>(),
                        BestCost = double.MaxValue
                    };

                    for (int j = 0; j < numHubs; j++)
                    {
                        int position = random.Next(clientes.Count);
                        particle.Position.Add(position);
                        particle.BestPosition.Add(position);
                        particle.Velocity.Add(random.NextDouble() * 2 - 1); // Velocidad aleatoria entre -1 y 1
                    }
                    Console.WriteLine(particle.ToString());

                    double cost = ObjectiveFunction(particle.Position);
                    if (cost < particle.BestCost)
                    {
                        particle.BestCost = cost;
                        particle.BestPosition = new List<int>(particle.Position);
                    }

                    if (cost < globalBestCost)
                    {
                        globalBestCost = cost;
                        BestCost = cost;
                        globalBestPosition = new List<int>(particle.Position);
                        BestCost = cost;

                        Bestparticle = particle;

                        BestAsignacionHubs = currentAsignacionHubs;
                    }
                   
                    particles.Add(particle);
                }                

                for (int iter = 0; iter < maxIterations; iter++)
                {
                    foreach (var particle in particles)
                    {
                        for (int j = 0; j < numHubs; j++)
                        {
                            double r1 = random.NextDouble();
                            double r2 = random.NextDouble();

                            // Actualizar velocidad
                            particle.Velocity[j] = w * particle.Velocity[j]
                                + c1 * r1 * (particle.BestPosition[j] - particle.Position[j])
                                + c2 * r2 * (globalBestPosition[j] - particle.Position[j]);

                            // Actualizar posición
                            particle.Position[j] = Math.Abs(particle.Position[j] + (int)particle.Velocity[j]) % clientes.Count;
                        }
                        // Evaluar función objetivo
                        double cost = ObjectiveFunction(particle.Position);
                        if (cost < particle.BestCost)
                        {
                            particle.BestCost = cost;
                            particle.BestPosition = new List<int>(particle.Position);
                        }

                        if (cost < globalBestCost)
                        {
                            globalBestCost = cost;
                            globalBestPosition = new List<int>(particle.Position);
                            BestCost = cost;
                            Bestparticle = particle;
                            BestAsignacionHubs = currentAsignacionHubs;
                        }
                    }
                }
                soluciones=sbsoluciones.ToString();

                return (globalBestPosition, globalBestCost, BestCost, BestAsignacionHubs);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}