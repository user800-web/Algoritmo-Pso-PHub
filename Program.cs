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

            string ruta = @"C:\Algoritmos PSO\Datos-generados\demanda-media";
            //ruta = @"C:\Algoritmos PSO\Datos-generados\demanda-media\Phub_1K_10.txt";
            //EjecutarProblema(ruta: ruta, num_ejecuciones: 5, numero_iteraciones: 40, intervalo: 5,
            //  max_estancamiento: 25, reintentos: 2, tasa_reinicio: 0.20, numero_particulas: 10);

            //EjecutarArchivosDirectorio(ruta: ruta, reintentos: 1, tasa_reinicio:0);

            //Application.Run(new frmPhub());
            //EjecutarArchivosDirectorio2(ruta, numero_iteraciones: 50,
            //    max_estancamiento: 30, reintentos: 2, tasa_reinicio: 0.20, numero_particulas: 20);

            EjecutarArchivosDirectorioTiempo(ruta, numero_iteraciones: 50,
                max_estancamiento: 30, reintentos: 2, tasa_reinicio: 0.20, numero_particulas: 20);
        }

        static void EjecutarProblema(string ruta, int num_ejecuciones, int numero_iteraciones, int intervalo, 
            int max_estancamiento, int reintentos, double tasa_reinicio, int numero_particulas)
        {
            try
            {
                string encabezado = "repeticion;iter;ajuste";

                int NUMERO_ITERACIONES = numero_iteraciones;
                int NUMERO_PARTICULAS = numero_particulas;
                int MAX_ESTANCAMIENTO = max_estancamiento;
                double W = 1;
                double C1 = 1.5;
                double C2 = 1.9;

                double UMBRAL_MEJORA = 0.0001;

                //string d_base = Path.GetDirectoryName(ruta);
                string d_base = @"C:\Algoritmos PSO\EXPERIMENTOS\Experimento2-2";
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
                    pso_phub.SetParametros(NUMERO_PARTICULAS, NUMERO_ITERACIONES, W, C1, C2, UMBRAL_MEJORA, MAX_ESTANCAMIENTO, ruta, true, tasa_reinicio, reintentos);
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

        static void EjecutarArchivosDirectorio(string ruta, int reintentos, double tasa_reinicio)
        {
            try
            {
                string[] archivos = Directory.GetFiles(ruta, "*.txt");
                string ruta_salida = @"C:\Algoritmos PSO\EXPERIMENTOS\Experimento1\out.txt";

                string encabezado = "nodos;server;tiempo_ms";
                string soluciones = string.Empty;

                int NUMERO_PARTICULAS = 10;
                int NUMERO_ITERACIONES = 10;
                double W = 0.7;
                double C1 = 1.5;
                double C2 = 1.5;

                double UMBRAL_MEJORA = 0;
                int MAX_ESTANCAMIENTO = NUMERO_ITERACIONES;

                foreach (var archivo in archivos)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    PSO pso_phub = new PSO();
                    pso_phub.SetParametros(NUMERO_PARTICULAS, NUMERO_ITERACIONES, W, C1, C2, UMBRAL_MEJORA, MAX_ESTANCAMIENTO, archivo, false, tasa_reinicio, reintentos);
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

        static void EjecutarArchivosDirectorio2(string ruta, int numero_iteraciones,
            int max_estancamiento, int reintentos, double tasa_reinicio, int numero_particulas)
        {
            try
            {
                string[] archivos = Directory.GetFiles(ruta, "*.txt");
                string ruta_salida = @"C:\Algoritmos PSO\EXPERIMENTOS\Experimento3\resultados\Algoritmo3.txt";

                string encabezado = "fitness;nodos;servidores";
                string soluciones = string.Empty;

                int NUMERO_ITERACIONES = numero_iteraciones;
                int NUMERO_PARTICULAS = numero_particulas;
                int MAX_ESTANCAMIENTO = max_estancamiento;
                double W = 1;
                double C1 = 1.5;
                double C2 = 1.9;

                double UMBRAL_MEJORA = 0.0001;

                foreach (var archivo in archivos)
                {
                    PSO pso_phub = new PSO();
                    pso_phub.SetParametros(NUMERO_PARTICULAS, NUMERO_ITERACIONES, W, C1, C2, UMBRAL_MEJORA, MAX_ESTANCAMIENTO, archivo, false, tasa_reinicio, reintentos);
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var (hubs, fitness) = pso_phub.ejecutar();

                    stopwatch.Stop();
                    TimeSpan tiempo = stopwatch.Elapsed;

                    soluciones += $"{fitness};{pso_phub.PHUB.NUM_CLIENTES};{pso_phub.PHUB.NUM_HUBS}" + "\n";
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

        static void EjecutarArchivosDirectorioTiempo(string ruta, int numero_iteraciones,
            int max_estancamiento, int reintentos, double tasa_reinicio, int numero_particulas)
        {
            try
            {
                string[] archivos = Directory.GetFiles(ruta, "*.txt");
                string ruta_salida = @"C:\Algoritmos PSO\EXPERIMENTOS\Experimento3\resultados\Algoritmo3Tiempo.txt";

                string encabezado = "tiempo_ms;nodos;servidores";
                string soluciones = string.Empty;

                int NUMERO_ITERACIONES = numero_iteraciones;
                int NUMERO_PARTICULAS = numero_particulas;
                int MAX_ESTANCAMIENTO = max_estancamiento;
                double W = 1;
                double C1 = 1.5;
                double C2 = 1.9;

                double UMBRAL_MEJORA = 0.0001;

                foreach (var archivo in archivos)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    PSO pso_phub = new PSO();
                    pso_phub.SetParametros(NUMERO_PARTICULAS, NUMERO_ITERACIONES, W, C1, C2, UMBRAL_MEJORA, MAX_ESTANCAMIENTO, archivo, false, tasa_reinicio, reintentos);
                    var (hubs, fitness) = pso_phub.ejecutar();

                    stopwatch.Stop();
                    TimeSpan tiempo = stopwatch.Elapsed;

                    soluciones += $"{tiempo.TotalMilliseconds};{pso_phub.PHUB.NUM_CLIENTES};{pso_phub.PHUB.NUM_HUBS}" + "\n";
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
