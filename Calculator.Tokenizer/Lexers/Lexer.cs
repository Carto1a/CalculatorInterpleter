using System.Text;
using Calculator.Tokenizer.Lexers.Exceptions;
using Calculator.Tokenizer.Tokens;
using Calculator.Tokenizer.Tokens.Mathematic;
using Calculator.Tokenizer.Tokens.Mathematic.Operators;
using Calculator.Tokenizer.Tokens.Mathematic.Scopes;
using Calculator.Tokenizer.Tokens.Mathematic.Signals;

namespace Calculator.Tokenizer.Lexers;
public class Lexer
{
    public readonly int MIN_SCOPE_LEVEL = 0;

    private readonly TokenList _tokenList = new();
    private readonly StreamReader _reader;
    private int _whiteSpaceCounter = 0;
    private int _atualScopeLevel;

    public Lexer(Stream input)
    {
        _reader = new StreamReader(input);
        _atualScopeLevel = MIN_SCOPE_LEVEL;
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
        Token? token = null;

        while (input != -1)
        {
            char currentChar = (char)input;
            if (currentChar == ' ')
            {
                _whiteSpaceCounter++;
                if (_whiteSpaceCounter > 2)
                    throw new LexerTooManySpacesException();

                input = NextCharacter();
                continue;
            }
            else if (IsNumber(currentChar))
            {
                _whiteSpaceCounter = 0;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(currentChar);
                var nextChar = NextCharacter();

                while (IsNumber((char)nextChar))
                {
                    stringBuilder.Append(nextChar);
                }

                var stringNumber = stringBuilder.ToString();
                token = new TokenNumber(decimal.Parse(stringNumber));

                _tokenList.Add(token);

                var nextToken = Tokenizer(nextChar, token);

                return token;
            }
            else if (currentChar == '+')
            {
                _whiteSpaceCounter = 0;
                if (previusToken is TokenOperator || previusToken is null)
                {
                    token = new SignalPositive();
                    _tokenList.Add(token);

                    var nextTokenSignal = Tokenizer(NextCharacter(), token);

                    if (nextTokenSignal is null)
                    {
                        throw new LexerSignalTokenException("No token after signal");
                    }
                    if (nextTokenSignal is TokenOperator)
                    {
                        throw new LexerSignalTokenException("Invalid token after signal");
                    }

                    return token;
                }

                token = new OperatorAdd(previusToken);
                _tokenList.Add(token);

                var nextTokenOperator = Tokenizer(NextCharacter(), token);

                if (nextTokenOperator is null)
                {
                    throw new LexerOperatorTokenException("No token after operator");
                }

                if (nextTokenOperator is TokenOperator)
                {
                    throw new LexerOperatorTokenException("Invalid token after operator");
                }

                return token;
            }
            else if (currentChar == '-')
            {
                _whiteSpaceCounter = 0;
                if (previusToken is TokenOperator || previusToken is null)
                {
                    token = new SignalNegative();
                    _tokenList.Add(token);

                    var nextTokenSignal = Tokenizer(NextCharacter(), token);

                    if (nextTokenSignal is null)
                    {
                        throw new LexerSignalTokenException("No token after signal");
                    }
                    if (nextTokenSignal is TokenOperator)
                    {
                        throw new LexerSignalTokenException("Invalid token after signal");
                    }

                    return token;
                }

                token = new OperatorSub(previusToken);
                _tokenList.Add(token);

                var nextTokenOperator = Tokenizer(NextCharacter(), token);

                if (nextTokenOperator is null)
                {
                    throw new LexerOperatorTokenException("No token after operator");
                }

                if (nextTokenOperator is TokenOperator)
                {
                    throw new LexerOperatorTokenException("Invalid token after operator");
                }

                return token;
            }
            /* else if (currentChar == '(') */
            /* { */
            /*     _whiteSpaceCounter = 0; */
            /*     token = new TokenOpenScope(MIN_SCOPE_LEVEL); */
            /*     _tokenList.Add(token); */

            /*     var openScopeToken = token as TokenOpenScope; */
            /*     while (true) */
            /*     { */
            /*         _whiteSpaceCounter = 0; */

            /*         var nextToken = Tokenizer(NextCharacter(), token); */
            /*         if (nextToken is null) */
            /*         { */
            /*             throw new Exception("Invalid token"); */
            /*         } */

            /*         openScopeToken!.Add(nextToken); */
            /*         _tokenList.Add(nextToken); */

            /*         if (nextToken is TokenCloseScope) */
            /*         { */
            /*             break; */
            /*         } */
            /*     } */

            /*     return token; */
            /* } */
            /* else if (currentChar == ')') */
            /* { */
            /*     _whiteSpaceCounter = 0; */
            /*     token = new TokenCloseScope(); */
            /*     _tokenList.Add(token); */

            /*     var nextToken = Tokenizer(NextCharacter(), token); */

            /*     return token; */

            /* } */
            else
            {
                throw new LexerInvalidTokenException("Character is a invalid token");
            }
        }

        return token;
    }

    private int NextCharacter()
    {
        return _reader.Read();
    }

    public Token Tokenization()
    {
        var character = _reader.Read();
        if (character == -1) throw new LexerNoDataException();

        var root = Tokenizer(character);
        if (root == null) throw new LexerEmptyTreeException();

        if (_tokenList.Count == 0) throw new LexerEmptyTreeException();

        return _tokenList.Head!;
    }
}
