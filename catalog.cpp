#include "catalog.h"

CatalogResult Catalog::addProduct(const Product& product) {
    for(const Product& pr : products){
        if(pr.sku == product.sku){
            return CatalogResult::AlreadyAvailable;
        }
    }
    products.push_back(product);
    return CatalogResult::Ok;
}

std::optional<Product> Catalog::findBySku(const std::string& sku) const {
    for(const Product& pr : products){
        if(pr.sku == sku){
            return pr;
        }
    }
    return std::nullopt; //if the product is not available, nullopt is like return None in Python
}