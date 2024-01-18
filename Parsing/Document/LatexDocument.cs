﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeX.Net.Parsing.Document;
public class LatexDocument {

    public DocumentNode RootNode { get; set; }

    internal LatexDocument(DocumentNode rootNode) {
        RootNode = rootNode;
    }
}
