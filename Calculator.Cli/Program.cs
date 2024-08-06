// See https://aka.ms/new-console-template for more information
using Calculator.Tokenizer.Lexers;
using Calculator.Tokenizer.Tokens.Mathematic;
using Calculator.Tokenizer.Tokens.Mathematic.Operators;

Console.WriteLine("Hello, World!");

var lexer = new Lexer();
var token1 = new TokenNumber(1);
var token2 = new TokenNumber(2);
var token3 = new TokenNumber(3);

var tokenadd1 = new OperatorAdd(token1, token2);
var tokenadd2 = new OperatorAdd(tokenadd1, token3);

token1.Next = tokenadd1;
token2.Next = tokenadd2;

Console.WriteLine(lexer.UntokenizeRecursive(token1));
