using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryExpressionEvaluator
{
    class ParseExpression
    {
        private StringReader _reader;
        private string _exp;
        public ParseExpression(string exp)
        {
            _exp = exp;
            _reader = new StringReader(_exp);
        }
        public Tuple<IEnumerable<IKeysAndSymbols>, IEnumerable<string>> ParsekeysAndVariables()
        {
            var keys = new List<IKeysAndSymbols>();
            var variables = new List<string>();
            while (_reader.Peek() != -1)
            {
                var c = (char)_reader.Peek();
                switch (c)
                {
                    case '!':
                        keys.Add(new NEGATION());
                        _reader.Read();
                        break;
                    case '|':
                        _reader.Read();
                        if (_reader.Peek() != -1 && (char)_reader.Peek() == '|')
                            keys.Add(new OR());
                        break;
                    case '&':
                        _reader.Read();
                        if (_reader.Peek() != -1 && (char)_reader.Peek() == '&')
                            keys.Add(new AND());
                        break;
                    case '(':
                        keys.Add(new LEFTOPENPARANTHESIS());
                        _reader.Read();
                        break;
                    case ')':
                        keys.Add(new RIGHCLOSETPARANTHESIS());
                        _reader.Read();
                        break;
                    case '>':
                        keys.Add(new GREATERTHAN());
                        _reader.Read();
                        break;
                    case '<':
                        keys.Add(new LESSTHAN());
                        _reader.Read();
                        break;
                    case '=':
                        _reader.Read();
                        if (_reader.Peek() != -1 && (char)_reader.Peek() == '=')
                            keys.Add(new EQUALS());
                        else
                            keys.Add(new ASSIGNMENTEQUALS());
                        break;
                    default:
                        if (Char.IsLetter(c))
                        {
                            var variable = parseVariables();
                            variables.Add(variable);
                        }
                        else if(char.IsNumber(c))
                        {
                            _reader.Read();
                        }
                        else
                        {
                            throw new InvalidDataException();
                        }
                        break;
                }
            }
            return new Tuple<IEnumerable<IKeysAndSymbols>, IEnumerable<string>>(keys, variables);
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
    }
}
