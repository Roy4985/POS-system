class StockMovement
{
    public Guid Id {get; private set;} = Guid.NewGuid();
    public Guid ProductId {get;private set;}
    public Guid? FromLocationId {get;private set;} // adjustments or initial stock up don't have from and to location
    public Guid? ToLocationId {get;private set;}
    public MovementType Type {get;private set;}
    public int Quantity {get;private set;}
    public DateTime Timestamp {get;private set;} = DateTime.UtcNow;

    public StockMovement(Guid productid, Guid? fromlocid, Guid? tolocid, MovementType type, int qtty)
    {
        ProductId = productid;
        FromLocationId = fromlocid;
        ToLocationId = tolocid;
        Type = type;
        Quantity = qtty;
    }

}