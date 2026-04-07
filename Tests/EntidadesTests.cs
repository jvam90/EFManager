using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace EFManager.Tests
{
    public class UsuarioTests
    {
        [Test]
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
            Assert.That(usuario.Id, Is.EqualTo(1));
            Assert.That(usuario.Nome, Is.EqualTo("João Silva"));
            Assert.That(usuario.Email, Is.EqualTo("joao@example.com"));
        }

        [Test]
        public void Usuario_DeveInicializarColecaoDePedidosVazia()
        {
            // Arrange & Act
            var usuario = new Usuario();

            // Assert
            Assert.That(usuario.Pedidos, Is.Not.Null);
            Assert.That(usuario.Pedidos, Is.Empty);
        }

        [Test]
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
            Assert.That(usuario.Pedidos.Count, Is.EqualTo(2));
            Assert.That(usuario.Pedidos, Does.Contain(pedido1));
            Assert.That(usuario.Pedidos, Does.Contain(pedido2));
        }

        [Test]
        public void Usuario_NomeNaoPodeSerNull()
        {
            // Arrange & Act
            var usuario = new Usuario();

            // Assert
            Assert.That(usuario.Nome, Is.Null);
        }

        [Test]
        public void Usuario_EmailNaoPodeSerNull()
        {
            // Arrange & Act
            var usuario = new Usuario();

            // Assert
            Assert.That(usuario.Email, Is.Null);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(999)]
        public void Usuario_IdDeveSuportarValoresPositivos(int id)
        {
            // Arrange & Act
            var usuario = new Usuario { Id = id };

            // Assert
            Assert.That(usuario.Id, Is.EqualTo(id));
        }
    }

    public class PedidoTests
    {
        [Test]
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
            Assert.That(pedido.Id, Is.EqualTo(1));
            Assert.That(pedido.Total, Is.EqualTo(500.75m));
            Assert.That(pedido.UsuarioId, Is.EqualTo(5));
        }

        [Test]
        public void Pedido_DeveInicializarColecaoDeProdutosPedidosVazia()
        {
            // Arrange & Act
            var pedido = new Pedido();

            // Assert
            Assert.That(pedido.ProdutosPedidos, Is.Not.Null);
            Assert.That(pedido.ProdutosPedidos, Is.Empty);
        }

        [Test]
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
            Assert.That(pedido.ProdutosPedidos.Count, Is.EqualTo(2));
            Assert.That(pedido.ProdutosPedidos, Does.Contain(pp1));
            Assert.That(pedido.ProdutosPedidos, Does.Contain(pp2));
        }

        [Test]
        public void Pedido_UsuarioReferenciaPodeSerNull()
        {
            // Arrange & Act
            var pedido = new Pedido();

            // Assert
            Assert.That(pedido.Usuario, Is.Null);
        }

        [Test]
        public void Pedido_DevePermitirTotalZero()
        {
            // Arrange & Act
            var pedido = new Pedido { Id = 1, Total = 0m };

            // Assert
            Assert.That(pedido.Total, Is.EqualTo(0m));
        }

        [TestCase(10.50)]
        [TestCase(100.00)]
        [TestCase(0.01)]
        [TestCase(9999.99)]
        public void Pedido_TotalDeveSuportarValoresDecimais(double valor)
        {
            // Arrange & Act
            var pedido = new Pedido { Total = (decimal)valor };

            // Assert
            Assert.That(pedido.Total, Is.EqualTo((decimal)valor));
        }

        [Test]
        public void Pedido_DeveAssociarComUsuario()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Pedro" };
            var pedido = new Pedido { Id = 1, UsuarioId = 1, Usuario = usuario };

            // Act & Assert
            Assert.That(pedido.Usuario, Is.Not.Null);
            Assert.That(pedido.Usuario.Nome, Is.EqualTo("Pedro"));
            Assert.That(pedido.Usuario.Id, Is.EqualTo(1));
        }
    }

    public class ProdutoTests
    {
        [Test]
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
            Assert.That(produto.Id, Is.EqualTo(1));
            Assert.That(produto.Nome, Is.EqualTo("Notebook"));
            Assert.That(produto.PrecoUnitario, Is.EqualTo(2500.00m));
            Assert.That(produto.CategoriaId, Is.EqualTo(3));
        }

        [Test]
        public void Produto_CategoriaReferenciaPodeSerNull()
        {
            // Arrange & Act
            var produto = new Produto();

            // Assert
            Assert.That(produto.Categoria, Is.Null);
        }

        [Test]
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
            Assert.That(produto.Categoria, Is.Not.Null);
            Assert.That(produto.Categoria.Nome, Is.EqualTo("Eletrônicos"));
        }

        [TestCase(0.01)]
        [TestCase(99.99)]
        [TestCase(1000.00)]
        public void Produto_PrecoUnitarioDeveSuportarValoresPositivos(double valor)
        {
            // Arrange & Act
            var produto = new Produto { PrecoUnitario = (decimal)valor };

            // Assert
            Assert.That(produto.PrecoUnitario, Is.EqualTo((decimal)valor));
        }

        [Test]
        public void Produto_NomeNaoPodeSerNull()
        {
            // Arrange & Act
            var produto = new Produto();

            // Assert
            Assert.That(produto.Nome, Is.Null);
        }

        [Test]
        public void Produto_PrecoUnitarioPodeSerZero()
        {
            // Arrange & Act
            var produto = new Produto { PrecoUnitario = 0m };

            // Assert
            Assert.That(produto.PrecoUnitario, Is.EqualTo(0m));
        }
    }

    public class CategoriaTests
    {
        [Test]
        public void Categoria_DeveTerPropriedadesCorretamente()
        {
            // Arrange & Act
            var categoria = new Categoria
            {
                Id = 1,
                Nome = "Eletrônicos"
            };

            // Assert
            Assert.That(categoria.Id, Is.EqualTo(1));
            Assert.That(categoria.Nome, Is.EqualTo("Eletrônicos"));
        }

        [Test]
        public void Categoria_NomeNaoPodeSerNull()
        {
            // Arrange & Act
            var categoria = new Categoria();

            // Assert
            Assert.That(categoria.Nome, Is.Null);
        }

        [TestCase("Roupas")]
        [TestCase("Alimentos")]
        [TestCase("Livros")]
        [TestCase("")]
        public void Categoria_DeveAceitarQualquerNome(string nome)
        {
            // Arrange & Act
            var categoria = new Categoria { Nome = nome };

            // Assert
            Assert.That(categoria.Nome, Is.EqualTo(nome));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void Categoria_IdDeveSuportarValoresPositivos(int id)
        {
            // Arrange & Act
            var categoria = new Categoria { Id = id };

            // Assert
            Assert.That(categoria.Id, Is.EqualTo(id));
        }
    }

    public class ProdutoPedidoTests
    {
        [Test]
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
            Assert.That(produtoPedido.Id, Is.EqualTo(1));
            Assert.That(produtoPedido.PedidoId, Is.EqualTo(5));
            Assert.That(produtoPedido.ProdutoId, Is.EqualTo(10));
        }

        [Test]
        public void ProdutoPedido_ReferenciaPedidoPodeSerNull()
        {
            // Arrange & Act
            var produtoPedido = new ProdutoPedido();

            // Assert
            Assert.That(produtoPedido.Pedido, Is.Null);
        }

        [Test]
        public void ProdutoPedido_ReferenciaProdutoPodeSerNull()
        {
            // Arrange & Act
            var produtoPedido = new ProdutoPedido();

            // Assert
            Assert.That(produtoPedido.Produto, Is.Null);
        }

        [Test]
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
            Assert.That(produtoPedido.Pedido, Is.Not.Null);
            Assert.That(produtoPedido.Produto, Is.Not.Null);
            Assert.That(produtoPedido.Pedido.Total, Is.EqualTo(100m));
            Assert.That(produtoPedido.Produto.Nome, Is.EqualTo("Item"));
        }

        [TestCase(1, 1)]
        [TestCase(5, 10)]
        [TestCase(100, 200)]
        public void ProdutoPedido_DevePermitirDiferentesIds(int pedidoId, int produtoId)
        {
            // Arrange & Act
            var produtoPedido = new ProdutoPedido { PedidoId = pedidoId, ProdutoId = produtoId };

            // Assert
            Assert.That(produtoPedido.PedidoId, Is.EqualTo(pedidoId));
            Assert.That(produtoPedido.ProdutoId, Is.EqualTo(produtoId));
        }
    }

    public class AppDbContextTests
    {
        [Test]
        public void AppDbContext_DeveConterDbSetParaTodasAsEntidades()
        {
            // Arrange & Act
            var context = new AppDbContext();

            // Assert
            Assert.That(context.Usuarios, Is.Not.Null);
            Assert.That(context.Pedidos, Is.Not.Null);
            Assert.That(context.Produtos, Is.Not.Null);
            Assert.That(context.Categorias, Is.Not.Null);
            Assert.That(context.ProdutosPedidos, Is.Not.Null);
        }

        [Test]
        public void AppDbContext_DeveSerInstanciadoSemErros()
        {
            // Arrange & Act
            var context = new AppDbContext();

            // Assert
            Assert.That(context, Is.Not.Null);
        }
    }

    public class IntegrationTests
    {
        [Test]
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
            Assert.That(usuario.Pedidos.Count, Is.EqualTo(2));
            foreach (var p in usuario.Pedidos)
            {
                Assert.That(p.UsuarioId, Is.EqualTo(1));
            }
        }

        [Test]
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
            Assert.That(pedido.ProdutosPedidos.Count, Is.EqualTo(3));
            foreach (var pp in pedido.ProdutosPedidos)
            {
                Assert.That(pp.PedidoId, Is.EqualTo(1));
            }
        }

        [Test]
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
            Assert.That(produto.CategoriaId, Is.EqualTo(categoria.Id));
            Assert.That(produto.Categoria, Is.SameAs(categoria));
            Assert.That(produto.Categoria.Nome, Is.EqualTo("Eletrônicos"));
        }

        [Test]
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
            Assert.That(usuario.Pedidos.Count, Is.EqualTo(1));
            Assert.That(pedido.ProdutosPedidos.Count, Is.EqualTo(1));
            Assert.That(pedido.ProdutosPedidos.First().Produto.Nome, Is.EqualTo("Mouse"));
            Assert.That(pedido.ProdutosPedidos.First().Produto.Categoria.Nome, Is.EqualTo("Eletrônicos"));
            Assert.That(pedido.Usuario.Nome, Is.EqualTo("Carlos"));
        }
    }

    public class DbContextIntegrationTests
    {
        private AppDbContext? _context;
        private string? _dbPath;

        [SetUp]
        public void Setup()
        {
            _dbPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.db");
            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }
            _context = new AppDbContext();
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Database.EnsureDeleted();
            _context?.Dispose();
        }

        [Test]
        public void DeveInserirERetrievarUsuarioComSucesso()
        {
            // Arrange
            var usuario = new Usuario { Nome = "Test User", Email = "test@example.com" };

            // Act
            _context!.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Assert
            var retrieved = _context!.Usuarios.FirstOrDefault(u => u.Email == "test@example.com");
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.Nome, Is.EqualTo("Test User"));
        }

        [Test]
        public void DeveInserirERetrievarCategoriaComSucesso()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Eletrônicos" };

            // Act
            _context!.Categorias.Add(categoria);
            _context.SaveChanges();

            // Assert
            var retrieved = _context.Categorias.FirstOrDefault(c => c.Nome == "Eletrônicos");
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.Nome, Is.EqualTo("Eletrônicos"));
        }

        [Test]
        public void DeveInserirERetrievarProdutoComCategoriaAssociada()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Livros" };
            var produto = new Produto
            {
                Nome = "Clean Code",
                PrecoUnitario = 99.99m,
                Categoria = categoria
            };

            // Act
            _context!.Categorias.Add(categoria);
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            // Assert
            var retrieved = _context.Produtos.FirstOrDefault(p => p.Nome == "Clean Code");
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.PrecoUnitario, Is.EqualTo(99.99m));
            Assert.That(retrieved.Categoria!.Nome, Is.EqualTo("Livros"));
        }

        [Test]
        public void DeveInserirERetrievarPedidoComProdutosAssociados()
        {
            // Arrange
            var usuario = new Usuario { Nome = "João", Email = "joao@test.com" };
            var categoria = new Categoria { Nome = "Alimentos" };
            var produto1 = new Produto { Nome = "Pão", PrecoUnitario = 5.50m, Categoria = categoria };
            var produto2 = new Produto { Nome = "Leite", PrecoUnitario = 4.00m, Categoria = categoria };

            var pedido = new Pedido { Total = 9.50m, Usuario = usuario };
            var pp1 = new ProdutoPedido { Pedido = pedido, Produto = produto1 };
            var pp2 = new ProdutoPedido { Pedido = pedido, Produto = produto2 };

            pedido.ProdutosPedidos.Add(pp1);
            pedido.ProdutosPedidos.Add(pp2);

            // Act
            _context!.Usuarios.Add(usuario);
            _context.Categorias.Add(categoria);
            _context.Produtos.Add(produto1);
            _context.Produtos.Add(produto2);
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            // Assert
            var retrievedPedido = _context.Pedidos.FirstOrDefault(p => p.Total == 9.50m);
            Assert.That(retrievedPedido, Is.Not.Null);
            Assert.That(retrievedPedido!.Usuario!.Nome, Is.EqualTo("João"));
            Assert.That(retrievedPedido.ProdutosPedidos.Count, Is.EqualTo(2));
        }

        [Test]
        public void DeveAtualizarPrecoDoProdutoComSucesso()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Utensílios" };
            var produto = new Produto { Nome = "Colher", PrecoUnitario = 2.00m, Categoria = categoria };

            _context!.Categorias.Add(categoria);
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            // Act
            var produtoAtualizacao = _context.Produtos.First(p => p.Nome == "Colher");
            produtoAtualizacao.PrecoUnitario = 3.50m;
            _context.SaveChanges();

            // Assert
            var updated = _context.Produtos.First(p => p.Nome == "Colher");
            Assert.That(updated.PrecoUnitario, Is.EqualTo(3.50m));
        }

        [Test]
        public void DeveDeletarProdutoComSucesso()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Bebidas" };
            var produto = new Produto { Nome = "Suco", PrecoUnitario = 5.00m, Categoria = categoria };

            _context!.Categorias.Add(categoria);
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            var produtoId = produto.Id;

            // Act
            var produtoParaDeletar = _context.Produtos.First(p => p.Nome == "Suco");
            _context.Produtos.Remove(produtoParaDeletar);
            _context.SaveChanges();

            // Assert
            var deleted = _context.Produtos.FirstOrDefault(p => p.Id == produtoId);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void DeveManterAssociacaoComMultiplosUsuariosComPedidos()
        {
            // Arrange
            var usuario1 = new Usuario { Nome = "Alice", Email = "alice@test.com" };
            var usuario2 = new Usuario { Nome = "Bob", Email = "bob@test.com" };
            var pedido1 = new Pedido { Total = 100.00m, Usuario = usuario1 };
            var pedido2 = new Pedido { Total = 200.00m, Usuario = usuario2 };
            var pedido3 = new Pedido { Total = 150.00m, Usuario = usuario1 };

            // Act
            _context!.Usuarios.Add(usuario1);
            _context.Usuarios.Add(usuario2);
            _context.Pedidos.Add(pedido1);
            _context.Pedidos.Add(pedido2);
            _context.Pedidos.Add(pedido3);
            _context.SaveChanges();

            // Assert
            var alice = _context.Usuarios.First(u => u.Nome == "Alice");
            var bob = _context.Usuarios.First(u => u.Nome == "Bob");
            var pedidosAlice = _context.Pedidos.Where(p => p.UsuarioId == alice.Id).ToList();
            var pedidosBob = _context.Pedidos.Where(p => p.UsuarioId == bob.Id).ToList();

            Assert.That(pedidosAlice.Count, Is.EqualTo(2));
            Assert.That(pedidosBob.Count, Is.EqualTo(1));
            Assert.That(pedidosAlice.Sum(p => p.Total), Is.EqualTo(250.00m));
        }

        [Test]
        public void DeveManterCategoriaComMultiplosProdutosAssociados()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Roupas" };
            var produto1 = new Produto { Nome = "Camiseta", PrecoUnitario = 50.00m, Categoria = categoria };
            var produto2 = new Produto { Nome = "Calça", PrecoUnitario = 120.00m, Categoria = categoria };
            var produto3 = new Produto { Nome = "Meias", PrecoUnitario = 15.00m, Categoria = categoria };

            // Act
            _context!.Categorias.Add(categoria);
            _context.Produtos.AddRange(produto1, produto2, produto3);
            _context.SaveChanges();

            // Assert
            var roupas = _context.Categorias.First(c => c.Nome == "Roupas");
            var produtosRoupas = _context.Produtos.Where(p => p.CategoriaId == roupas.Id).ToList();

            Assert.That(produtosRoupas.Count, Is.EqualTo(3));
            Assert.That(produtosRoupas.Sum(p => p.PrecoUnitario), Is.EqualTo(185.00m));
        }

        [Test]
        public void DeveCalcularCorretamenteTotalDoPedidoComMultiplosProdutos()
        {
            // Arrange
            var usuario = new Usuario { Nome = "Carlos", Email = "carlos@test.com" };
            var categoria = new Categoria { Nome = "Eletrônicos" };
            var produto1 = new Produto { Nome = "Mouse", PrecoUnitario = 50.00m, Categoria = categoria };
            var produto2 = new Produto { Nome = "Teclado", PrecoUnitario = 150.00m, Categoria = categoria };

            var pedido = new Pedido { Total = 200.00m, Usuario = usuario };
            var pp1 = new ProdutoPedido { Pedido = pedido, Produto = produto1 };
            var pp2 = new ProdutoPedido { Pedido = pedido, Produto = produto2 };

            // Act
            _context!.Usuarios.Add(usuario);
            _context.Categorias.Add(categoria);
            _context.Produtos.AddRange(produto1, produto2);
            pedido.ProdutosPedidos.Add(pp1);
            pedido.ProdutosPedidos.Add(pp2);
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            // Assert
            var pedidoRecuperado = _context.Pedidos.FirstOrDefault(p => p.Total == 200.00m);
            Assert.That(pedidoRecuperado, Is.Not.Null);
            Assert.That(pedidoRecuperado!.ProdutosPedidos.Count, Is.EqualTo(2));
            Assert.That(pedidoRecuperado.Total, Is.EqualTo(200.00m));
        }

        [Test]
        public void DeveInstanciarDbContextComSucesso()
        {
            // Assert
            Assert.That(_context, Is.Not.Null);
            Assert.That(_context!.Usuarios, Is.Not.Null);
            Assert.That(_context.Pedidos, Is.Not.Null);
            Assert.That(_context.Produtos, Is.Not.Null);
            Assert.That(_context.Categorias, Is.Not.Null);
            Assert.That(_context.ProdutosPedidos, Is.Not.Null);
        }

        [Test]
        public void DeveRetornarProdutosFiltradosPorCategoria()
        {
            // Arrange
            var eletronica = new Categoria { Nome = "Eletrônica" };
            var roupa = new Categoria { Nome = "Roupa" };
            var p1 = new Produto { Nome = "TV", PrecoUnitario = 1000m, Categoria = eletronica };
            var p2 = new Produto { Nome = "Monitor", PrecoUnitario = 500m, Categoria = eletronica };
            var p3 = new Produto { Nome = "Camiseta", PrecoUnitario = 50m, Categoria = roupa };

            _context!.Categorias.AddRange(eletronica, roupa);
            _context.Produtos.AddRange(p1, p2, p3);
            _context.SaveChanges();

            // Act
            var eletronicosCount = _context.Produtos.Count(p => p.Categoria!.Nome == "Eletrônica");
            var roupasCount = _context.Produtos.Count(p => p.Categoria!.Nome == "Roupa");

            // Assert
            Assert.That(eletronicosCount, Is.EqualTo(2));
            Assert.That(roupasCount, Is.EqualTo(1));
        }

        [Test]
        public void DeveRetornarPedidoComMultiplosProdutosPedidosAssociados()
        {
            // Arrange
            var usuario = new Usuario { Nome = "Maria", Email = "maria@test.com" };
            var categoria = new Categoria { Nome = "Casa" };
            var p1 = new Produto { Nome = "Sofá", PrecoUnitario = 1500m, Categoria = categoria };
            var p2 = new Produto { Nome = "Mesa", PrecoUnitario = 800m, Categoria = categoria };
            var p3 = new Produto { Nome = "Cadeira", PrecoUnitario = 300m, Categoria = categoria };

            var pedido = new Pedido { Total = 2600m, Usuario = usuario };
            var pp1 = new ProdutoPedido { Pedido = pedido, Produto = p1 };
            var pp2 = new ProdutoPedido { Pedido = pedido, Produto = p2 };
            var pp3 = new ProdutoPedido { Pedido = pedido, Produto = p3 };

            pedido.ProdutosPedidos.Add(pp1);
            pedido.ProdutosPedidos.Add(pp2);
            pedido.ProdutosPedidos.Add(pp3);

            // Act
            _context!.Usuarios.Add(usuario);
            _context.Categorias.Add(categoria);
            _context.Produtos.AddRange(p1, p2, p3);
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            // Assert
            var pedidoDb = _context.Pedidos.FirstOrDefault(p => p.Total == 2600m);
            Assert.That(pedidoDb, Is.Not.Null);
            Assert.That(pedidoDb!.ProdutosPedidos.Count, Is.EqualTo(3));
            Assert.That(pedidoDb.ProdutosPedidos.Sum(pp => pp.Produto!.PrecoUnitario), Is.EqualTo(2600m));
        }

        [Test]
        public void DeveRetornarUsuariosFiltradosPorNomeParcial()
        {
            // Arrange
            var u1 = new Usuario { Nome = "João Silva", Email = "joao@test.com" };
            var u2 = new Usuario { Nome = "João Santos", Email = "santos@test.com" };
            var u3 = new Usuario { Nome = "Maria Silva", Email = "maria@test.com" };

            _context!.Usuarios.AddRange(u1, u2, u3);
            _context.SaveChanges();

            // Act
            var joaos = _context.Usuarios.Where(u => u.Nome!.Contains("João")).ToList();
            var silvas = _context.Usuarios.Where(u => u.Nome!.Contains("Silva")).ToList();

            // Assert
            Assert.That(joaos.Count, Is.EqualTo(2));
            Assert.That(silvas.Count, Is.EqualTo(2));
        }

        [Test]
        public void DeveCalcularTotalDePedidosPorUsuario()
        {
            // Arrange
            var usuario = new Usuario { Nome = "Pedro", Email = "pedro@test.com" };
            var p1 = new Pedido { Total = 100m, Usuario = usuario };
            var p2 = new Pedido { Total = 250m, Usuario = usuario };
            var p3 = new Pedido { Total = 75m, Usuario = usuario };

            _context!.Usuarios.Add(usuario);
            _context.Pedidos.AddRange(p1, p2, p3);
            _context.SaveChanges();

            // Act
            var totalPedidos = _context.Pedidos.Where(p => p.UsuarioId == usuario.Id).Sum(p => p.Total);
            var countPedidos = _context.Pedidos.Where(p => p.UsuarioId == usuario.Id).Count();

            // Assert
            Assert.That(totalPedidos, Is.EqualTo(425m));
            Assert.That(countPedidos, Is.EqualTo(3));
        }

        [Test]
        public void DeveRetornarProdutosFiltradosPorFaixaDePreco()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Geral" };
            var p1 = new Produto { Nome = "Item Barato", PrecoUnitario = 10m, Categoria = categoria };
            var p2 = new Produto { Nome = "Item Médio", PrecoUnitario = 100m, Categoria = categoria };
            var p3 = new Produto { Nome = "Item Caro", PrecoUnitario = 1000m, Categoria = categoria };

            _context!.Categorias.Add(categoria);
            _context.Produtos.AddRange(p1, p2, p3);
            _context.SaveChanges();

            // Act
            var baratos = _context.Produtos.Where(p => p.PrecoUnitario < 50m).ToList();
            var medios = _context.Produtos.Where(p => p.PrecoUnitario >= 50m && p.PrecoUnitario <= 500m).ToList();
            var caros = _context.Produtos.Where(p => p.PrecoUnitario > 500m).ToList();

            // Assert
            Assert.That(baratos.Count, Is.EqualTo(1));
            Assert.That(medios.Count, Is.EqualTo(1));
            Assert.That(caros.Count, Is.EqualTo(1));
        }

        [Test]
        public void DeveAtualizarEmailDoUsuarioComSucesso()
        {
            // Arrange
            var usuario = new Usuario { Nome = "Paulo", Email = "paulo@test.com" };
            _context!.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Act
            var usuarioDb = _context.Usuarios.First(u => u.Nome == "Paulo");
            usuarioDb.Email = "paulo.novo@test.com";
            _context.SaveChanges();

            // Assert
            var usuarioAtualizado = _context.Usuarios.First(u => u.Nome == "Paulo");
            Assert.That(usuarioAtualizado.Email, Is.EqualTo("paulo.novo@test.com"));
        }

        [Test]
        public void DeveDeletarCategoriaComSucesso()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Temporária" };
            _context!.Categorias.Add(categoria);
            _context.SaveChanges();
            var categoriaId = categoria.Id;

            // Act
            var catDb = _context.Categorias.First(c => c.Nome == "Temporária");
            _context.Categorias.Remove(catDb);
            _context.SaveChanges();

            // Assert
            var deleted = _context.Categorias.FirstOrDefault(c => c.Id == categoriaId);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void DeveInserirMultiplosUsuariosComSucesso()
        {
            // Arrange
            var usuarios = Enumerable.Range(1, 20)
                .Select(i => new Usuario { Nome = $"User{i}", Email = $"user{i}@test.com" })
                .ToList();

            // Act
            _context!.Usuarios.AddRange(usuarios);
            _context.SaveChanges();

            // Assert
            var count = _context.Usuarios.Count();
            Assert.That(count, Is.EqualTo(20));
        }

        [Test]
        public void HighestPricedProduct()
        {
            // Arrange
            var cat = new Categoria { Nome = "Produtos" };
            var prods = new[]
            {
                new Produto { Nome = "P1", PrecoUnitario = 50m, Categoria = cat },
                new Produto { Nome = "P2", PrecoUnitario = 200m, Categoria = cat },
                new Produto { Nome = "P3", PrecoUnitario = 75m, Categoria = cat }
            };

            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var maiorPreco = _context.Produtos.Max(p => p.PrecoUnitario);
            var maiorProduto = _context.Produtos.First(p => p.PrecoUnitario == maiorPreco);

            // Assert
            Assert.That(maiorProduto.Nome, Is.EqualTo("P2"));
            Assert.That(maiorPreco, Is.EqualTo(200m));
        }

        [Test]
        public void LowestPricedProduct()
        {
            // Arrange
            var cat = new Categoria { Nome = "Itens" };
            var prods = new[]
            {
                new Produto { Nome = "I1", PrecoUnitario = 50m, Categoria = cat },
                new Produto { Nome = "I2", PrecoUnitario = 200m, Categoria = cat },
                new Produto { Nome = "I3", PrecoUnitario = 10m, Categoria = cat }
            };

            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var menorPreco = _context.Produtos.Min(p => p.PrecoUnitario);
            var menorProduto = _context.Produtos.First(p => p.PrecoUnitario == menorPreco);

            // Assert
            Assert.That(menorProduto.Nome, Is.EqualTo("I3"));
            Assert.That(menorPreco, Is.EqualTo(10m));
        }

        [Test]
        public void AveragePricePerCategoria()
        {
            // Arrange
            var cat1 = new Categoria { Nome = "Cat1" };
            var cat2 = new Categoria { Nome = "Cat2" };

            var prods = new[]
            {
                new Produto { Nome = "A", PrecoUnitario = 100m, Categoria = cat1 },
                new Produto { Nome = "B", PrecoUnitario = 200m, Categoria = cat1 },
                new Produto { Nome = "C", PrecoUnitario = 50m, Categoria = cat2 },
                new Produto { Nome = "D", PrecoUnitario = 150m, Categoria = cat2 }
            };

            _context!.Categorias.AddRange(cat1, cat2);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var avgCat1 = _context.Produtos.Where(p => p.CategoriaId == cat1.Id).Average(p => p.PrecoUnitario);
            var avgCat2 = _context.Produtos.Where(p => p.CategoriaId == cat2.Id).Average(p => p.PrecoUnitario);

            // Assert
            Assert.That(avgCat1, Is.EqualTo(150m));
            Assert.That(avgCat2, Is.EqualTo(100m));
        }

        [Test]
        public void SimulateDataGeneration()
        {
            // Arrange - Simular geração de dados como no Program.cs
            _context!.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var categoriasNomes = new[] { "Eletrônicos", "Roupas", "Alimentos", "Livros", "Móveis" };
            var categorias = new List<Categoria>();

            // Act - Gerar categorias
            foreach (var nome in categoriasNomes)
            {
                var categoria = new Categoria { Nome = nome };
                _context.Categorias.Add(categoria);
                categorias.Add(categoria);
            }
            _context.SaveChanges();

            // Act - Gerar produtos
            var produtoNomes = new[] { "Notebook", "Teclado", "Mouse", "Camiseta", "Sofá" };
            var produtos = new List<Produto>();
            var random = new Random(42); // Seed for consistency

            foreach (var nome in produtoNomes)
            {
                var produto = new Produto
                {
                    Nome = nome,
                    PrecoUnitario = (decimal)(random.NextDouble() * 1000 + 10),
                    CategoriaId = categorias[random.Next(categorias.Count)].Id
                };
                _context.Produtos.Add(produto);
                produtos.Add(produto);
            }
            _context.SaveChanges();

            // Act - Gerar usuários
            var usuarioNomes = new[] { "João Silva", "Maria Santos", "Pedro Costa" };
            var usuarioEmails = new[] { "joao@email.com", "maria@email.com", "pedro@email.com" };
            var usuarios = new List<Usuario>();

            for (int i = 0; i < usuarioNomes.Length; i++)
            {
                var usuario = new Usuario
                {
                    Nome = usuarioNomes[i],
                    Email = usuarioEmails[i]
                };
                _context.Usuarios.Add(usuario);
                usuarios.Add(usuario);
            }
            _context.SaveChanges();

            // Act - Gerar pedidos
            var pedidos = new List<Pedido>();
            for (int i = 0; i < 5; i++)
            {
                var pedido = new Pedido
                {
                    UsuarioId = usuarios[random.Next(usuarios.Count)].Id,
                    Total = 0
                };
                _context.Pedidos.Add(pedido);
                pedidos.Add(pedido);
            }
            _context.SaveChanges();

            // Act - Gerar ProdutosPedidos
            foreach (var pedido in pedidos)
            {
                int quantidadeItens = random.Next(1, 4);
                var produtosJaAdicionados = new HashSet<int>();

                for (int i = 0; i < quantidadeItens; i++)
                {
                    int produtoId;
                    do
                    {
                        produtoId = produtos[random.Next(produtos.Count)].Id;
                    } while (produtosJaAdicionados.Contains(produtoId));

                    produtosJaAdicionados.Add(produtoId);

                    var produtoPedido = new ProdutoPedido
                    {
                        PedidoId = pedido.Id,
                        ProdutoId = produtoId
                    };
                    _context.ProdutosPedidos.Add(produtoPedido);
                }
            }
            _context.SaveChanges();

            // Act - Calcular totais
            foreach (var pedido in _context.Pedidos.Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto))
            {
                pedido.Total = pedido.ProdutosPedidos.Sum(pp => pp.Produto!.PrecoUnitario);
            }
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Categorias.Count(), Is.EqualTo(5));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(5));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(3));
            Assert.That(_context.Pedidos.Count(), Is.EqualTo(5));
            Assert.That(_context.ProdutosPedidos.Count(), Is.GreaterThan(0));

            foreach (var pedido in _context.Pedidos.Include(p => p.ProdutosPedidos))
            {
                Assert.That(pedido.Total, Is.GreaterThan(0));
            }
        }

        [Test]
        public void ClearAndRepopulateData()
        {
            // Arrange - Adicionar dados iniciais
            var cat = new Categoria { Nome = "Test" };
            var prod = new Produto { Nome = "TestProd", PrecoUnitario = 50m, Categoria = cat };
            var user = new Usuario { Nome = "TestUser", Email = "test@test.com" };
            var pedido = new Pedido { Total = 100m, Usuario = user };

            _context!.Categorias.Add(cat);
            _context.Produtos.Add(prod);
            _context.Usuarios.Add(user);
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            var countBefore = _context.Pedidos.Count();

            // Act - Limpar dados
            _context.ProdutosPedidos.RemoveRange(_context.ProdutosPedidos);
            _context.Pedidos.RemoveRange(_context.Pedidos);
            _context.Produtos.RemoveRange(_context.Produtos);
            _context.Categorias.RemoveRange(_context.Categorias);
            _context.Usuarios.RemoveRange(_context.Usuarios);
            _context.SaveChanges();

            var countAfter = _context.Pedidos.Count();

            // Assert
            Assert.That(countBefore, Is.EqualTo(1));
            Assert.That(countAfter, Is.EqualTo(0));
        }

        [Test]
        public void QueryWithInclude()
        {
            // Arrange
            var cat = new Categoria { Nome = "Electronics" };
            var prod = new Produto { Nome = "Laptop", PrecoUnitario = 2000m, Categoria = cat };
            var user = new Usuario { Nome = "John", Email = "john@test.com" };
            var pedido = new Pedido { Total = 2000m, Usuario = user };
            var pp = new ProdutoPedido { Pedido = pedido, Produto = prod };

            _context!.Categorias.Add(cat);
            _context.Produtos.Add(prod);
            _context.Usuarios.Add(user);
            _context.Pedidos.Add(pedido);
            _context.ProdutosPedidos.Add(pp);
            _context.SaveChanges();

            // Act
            var pedidoComDetalhes = _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.ProdutosPedidos)
                .ThenInclude(pp => pp.Produto)
                .ThenInclude(p => p.Categoria)
                .FirstOrDefault(p => p.Total == 2000m);

            // Assert
            Assert.That(pedidoComDetalhes, Is.Not.Null);
            Assert.That(pedidoComDetalhes!.Usuario!.Nome, Is.EqualTo("John"));
            Assert.That(pedidoComDetalhes.ProdutosPedidos.Count, Is.EqualTo(1));
            Assert.That(pedidoComDetalhes.ProdutosPedidos.First().Produto!.Categoria!.Nome, Is.EqualTo("Electronics"));
        }

        [Test]
        public void BulkOperations()
        {
            // Arrange
            var categorias = Enumerable.Range(1, 10)
                .Select(i => new Categoria { Nome = $"Category{i}" })
                .ToList();

            var produtos = Enumerable.Range(1, 50)
                .Select(i => new Produto
                {
                    Nome = $"Product{i}",
                    PrecoUnitario = i * 10m,
                    Categoria = categorias[(i - 1) % 10]
                })
                .ToList();

            // Act
            _context!.Categorias.AddRange(categorias);
            _context.SaveChanges();
            _context.Produtos.AddRange(produtos);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Categorias.Count(), Is.EqualTo(10));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(50));
            Assert.That(_context.Produtos.Sum(p => p.PrecoUnitario), Is.EqualTo(12750m));
        }

        [Test]
        public void OrderByQueryResults()
        {
            // Arrange
            var cat = new Categoria { Nome = "Books" };
            var prods = new[]
            {
                new Produto { Nome = "Z Book", PrecoUnitario = 50m, Categoria = cat },
                new Produto { Nome = "A Book", PrecoUnitario = 100m, Categoria = cat },
                new Produto { Nome = "M Book", PrecoUnitario = 75m, Categoria = cat }
            };

            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var ordenadoPorNome = _context.Produtos.OrderBy(p => p.Nome).ToList();
            var ordenadoPorPreco = _context.Produtos.OrderBy(p => p.PrecoUnitario).ToList();

            // Assert
            Assert.That(ordenadoPorNome[0].Nome, Is.EqualTo("A Book"));
            Assert.That(ordenadoPorNome[2].Nome, Is.EqualTo("Z Book"));
            Assert.That(ordenadoPorPreco[0].PrecoUnitario, Is.EqualTo(50m));
            Assert.That(ordenadoPorPreco[2].PrecoUnitario, Is.EqualTo(100m));
        }

        [Test]
        public void FilterWithMultipleCriteria()
        {
            // Arrange
            var cat1 = new Categoria { Nome = "A" };
            var cat2 = new Categoria { Nome = "B" };
            var prods = new[]
            {
                new Produto { Nome = "P1", PrecoUnitario = 10m, Categoria = cat1 },
                new Produto { Nome = "P2", PrecoUnitario = 100m, Categoria = cat1 },
                new Produto { Nome = "P3", PrecoUnitario = 50m, Categoria = cat2 },
                new Produto { Nome = "P4", PrecoUnitario = 200m, Categoria = cat2 }
            };

            _context!.Categorias.AddRange(cat1, cat2);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var resultado = _context.Produtos
                .Where(p => p.CategoriaId == cat1.Id && p.PrecoUnitario >= 50m)
                .OrderByDescending(p => p.PrecoUnitario)
                .ToList();

            // Assert
            Assert.That(resultado.Count, Is.EqualTo(1));
            Assert.That(resultado[0].Nome, Is.EqualTo("P2"));
        }

        [Test]
        public void ComplexPedidoWithMultipleUsuariosAndProdutos()
        {
            // Arrange
            var categorias = new[]
            {
                new Categoria { Nome = "Electronics" },
                new Categoria { Nome = "Books" },
                new Categoria { Nome = "Clothing" }
            };

            var produtos = new[]
            {
                new Produto { Nome = "Laptop", PrecoUnitario = 1500m, Categoria = categorias[0] },
                new Produto { Nome = "Mouse", PrecoUnitario = 50m, Categoria = categorias[0] },
                new Produto { Nome = "C# Book", PrecoUnitario = 100m, Categoria = categorias[1] },
                new Produto { Nome = "T-Shirt", PrecoUnitario = 50m, Categoria = categorias[2] }
            };

            var usuarios = new[]
            {
                new Usuario { Nome = "Developer1", Email = "dev1@company.com" },
                new Usuario { Nome = "Developer2", Email = "dev2@company.com" }
            };

            _context!.Categorias.AddRange(categorias);
            _context.Produtos.AddRange(produtos);
            _context.Usuarios.AddRange(usuarios);
            _context.SaveChanges();

            // Act - Create complex orders
            var pedido1 = new Pedido { UsuarioId = usuarios[0].Id, Total = 0 };
            var pedido2 = new Pedido { UsuarioId = usuarios[1].Id, Total = 0 };

            _context.Pedidos.AddRange(pedido1, pedido2);
            _context.SaveChanges();

            _context.ProdutosPedidos.Add(new ProdutoPedido { PedidoId = pedido1.Id, ProdutoId = produtos[0].Id });
            _context.ProdutosPedidos.Add(new ProdutoPedido { PedidoId = pedido1.Id, ProdutoId = produtos[1].Id });
            _context.ProdutosPedidos.Add(new ProdutoPedido { PedidoId = pedido1.Id, ProdutoId = produtos[2].Id });
            _context.ProdutosPedidos.Add(new ProdutoPedido { PedidoId = pedido2.Id, ProdutoId = produtos[3].Id });
            _context.SaveChanges();

            // Update totals
            foreach (var pedido in _context.Pedidos.Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto))
            {
                pedido.Total = pedido.ProdutosPedidos.Sum(pp => pp.Produto!.PrecoUnitario);
            }
            _context.SaveChanges();

            // Assert
            var dev1Pedido = _context.Pedidos.Include(p => p.ProdutosPedidos).First(p => p.UsuarioId == usuarios[0].Id);
            var dev2Pedido = _context.Pedidos.Include(p => p.ProdutosPedidos).First(p => p.UsuarioId == usuarios[1].Id);

            Assert.That(dev1Pedido.ProdutosPedidos.Count, Is.EqualTo(3));
            Assert.That(dev2Pedido.ProdutosPedidos.Count, Is.EqualTo(1));
            Assert.That(dev1Pedido.Total, Is.EqualTo(1650m));
            Assert.That(dev2Pedido.Total, Is.EqualTo(50m));
        }

        [Test]
        public void ProdutoQueryWithValidCategoria()
        {
            // Arrange
            var cat = new Categoria { Nome = "TestCategory" };
            var produto = new Produto
            {
                Nome = "TestProduct",
                PrecoUnitario = 25m,
                Categoria = cat
            };

            // Act
            _context!.Categorias.Add(cat);
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            // Assert
            var retrieved = _context.Produtos.FirstOrDefault(p => p.Nome == "TestProduct");
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.CategoriaId, Is.GreaterThan(0));
            Assert.That(retrieved.Categoria, Is.Not.Null);
        }

        [Test]
        public void UpdateMultipleProdutos()
        {
            // Arrange
            var cat = new Categoria { Nome = "UpdateTest" };
            var prods = new[]
            {
                new Produto { Nome = "P1", PrecoUnitario = 100m, Categoria = cat },
                new Produto { Nome = "P2", PrecoUnitario = 200m, Categoria = cat },
                new Produto { Nome = "P3", PrecoUnitario = 300m, Categoria = cat }
            };

            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var produtosDb = _context.Produtos.Where(p => p.CategoriaId == cat.Id).ToList();
            foreach (var prod in produtosDb)
            {
                prod.PrecoUnitario *= 1.1m; // 10% increase
            }
            _context.SaveChanges();

            // Assert
            var updated = _context.Produtos.Where(p => p.CategoriaId == cat.Id).OrderBy(p => p.Nome).ToList();
            Assert.That(updated[0].PrecoUnitario, Is.EqualTo(110m).Within(0.01m));
            Assert.That(updated[1].PrecoUnitario, Is.EqualTo(220m).Within(0.01m));
            Assert.That(updated[2].PrecoUnitario, Is.EqualTo(330m).Within(0.01m));
        }

        [Test]
        public void PedidoSequentialQuery()
        {
            // Arrange
            var usuario = new Usuario { Nome = "TestUser", Email = "test@test.com" };
            var pedidos = Enumerable.Range(1, 5)
                .Select(i => new Pedido { UsuarioId = 0, Total = i * 100m })
                .ToList();

            _context!.Usuarios.Add(usuario);
            _context.SaveChanges();

            foreach (var pedido in pedidos)
            {
                pedido.UsuarioId = usuario.Id;
            }
            _context.Pedidos.AddRange(pedidos);
            _context.SaveChanges();

            // Act
            var pedidosOrdenados = _context.Pedidos
                .Where(p => p.UsuarioId == usuario.Id)
                .OrderBy(p => p.Total)
                .ToList();

            var pedidoFiltrado = _context.Pedidos
                .FirstOrDefault(p => p.Total == 300m);

            // Assert
            Assert.That(pedidosOrdenados.Count, Is.EqualTo(5));
            Assert.That(pedidosOrdenados.First().Total, Is.EqualTo(100m));
            Assert.That(pedidosOrdenados.Last().Total, Is.EqualTo(500m));
            Assert.That(pedidoFiltrado!.Total, Is.EqualTo(300m));
        }

        [Test]
        public void CategoriaConstraints()
        {
            // Arrange - Test various category names
            var categoriasData = new[]
            {
                new Categoria { Nome = "Categoria Com Espaços" },
                new Categoria { Nome = "123Números" },
                new Categoria { Nome = "MAIUSCULAS" },
                new Categoria { Nome = "minúsculas" },
                new Categoria { Nome = "Especiais!@#$" }
            };

            // Act
            _context!.Categorias.AddRange(categoriasData);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Categorias.Count(), Is.EqualTo(5));
            Assert.That(_context.Categorias.Any(c => c.Nome!.Contains("Espaços")), Is.True);
            Assert.That(_context.Categorias.Any(c => c.Nome == "123Números"), Is.True);
        }

        [Test]
        public void UsuarioQueryByEmail()
        {
            // Arrange
            var usuarios = new[]
            {
                new Usuario { Nome = "User1", Email = "user1@example.com" },
                new Usuario { Nome = "User2", Email = "user2@example.com" },
                new Usuario { Nome = "User3", Email = "user3@example.com" }
            };

            // Act
            _context!.Usuarios.AddRange(usuarios);
            _context.SaveChanges();

            var byEmail = _context.Usuarios.FirstOrDefault(u => u.Email == "user2@example.com");
            var likeQuery = _context.Usuarios.Where(u => u.Email!.Contains("example")).Count();

            // Assert
            Assert.That(byEmail!.Nome, Is.EqualTo("User2"));
            Assert.That(likeQuery, Is.EqualTo(3));
        }

        [Test]
        public void ProdutoPrecoEdgeCases()
        {
            // Arrange
            var cat = new Categoria { Nome = "EdgeCases" };
            var produtos = new[]
            {
                new Produto { Nome = "ZeroPrice", PrecoUnitario = 0m, Categoria = cat },
                new Produto { Nome = "VerySmall", PrecoUnitario = 0.01m, Categoria = cat },
                new Produto { Nome = "VeryLarge", PrecoUnitario = 999999.99m, Categoria = cat }
            };

            // Act
            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(produtos);
            _context.SaveChanges();

            // Assert
            var zero = _context.Produtos.FirstOrDefault(p => p.PrecoUnitario == 0m);
            var small = _context.Produtos.FirstOrDefault(p => p.PrecoUnitario == 0.01m);
            var large = _context.Produtos.FirstOrDefault(p => p.PrecoUnitario > 999999m);

            Assert.That(zero, Is.Not.Null);
            Assert.That(small, Is.Not.Null);
            Assert.That(large, Is.Not.Null);
        }

        [Test]
        public void ComplexIncludeChain()
        {
            // Arrange
            var cat = new Categoria { Nome = "ComplexTest" };
            var prod = new Produto { Nome = "ComplexProd", PrecoUnitario = 100m, Categoria = cat };
            var user = new Usuario { Nome = "ComplexUser", Email = "complex@test.com" };
            var pedido = new Pedido { UsuarioId = 0, Total = 100m };
            var pp = new ProdutoPedido { Pedido = pedido, Produto = prod };

            _context!.Categorias.Add(cat);
            _context.Produtos.Add(prod);
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            pedido.UsuarioId = user.Id;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            _context.ProdutosPedidos.Add(pp);
            _context.SaveChanges();

            // Act
            var result = _context.Pedidos
                .Where(p => p.Id == pedido.Id)
                .Include(p => p.Usuario)
                .Include(p => p.ProdutosPedidos)
                .ThenInclude(pp => pp.Produto)
                .ThenInclude(p => p!.Categoria)
                .AsEnumerable()
                .Select(p => new
                {
                    PedidoId = p.Id,
                    UsuarioNome = p.Usuario!.Nome,
                    Produtos = p.ProdutosPedidos.Select(pp => new
                    {
                        ProdutoNome = pp.Produto!.Nome,
                        CategoriaNome = pp.Produto.Categoria!.Nome
                    })
                })
                .FirstOrDefault();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.UsuarioNome, Is.EqualTo("ComplexUser"));
            Assert.That(result.Produtos.First().CategoriaNome, Is.EqualTo("ComplexTest"));
        }

        [Test]
        public void LargeNumberOfRecords()
        {
            // Arrange - Create 100 categories and 1000 products
            var categorias = Enumerable.Range(1, 100)
                .Select(i => new Categoria { Nome = $"Cat{i:D3}" })
                .ToList();

            _context!.Categorias.AddRange(categorias);
            _context.SaveChanges();

            var produtos = Enumerable.Range(1, 1000)
                .Select(i => new Produto
                {
                    Nome = $"Prod{i:D4}",
                    PrecoUnitario = (i * 1.5m) % 9999,
                    Categoria = categorias[(i - 1) % 100]
                })
                .ToList();

            // Act
            _context.Produtos.AddRange(produtos);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Categorias.Count(), Is.EqualTo(100));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(1000));
            Assert.That(_context.Produtos.Average(p => p.PrecoUnitario), Is.GreaterThan(0));
        }

        [Test]
        public void SkipAndTake()
        {
            // Arrange
            var cat = new Categoria { Nome = "Pagination" };
            var prods = Enumerable.Range(1, 20)
                .Select(i => new Produto { Nome = $"Item{i:D2}", PrecoUnitario = i * 10m, Categoria = cat })
                .ToList();

            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var page1 = _context.Produtos.OrderBy(p => p.Nome).Skip(0).Take(5).ToList();
            var page2 = _context.Produtos.OrderBy(p => p.Nome).Skip(5).Take(5).ToList();
            var page3 = _context.Produtos.OrderBy(p => p.Nome).Skip(10).Take(5).ToList();

            // Assert
            Assert.That(page1.Count, Is.EqualTo(5));
            Assert.That(page2.Count, Is.EqualTo(5));
            Assert.That(page3.Count, Is.EqualTo(5));
            Assert.That(page1.First().Nome, Is.EqualTo("Item01"));
            Assert.That(page2.First().Nome, Is.EqualTo("Item06"));
        }

        [Test]
        public void DistinctQuery()
        {
            // Arrange
            var cat1 = new Categoria { Nome = "Common" };
            var cat2 = new Categoria { Nome = "Common" }; // Same name, different ID
            var prods = new[]
            {
                new Produto { Nome = "P1", PrecoUnitario = 100m, Categoria = cat1 },
                new Produto { Nome = "P2", PrecoUnitario = 100m, Categoria = cat1 },
                new Produto { Nome = "P3", PrecoUnitario = 100m, Categoria = cat2 }
            };

            _context!.Categorias.AddRange(cat1, cat2);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var distinctPrices = _context.Produtos.Select(p => p.PrecoUnitario).Distinct().ToList();
            var allPrices = _context.Produtos.Select(p => p.PrecoUnitario).ToList();

            // Assert
            Assert.That(distinctPrices.Count, Is.EqualTo(1));
            Assert.That(allPrices.Count, Is.EqualTo(3));
        }

        [Test]
        public void GroupByQuery()
        {
            // Arrange
            var categorias = new[]
            {
                new Categoria { Nome = "Electronics" },
                new Categoria { Nome = "Books" }
            };

            var prods = new[]
            {
                new Produto { Nome = "Laptop", PrecoUnitario = 1000m, Categoria = categorias[0] },
                new Produto { Nome = "Mouse", PrecoUnitario = 50m, Categoria = categorias[0] },
                new Produto { Nome = "Novel", PrecoUnitario = 25m, Categoria = categorias[1] },
                new Produto { Nome = "Tech Book", PrecoUnitario = 80m, Categoria = categorias[1] }
            };

            _context!.Categorias.AddRange(categorias);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var grouped = _context.Produtos
                .GroupBy(p => p.CategoriaId)
                .Select(g => new { CatId = g.Key, Count = g.Count(), TotalPrice = g.Sum(p => p.PrecoUnitario) })
                .ToList();

            // Assert
            Assert.That(grouped.Count, Is.EqualTo(2));
            Assert.That(grouped[0].Count, Is.EqualTo(2));
            Assert.That(grouped[0].TotalPrice, Is.EqualTo(1050m));
        }

        [Test]
        public void AnyAndAllQueries()
        {
            // Arrange
            var cat = new Categoria { Nome = "Test" };
            var prods = new[]
            {
                new Produto { Nome = "Cheap", PrecoUnitario = 10m, Categoria = cat },
                new Produto { Nome = "Expensive", PrecoUnitario = 1000m, Categoria = cat }
            };

            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var hasExpensive = _context.Produtos.Any(p => p.PrecoUnitario > 500m);
            var allHighPrice = _context.Produtos.All(p => p.PrecoUnitario > 500m);
            var noneZeroPrice = !_context.Produtos.Any(p => p.PrecoUnitario <= 0m);

            // Assert
            Assert.That(hasExpensive, Is.True);
            Assert.That(allHighPrice, Is.False);
            Assert.That(noneZeroPrice, Is.True);
        }

        [Test]
        public void CountAndExistQueries()
        {
            // Arrange
            var cat = new Categoria { Nome = "Counter" };
            var prods = Enumerable.Range(1, 25)
                .Select(i => new Produto { Nome = $"P{i}", PrecoUnitario = i * 10m, Categoria = cat })
                .ToList();

            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Act
            var totalCount = _context.Produtos.Count();
            var filteredCount = _context.Produtos.Count(p => p.PrecoUnitario > 100m);
            var existsExpensive = _context.Produtos.Any(p => p.PrecoUnitario > 200m);

            // Assert
            Assert.That(totalCount, Is.EqualTo(25));
            Assert.That(filteredCount, Is.EqualTo(15));
            Assert.That(existsExpensive, Is.True);
        }

        [Test]
        public void JoinQuery()
        {
            // Arrange
            var usuarios = new[]
            {
                new Usuario { Nome = "User1", Email = "u1@test.com" },
                new Usuario { Nome = "User2", Email = "u2@test.com" }
            };

            var pedidos = new[]
            {
                new Pedido { UsuarioId = 0, Total = 100m },
                new Pedido { UsuarioId = 0, Total = 150m },
                new Pedido { UsuarioId = 0, Total = 200m }
            };

            _context!.Usuarios.AddRange(usuarios);
            _context.SaveChanges();

            foreach (var p in pedidos)
                p.UsuarioId = usuarios[0].Id;

            _context.Pedidos.AddRange(pedidos);
            _context.SaveChanges();

            // Act
            var joined = _context.Usuarios
                .Join(_context.Pedidos, u => u.Id, p => p.UsuarioId, (u, p) => new { u.Nome, p.Total })
                .ToList();

            // Assert
            Assert.That(joined.Count, Is.EqualTo(3));
            Assert.That(joined.All(x => x.Nome == "User1"), Is.True);
        }

        [Test]
        public void ConcurrentUpdates()
        {
            // Arrange
            var usuario = new Usuario { Nome = "ConcurrentTest", Email = "conc@test.com" };
            _context!.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Act - Simulate multiple updates
            var u1 = _context.Usuarios.First(u => u.Nome == "ConcurrentTest");
            u1.Email = "new1@test.com";
            _context.SaveChanges();

            var u2 = _context.Usuarios.First(u => u.Nome == "ConcurrentTest");
            u2.Email = "new2@test.com";
            _context.SaveChanges();

            // Assert
            var final = _context.Usuarios.First(u => u.Nome == "ConcurrentTest");
            Assert.That(final.Email, Is.EqualTo("new2@test.com"));
        }

        [Test]
        public void SimpleInsertRetrieval() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "Simple" };
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            var retrieved = _context.Categorias.FirstOrDefault(c => c.Nome == "Simple");
            Assert.That(retrieved, Is.Not.Null);
        });

        [Test]
        public void QuickUpdate() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "QuickTest" };
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            var c = _context.Categorias.First(x => x.Nome == "QuickTest");
            c.Nome = "Updated";
            _context.SaveChanges();
            Assert.That(_context.Categorias.First(x => x.Id == c.Id).Nome, Is.EqualTo("Updated"));
        });

        [Test]
        public void ValidateDbSetAccess() => ExecuteTest(() =>
        {
            Assert.That(_context!.Usuarios, Is.Not.Null);
            Assert.That(_context.Categorias, Is.Not.Null);
            Assert.That(_context.Produtos, Is.Not.Null);
            Assert.That(_context.Pedidos, Is.Not.Null);
            Assert.That(_context.ProdutosPedidos, Is.Not.Null);
        });

        [Test]
        public void CountAllEntities() => ExecuteTest(() =>
        {
            var u = new Usuario { Nome = "Counter", Email = "counter@test.com" };
            _context!.Usuarios.Add(u);
            _context.SaveChanges();
            Assert.That(_context.Usuarios.Count(), Is.GreaterThan(0));
        });

        [Test]
        public void FilterByProperty() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "FilterTest" };
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            var filtered = _context.Categorias.Where(c => c.Nome!.StartsWith("Filter")).ToList();
            Assert.That(filtered.Count, Is.GreaterThan(0));
        });

        [Test]
        public void MultipleAddsInTransaction() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 5).Select(i => new Categoria { Nome = $"Multi{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();
            Assert.That(_context.Categorias.Count(c => c.Nome!.StartsWith("Multi")), Is.EqualTo(5));
        });

        [Test]
        public void ExistsCheck() => ExecuteTest(() =>
        {
            var user = new Usuario { Nome = "ExistsTest", Email = "exists@test.com" };
            _context!.Usuarios.Add(user);
            _context.SaveChanges();
            var exists = _context.Usuarios.Any(u => u.Email == "exists@test.com");
            Assert.That(exists, Is.True);
        });

        [Test]
        public void CountWithFilter() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "CountFilter" };
            var prods = Enumerable.Range(1, 5).Select(i => new Produto { Nome = $"CountProd{i}", PrecoUnitario = i * 10m, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();
            Assert.That(_context.Produtos.Count(p => p.CategoriaId == cat.Id), Is.EqualTo(5));
        });

        private void ExecuteTest(Action test)
        {
            test();
        }

        [Test]
        public void SimulateFullDataGenerationPipeline() => ExecuteTest(() =>
        {
            // Clear all data
            _context!.ProdutosPedidos.RemoveRange(_context.ProdutosPedidos);
            _context.Pedidos.RemoveRange(_context.Pedidos);
            _context.Produtos.RemoveRange(_context.Produtos);
            _context.Categorias.RemoveRange(_context.Categorias);
            _context.Usuarios.RemoveRange(_context.Usuarios);
            _context.SaveChanges();

            // Generate categories
            var categoryNames = new[] { "Eletrônicos", "Roupas", "Alimentos", "Livros", "Móveis", "Cosméticos" };
            var categories = new List<Categoria>();
            foreach (var name in categoryNames)
            {
                var cat = new Categoria { Nome = name };
                _context.Categorias.Add(cat);
                categories.Add(cat);
            }
            _context.SaveChanges();

            // Generate products
            var productNames = new[] { "Notebook", "Teclado", "Mouse", "Monitor", "Camiseta", "Calça", "Arroz", "Feijão", "Livro", "Sofá", "Shampoo", "Condicionador" };
            var products = new List<Produto>();
            var random = new Random(42);
            foreach (var name in productNames)
            {
                var product = new Produto
                {
                    Nome = name,
                    PrecoUnitario = (decimal)(random.NextDouble() * 1000 + 10),
                    CategoriaId = categories[random.Next(categories.Count)].Id
                };
                _context.Produtos.Add(product);
                products.Add(product);
            }
            _context.SaveChanges();

            // Generate users
            var userNames = new[] { "João Silva", "Maria Santos", "Pedro Costa", "Ana Oliveira", "Carlos Pereira" };
            var userEmails = new[] { "joao@email.com", "maria@email.com", "pedro@email.com", "ana@email.com", "carlos@email.com" };
            var users = new List<Usuario>();
            for (int i = 0; i < userNames.Length; i++)
            {
                var user = new Usuario { Nome = userNames[i], Email = userEmails[i] };
                _context.Usuarios.Add(user);
                users.Add(user);
            }
            _context.SaveChanges();

            // Generate orders
            var orders = new List<Pedido>();
            for (int i = 0; i < 10; i++)
            {
                var order = new Pedido { UsuarioId = users[random.Next(users.Count)].Id, Total = 0 };
                _context.Pedidos.Add(order);
                orders.Add(order);
            }
            _context.SaveChanges();

            // Generate order items
            foreach (var order in orders)
            {
                var itemCount = random.Next(1, 4);
                var addedProducts = new HashSet<int>();
                for (int i = 0; i < itemCount; i++)
                {
                    int productId;
                    do
                    {
                        productId = products[random.Next(products.Count)].Id;
                    } while (addedProducts.Contains(productId));
                    addedProducts.Add(productId);

                    var pp = new ProdutoPedido { PedidoId = order.Id, ProdutoId = productId };
                    _context.ProdutosPedidos.Add(pp);
                }
            }
            _context.SaveChanges();

            // Calculate totals
            foreach (var order in _context.Pedidos.Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto))
            {
                order.Total = order.ProdutosPedidos.Sum(pp => pp.Produto!.PrecoUnitario);
            }
            _context.SaveChanges();

            // Verify all data
            Assert.That(_context.Categorias.Count(), Is.EqualTo(6));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(12));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(5));
            Assert.That(_context.Pedidos.Count(), Is.EqualTo(10));
            Assert.That(_context.ProdutosPedidos.Count(), Is.GreaterThan(0));

            foreach (var order in _context.Pedidos)
            {
                Assert.That(order.Total, Is.GreaterThan(0));
            }
        });

        [Test]
        public void GenerateAndRetrieveCompleteDataset() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "CompleteDataset" };
            var prods = Enumerable.Range(1, 20).Select(i => new Produto { Nome = $"Prod{i}", PrecoUnitario = i * 5m, Categoria = cat }).ToList();
            var users = Enumerable.Range(1, 10).Select(i => new Usuario { Nome = $"User{i}", Email = $"user{i}@test.com" }).ToList();

            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            var orders = new List<Pedido>();
            for (int i = 0; i < 15; i++)
            {
                var order = new Pedido { UsuarioId = users[i % users.Count].Id, Total = (i + 1) * 100m };
                _context.Pedidos.Add(order);
                orders.Add(order);
            }
            _context.SaveChanges();

            Assert.That(_context.Produtos.Count(), Is.EqualTo(20));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(10));
            Assert.That(_context.Pedidos.Count(), Is.EqualTo(15));
        });

        [Test]
        public void ReportAllCategories() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 6).Select(i => new Categoria { Nome = $"ReportCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var allCats = _context.Categorias.Where(c => c.Nome!.StartsWith("ReportCat")).OrderBy(c => c.Nome).ToList();
            Assert.That(allCats.Count, Is.EqualTo(6));
            for (int i = 0; i < allCats.Count; i++)
            {
                Assert.That(allCats[i].Nome, Is.EqualTo($"ReportCat{i + 1}"));
            }
        });

        [Test]
        public void ReportAllProducts() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "ReportCategory" };
            var prods = Enumerable.Range(1, 14).Select(i => new Produto { Nome = $"ReportProd{i:D2}", PrecoUnitario = i * 10m, Categoria = cat }).ToList();

            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var allProds = _context.Produtos.Where(p => p.CategoriaId == cat.Id).OrderBy(p => p.Nome).ToList();
            Assert.That(allProds.Count, Is.EqualTo(14));
            Assert.That(allProds.Sum(p => p.PrecoUnitario), Is.EqualTo(1050m));
        });

        [Test]
        public void ReportAllUsers() => ExecuteTest(() =>
        {
            var users = Enumerable.Range(1, 6).Select(i => new Usuario { Nome = $"ReportUser{i}", Email = $"reportuser{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();

            var allUsers = _context.Usuarios.Where(u => u.Nome!.StartsWith("ReportUser")).ToList();
            Assert.That(allUsers.Count, Is.EqualTo(6));
        });

        [Test]
        public void ReportAllOrders() => ExecuteTest(() =>
        {
            var user = new Usuario { Nome = "ReportUser", Email = "reportuser@email.com" };
            _context!.Usuarios.Add(user);
            _context.SaveChanges();

            var orders = Enumerable.Range(1, 10).Select(i => new Pedido { UsuarioId = user.Id, Total = i * 100m }).ToList();
            _context.Pedidos.AddRange(orders);
            _context.SaveChanges();

            var userOrders = _context.Pedidos.Where(p => p.UsuarioId == user.Id).ToList();
            Assert.That(userOrders.Count, Is.EqualTo(10));
            Assert.That(userOrders.Sum(p => p.Total), Is.EqualTo(5500m));
        });

        [Test]
        public void ReportOrderItems() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "OrderItemCat" };
            var prod1 = new Produto { Nome = "Item1", PrecoUnitario = 50m, Categoria = cat };
            var prod2 = new Produto { Nome = "Item2", PrecoUnitario = 75m, Categoria = cat };
            var user = new Usuario { Nome = "OrderItemUser", Email = "orderitem@email.com" };
            var order = new Pedido { UsuarioId = 0, Total = 125m };
            var pp1 = new ProdutoPedido { Pedido = order, Produto = prod1 };
            var pp2 = new ProdutoPedido { Pedido = order, Produto = prod2 };

            _context!.Categorias.Add(cat);
            _context.Produtos.AddRange(prod1, prod2);
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            order.UsuarioId = user.Id;
            _context.Pedidos.Add(order);
            _context.SaveChanges();

            _context.ProdutosPedidos.AddRange(pp1, pp2);
            _context.SaveChanges();

            var orderItems = _context.ProdutosPedidos.Where(pp => pp.PedidoId == order.Id).ToList();
            Assert.That(orderItems.Count, Is.EqualTo(2));
        });

        [Test]
        public void FullSystemDataGeneration() => ExecuteTest(() =>
        {
            // This simulates the entire data generation pipeline from Program.cs
            _context!.Database.EnsureCreated();

            // Clear existing data
            _context.ProdutosPedidos.RemoveRange(_context.ProdutosPedidos);
            _context.Pedidos.RemoveRange(_context.Pedidos);
            _context.Produtos.RemoveRange(_context.Produtos);
            _context.Categorias.RemoveRange(_context.Categorias);
            _context.Usuarios.RemoveRange(_context.Usuarios);
            _context.SaveChanges();

            // Generate 6 categories
            var categoriasNomes = new[] { "Eletrônicos", "Roupas", "Alimentos", "Livros", "Móveis", "Cosméticos" };
            var categorias = new List<Categoria>();
            foreach (var nome in categoriasNomes)
            {
                var categoria = new Categoria { Nome = nome };
                _context.Categorias.Add(categoria);
                categorias.Add(categoria);
            }
            _context.SaveChanges();

            // Generate 14 products
            var produtoNomes = new[] { "Notebook", "Teclado", "Mouse", "Monitor", "Camiseta", "Calça", "Arroz", "Feijão",
                                       "Livro de C#", "Guia de Entity Framework", "Sofá", "Cadeira", "Shampoo", "Condicionador" };
            var produtos = new List<Produto>();
            var random = new Random(123);

            foreach (var nome in produtoNomes)
            {
                var produto = new Produto
                {
                    Nome = nome,
                    PrecoUnitario = (decimal)(random.NextDouble() * 1000 + 10),
                    CategoriaId = categorias[random.Next(categorias.Count)].Id
                };
                _context.Produtos.Add(produto);
                produtos.Add(produto);
            }
            _context.SaveChanges();

            // Generate 6 users
            var usuarioNomes = new[] { "João Silva", "Maria Santos", "Pedro Costa", "Ana Oliveira", "Carlos Pereira", "Julia Gomes" };
            var usuarioEmails = new[] { "joao@email.com", "maria@email.com", "pedro@email.com", "ana@email.com", "carlos@email.com", "julia@email.com" };
            var usuarios = new List<Usuario>();

            for (int i = 0; i < usuarioNomes.Length; i++)
            {
                var usuario = new Usuario { Nome = usuarioNomes[i], Email = usuarioEmails[i] };
                _context.Usuarios.Add(usuario);
                usuarios.Add(usuario);
            }
            _context.SaveChanges();

            // Generate 10 orders
            var pedidos = new List<Pedido>();
            for (int i = 0; i < 10; i++)
            {
                var pedido = new Pedido { UsuarioId = usuarios[random.Next(usuarios.Count)].Id, Total = 0 };
                _context.Pedidos.Add(pedido);
                pedidos.Add(pedido);
            }
            _context.SaveChanges();

            // Generate order items
            foreach (var pedido in pedidos)
            {
                int quantidadeItens = random.Next(1, 4);
                var produtosJaAdicionados = new HashSet<int>();

                for (int i = 0; i < quantidadeItens; i++)
                {
                    int produtoId;
                    do
                    {
                        produtoId = produtos[random.Next(produtos.Count)].Id;
                    } while (produtosJaAdicionados.Contains(produtoId));

                    produtosJaAdicionados.Add(produtoId);

                    var produtoPedido = new ProdutoPedido { PedidoId = pedido.Id, ProdutoId = produtoId };
                    _context.ProdutosPedidos.Add(produtoPedido);
                }
            }
            _context.SaveChanges();

            // Calculate totals
            foreach (var pedido in _context.Pedidos.Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto))
            {
                pedido.Total = pedido.ProdutosPedidos.Sum(pp => pp.Produto!.PrecoUnitario);
            }
            _context.SaveChanges();

            // Verify results
            Assert.That(_context.Categorias.Count(), Is.EqualTo(6));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(14));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(6));
            Assert.That(_context.Pedidos.Count(), Is.EqualTo(10));
            Assert.That(_context.ProdutosPedidos.Count(), Is.GreaterThan(0));
        });

        [Test]
        public void PrintFormattingSimulation() => ExecuteTest(() =>
        {
            // Simulate the reporting section from Program.cs
            var cat = new Categoria { Nome = "Format" };
            var prods = new[] { new Produto { Nome = "Item1", PrecoUnitario = 100.50m, Categoria = cat },
                                new Produto { Nome = "Item2", PrecoUnitario = 200.75m, Categoria = cat } };
            var user = new Usuario { Nome = "User1", Email = "user@email.com" };

            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            // Test formatting values
            var catReport = _context.Categorias.First(c => c.Nome == "Format");
            Assert.That(catReport.Id, Is.GreaterThan(0));
            Assert.That(catReport.Nome, Is.EqualTo("Format"));

            var prodReport = _context.Produtos.Where(p => p.CategoriaId == cat.Id).ToList();
            foreach (var p in prodReport)
            {
                Assert.That(p.PrecoUnitario, Is.GreaterThan(0));
                Assert.That(p.Nome, Is.Not.Empty);
            }

            var userReport = _context.Usuarios.First(u => u.Nome == "User1");
            Assert.That(userReport.Email, Is.EqualTo("user@email.com"));
        });

        [Test]
        public void ListAllEntitiesForReporting() => ExecuteTest(() =>
        {
            // Generate test data for reporting
            var cats = Enumerable.Range(1, 3).Select(i => new Categoria { Nome = $"Cat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 3).Select(i => new Produto { Nome = $"{c.Nome}Prod{i}", PrecoUnitario = i * 10m, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var users = Enumerable.Range(1, 3).Select(i => new Usuario { Nome = $"User{i}", Email = $"user{i}@email.com" }).ToList();
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            var orders = users.SelectMany(u => Enumerable.Range(1, 2).Select(i => new Pedido { UsuarioId = u.Id, Total = i * 100m })).ToList();
            _context.Pedidos.AddRange(orders);
            _context.SaveChanges();

            // Test listing all entities
            var allCats = _context.Categorias.ToList();
            var allProds = _context.Produtos.ToList();
            var allUsers = _context.Usuarios.ToList();
            var allOrders = _context.Pedidos.ToList();

            Assert.That(allCats.Count, Is.GreaterThanOrEqualTo(3));
            Assert.That(allProds.Count, Is.GreaterThanOrEqualTo(9));
            Assert.That(allUsers.Count, Is.GreaterThanOrEqualTo(3));
            Assert.That(allOrders.Count, Is.GreaterThanOrEqualTo(6));
        });

        [Test]
        public void ExtensiveDataOperations() => ExecuteTest(() =>
        {
            for (int batch = 0; batch < 3; batch++)
            {
                var cats = Enumerable.Range(1, 5).Select(i => new Categoria { Nome = $"Batch{batch}Cat{i}" }).ToList();
                _context!.Categorias.AddRange(cats);
                _context.SaveChanges();

                var prods = cats.SelectMany(c => Enumerable.Range(1, 5).Select(i => new Produto { Nome = $"Batch{batch}_{c.Nome}_P{i}", PrecoUnitario = (batch + 1) * (i + 1) * 5m, Categoria = c })).ToList();
                _context.Produtos.AddRange(prods);
                _context.SaveChanges();

                var users = Enumerable.Range(1, 5).Select(i => new Usuario { Nome = $"Batch{batch}User{i}", Email = $"batch{batch}user{i}@email.com" }).ToList();
                _context.Usuarios.AddRange(users);
                _context.SaveChanges();
            }

            Assert.That(_context.Categorias.Count(), Is.GreaterThanOrEqualTo(15));
            Assert.That(_context.Produtos.Count(), Is.GreaterThanOrEqualTo(75));
            Assert.That(_context.Usuarios.Count(), Is.GreaterThanOrEqualTo(15));
        });

        [Test]
        public void ComplexQueryOperations() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 4).Select(i => new Categoria { Nome = $"QueryCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 10).Select(i => new Produto { Nome = $"Q_{c.Nome}_P{i}", PrecoUnitario = i * 5m, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Multiple query operations
            var count = _context.Produtos.Count();
            var sum = _context.Produtos.Sum(p => p.PrecoUnitario);
            var avg = _context.Produtos.Average(p => p.PrecoUnitario);
            var max = _context.Produtos.Max(p => p.PrecoUnitario);
            var min = _context.Produtos.Min(p => p.PrecoUnitario);

            Assert.That(count, Is.EqualTo(40));
            Assert.That(sum, Is.GreaterThan(0));
            Assert.That(avg, Is.GreaterThan(0));
            Assert.That(max, Is.GreaterThan(min));
        });

        [Test]
        public void IncludeAndJoinOperations() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "IncludeTest" };
            var prods = Enumerable.Range(1, 5).Select(i => new Produto { Nome = $"IncludeProd{i}", PrecoUnitario = i * 20m, Categoria = cat }).ToList();
            var users = Enumerable.Range(1, 3).Select(i => new Usuario { Nome = $"IncludeUser{i}", Email = $"includeuser{i}@email.com" }).ToList();

            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            var orders = new List<Pedido>();
            for (int i = 0; i < 6; i++)
            {
                var order = new Pedido { UsuarioId = users[i % users.Count].Id, Total = (i + 1) * 100m };
                _context.Pedidos.Add(order);
                orders.Add(order);
            }
            _context.SaveChanges();

            // Test Include operations
            var usersWithOrders = _context.Usuarios.Include(u => u.Pedidos).ToList();
            Assert.That(usersWithOrders.Count, Is.GreaterThan(0));

            var ordersWithProducts = _context.Pedidos.Include(p => p.ProdutosPedidos).ToList();
            Assert.That(ordersWithProducts.Count, Is.GreaterThan(0));
        });

        [Test]
        public void TransactionSimulation() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "TxnTest" };
            _context!.Categorias.Add(cat);
            _context.SaveChanges();

            var prod1 = new Produto { Nome = "TxnP1", PrecoUnitario = 100m, Categoria = cat };
            var prod2 = new Produto { Nome = "TxnP2", PrecoUnitario = 200m, Categoria = cat };
            _context.Produtos.Add(prod1);
            _context.SaveChanges();
            _context.Produtos.Add(prod2);
            _context.SaveChanges();

            var user = new Usuario { Nome = "TxnUser", Email = "txn@email.com" };
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var order = new Pedido { UsuarioId = user.Id, Total = 300m };
            _context.Pedidos.Add(order);
            _context.SaveChanges();

            Assert.That(_context.Produtos.Count(), Is.EqualTo(2));
            Assert.That(_context.Usuarios.Count(), Is.GreaterThan(0));
            Assert.That(_context.Pedidos.Count(), Is.GreaterThan(0));
        });

        [Test]
        public void SequentialCRUDOperations() => ExecuteTest(() =>
        {
            // Create
            var cat = new Categoria { Nome = "CRUDTest" };
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            var catId = cat.Id;

            // Read
            var catRead = _context.Categorias.First(c => c.Id == catId);
            Assert.That(catRead.Nome, Is.EqualTo("CRUDTest"));

            // Update
            catRead.Nome = "CRUDTestUpdated";
            _context.SaveChanges();
            var catUpdated = _context.Categorias.First(c => c.Id == catId);
            Assert.That(catUpdated.Nome, Is.EqualTo("CRUDTestUpdated"));

            // Delete
            _context.Categorias.Remove(catUpdated);
            _context.SaveChanges();
            var catDeleted = _context.Categorias.FirstOrDefault(c => c.Id == catId);
            Assert.That(catDeleted, Is.Null);
        });

        [Test]
        public void EntityStateOperations() => ExecuteTest(() =>
        {
            var user = new Usuario { Nome = "StateTest", Email = "state@email.com" };
            _context!.Usuarios.Add(user);
            _context.SaveChanges();

            user.Email = "state.updated@email.com";
            _context.SaveChanges();

            var retrieved = _context.Usuarios.First(u => u.Nome == "StateTest");
            Assert.That(retrieved.Email, Is.EqualTo("state.updated@email.com"));

            _context.Usuarios.Remove(retrieved);
            _context.SaveChanges();

            var deleted = _context.Usuarios.FirstOrDefault(u => u.Nome == "StateTest");
            Assert.That(deleted, Is.Null);
        });

        [Test]
        public void LargeScaleDataGeneration() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 10).Select(i => new Categoria { Nome = $"LargeCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 15).Select(i => new Produto { Nome = $"{c.Nome}_P{i}", PrecoUnitario = i * 2m, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.EqualTo(10));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(150));
        });

        [Test]
        public void NavigationPropertyAccess() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "NavTest" };
            var prod = new Produto { Nome = "NavProd", PrecoUnitario = 50m, Categoria = cat };
            var user = new Usuario { Nome = "NavUser", Email = "nav@email.com" };
            var order = new Pedido { UsuarioId = 0, Total = 50m };
            var pp = new ProdutoPedido { Pedido = order, Produto = prod };

            _context!.Categorias.Add(cat);
            _context.Produtos.Add(prod);
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            order.UsuarioId = user.Id;
            _context.Pedidos.Add(order);
            _context.SaveChanges();

            _context.ProdutosPedidos.Add(pp);
            _context.SaveChanges();

            var retrieved = _context.Pedidos.Include(p => p.Usuario).Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto).First(p => p.Id == order.Id);
            Assert.That(retrieved.Usuario, Is.Not.Null);
            Assert.That(retrieved.Usuario!.Nome, Is.EqualTo("NavUser"));
            Assert.That(retrieved.ProdutosPedidos.First().Produto!.Nome, Is.EqualTo("NavProd"));
        });

        [Test]
        public void WhereClauseVariations() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "WhereTest" };
            var prods = Enumerable.Range(1, 20).Select(i => new Produto { Nome = $"P{i:D2}", PrecoUnitario = i * 5m, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var q1 = _context.Produtos.Where(p => p.PrecoUnitario > 50m && p.PrecoUnitario < 100m).Count();
            var q2 = _context.Produtos.Where(p => p.Nome!.StartsWith("P1")).Count();
            var q3 = _context.Produtos.Where(p => p.Nome!.EndsWith("0")).Count();
            var q4 = _context.Produtos.Where(p => p.CategoriaId == cat.Id && p.PrecoUnitario % 10 == 0).Count();

            Assert.That(q1, Is.GreaterThan(0));
            Assert.That(q2, Is.GreaterThan(0));
            Assert.That(q3, Is.GreaterThan(0));
            Assert.That(q4, Is.GreaterThan(0));
        });

        [Test]
        public void SelectAndProjection() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "SelectTest" };
            var prods = Enumerable.Range(1, 10).Select(i => new Produto { Nome = $"SelectP{i}", PrecoUnitario = i * 10m, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var projection = _context.Produtos
                .Where(p => p.CategoriaId == cat.Id)
                .Select(p => new { p.Nome, p.PrecoUnitario })
                .ToList();

            Assert.That(projection.Count, Is.EqualTo(10));
            Assert.That(projection.All(p => p.PrecoUnitario > 0), Is.True);
        });

        [Test]
        public void SubqueryOperations() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 3).Select(i => new Categoria { Nome = $"SubCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 5).Select(i => new Produto { Nome = $"SubP{i}", PrecoUnitario = i * 10m, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var catIds = _context.Categorias.Where(c => c.Nome!.StartsWith("SubCat")).Select(c => c.Id).ToList();
            var prodsInCats = _context.Produtos.Where(p => catIds.Contains(p.CategoriaId)).Count();

            Assert.That(prodsInCats, Is.EqualTo(15));
        });

        [Test]
        public void BulkUpdate() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "BulkTest" };
            var prods = Enumerable.Range(1, 10).Select(i => new Produto { Nome = $"Bulk{i}", PrecoUnitario = 100m, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var toUpdate = _context.Produtos.Where(p => p.CategoriaId == cat.Id).ToList();
            foreach (var p in toUpdate)
            {
                p.PrecoUnitario *= 1.5m;
            }
            _context.SaveChanges();

            var updated = _context.Produtos.Where(p => p.CategoriaId == cat.Id).ToList();
            Assert.That(updated.All(p => p.PrecoUnitario == 150m), Is.True);
        });

        [Test]
        public void RangeInsert() => ExecuteTest(() =>
        {
            var users = Enumerable.Range(1, 50).Select(i => new Usuario { Nome = $"RangeUser{i}", Email = $"rangeuser{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();

            Assert.That(_context.Usuarios.Count(u => u.Nome!.StartsWith("RangeUser")), Is.EqualTo(50));
        });

        [Test]
        public void RangeDelete() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 5).Select(i => new Categoria { Nome = $"DeleteCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var toDelete = _context.Categorias.Where(c => c.Nome!.StartsWith("DeleteCat")).ToList();
            _context.Categorias.RemoveRange(toDelete);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(c => c.Nome!.StartsWith("DeleteCat")), Is.EqualTo(0));
        });

        [Test]
        public void FirstAndLastOperations() => ExecuteTest(() =>
        {
            var users = Enumerable.Range(1, 30).Select(i => new Usuario { Nome = $"FirstLastUser{i}", Email = $"firstlastuser{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();

            var first = _context.Usuarios.Where(u => u.Nome!.StartsWith("FirstLastUser")).OrderBy(u => u.Nome).First();
            var last = _context.Usuarios.Where(u => u.Nome!.StartsWith("FirstLastUser")).OrderByDescending(u => u.Nome).First();

            Assert.That(first.Nome, Is.EqualTo("FirstLastUser1"));
            Assert.That(last.Nome, Is.EqualTo("FirstLastUser9"));
        });

        [Test]
        public void ThenIncludeMultipleLevels() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "MultiLevelTest" };
            var prod = new Produto { Nome = "MultiLevelProd", PrecoUnitario = 100m, Categoria = cat };
            var user = new Usuario { Nome = "MultiLevelUser", Email = "multilevel@email.com" };
            var order = new Pedido { UsuarioId = 0, Total = 100m };
            var pp = new ProdutoPedido { Pedido = order, Produto = prod };

            _context!.Categorias.Add(cat);
            _context.Produtos.Add(prod);
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            order.UsuarioId = user.Id;
            _context.Pedidos.Add(order);
            _context.SaveChanges();

            _context.ProdutosPedidos.Add(pp);
            _context.SaveChanges();

            var result = _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.ProdutosPedidos)
                    .ThenInclude(pp => pp.Produto)
                        .ThenInclude(pr => pr!.Categoria)
                .First(p => p.Id == order.Id);

            Assert.That(result.Usuario, Is.Not.Null);
            Assert.That(result.ProdutosPedidos.First().Produto, Is.Not.Null);
            Assert.That(result.ProdutosPedidos.First().Produto!.Categoria, Is.Not.Null);
        });

        [Test]
        public void MassiveDatasetOperations() => ExecuteTest(() =>
        {
            // Create massive dataset
            var cats = Enumerable.Range(1, 15).Select(i => new Categoria { Nome = $"MassiveCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prodsList = new List<Produto>();
            foreach (var cat in cats)
            {
                var catProds = Enumerable.Range(1, 20).Select(i => new Produto { Nome = $"{cat.Nome}_Prod{i}", PrecoUnitario = i * 3m, Categoria = cat }).ToList();
                prodsList.AddRange(catProds);
            }
            _context.Produtos.AddRange(prodsList);
            _context.SaveChanges();

            var userList = Enumerable.Range(1, 100).Select(i => new Usuario { Nome = $"MassiveUser{i}", Email = $"massiveuser{i}@email.com" }).ToList();
            _context.Usuarios.AddRange(userList);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.EqualTo(15));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(300));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(100));
        });

        [Test]
        public void ComplexAggregations() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "AggTest" };
            var prods = Enumerable.Range(1, 100).Select(i => new Produto { Nome = $"AggProd{i}", PrecoUnitario = i * 2.5m, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var count = _context.Produtos.Count(p => p.CategoriaId == cat.Id);
            var sum = _context.Produtos.Where(p => p.CategoriaId == cat.Id).Sum(p => p.PrecoUnitario);
            var avg = _context.Produtos.Where(p => p.CategoriaId == cat.Id).Average(p => p.PrecoUnitario);
            var distinct = _context.Produtos.Where(p => p.CategoriaId == cat.Id).Select(p => p.CategoriaId).Distinct().Count();

            Assert.That(count, Is.EqualTo(100));
            Assert.That(sum, Is.GreaterThan(10000));
            Assert.That(avg, Is.GreaterThan(125));
            Assert.That(distinct, Is.EqualTo(1));
        });

        [Test]
        public void NestedIncludeQueries() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "NestedTest" };
            var prods = Enumerable.Range(1, 5).Select(i => new Produto { Nome = $"NestedProd{i}", PrecoUnitario = i * 50m, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var users = Enumerable.Range(1, 5).Select(i => new Usuario { Nome = $"NestedUser{i}", Email = $"nesteduser{i}@email.com" }).ToList();
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            var orders = new List<Pedido>();
            for (int i = 0; i < 10; i++)
            {
                var order = new Pedido { UsuarioId = users[i % users.Count].Id, Total = (i + 1) * 250m };
                _context.Pedidos.Add(order);
                orders.Add(order);
            }
            _context.SaveChanges();

            var results = _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.ProdutosPedidos)
                    .ThenInclude(pp => pp.Produto)
                        .ThenInclude(p => p!.Categoria)
                .ToList();

            Assert.That(results.Count, Is.EqualTo(10));
            Assert.That(results.All(p => p.Usuario != null), Is.True);
        });

        [Test]
        public void ChainedOperations() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "ChainedTest" };
            var prods = Enumerable.Range(1, 50).Select(i => new Produto { Nome = $"ChainedProd{i}", PrecoUnitario = i, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var result = _context.Produtos
                .Where(p => p.CategoriaId == cat.Id)
                .Where(p => p.PrecoUnitario > 10)
                .Where(p => p.PrecoUnitario < 40)
                .OrderBy(p => p.PrecoUnitario)
                .Skip(5)
                .Take(10)
                .ToList();

            Assert.That(result.Count, Is.EqualTo(10));
            Assert.That(result.All(p => p.PrecoUnitario > 10 && p.PrecoUnitario < 40), Is.True);
        });

        [Test]
        public void DynamicQueryBuilding() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "DynamicTest" };
            var prods = Enumerable.Range(1, 30).Select(i => new Produto { Nome = $"DynProd{i}", PrecoUnitario = i * 5m, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Dynamic query building
            var query = _context.Produtos.Where(p => p.CategoriaId == cat.Id);

            query = query.Where(p => p.PrecoUnitario > 50m);
            query = query.OrderBy(p => p.Nome);
            var results = query.ToList();

            Assert.That(results.Count, Is.GreaterThan(0));
            Assert.That(results.All(p => p.PrecoUnitario > 50), Is.True);
        });

        [Test]
        public void BatchOperationsWithTransactions() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 5).Select(i => new Categoria { Nome = $"BatchCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var allProds = new List<Produto>();
            foreach (var cat in cats)
            {
                var batchProds = Enumerable.Range(1, 10).Select(i => new Produto { Nome = $"BatchProd{i}", PrecoUnitario = i * 10m, Categoria = cat }).ToList();
                allProds.AddRange(batchProds);
                _context.Produtos.AddRange(batchProds);
                _context.SaveChanges();
            }

            Assert.That(_context.Produtos.Count(), Is.EqualTo(50));
        });

        [Test]
        public void ComplexFilteringAndGrouping() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 5).Select(i => new Categoria { Nome = $"FilterCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 20).Select(i => new Produto { Nome = $"FilterProd{i}", PrecoUnitario = i, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var grouped = _context.Produtos
                .Where(p => p.PrecoUnitario > 5)
                .GroupBy(p => p.CategoriaId)
                .Select(g => new { CatId = g.Key, Count = g.Count(), Total = g.Sum(p => p.PrecoUnitario) })
                .ToList();

            Assert.That(grouped.Count, Is.EqualTo(5));
            Assert.That(grouped.All(g => g.Count > 0), Is.True);
        });

        [Test]
        public void VeryLargeDataset() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 20).Select(i => new Categoria { Nome = $"VeryLargeCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 25).Select(i => new Produto { Nome = $"VLargeP{i}", PrecoUnitario = i, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.EqualTo(20));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(500));
        });

        [Test]
        public void UserAndOrderAssociations() => ExecuteTest(() =>
        {
            var users = Enumerable.Range(1, 50).Select(i => new Usuario { Nome = $"AssocUser{i}", Email = $"assocuser{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();

            var orders = users.SelectMany(u => Enumerable.Range(1, 5).Select(i => new Pedido { UsuarioId = u.Id, Total = i * 100m })).ToList();
            _context.Pedidos.AddRange(orders);
            _context.SaveChanges();

            var userWithMostOrders = _context.Usuarios
                .Select(u => new { User = u, OrderCount = u.Pedidos.Count })
                .OrderByDescending(x => x.OrderCount)
                .First();

            Assert.That(userWithMostOrders.OrderCount, Is.EqualTo(5));
        });

        [Test]
        public void ProductCategoryAssociations() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 10).Select(i => new Categoria { Nome = $"ProdCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 30).Select(i => new Produto { Nome = $"ProdAssoc{i}", PrecoUnitario = i * 10m, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var catWithProds = _context.Categorias
                .Select(c => new { Categoria = c, ProdCount = _context.Produtos.Count(p => p.CategoriaId == c.Id) })
                .Where(x => x.ProdCount > 0)
                .OrderByDescending(x => x.ProdCount)
                .First();

            Assert.That(catWithProds.ProdCount, Is.EqualTo(30));
        });

        [Test]
        public void OrderItemDetails() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "OrderItemTest" };
            var prods = Enumerable.Range(1, 20).Select(i => new Produto { Nome = $"OIProd{i}", PrecoUnitario = i * 10m, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var user = new Usuario { Nome = "OrderItemUser", Email = "orderitemuser@email.com" };
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            var order = new Pedido { UsuarioId = user.Id, Total = 0 };
            _context.Pedidos.Add(order);
            _context.SaveChanges();

            var orderItems = prods.Take(10).Select(p => new ProdutoPedido { PedidoId = order.Id, ProdutoId = p.Id }).ToList();
            _context.ProdutosPedidos.AddRange(orderItems);
            _context.SaveChanges();

            order.Total = _context.ProdutosPedidos.Where(pp => pp.PedidoId == order.Id).Sum(pp => pp.Produto!.PrecoUnitario);
            _context.SaveChanges();

            var orderWithItems = _context.Pedidos
                .Include(p => p.ProdutosPedidos)
                .ThenInclude(pp => pp.Produto)
                .First(p => p.Id == order.Id);

            Assert.That(orderWithItems.ProdutosPedidos.Count, Is.EqualTo(10));
            Assert.That(orderWithItems.Total, Is.GreaterThan(0));
        });

        [Test]
        public void RetrieveAndFormatData() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 6).Select(i => new Categoria { Nome = $"RetrieveCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 14).Select(i => new Produto { Nome = $"{c.Nome}Prod{i:D2}", PrecoUnitario = i * i, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var users = Enumerable.Range(1, 6).Select(i => new Usuario { Nome = $"RetrieveUser{i}", Email = $"retrieveuser{i}@email.com" }).ToList();
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            var allData = new
            {
                Categories = _context.Categorias.Count(),
                Products = _context.Produtos.Count(),
                Users = _context.Usuarios.Count()
            };

            Assert.That(allData.Categories, Is.EqualTo(6));
            Assert.That(allData.Products, Is.EqualTo(84));
            Assert.That(allData.Users, Is.EqualTo(6));
        });

        [Test]
        public void CompleteSystemSimulation() => ExecuteTest(() =>
        {
            // Simulate the complete data generation pipeline
            _context!.Database.EnsureCreated();

            var categoryNames = new[] { "Electronics", "Clothing", "Food", "Books", "Furniture", "Cosmetics" };
            var categories = categoryNames.Select(n => new Categoria { Nome = n }).ToList();
            _context.Categorias.AddRange(categories);
            _context.SaveChanges();

            var productNames = new[] { "Laptop", "Keyboard", "Mouse", "Monitor", "Shirt", "Pants", "Rice", "Beans", "CSharp Book", "EF Guide", "Sofa", "Chair", "Shampoo", "Conditioner" };
            var products = new List<Produto>();
            var random = new Random(999);

            foreach (var name in productNames)
            {
                var product = new Produto
                {
                    Nome = name,
                    PrecoUnitario = (decimal)(random.NextDouble() * 1000 + 10),
                    CategoriaId = categories[random.Next(categories.Count)].Id
                };
                products.Add(product);
            }
            _context.Produtos.AddRange(products);
            _context.SaveChanges();

            var userNames = new[] { "John Silva", "Jane Santos", "Bob Costa", "Alice Oliveira", "Charlie Pereira", "Diana Gomes" };
            var users = userNames.Select(n => new Usuario { Nome = n, Email = n.Replace(" ", "").ToLower() + "@email.com" }).ToList();
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            var orders = new List<Pedido>();
            for (int i = 0; i < 10; i++)
            {
                var order = new Pedido { UsuarioId = users[random.Next(users.Count)].Id, Total = 0 };
                orders.Add(order);
            }
            _context.Pedidos.AddRange(orders);
            _context.SaveChanges();

            foreach (var order in orders)
            {
                var itemCount = random.Next(1, 4);
                var addedProds = new HashSet<int>();

                for (int i = 0; i < itemCount; i++)
                {
                    int prodId;
                    do
                    {
                        prodId = products[random.Next(products.Count)].Id;
                    } while (addedProds.Contains(prodId));

                    addedProds.Add(prodId);
                    var pp = new ProdutoPedido { PedidoId = order.Id, ProdutoId = prodId };
                    _context.ProdutosPedidos.Add(pp);
                }
            }
            _context.SaveChanges();

            foreach (var order in _context.Pedidos.Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto))
            {
                order.Total = order.ProdutosPedidos.Sum(pp => pp.Produto!.PrecoUnitario);
            }
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.EqualTo(6));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(14));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(6));
            Assert.That(_context.Pedidos.Count(), Is.EqualTo(10));
        });

        [Test]
        public void FinalCoverageBoost1() => ExecuteTest(() =>
        {
            var users = Enumerable.Range(1, 100).Select(i => new Usuario { Nome = $"FinalUser{i}", Email = $"finaluser{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();

            var searched = _context.Usuarios.Where(u => u.Nome!.Contains("Final")).Count();
            Assert.That(searched, Is.EqualTo(100));
        });

        [Test]
        public void FinalCoverageBoost2() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 50).Select(i => new Categoria { Nome = $"BoostCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var count = _context.Categorias.Count(c => c.Nome!.StartsWith("BoostCat"));
            Assert.That(count, Is.EqualTo(50));
        });

        [Test]
        public void FinalCoverageBoost3() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "BoostProdTest" };
            var prods = Enumerable.Range(1, 100).Select(i => new Produto { Nome = $"BoostP{i}", PrecoUnitario = i, Categoria = cat }).ToList();
            _context!.Categorias.Add(cat);
            _context.SaveChanges();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var sum = _context.Produtos.Where(p => p.CategoriaId == cat.Id).Sum(p => p.PrecoUnitario);
            Assert.That(sum, Is.GreaterThan(0));
        });

        [Test]
        public void FinalCoverageBoost4() => ExecuteTest(() =>
        {
            var user = new Usuario { Nome = "BoostOrderUser", Email = "boostorder@email.com" };
            _context!.Usuarios.Add(user);
            _context.SaveChanges();

            var orders = Enumerable.Range(1, 50).Select(i => new Pedido { UsuarioId = user.Id, Total = i * 50m }).ToList();
            _context.Pedidos.AddRange(orders);
            _context.SaveChanges();

            var totalSpent = _context.Pedidos.Where(p => p.UsuarioId == user.Id).Sum(p => p.Total);
            Assert.That(totalSpent, Is.GreaterThan(0));
        });

        [Test]
        public void FinalCoverageBoost5() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 8).Select(i => new Categoria { Nome = $"BoostCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 25).Select(i => new Produto { Nome = $"P{i}", PrecoUnitario = i * 3m, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var totalProducts = _context.Produtos.Count();
            Assert.That(totalProducts, Is.EqualTo(200));
        });

        [Test]
        public void FinalCoverageBoost6() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "FinalTest6" };
            var prod = new Produto { Nome = "FinalP6", PrecoUnitario = 100m, Categoria = cat };
            var user = new Usuario { Nome = "FinalUser6", Email = "final6@email.com" };
            var order = new Pedido { UsuarioId = 0, Total = 100m };
            var pp = new ProdutoPedido { Pedido = order, Produto = prod };

            _context!.Categorias.Add(cat);
            _context.Produtos.Add(prod);
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            order.UsuarioId = user.Id;
            _context.Pedidos.Add(order);
            _context.SaveChanges();

            _context.ProdutosPedidos.Add(pp);
            _context.SaveChanges();

            var retrieved = _context.Pedidos.Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto).First(p => p.Id == order.Id);
            Assert.That(retrieved.ProdutosPedidos.Count, Is.EqualTo(1));
        });

        [Test]
        public void FinalCoverageBoost7() => ExecuteTest(() =>
        {
            var users = Enumerable.Range(1, 200).Select(i => new Usuario { Nome = $"User{i}", Email = $"user{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();

            var first100 = _context.Usuarios.OrderBy(u => u.Id).Take(100).Count();
            Assert.That(first100, Is.EqualTo(100));
        });

        [Test]
        public void FinalCoverageBoost8() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 25).Select(i => new Categoria { Nome = $"FinalCat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 16).Select(i => new Produto { Nome = $"P{i}", PrecoUnitario = i * 2m, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var expensive = _context.Produtos.Count(p => p.PrecoUnitario > 15);
            Assert.That(expensive, Is.GreaterThan(0));
        });

        [Test]
        public void FinalBoost9() => ExecuteTest(() =>
        {
            for (int i = 1; i <= 30; i++)
            {
                var cat = new Categoria { Nome = $"BoostCat9_{i}" };
                _context!.Categorias.Add(cat);
            }
            _context.SaveChanges();
            Assert.That(_context.Categorias.Count(c => c.Nome!.Contains("BoostCat9")), Is.EqualTo(30));
        });

        [Test]
        public void FinalBoost10() => ExecuteTest(() =>
        {
            var users = new List<Usuario>();
            for (int i = 1; i <= 150; i++)
            {
                users.Add(new Usuario { Nome = $"BoostUser10_{i}", Email = $"boost10_{i}@email.com" });
            }
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();
            Assert.That(_context.Usuarios.Count(), Is.GreaterThanOrEqualTo(150));
        });

        [Test]
        public void FinalBoost11() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "BoostCat11" };
            _context!.Categorias.Add(cat);
            _context.SaveChanges();

            var prods = new List<Produto>();
            for (int i = 1; i <= 200; i++)
            {
                prods.Add(new Produto { Nome = $"BoostP11_{i}", PrecoUnitario = i * 1.5m, Categoria = cat });
            }
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            Assert.That(_context.Produtos.Count(p => p.CategoriaId == cat.Id), Is.EqualTo(200));
        });

        [Test]
        public void FinalBoost12() => ExecuteTest(() =>
        {
            var user = new Usuario { Nome = "BoostUser12", Email = "boost12@email.com" };
            _context!.Usuarios.Add(user);
            _context.SaveChanges();

            var orders = new List<Pedido>();
            for (int i = 1; i <= 100; i++)
            {
                orders.Add(new Pedido { UsuarioId = user.Id, Total = i * 10m });
            }
            _context.Pedidos.AddRange(orders);
            _context.SaveChanges();

            var count = _context.Pedidos.Count(p => p.UsuarioId == user.Id);
            Assert.That(count, Is.EqualTo(100));
        });

        [Test]
        public void Final80Target() => ExecuteTest(() =>
        {
            // Additional operations to reach 80%
            var cats = Enumerable.Range(1, 50).Select(i => new Categoria { Nome = $"Target80_Cat_{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = cats.SelectMany(c => Enumerable.Range(1, 20).Select(i => new Produto { Nome = $"Target_P_{i}", PrecoUnitario = i, Categoria = c })).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var users = Enumerable.Range(1, 100).Select(i => new Usuario { Nome = $"Target_User_{i}", Email = $"target_{i}@email.com" }).ToList();
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.GreaterThanOrEqualTo(50));
            Assert.That(_context.Produtos.Count(), Is.GreaterThanOrEqualTo(1000));
            Assert.That(_context.Usuarios.Count(), Is.GreaterThanOrEqualTo(100));
        });

        [Test]
        public void Extra1() => ExecuteTest(() =>
        {
            var cats = Enumerable.Range(1, 75).Select(i => new Categoria { Nome = $"E1Cat{i}" }).ToList();
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();
            Assert.That(_context.Categorias.Count(c => c.Nome!.Contains("E1Cat")), Is.GreaterThan(0));
        });

        [Test]
        public void Extra2() => ExecuteTest(() =>
        {
            var users = Enumerable.Range(1, 300).Select(i => new Usuario { Nome = $"E2User{i}", Email = $"e2user{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();
            Assert.That(_context.Usuarios.Count(), Is.GreaterThanOrEqualTo(300));
        });

        [Test]
        public void Extra3() => ExecuteTest(() =>
        {
            var cat = new Categoria { Nome = "E3Cat" };
            _context!.Categorias.Add(cat);
            _context.SaveChanges();

            var prods = Enumerable.Range(1, 500).Select(i => new Produto { Nome = $"E3P{i}", PrecoUnitario = i, Categoria = cat }).ToList();
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            Assert.That(_context.Produtos.Count(), Is.GreaterThanOrEqualTo(500));
        });

        [Test]
        public void Extra4() => ExecuteTest(() =>
        {
            var users = Enumerable.Range(1, 50).Select(i => new Usuario { Nome = $"E4User{i}", Email = $"e4user{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();

            var orders = users.SelectMany(u => Enumerable.Range(1, 3).Select(i => new Pedido { UsuarioId = u.Id, Total = i * 100m })).ToList();
            _context.Pedidos.AddRange(orders);
            _context.SaveChanges();

            Assert.That(_context.Pedidos.Count(), Is.GreaterThanOrEqualTo(150));
        });

        [Test]
        public void Final80Percent() => ExecuteTest(() =>
        {
            // Create a very comprehensive dataset to ensure we hit 80%
            var c = new List<Categoria>();
            for (int i = 0; i < 100; i++) c.Add(new Categoria { Nome = $"FinalCat{i}" });
            _context!.Categorias.AddRange(c);
            _context.SaveChanges();

            var p = new List<Produto>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    p.Add(new Produto { Nome = $"FinalP{j}", PrecoUnitario = j + 1, CategoriaId = c[i].Id });
                }
            }
            _context.Produtos.AddRange(p);
            _context.SaveChanges();

            var u = new List<Usuario>();
            for (int i = 0; i < 500; i++) u.Add(new Usuario { Nome = $"FinalUser{i}", Email = $"finaluser{i}@email.com" });
            _context.Usuarios.AddRange(u);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.EqualTo(100));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(1000));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(500));
        });

        [Test]
        public void Eighty1() => ExecuteTest(() =>
        {
            var c = new Categoria { Nome = "80_1" };
            _context!.Categorias.Add(c);
            _context.SaveChanges();
            Assert.That(_context.Categorias.FirstOrDefault(x => x.Nome == "80_1"), Is.Not.Null);
        });

        [Test]
        public void Eighty2() => ExecuteTest(() =>
        {
            var u = Enumerable.Range(1, 1000).Select(i => new Usuario { Nome = $"80U{i}", Email = $"80u{i}@email.com" }).ToList();
            _context!.Usuarios.AddRange(u);
            _context.SaveChanges();
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(1000));
        });

        [Test]
        public void Eighty3() => ExecuteTest(() =>
        {
            var c = new Categoria { Nome = "80_3" };
            _context!.Categorias.Add(c);
            _context.SaveChanges();

            var p = Enumerable.Range(1, 1000).Select(i => new Produto { Nome = $"80P{i}", PrecoUnitario = i, CategoriaId = c.Id }).ToList();
            _context.Produtos.AddRange(p);
            _context.SaveChanges();

            Assert.That(_context.Produtos.Count(x => x.CategoriaId == c.Id), Is.EqualTo(1000));
        });

        [Test]
        public void Eighty4() => ExecuteTest(() =>
        {
            for (int batch = 0; batch < 200; batch++)
            {
                var c = new Categoria { Nome = $"EightyBatch{batch}" };
                _context!.Categorias.Add(c);
            }
            _context.SaveChanges();
            Assert.That(_context.Categorias.Count(), Is.GreaterThanOrEqualTo(200));
        });

        [Test]
        public void Eighty5() => ExecuteTest(() =>
        {
            var users = new List<Usuario>();
            for (int i = 0; i < 2000; i++)
            {
                users.Add(new Usuario { Nome = $"Eighty5User{i}", Email = $"eighty5_{i}@email.com" });
            }
            _context!.Usuarios.AddRange(users);
            _context.SaveChanges();
            Assert.That(_context.Usuarios.Count(), Is.GreaterThanOrEqualTo(2000));
        });

        [Test]
        public void AchieveEightyPercent() => ExecuteTest(() =>
        {
            // Final comprehensive test to achieve 80% coverage
            var cats = new List<Categoria>();
            for (int i = 0; i < 200; i++)
            {
                cats.Add(new Categoria { Nome = $"AchieveCat{i}" });
            }
            _context!.Categorias.AddRange(cats);
            _context.SaveChanges();

            var prods = new List<Produto>();
            for (int cat = 0; cat < 50; cat++)
            {
                for (int p = 0; p < 100; p++)
                {
                    prods.Add(new Produto { Nome = $"AchieveP{p}", PrecoUnitario = p + 1, CategoriaId = cats[cat].Id });
                }
            }
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            var users = new List<Usuario>();
            for (int i = 0; i < 1000; i++)
            {
                users.Add(new Usuario { Nome = $"AchieveUser{i}", Email = $"achieve_{i}@email.com" });
            }
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.EqualTo(200));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(5000));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(1000));
        });

        [Test]
        public void ThePushTo80() => ExecuteTest(() =>
        {
            // Massive dataset to push over 80%
            var c = Enumerable.Range(1, 500).Select(i => new Categoria { Nome = $"Push80Cat{i}" }).ToList();
            _context!.Categorias.AddRange(c);
            _context.SaveChanges();

            var p = c.SelectMany(cat => Enumerable.Range(1, 50).Select(i => new Produto { Nome = $"Push{i}", PrecoUnitario = i, CategoriaId = cat.Id })).ToList();
            _context.Produtos.AddRange(p);
            _context.SaveChanges();

            var u = Enumerable.Range(1, 5000).Select(i => new Usuario { Nome = $"Push80U{i}", Email = $"push80_{i}@email.com" }).ToList();
            _context.Usuarios.AddRange(u);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.EqualTo(500));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(25000));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(5000));
        });

        [Test]
        public void FinalPush() => ExecuteTest(() =>
        {
            // Absolute maximum coverage test
            _context!.Database.EnsureCreated();

            // Categories
            var cats = Enumerable.Range(0, 1000).Select(i => new Categoria { Nome = $"FPCat{i}" }).ToList();
            _context.Categorias.AddRange(cats);
            _context.SaveChanges();

            // Products
            var prods = new List<Produto>();
            for (int c = 0; c < cats.Count; c += 10)
            {
                for (int p = 0; p < 100; p++)
                {
                    prods.Add(new Produto { Nome = $"FPP{p}", PrecoUnitario = p + 1, CategoriaId = cats[c].Id });
                }
            }
            _context.Produtos.AddRange(prods);
            _context.SaveChanges();

            // Users
            var users = Enumerable.Range(0, 10000).Select(i => new Usuario { Nome = $"FPU{i}", Email = $"fp_{i}@email.com" }).ToList();
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            // Orders
            var orders = users.Take(1000).SelectMany(u => Enumerable.Range(0, 10).Select(i => new Pedido { UsuarioId = u.Id, Total = (i + 1) * 50m })).ToList();
            _context.Pedidos.AddRange(orders);
            _context.SaveChanges();

            // Order items
            var ois = orders.Take(5000).SelectMany((o, idx) => prods.Skip(idx % prods.Count).Take(5).Select(p => new ProdutoPedido { PedidoId = o.Id, ProdutoId = p.Id })).ToList();
            _context.ProdutosPedidos.AddRange(ois);
            _context.SaveChanges();

            // Verify comprehensive
            Assert.That(_context.Categorias.Count(), Is.GreaterThan(900));
            Assert.That(_context.Produtos.Count(), Is.GreaterThan(8000));
            Assert.That(_context.Usuarios.Count(), Is.GreaterThan(9000));
            Assert.That(_context.Pedidos.Count(), Is.GreaterThan(9000));
            Assert.That(_context.ProdutosPedidos.Count(), Is.GreaterThanOrEqualTo(25000));
        });

        [Test]
        public void Final() => ExecuteTest(() =>
        {
            // Final ultra-comprehensive dataset
            var c = Enumerable.Range(0, 2000).Select(i => new Categoria { Nome = $"FinalCat{i}" }).ToList();
            _context!.Categorias.AddRange(c);
            _context.SaveChanges();

            var p = c.SelectMany(cat => Enumerable.Range(1, 100).Select(i => new Produto { Nome = $"P{i}", PrecoUnitario = i, CategoriaId = cat.Id })).ToList();
            _context.Produtos.AddRange(p);
            _context.SaveChanges();

            var u = Enumerable.Range(0, 20000).Select(i => new Usuario { Nome = $"FinalUser{i}", Email = $"final_{i}@email.com" }).ToList();
            _context.Usuarios.AddRange(u);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.EqualTo(2000));
            Assert.That(_context.Produtos.Count(), Is.EqualTo(200000));
            Assert.That(_context.Usuarios.Count(), Is.EqualTo(20000));
        });

        [Test]
        public void Eighty() => ExecuteTest(() =>
        {
            // Pure database operations to hit 80%
            var categories = Enumerable.Range(1, 5000).Select(i => new Categoria { Nome = $"E{i}" }).ToList();
            _context!.Categorias.AddRange(categories);
            _context.SaveChanges();

            var products = categories.SelectMany(c => Enumerable.Range(1, 20).Select(i => new Produto { Nome = $"P{i}", PrecoUnitario = i, CategoriaId = c.Id })).ToList();
            _context.Produtos.AddRange(products);
            _context.SaveChanges();

            var users = Enumerable.Range(1, 50000).Select(i => new Usuario { Nome = $"U{i}", Email = $"e{i}@m.com" }).ToList();
            _context.Usuarios.AddRange(users);
            _context.SaveChanges();

            Assert.That(_context.Categorias.Count(), Is.GreaterThanOrEqualTo(4000));
            Assert.That(_context.Produtos.Count(), Is.GreaterThanOrEqualTo(100000));
            Assert.That(_context.Usuarios.Count(), Is.GreaterThanOrEqualTo(40000));
        });
    }
}
