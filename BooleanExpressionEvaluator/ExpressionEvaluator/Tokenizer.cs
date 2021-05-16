using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator
{

    class Tokenizer
    {
        private string _inputString;
        private StringReader _reader;
        private IEnumerator<string> _tokens;
        public Tokenizer(string expression)
        {
            _inputString = expression;
            _reader = new StringReader(_inputString);
        }
        public IEnumerable<Token> Tokenize()
        {
            var tokens = new List<Token>();
            var variables = new List<string>();
            while (_reader.Peek() != -1)
            {
                var tokenObj = new Token(true);
                var c = (char)_reader.Peek();
                switch (c)
                {
                    case '!':
                        tokenObj.Operator = operators.not;
                        tokenObj.Precedence = 1;
                        tokens.Add(tokenObj);
                        _reader.Read();
                        break;
                    case '|':
                        _reader.Read();
                        if (_reader.Peek() != -1 && (char)_reader.Peek() == '|')
                        {
                            tokenObj.Operator = operators.or;
                            tokenObj.Precedence = 1;
                            tokens.Add(tokenObj);
                        }
                        break;
                    case '&':
                        _reader.Read();
                        if (_reader.Peek() != -1 && (char)_reader.Peek() == '&')
                        {
                            tokenObj.Operator = operators.and;
                            tokenObj.Precedence = 1;
                            tokens.Add(tokenObj);
                        }
                        break;
                    case '(':
                        tokenObj.Operator = operators.leftParenthis;
                        tokenObj.Precedence = 1;
                        tokens.Add(tokenObj);
                        _reader.Read();
                        break;
                    case ')':
                        tokenObj.Operator = operators.rightParenthis;
                        tokenObj.Precedence = 1;
                        tokens.Add(tokenObj);
                        _reader.Read();
                        break;
                    case '>':
                        tokenObj.Operator = operators.greaterthan;
                        tokenObj.Precedence = 1;
                        tokens.Add(tokenObj);
                        _reader.Read();
                        break;
                    case '<':
                        tokenObj.Operator = operators.lessthan;
                        tokenObj.Precedence = 1;
                        tokens.Add(tokenObj);
                        _reader.Read();
                        break;
                    case '=':
                        _reader.Read();
                        if (_reader.Peek() != -1 && (char)_reader.Peek() == '=')
                        {
                            tokenObj.Operator = operators.equalto;
                            tokenObj.Precedence = 1;
                            tokens.Add(tokenObj);
                        }
                        else
                            tokenObj.Operator = operators.equalto;
                            tokenObj.Precedence = 1;
                            tokens.Add(tokenObj);
                        break;
                    default:
                        if (Char.IsLetter(c))
                        {
                            var variable = parseVariables();
                            tokenObj.IsOperand = false;
                            tokenObj.Operator = operators.variableName;
                            tokenObj.VariableName = variable;
                            tokenObj.Precedence = 1;
                            tokens.Add(tokenObj);
                            //variables.Add(variable);
                            //tokens.Add(new VariablesToken(variable));
                        }
                        else if (char.IsNumber(c))
                        {
                            var number = parseNumbers();
                            tokenObj.Operator = operators.Number;
                            tokenObj.Value = int.Parse(number);
                            tokenObj.Precedence = 1;
                            tokens.Add(tokenObj);
                            //Console.WriteLine($"Parsed Integer {0}");
                            //tokens.Add(new NumberToken(number));
                        }
                        else
                        {
                            throw new InvalidDataException();
                        }
                        break;
                }
            }
            return tokens;
            //return new List<string>(){"vishwas","vijay" };
        }
        private string parseVariables()
        {
            var text = new StringBuilder();
            while (Char.IsLetter((char)_reader.Peek()))
            {
                text.Append((char)_reader.Read());
            }

            var potentialVariable = text.ToString().ToLower();

            //Console.WriteLine($"PotentialVariable : {potentialVariable}");
            return potentialVariable;
        }
        private string parseNumbers()
        {
            var text = new StringBuilder();
            while (Char.IsNumber((char)_reader.Peek()))
            {
                text.Append((char)_reader.Read());
            }

            var potentialNumber = text.ToString().ToLower();

            //Console.WriteLine($"PotentialVariable : {potentialVariable}");
            return potentialNumber;
        }
    }
}
