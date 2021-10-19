using AppCore.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Services
{
    public   class Productoservice : iProductoservice
    {
        iProductoservice productoModel;


















        public decimal UEPS(decimal salida)
        {
            return productoModel.UEPS(salida);
        }

        public decimal PEPS(decimal salida)
        {
            return productoModel.PEPS(salida);
        }

        public Producto PP(Producto p, decimal VTT, int unidades)
        {
            return productoModel.PP(p, VTT, unidades);
        }

        public Producto PPsalida(Producto p, int unidades)
        {
            return productoModel.PPsalida(p, unidades);
        }

        public void Create(Producto t)
        {
            productoModel.Create(t);
        }

        public int Update(Producto t)
        {
            return productoModel.Update(t);
        }

        public bool Delete(Producto t)
        {
            return productoModel.Delete(t);
        }

        public Producto[] FindAll()
        {
            return productoModel.FindAll();
        }
    }
}
