using ApiTarefasData.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTarefasData.Database;

public class TarefaContext : DbContext
{
    #nullable disable
    public TarefaContext(DbContextOptions<TarefaContext> options) : base(options){}

    public DbSet<Tarefa> Tarefas {get;set;}
}