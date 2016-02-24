using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dydutility
{
    static class RandomSingleton
    {
        private static Random random = new Random();

        public static int Next(int minValue, int exclusiveMaxValue)
        {
            return random.Next(minValue, exclusiveMaxValue);
        }
    }
}
