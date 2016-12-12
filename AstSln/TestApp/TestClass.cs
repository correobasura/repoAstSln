using IView;
using Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class TestClass : IAccesoDatos
    {
        private PAccesoDatos _pAccesoDatos;

        public TestClass()
        {
            //for (int i = -1000; i < 0; i++)
            //{

            //    _pAccesoDatos = new PAccesoDatos(this, DateTime.Today.AddDays(i));
            //    _pAccesoDatos.ObtenerResultados();
            //}
            //_pAccesoDatos = new PAccesoDatos(this, DateTime.Today.AddDays(-10));
            //_pAccesoDatos.ObtenerResultados();
            //_pAccesoDatos = new PAccesoDatos(this, DateTime.Today.AddDays(-6));
            ////_pAccesoDatos.ObtenerResultados();
            //_pAccesoDatos = new PAccesoDatos(this, DateTime.Today.AddDays(-1));
            //_pAccesoDatos.ObtenerResultados();
            _pAccesoDatos = new PAccesoDatos(this, DateTime.Today);
            _pAccesoDatos.ObtenerResultados();
        }
    }
}
