import { useState, useEffect } from "react";
import type  { Pessoa, PessoaCreateDto } from "./types";
import { listarPessoas, criarPessoa, atualizarPessoa, deletarPessoa } from "./api";

function App() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [editandoId, setEditandoId] = useState<string | null>(null);

  useEffect(() => {
    carregarPessoas();
  }, []);

  async function carregarPessoas() {
    const dados = await listarPessoas();
    setPessoas(dados);
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    const dto: PessoaCreateDto = { nome, idade: Number(idade) };

    if (editandoId) {
      await atualizarPessoa(editandoId, dto);
      setEditandoId(null);
    } else {
      await criarPessoa(dto);
    }

    setNome("");
    setIdade("");
    await carregarPessoas();
  }

  function handleEditar(pessoa: Pessoa) {
    setNome(pessoa.nome);
    setIdade(String(pessoa.idade));
    setEditandoId(pessoa.id);
  }

  async function handleDeletar(id: string) {
    await deletarPessoa(id);
    await carregarPessoas();
  }

  return (
    <div>
      <h1>Pessoas</h1>

      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
        />
        <input
          type="number"
          placeholder="Idade"
          value={idade}
          onChange={(e) => setIdade(e.target.value)}
        />
        <button type="submit">{editandoId ? "Atualizar" : "Criar"}</button>
      </form>

      <table>
        <thead>
          <tr>
            <th>Nome</th>
            <th>Idade</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.map((pessoa) => (
            <tr key={pessoa.id}>
              <td>{pessoa.nome}</td>
              <td>{pessoa.idade}</td>
              <td>
                <button onClick={() => handleEditar(pessoa)}>Editar</button>
                <button onClick={() => handleDeletar(pessoa.id)}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;