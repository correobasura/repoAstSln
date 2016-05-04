using IView;
using Model.DataContextModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    public class PAccesoDatos
    {
        private IAccesoDatos _iAccesoDatos;
        private AstEntities _astEntities;

        public PAccesoDatos(IAccesoDatos iAccesoDatos)
        {
            this._astEntities = new AstEntities();
            this._iAccesoDatos = iAccesoDatos;
        }

        public void ObtenerResultados()
        {
            using (var context = new AstEntities())
            {
                var objectContex = ((IObjectContextAdapter)context).ObjectContext;
                var datos = objectContex.CreateObjectSet<ASTR>().OrderByDescending(x => x.FECHA).Skip(20).Take(20).ToList();
                int data = datos.Count();
            }
        }
    }
}
