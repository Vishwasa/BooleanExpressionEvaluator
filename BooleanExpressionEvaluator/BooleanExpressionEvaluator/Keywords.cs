namespace BinaryExpressionEvaluator
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

}