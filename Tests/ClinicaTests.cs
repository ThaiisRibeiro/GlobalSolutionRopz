using GlobalSolutionRopz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ClinicaTests
    {
        [Fact]
        public void CnpjInvalidoComLetras_DeveLancarExcecao()
        {
            var clinica = new Clinica();

            var ex = Assert.Throws<ArgumentException>(() => clinica.cnpj = "12A45678B001CC");
            Assert.Equal("CNPJ inválido. Deve conter apenas números e ter 14 dígitos.", ex.Message);
        }

        [Fact]
        public void CnpjComMenosDe14Digitos_DeveLancarExcecao()
        {
            var clinica = new Clinica();

            var ex = Assert.Throws<ArgumentException>(() => clinica.cnpj = "1234567890123");
            Assert.Equal("CNPJ inválido. Deve conter apenas números e ter 14 dígitos.", ex.Message);
        }

        [Fact]
        public void DeveCriarClinicaComDadosValidos()
        {
            var clinica = new Clinica
            {
                nome_clinica = "Clínica Sorriso",
                endereco = "Rua das Flores, 123",
                id_dentista = 1,
                cnpj = "12345678000199"
            };

            Assert.Equal("Clínica Sorriso", clinica.nome_clinica);
            Assert.Equal("Rua das Flores, 123", clinica.endereco);
            Assert.Equal(1, clinica.id_dentista);
            Assert.Equal("12345678000199", clinica.cnpj);
        }

    }
}
