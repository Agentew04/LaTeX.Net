using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeX.Net.Parsing.Expressions; 

/// <summary>
/// Represents a control sequence for the LaTeX document.
/// </summary>
public class ControlExpr : Expr {

    /// <summary>
    /// The command that this control sequence represents.
    /// It comes after the backslash.
    /// </summary>
    public required string Command { get; set; }
}
