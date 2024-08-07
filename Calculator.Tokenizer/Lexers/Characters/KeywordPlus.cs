using Calculator.Tokenizer.Tokens;
using Calculator.Tokenizer.Tokens.Mathematic.Operators;
using Calculator.Tokenizer.Tokens.Mathematic.Signals;

namespace Calculator.Tokenizer.Lexers.Characters;
public class KeywordPlus : IKeyword
{
    public void Analyze(IToken token, IToken? previusToken)
    {
        throw new NotImplementedException();
    }

    public IToken ToToken(
        int characterNumber, IToken previusToken, LexerContext context)
    {
        if (previusToken is TokenOperator or null)
        {
            return new SignalPositive();
        }
        else
        {
            return new OperatorAdd(previusToken);
        }
    }
}