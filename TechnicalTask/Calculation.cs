using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask
{
    public class Calculation
    {
        public double Mediana(List<int> Numbers)
        {
            int Lenght = Numbers.Count()/2;
            if (Lenght % 2 == 0)
            {
                return (Numbers[Lenght] + Numbers[Lenght + 1]) / 2;
            }
            else
                return Numbers[Lenght];
        }
    }
}
