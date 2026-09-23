#pragma once
#include <string>
#include <cstdint>


struct Product {
    std::string sku;
    std::string name;
    std::int64_t price_cents;
};

struct CartItem {
    Product product;
    int quantity;
};