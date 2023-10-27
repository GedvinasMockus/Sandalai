﻿using System.Collections.Generic;

namespace SwordsAndSandals.Objects.Grid
{
    public class GridRow
    {
        public List<GridColumn> Columns { get; set; }
        public List<Button> Buttons { get; set; }

        public GridRow()
        {
            Columns = new List<GridColumn>();
            Buttons = new List<Button>();
        }
        public void AddData(GridColumn column)
        {
            Columns.Add(column);
        }
        public void AddButton(Button spectate)
        {
            Buttons.Add(spectate);
        }
    }
}
