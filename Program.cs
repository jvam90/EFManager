using var context = new AppDbContext();

// Criar banco de dados se não existir
context.Database.EnsureCreated();

var usuarios = context.Usuarios.ToList();

if(usuarios.Count == 0)
{
    Console.WriteLine("Nenhum usuário encontrado!");
    var usuario = new Usuario
    {
        Nome = "João Silva",
        Email = "joao.silva@email.com"        
    };
    context.Usuarios.Add(usuario);
    context.SaveChanges();
    Console.WriteLine("Usuário criado com sucesso!");
}
else
{
    Console.WriteLine("Usuários encontrados:");
    foreach (var usuario in usuarios)
    {
        Console.WriteLine($"ID: {usuario.Id}, Nome: {usuario.Nome}, Email: {usuario.Email}");
    }
}

context.Usuarios.RemoveRange(usuarios);
context.SaveChanges();  