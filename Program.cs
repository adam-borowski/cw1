using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(args[0]);
            if (response.IsSuccessStatusCode) {
                var html = await response.Content.ReadAsStringAsync();
                var rx = new Regex("[a-zA-Z0-9\\.\\-_]+@[a-zA-Z0-9\\-_]+\\.[a-zA-Z0-9\\.]+");
                var matches = rx.Matches(html);
                foreach (var match in matches) {
                    System.Console.WriteLine(match);
                }
            }
        }
    }
}
