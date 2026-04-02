using Xunit;

namespace EFManager.Tests
{
    public class UsuarioTests
    {
        [Fact]
        public void Usuario_DeveTerPropriedadesCorretamente()
        {
            // Arrange & Act
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João Silva",
                Email = "joao@example.com"
            };

            // Assert
            Assert.Equal(1, usuario.Id);
            Assert.Equal("João Silva", usuario.Nome);
            Assert.Equal("joao@example.com", usuario.Email);
        }

        [Fact]
        public void Usuario_DeveInicializarColecaoDePedidosVazia()
        {
            // Arrange & Act
            var usuario = new Usuario();

            // Assert
            Assert.NotNull(usuario.Pedidos);
            Assert.Empty(usuario.Pedidos);
        }

        [Fact]
        public void Usuario_DevePermitirAdicionarPedidos()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Maria" };
            var pedido1 = new Pedido { Id = 1, Total = 100.00m };
            var pedido2 = new Pedido { Id = 2, Total = 250.50m };

            // Act
            usuario.Pedidos.Add(pedido1);
            usuario.Pedidos.Add(pedido2);

            // Assert
            Assert.Equal(2, usuario.Pedidos.Count);
            Assert.Contains(pedido1, usuario.Pedidos);
            Assert.Contains(pedido2, usuario.Pedidos);
        }

        [Fact]
        public void Usuario_NomeNaoPodeSerNull()
        {
            // Arrange & Act
            var usuario = new Usuario();

            // Assert
            Assert.Null(usuario.Nome);
        }

        [Fact]
        public void Usuario_EmailNaoPodeSerNull()
        {
            // Arrange & Act
            var usuario = new Usuario();

            // Assert
            Assert.Null(usuario.Email);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(999)]
        public void Usuario_IdDeveSuportarValoresPositivos(int id)
        {
            // Arrange & Act
            var usuario = new Usuario { Id = id };

            // Assert
            Assert.Equal(id, usuario.Id);
        }
    }

    public class PedidoTests
    {
        [Fact]
        public void Pedido_DeveTerPropriedadesCorretamente()
        {
            // Arrange & Act
            var pedido = new Pedido
            {
                Id = 1,
                Total = 500.75m,
                UsuarioId = 5
            };

            // Assert
            Assert.Equal(1, pedido.Id);
            Assert.Equal(500.75m, pedido.Total);
            Assert.Equal(5, pedido.UsuarioId);
        }

        [Fact]
        public void Pedido_DeveInicializarColecaoDeProdutosPedidosVazia()
        {
            // Arrange & Act
            var pedido = new Pedido();

            // Assert
            Assert.NotNull(pedido.ProdutosPedidos);
            Assert.Empty(pedido.ProdutosPedidos);
        }

        [Fact]
        public void Pedido_DevePermitirAdicionarProdutos()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, Total = 100m };
            var pp1 = new ProdutoPedido { Id = 1, ProdutoId = 10, PedidoId = 1 };
            var pp2 = new ProdutoPedido { Id = 2, ProdutoId = 20, PedidoId = 1 };

            // Act
            pedido.ProdutosPedidos.Add(pp1);
            pedido.ProdutosPedidos.Add(pp2);

            // Assert
            Assert.Equal(2, pedido.ProdutosPedidos.Count);
            Assert.Contains(pp1, pedido.ProdutosPedidos);
            Assert.Contains(pp2, pedido.ProdutosPedidos);
        }

        [Fact]
        public void Pedido_UsuarioReferenciaPodeSerNull()
        {
            // Arrange & Act
            var pedido = new Pedido();

            // Assert
            Assert.Null(pedido.Usuario);
        }

        [Fact]
        public void Pedido_DevePermitirTotalZero()
        {
            // Arrange & Act
            var pedido = new Pedido { Id = 1, Total = 0m };

            // Assert
            Assert.Equal(0m, pedido.Total);
        }

        [Theory]
        [InlineData(10.50)]
        [InlineData(100.00)]
        [InlineData(0.01)]
        [InlineData(9999.99)]
        public void Pedido_TotalDeveSuportarValoresDecimais(double valor)
        {
            // Arrange & Act
            var pedido = new Pedido { Total = (decimal)valor };

            // Assert
            Assert.Equal((decimal)valor, pedido.Total);
        }

        [Fact]
        public void Pedido_DeveAssociarComUsuario()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Pedro" };
            var pedido = new Pedido { Id = 1, UsuarioId = 1, Usuario = usuario };

            // Act & Assert
            Assert.NotNull(pedido.Usuario);
            Assert.Equal("Pedro", pedido.Usuario.Nome);
            Assert.Equal(1, pedido.Usuario.Id);
        }
    }

    public class ProdutoTests
    {
        [Fact]
        public void Produto_DeveTerPropriedadesCorretamente()
        {
            // Arrange & Act
            var produto = new Produto
            {
                Id = 1,
                Nome = "Notebook",
                PrecoUnitario = 2500.00m,
                CategoriaId = 3
            };

            // Assert
            Assert.Equal(1, produto.Id);
            Assert.Equal("Notebook", produto.Nome);
            Assert.Equal(2500.00m, produto.PrecoUnitario);
            Assert.Equal(3, produto.CategoriaId);
        }

        [Fact]
        public void Produto_CategoriaReferenciaPodeSerNull()
        {
            // Arrange & Act
            var produto = new Produto();

            // Assert
            Assert.Null(produto.Categoria);
        }

        [Fact]
        public void Produto_DeveAssociarComCategoria()
        {
            // Arrange
            var categoria = new Categoria { Id = 1, Nome = "Eletrônicos" };
            var produto = new Produto
            {
                Id = 1,
                Nome = "Mouse",
                PrecoUnitario = 50.00m,
                CategoriaId = 1,
                Categoria = categoria
            };

            // Act & Assert
            Assert.NotNull(produto.Categoria);
            Assert.Equal("Eletrônicos", produto.Categoria.Nome);
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(99.99)]
        [InlineData(1000.00)]
        public void Produto_PrecoUnitarioDeveSuportarValoresPositivos(double valor)
        {
            // Arrange & Act
            var produto = new Produto { PrecoUnitario = (decimal)valor };

            // Assert
            Assert.Equal((decimal)valor, produto.PrecoUnitario);
        }

        [Fact]
        public void Produto_NomeNaoPodeSerNull()
        {
            // Arrange & Act
            var produto = new Produto();

            // Assert
            Assert.Null(produto.Nome);
        }

        [Fact]
        public void Produto_PrecoUnitarioPodeSerZero()
        {
            // Arrange & Act
            var produto = new Produto { PrecoUnitario = 0m };

            // Assert
            Assert.Equal(0m, produto.PrecoUnitario);
        }
    }

    public class CategoriaTests
    {
        [Fact]
        public void Categoria_DeveTerPropriedadesCorretamente()
        {
            // Arrange & Act
            var categoria = new Categoria
            {
                Id = 1,
                Nome = "Eletrônicos"
            };

            // Assert
            Assert.Equal(1, categoria.Id);
            Assert.Equal("Eletrônicos", categoria.Nome);
        }

        [Fact]
        public void Categoria_NomeNaoPodeSerNull()
        {
            // Arrange & Act
            var categoria = new Categoria();

            // Assert
            Assert.Null(categoria.Nome);
        }

        [Theory]
        [InlineData("Roupas")]
        [InlineData("Alimentos")]
        [InlineData("Livros")]
        [InlineData("")]
        public void Categoria_DeveAceitarQualquerNome(string nome)
        {
            // Arrange & Act
            var categoria = new Categoria { Nome = nome };

            // Assert
            Assert.Equal(nome, categoria.Nome);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void Categoria_IdDeveSuportarValoresPositivos(int id)
        {
            // Arrange & Act
            var categoria = new Categoria { Id = id };

            // Assert
            Assert.Equal(id, categoria.Id);
        }
    }

    public class ProdutoPedidoTests
    {
        [Fact]
        public void ProdutoPedido_DeveTerPropriedadesCorretamente()
        {
            // Arrange & Act
            var produtoPedido = new ProdutoPedido
            {
                Id = 1,
                PedidoId = 5,
                ProdutoId = 10
            };

            // Assert
            Assert.Equal(1, produtoPedido.Id);
            Assert.Equal(5, produtoPedido.PedidoId);
            Assert.Equal(10, produtoPedido.ProdutoId);
        }

        [Fact]
        public void ProdutoPedido_ReferenciaPedidoPodeSerNull()
        {
            // Arrange & Act
            var produtoPedido = new ProdutoPedido();

            // Assert
            Assert.Null(produtoPedido.Pedido);
        }

        [Fact]
        public void ProdutoPedido_ReferenciaProdutoPodeSerNull()
        {
            // Arrange & Act
            var produtoPedido = new ProdutoPedido();

            // Assert
            Assert.Null(produtoPedido.Produto);
        }

        [Fact]
        public void ProdutoPedido_DeveAssociarComPedidoEProduto()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, Total = 100m };
            var produto = new Produto { Id = 1, Nome = "Item", PrecoUnitario = 50m };
            var produtoPedido = new ProdutoPedido
            {
                Id = 1,
                PedidoId = 1,
                ProdutoId = 1,
                Pedido = pedido,
                Produto = produto
            };

            // Act & Assert
            Assert.NotNull(produtoPedido.Pedido);
            Assert.NotNull(produtoPedido.Produto);
            Assert.Equal(100m, produtoPedido.Pedido.Total);
            Assert.Equal("Item", produtoPedido.Produto.Nome);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 10)]
        [InlineData(100, 200)]
        public void ProdutoPedido_DevePermitirDiferentesIds(int pedidoId, int produtoId)
        {
            // Arrange & Act
            var produtoPedido = new ProdutoPedido { PedidoId = pedidoId, ProdutoId = produtoId };

            // Assert
            Assert.Equal(pedidoId, produtoPedido.PedidoId);
            Assert.Equal(produtoId, produtoPedido.ProdutoId);
        }
    }

    public class AppDbContextTests
    {
        [Fact]
        public void AppDbContext_DeveConterDbSetParaTodasAsEntidades()
        {
            // Arrange & Act
            var context = new AppDbContext();

            // Assert
            Assert.NotNull(context.Usuarios);
            Assert.NotNull(context.Pedidos);
            Assert.NotNull(context.Produtos);
            Assert.NotNull(context.Categorias);
            Assert.NotNull(context.ProdutosPedidos);
        }

        [Fact]
        public void AppDbContext_DeveSerInstanciadoSemErros()
        {
            // Arrange & Act
            var context = new AppDbContext();

            // Assert
            Assert.NotNull(context);
        }
    }

    public class IntegrationTests
    {
        [Fact]
        public void Usuario_ComMultiplosPedidos_DeveMantenerAssociacao()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Ana", Email = "ana@example.com" };
            var pedido1 = new Pedido { Id = 1, Total = 100m, UsuarioId = 1 };
            var pedido2 = new Pedido { Id = 2, Total = 200m, UsuarioId = 1 };

            // Act
            usuario.Pedidos.Add(pedido1);
            usuario.Pedidos.Add(pedido2);

            // Assert
            Assert.Equal(2, usuario.Pedidos.Count);
            Assert.All(usuario.Pedidos, p => Assert.Equal(1, p.UsuarioId));
        }

        [Fact]
        public void Pedido_ComMultiplosProdutos_DeveMantenerAssociacao()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, Total = 300m };
            var pp1 = new ProdutoPedido { Id = 1, PedidoId = 1, ProdutoId = 10 };
            var pp2 = new ProdutoPedido { Id = 2, PedidoId = 1, ProdutoId = 20 };
            var pp3 = new ProdutoPedido { Id = 3, PedidoId = 1, ProdutoId = 30 };

            // Act
            pedido.ProdutosPedidos.Add(pp1);
            pedido.ProdutosPedidos.Add(pp2);
            pedido.ProdutosPedidos.Add(pp3);

            // Assert
            Assert.Equal(3, pedido.ProdutosPedidos.Count);
            Assert.All(pedido.ProdutosPedidos, pp => Assert.Equal(1, pp.PedidoId));
        }

        [Fact]
        public void Produto_ComCategoria_DeveManterReferencia()
        {
            // Arrange
            var categoria = new Categoria { Id = 1, Nome = "Eletrônicos" };
            var produto = new Produto
            {
                Id = 1,
                Nome = "Teclado",
                PrecoUnitario = 150m,
                CategoriaId = 1,
                Categoria = categoria
            };

            // Act & Assert
            Assert.Equal(categoria.Id, produto.CategoriaId);
            Assert.Same(categoria, produto.Categoria);
            Assert.Equal("Eletrônicos", produto.Categoria.Nome);
        }

        [Fact]
        public void SistemaCompleto_DeveManterTodosRelacionamentos()
        {
            // Arrange - Criar dados completos
            var categoria = new Categoria { Id = 1, Nome = "Eletrônicos" };
            var produto = new Produto { Id = 1, Nome = "Mouse", PrecoUnitario = 50m, CategoriaId = 1, Categoria = categoria };
            var usuario = new Usuario { Id = 1, Nome = "Carlos", Email = "carlos@example.com" };
            var pedido = new Pedido { Id = 1, Total = 50m, UsuarioId = 1, Usuario = usuario };
            var produtoPedido = new ProdutoPedido { Id = 1, PedidoId = 1, ProdutoId = 1, Pedido = pedido, Produto = produto };

            // Act
            usuario.Pedidos.Add(pedido);
            pedido.ProdutosPedidos.Add(produtoPedido);

            // Assert
            Assert.Equal(1, usuario.Pedidos.Count);
            Assert.Equal(1, pedido.ProdutosPedidos.Count);
            Assert.Equal("Mouse", pedido.ProdutosPedidos.First().Produto.Nome);
            Assert.Equal("Eletrônicos", pedido.ProdutosPedidos.First().Produto.Categoria.Nome);
            Assert.Equal("Carlos", pedido.Usuario.Nome);
        }
    }
}
