using PessoaApi.Api.Dtos;

namespace PessoaApi.Api.Services;

public interface IPessoaService
{
    Task<List<PessoaResponseDto>> ListarAsync();
    Task<PessoaResponseDto> CriarAsync(PessoaCreateDto dto);
    Task<PessoaResponseDto?> AtualizarAsync(Guid id, PessoaCreateDto dto);
    Task<bool> DeletarAsync(Guid id);
}