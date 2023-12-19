using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Visitor
{
    public class UpdateVisitor : IVisitor
    {
        public void VisitSpinner(Spinner spinner)
        {
            spinner.Update(spinner.GameTime);
        }
    }
}
