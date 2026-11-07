package Task2;
import Task2.retail.RetailProductFactory;
import Task2.premium.PremiumProductFactory;
import Task2.business.BusinessProductFactory;
import Task2.student.StudentProductFactory;
public class Task2 {
    public static void main(String[] args) {
        BankBranch bankBranch = new BankBranch("Центральне відділення банку 'Прут-Кредит'");
        RetailProductFactory factory = new RetailProductFactory();
        bankBranch.openClientPackage("Олексій Коваленко", factory);
        PremiumProductFactory factory1 = new PremiumProductFactory();
        bankBranch.openClientPackage("Георгій Степенчук", factory1);
        BusinessProductFactory factory2 = new BusinessProductFactory();
        bankBranch.openClientPackage("ТОВ Буковина", factory2);
        StudentProductFactory factory3 = new StudentProductFactory();
        bankBranch.openClientPackage("Марія студентка", factory3);

    }
}
