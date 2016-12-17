using IView;
using Presenter;
using System;

namespace TestApp
{
    public class TestClass : IAccesoDatos
    {
        private PAccesoDatos _pAccesoDatos;

        public TestClass()
        {
            //for (int i = -30; i < 0; i++)
            //{
            //    _pAccesoDatos = new PAccesoDatos(this, DateTime.Today.AddDays(i));
            //    _pAccesoDatos.ObtenerResultados();
            //}
            string dateTime = "12/11/2016";
            DateTime dt = Convert.ToDateTime(dateTime);
            for (int i = 0; i < 30; i++)
            {
                _pAccesoDatos = new PAccesoDatos(this, dt.AddDays(i));
                _pAccesoDatos.ObtenerResultados();
            }
            //_pAccesoDatos = new PAccesoDatos(this, DateTime.Today.AddDays(-10));
            //_pAccesoDatos.ObtenerResultados();
            //_pAccesoDatos = new PAccesoDatos(this, DateTime.Today.AddDays(-6));
            ////_pAccesoDatos.ObtenerResultados();
            //_pAccesoDatos = new PAccesoDatos(this, DateTime.Today.AddDays(-1));
            //_pAccesoDatos.ObtenerResultados();
            //_pAccesoDatos = new PAccesoDatos(this, DateTime.Today);
            //_pAccesoDatos.ObtenerResultados();
        }
    }
}