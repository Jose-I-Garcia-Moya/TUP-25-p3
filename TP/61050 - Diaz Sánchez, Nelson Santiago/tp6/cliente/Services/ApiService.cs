using System.Net.Http.Json;
using cliente.Models;
using cliente.DTOs;

namespace cliente.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DatosRespuesta> ObtenerDatosAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<DatosRespuesta>("/api/datos");
            return response ?? new DatosRespuesta { Mensaje = "No se recibieron datos del servidor", Fecha = DateTime.Now };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener datos: {ex.Message}");
            return new DatosRespuesta { Mensaje = $"Error: {ex.Message}", Fecha = DateTime.Now };
        }
    }
    public async Task<List<Producto>> ObtenerProductosAsync() {
        try {
            var productos = await _httpClient.GetFromJsonAsync<List<Producto>>("Controladordeproductos");
            return productos ?? new List<Producto>();
        } catch (Exception ex) {
            Console.WriteLine($"Error al obtener productos: {ex.Message}");
            return new List<Producto>();
        }
    }
    public async Task<bool> ReservarProductoAsync(ReservacionDTO reserva)
{
    var response = await _httpClient.PostAsJsonAsync("api/reservas", reserva); 
    return response.IsSuccessStatusCode;
}
    public async Task<bool> ConfirmarCompraAsync(CompraDTO compra)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("api/ControladorCarrito/confirmar", compra);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al confirmar la compra: {ex.Message}");
            return false;
        }
    }
}



public class DatosRespuesta
{
    public string Mensaje { get; set; }
    public DateTime Fecha { get; set; }
}
