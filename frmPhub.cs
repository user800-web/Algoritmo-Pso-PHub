using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algoritmo_PSO_Problema_PHUB
{
    public partial class frmPhub : Form
    {
        // Configuración de variables
        PSO pso_phub;
        int NUMERO_PARTICULAS = 20;
        int NUMERO_ITERACIONES = 100;
        double W = 0.7;
        double C1 = 1.5;
        double C2 = 1.5;

        double UMBRAL_MEJORA = 0.0001;
        int MAX_ESTANCAMIENTO = 20;
        private int MAX_REINTENTOS = 2;
        private double TASA_REINICIO = 0.20;

        // Ruta del problema
        string filepath;

        // Banderas
        bool PUEDE_DIBUJAR = false;
        bool DIBUJO_DINAMICO = false;

        // Variables para el movimiento del dibujo
        private const float DELTA = 0.2f;
        private float scale = 1f;
        private PointF offset = new PointF(0, 0);
        private Point prevMousePos;


        public frmPhub()
        {
            InitializeComponent();

            if (DIBUJO_DINAMICO)
            {
                this.Pbx_Nodos.MouseDown += new MouseEventHandler(Pbx_Nodos_MouseDown);
                this.Pbx_Nodos.MouseMove += new MouseEventHandler(Pbx_Nodos_MouseMove);
                this.Pbx_Nodos.MouseWheel += new MouseEventHandler(Pbx_Nodos_MouseWheel);
            }
        }

        private void frmPhub_Load(object sender, EventArgs e)
        {
            filepath = string.Empty;
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
                filepath = openFileDialog.FileName;

                // Enviar la ruta del archivo al método LeerDatos
                pso_phub = new PSO(NUMERO_PARTICULAS, NUMERO_ITERACIONES, W, C1, C2, UMBRAL_MEJORA, MAX_ESTANCAMIENTO, filepath, 
                    chbMostrarSoluciones.Checked, TASA_REINICIO, MAX_REINTENTOS);
                lbcapacidad.Text = pso_phub.PHUB.CAPACIDAD_HUB.ToString();

                PUEDE_DIBUJAR = false;
                Pbx_Nodos.Invalidate();

                CargarDemandasEnDataGridView();
            }
        }

        private void Btn_GenerarPso_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(filepath))
            {
                MessageBox.Show("No ha seleccionado ningún archivo.");
                return;
            }

            // Obtener el número de iteraciones especificadas por el usuario
            if (!int.TryParse(Txt_NumeroIteraciones.Text, out NUMERO_ITERACIONES))
            {
                MessageBox.Show("Por favor, ingresa un número válido de iteraciones.");
                return;
            }

            txtSoluciones.Visible = chbMostrarSoluciones.Checked;

            // Reiniciar el texto de los textbox
            lblTiempoTranscurrido.Text = "0 ms";
            txtSoluciones.Text = "";
            txt_MejorSolucion.Text = "";

            // Empezar el contador
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Establecer los parámetros y ejecutar PSO para la resolución del problema
            pso_phub.SetParametros(NUMERO_PARTICULAS, NUMERO_ITERACIONES, W, C1, C2, UMBRAL_MEJORA, MAX_ESTANCAMIENTO, filepath, true, 
                TASA_REINICIO, MAX_REINTENTOS);
            pso_phub.ejecutar();

            // Detener el contador y obtener el tiempo transcurrido
            stopwatch.Stop();
            TimeSpan tiempo = stopwatch.Elapsed;

            lblTiempoTranscurrido.Text = tiempo.TotalMilliseconds.ToString() + " ms";

            txtSoluciones.Text = pso_phub.soluciones.ToString();
            txt_MejorSolucion.Text = pso_phub.mejor_solucion.ToString();

            PUEDE_DIBUJAR = true;
            Pbx_Nodos.Invalidate();
        }

        private void Pbx_Nodos_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Pbx_Nodos.BackColor);

            if (!PUEDE_DIBUJAR) return;

            Pen pen = new Pen(Color.Black);

            if (pso_phub == null || pso_phub.gBestHubs == null || pso_phub.gBestHubs.Count == 0) return;

            // Obtener los límites de las coordenadas
            double minX = pso_phub.PHUB.XMIN;
            double maxX = pso_phub.PHUB.XMAX;
            double minY = pso_phub.PHUB.YMIN;
            double maxY = pso_phub.PHUB.YMAX;

            // Calcular el factor de escala ajustado
            float scaleX = (float)(Pbx_Nodos.Width / (maxX - minX + 20));
            float scaleY = (float)(Pbx_Nodos.Height / (maxY - minY + 20));
            float nueva_escala = Math.Min(scaleX, scaleY) * scale;

            // Tamaño relativo de los nodos
            float hubSize = 20 / nueva_escala;
            float clientSize = 10 / nueva_escala;

            pen = new Pen(Color.Black, 3 / nueva_escala);

            // Ajustar el desplazamiento
            float offsetX = (float)((Pbx_Nodos.Width - (maxX - minX) * nueva_escala) / 2 - minX * nueva_escala + offset.X);
            float offsetY = (float)((Pbx_Nodos.Height - (maxY - minY) * nueva_escala) / 2 - minY * nueva_escala + offset.Y);

            // Aplicar la transformación de desplazamiento y escala
            g.TranslateTransform(offsetX, offsetY);
            g.ScaleTransform(nueva_escala, nueva_escala);

            Font font = new Font(Font.FontFamily, Font.Size / nueva_escala);

            // Dibujar los hubs y clientes
            for (int i = 0; i < pso_phub.gBestHubs.Count; i++)
            {
                float x = (float)pso_phub.gBestHubs[i].Cliente.X;
                float y = (float)pso_phub.gBestHubs[i].Cliente.Y;

                for (int j = 0; j < pso_phub.gBestHubs[i].Clientes.Count; j++)
                {
                    Cliente cliente = pso_phub.gBestHubs[i].Clientes[j];
                    int xCliente = int.Parse(cliente.X.ToString());
                    int yCliente = int.Parse(cliente.Y.ToString());

                    g.DrawLine(pen, xCliente, yCliente, x, y);
                    g.FillEllipse(Brushes.Blue, xCliente - clientSize / 2, yCliente - clientSize / 2, clientSize, clientSize);
                    g.DrawString($"Cliente {cliente.ID + 1} ({cliente.X},{cliente.Y})", font, Brushes.Black, xCliente + 5, yCliente + 5);
                }

                Color colorHub = ObtenerColorHub(i);
                using (Brush brushHub = new SolidBrush(colorHub))
                {
                    g.FillEllipse(brushHub, x - hubSize / 2, y - hubSize / 2, hubSize, hubSize);
                }

                g.DrawString($"Hub {i + 1}", font, Brushes.Black, x, y);
            }

            // Visualizar los clientes no conectados
            if (chbClientesSinConexion.Checked && !pso_phub.PHUB.DEMANDA_SATISFACIBLE)
            {
                foreach (var cliente in pso_phub.PHUB.CLIENTES)
                {
                    bool conectado = pso_phub.gBestHubs.Any(hub => hub.Clientes.Contains(cliente));
                    int xCliente = int.Parse(cliente.X.ToString());
                    int yCliente = int.Parse(cliente.Y.ToString());

                    if (!conectado)
                    {
                        g.FillEllipse(Brushes.Red, xCliente - clientSize / 2, yCliente - clientSize / 2, clientSize, clientSize);
                        g.DrawString($"Cliente {cliente.ID + 1} ({cliente.X},{cliente.Y})", font, Brushes.Black, xCliente + 5, yCliente + 5);
                    }
                }
            }
        }

        private void Pbx_Nodos_MouseWheel(object sender, MouseEventArgs e)
        {
            // Determina si el zoom es hacia adentro o hacia afuera
            float delta = e.Delta > 0 ? DELTA : -DELTA;
            scale += delta;

            if (scale < 0.1f) scale = 0.1f;

            // Redibujar
            Pbx_Nodos.Invalidate();
        }

        private void Pbx_Nodos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                prevMousePos = e.Location;
            }
        }

        private void Pbx_Nodos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                offset.X += e.X - prevMousePos.X;
                offset.Y += e.Y - prevMousePos.Y;
                prevMousePos = e.Location;

                Pbx_Nodos.Invalidate();
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
                dtdemandas.Columns.Add("demanda", "Dem.");
            }

            // Recorrer la matriz 'clientes' para cargar los datos en el DataGridView
            for (int i = 0; i < pso_phub.PHUB.NUM_CLIENTES; i++)
            {
                int id = pso_phub.PHUB.CLIENTES[i].ID;
                int demanda = pso_phub.PHUB.CLIENTES[i].Demanda;

                // Agregar una fila al DataGridView con el ID y la demanda
                dtdemandas.Rows.Add(id, demanda);
            }
        }

        private Color ObtenerColorHub(int index)
        {
            // Colores predeterminados para los primeros hubs
            Color[] coloresPredeterminados = { Color.Red, Color.Green, Color.Aqua, Color.Brown };

            if (index < coloresPredeterminados.Length)
            {
                return coloresPredeterminados[index];
            }
            else
            {
                // Si se acaban los colores predeterminados, genera un color aleatorio
                Random random = new Random(index);
                return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }
        }

        private void Pbx_Nodos_Resize(object sender, EventArgs e)
        {
            Pbx_Nodos.Invalidate();
        }

        private void chbMostrarSoluciones_CheckedChanged(object sender, EventArgs e)
        {
            txtSoluciones.Visible = chbMostrarSoluciones.Checked;
        }

        private void chbClientesSinConexion_CheckedChanged(object sender, EventArgs e)
        {
            Pbx_Nodos.Invalidate();
        }

        private void gbEjecucion_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.gbEjecucion.ClientRectangle, Color.FromArgb(100, 146, 146, 146), ButtonBorderStyle.Solid);
        }
    }
}
