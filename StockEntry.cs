class StockEntry
{
    public Guid ProductId {get; private set;}
    public Guid LocationId {get; private set;}
    public int Quantity {get; set;}
    public int? Row {get;set;}
    public int? Column {get;set;}

    public StockEntry(Guid pid, Guid lid, int qtty, int? row=null, int? col=null)
    {
        ProductId = pid;
        LocationId = lid;
        Quantity = qtty;
        Row = row;
        Column = col;
    }
}