#include "catalog.h"
#include <iostream>

CatalogResult Catalog::addProduct(const Product& product) {
    if(products.contains(product.sku)){
        return CatalogResult::AlreadyAvailable;
    }
    products[product.sku] = product;
    return CatalogResult::Ok;
}

std::optional<Product> Catalog::findBySku(const std::string& sku) const {
    auto i = products.find(sku);
    if(i == products.end()){
        return std::nullopt; //if the product is not available, nullopt is like return None in Python
    } else {
        return i->second;
    }
}

FileOpening Catalog::loadFromFile(const std::string& path) {
    std::ifstream file(path);
    if(!file){
        return FileOpening::Error;
    }

    std::string line;
    while(std::getline(file, line)){
        std::cout << "[" << line << "]\n";
        std::istringstream ss(line);
        std::string sku, name, priceText;

        std::getline(ss, sku, ',');
        std::getline(ss, name, ',');
        std::getline(ss, priceText, ',');

        std::int64_t price = 0;

        try {
            price = std::stoll(priceText);
        } catch (const std::exception& e) {
            // bad row — skip it
            std::cout << "Skipping bad row: [" << line << "] — " << e.what() << "\n";
            continue;
        }

        price = std::stoll(priceText);

        (void)addProduct(Product{sku, name, price});
    }
    return FileOpening::Ok;
}