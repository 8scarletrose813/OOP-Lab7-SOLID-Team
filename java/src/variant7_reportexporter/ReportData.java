package variant7_reportexporter;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

class ReportData {
    private String title;
    private LocalDateTime generatedAt;
    private List<String> headers = new ArrayList<>();
    private List<List<String>> rows = new ArrayList<>();

    public ReportData(String title, LocalDateTime generatedAt) {
        this.title = title;
        this.generatedAt = generatedAt;
    }

    public String getTitle() {
        return title;
    }
    public LocalDateTime getGeneratedAt() {
        return generatedAt;
    }
    public List<String> getHeaders() {
        return headers;
    }
    public List<List<String>> getRows() {
        return rows;
    }
}