using Newtonsoft.Json;
using System.Net.Http.Headers;

public class TMDbClient
{
    private readonly HttpClient _httpClient;
    private const string apiKey = "f46f677dacac1ef7e32a77eaefd8d181";

    public TMDbClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool IsSuccess, string ErrorMessage, Filme Filme)> ObterFilme(int filmeId)
    {
        var requestUrl = $"https://api.themoviedb.org/3/movie/{filmeId}?api_key={apiKey}";

        try
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var filme = JsonConvert.DeserializeObject<Filme>(responseData);
                return (true, string.Empty, filme ?? new Filme());
            }
            else
            {
                return (false, $"Erro ao buscar dados: {response.ReasonPhrase}", null);
            }
        }
        catch (Exception e)
        {
            return (false, $"Erro desconhecido: {e.Message}", null);
        }
    }

    public async Task<List<Filme>> ObterFilmesPopulares()
    {
        var requestUrl = $"https://api.themoviedb.org/3/movie/popular?api_key={apiKey}";

        try
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var movieResponse = JsonConvert.DeserializeObject<MovieResponse>(responseData);
                return movieResponse.Results;
            }
            else
            {
                return new List<Filme>();
            }
        }
        catch (Exception e)
        {
            return new List<Filme> { new Filme { Title = $"Erro desconhecido: {e.Message}" } };
        }
    }

    public class MovieResponse
    {
        [JsonProperty("results")]
        public List<Filme> Results { get; set; }
    }
}
