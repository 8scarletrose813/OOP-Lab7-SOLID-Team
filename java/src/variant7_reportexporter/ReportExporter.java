package variant7_reportexporter;
public class ReportExporter {
    public String export(ReportData data, ExportFormat format) {
        StringBuilder builder = new StringBuilder();
        switch (format) {
            case PDF:
                builder.append("%PDF-1.7 HEADER ");
                builder.append("TITLE: ").append(data.getTitle()).append(" ");
                builder.append("GENERATED: ").append(data.getGeneratedAt().format(DateTimeFormatter.ISO_LOCAL_DATE)).append(" ");
                builder.append("[PDF Table Stream Rendered with ").append(data.getRows().size()).append(" rows] ");
                break;
                case EXCEL:
                    builder.append("<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"> ");
                    builder.append("  <Worksheet ss:Name="").append(data.getTitle()).append(""> ");
                    builder.append("    <!-- Rendered ").append(data.getRows().size()).append(" rows --> ");
                    builder.append("  </Worksheet> ");
                    builder.append("</Workbook> ");
                    break;
                    case CSV:
                        builder.append(String.join(",", data.getHeaders())).append(" ");
                        for (List<String> row : data.getRows()) {builder.append(String.join(",", row)).append(" ");
                }
                break;
                        case JSON:
                            builder.append("{ ");
                            builder.append("  "title": "").append(data.getTitle()).append("", ");
                            builder.append("  "generatedAt": "").append(data.getGeneratedAt()).append("", ");
                            builder.append("  "totalRows": ").append(data.getRows().size()).append(" ");
                            builder.append("} ");
                            break;
                            default: throw new UnsupportedOperationException("Формат експорту " + format + " не підтримується.");}
        System.out.println("[EXPORTER] Успішно створено звіт '" + data.getTitle() + "' у форматі " + format + ". Довжина: " + builder.length() + " символів.");
        return builder.toString();
    }
}