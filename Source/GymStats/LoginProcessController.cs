using GymStats.Data.Entities;
using GymStats.Data;
using GymStats.DataAccess;

namespace GymStats
{
    public class LoginProcessController
    {
        private string InputEmail { get; set; }
        private string InputPassword { get; set; }
        private BrowserOperations Browser { get; set; }

        private void InitializeBrowser()
        {
            Browser = new BrowserOperations();
        }

        public void PerformInitializationProcess()
        {
            InitializeBrowser();

            if (Browser.LogIn(InputEmail, InputPassword) == true)
            {
                DisplayDataModel dataToDisplay = ReturnDisplayModel();
                DisplayData(dataToDisplay);
                Browser.ShutDownBrowser();
            }
            else
            {
                Console.WriteLine("Error occurred");
                Browser.ShutDownBrowser();
            }
        }

        public bool TryCacheLoginData()
        {
            Console.Write("Provide email adress: ");
            InputEmail = Console.ReadLine();
            Console.Write("Provide your password: ");
            InputPassword = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(InputEmail) || String.IsNullOrWhiteSpace(InputPassword))
            {
                Console.WriteLine("Please provide valid login data");
                return false;
            }

            return true;
        }

        private DisplayDataModel ReturnDisplayModel()
        {
            DatabaseAccess database = new DatabaseAccess(new DatabaseConnection());
            EntriesCollector entriesCollector = new EntriesCollector();

            string entriesSource = Browser.GetEntriesSource();

            GymBro gymBro = new GymBro(InputEmail, Browser.GetGymBroSource(), entriesSource);

            List<Entry> entries = new List<Entry>(entriesCollector.GetGymBroEntries(entriesSource));

            GymBroStats stats = new GymBroStats(gymBro, entries);

            UpdateInDB(gymBro, entries, stats, database);

            var returnedStatsToDisplay = database.UpdateAllGymBrosStats();

            return new DisplayDataModel(gymBro, stats, returnedStatsToDisplay.Item1, returnedStatsToDisplay.Item2);
        }

        private void UpdateInDB(GymBro gymBro, List<Entry> entries, GymBroStats stats, DatabaseAccess database)
        {
            database.UpdateGymBro(gymBro);
            database.UpdateEntries(gymBro, entries);
            database.UpdateGymBroStats(gymBro, stats);
        }

        private void DisplayData(DisplayDataModel displayData)
        {
            Console.Clear();
            ConsoleDisplay.DisplayData(displayData);
            Console.ReadLine();
        }
    }
}