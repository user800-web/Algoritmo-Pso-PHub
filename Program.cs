using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algoritmo_PSO_Problema_PHUB
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string ruta = @"C:\Algoritmos PSO\Datos-generados";
            ruta = @"C:\Algoritmos PSO\Datos-generados\demanda-baja\Phub_1K_10.txt";
            //EjecutarProblema(ruta, 5, 40, 5);

            //EjecutarArchivosDirectorio(ruta);

            Application.Run(new frmPhub());
        }

        static void EjecutarProblema(string ruta, int num_ejecuciones, int numero_iteraciones, int intervalo)
        {
            try
            {
                string encabezado = "repeticion;iter;ajuste";

                int NUMERO_ITERACIONES = numero_iteraciones;
                int NUMERO_PARTICULAS = 10;
                double W = 0.7;
                double C1 = 1.7;
                double C2 = 1.5;

                //string d_base = Path.GetDirectoryName(ruta);
                string d_base = @"C:\Algoritmos PSO\EXPERIMENTOS\Experimento2";
                string nombre_archivo = Path.GetFileName(ruta);
                string d_salida = Path.Combine(d_base, $"resultados_{nombre_archivo}");
                if (!Directory.Exists(d_salida))
                {
                    Directory.CreateDirectory(d_salida);
                }

                StringBuilder output_repeticion = new StringBuilder();
                string ruta_salida = Path.Combine(d_salida, $"out.txt");

                for (int i = 0; i < num_ejecuciones; i++)
                {

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    PSO pso_phub = new PSO();
                    pso_phub.SetParametros(NUMERO_PARTICULAS, NUMERO_ITERACIONES, W, C1, C2, ruta, true);
                    pso_phub.ejecutar_enlistando(intervalo, i + 1);

                    output_repeticion.Append(pso_phub.soluciones_csv.ToString());

                    stopwatch.Stop();
                    TimeSpan tiempo = stopwatch.Elapsed;
                }


                using (StreamWriter writer = new StreamWriter(ruta_salida, append: false))
                {
                    writer.WriteLine(encabezado);
                    writer.WriteLine(output_repeticion.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static void EjecutarArchivosDirectorio(string ruta)
        {
            try
            {
                string[] archivos = Directory.GetFiles(ruta, "*.txt");
                string ruta_salida = ruta + @"\out.csv";

                string encabezado = "nodos;server;tiempo_ms";
                string soluciones = string.Empty;

                int NUMERO_PARTICULAS = 10;
                int NUMERO_ITERACIONES = 10;
                double W = 0.7;
                double C1 = 1.5;
                double C2 = 1.5;

                foreach (var archivo in archivos)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    PSO pso_phub = new PSO();
                    pso_phub.SetParametros(NUMERO_PARTICULAS, NUMERO_ITERACIONES, W, C1, C2, archivo, false);
                    pso_phub.ejecutar();

                    stopwatch.Stop();
                    TimeSpan tiempo = stopwatch.Elapsed;

                    soluciones += pso_phub.GetMejorSolucion(tiempo) + "\n";
                }

                using (StreamWriter writer = new StreamWriter(ruta_salida, append: false))
                {
                    writer.WriteLine(encabezado);
                    writer.WriteLine(soluciones);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
