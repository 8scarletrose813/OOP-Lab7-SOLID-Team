package variant7_reportexporter;

import java.util.List;

public class CsvReportFormatter implements ReportFormatter {
    @Override
    public String formatReport(ReportData data) {
        StringBuilder builder = new StringBuilder();
        builder.append(String.join(",", data.getHeaders())).append(" ");
        for (List<String> row : data.getRows()) {
            builder.append(String.join(",", row)).append(" ");
        }
        return builder.toString();
    }
}

