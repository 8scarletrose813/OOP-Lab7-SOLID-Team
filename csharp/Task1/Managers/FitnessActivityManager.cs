using Variant7_FitnessTracker.Models;
using Variant7_FitnessTracker.Services.Calculations;
using Variant7_FitnessTracker.Services.Notifications;
using Variant7_FitnessTracker.Services.Persistence;
using Variant7_FitnessTracker.Services.Validation;

// Опис предметної області: Мобільний додаток для фітнесу та спортивних тренувань.
// Завдання коду: Обробка даних забігу/тренування — валідація показань датчиків, розрахунок спалених калорій, середнього темпу та пульсових зон, формування підсумкового звіту активності у форматі JSON, збереження тренування у файл локальної бази та відправка пуш-сповіщення у мобільний профіль користувача.


namespace Variant7_FitnessTracker.Managers
{
    /// Клас-оркестратор, який координує виконання бізнес-процесу обробки тренування.
    public class FitnessActivityManager
    {
        private readonly IWorkoutValidator _validator;
        private readonly IWorkoutCalculator _calculator;
        private readonly IWorkoutRepository _repository;
        private readonly INotificationService _notificationService;

        public FitnessActivityManager(
            IWorkoutValidator validator,
            IWorkoutCalculator calculator,
            IWorkoutRepository repository,
            INotificationService notificationService)
        {
            _validator = validator;
            _calculator = calculator;
            _repository = repository;
            _notificationService = notificationService;
        }

        public void ProcessWorkoutSession(WorkoutSessionData session)
        {
            // 1. Валідація
            _validator.Validate(session);

            // 2. Бізнес-розрахунки
            WorkoutResult result = _calculator.Calculate(session);

            // 3. Збереження результатів
            _repository.Save(result);

            // 4. Сповіщення користувача
            _notificationService.SendPushNotification(session.DeviceToken, result.DistanceKm, result.Calories);
        }
    }
}