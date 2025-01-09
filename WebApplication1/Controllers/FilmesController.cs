using Microsoft.AspNetCore.Mvc;

public class FilmesController : Controller
{
    private readonly TMDbClient _tmdbClient;
    private readonly FilmeServico _filmeServico;

    public FilmesController(TMDbClient tmdbClient, FilmeServico filmeServico)
    {
        _tmdbClient = tmdbClient;
        _filmeServico = filmeServico;
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

    [HttpPost, Route("filmes/salvarfilme")]
    public async Task<IActionResult> SalvarFilme(Filme filme)
    {
        try
        {
            await _filmeServico.SalvarFilme(filme); 
            TempData["SuccessMessage"] = "Filme salvo com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage = $"Erro ao salvar o filme: {e.Message}";
            return View("Details", filme);
        }
    }

    public async Task<IActionResult> FilmesSalvos()
    {
        try
        {
            var filmes = await _filmeServico.ObterFilmesSalvos();
            return View("FilmeSalvos", filmes);
        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage = $"Erro ao obter filmes salvos: {e.Message}";
            return View("Error");
        }
    }
}
