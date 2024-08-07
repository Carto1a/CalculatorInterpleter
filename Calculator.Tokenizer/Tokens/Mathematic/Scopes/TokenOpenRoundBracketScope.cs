using Calculator.Tokenizer.Tokens.Mathematic.Signals;

namespace Calculator.Tokenizer.Tokens.Mathematic.Scopes;
public class TokenOpenRoundBracketScope : TokenOpenScope, ICanHaveSignal
{
    public TokenOpenRoundBracketScope() { }

    public override string ToString()
    {
        return "( ";
    }
}