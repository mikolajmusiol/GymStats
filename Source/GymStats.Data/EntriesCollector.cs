using GymStats.Data.Entities;
using System.Text.RegularExpressions;

namespace GymStats.Data
{
    public class EntriesCollector
    {
        public List<Entry> GetGymBroEntries(string entriesSource)
        {
            string divToParse = FindEntryHistoryElement(entriesSource);

            List<string> tableRowList = GetTableRowList(divToParse);

            return InitializeEntryList(tableRowList);
        }

        private string FindEntryHistoryElement(string entriesSource)
        {
            Regex regexDiv = new Regex(@"(?<=<tbody>).*?(?=</tbody>)");
            Match matchDiv = regexDiv.Match(entriesSource);

            while (matchDiv.Value.Contains("Wejście") == false)
            {
                matchDiv = matchDiv.NextMatch();
            }

            return matchDiv.ToString();
        }

        private List<string> GetTableRowList(string divToParse)
        {
            List<string> tableRowList = new List<string>();

            Regex regexToList = new Regex(@"(?<=<tdclass=""user-calendar__td"">).*?(?=</tr>)");
            Match matchToList = regexToList.Match(divToParse);

            while (matchToList.Success)
            {
                tableRowList.Add(matchToList.ToString());
                matchToList = matchToList.NextMatch();
            }

            return tableRowList;
        }

        private List<Entry> InitializeEntryList(List<string> tableRowList)
        {
            List<Entry> entries = new List<Entry>();

            Regex regexRow = new Regex(@"(?<=<spanclass=""user-calendar__mobile-name"">.*?:</span>).*?(?=</td>)");

            DateTime? entryDate;
            DateTime? entryTimeIn;
            DateTime? entryTimeOut;
            DateTime?[] entryParameters;
            Match matchRow;

            foreach (string row in tableRowList)
            {
                entryDate = null;
                entryTimeIn = null;
                entryTimeOut = null;

                entryParameters = new[] { entryDate, entryTimeIn, entryTimeOut };

                matchRow = regexRow.Match(row);

                for (int i = 0; i < regexRow.Matches(row).Count - 1; i++)
                {
                    matchRow = matchRow.NextMatch();

                    if (matchRow.ToString() != String.Empty)
                    {
                        entryParameters[i] = DateTime.Parse(matchRow.Value.ToString());
                    }
                }

                entries.Add(new Entry(entryParameters[0], entryParameters[1], entryParameters[2]));
            }

            entries.Reverse();

            return entries;
        }
    }
}