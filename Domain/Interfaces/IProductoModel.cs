using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IProductoModel : IModel<Producto>
    {
        decimal UEPS(decimal salida);
        decimal PEPS(decimal salida);
        Producto PP(Producto p, decimal VTT, int unidades);
        Producto PPsalida(Producto p, int unidades);


    }
}
