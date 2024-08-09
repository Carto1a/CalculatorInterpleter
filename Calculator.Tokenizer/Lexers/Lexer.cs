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
        { '0', new KeywordNumber() },
        { '1', new KeywordNumber() },
        { '2', new KeywordNumber() },
        { '3', new KeywordNumber() },
        { '4', new KeywordNumber() },
        { '5', new KeywordNumber() },
        { '6', new KeywordNumber() },
        { '7', new KeywordNumber() },
        { '8', new KeywordNumber() },
        { '9', new KeywordNumber() },
        { '.', new KeywordNumber() },
        { ',', new KeywordNumber() },
        { '+', new KeywordPlus() },
        { '-', new KeywordMinus() },
        { '(', new KeywordOpenRoundBracket() },
        { ')', new KeywordCloseRoundBracket() }
    };

    public Lexer(Stream input)
    {
        _context = new LexerContext(input);
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

            IKeyword? keyword = null;
            if (_keywords.TryGetValue(currentChar, out keyword))
            {
                token = keyword.ToToken(currentChar, previusToken, _context);
                _context.AddToken(token);
                previusToken = token;
                input = _context.NextCharacter();
                continue;
            }
            else
            {
                throw new LexerInvalidTokenException("Character is a invalid token");
            }
        }

        return token;
    }

    public IToken InitializerTokenization()
    {
        var character = _context.NextCharacter();
        if (character == -1) throw new LexerNoDataException();

        var root = Tokenizer(character);
        if (root == null) throw new LexerEmptyTreeException();

        if (_context.Count == 0) throw new LexerEmptyTreeException();

        return _context.Head!;
    }
}