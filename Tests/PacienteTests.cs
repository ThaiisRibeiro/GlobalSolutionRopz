using Xunit;
using System;
using GlobalSolutionRopz.Model;


public class PacienteTests
{
    [Fact]
    public void EmailSemArroba_DeveLancarExcecao()
    {
        var paciente = new Paciente();

        var ex = Assert.Throws<ArgumentException>(() => paciente.email = "testeemail.com");
        Assert.Equal("E-mail inválido. Deve conter '@'.", ex.Message);
    }

    [Fact]
    public void CpfCnpjComLetras_DeveLancarExcecao()
    {
        var paciente = new Paciente();

        var ex = Assert.Throws<ArgumentException>(() => paciente.cpf_cnpj = "12A45678B001CC");
        Assert.Equal("CPF/CNPJ inválido. Deve conter apenas números e ter 11 (CPF) ou 14 (CNPJ) dígitos.", ex.Message);
    }

    [Fact]
    public void TelefoneInvalido_DeveLancarExcecao()
    {
        var paciente = new Paciente();

        var ex = Assert.Throws<ArgumentException>(() => paciente.telefone = "234567");
        Assert.Equal("Telefone inválido. Deve ter 11 dígitos e começar com 9 ou 1.", ex.Message);
    }

    [Fact]
    public void DeveCriarPacienteComDadosValidos()
    {
        var paciente = new Paciente
        {
            nome = "Thais Ribeiro",
            email = "thais@teste.com",
            telefone = "91234567890",
            cpf_cnpj = "12345678000199" // ou 11 dígitos para CPF
        };

        Assert.Equal("Thais Ribeiro", paciente.nome);
        Assert.Equal("thais@teste.com", paciente.email);
        Assert.Equal("91234567890", paciente.telefone);
        Assert.Equal("12345678000199", paciente.cpf_cnpj);
    }
}
