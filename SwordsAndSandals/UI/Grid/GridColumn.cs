namespace SwordsAndSandals.UI.Grid
{
    public class GridColumn
    {
        public string Header { get; }
        public int Width { get; }

        public GridColumn(string header, int width = 100)
        {
            Header = header;
            Width = width;
        }
    }
}
