using AppCore.Interfaces;
using AppCore.Services;
using Autofac;
using Domain.Interfaces;
using Infraestructure.Empleados;
using Infraestructure.Productos;
using ProductosApp.Formularios;
using System;
using App = System.Windows.Forms.Application;
namespace ProductosApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();



            builder.RegisterType<Productoservice>().As<iProductoservice>();
            builder.RegisterType<ProductoModel>().As<IProductoModel>();
            var container = builder.Build();

            App.EnableVisualStyles();
            App.SetCompatibleTextRenderingDefault(false);
            App.Run(new FrmProductos(container.Resolve<iProductoservice>()));
        }
    }
}
