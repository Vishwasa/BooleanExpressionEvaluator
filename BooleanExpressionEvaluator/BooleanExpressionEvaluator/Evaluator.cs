using BooleanExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanExpressionEvaluator
{
    class Evaluator
    {
        private IEnumerator<IKeysAndSymbols> _tokens;
        private IDictionary<string, int> dictOfInputVariablesAndValues;
        private IList<IKeysAndSymbols> listOfBoolenEvaluateTokens;
        private IEnumerator<IKeysAndSymbols> _booleanTokens;
        public Evaluator(IEnumerable<IKeysAndSymbols> tokens)
        {
            _tokens = tokens.GetEnumerator();
            _tokens.MoveNext();
        }
        public Evaluator(IEnumerable<IKeysAndSymbols> tokens, Dictionary<string, int> dictOfInputVariablesAndValues) : this(tokens)
        {
            this.dictOfInputVariablesAndValues = dictOfInputVariablesAndValues;
            _tokens = tokens.GetEnumerator();
            _tokens.MoveNext();
        }

        public bool evaluateExpression()
        {
            //for (int i=0; i<logicalKeys.Count; i++)
            //{
            //    if (logicalKeys[0] is LEFTOPENPARANTHESIS)
            //        evaluateExpression(logicalKeys);
            //}
            //List<IKeysAndSymbols> listOfBoolenEvaluateTokens = new List<IKeysAndSymbols>();
            listOfBoolenEvaluateTokens = new List<IKeysAndSymbols>();
            while (_tokens.Current != null)
            {
                var isVariable = _tokens.Current is VariablesToken;
                if (!isVariable)
                {
                    listOfBoolenEvaluateTokens.Add(_tokens.Current);
                    _tokens.MoveNext();
                    continue;
                }
                var resultantBoolean = false;
                if (isVariable)
                {
                    resultantBoolean = ParseArithmaticComparisionExpression();
                }
                if (resultantBoolean)
                {
                    listOfBoolenEvaluateTokens.Add(new TrueToken());
                }
                else
                {
                    listOfBoolenEvaluateTokens.Add(new FalseToken());
                }
                _tokens.MoveNext();
            }
            //var _booleanTokens = listOfBoolenEvaluateTokens.GetEnumerator();
            if (listOfBoolenEvaluateTokens.Count == 1)
            {
                return listOfBoolenEvaluateTokens[0] is TrueToken? true:false;
            }
            _booleanTokens = listOfBoolenEvaluateTokens.GetEnumerator();
            _booleanTokens.MoveNext();
            while (_booleanTokens.Current != null)
            {
                var isNegated = _booleanTokens.Current is NEGATION;
                if (isNegated)
                    _booleanTokens.MoveNext();
                //var isVariable = _tokens.Current is VariablesToken;
                //var resultantBoolean = false;
                //if (isVariable)
                //{
                //    resultantBoolean = ParseArithmaticComparisionExpression();
                //}
                var boolean = this.ParseBoolean();
                if (isNegated)
                    boolean = !boolean;
                
                while (_booleanTokens.Current is AND || _booleanTokens.Current is OR)
                {
                    var operand = _booleanTokens.Current;
                    if (!_booleanTokens.MoveNext())
                    {
                        throw new Exception("Missing expression after operand");
                    }
                    var nextBoolean = this.ParseBoolean();

                    if (operand is AND)
                        boolean = boolean && nextBoolean;
                    else if (operand is OR)
                        boolean = boolean || nextBoolean;
                }
                return boolean;

            }
            throw new Exception("Empty expression");
        }
        private bool ParseBoolean()
        {
            if (_booleanTokens.Current is BooleanValueToken)
            {
                var current = _booleanTokens.Current;
                _booleanTokens.MoveNext();
                //VariablesToken s = new VariablesToken("s");
                if (current is TrueToken)
                    return true;
                return false;
            }
            if (_booleanTokens.Current is LEFTOPENPARANTHESIS)
            {
                _booleanTokens.MoveNext();

                var expInPars = ParseBoolean();

                //if (!(_booleanTokens.Current is RIGHCLOSETPARANTHESIS))
                //    throw new Exception("Expecting Closing Parenthesis");

                _booleanTokens.MoveNext();

                return expInPars;
            }
            if (_booleanTokens.Current is RIGHCLOSETPARANTHESIS)
                throw new Exception("Unexpected Closed Parenthesis");

            // since its not a BooleanConstant or Expression in parenthesis, it must be a expression again
            var val = evaluateExpression();
            return val;
        }



        private bool ParseArithmaticComparisionExpression()
        {
            //List<IKeysAndSymbols> tempTokens = new List<IKeysAndSymbols>();
            var currentToken = _tokens.Current;
            _tokens.MoveNext();
            var NextPossibleOperand = _tokens.Current;
            _tokens.MoveNext();
            var NextToken = _tokens.Current;
            int firstVariableValue = 0;
            int secondVariableValue = 0;
            if(currentToken is NumberToken)
            {
                var numToken = (NumberToken)currentToken;
                firstVariableValue = int.Parse(numToken.Value);
            }
            if (NextToken is NumberToken)
            {
                var numToken = (NumberToken)NextToken;
                secondVariableValue = int.Parse(numToken.Value);
            }
            if (currentToken is VariablesToken)
            {
                var usedVariables = currentToken as VariablesToken;
                
                if (dictOfInputVariablesAndValues.ContainsKey(usedVariables.Name))
                {
                    firstVariableValue = dictOfInputVariablesAndValues[usedVariables.Name];
                }
                else
                {
                    throw new Exception($"Value for this variable not found {usedVariables.Name}");
                }
            }
            if (NextToken is VariablesToken)
            {
                var usedVariables = NextToken as VariablesToken;
                if (dictOfInputVariablesAndValues.ContainsKey(usedVariables.Name))
                {
                    secondVariableValue = dictOfInputVariablesAndValues[usedVariables.Name];
                }
                else
                {
                    throw new Exception($"Value for this variable not found {usedVariables.Name}");
                }
            }
            if (NextPossibleOperand is GREATERTHAN)
            {
                return firstVariableValue > secondVariableValue;
            }
            else if (NextPossibleOperand is LESSTHAN)
            {
                return firstVariableValue < secondVariableValue;
            }
            else if (NextPossibleOperand is EQUALS)
            {
                return firstVariableValue == secondVariableValue;
            }
            return false;
        }
    }
}
