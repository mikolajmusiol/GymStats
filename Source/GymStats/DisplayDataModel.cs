using GymStats.Data.Entities;

namespace GymStats
{
    public class DisplayDataModel
    {
        public string Name { get; private set; }
        public int UserAge { get; private set; }
        public int AvgUserAge { get; private set; }

        public DateOnly? FirstWorkoutDate { get; private set; }
        public int TotalEntries { get; private set; }
        public int EntriesPercentage { get; private set; }
        public int AvgWorkoutsWeekly { get; set; }
        public int TotalHours { get; private set; }
        public int AvgTotalHours { get; private set; }

        public TimeOnly AvgTimeIn { get; private set; }
        public TimeOnly RushHoursIn { get; private set; }
        public TimeOnly RushHoursOut { get; private set; }
        public TimeSpan AvgTimeSpent { get; private set; }
        public TimeSpan AllAvgTimeSpent { get; private set; }

        public int PassPrice { get; private set; }
        public int AvgPassPrice { get; private set; }
        public int TotalMoneySpent { get; private set; }
        public int AvgTotalMoneySpent { get; private set; }

        public DisplayDataModel(GymBro gymBro, GymBroStats gymBroStats, AllGymBrosStats allStats, List<GymBroStats> listofStats)
        {
            Name = gymBro.UserName;
            UserAge = gymBro.UserAge;
            AvgUserAge = allStats.AllAvgAge;

            FirstWorkoutDate = DateOnly.FromDateTime(gymBroStats.UserFirstWorkoutDate.Value);
            TotalEntries = gymBroStats.UserTotalEntries;
            EntriesPercentage = CalcEntriesPercentage(gymBroStats, listofStats);
            AvgWorkoutsWeekly = gymBroStats.UserAvgWorkoutsWeekly;
            TotalHours = gymBroStats.UserTotalHoursSpent;
            AvgTotalHours = allStats.AllAvgTotalHours;

            AvgTimeIn = TimeOnly.FromTimeSpan(gymBroStats.UserAvgTimeIn);
            RushHoursIn = TimeOnly.FromTimeSpan(allStats.AllAvgTimeIn);
            RushHoursOut = TimeOnly.FromTimeSpan(allStats.AllAvgTimeOut);
            AvgTimeSpent = gymBroStats.UserAvgTimeSpent;
            AllAvgTimeSpent = allStats.AllAvgTimeSpent;

            PassPrice = gymBro.UserPassPrice;
            AvgPassPrice = allStats.AllAvgPassPrice;
            TotalMoneySpent = gymBroStats.UserTotalMoneySpent;
            AvgTotalMoneySpent = allStats.AllAvgMoneySpent;
        }

        private int CalcEntriesPercentage(GymBroStats gymBroStats, List<GymBroStats> listofStats)
        {
            double numOfUsersWithLessEntriesThanUser = listofStats.Where(x => x.UserTotalEntries < gymBroStats.UserTotalEntries).Count();
            double percentage = (numOfUsersWithLessEntriesThanUser / (double)listofStats.Count) * 100;
            return Convert.ToInt32(Math.Round(percentage, 1));
        }
    }
}