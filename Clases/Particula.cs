using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo_PSO_Problema_PHUB
{
    class Particula
    {
        public List<int> Posicion { get; set; }
        public List<double> Velocidad { get; set; }
        public List<int> MejorPosicion { get; set; }
        public double PBest { get; set; }

        public Particula()
        {
            Posicion = new List<int>();
            MejorPosicion = new List<int>();
            Velocidad = new List<double>();
            PBest = double.MaxValue;
        }
    }
}
