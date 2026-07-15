using Microsoft.AspNetCore.Mvc;
using PessoaApi.Api.Dtos;
using PessoaApi.Api.Services;

namespace PessoaApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaControler : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoaControler(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PessoaResponseDto>>> Listar()
    {
        var pessoas = await _pessoaService.ListarAsync();
        return Ok(pessoas);
    }
    [HttpPost]
    public async Task<ActionResult<PessoaResponseDto>> Criar(PessoaCreateDto dto)
    {
        var pessoa = await _pessoaService.CriarAsync(dto);
        return CreatedAtAction(nameof(Listar), new {id = pessoa.Id }, pessoa);
    }

      [HttpPut("{id}")]
    public async Task<ActionResult<PessoaResponseDto>> Atualizar(Guid id, PessoaCreateDto dto)
    {
        var pessoa = await _pessoaService.AtualizarAsync(id, dto);
        if (pessoa is null) return NotFound();
        return Ok(pessoa);
    }

      [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        var deletado = await _pessoaService.DeletarAsync(id);
        if (!deletado) return NotFound();
        return NoContent();
    }
}