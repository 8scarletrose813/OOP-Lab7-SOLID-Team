package Task3.swift;

public class SwiftService {
    public int sendSwiftMessage(String sendSwift, double amount) {
        if (sendSwift == null || sendSwift.trim().isEmpty()) {
            return 400;
        }
        System.out.println("Вихідне сформоване SWIFT-повідомлення: " + sendSwift);
        if (sendSwift.contains("REJECT") || sendSwift.contains("ERROR") || amount > 1000000) {
            return amount > 1000000 ? 500 : 401;
        }
        return 200;
    }
}
