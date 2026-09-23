#include <iostream>

#include "money.h"
#include "product.h"
#include "cart.h"
#include "catalog.h"

int main() {
    Product milk{"123","Milk", 200};
    Product coffee{"456","Coffee", 375};

    Cart cart1;
    Catalog catalog;

        // AddResult r = cart1.addItem(milk, 5);
        // if (r != AddResult::Ok) {std::cout << "Could Not add Item" << "\n";}

        // std::cout << cart1.unitCount() << std::endl;

        // if(cart1.isEmpty()){
        //     std::cout << "The Cart is empty" << "\n";
        // } else {std::cout << "The Cart is not empty" << "\n";}

    std::string scanned = "123";                    // from the barcode reader

    catalog.addProduct(milk);

    auto found = catalog.findBySku(scanned);        // look it up
    if (found) {
        cart1.addItem(*found, 1);                   // add the real product
    } else {
        std::cout << "Unknown barcode\n";           // <- this is why optional exists
    }
}