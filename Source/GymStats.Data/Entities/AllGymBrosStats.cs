namespace GymStats.Data.Entities
{
    public class AllGymBrosStats
    {
        public TimeSpan AllAvgTimeSpent { get; private set; }
        public TimeSpan AllAvgTimeIn { get; private set; }
        public TimeSpan AllAvgTimeOut { get; private set; }
        public int AllAvgAge { get; private set; }
        public int AllAvgPassPrice { get; private set; }
        public int AllAvgWorkoutsWeekly { get; private set; }
        public int AllAvgTotalHours { get; private set; }
        public int AllAvgMoneySpent { get; private set; }

        public AllGymBrosStats(List<GymBroStats> stats, List<GymBro> gymBros)
        {
            AllAvgTimeSpent = CalcAvgTimeSpan(stats, x => x.UserAvgTimeSpent);
            AllAvgTimeIn = CalcAvgTimeSpan(stats, x => x.UserAvgTimeIn);
            AllAvgTimeOut = CalcAvgTimeSpan(stats, x => x.UserAvgTimeOut);

            AllAvgAge = CalcAvgInt(gymBros, x => x.UserAge);
            AllAvgPassPrice = CalcAvgInt(gymBros, x => x.UserPassPrice);
            AllAvgWorkoutsWeekly = CalcAvgInt(stats, x => x.UserAvgWorkoutsWeekly);
            AllAvgTotalHours = CalcAvgInt(stats, x => x.UserTotalHoursSpent);
            AllAvgMoneySpent = CalcAvgInt(stats, x => x.UserTotalMoneySpent);
        }

        private TimeSpan CalcAvgTimeSpan<GymBroStats>(List<GymBroStats> stats, Func<GymBroStats, TimeSpan> property)
        {
            if (stats == null || stats.Count == 0)
            {
                return TimeSpan.Zero;
            }

            double sum = 0D;
            double minutesToFraction;

            foreach (GymBroStats record in stats)
            {
                minutesToFraction = (double)property(record).Minutes / 60D;
                sum += (double)property(record).Hours + minutesToFraction;
            }

            return CalculationsHelper.DoubleToTimeSpan((double)sum / (double)stats.Count);
        }

        private int CalcAvgInt<T>(List<T> objectList, Func<T, int> property) where T : class
        {
            if (objectList == null || objectList.Count == 0)
            {
                return 0;
            }

            double sum = 0D;

            foreach (var obj in objectList)
            {
                sum += (double)property(obj);
            }

            return Convert.ToInt32((double)sum / (double)objectList.Count);
        }
    }
}