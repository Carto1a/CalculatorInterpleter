using System.Text;
using Calculator.Tokenizer.Tokens;
using Calculator.Tokenizer.Tokens.Mathematic;
using Calculator.Tokenizer.Tokens.Mathematic.Operators;
using Calculator.Tokenizer.Tokens.Mathematic.Signals;

namespace Calculator.Tokenizer.Lexers;
public class Lexer
{
    private readonly StreamReader _reader;
    private Token? _root;

    public Lexer(Stream input)
    {
        _reader = new StreamReader(input);
    }

    private bool IsNumber(char c)
    {
        var number0 = 48;
        var number9 = 57;

        return c >= number0 && c <= number9;
    }

    public string Untokenize(Token root)
    {
        var buffer = new StringBuilder();
        var current = root;

        while (current != null)
        {
            buffer.Append(current.ToString());
            current = current.NextToken;
        }

        return buffer.ToString();
    }

    public string UntokenizeRecursive(
        Token? root, StringBuilder? buffer = null)
    {
        buffer ??= new StringBuilder();
        if (root == null)
        {
            return buffer.ToString();
        }

        return UntokenizeRecursive(root.NextToken, buffer.Append(root.ToString()));
    }

    // 1 + 2 + 3: good
    // -1 + 2 + 3: good
    // -1 + -2 + 3: good
    //
    // -1 + (-2) + 3: good
    // -1 ( 2 + 2 ) + 3: good
    // - ( 1 + 2 ) + 3: good
    private Token? Tokenizer(int input, Token? previus = null)
    {
        while (input != -1)
        {
            char current = (char)input;
            if(current == ' ')
            {
                input = NextCharacter();
                continue;
            }
            else if (IsNumber(current))
            {
                var buffer = new StringBuilder();
                buffer.Append(current);
                char nextchar = (char)NextCharacter();

                while(IsNumber(nextchar))
                {
                    buffer.Append(nextchar);
                }

                var stringNumber = buffer.ToString();
                var token = new TokenNumber(decimal.Parse(stringNumber));
                SetNextToken(previus, token);

                return Tokenizer(NextCharacter(), token);
            }
            else if (current == '+')
            {
                if (previus is TokenOperator || previus is null)
                {
                    var tokenSignal = new SignalPositive();
                    var nextTokenSignal = Tokenizer(NextCharacter(), tokenSignal);
                    if (nextTokenSignal is TokenOperator)
                    {
                        throw new Exception("Invalid token");
                    }

                    SetNextToken(previus, tokenSignal);

                    // NOTE: return next or token?
                    return tokenSignal;
                }

                var tokenOperator = new OperatorAdd(previus);
                var nextTokenOperator = Tokenizer(NextCharacter(), tokenOperator);
                SetNextToken(previus, tokenOperator);

                if (nextTokenOperator is TokenOperator)
                {
                    throw new Exception("Invalid token");
                }

                return nextTokenOperator;
            }
            else
            {
                throw new Exception("Invalid token");
            }
        }

        return _root;
    }

    private int NextCharacter()
    {
        return _reader.Read();
    }

    private void SetNextToken(Token? previus, Token next)
    {
        if (previus == null)
        {
            _root = next;
            return;
        }

        previus.NextToken = next;
    }

    private Token ToToken(string input, int start, int end)
    {
        throw new NotImplementedException();
        /* return T(input[start..end]); */
    }

    public Token Tokenization()
    {
        var character = _reader.Read();
        if (character == -1) throw new Exception("No more characters to read");

        return Tokenizer(character);
    }
}
