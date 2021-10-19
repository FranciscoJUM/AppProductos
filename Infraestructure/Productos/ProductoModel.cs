using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Productos
{
    public class ProductoModel : IProductoModel
    {
        private Producto[] productos;

        #region CRUD
        public void Add(Producto p)
        {
            Add(p, ref productos);
        }

        public int Update(Producto p)
        {
            if(p == null)
            {
                throw new ArgumentException("El producto no puede ser null.");
            }

            int index = GetIndexById(p.Id);
            if(index < 0)
            {
                throw new Exception($"El producto con id {p.Id} no se encuentra.");
            }

            productos[index] = p;
            return index;
        }

        public bool Delete(Producto p)
        {
            if (p == null)
            {
                throw new ArgumentException("El producto no puede ser null.");
            }

            int index = GetIndexById(p.Id);
            if (index < 0)
            {
                throw new Exception($"El producto con id {p.Id} no se encuentra.");
            }

            if(index != productos.Length - 1)
            {
                productos[index] = productos[productos.Length - 1];
            }

            Producto[] tmp = new Producto[productos.Length - 1];
            Array.Copy(productos, tmp, tmp.Length);
            productos = tmp;

            return productos.Length == tmp.Length;
        }
        public Producto[] GetAll()
        {
            return productos;
        }

        #endregion

        #region Queries
        public Producto GetProductoById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException($"El Id: {id} no es valido.");
            }

            int index = GetIndexById(id);            

            return index <= 0 ? null : productos[index];
        }

        public Producto[] GetProductosByUnidadMedida(UnidadMedida um)
        {
            Producto[] tmp = null;
            if (productos == null)
            {
                return tmp;
            }

            foreach (Producto p in productos)
            {
                if(p.UnidadMedida == um)
                {
                    Add(p, ref tmp);
                }
            }

            return tmp;
        }
        public Producto[] GetProductosByFechaVencimiento(DateTime dt)
        {
            Producto[] tmp = null;
            if(productos == null)
            {
                return tmp;
            }

            foreach(Producto p in productos)
            {
                if(p.FechaVencimiento.CompareTo(dt) <= 0)
                {
                    Add(p, ref tmp);
                }
            }

            return tmp;
        }
        public Producto[] GetProductosByRangoPrecio(decimal start, decimal end)
        {
            Producto[] tmp = null;
            if(productos == null)
            {
                return tmp;
            }

            foreach(Producto p in productos)
            {
                if(p.Precio >= start && p.Precio <= end)
                {
                    Add(p, ref tmp);
                }
            }

            return tmp;
        }
        public string GetProductosAsJson()
        {
            return JsonConvert.SerializeObject(productos);
        }
        public Producto[] GetProductosOrderByPrecio()
        {
            Array.Sort(productos, new Producto.ProductoOrderByPrecio());
            return productos;
        }
        public int GetLastProductoId()
        {
            return productos == null ? 0 : productos[productos.Length - 1].Id;
        }


       

       
        #endregion

        #region Private Method
        private void Add(Producto p, ref Producto[] pds)
        {
            if(pds == null)
            {
                pds = new Producto[1];
                pds[pds.Length - 1] = p;
                return;
            }

            Producto[] tmp = new Producto[pds.Length + 1];
            Array.Copy(pds, tmp, pds.Length);
            tmp[tmp.Length - 1] = p;
            pds = tmp;
        }

        private int GetIndexById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

            int index = int.MinValue, i = 0;
            if(productos == null)
            {
                return index;
            }

            foreach (Producto p in productos)
            {
                if(p.Id == id)
                {
                    index = i;
                    break;
                }
                i++;
            }

            return index;
        }


        public void Create(Producto t)
        {
            throw new NotImplementedException();
        }

        public Producto[] FindAll()
        {
            throw new NotImplementedException();
        }

        public decimal UEPS(decimal salida)
        {
            if (productos == null)
            { throw new Exception("No hay productos para calcular"); }
            if (salida > productos[0].Existencia)
            { throw new Exception("para evitar problemas divida las compras "); }
            decimal valo = productos[0].Precio;
            Vender(salida);
            decimal valordeinventa = valo * salida;
            decimal totaldeventas = valo;
            return valo;
        }
        public decimal PEPS(decimal salida)
        {
            if (productos == null)
            { throw new Exception("No hay producyos para calcular"); }
            if (salida > productos[0].Existencia)
            { throw new Exception("para evitar problemas divida las compras "); }
            decimal valor = productos[0].Precio;
            Vender(salida);
            decimal valordeinventa = valor * salida;
            decimal totaldeventas = valor;
            return valor;
        }

        public Producto PP(Producto p, decimal VTT, int unidades)
        {
            decimal nuevoVAlorporUnidad = p.VAlorTotalDemercancia / p.Existencia;

            decimal nuevovalortotal = (p.VAlorTotalDemercancia + VTT);


            Producto yx = new Producto()
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Existencia = p.Existencia,
                Precio = p.Precio,
                FechaVencimiento = p.FechaVencimiento,
                UnidadMedida = p.UnidadMedida,
                VAlorTotalDemercancia = nuevovalortotal,
                valoporUnidad = nuevoVAlorporUnidad,



            };

            Create(yx);
            return yx;


        }
        public Producto PPsalida(Producto p, int unidades)
        {
            Producto yx = new Producto()
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Existencia = p.Existencia - unidades,
                Precio = p.Precio,
                FechaVencimiento = p.FechaVencimiento,
                UnidadMedida = p.UnidadMedida,
                VAlorTotalDemercancia = p.VAlorTotalDemercancia,
                valoporUnidad = p.VAlorTotalDemercancia,



            };

            Create(yx);
            return yx;
        }


        private decimal Vender(decimal a)
        {

            while (a > productos[0].Existencia)
            {
                a = a - productos[0].Existencia;

                if (productos[0].Existencia - a == 0)
                {
                    a = 0;
                    return a;
                }
            }
            return a;
        }
        #endregion
    }
}
