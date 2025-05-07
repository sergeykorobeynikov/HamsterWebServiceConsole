using System.ServiceModel;

namespace HamsterWebServiceConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Exec();
        }

        private static async void Exec()
        {
            Console.WriteLine("Hello, World!");

            var binding = new BasicHttpBinding
            {
                MaxBufferPoolSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
                AllowCookies = true,
                SendTimeout = new TimeSpan(0, 0, 1, 0, 0),
                ReceiveTimeout = new TimeSpan(0, 0, 1, 0, 0),
            };
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            var endpoint = new EndpointAddress("https://ol2.saas.rarus.ru/0/ws/hamster.1cws");
            var oneC = new XyloCode.OneC.hamsterPortTypeClient(binding, endpoint);
            oneC.ClientCredentials.UserName.UserName = "";
            oneC.ClientCredentials.UserName.Password = "";
            oneC.Open();

            var orgs = await oneC.GetOrganizationsAsync();
            foreach (var org in orgs)
            {
                Console.WriteLine("{0}: {1} / {2} / {3}", org.id, org.Name, org.TaxNumber, org.StateRegistrationNumber);
            }

            var staff = await oneC.GetEmployeesAsync();
            foreach (var employee in staff)
            {
                Console.WriteLine("{1}: {2} / {0} // {3}", employee.id, employee.Code, employee.Name, employee.Organization);
            }
            oneC.Close();

            Console.Beep();
            Console.WriteLine("the end!");
            Console.ReadLine();
        }
    }
}
