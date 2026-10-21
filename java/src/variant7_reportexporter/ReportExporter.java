package variant7_reportexporter;

// Опис предметної області: Платіжний шлюз для інтернет-еквайрингу.
// Завдання коду: Розрахунок комісії та проведення платежу залежно від обраного типу платіжної системи.

public class ReportExporter {
    public String export(ReportData data, ReportFormatter formatter) {
        String formattedResult = formatter.formatReport(data);
        System.out.println("Успішно створено: " + formattedResult);
        return formattedResult;
    }
}