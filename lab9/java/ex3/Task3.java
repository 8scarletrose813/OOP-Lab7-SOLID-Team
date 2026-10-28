package ex3;

public class Task3 {
    public static void main(String[] args) throws CloneNotSupportedException {
        DepositContract depositContract = new DepositContract("Депозит Стабільний", 15.0, 12,"Виплата 50% відсотків при достроковому закритті");
        Addendum addendum = new Addendum("Базове обслуговування", "Стандартний пакет послуг");
        depositContract.addAddendum(addendum);
        PaymentSchedule paymentSchedule = new PaymentSchedule("01.12.2026", 1250.0);
        depositContract.addPaymentSchedule(paymentSchedule);
        Client client1 = new Client("Шевченко Тарас Григорович", "UA44300001000002600123456789");
        DepositContract contract1 = (DepositContract) depositContract.clone();
        contract1.setClient(client1);
        Client client2 = new Client("Українка Леся Петрівна", "UA44300001000002600987654321");
        DepositContract contract2 = (DepositContract) depositContract.clone();
        contract2.setClient(client2);
        Client client3 = new Client("Франко Іван Якович", "UA44300001000002600555555555");
        DepositContract contract3 = (DepositContract) depositContract.clone();
        contract3.setClient(client3);
        Addendum addendum1 = new Addendum("Персональний менеджер", "Право на дострокове зняття до 20% суми");
        contract1.addAddendum(addendum1);
        PaymentSchedule paymentSchedule1 = new PaymentSchedule("01.01.2027", 1300.0);
        contract1.addPaymentSchedule(paymentSchedule1);
        System.out.println("Еталоний договір");
        System.out.println(depositContract);
        System.out.println("Зміний договір клієнт 1");
        System.out.println(contract1);



    }
}
