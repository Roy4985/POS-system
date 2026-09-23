#pragma once
#include <vector>
#include <cstdint>
#include "product.h"

enum class AddResult {Ok, InvalidQuantity, OutOfStock};

class Cart {
    public:
        AddResult addItem(const Product& product, int quantity);
        std::int64_t total() const;
        int unitCount() const;
        bool isEmpty() const;

    private:
        std::vector<CartItem> items;
};