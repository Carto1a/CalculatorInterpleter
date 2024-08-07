using System.Text;
using Calculator.Tokenizer.Tokens;
using Calculator.Tokenizer.Tokens.Mathematic;

namespace Calculator.Tokenizer.Lexers.Characters;
public class KeywordNumber : IKeyword
{
    public IToken ToToken(
        int characterNumber, IToken previusToken, LexerContext context)
    {
        var stringBuilder = new StringBuilder();
        var nextChar = characterNumber;

        while (characterNumber != -1 && LexerContext.IsNumber((char)nextChar))
        {
            _ = stringBuilder.Append((char)nextChar);
            nextChar = context.NextCharacter();
        }

        var stringNumber = stringBuilder.ToString();
        var number = decimal.Parse(stringNumber);

        return new TokenNumber(number);
    }

    public void Analyze(IToken token, IToken? previusToken)
    {
        throw new NotImplementedException();
    }
}