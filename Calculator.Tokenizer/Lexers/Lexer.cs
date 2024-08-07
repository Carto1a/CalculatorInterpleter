using System.Text;

using Calculator.Tokenizer.Lexers.Characters;
using Calculator.Tokenizer.Lexers.Exceptions;
using Calculator.Tokenizer.Tokens;

namespace Calculator.Tokenizer.Lexers;
public class Lexer
{
    private readonly LexerContext _context;

    private static Dictionary<char, IKeyword> _keywords = new()
    {
        { '+', new KeywordPlus() },
        { '-', new KeywordMinus() },
        { '(', new KeywordOpenRoundBracket() },
        { ')', new KeywordCloseRoundBracket() }
    };

    public Lexer(Stream input)
    {
        _context = new LexerContext(input);
        _keywords.Add('.', new KeywordNumber());
        _keywords.Add(',', new KeywordNumber());

        for(int i = LexerContext.ASCII_NUMBER_0; i <= LexerContext.ASCII_NUMBER_9; i++)
        {
            _keywords.Add((char)i, new KeywordNumber());
        }
    }

    public string Untokenize(IToken root)
    {
        var buffer = new StringBuilder();
        var current = root;

        while (current != null)
        {
            _ = buffer.Append(current.ToString());
            current = current.NextToken;
        }

        return buffer.ToString();
    }

    public string UntokenizeRecursive(IToken? root, StringBuilder? buffer = null)
    {
        buffer ??= new StringBuilder();
        if (root == null)
        {
            return buffer.ToString();
        }

        return UntokenizeRecursive(root.NextToken, buffer.Append(root.ToString()));
    }

    // 1 + 2 + 3: good work
    // -1 + 2 + 3: good work
    // -1 + +2 + 3: good work
    //
    // -1 + (-2) + 3: good no
    // -1 ( 2 + 2 ) + 3: good no
    // - ( 1 + 2 ) + 3: good no
    private IToken? Tokenizer(int input, IToken? previusToken = null)
    {
        IToken? token = null;

        while (input != -1)
        {
            char currentChar = (char)input;
            if (currentChar == ' ')
            {
                _context.IncrementWhiteSpaceCounter();

                if (_context.IsTooManyWhiteSpaces())
                    throw new LexerTooManySpacesException();

                input = _context.NextCharacter();
                continue;
            }
            else
            {
                _context.ResetWhiteSpaceCounter();
            }


            {
                throw new LexerInvalidTokenException("Character is a invalid token");
            }

            previusToken = token;
            input = NextCharacter();
            continue;
        }

        return token;
    }

    public IToken InitializerTokenization()
    {
        var character = _reader.Read();
        if (character == -1) throw new LexerNoDataException();

        var root = Tokenizer(character);
        if (root == null) throw new LexerEmptyTreeException();

        if (_tokenList.Count == 0) throw new LexerEmptyTreeException();

        return _tokenList.Head!;
    }
}