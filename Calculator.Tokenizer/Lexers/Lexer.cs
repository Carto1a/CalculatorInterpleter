using System.Text;
using Calculator.Tokenizer.Tokens;
using Calculator.Tokenizer.Tokens.Mathematic;
using Calculator.Tokenizer.Tokens.Mathematic.Operators;
using Calculator.Tokenizer.Tokens.Mathematic.Signals;

namespace Calculator.Tokenizer.Lexers;
public class Lexer
{
    private readonly StreamReader _reader;
    private int _whiteSpaceCounter = 0;
    private Token? _root;

    public Lexer(Stream input)
    {
        _reader = new StreamReader(input);
    }

    private bool IsNumber(char character)
    {
        int asciinumber0 = 48;
        int asciinumber9 = 57;

        return character >= asciinumber0 && character <= asciinumber9;
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

    public string UntokenizeRecursive(Token? root, StringBuilder? buffer = null)
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
    private Token? Tokenizer(int input, Token? previusToken = null)
    {
        while (input != -1)
        {
            char currentChar = (char)input;
            if(currentChar == ' ')
            {
                _whiteSpaceCounter++;
                if (_whiteSpaceCounter > 2)
                    throw new Exception("too many white spaces");

                input = NextCharacter();
                continue;
            }
            else if (IsNumber(currentChar))
            {
                _whiteSpaceCounter = 0;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(currentChar);
                var nextChar = NextCharacter();

                while(IsNumber((char)nextChar))
                {
                    stringBuilder.Append(nextChar);
                }

                var stringNumber = stringBuilder.ToString();
                var token = new TokenNumber(decimal.Parse(stringNumber));
                SetNextToken(previusToken, token);

                return Tokenizer(nextChar, token);
            }
            else if (currentChar == '+')
            {
                _whiteSpaceCounter = 0;
                if (previusToken is TokenOperator || previusToken is null)
                {
                    var tokenSignal = new SignalPositive();
                    var nextTokenSignal = Tokenizer(NextCharacter(), tokenSignal);
                    if (nextTokenSignal is TokenOperator)
                    {
                        throw new Exception("Invalid token");
                    }

                    SetNextToken(previusToken, tokenSignal);

                    return tokenSignal;
                }

                var tokenOperator = new OperatorAdd(previusToken);
                var nextTokenOperator = Tokenizer(NextCharacter(), tokenOperator);
                SetNextToken(previusToken, tokenOperator);

                if (nextTokenOperator is TokenOperator)
                {
                    throw new Exception("Invalid token");
                }

                return nextTokenOperator;
            }
            else if (currentChar == '-')
            {
                _whiteSpaceCounter = 0;
                if (previusToken is TokenOperator || previusToken is null)
                {
                    var tokenSignal = new SignalNegative();
                    var nextTokenSignal = Tokenizer(NextCharacter(), tokenSignal);

                    if (nextTokenSignal is TokenOperator)
                    {
                        throw new Exception("Invalid token");
                    }

                    SetNextToken(previusToken, tokenSignal);

                    return tokenSignal;
                }

                var tokenOperator = new OperatorSub(previusToken);
                var nextTokenOperator = Tokenizer(NextCharacter(), tokenOperator);
                SetNextToken(previusToken, tokenOperator);

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

        return previusToken;
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

    public Token Tokenization()
    {
        var character = _reader.Read();
        if (character == -1) throw new Exception("No character to tokenize");

        var root = Tokenizer(character);
        if (root == null) throw new Exception("Error or no data");

        return _root;
    }
}
