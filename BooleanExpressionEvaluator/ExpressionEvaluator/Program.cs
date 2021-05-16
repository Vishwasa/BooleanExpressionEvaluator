using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator
{
    class Program
    {
        public static Stack<Token> OperatorStack { get; set; }
        public static Stack<Token> ValueStack { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter the expression to be evaluated");
            string ExpressoinString = Console.ReadLine();
            //Steps: Tokenize
            var tokenizer = new Tokenizer(ExpressoinString);
            var Tokens = tokenizer.Tokenize();
            //Convert the Infix expression in Tokens to PostFix expression using stack
            InfixToPostFix(Tokens);

            //Evaluate the expression
        }

        private static void InfixToPostFix(IEnumerable<Token> tokens)
        {
            foreach (var token in tokens)
            {
                if (token.IsOperand==true)
                {
                    PushToOperatorStack(token);
                }
                else
                {
                    PushToValueStack(token);
                }
            }
        }

        private static void PushToValueStack(Token token)
        {
            ValueStack.Push(token);
        }

        private static void PushToOperatorStack(Token token)
        {
            //CheckIfStackHas already a operator, check precedence
            var precedence = CheckPrecedence(token);
            //If precedence is higher then add to stack
            if (precedence)
            {
                OperatorStack.Push(token);
            }
            //if Precedence is same or lower, then pop the opertor till the stack is empty
            else
            {
                PopAndEvaluate(OperatorStack.Pop());
            }
        }

        private static void PopAndEvaluate(Token token)
        {
            var value1 = ValueStack.Pop();
            var value2 = ValueStack.Pop();
            ValueStack.Push(Evaluate(value1, value2, token));
        }

        private static Token Evaluate(Token value1, Token value2, Token token)
        {
            throw new NotImplementedException();
        }

        private static bool CheckPrecedence(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
