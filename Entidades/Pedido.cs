public class Pedido{
    public int Id { get; set; }    
    public decimal Total { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<ProdutoPedido> ProdutosPedidos { get; set; }
}