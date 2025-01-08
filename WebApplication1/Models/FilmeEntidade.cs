public class FilmeEntidade
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string DataLancamento { get; set; }
    public string Sinopse { get; set; }
    public string CaminhoPoster { get; set; }
    public double MediaVotos { get; set; }
}
public List<GeneroEntidade> Generos { get; set; } } public class GeneroEntidade
{
    public int Id { get; set; }
    public string Nome { get; set; }
}
