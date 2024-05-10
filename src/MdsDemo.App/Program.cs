using System.Net.Http.Headers;

var token = Environment.GetCommandLineArgs().SkipWhile(arg => arg != "--token").Skip(1).FirstOrDefault();
if (string.IsNullOrEmpty(token))
    throw new InvalidOperationException("Token was not specified");

var request = new HttpRequestMessage(HttpMethod.Get, "https://api.lyft.com/v2/mds/SFO/trips?end_time=2024-05-01T10");
request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/vnd.mds+json;version=2.0"));

using var httpClient = new HttpClient();
var response = await httpClient.SendAsync(request);
var jsonString = await response.Content.ReadAsStringAsync();

Console.WriteLine(jsonString);
