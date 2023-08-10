using GymStats.Data.Entities;

namespace GymStats.DataAccess
{
    public interface IDataAccess
    {
        void UpdateGymBro(GymBro gymBro);
        void UpdateEntries(GymBro gymBro, List<Entry> entries);
        void UpdateGymBroStats(GymBro gymBro, GymBroStats gymBroStats);
        Tuple<AllGymBrosStats, List<GymBroStats>> UpdateAllGymBrosStats();
    }
}