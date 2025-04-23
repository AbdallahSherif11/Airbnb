using Airbnb.Core.DTOs.SmartSearchDTOs;
using Airbnb.Core.Services.Contract.SmartSearchService.Contract;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Airbnb.Service.Services.SearchService
{
    public class OllamaService : IOllamaService
    {
        private readonly HttpClient _httpClient;

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FilterResult> ExtractFiltersFromPromptAsync(string prompt)
        {
            var refinedPrompt =
                    "Extract only the explicitly mentioned or clearly implied filters from the user query below. " +
                    "Return a valid JSON object using the following structure, and nothing else:\n\n" +
                    "{\n" +
                    "  \"country\": string | null,\n" +
                    "  \"city\": string | null,\n" +
                    "  \"rooms\": number | null,\n" +
                    "  \"beds\": number | null,\n" +
                    "  \"minPrice\": number | null,\n" +
                    "  \"maxPrice\": number | null,\n" +
                    "  \"amenities\": string[] | null,\n" +
                    "  \"houseView\": string | null\n" +
                    "}\n\n" +
                    "- Do not assume or fabricate any value.\n" +
                    "- You may infer the number of beds if the number of people is clearly stated or implied (e.g., \"me and my friend\" → beds: 2).\n" +
                    "- If any field is not mentioned or implied, return null.\n" +
                    "- Correct obvious spelling mistakes in country or city names.\n" +
                    "- Return only raw JSON without any comments, formatting, or explanation.\n\n" +
                    $"Query: {prompt}";





            var request = new
            {
                model = "llama3",
                prompt = refinedPrompt,
                stream = false
            };

            var response = await _httpClient.PostAsJsonAsync("https://7233-197-54-31-93.ngrok-free.app/api/generate", request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<OllamaResponse>(responseString);
            var rawJson = parsed?.response?.Trim();

            Console.WriteLine("=== Raw Ollama JSON ===");
            Console.WriteLine(rawJson);
            Console.WriteLine("=======================");

            try
            {
                return JsonConvert.DeserializeObject<FilterResult>(rawJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialization error:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Offending JSON:");
                Console.WriteLine(rawJson);
                throw;
            }
        }

        private class OllamaResponse
        {
            public string response { get; set; }
        }
    }
}
