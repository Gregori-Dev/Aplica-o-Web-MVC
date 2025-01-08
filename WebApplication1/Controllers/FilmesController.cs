using Microsoft.AspNetCore.Mvc;

public class FilmesController : Controller
{
    private readonly TMDbClient _tmdbClient;

    public FilmesController()
    {
        _tmdbClient = new TMDbClient();
    }

    [Route("filmes")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var filmes = await _tmdbClient.ObterFilmesPopulares();
            return View(filmes);
        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage = $"Erro ao obter filmes populares: {e.Message}";
            return View("Error");
        }
    }

    [Route("filmes/details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var result = await _tmdbClient.ObterFilme(id);
            if (result.IsSuccess)
            {
                return View(result.Filme);
            }
            else
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                return View("Error");
            }
        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage = $"Erro ao obter detalhes do filme: {e.Message}";
            return View("Error");
        }
    }
}
