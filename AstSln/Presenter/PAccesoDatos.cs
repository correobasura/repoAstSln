using IView;
using Model.DataContextModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constantes;

namespace Presenter
{
    public class PAccesoDatos
    {
        private IAccesoDatos _iAccesoDatos;
        private AstEntities _astEntities;
        private ASTR _resultActualSol;
        private ASTR _resultActualLun;

        public PAccesoDatos(IAccesoDatos iAccesoDatos)
        {
            this._astEntities = new AstEntities();
            this._iAccesoDatos = iAccesoDatos;
        }

        /// <summary>
        /// Método que obtiene la lista de resultados de acuerdo a los parámetros ingresados
        /// </summary>
        public void ObtenerResultados(int pagina = 1, int cantidad = 0)
        {
            using (var context = new AstEntities())
            {
                var objectContex = ((IObjectContextAdapter)context).ObjectContext;
                //Si no se pagina la lista, se obtienen todos los resultados, de lo contrario, se traen los resultados solicitados
                if (cantidad > 0)
                {
                    var datos = objectContex.CreateObjectSet<ASTR>().OrderByDescending(x => x.FECHA).AsEnumerable();
                }
                else
                {
                    var datos = objectContex.CreateObjectSet<ASTR>().OrderByDescending(x => x.FECHA).Skip((pagina-1)*cantidad).Take(cantidad).AsEnumerable();
                }
            }
        }

        /// <summary>
        /// Método que obtiene y asigna los datos de los últimos resultados ingresados
        /// </summary>
        public void ObtenerUltimoResultado()
        {
            var ultimoSort = (from x in _astEntities.ASTR
                              select x.FECHA).Max();
            _resultActualSol = _astEntities.ASTR.Where(x => x.FECHA == ultimoSort && x.TIPO == ConstantesTipoSor.TIPO_SOL).AsEnumerable().FirstOrDefault();
            _resultActualLun = _astEntities.ASTR.Where(x => x.FECHA == ultimoSort && x.TIPO == ConstantesTipoSor.TIPO_LUN).AsEnumerable().FirstOrDefault();
        }
    }
}
