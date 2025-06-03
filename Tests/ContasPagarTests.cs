using GlobalSolutionRopz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ContasPagarTests
    {
        [Fact]
        public void ValorNegativo_DeveLancarExcecao()
        {
            var conta = new ContasPagar();

            var ex = Assert.Throws<ArgumentException>(() => conta.valor = -100.00);
            Assert.Equal("Valor inválido. Deve ser maior que zero.", ex.Message);
        }

        [Fact]
        public void DataVencimentoAntesDataEmissao_DeveLancarExcecao()
        {
            var conta = new ContasPagar();
            conta.data_emissao = new DateTime(2025, 5, 12);

            var ex = Assert.Throws<ArgumentException>(() =>
                conta.data_vencimento = new DateTime(2025, 5, 10));

            Assert.Equal("Data de vencimento não pode ser anterior à data de emissão.", ex.Message);
        }

        [Fact]
        public void DeveCriarContaReceberComDadosValidos()
        {
            var conta = new ContasPagar
            {
                id_clinica = 1,
                valor = 250.00,
                data_emissao = new DateTime(2025, 5, 10),
                data_vencimento = new DateTime(2025, 5, 20)
            };

            Assert.Equal(1, conta.id_clinica);
            Assert.Equal(250.00, conta.valor);
            Assert.Equal(new DateTime(2025, 5, 10), conta.data_emissao);
            Assert.Equal(new DateTime(2025, 5, 20), conta.data_vencimento);
        }
    }
}

