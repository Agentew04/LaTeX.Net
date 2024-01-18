using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeX.Net.Parsing.Document; 
public class DocumentNode {

    public DocumentNode? Parent { get; set; }

    public List<DocumentNode> Children { get; set; } = new List<DocumentNode>();
}
