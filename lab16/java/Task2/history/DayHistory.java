package Task2.history;

import Task2.model.LedgerMemento;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.HashMap;
import java.util.Map;

public class DayHistory {
    private Map<String, LedgerMemento> checkpoints = new HashMap<>();
    public LedgerMemento get(String checkpointName){
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        if (checkpointName == null || !checkpoints.containsKey(checkpointName)) {
            System.out.println("[" + time + "] [DayHistory] Помилка: контрольну точку " +checkpointName + " не знайдено в історії дня.");
            return null;
        }
        LedgerMemento memento = checkpoints.get(checkpointName);
        System.out.println("[" + time + "] [DayHistory] Завантажено контрольну точку " + checkpointName + " .");
        return memento;
    }
    public void save(LedgerMemento memento){
        if (memento == null) {
            System.out.println("Помилка: Контрольна точка не знайдена.");
            return;
        }
        String checkpointName = memento.getCheckpointName();
        checkpoints.put(checkpointName, memento);
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("[" + time + "] [DayHistory] Контрольну точку " + checkpointName + " додано до історії дня.");
    }
}
