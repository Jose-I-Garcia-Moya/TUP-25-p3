namespace servidor.Models;

public class RegistrarVentaRequest
{
    public string NombreCliente { get; set; }
    public string ApellidoCliente { get; set; }
    public string EmailCliente { get; set; }
    public List<DetalleVentaRequest> Detalles { get; set; }
}

public class DetalleVentaRequest
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}