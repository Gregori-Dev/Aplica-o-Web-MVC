using Newtonsoft.Json;

namespace Domain.Entidades
{
    public class Filme
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("titulo")]
        public string Titulo { get; set; }

        [JsonProperty("data_lancamento")]
        public string DataLancamento { get; set; }

        [JsonProperty("sinopse")]
        public string Sinopse { get; set; }

        [JsonProperty("caminho_poster")]
        public string CaminhoPoster { get; set; }

        [JsonProperty("media_votos")]
        public double MediaVotos { get; set; }

        [JsonProperty("generos")]
        public List<Genero> Genero { get; set; }
    }
}