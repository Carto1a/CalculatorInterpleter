using Calculator.Tokenizer.Tokens;
using Calculator.Tokenizer.Tokens.Mathematic.Scopes;

namespace Calculator.Tokenizer.Lexers.Characters;
public class KeywordCloseRoundBracket : IKeyword
{
    public void Analyze(IToken token, IToken? previusToken)
    {
        throw new NotImplementedException();
    }

    public IToken ToToken(int characterNumber, IToken? previusToken, LexerContext context)
    {
        return new TokenCloseRoundBracketScope();
    }
}