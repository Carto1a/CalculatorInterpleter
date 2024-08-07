using Calculator.Tokenizer.Tokens;
using Calculator.Tokenizer.Tokens.Mathematic.Operators;
using Calculator.Tokenizer.Tokens.Mathematic.Signals;

namespace Calculator.Tokenizer.Lexers.Characters;
public class KeywordMinus : IKeyword
{
    public void Analyze(
        IToken token, IToken? previusToken)
    {
        throw new NotImplementedException();
    }

    public IToken ToToken(
        int character, IToken previusToken, LexerContext context)
    {
        if (previusToken is TokenOperator or null)
        {
            return new SignalNegative();
        }
        else
        {
            return new OperatorSub(previusToken);
        }
    }
}