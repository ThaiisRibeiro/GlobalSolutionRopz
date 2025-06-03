using GlobalSolutionRopz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class TabelaPrecoTests
    {
        [Fact]
        public void ValorNegativo_DeveLancarExcecao()
        {
            var tabela = new TabelaPreco();
            var ex = Assert.Throws<ArgumentException>(() => tabela.valor = -50.00);
            Assert.Equal("Valor inválido. Deve ser maior que zero.", ex.Message);
        }

        [Fact]
        public void NomeProcedimentoVazio_DeveLancarExcecao()
        {
            var tabela = new TabelaPreco();
            var ex = Assert.Throws<ArgumentException>(() => tabela.nome_procedimento = "");
            Assert.Equal("Nome do procedimento não pode ser vazio.", ex.Message);
        }

        [Fact]
        public void DescricaoVazia_DeveLancarExcecao()
        {
            var tabela = new TabelaPreco();
            var ex = Assert.Throws<ArgumentException>(() => tabela.descricao = " ");
            Assert.Equal("Descrição não pode ser vazia.", ex.Message);
        }

        [Fact]
        public void DeveCriarTabelaPrecoComDadosValidos()
        {
            var tabela = new TabelaPreco
            {
                id_tabela_preco = 1,
                nome_procedimento = "Limpeza",
                valor = 200.00,
                descricao = "Procedimento de limpeza dentária"
            };

            Assert.Equal(1, tabela.id_tabela_preco);
            Assert.Equal("Limpeza", tabela.nome_procedimento);
            Assert.Equal(200.00, tabela.valor);
            Assert.Equal("Procedimento de limpeza dentária", tabela.descricao);
        }
    }
}
