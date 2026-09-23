#pragma once
#include <vector>
#include <optional>
#include <string>
#include "product.h"

enum class CatalogResult {Ok, AlreadyAvailable};

class Catalog {
    public:
        CatalogResult addProduct(const Product& product);
        std::optional<Product> findBySku(const std::string& sku) const;

    private:
        std::vector<Product> products;
};