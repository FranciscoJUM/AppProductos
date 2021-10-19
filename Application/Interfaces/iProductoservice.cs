using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Interfaces
{
   public interface iProductoservice : Iservice<Producto>
    {

        decimal UEPS(decimal salida);
        decimal PEPS(decimal salida);
        Producto PP(Producto p, decimal VTT, int unidades);
        Producto PPsalida(Producto p, int unidades);
    }
}
