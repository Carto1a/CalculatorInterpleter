using Calculator.Tokenizer.Tokens;

namespace Calculator.Tokenizer.Lexers;
public class LexerContext
{
    public static int MIN_SCOPE_LEVEL = 0;
    public static int ASCII_NUMBER_0 = 48;
    public static int ASCII_NUMBER_9 = 57;

    public int Count => _tokenList.Count;
    public IToken? Head => _tokenList.Head;

    private readonly TokenList _tokenList = new();
    private readonly StreamReader _reader;

    private int _whiteSpaceCounter;
    private int _atualScopeLevel = -1;

    public LexerContext(Stream input)
    {
        _reader = new StreamReader(input);
        _whiteSpaceCounter = 0;
    }

    public static bool IsNumber(char character)
    {
        return (character >= LexerContext.ASCII_NUMBER_0 && character <= LexerContext.ASCII_NUMBER_9)
             || character == '.';
    }

    public int NextCharacter()
    {
        return _reader.Read();
    }

    public void ResetWhiteSpaceCounter()
    {
        _whiteSpaceCounter = 0;
    }

    public void IncrementWhiteSpaceCounter()
    {
        _whiteSpaceCounter++;
    }

    public bool IsTooManyWhiteSpaces()
    {
        return _whiteSpaceCounter > 2;
    }

    public void AddToken(IToken token)
    {
        _tokenList.Add(token);
    }
}