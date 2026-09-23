#include "money.h"

#include <iomanip>
#include <sstream>

std::string formatMoney(std::int64_t cents) {
    std::ostringstream out;
    if(cents < 0) {
        cents = -cents;
        out << '-';
    }
    out << cents / 100 << '.' << std::setfill('0')  // pad with '0' instead of spaces
        << std::setw(2) << cents % 100; // force the next item to 2 characters: 5 -> 05

    return out.str();
}