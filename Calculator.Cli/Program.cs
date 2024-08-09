﻿// See https://aka.ms/new-console-template for more information
using System.Text;

using Calculator.Tokenizer.Lexers;

// TODO: stop using recursion
// TODO: implement scopes

Console.WriteLine("Hello, World!");

var input = "10 + 10";
Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
var lexer = new Lexer(stream);
int t = 1;

var token = lexer.InitializerTokenization();
Console.WriteLine(lexer.Untokenize(token));