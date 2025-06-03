using GlobalSolutionRopz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UsuarioTest
{
    public class AlertaTest
    {
        [Fact]
        public void TipoMensagemVazio_DeveLancarExcecao()
        {
            var alerta = new Alerta();

            var ex = Assert.Throws<ArgumentException>(() => alerta.tipo_mensagem = 0);
            Assert.Equal("Tipo de mensagem é obrigatório.", ex.Message);
        }

    

        [Fact]
        public void EstadoVazio_DeveLancarExcecao()
        {
            var alerta = new Alerta();

            var ex = Assert.Throws<ArgumentException>(() => alerta.estado = "");
            Assert.Equal("Estado é obrigatório.", ex.Message);
        }

        [Fact]
        public void TemperaturaMuitoAlta_DeveLancarExcecao()
        {
            var alerta = new Alerta();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => alerta.temperatura = 100);
            Assert.Contains("Temperatura fora do intervalo permitido", ex.Message);
        }

        [Fact]
        public void TemperaturaMuitoBaixa_DeveLancarExcecao()
        {
            var alerta = new Alerta();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => alerta.temperatura = -50);
            Assert.Contains("Temperatura fora do intervalo permitido", ex.Message);
        }

        [Fact]
        public void DeveCriarAlertaComDadosValidos()
        {
            var alerta = new Alerta
            {
                tipo_mensagem = 1,
                temperatura = 42
            };

            Assert.Equal(1, alerta.tipo_mensagem);
            Assert.Equal(42, alerta.temperatura);
        }

    }
}
