# LaTeX.Net

This is a simple and lightweight library to compile snippets of LaTeX code into a
document object so you can do your own converting to your format.

## Usage

```csharp

// each of these namespaces are increasing levels of abstraction
using LaTeX.Net.Lexing;
using LaTeX.Net.Parsing;
using LaTeX.Net.Parsing.Document;

// the original LaTeX code
string latex = """
    Good morning \textbf{Santa Maria, the \textit{heart of Rio Grande}, today is \textit{25ºC}!
    """;

// this will convert the LaTeX code into a list of tokens
Scanner scanner = new(latex);
List<Token> tokens = scanner.ScanTokens();

// here we parse these tokens and create a abstract syntax tree, you can use
// this, but it will be a little harder
Parser parser = new(tokens);
SyntaxTree tree = parser.Parse();

// the last abstraction, with the document object you can do whatever you want
LatexDocument document = parser.GetDocument(tree);
```