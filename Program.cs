using LaTeX.Net.Lexing;
using LaTeX.Net.Parsing;
using LaTeX.Net.Parsing.Document;

namespace LaTeX.Net {
    public static class Program {
        public static void Main(string[] args) {
            string latex = """
                Bom dia, aqui em \textbf{Santa Maria, o \textit{coração do Rio Grande}, está 25 graus!
                """;

            Scanner scanner = new(latex);
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new(tokens);

            SyntaxTree tree = parser.Parse();

            LatexDocument document = parser.GetDocument(tree);
        }
    }
}
