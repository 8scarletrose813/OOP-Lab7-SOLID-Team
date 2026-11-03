package Task2.interfaces;
/*
 * Опис проблеми: Окремий вибір продуктів призводив до змішування несумісних даних
 * - наприклад, відкриття роздрібного тарифу для бізнес-клієнта.
 * Рішення:
 * Використання шаблону Abstract Factory
 */
public interface Account {
    String getAccountDetails();
}