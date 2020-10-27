using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0) {
                throw new ArgumentNullException();
            }

            var httpClient = new HttpClient();
            try {
                
                var response = await httpClient.GetAsync(args[0]);
                if (response.IsSuccessStatusCode) {
                    var html = await response.Content.ReadAsStringAsync();
                    var rx = new Regex("[a-zA-Z0-9\\.\\-_]+@[a-zA-Z0-9\\-_]+\\.[a-zA-Z0-9\\.]+");
                    HashSet<String> emails = new HashSet<String>();
                    var matches = rx.Matches(html);

                    if (matches.Count == 0) {
                        System.Console.WriteLine("Nie znaleziono adresów email.");
                    } else {
                        foreach (var match in matches) {
                            emails.Add(match.ToString());
                        }
                        foreach (String email in emails) {
                            System.Console.WriteLine(email);
                        }
                    }
                }
            } catch (InvalidOperationException) {
                throw new ArgumentException();
            } catch (HttpRequestException) {
                System.Console.WriteLine("Błąd w czasie pobierania strony");
            } finally {
                httpClient.Dispose();
            }
        }
    }
}
