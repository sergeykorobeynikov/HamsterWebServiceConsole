using System.ServiceModel;

namespace HamsterWebServiceConsole
{
    internal class Program
    {
        static void Main(string[] args)
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
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            var endpoint = new EndpointAddress("https://ol2.saas.rarus.ru/6ea59198/ws/hamster.1cws");
            var oneC = new XyloCode.OneC.hamsterPortTypeClient(binding, endpoint);
            oneC.ClientCredentials.UserName.UserName = "";
            oneC.ClientCredentials.UserName.Password = "";
            oneC.Open();

            var orgs = oneC.GetOrganizations();
            foreach (var org in orgs)
            {
                Console.WriteLine("{0}: {1} (ИНН: {2}; ОГРН: {3})", org.id, org.Name, org.TaxNumber, org.StateRegistrationNumber);
            }

            var staff = oneC.GetEmployees();
            foreach (var employee in staff)
            {
                Console.WriteLine("{0}: {1} - {2} // {3}", employee.id, employee.Code, employee.Name, employee.Organization);
            }
            oneC.Close();

            Console.Beep();
            Console.WriteLine("the end!");
            Console.ReadLine();
        }
    }
}
