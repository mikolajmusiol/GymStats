namespace GymStats.Data.Entities
{
    public class GymBroStats
    {
        public TimeSpan UserAvgTimeSpent { get; private set; }
        public TimeSpan UserAvgTimeIn { get; private set; }
        public TimeSpan UserAvgTimeOut { get; private set; }
        public int UserTotalHoursSpent { get; private set; }
        public int UserTotalEntries { get; private set; }
        public int UserTotalMoneySpent { get; private set; }
        public DateTime? UserFirstWorkoutDate { get; private set; }
        public int UserAvgWorkoutsWeekly { get; private set; }
        public DateTime UserLastLoginDate { get; private set; }

        private DateTime? _userLastWorkoutDate;
        private const int REGISTRATION_FEE = 39;

        public GymBroStats(GymBro gymBro, List<Entry> entries)
        {
            _userLastWorkoutDate = entries.LastOrDefault(x => x != null).EntryDate.Value;
            UserFirstWorkoutDate = entries.FirstOrDefault(x => x != null).EntryDate.Value;

            UserAvgTimeSpent = CalculationsHelper.DoubleToTimeSpan((double)CalcTotalHoursSpent(entries) / (double)entries.Count);
            UserAvgTimeIn = CalcAvgTimeSpan(entries, x => x.EntryTimeIn);
            UserAvgTimeOut = CalcAvgTimeSpan(entries, x => x.EntryTimeOut);

            UserTotalHoursSpent = CalcTotalHoursSpent(entries);
            UserTotalEntries = entries.Count;
            UserTotalMoneySpent = CalcTotalMoney(gymBro);
            UserAvgWorkoutsWeekly = CalcAvgWorkoutsWeekly(entries);
            UserLastLoginDate = DateTime.Now;
        }

        public GymBroStats() { }

        private TimeSpan CalcAvgTimeSpan(List<Entry> entries, Func<Entry, DateTime?> property)
        {
            if (entries == null || entries.Count == 0)
            {
                return TimeSpan.Zero;
            }

            double sum = 0D;
            double minutesToFraction;
            TimeSpan time;

            foreach (Entry entry in entries)
            {
                if (entry.EntryDate == null || entry.EntryTimeIn == null || entry.EntryTimeOut == null)
                {
                    continue;
                }

                time = new TimeSpan(property(entry).Value.Hour, property(entry).Value.Minute, 0);

                minutesToFraction = (double)time.Minutes / 60D;
                sum += (double)time.Hours + minutesToFraction;
            }

            return CalculationsHelper.DoubleToTimeSpan((double)sum / (double)entries.Count);
        }


        private int CalcTotalHoursSpent(List<Entry> entries)
        {
            if (entries == null || entries.Count == 0)
            {
                return 0;
            }

            TimeSpan total = new TimeSpan();
            TimeSpan result;

            foreach (Entry entry in entries)
            {
                if (entry.EntryDate == null || entry.EntryTimeIn == null || entry.EntryTimeOut == null)
                {
                    continue;
                }

                result = entry.EntryTimeOut.Value - entry.EntryTimeIn.Value;

                if (entry.EntryTimeOut < entry.EntryTimeIn)
                {
                    result = new TimeSpan(24, 0, 0) + result;
                }

                total += result;
            }

            return Convert.ToInt32(total.TotalHours);
        }

        private int CalcTotalMoney(GymBro gymBro)
        {
            if (UserFirstWorkoutDate == null || _userLastWorkoutDate == null)
            {
                return 0;
            }

            double daysToAdd = CalculationsHelper.DaysToAdd(UserFirstWorkoutDate.Value);
            int firstDayCount = 0;

            for (DateTime date = UserFirstWorkoutDate.Value.AddDays(daysToAdd); date <= _userLastWorkoutDate; date = date.AddDays(1))
            {
                if (date.Day == 1)
                {
                    firstDayCount++;
                }
            }

            int priceBeforeFirstDay = CalculationsHelper.CalcBeforeFirstDay(gymBro.UserPassPrice, UserFirstWorkoutDate.Value, daysToAdd);

            return priceBeforeFirstDay + (firstDayCount * gymBro.UserPassPrice) + REGISTRATION_FEE;
        }

        private int CalcAvgWorkoutsWeekly(List<Entry> entries)
        {
            if (UserFirstWorkoutDate == null || _userLastWorkoutDate == null)
            {
                return 0;
            }
            else return Convert.ToInt32((double)entries.Count / ((_userLastWorkoutDate - UserFirstWorkoutDate).Value.TotalDays / 7));
        }
    }
}