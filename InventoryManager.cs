using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

class InventoryManager
{
    private InventoryContext context;

    public InventoryManager(InventoryContext ctx)
    {
        context = ctx;
    }

    public void AddProduct(Product prod)
    {
        context.Products.Add(prod);
        context.SaveChanges();
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
            context.SaveChanges();
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
        var active_products = context.Products.Where(p => p.IsActive == true);
        PrintTitles();
        foreach (Product product in active_products)
        {
            PrintProduct(product);
        }
    }

    public void AddLocation(Location loc)
    {
        context.Locations.Add(loc);
        context.SaveChanges();
    }

    public Product? FindProduct(string itemcode = "", string barcode = "")
    {
        Product? prod = context.Products.FirstOrDefault(p => p.ItemCode == itemcode || p.Barcode == barcode);
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
        var exists = context.StockEntries.FirstOrDefault(s => s.ProductId == productid && s.LocationId == locationid);
        if (exists != null) // product is already in stock
        {
            exists.Quantity += qtty;
        } else // new product is being added in stock
        {
            context.StockEntries.Add(new StockEntry(productid, locationid, qtty, row, col));      
        }

        context.StockMovements.Add(new StockMovement(productid, null, locationid, MovementType.StockIn, qtty));
        context.SaveChanges();

    }

    public int GetStock(Guid productid, Guid locationid)
    {
        var exists = context.StockEntries.FirstOrDefault(s => s.ProductId == productid && s.LocationId == locationid);
        if (exists != null)
        {
            return exists.Quantity;
        }
        return 0;
    }

    public bool TransferStock(Guid productid, Guid fromid, Guid toid, int qtty, int? row = null, int? col = null)
    {
        // Guard clauses start
        if(qtty <= 0){return false;}

        if(fromid == toid){return false;}
        
        Product? found = context.Products.FirstOrDefault(p => p.Id == productid);
        if(found == null){return false;}

        int available_qtty = GetStock(productid, fromid);
        if (available_qtty < qtty){return false;}

        using var transaction = context.Database.BeginTransaction();
        try
        {
            var init_stock = context.StockEntries.FirstOrDefault(s => s.ProductId == productid && s.LocationId == fromid);
        if(init_stock != null)
        {
            init_stock.Quantity -= qtty;
        }
            
        // Guard clauses end
        var dest_stock = context.StockEntries.FirstOrDefault(s => s.ProductId == productid && s.LocationId == toid);
        if (dest_stock != null) // product is already in stock
        {
            dest_stock.Quantity += qtty;
        } else
        {
            context.StockEntries.Add(new StockEntry(productid, toid, qtty, row, col));
        }
        context.StockMovements.Add(new StockMovement(productid, fromid, toid, MovementType.Transfer, qtty));
        context.SaveChanges();
        transaction.Commit();
        return true;
        } catch
        {
            transaction.Rollback();
            return false;
        }
        
    }
}