using Calculator.Tokenizer.Tokens.Mathematic.Signals;

namespace Calculator.Tokenizer.Tokens.Mathematic.Scopes;
public class TokenOpenScope : TokenScope, ICanHaveSignal
{
    public TokenOpenScope() { }

    public void Add(IToken token)
    {
        if (Scope is null)
            throw new InvalidOperationException("Scope is not set");

        Scope.Add(token);
    }
}