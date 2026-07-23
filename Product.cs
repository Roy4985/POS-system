using System.ComponentModel;

class Product
{
    public Guid Id {get; private set;} = Guid.NewGuid(); // for automatic Ids
    public string Name {get;set;} = null!;
    public string Barcode {get;set;} = null!;
    public string Brand {get;set;} = null!;
    public string Family {get;set;} = null!;
    public string Category {get;set;} = null!;
    public string ItemCode {get;set;} = null!;
    public decimal CostPrice {get;set;}
    public decimal SellPrice {get;set;}
    public bool HasTVA {get;set;}
    public bool IsActive {get; set;} = true;

    private Product() { }

    // Constructor
    public Product(string name, string barcode, string brand, string itemcode, decimal costp, decimal sellp, bool tva=true, string family="", string category="")
    {
        Name = name;
        Barcode = barcode;
        Brand = brand;
        Family = family;
        Category = category;
        ItemCode = itemcode;
        CostPrice = costp;
        SellPrice = sellp;
        HasTVA = tva;

    }
}   