namespace Calculator.Tokenizer.Tokens.Mathematic;
public class TokenValue : TerminalToken
{
    public decimal Value { get; }

    public TokenValue(decimal value)
    {
        Value = value;
    }
}
