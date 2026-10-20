package variant7_reportexporter;

public class ReportExporter {
    public String export(ReportData data, ReportFormatter formatter) {
        String formattedResult = formatter.formatReport(data);
        System.out.println("Успішно створено: " + formattedResult);
        return formattedResult;
    }
}