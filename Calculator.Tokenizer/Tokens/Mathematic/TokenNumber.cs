using Calculator.Tokenizer.Tokens.Mathematic.Signals;

namespace Calculator.Tokenizer.Tokens.Mathematic;
public class TokenNumber : Token, ICanHaveSignal
{
    public decimal Value { get; }

    public TokenNumber(decimal value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value} ";
    }
}