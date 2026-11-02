package variant7_employeemanagement;

// Опис предметної області: Управління банківськими рахунками клієнтів.
// Завдання коду: Забезпечити зняття та поповнення коштів на рахунках, переказ між рахунками та розрахунок відсотків.

public class Employee {
        private String employeeId;
        private String fullName;

        public Employee(String employeeId, String fullName) {
            this.employeeId = employeeId;
            this.fullName = fullName;
        }

        public String getEmployeeId() { return employeeId; }
        public String getFullName() { return fullName;
        }
}


