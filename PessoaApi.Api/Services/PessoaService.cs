using Microsoft.EntityFrameworkCore;
using PessoaApi.Api.Data;
using PessoaApi.Api.Domain;
using PessoaApi.Api.Dtos;

namespace PessoaApi.Api.Services;

public class PessoaService : IPessoaService
{
    private readonly AppDbContext _context;

    public PessoaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PessoaResponseDto>> ListarAsync()
    {
        return await _context.Pessoas
            .Select(p => new PessoaResponseDto(p.Id, p.Nome, p.Idade))
            .ToListAsync();
    }

    public async Task<PessoaResponseDto> CriarAsync(PessoaCreateDto dto)
    {
        var pessoa = new Pessoa
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Idade = dto.Idade
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        return new PessoaResponseDto(pessoa.Id, pessoa.Nome, pessoa.Idade);
    }

    public async Task<PessoaResponseDto?> AtualizarAsync(Guid id, PessoaCreateDto dto)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa is null) return null;

        pessoa.Nome = dto.Nome;
        pessoa.Idade = dto.Idade;
        await _context.SaveChangesAsync();

        return new PessoaResponseDto(pessoa.Id, pessoa.Nome, pessoa.Idade);
    }

    public async Task<bool> DeletarAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa is null) return false;

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
        return true;
    }
}