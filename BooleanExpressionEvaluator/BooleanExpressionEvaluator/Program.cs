using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanExpressionEvaluator
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
            var KeysAndVars = keysAndVars.Item1;
            var variables = keysAndVars.Item2;
            foreach (var item in variables)
            {
                if (!dictOfInputVariablesAndValues.ContainsKey(item))
                {
                    dictOfInputVariablesAndValues.Add(item,0);
                }
            }
            foreach(var key in dictOfInputVariablesAndValues.Keys.ToList())
            {
                Console.WriteLine($"please enter value for {key}");
                var input = Console.ReadLine();
                int res;
                if (int.TryParse(input, out res))
                    dictOfInputVariablesAndValues[key] = res;
                else
                    Console.WriteLine($"{input} is not valid"); //Need to provide retry options
            }
            Evaluator eval = new Evaluator(KeysAndVars, dictOfInputVariablesAndValues);
            
            var output = eval.evaluateExpression();
            Console.WriteLine($"res:{output}");
            Console.ReadLine();
        }
    }
}
