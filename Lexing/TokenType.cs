namespace LaTeX.Net.Lexing;

/// <summary>
/// The type of the token for a lexeme.
/// </summary>
public enum TokenType
{
    /// <summary>
    /// The token is a text.
    /// </summary>
    Text,
    /// <summary>
    /// The token is a slash. Double slashes are covered by <see cref="NewLine"/>
    /// </summary>
    Slash,
    /// <summary>
    /// The token is a special latex command
    /// </summary>
    Command,
    /// <summary>
    /// The token is '{', opens a scope block
    /// </summary>
    OpenCurlyBrace,
    /// <summary>
    /// The token is '}', closes a scope block
    /// </summary>
    CloseCurlyBrace,
    /// <summary>
    /// Represents a indicator to new line in LaTeX
    /// </summary>
    NewLine,
    /// <summary>
    /// Represents the end of the file
    /// </summary>
    EOF
}
