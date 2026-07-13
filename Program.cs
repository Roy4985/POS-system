var p = new Product("Spoon", "1231230", "Gucci", "Item0012", 5, 10);

// System.Console.WriteLine(p.Name);

var manager = new InventoryManager();

manager.AddProduct(p);
manager.ListProducts();