using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTarefasData.Models;

[Table("Tarefas")]
public class Tarefa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}

    [Required]
    [StringLength(100)]
    public string Titulo {get;set;} = default!;

    [Column(TypeName = "text")]
    public string Descricao {get;set;} = default!;

    [DataType(DataType.Date)]
    [Column(TypeName = "datetime")]
    public DateTime DataConclusao {get;set;}

    public bool Concluida {get;set;}
}