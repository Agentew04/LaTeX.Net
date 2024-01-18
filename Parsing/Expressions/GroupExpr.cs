using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeX.Net.Parsing.Expressions;

/// <summary>
/// Represents a group of expressions in the syntax tree.
/// </summary>
public class GroupExpr : Expr {
    /// <summary>
    /// The expressions that are in this group.
    /// </summary>
    public List<Expr> Children { get; set; } = [];
}
