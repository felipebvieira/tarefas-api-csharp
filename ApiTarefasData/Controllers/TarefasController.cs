using ApiTarefasData.Database;
using ApiTarefasData.Models;
using ApiTarefasData.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace ApiTarefasData.Controllers;

[ApiController]
[Route("/tarefas")]
public class TarefasController : ControllerBase
{
    public TarefasController(TarefaContext db)
    {
        _db = db;
    }

    private TarefaContext _db;

[HttpGet]
public IActionResult Index()
{
    var todasAsTarefas = _db.Tarefas.ToList();
    return StatusCode(200, todasAsTarefas);
}

[HttpGet("nao-concluidas")]
public IActionResult IndexNaoConcluidas()
{
    var tarefasNaoConcluidas = _db.Tarefas
                                .Where(t => !t.Concluida)
                                .OrderBy(t => t.DataConclusao)
                                .ToList();

    return StatusCode(200, tarefasNaoConcluidas);
}

    [HttpPost]
    public IActionResult Create([FromBody] Tarefa tarefa)
    {
        if(string.IsNullOrEmpty(tarefa.Titulo))
        {
            return StatusCode(400, new ErroView {Mensagem = "Titulo é Obrigatório"});
        }

        _db.Tarefas.Add(tarefa);
        _db.SaveChanges();

        return StatusCode(201, tarefa);
    }

    [HttpGet("{id}")]
    public IActionResult Show([FromRoute] int id)
    {
        var tarefaDb = _db.Tarefas.Find(id);
        if(tarefaDb == null)
        {
            return StatusCode(404, new ErroView {Mensagem = $"ID ({id}) não encontrado"});
        }

        return StatusCode(200, tarefaDb);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Tarefa tarefa)
    {
        if(string.IsNullOrEmpty(tarefa.Titulo))
        {
            return StatusCode(400, new ErroView {Mensagem = "Titulo é Obrigatório"});
        }

        var tarefaDb = _db.Tarefas.Find(id);
        if(tarefaDb == null)
        {
            return StatusCode(404, new ErroView {Mensagem = $"ID ({id}) não encontrado"});
        }

        tarefaDb.Titulo = tarefa.Titulo;
        tarefaDb.Descricao = tarefa.Descricao;
        tarefaDb.DataConclusao = tarefa.DataConclusao;
        tarefaDb.Concluida = tarefa.Concluida;

        _db.Tarefas.Update(tarefaDb);
        _db.SaveChanges();

        return StatusCode(200, tarefaDb);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var tarefaDb = _db.Tarefas.Find(id);
        if(tarefaDb == null)
        {
            return StatusCode(404, new ErroView {Mensagem = $"ID ({id}) não encontrado"});
        };

        _db.Tarefas.Remove(tarefaDb);
        _db.SaveChanges();

        return StatusCode(204);
    }
}
