using LaTeX.Net.Lexing;
using LaTeX.Net.Parsing;
using LaTeX.Net.Parsing.Document;

namespace LaTeX.Net {
    public static class Program {
        public static void Main(string[] args) {
            string latex = """
                Good morning \textbf{Santa Maria, the \textit{heart of Rio Grande}, today is \textit{25ºC}!
                """;

            Scanner scanner = new(latex);
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new(tokens);

            SyntaxTree tree = parser.Parse();

            LatexDocument document = parser.GetDocument(tree);
        }
    }
}
