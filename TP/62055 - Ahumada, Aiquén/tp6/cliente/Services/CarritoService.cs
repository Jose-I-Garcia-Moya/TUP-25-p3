using cliente.Models;

namespace cliente.Services;

public class ItemCarrito
{
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }
}

public class CarritoService
{
    public List<ItemCarrito> Items { get; private set; } = new();

    public event Action OnChange;

    public void Agregar(Producto producto)
    {
        var item = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
        int cantidadEnCarrito = item?.Cantidad ?? 0;
        if (cantidadEnCarrito < producto.Stock + cantidadEnCarrito)
        {
            if (item != null)
            {
                item.Cantidad++;
            }
            else
            {
                
                var productoCopia = new Producto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    ImagenUrl = producto.ImagenUrl,
                    Stock = producto.Stock + cantidadEnCarrito
                };
                Items.Add(new ItemCarrito { Producto = productoCopia, Cantidad = 1 });
            }
            OnChange?.Invoke();
        }
    }

    public void Quitar(Producto producto)
    {
        var item = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
        if (item != null)
        {
            item.Cantidad--;
            if (item.Cantidad <= 0)
                Items.Remove(item);
            OnChange?.Invoke();
        }
    }

    public void Eliminar(Producto producto)
    {
        var item = Items.FirstOrDefault(i => i.Producto.Id == producto.Id);
        if (item != null)
        {
            Items.Remove(item);
            OnChange?.Invoke();
        }
    }

    public void Vaciar()
    {
        Items.Clear();
        OnChange?.Invoke();
    }

    public decimal Total() => Items.Sum(i => i.Producto.Precio * i.Cantidad);
    public int TotalItems() => Items.Sum(i => i.Cantidad);
}