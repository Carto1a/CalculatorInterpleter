namespace Calculator.Tokenizer.Tokens.Mathematic.Signals;
public class SignalPositive : TokenSignal
{
    public SignalPositive() { }
    public SignalPositive(TokenNumber value) : base(value) { }

    public override string ToString()
    {
        if (NextToken is TokenNumber)
            return "+";

        return "+ ";
    }
}
