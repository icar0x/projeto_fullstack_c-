import type { Pessoa, PessoaCreateDto } from "./types";

const BASE_URL = "http://localhost:5070/api/PessoaControler";

export async function listarPessoas(): Promise<Pessoa[]> {
  const response = await fetch(BASE_URL);
  return response.json();
}

export async function criarPessoa(dto: PessoaCreateDto): Promise<Pessoa> {
  const response = await fetch(BASE_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(dto),
  });
  return response.json();
}

export async function atualizarPessoa(id: string, dto: PessoaCreateDto): Promise<Pessoa> {
  const response = await fetch(`${BASE_URL}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(dto),
  });
  return response.json();
}

export async function deletarPessoa(id: string): Promise<void> {
  await fetch(`${BASE_URL}/${id}`, { method: "DELETE" });
}