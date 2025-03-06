using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo_PSO_Problema_PHUB
{
    class PSO
    {
        // Variables definidas para la configuración del algoritmo
        private int NUMERO_PARTICULAS = 30;     // Número de partículas o swarm
        private int ITERACIONES = 100;          // Número de iteraciones
        private double WMAX = 0.9;              // Factor de inercia (máximo)
        private double WMIN = 0.4;              // Factor de inercia (mínimo)
        private double W = 0.5;                 // Factor de inercia
        private double C1 = 1.5;                // Factor cognitivo
        private double C2 = 1.5;                // Factor social

        // Configuración para la terminación cuando no exista mejora en las soluciones
        private double UMBRAL_MEJORA = 0.0001;  // Umbral aceptable al calcular la diferencia relativa entre el ajuste de las soluciones
        private int MAX_ESTANCAMIENTO = 20;     // Número máximo de iteraciones antes de romper el bucle, debido a estancamiento

        // Variables para controlar una perturbación cuando se encuentre estancamiento
        private int MAX_REINTENTOS = 3;         // Número de reintentos a aplicarse dentro del rango (iteraciones) de estancamiento
        private double TASA_REINICIO = 0.20;    // Porcentaje de partículas que serán reiniciadas

        // Variables del problema
        public Phub PHUB;
        Random rnd;

        // Variables de salida
        bool guardar_soluciones = false;
        public List<Hub> gBestHubs;
        public double gBest;

        // Se activa cuando 'guardar_soluciones' sea verdadaero. Almacenará los valores separados por ';',
        // estos valores serán el número de iteración y su valor de ajuste
        public StringBuilder soluciones_csv;

        public StringBuilder soluciones;
        public StringBuilder mejor_solucion;
        public TimeSpan tiempo;

        public PSO() { }

        public PSO(int NUMERO_PARTICULAS, int ITERACIONES, double W, double C1, double C2, 
            double TOLERANCIA, int CONTADOR_ESTANCAMIENTO, string path, bool guardar_soluciones,
            double TASA_REINICIO, int MAX_REINTENTOS)
        {
            //Inicializar las variables con las definidas por el usuario
            this.NUMERO_PARTICULAS = NUMERO_PARTICULAS;
            this.ITERACIONES = ITERACIONES;


            this.W = W;
            this.C1 = C1;
            this.C2 = C2;

            this.guardar_soluciones = guardar_soluciones;
            this.UMBRAL_MEJORA = TOLERANCIA;
            this.MAX_ESTANCAMIENTO = CONTADOR_ESTANCAMIENTO;

            this.TASA_REINICIO = TASA_REINICIO;
            this.MAX_REINTENTOS = MAX_REINTENTOS;

            // Inicializar el procesamiento de los datos del algoritmo
            PHUB = new Phub();
            bool datos_validos = PHUB.LeerDatos(path);

            if (!datos_validos)
            {
                System.Windows.Forms.MessageBox.Show("El conjunto de datos de entrada no es válido");
                return;
            }

            rnd = new Random();

            soluciones_csv = new StringBuilder();
            soluciones = new StringBuilder();
            mejor_solucion = new StringBuilder();
            tiempo = new TimeSpan();
        }

        public void SetParametros(int NUMERO_PARTICULAS, int ITERACIONES, double W, double C1, double C2, double TOLERANCIA, 
            int CONTADOR_ESTANCAMIENTO, string path, bool guardar_soluciones,
            double TASA_REINICIO, int MAX_REINTENTOS)
        {
            //Inicializar las variables con las definidas por el usuario
            this.NUMERO_PARTICULAS = NUMERO_PARTICULAS;
            this.ITERACIONES = ITERACIONES;

            this.W = W;
            this.C1 = C1;
            this.C2 = C2;

            this.guardar_soluciones = guardar_soluciones;
            this.UMBRAL_MEJORA = TOLERANCIA;
            this.MAX_ESTANCAMIENTO = CONTADOR_ESTANCAMIENTO;

            this.TASA_REINICIO = TASA_REINICIO;
            this.MAX_REINTENTOS = MAX_REINTENTOS;

            // Inicializar el procesamiento de los datos del algoritmo
            PHUB = new Phub();
            bool datos_validos = PHUB.LeerDatos(path);
            if (!datos_validos)
            {
                System.Windows.Forms.MessageBox.Show("El conjunto de datos de entrada no es válido");
                return;
            }

            rnd = new Random();
            soluciones_csv = new StringBuilder();

            soluciones = new StringBuilder();
            mejor_solucion = new StringBuilder();
            tiempo = new TimeSpan();
        }

        public (List<Hub> hubs, double costo) ejecutar()
        {
            // Inicializar las variables relacionadas con la mejor solución
            List<Particula> particulas = new List<Particula>();
            List<int> gBestPosition = new List<int>();

            // Establecer el máximo valor como mejor global (puesto que se trata de minimización)
            gBest = double.MaxValue;

            // Inicializar las variables para el control del estancamiento en las soluciones
            double prev_gBest = gBest;
            int contador_estancamiento = 0;

            int puntos_perturbacion = MAX_ESTANCAMIENTO / MAX_REINTENTOS;
            int particulas_reiniciar = (int)(NUMERO_PARTICULAS * TASA_REINICIO);
            int contador_reintentos = 0;

            for (int i = 0; i < NUMERO_PARTICULAS; i++)
            {
                // Inicializar la partícula y sus características asociadas (posición y velocidad)
                Particula particula = InicializarParticula();

                // Evaluar la primera asignación aleatoria de cada partícula
                var (hubs, cost) = PHUB.FuncionObjetivo(particula.Posicion);
                if (guardar_soluciones) AgregarSolucion(hubs, cost);


                // Ya que se trata de la primera asignación a cada partícula
                // no es necesario verificar si su costo es el mejor local
                particula.PBest = cost;
                particula.MejorPosicion = new List<int>(particula.Posicion);

                // Verificar que si el costo al evaluar la partícula es menor que el mejor global
                if (cost < gBest)
                {
                    // Almacenar las mejores posiciones hasta el momento
                    gBestPosition = new List<int>(particula.Posicion);
                    gBest = cost;
                    gBestHubs = hubs;
                }

                particulas.Add(particula);
            }

            for (int iter = 0; iter < ITERACIONES; iter++)
            {
                W = WMAX - ((double)iter / ITERACIONES) * (WMAX - WMIN);             
                prev_gBest = gBest;

                for (int p = 0; p < NUMERO_PARTICULAS; p++)
                {
                    Particula particula = particulas[p];
                    for (int j = 0; j < PHUB.NUM_HUBS; j++)
                    {
                        double r1 = rnd.NextDouble();
                        double r2 = rnd.NextDouble();

                        // Actualizar la velocidad de la partícula
                        particula.Velocidad[j] = W * particula.Velocidad[j]
                            + C1 * r1 * (particula.MejorPosicion[j] - particula.Posicion[j])
                            + C2 * r2 * (gBestPosition[j] - particula.Posicion[j]);

                        // Actualizar la posición
                        particula.Posicion[j] = Math.Abs(particula.Posicion[j] + (int)particula.Velocidad[j]) % PHUB.NUM_CLIENTES;
                    }

                    // Evaluar la función objetivo, con las nuevas posiciones de la partícula
                    var (hubs, cost) = PHUB.FuncionObjetivo(particula.Posicion);
                    if (guardar_soluciones) AgregarSolucion(hubs, cost);

                    // Verificar si el costo es menor al mejor local
                    if (cost < particula.PBest)
                    {
                        particula.PBest = cost;
                        particula.MejorPosicion = new List<int>(particula.Posicion);
                    }

                    // Verificar si el costo es menor que el mejor global
                    if (cost < gBest)
                    {
                        gBest = cost;
                        gBestPosition = new List<int>(particula.Posicion);
                        gBestHubs = hubs;
                    }
                }

                // Calcular la diferencia relativa entre la solución actual y la anterior
                double delta = Math.Abs(gBest - prev_gBest) / prev_gBest;
                if (delta < UMBRAL_MEJORA) contador_estancamiento++;
                else contador_estancamiento = 0; 

                // Si no se logró alcanzar una mejora en la ejecución durante varias iteraciones
                // finalizar la ejecución
                if (contador_estancamiento >= MAX_ESTANCAMIENTO)
                {
                    break;
                }
                else if (contador_estancamiento > 0 && contador_estancamiento % puntos_perturbacion == 0)
                {
                    particulas.Sort((a, b) => a.PBest.CompareTo(b.PBest));

                    // Reiniciar un porcentaje de las partículas que presentaron peor valor de calidad
                    for (int i = NUMERO_PARTICULAS - particulas_reiniciar; i < NUMERO_PARTICULAS; i++)
                    {
                        particulas[i] = InicializarParticula();
                        var (hubs, cost) = PHUB.FuncionObjetivo(particulas[i].Posicion);

                        // Ya que se trata de la primera asignación a cada partícula
                        // no es necesario verificar si su costo es el mejor local
                        particulas[i].PBest = cost;
                        particulas[i].MejorPosicion = new List<int>(particulas[i].Posicion);

                        if (cost < gBest)
                        {
                            // Almacenar las mejores posiciones hasta el momento
                            gBestPosition = new List<int>(particulas[i].Posicion);
                            gBest = cost;
                            gBestHubs = hubs;
                        }
                    }

                    contador_reintentos++;
                }
            }

            // Transformar la mejor solución global a un formato de salida
            SetMejorSolucion(gBestHubs, gBest);

            return (gBestHubs, gBest);
        }

        public (List<Hub> hubs, double costo, string soluciones_csv) ejecutar_enlistando(int intervalo_guardado, int n_repeticion)
        {
            // Inicializar las variables relacionadas con la mejor solución
            List<Particula> particulas = new List<Particula>();
            List<int> gBestPosition = new List<int>();

            // Establecer el máximo valor como mejor global (puesto que se trata de minimización)
            gBest = double.MaxValue;

            // Inicializar las variables para el control del estancamiento en las soluciones
            double prev_gBest = gBest;
            int contador_estancamiento = 0;

            int puntos_perturbacion = MAX_ESTANCAMIENTO / MAX_REINTENTOS;
            int particulas_reiniciar = (int)(NUMERO_PARTICULAS * TASA_REINICIO);
            int contador_reintentos = 0;

            for (int i = 0; i < NUMERO_PARTICULAS; i++)
            {
                // Inicializar la partícula y sus características asociadas (posición y velocidad)
                Particula particula = InicializarParticula();

                // Evaluar la primera asignación aleatoria de cada partícula
                var (hubs, cost) = PHUB.FuncionObjetivo(particula.Posicion);

                // Ya que se trata de la primera asignación a cada partícula
                // no es necesario verificar si su costo es el mejor local
                particula.PBest = cost;
                particula.MejorPosicion = new List<int>(particula.Posicion);

                // Verificar que si el costo al evaluar la partícula es menor que el mejor global
                if (cost < gBest)
                {
                    // Almacenar las mejores posiciones hasta el momento
                    gBestPosition = new List<int>(particula.Posicion);
                    gBest = cost;
                    gBestHubs = hubs;
                }

                particulas.Add(particula);
            }

            for (int iter = 0; iter < ITERACIONES; iter++)
            {
                W = WMAX - ((double)iter / ITERACIONES) * (WMAX - WMIN);
                prev_gBest = gBest;

                for (int p = 0; p < NUMERO_PARTICULAS; p++)
                {
                    Particula particula = particulas[p];

                    for (int j = 0; j < PHUB.NUM_HUBS; j++)
                    {
                        double r1 = rnd.NextDouble();
                        double r2 = rnd.NextDouble();

                        // Actualizar la velocidad de la partícula
                        particula.Velocidad[j] = W * particula.Velocidad[j]
                            + C1 * r1 * (particula.MejorPosicion[j] - particula.Posicion[j])
                            + C2 * r2 * (gBestPosition[j] - particula.Posicion[j]);

                        // Actualizar la posición
                        particula.Posicion[j] = Math.Abs(particula.Posicion[j] + (int)particula.Velocidad[j]) % PHUB.NUM_CLIENTES;
                    }

                    // Evaluar la función objetivo, con las nuevas posiciones de la partícula
                    var (hubs, cost) = PHUB.FuncionObjetivo(particula.Posicion);

                    // Verificar si el costo es menor al mejor local
                    if (cost < particula.PBest)
                    {
                        particula.PBest = cost;
                        particula.MejorPosicion = new List<int>(particula.Posicion);
                    }

                    // Verificar si el costo es menor que el mejor global
                    if (cost < gBest)
                    {
                        gBest = cost;
                        gBestPosition = new List<int>(particula.Posicion);
                        gBestHubs = hubs;
                    }
                }

                // Almacenar la solución en la variable correspondiente concatenandola en formato csv
                if ((iter + 1) % intervalo_guardado == 0)
                {
                    EnlistarSolucion(n_repeticion, iter + 1, gBest);
                }

                // Calcular la diferencia relativa entre la solución actual y la anterior
                double delta = Math.Abs(gBest - prev_gBest) / prev_gBest;
                if (delta < UMBRAL_MEJORA)
                {
                    contador_estancamiento++;
                } 
                else
                {
                    contador_estancamiento = 0;
                }

                // Si no se logró alcanzar una mejora en la ejecución durante varias iteraciones
                // finalizar la ejecución
                if (contador_estancamiento >= MAX_ESTANCAMIENTO)
                {
                    break;
                }
                else if (contador_estancamiento > 0 && contador_estancamiento % puntos_perturbacion == 0)
                {
                    particulas.Sort((a, b) => a.PBest.CompareTo(b.PBest));

                    // Reiniciar un porcentaje de las partículas que presentaron peor valor de calidad
                    for (int i = NUMERO_PARTICULAS - particulas_reiniciar;  i < NUMERO_PARTICULAS; i++)
                    {
                        particulas[i] = InicializarParticula();
                        var (hubs, cost) = PHUB.FuncionObjetivo(particulas[i].Posicion);

                        // Ya que se trata de la primera asignación a cada partícula
                        // no es necesario verificar si su costo es el mejor local
                        particulas[i].PBest = cost;
                        particulas[i].MejorPosicion = new List<int>(particulas[i].Posicion);

                        if (cost < gBest)
                        {
                            // Almacenar las mejores posiciones hasta el momento
                            gBestPosition = new List<int>(particulas[i].Posicion);
                            gBest = cost;
                            gBestHubs = hubs;
                        }
                    }

                    contador_reintentos++;
                }
            }

            // Transformar la mejor solución global a un formato de salida
            SetMejorSolucion(gBestHubs, gBest);

            return (gBestHubs, gBest, soluciones_csv.ToString());
        }

        private Particula InicializarParticula()
        {
            Particula particula = new Particula();

            for (int j = 0; j < PHUB.NUM_HUBS; j++)
            {
                int indice;
                do
                {
                    indice = rnd.Next(PHUB.NUM_CLIENTES);
                } while (particula.Posicion.Contains(indice));

                particula.Posicion.Add(indice);

                //particula.Velocidad.Add(rnd.NextDouble() * 2 - 1);
                particula.Velocidad.Add(rnd.NextDouble() * PHUB.NUM_CLIENTES / 2 - PHUB.NUM_CLIENTES / 4);
            }

            return particula;
        }

        private void EnlistarSolucion(int n_repeticion, int i, double costoTotal)
        {
            soluciones_csv.AppendLine($"Repeticion{n_repeticion};{i};{costoTotal}");
        }

        private void AgregarSolucion(List<Hub> hubs, double costoTotal)
        {
            soluciones.AppendLine("\nAsignación actual de clientes a hubs y distancias:");

            for (int i = 0; i < hubs.Count; i++)
            {
                var hub = hubs[i];
                soluciones.AppendLine($"\nHub {i + 1} (x = {hub.Cliente.X}, y = {hub.Cliente.Y}): Demanda acumulada = {hub.Demanda_acumulada}");

                for (int j = 0; j < hub.Clientes.Count; j++)
                {
                    Cliente cliente = hub.Clientes[j];
                    soluciones.AppendLine($"  Cliente en (x = {cliente.X}, y = {cliente.Y}), demanda = {cliente.Demanda}, distancia = {hub.Distancias[j]:F2}");
                }
                //foreach (var cliente in hub.clientes)
                //{
                //    soluciones.AppendLine($"  Cliente en (x = {cliente.X}, y = {cliente.Y}), demanda = {cliente.Demanda}, distancia = {distancia:F2}");
                //}
            }

            soluciones.AppendLine($"\nCosto total: {costoTotal:F2}");
        }

        private void SetMejorSolucion(List<Hub> hubs, double costo)
        {
            mejor_solucion = new StringBuilder();
            mejor_solucion.AppendLine("Mejor solución encontrada:");
            mejor_solucion.AppendLine("Clientes seleccionados como hubs:");
            mejor_solucion.AppendLine("...............................................................................");

            mejor_solucion.AppendLine("\nAsignación actual de clientes a hubs y distancias:");

            for (int i = 0; i < hubs.Count; i++)
            {
                var hub = hubs[i];
                mejor_solucion.AppendLine($"Hub {i + 1} (x = {hub.Cliente.X}, y = {hub.Cliente.Y}): Demanda = {hub.Cliente.Demanda}");
            }

            mejor_solucion.AppendLine($"\nCosto total de distancia mínima calculada: {costo:F2}");

            mejor_solucion.AppendLine("\nAsignación de clientes a hubs y distancias:");

            for (int i = 0; i < hubs.Count; i++)
            {
                var hub = hubs[i];
                mejor_solucion.AppendLine($"\nHub {i + 1} (x = {hub.Cliente.X}, y = {hub.Cliente.Y}): Demanda acumulada = {hub.Demanda_acumulada}");

                for (int j = 0; j < hub.Clientes.Count; j++)
                {
                    Cliente cliente = hub.Clientes[j];
                    mejor_solucion.AppendLine($"  Cliente en (x = {cliente.X}, y = {cliente.Y}), demanda = {cliente.Demanda}, distancia = {hub.Distancias[j]:F2}");
                }
            }
        }

        public string GetMejorSolucion(TimeSpan tiempo)
        {
            string num_clientes = FormatearNumero(PHUB.NUM_CLIENTES);
            string num_hubs = FormatearNumero(PHUB.NUM_HUBS);
            return String.Format("{0}; {1}; {2}", num_clientes, num_hubs, tiempo.TotalMilliseconds.ToString());
        }

        public static string FormatearNumero(int numero)
        {
            if (numero >= 1000000)
                return (numero / 1000000.0f).ToString("0.##") + "M";
            if (numero >= 1000)
                return (numero / 1000.0f).ToString("0.##") + "K";
            return numero.ToString();
        }
    }
}
