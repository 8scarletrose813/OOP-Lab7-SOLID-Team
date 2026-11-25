package Task2.model;

import java.util.HashMap;
import java.util.Map;

public class LedgerMemento {
    private String checkpointName;
    private Map<String, Double> savedBalances;

    public LedgerMemento(String checkpointName, Map<String, Double> balances ) {
        this.checkpointName = checkpointName;
        this.savedBalances = new HashMap<>(balances);
    }

    public String getCheckpointName() {
        return checkpointName;
    }

   Map<String, Double> getSavedBalances() {
        Map<String, Double> saveBalance = new HashMap<>(savedBalances);
        return saveBalance;
    }
}
