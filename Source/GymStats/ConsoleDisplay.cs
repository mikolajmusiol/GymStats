namespace GymStats;

public static class ConsoleDisplay
{
    public static void DisplayData(DisplayDataModel dataModel)
    {
        Dictionary<string, string> descriptions = new Dictionary<string, string>()
    {
        { "Name", "Imię"},
        { "UserAge", "Wiek"},
        { "AvgUserAge", "Średni wiek" },
        { "FirstWorkoutDate", "Data pierwszego treningu" },
        { "TotalEntries", "Wejść łącznie" },
        { "EntriesPercentage", "Procent osób, od ilu ma się więcej wejść" },
        { "AvgWorkoutsWeekly", "Średnia liczba treningów tygodniowo" },
        { "TotalHours", "Godzin łącznie" },
        { "AvgTotalHours", "Średnio godzin łącznie" },
        { "AvgTimeIn", "Średnia godzina wejścia" },
        { "RushHoursIn", "Godzina rozpoczęcia godzin szczytu" },
        { "RushHoursOut", "Godzina zakończenia godzin szczytu" },
        { "AvgTimeSpent", "Średni czas trwania treningu" },
        { "AllAvgTimeSpent", "Średni czas trwania treningu wszystkich osób" },
        { "PassPrice", "Cena karnetu" },
        { "AvgPassPrice", "Średnia cena karnetu" },
        { "TotalMoneySpent", "Pieniędze wydane na karnety" },
        { "AvgTotalMoneySpent", "Średnia ilość wydanych pieniędzy" }
    };

        var properties = dataModel.GetType().GetProperties();

        foreach (var property in properties)
        {
            string keyEqualToProp = descriptions.FirstOrDefault(x => x.Key == property.Name).Key;
            if (property.Name == keyEqualToProp)
            {
                Console.WriteLine($"{descriptions[keyEqualToProp]}: {property.GetValue(dataModel)}");
            }
        }
    }
}