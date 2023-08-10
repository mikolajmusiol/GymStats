using System.Text.RegularExpressions;

namespace GymStats.Data.Entities
{
    public class GymBro
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public int UserAge { get; set; }
        public int UserPassPrice { get; set; }

        public GymBro(string email, string gymBroSource, string entriesSource)
        {
            AssignPersonalData(email, gymBroSource);
            GetPassPrice(entriesSource);
        }

        public GymBro() { }

        private void AssignPersonalData(string email, string gymBroSource)
        {
            Regex regexDiv = new Regex(@"(?<=<divclass=""user-data__list"">).*(?=<divclass=""user-data__list-head"">)");
            Match matchDiv = regexDiv.Match(gymBroSource);
            string divToParse = matchDiv.Value.ToString();

            Regex regex = new Regex(@"(?<=""user-data__list-content"">).*?(?=</div>)");
            Match match = regex.Match(divToParse);

            UserName = match.Value.ToString();
            match = match.NextMatch();
            UserSurname = match.Value.ToString();
            match = match.NextMatch();
            match = match.NextMatch();

            DateTime birthdate = DateTime.ParseExact(match.Value.ToString(), "dd.MM.yyyy", null);
            UserAge = CalculationsHelper.CalculateAge(birthdate);

            UserEmail = email;
        }

        private void GetPassPrice(string entriesSource)
        {
            Regex regexDiv = new Regex(@"(?<=<tbody>).*?(?=</tbody>)");
            Match matchDiv = regexDiv.Match(entriesSource);
            string divToParse = matchDiv.Value.ToString();

            Regex regexPrice = new Regex(@"(?<=Kosztkarnetu:</span>)(\d{2,3})\,00.*?(?=</td>)");
            Match matchPrice = regexPrice.Match(divToParse);
            UserPassPrice = Convert.ToInt32(matchPrice.Groups[1].ToString());
        }
    }
}