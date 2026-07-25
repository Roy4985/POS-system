using var ctx = new InventoryContext();
var manager = new InventoryManager(ctx);

// var pr1 = new Product("Megabonk","123","JoeBrand","Item001",5,10, "JoeFamily","JoeCategory");
// var pr2 = new Product("bonkmega","321","RoyBrand","Item002",5,10, "RoyFamily","RoyCategory");

// manager.AddProduct(pr1);
// manager.AddProduct(pr2);

// var l1 = new Location("Joe Location", LocationType.Store);
// manager.AddLocation(l1);

// manager.AddStock(Guid.Parse("40687ac8-4106-4d74-9c69-f95cb20a7df6"), Guid.Parse("d914b97c-79f5-4a3c-8d74-a6b53e2f5248"), 100);
// manager.AddStock(Guid.Parse("90a1e536-20f5-40e5-bfa2-651fcd4b4246"), Guid.Parse("8d63fbd5-ca71-41f3-82e7-e8613c98eaf3"),10);


bool working = manager.TransferStock(Guid.Parse("40687ac8-4106-4d74-9c69-f95cb20a7df6"),Guid.Parse("d914b97c-79f5-4a3c-8d74-a6b53e2f5248"),Guid.Parse("8d63fbd5-ca71-41f3-82e7-e8613c98eaf3"), 50);
System.Console.WriteLine($"The Transfer is {working}");