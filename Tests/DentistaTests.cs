using GlobalSolutionRopz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class DentistaTests
    {
        [Fact]
        public void EmailSemArroba_DeveLancarExcecao()
        {
            var dentista = new Dentista();

            var ex = Assert.Throws<ArgumentException>(() => dentista.email = "dentistaemail.com");
            Assert.Equal("E-mail inválido. Deve conter '@'.", ex.Message);
        }

        [Fact]
        public void CpfCnpjComLetras_DeveLancarExcecao()
        {
            var dentista = new Dentista();

            var ex = Assert.Throws<ArgumentException>(() => dentista.cpf = "123ABC4567DEF");
            Assert.Equal("CPF/CNPJ inválido. Deve conter apenas números e ter 11 (CPF) ou 14 (CNPJ) dígitos.", ex.Message);
        }

        [Fact]
        public void TelefoneInvalido_DeveLancarExcecao()
        {
            var dentista = new Dentista();

            var ex = Assert.Throws<ArgumentException>(() => dentista.telefone = "834567");
            Assert.Equal("Telefone inválido. Deve ter 11 dígitos e começar com 9 ou 1.", ex.Message);
        }

        [Fact]
        public void DeveCriarDentistaComDadosValidos()
        {
            var dentista = new Dentista
            {
                nome = "Dr. Bruno Costa",
                email = "bruno@teste.com",
                telefone = "91234567890",
                cpf = "12345678901",
                numero_cro = "CRO56789"
            };

            Assert.Equal("Dr. Bruno Costa", dentista.nome);
            Assert.Equal("bruno@teste.com", dentista.email);
            Assert.Equal("91234567890", dentista.telefone);
            Assert.Equal("12345678901", dentista.cpf);
            Assert.Equal("CRO56789", dentista.numero_cro);
        }
    }
}
