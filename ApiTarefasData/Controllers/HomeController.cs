using ApiTarefasData.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace ApiTarefasData.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public HomeView Index()
    {
        return new HomeView
        {
            Mensagem = "Bem vindo a API de Tarefas",
            Documentacao = "/swagger"
        };
    }
}
