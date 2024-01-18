using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeX.Net.Lexing; 

/// <summary>
/// Represents a single token for the Lexing process
/// </summary>
public class Token {

    /// <summary>
    /// The token type of the token
    /// </summary>
    public TokenType Type { get; set; }

    /// <summary>
    /// The literal text that is represented by the token
    /// </summary>
    public string Lexeme { get; set; }

    public Token(TokenType type, string lexeme)
    {
        Type = type;
        Lexeme = lexeme;
    }
}
