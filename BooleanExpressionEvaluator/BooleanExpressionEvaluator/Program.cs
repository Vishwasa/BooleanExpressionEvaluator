using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryExpressionEvaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictOfInputVariablesAndValues = new Dictionary<string, int>();
            Console.WriteLine("Please Enter the expression to be evaluated");
            string ExpressoinString = Console.ReadLine();
            var parseExp = new ParseExpression(ExpressoinString);
            var keysAndVars = parseExp.ParsekeysAndVariables();
            //ProvideValues for variables
            var Keys = keysAndVars.Item1;
            var variables = keysAndVars.Item2;
            foreach(string i in variables)
            {
                Console.WriteLine($"please enter value for {i}");
                var input = Console.ReadLine();
                int res;
                if (int.TryParse(input, out res))
                    dictOfInputVariablesAndValues.Add(i, res);
                else
                    Console.WriteLine($"{input} is not valid"); //Need to provide retry options
            }
        }
    }
}
