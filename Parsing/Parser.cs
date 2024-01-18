using LaTeX.Net.Exceptions;
using LaTeX.Net.Lexing;
using LaTeX.Net.Parsing.Document;
using LaTeX.Net.Parsing.Expressions;
using System.Diagnostics;

namespace LaTeX.Net.Parsing;

/// <summary>
/// Class to parse a series of tokens into a syntax tree
/// </summary>
public class Parser {

    private readonly List<Token> tokens;

    private int current = 0;

    public Parser(List<Token> tokens) {
        this.tokens = tokens;
    }

    #region Helpers

    [DebuggerStepThrough]
    private bool IsAtEnd() {
        return Peek().Type == TokenType.EOF;
    }

    [DebuggerStepThrough]
    private Token Peek() => tokens[current];

    [DebuggerStepThrough]
    private Token Advance() {
        if (!IsAtEnd()) current++;
        return Previous();
    }

    [DebuggerStepThrough]
    private Token Previous() => tokens[current - 1];

    [DebuggerStepThrough]
    private bool Expect(TokenType type) {
        if (Peek().Type != type) return false;
        Advance();
        return true;
    }

    #endregion

    #region Parse Functions

    public SyntaxTree Parse() {
        SyntaxTree tree = new();
        while (!IsAtEnd()) {
            tree.Nodes.Add(ParseExpression());
        }
        return tree;
    }

    private Expr ParseExpression() {
        Token token = Advance();

        if(token.Type == TokenType.Text) {
            return new TextExpr() {
                Text = token.Lexeme
            };
        } else if(token.Type == TokenType.Command) {
            return ParseCommand();
        } else if(token.Type == TokenType.OpenCurlyBrace) {
            return ParseGroup();
        } else if(token.Type == TokenType.NewLine) {
            return new NewLineExpr();
        } else {
            throw new ParserException("Unexpected token type: " + token.Type);
        }
    }

    private Expr ParseCommand() {
        // we just read a command. We need to peek to see if its a group(then its a modifier)
        Token command = Previous();

        if(Expect(TokenType.OpenCurlyBrace)) {
            // we have a modifier
            // so we need to loop parsing expressions until we find a close curly brace

            ControlExpr control = new() {
                Command = command.Lexeme
            };

            GroupExpr group = ParseGroup();

            return new ModifierExpr() {
                Control = control,
                Group = group
            };
        } else {
            // we have a command
            return new ControlExpr() {
                Command = command.Lexeme
            };
        }
    }

    private GroupExpr ParseGroup() {
        // we just read a open curly brace. We need to read the expressions until we find a close curly brace

        GroupExpr group = new();

        while (!Expect(TokenType.CloseCurlyBrace) && !IsAtEnd()) {
            group.Children.Add(ParseExpression());
        }

        return group;
    }

    #endregion

    #region Converter

    public LatexDocument GetDocument(SyntaxTree tree) {
        DocumentNode root = new();

        foreach(var expr in tree.Nodes) {
            DocumentNode node = ExprToNode(expr, root);
            root.Children.Add(node);
        }

        return new LatexDocument(root);
    }

    private DocumentNode ExprToNode(Expr expr, DocumentNode parent) {
        if(expr is TextExpr text) {
            return new TextNode() {
                Text = text.Text,
                Children = [],
                Parent = parent
            };

        } else if(expr is ControlExpr control) {
            // our document node currently will not support control commands
            throw new ParserException("Control commands are not supported yet");

        } else if(expr is ModifierExpr modifier) {
            DocumentNode modif;

            // check bold,italic,underline
            modif = modifier.Control.Command switch {
                "textbf" => new BoldModifier(),
                "textit" => new ItalicModifier(),
                "underline" => new UnderlineModifier(),
                _ => throw new ParserException("Unexpected modifier command: " + modifier.Control.Command)
            };

            // get children
            foreach(var child in modifier.Group.Children) {
                modif.Children.Add(ExprToNode(child, modif));
            }
            modif.Parent = parent;
            return modif;

        } else if(expr is GroupExpr group) {
            DocumentNode node = new();
            foreach(var child in group.Children) {
                node.Children.Add(ExprToNode(child, node));
            }
            return node;

        } else if(expr is NewLineExpr) {
            return new NewLineNode {
                Children = [],
                Parent = parent
            };

        } else {
            throw new ParserException("Unexpected expression type: " + expr.GetType().Name);
        }
    }

    #endregion
}
