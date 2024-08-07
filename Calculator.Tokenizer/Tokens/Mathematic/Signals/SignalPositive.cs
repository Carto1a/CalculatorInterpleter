using Calculator.Tokenizer.Tokens.Mathematic.Scopes;

namespace Calculator.Tokenizer.Tokens.Mathematic.Signals;
public class SignalPositive : TokenSignal
{
    public SignalPositive() { }

    public override string ToString()
    {
        if (NextToken is TokenNumber)
            return "+";

        return "+ ";
    }
}