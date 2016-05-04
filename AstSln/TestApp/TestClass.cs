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
            _pAccesoDatos = new PAccesoDatos(this);
            _pAccesoDatos.ObtenerResultados();
        }
    }
}
