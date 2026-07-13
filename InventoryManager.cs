class InventoryManager
{
    public List<Product> Products {get;set;} = new();
    public List<Location> Locations {get;set;} = new();
    public Dictionary<(Guid, Guid), StockEntry> Stock {get;set;} = new(); // this is to be able to see which item is where and which row/column...
    public List<StockMovement> Movements {get;set;}  = new(); //Movement history

    public void AddProduct(Product prod)
    {
        Products.Add(prod);
        System.Console.WriteLine($"{prod} Succesfully added to product list");
    }

    public void ListProducts()
    {
        foreach (Product product in Products)
        {
            System.Console.WriteLine("name\tbarcode\tbrand\tfamily\tcategory\titemcode\tcostp\tsellp\thasTVA");
            System.Console.WriteLine($"{product.Name,-15}{product.Barcode,-15}{product.Brand,-15}{product.Family,-15}{product.Category,-15}{product.ItemCode,-15}{product.CostPrice,-15}{product.SellPrice,-15}{product.HasTVA,-15}");
        }
    }
}