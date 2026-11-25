package Task3.products;

import Task3.visitors.ProductVisitor;

public interface BankProduct {
    void accept(ProductVisitor visitor);
}
