using Newtonsoft.Json;

namespace Domain.Entidades
{
    public class Genero
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Nome { get; set; }

    }
}
