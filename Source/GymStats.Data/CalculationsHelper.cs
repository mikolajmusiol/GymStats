namespace GymStats.Data
{
    public static class CalculationsHelper
    {
        public static TimeSpan DoubleToTimeSpan(double argDouble)
        {
            double hours = Math.Truncate(argDouble);
            double fractionalpart = argDouble - hours;
            double minutes = fractionalpart * 60;
            return new TimeSpan(Convert.ToInt32(hours), Convert.ToInt32(minutes), 0);
        }

        public static double DaysToAdd(DateTime date)
        {
            int increment = 0;
            for (DateTime dateIncremented = date; dateIncremented.Month != date.AddMonths(1).Month; dateIncremented = dateIncremented.AddDays(1))
            {
                increment++;
            }

            return increment;
        }

        public static int CalcBeforeFirstDay(int GymBroPassPrice, DateTime date, double daysToAdd)
        {
            return Convert.ToInt32(((double)GymBroPassPrice / DateTime.DaysInMonth(date.Year, date.Month)) * daysToAdd);
        }

        public static int CalculateAge(DateTime birthdate)
        {
            int age = DateTime.Today.Year - birthdate.Year;
            if (birthdate.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}