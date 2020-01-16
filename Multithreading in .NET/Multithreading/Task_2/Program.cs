using System;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(OperationWithArray.CreateArray)
                .ContinueWith(antecedent => OperationWithArray.MultipliesArray(antecedent.Result))
                .ContinueWith(antecedent => OperationWithArray.SortArray(antecedent.Result))
                .ContinueWith(antecedent => OperationWithArray.CalculateAverageValue(antecedent.Result));

            Console.ReadKey();
        }
    }
}
