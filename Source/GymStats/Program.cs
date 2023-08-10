namespace GymStats
{
    public class Program
    {
        static void Main(string[] args)
        {
            LoginProcessController controller = new LoginProcessController();

            if (controller.TryCacheLoginData() == true)
            {
                controller.PerformInitializationProcess();
            }
        }
    }
}