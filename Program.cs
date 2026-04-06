using Microsoft.EntityFrameworkCore;

using var context = new AppDbContext();

// Criar banco de dados se não existir
context.Database.EnsureCreated();

// Limpar dados existentes para começar do zero
context.ProdutosPedidos.RemoveRange(context.ProdutosPedidos);
context.Pedidos.RemoveRange(context.Pedidos);
context.Produtos.RemoveRange(context.Produtos);
context.Categorias.RemoveRange(context.Categorias);
context.Usuarios.RemoveRange(context.Usuarios);
context.SaveChanges();

var random = new Random();

// ========== GERAR CATEGORIAS ==========
Console.WriteLine("\n📦 GERANDO CATEGORIAS...\n");
var categoriasNomes = new[] { "Eletrônicos", "Roupas", "Alimentos", "Livros", "Móveis", "Cosméticos" };
var categorias = new List<Categoria>();

foreach (var nome in categoriasNomes)
{
    var categoria = new Categoria { Nome = nome };
    context.Categorias.Add(categoria);
    categorias.Add(categoria);
}
context.SaveChanges();

Console.WriteLine("✅ Categorias criadas com sucesso!\n");

// ========== GERAR PRODUTOS ==========
Console.WriteLine("📦 GERANDO PRODUTOS...\n");
var produtoNomes = new[] { "Notebook", "Teclado", "Mouse", "Monitor", "Camiseta", "Calça", "Arroz", "Feijão",
                           "Livro de C#", "Guia de Entity Framework", "Sofá", "Cadeira", "Shampoo", "Condicionador" };
var produtos = new List<Produto>();

foreach (var nome in produtoNomes)
{
    var produto = new Produto
    {
        Nome = nome,
        PrecoUnitario = (decimal)(random.NextDouble() * 1000 + 10),
        CategoriaId = categorias[random.Next(categorias.Count)].Id
    };
    context.Produtos.Add(produto);
    produtos.Add(produto);
}
context.SaveChanges();

Console.WriteLine("✅ Produtos criados com sucesso!\n");

// ========== GERAR USUÁRIOS ==========
Console.WriteLine("👥 GERANDO USUÁRIOS...\n");
var usuarioNomes = new[] { "João Silva", "Maria Santos", "Pedro Costa", "Ana Oliveira", "Carlos Pereira", "Julia Gomes" };
var usuarioEmails = new[] { "joao@email.com", "maria@email.com", "pedro@email.com", "ana@email.com", "carlos@email.com", "julia@email.com" };
var usuarios = new List<Usuario>();

for (int i = 0; i < usuarioNomes.Length; i++)
{
    var usuario = new Usuario
    {
        Nome = usuarioNomes[i],
        Email = usuarioEmails[i]
    };
    context.Usuarios.Add(usuario);
    usuarios.Add(usuario);
}
context.SaveChanges();

Console.WriteLine("✅ Usuários criados com sucesso!\n");

// ========== GERAR PEDIDOS ==========
Console.WriteLine("🛒 GERANDO PEDIDOS...\n");
var pedidos = new List<Pedido>();

for (int i = 0; i < 10; i++)
{
    var pedido = new Pedido
    {
        UsuarioId = usuarios[random.Next(usuarios.Count)].Id,
        Total = 0 // Será calculado depois
    };
    context.Pedidos.Add(pedido);
    pedidos.Add(pedido);
}
context.SaveChanges();

Console.WriteLine("✅ Pedidos criados com sucesso!\n");

// ========== GERAR PRODUTO PEDIDO ==========
Console.WriteLine("📋 GERANDO ITENS DE PEDIDO...\n");

foreach (var pedido in pedidos)
{
    int quantidadeItens = random.Next(1, 4); // 1 a 3 itens por pedido
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
        context.ProdutosPedidos.Add(produtoPedido);
    }
}
context.SaveChanges();

// Calcular total dos pedidos
foreach (var pedido in context.Pedidos.Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto))
{
    pedido.Total = pedido.ProdutosPedidos.Sum(pp => pp.Produto.PrecoUnitario);
}
context.SaveChanges();

Console.WriteLine("✅ Itens de pedido criados com sucesso!\n");

// ========== IMPRIMIR TODOS OS DADOS ==========
Console.WriteLine("\n" + new string('=', 80));
Console.WriteLine("📊 RELATÓRIO COMPLETO DO BANCO DE DADOS");
Console.WriteLine(new string('=', 80) + "\n");

// Imprimir Categorias
Console.WriteLine("\n📦 CATEGORIAS:");
Console.WriteLine(new string('-', 80));
foreach (var cat in context.Categorias)
{
    Console.WriteLine($"  ID: {cat.Id} | Nome: {cat.Nome}");
}

// Imprimir Produtos
Console.WriteLine("\n\n📦 PRODUTOS:");
Console.WriteLine(new string('-', 80));
foreach (var prod in context.Produtos.Include(p => p.Categoria))
{
    Console.WriteLine($"  ID: {prod.Id} | Nome: {prod.Nome} | Preço: R$ {prod.PrecoUnitario:F2} | Categoria: {prod.Categoria.Nome}");
}

// Imprimir Usuários
Console.WriteLine("\n\n👥 USUÁRIOS:");
Console.WriteLine(new string('-', 80));
foreach (var usr in context.Usuarios)
{
    Console.WriteLine($"  ID: {usr.Id} | Nome: {usr.Nome} | Email: {usr.Email}");
}

// Imprimir Pedidos
Console.WriteLine("\n\n🛒 PEDIDOS:");
Console.WriteLine(new string('-', 80));
foreach (var ped in context.Pedidos.Include(p => p.Usuario))
{
    Console.WriteLine($"  ID: {ped.Id} | Usuário: {ped.Usuario.Nome} | Total: R$ {ped.Total:F2}");
}

// Imprimir Produtos do Pedido
Console.WriteLine("\n\n📋 ITENS DOS PEDIDOS:");
Console.WriteLine(new string('-', 80));
foreach (var ped in context.Pedidos.Include(p => p.ProdutosPedidos).ThenInclude(pp => pp.Produto))
{
    Console.WriteLine($"\n  Pedido #{ped.Id}:");
    foreach (var item in ped.ProdutosPedidos)
    {
        Console.WriteLine($"    └─ {item.Produto.Nome} (R$ {item.Produto.PrecoUnitario:F2})");
    }
}

Console.WriteLine("\n\n" + new string('=', 80));
Console.WriteLine("✅ DADOS GERADOS COM SUCESSO!");
Console.WriteLine(new string('=', 80) + "\n");  