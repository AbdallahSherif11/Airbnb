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
                    "Return a valid JSON object with the following structure, and nothing else:\n\n" +
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
                    "- Do not assume any value unless the query clearly hints it.\n" +
                    "- Infer user preferences from indirect expressions (e.g., if the user mentions enjoying cooking, infer a preference for a kitchen).\n" +
                    "- Identify implied amenities from the user’s wording, even if not explicitly listed.\n" +
                    "- Correct obvious misspellings in locations.\n" +
                    "- Output only the raw JSON—no comments, no markdown, no explanations.\n\n" +
                    $"Query: {prompt}";




            var request = new
            {
                model = "llama3",
                prompt = refinedPrompt,
                stream = false
            };

            var response = await _httpClient.PostAsJsonAsync("http://localhost:11434/api/generate", request);
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
