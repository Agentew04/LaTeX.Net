using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeX.Net.Parsing.Expressions;

/// <summary>
/// Represents a TEXT token in the syntax tree.
/// </summary>
public class TextExpr : Expr {

    /// <summary>
    /// The literal text that this expression has.
    /// </summary>
    public required string Text { get; set; }
}
