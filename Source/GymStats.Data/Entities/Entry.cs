namespace GymStats.Data.Entities
{
    public class Entry
    {
        public DateTime? EntryDate { get; private set; }
        public DateTime? EntryTimeIn { get; private set; }
        public DateTime? EntryTimeOut { get; private set; }

        public Entry(DateTime? entryDate, DateTime? entryTimeIn, DateTime? entryTimeOut)
        {
            EntryDate = entryDate;
            EntryTimeIn = entryTimeIn;
            EntryTimeOut = entryTimeOut;
        }

        public Entry() { }
    }
}