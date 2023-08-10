using System.Data;
using Dapper;
using GymStats.Data.Entities;

namespace GymStats.DataAccess
{
    public class DatabaseAccess : IDataAccess
    {
        private readonly IDatabaseConnection _db;
        public DatabaseAccess(IDatabaseConnection database)
        {
            _db = database;
        }

        public void UpdateGymBro(GymBro gymBro)
        {
            using (IDbConnection connection = _db.GetConnection())
            {
                if (connection.ExecuteScalar<bool>("dbo.CheckIfInDB @UserEmail", gymBro) == true)
                {
                    connection.Execute("dbo.UpdateAgeIfChanged @UserEmail, @UserAge", gymBro);
                }
                else
                {
                    connection.Execute("dbo.AddGymBro @UserEmail, @UserName, @UserSurname, @UserAge, @UserPassPrice", gymBro);
                }
            }
        }

        public void UpdateEntries(GymBro gymBro, List<Entry> entries)
        {
            using (IDbConnection connection = _db.GetConnection())
            {
                List<Entry> entriesFromDB = connection.Query<Entry>("dbo.GetEntries @UserEmail", gymBro).ToList();
                List<Entry> entriesToUpdate = new List<Entry>();

                for (int i = entriesFromDB.IndexOf(entriesFromDB.LastOrDefault(x => x != null)) + 1; i < entries.Count; i++)
                {
                    entriesToUpdate.Add(entries[i]);
                }
                foreach (Entry entry in entriesToUpdate)
                {
                    connection.Execute("dbo.AddEntries @UserEmail, @EntryDate, @EntryTimeIn, @EntryTimeOut",
                                       new
                                       {
                                           UserEmail = gymBro.UserEmail,
                                           EntryDate = entry.EntryDate,
                                           EntryTimeIn = entry.EntryTimeIn,
                                           EntryTimeOut = entry.EntryTimeOut
                                       });
                }
            }
        }

        public void UpdateGymBroStats(GymBro gymBro, GymBroStats gymBroStats)
        {
            using (IDbConnection connection = _db.GetConnection())
            {
                connection.Execute("dbo.UpdateGymBroStats @UserEmail, @UserAvgTimeSpent, @UserAvgTimeIn, " +
                                   "@UserAvgTimeOut, @UserTotalHoursSpent, @UserTotalEntries, @UserTotalMoneySpent, " +
                                   "@UserFirstWorkoutDate, @UserAvgWorkoutsWeekly, @UserLastLoginDate",
                                   new
                                   {
                                       UserEmail = gymBro.UserEmail,
                                       UserAvgTimeSpent = gymBroStats.UserAvgTimeSpent,
                                       UserAvgTimeIn = gymBroStats.UserAvgTimeIn,
                                       UserAvgTimeOut = gymBroStats.UserAvgTimeOut,
                                       UserTotalHoursSpent = gymBroStats.UserTotalHoursSpent,
                                       UserTotalEntries = gymBroStats.UserTotalEntries,
                                       UserTotalMoneySpent = gymBroStats.UserTotalMoneySpent,
                                       UserFirstWorkoutDate = gymBroStats.UserFirstWorkoutDate,
                                       UserAvgWorkoutsWeekly = gymBroStats.UserAvgWorkoutsWeekly,
                                       UserLastLoginDate = gymBroStats.UserLastLoginDate
                                   });
            }
        }

        public Tuple<AllGymBrosStats, List<GymBroStats>> UpdateAllGymBrosStats()
        {
            using (IDbConnection connection = _db.GetConnection())
            {
                List<GymBroStats> stats = connection.Query<GymBroStats>("dbo.GetAllGymBrosStats").ToList();

                List<GymBro> gymBros = connection.Query<GymBro>("dbo.GetGymBros").ToList();

                AllGymBrosStats allStats = new AllGymBrosStats(stats, gymBros);

                connection.Execute("dbo.UpdateAllGymBrosStats @AllAvgTimeSpent, @AllAvgTimeIn, @AllAvgTimeOut, " +
                                   "@AllAvgAge, @AllAvgPassPrice, @AllAvgWorkoutsWeekly, @AllAvgTotalHours, @AllAvgMoneySpent", allStats);

                return new Tuple<AllGymBrosStats, List<GymBroStats>>(allStats, stats);
            }
        }
    }
}