#pragma once
#include <vector>
#include <optional>
#include <string>
#include <fstream>
#include <unordered_map>
#include <sstream>
#include <cstdint>
#include "product.h"

enum class CatalogResult {Ok, AlreadyAvailable};
enum class FileOpening {Ok, Error};

class Catalog {
    public:
        CatalogResult addProduct(const Product& product);
        std::optional<Product> findBySku(const std::string& sku) const;
        FileOpening loadFromFile(const std::string& path);

    private:
      std::unordered_map<std::string, Product> products;
};