namespace Calculator.Tokenizer.Tokens.Mathematic.Signals;
public class SignalNegative : TokenSignal
{
    public SignalNegative() { }
    public SignalNegative(TokenNumber value) : base(value) { }

    public override string ToString()
    {
        if (NextToken is TokenNumber)
            return "-";

        return "- ";
    }
}
