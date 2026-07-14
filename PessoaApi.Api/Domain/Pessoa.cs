namespace PessoaApi.Api.Domain;

public class Pessoa
{
    public Guid Id { get; set; }
    public string Nome {get; set; } = string.Empty;

    public int Idade {get;set; }
}