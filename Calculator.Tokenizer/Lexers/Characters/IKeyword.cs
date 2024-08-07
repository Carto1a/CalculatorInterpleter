using Calculator.Tokenizer.Tokens;

namespace Calculator.Tokenizer.Lexers.Characters;
public interface IKeyword
{
    IToken ToToken(int characterNumber, IToken previusToken, LexerContext context);
    void Analyze(IToken token, IToken? previusToken);
}