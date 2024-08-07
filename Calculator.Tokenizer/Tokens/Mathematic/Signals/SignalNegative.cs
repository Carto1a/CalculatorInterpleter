using Calculator.Tokenizer.Tokens.Mathematic.Scopes;

namespace Calculator.Tokenizer.Tokens.Mathematic.Signals;
public class SignalNegative : TokenSignal
{
    public SignalNegative() { }

    public override string ToString()
    {
        if (NextToken is TokenNumber)
            return "-";

        return "- ";
    }
}