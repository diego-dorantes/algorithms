using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    internal static class PrefixSum
    {
        static public void Prefix(int[] array)
        {
            int[] prefixSumArray = array;

            for(int i = 1; i < prefixSumArray.Length; i++)
            {
                prefixSumArray[i] = prefixSumArray[i - 1] + prefixSumArray[i];
            }
        }
    }
}
