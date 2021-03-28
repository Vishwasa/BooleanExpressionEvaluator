namespace BooleanExpressionEvaluator
{
    public interface IKeysAndSymbols
    {
    }
    class NEGATION : IKeysAndSymbols //!
    {
    }
    class OR : IKeysAndSymbols //||
    {
    }
    class AND:IKeysAndSymbols //&&
    {
    }
    class LEFTOPENPARANTHESIS : IKeysAndSymbols //(
    {

    }
    class RIGHCLOSETPARANTHESIS : IKeysAndSymbols { } //)

    class GREATERTHAN : IKeysAndSymbols { } //>
    class LESSTHAN : IKeysAndSymbols { }  //<
    class EQUALS : IKeysAndSymbols { } //==
    class ASSIGNMENTEQUALS : IKeysAndSymbols { } //=
    class VariablesToken : IKeysAndSymbols
    {
        private string _name;
        public VariablesToken(string name)
        {
            _name = name;
        }
        public string Name { get { return _name; } }
    }

    class NumberToken : IKeysAndSymbols
    {
        private string _value;
        public NumberToken(string value)
        {
            _value = value;
        }
        public string Value { get { return _value; } }
    }
    public class BooleanValueToken : IKeysAndSymbols
    {

    }

    public class FalseToken : BooleanValueToken
    {
    }

    public class TrueToken : BooleanValueToken
    {
    }

}