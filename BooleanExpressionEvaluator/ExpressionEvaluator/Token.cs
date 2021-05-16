using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator
{
    class Token
    {
        public Token(bool _isOperand)
        {
            IsOperand = _isOperand;
            //Precedence = getPrecedence(
        }
        public Token(bool _isOperand, operators _operator, short _precedence)
        {
            IsOperand = _isOperand;
            Operator = _operator;
            Precedence = _precedence;
        }
        public bool IsOperand { get; set; }
        public operators Operator { get; set; }
        public short Precedence { get; set; }
        public string VariableName { get; set; }
        public int Value { get; set; }
    }
    enum operators
    {
        not,
        and,
        nand,
        or,
        nor,
        xor,
        xnor,
        plus,
        minus,
        multiply,
        divide,
        exponent,
        leftParenthis,
        rightParenthis,
        greaterthan,
        lessthan,
        equalto,
        variableName,
        Number
    }
}
