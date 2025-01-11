using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo_PSO_Problema_PHUB
{
    class Hub
    {
        // Cliente se refiere al cliente utilizado como hub
        public Cliente Cliente { get; }
        public double Demanda_acumulada { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<double> Distancias { get; set; }

        public Hub(Cliente cliente)
        {
            this.Cliente = cliente;
            this.Demanda_acumulada = 0;

            this.Clientes = new List<Cliente>();
            this.Distancias = new List<double>();
        }
    }
}
