﻿using SwordsAndSandals.Classes;
using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Visitor
{
    public interface IVisitor
    {
        void VisitSpinner(Spinner spinner);
    }
}
