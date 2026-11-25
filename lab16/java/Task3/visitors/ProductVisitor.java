package Task3.visitors;

import Task3.products.CardProduct;
import Task3.products.CreditProduct;
import Task3.products.DepositProduct;
import Task3.products.InvestmentAccount;

public interface ProductVisitor {
    void visit(DepositProduct deposit);
    void visit(CreditProduct credit);
    void visit(CardProduct card);
    void visit(InvestmentAccount investment);
    default String getOperationName(){
        return this.getClass().getSimpleName();
    }
}
