using System.ComponentModel;

class Product
{
    public Guid Id {get; private set;} = Guid.NewGuid(); // for automatic Ids
    public string Name {get;set;}
    public string Barcode {get;set;}
    public string Brand {get;set;}
    public string Family {get;set;}
    public string Category {get;set;}
    public string ItemCode {get;set;}
    public decimal CostPrice {get;set;}
    public decimal SellPrice {get;set;}
    public bool HasTVA {get;set;}
    public bool IsActive {get; set;} = true;

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