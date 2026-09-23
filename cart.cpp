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

CartItem* Cart::findItem(const Product& product) {
    for(CartItem& i : items) {
        if(i.product.sku == product.sku) {
            return &i;
        }
    }
    return nullptr;
}


RemoveResult Cart::removeItem(const std::string& sku, int quantity) {
    if(quantity <= 0) {
        return RemoveResult::InvalidQuantity;
    }
    for(std::size_t i=0; i<items.size(); ++i){
        if(items[i].product.sku == sku){
            if(items[i].quantity <= quantity) {
                items.erase(items.begin() + i);
            } else {
                items[i].quantity -= quantity;
            }
            return RemoveResult::Ok;
        }
    }
    return RemoveResult::NotInCart;
}