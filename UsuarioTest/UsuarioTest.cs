using GlobalSolutionRopz.Model;
using Xunit;

namespace UsuarioTest
{
    public class UsuarioTest
    {
        [Fact]
        public void EmailSemArroba_DeveLancarExcecao()
        {
            var usuario = new Usuario();

            var ex = Assert.Throws<ArgumentException>(() => usuario.email = "testeemail.com");
            Assert.Equal("E-mail inválido. Deve conter '@'.", ex.Message);
        }

    
        [Fact]
        public void TelefoneInvalido_DeveLancarExcecao()
        {
            var usuario = new Usuario();

            var ex = Assert.Throws<ArgumentException>(() => usuario.telefone = "234567");
            Assert.Equal("Telefone inválido. Deve ter 11 dígitos e começar com 9 ou 1.", ex.Message);
        }

        [Fact]
        public void DeveCriarUsuarioComDadosValidos()
        {
            var usuario = new Usuario
            {
                nome = "Thais Ribeiro",
                email = "thais@teste.com",
                telefone = "91234567890",
                
            };

            Assert.Equal("Thais Ribeiro", usuario.nome);
            Assert.Equal("thais@teste.com", usuario.email);
            Assert.Equal("91234567890", usuario.telefone);
        
        }
    }
}
