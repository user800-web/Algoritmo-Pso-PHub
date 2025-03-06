using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo_PSO_Problema_PHUB
{
    public class Cliente
    {
        public int ID { get; }
        public double X { get; }
        public double Y { get; }
        public int Demanda { get; }

        public Cliente(int id, double x, double y, int demanda)
        {
            this.ID = id;
            X = x;
            Y = y;
            Demanda = demanda;
        }
    }
}
