using Microsoft.EntityFrameworkCore;
using PessoaApi.Api.Domain;

namespace PessoaApi.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; }
}