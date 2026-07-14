using Microsoft.VisualBasic;

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

    public void RemoveProduct(string itemcode)
    {
        Product? prod = FindProduct(itemcode);
        if(prod == null)
        {
            System.Console.WriteLine("This product is not available"); // TEMP
        } else
        {
            prod.IsActive = false;
            System.Console.WriteLine($"{prod} was succesfully removed"); // TEMP       
        }
    }

    public void PrintTitles()
    {
        System.Console.WriteLine("name\tbarcode\tbrand\tfamily\tcategory\titemcode\tcostp\tsellp\thasTVA");
    }

    public void PrintProduct(Product product)
    {
        System.Console.WriteLine($"{product.Name,-15}{product.Barcode,-15}{product.Brand,-15}{product.Family,-15}{product.Category,-15}{product.ItemCode,-15}{product.CostPrice,-15}{product.SellPrice,-15}{product.HasTVA,-15}");
    }
    public void ListProducts()
    {
        PrintTitles();
        foreach (Product product in Products)
        {
            PrintProduct(product);
        }
    }

    public void AddLocation(Location loc)
    {
        Locations.Add(loc);
        System.Console.WriteLine($"{loc.Name} Successfuly added to locations"); // TEMP
    }

    public Product? FindProduct(string itemcode = "", string barcode = "")
    {
        Product? prod = Products.FirstOrDefault(p => p.ItemCode == itemcode || p.Barcode == barcode);
        if(prod == null)
        {
            System.Console.WriteLine("The product was not found"); // TEMP
            return prod;
        } else
        {
            System.Console.WriteLine($"{prod} was found"); // TEMP
            return prod;
        }
    }

    public void AddStock(Guid productid, Guid locationid, int qtty, int? row = null, int? col = null)
    {
        var key = (productid, locationid);
        if (Stock.ContainsKey(key)) // product is already in stock
        {
            Stock[key].Quantity += qtty;
        } else // new product is being added in stock
        {
            Stock[key] = new StockEntry(productid, locationid, qtty, row, col);
        }
    Movements.Add(new StockMovement(productid, null, locationid, MovementType.StockIn, qtty));

    }

    public int GetStock(Guid productid, Guid locationid)
    {
        var key = (productid, locationid);
        if (Stock.ContainsKey(key))
        {
            return Stock[key].Quantity;
        }
        return 0;
    }

    public bool TransferStock(Guid productid, Guid fromid, Guid toid, int qtty, int? row = null, int? col = null)
    {
        // Guard clauses start
        if(qtty <= 0){return false;}

        if(fromid == toid){return false;}
        
        Product? found = Products.FirstOrDefault(p => p.Id == productid);
        if(found == null){return false;}

        int available_qtty = GetStock(productid, fromid);
        if (available_qtty < qtty){return false;}
        // Guard clauses end

        Stock[(productid,fromid)].Quantity -= qtty;
        if (Stock.ContainsKey((productid, toid)))
        {
            Stock[(productid,toid)].Quantity += qtty;
        } else
        {
            Stock[(productid,toid)] = new StockEntry(productid, toid, qtty, row, col);
        }
        Movements.Add(new StockMovement(productid, fromid, toid, MovementType.Transfer, qtty));
        return true;
    }
}