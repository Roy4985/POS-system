#include "cart.h"
#include <vector>

AddResult Cart::addItem(const Product& product, int quantity) {
    if(quantity <= 0) {return AddResult::InvalidQuantity;}
    // If the item is already in cart, add quantity
    for(CartItem& item : items) {
        if (item.product.sku == product.sku) {
            item.quantity += quantity;
            return AddResult::Ok;
        }
    }
    // If not in Cart Add it
    items.push_back(CartItem{product, quantity});
    return AddResult::Ok;
}

std::int64_t Cart::total() const {
    std::int64_t sum = 0;
    for(const CartItem& item : items) {
        sum += item.product.price_cents * item.quantity;
    }
    return sum;
}

int Cart::unitCount() const {
    int sum = 0;
    for(const CartItem& item : items){
        sum += item.quantity;
    }
    return sum;
}

bool Cart::isEmpty() const {
    return items.empty();
}