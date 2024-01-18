using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeX.Net.Parsing.Expressions; 

/// <summary>
/// Represents a modifier in the syntax tree. It is a control with a grouping that acts as a scope.
/// </summary>
public class ModifierExpr : Expr {

    /// <summary>
    /// The command of this modifier
    /// </summary>
    public required ControlExpr Control { get; set; }

    /// <summary>
    /// The scope of this modifier
    /// </summary>
    public required GroupExpr Group { get; set; }
}
