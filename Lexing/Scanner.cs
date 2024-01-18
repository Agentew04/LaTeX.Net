
namespace LaTeX.Net.Lexing;

/// <summary>
/// Class to scan the source code and produce tokens
/// </summary>
internal class Scanner {

    /// <summary>
    /// O código fonte que será lido pelo scanner.
    /// </summary>
    private readonly string source;

    /// <summary>
    /// Uma lista dos tokens que vai ser construida pelo scanner.
    /// </summary>
    private readonly List<Token> tokens = [];

    /// <summary>
    /// Um offset de <see cref="source"/>. Aponta para o primeiro caractere do lexema atual.
    /// </summary>
    private int start = 0;
    /// <summary>
    /// Um offset se <see cref="source"/>. Aponta para o caractere atual que está sendo lido do lexema atual.
    /// </summary>
    private int current = 0;

    public Scanner(string source)
    {
        this.source = source;
    }

    public List<Token> ScanTokens() {
        while (!IsAtEnd()) {
            start = current;
            ScanToken();
        }

        tokens.Add(new Token(TokenType.EOF, ""));
        return tokens;
    }

    private bool IsAtEnd() => current >= source.Length;

    private char Advance() => source[++current - 1];

    private void ScanToken() {

        while (current < source.Length && source[current] != '\\' && source[current] != '{' && source[current] != '}') {
            Advance();
        }
        if(start != current)
            AddToken(TokenType.Text);
        start = current;

        if(IsAtEnd()) return;

        // check if we stopped at a slash or a curly brace
        if (source[current] == '\\') {
            // check if we have a command or a newline
            if (source[current + 1] == '\\') {
                // we have a newline
                current += 2;
                AddToken(TokenType.NewLine);
            } else {
                // we have a command
                current++;
                while (source[current] != '{') {
                    Advance();
                }
                AddToken(TokenType.Command);
            }
        } else if (source[current] == '{') {
            current++;
            AddToken(TokenType.OpenCurlyBrace);
        } else if (source[current] == '}') {
            current++;
            AddToken(TokenType.CloseCurlyBrace);
        }
    }

    private void AddToken(TokenType type) {
        string lexeme = source[start..current];
        if(type is TokenType.Command) {
            lexeme = lexeme[1..];
        }
        tokens.Add(new Token(type, lexeme));
    }

    private bool Expect(char expected) {
        if (IsAtEnd()) return false;
        if (source[current] != expected) return false;

        current++;
        return true;
    }
}
