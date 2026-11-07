package Task2.interfaces;
/*При додаванні до банківської картки додаткових послуг створення окремого класу
під кожну можливу комбінацію призводило до десятків майже однакових класів. -
Decorator*/
public interface BankCard {
    String getCardDetails();
    double getServiceCost();
}
