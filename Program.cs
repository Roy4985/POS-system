using var context = new InventoryContext();
System.Console.WriteLine(context.Database.CanConnect());
