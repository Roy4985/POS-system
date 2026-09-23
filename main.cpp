#include <iostream>

#include "money.h"
#include "product.h"
#include "cart.h"
#include "catalog.h"

Catalog makeCatalog() {
        Catalog c;
        (void)c.addProduct(Product{"123", "Milk",   200});
        (void)c.addProduct(Product{"456", "Coffee", 375});
        (void)c.addProduct(Product{"789", "Bread",  150});
        return c;
    }

    Cart makeCart(const Catalog& catalog) {
        Cart c;
        auto milk = catalog.findBySku("123");
        if (milk) { (void)c.addItem(*milk, 2); }
        return c;
    }

    

int main() {
    Catalog catalog = makeCatalog();
    Cart cart = makeCart(catalog);

    Product milk{"123", "Milk", 200};

    CartItem* item1 = cart.findItem(milk);

    std::cout << "The Item is " << item1->product.name << "\n";

    RemoveResult result = cart.removeItem("345", 1);

    switch (result) {
        case RemoveResult::Ok:
            std::cout << "Removed\n";
            break;
        case RemoveResult::InvalidQuantity:
            std::cout << "Quantity must be at least 1\n";
            break;
        case RemoveResult::NotInCart:
            std::cout << "That item isn't in the cart\n";
            break;
    }



}