using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;    
using System.IO;
using System.Net.Http;
using System.Text;
public interface IAIService
{
    Task<string> ProcessPhotoWithTextAsync(IFormFile photo, string prompt);
}

public class AIService : IAIService
{
    private readonly string _apiKey = "****************************************";
    private readonly string _apiUrl = "****************************************";

    public async Task<string> ProcessPhotoWithTextAsync(IFormFile photo, string prompt)
    {
        using var client = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(5)
        };
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _apiKey);

        using var memoryStream = new MemoryStream();
        await photo.CopyToAsync(memoryStream);
        var base64Image = Convert.ToBase64String(memoryStream.ToArray());

        var payload = new
        {
            version = "************************",
            input = new
            {
                image = $"data:image/png;base64,{base64Image}",
                prompt = prompt,
                strength = 0.5
            }
        };

        var jsonPayload = JsonConvert.SerializeObject(payload);
        var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(_apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var prediction = JsonConvert.DeserializeObject<dynamic>(responseBody);

            var predictionUrl = prediction.urls.get.ToString();
            while (true)
            {
                await Task.Delay(2000);

                var pollResponse = await client.GetAsync(predictionUrl);
                var pollResponseBody = await pollResponse.Content.ReadAsStringAsync();
                var pollResult = JsonConvert.DeserializeObject<dynamic>(pollResponseBody);

                Console.WriteLine($"DEBUG: Polling Response = {pollResponseBody}");

                if (pollResult.status == "succeeded")
                {
                    return pollResult.output.ToString();
                }
                else if (pollResult.status == "failed")
                {
                    throw new Exception("Prediction failed. Check your inputs.");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error calling Replicate API: {ex.Message}");
        }
    }
} 