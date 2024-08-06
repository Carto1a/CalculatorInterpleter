namespace Calculator.Tokenizer.Tokens.Mathematic;
public class TokenNumber : Token
{
    public decimal Value { get; }

    public TokenNumber(decimal value)
    {
        Value = value;
    }

    public TokenNumber(decimal value, Token? next)
    : base(next)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value.ToString()} ";
    }
}
