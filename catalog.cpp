#include "catalog.h"

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