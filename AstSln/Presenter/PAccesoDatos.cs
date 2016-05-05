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
        private List<ASTR> listaDatosSol;
        private List<ASTR> listaDatosLun;
        private List<ASTR> listaDatosGeneral;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="iAccesoDatos"></param>
        public PAccesoDatos(IAccesoDatos iAccesoDatos)
        {
            this._astEntities = new AstEntities();
            this._iAccesoDatos = iAccesoDatos;
        }

        /// <summary>
        /// Método que obtiene la lista de resultados de acuerdo a los parámetros ingresados
        /// </summary>
        public void ObtenerResultados()
        {
            using (var context = new AstEntities())
            {
                var objectContex = ((IObjectContextAdapter)context).ObjectContext;
                //Si no se pagina la lista, se obtienen todos los resultados, de lo contrario, se traen los resultados solicitados
                listaDatosGeneral = objectContex.CreateObjectSet<ASTR>().OrderBy(x => x.FECHA).ToList();
                listaDatosSol = listaDatosGeneral.Where(x => x.TIPO == 1).ToList();
                listaDatosLun = listaDatosGeneral.Where(x => x.TIPO == 2).ToList();
            }
            this.ObtenerUltimoResultado();
            this.GetNumerosDespuesDelActual(listaDatosSol, _resultActualSol);
            this.GetNumerosDespuesDelActual(listaDatosLun, _resultActualLun);
        }

        /// <summary>
        /// Método que obtiene y asigna los datos de los últimos resultados ingresados
        /// </summary>
        private void ObtenerUltimoResultado()
        {
            var ultimoSort = (from x in _astEntities.ASTR
                              select x.FECHA).Max();
            _resultActualSol = _astEntities.ASTR.Where(x => x.FECHA == ultimoSort && x.TIPO == ConstantesTipoSor.TIPO_SOL).AsEnumerable().FirstOrDefault();
            _resultActualLun = _astEntities.ASTR.Where(x => x.FECHA == ultimoSort && x.TIPO == ConstantesTipoSor.TIPO_LUN).AsEnumerable().FirstOrDefault();
        }

        private void GetNumerosDespuesDelActual(List<ASTR> listaValidar, ASTR sorComparador)
        {
            int[] contadorPosUno = new int[10];
            int[] contadorPosDos = new int[10];
            int[] contadorPosTres = new int[10];
            int[] contadorPosCuatro = new int[10];
            Dictionary<string, int> dicSign = new Dictionary<string, int>();
            bool flagPosUno = false;
            bool flagPosDos = false;
            bool flagPosTres = false;
            bool flagPosCuatro = false;
            bool flagSign = false;
            foreach (var item in listaValidar)
            {
                if (flagPosUno)
                {
                    contadorPosUno[(int)item.POS_UNO]++;
                }
                if (flagPosDos)
                {
                    contadorPosDos[(int)item.POS_DOS]++;
                }
                if (flagPosTres)
                {
                    contadorPosTres[(int)item.POS_TRES]++;
                }
                if (flagPosCuatro)
                {
                    contadorPosCuatro[(int)item.POS_CUATRO]++;
                }
                //if (flagSign)
                //{
                //    dicSign.it
                //}
                flagPosUno = this.ValidarMismoDato(1, item, sorComparador);
                flagPosDos = this.ValidarMismoDato(2, item, sorComparador);
                flagPosTres = this.ValidarMismoDato(3, item, sorComparador);
                flagPosCuatro = this.ValidarMismoDato(4, item, sorComparador);
            }
        }

        /// <summary>
        /// Método encargado de validar de acuerdo al caso, el resultado de comparar los dos resultados
        /// </summary>
        /// <param name="caso">caso que indica que se debe validar</param>
        /// <param name="sorValidar">Variable que indica el elemento que se quiere validar</param>
        /// <param name="sorComparador">Objeto que referencia al resultado que sirve como comparador</param>
        /// <returns>bFlag: Bandera con resultado de compararación</returns>
        private bool ValidarMismoDato(int caso, ASTR sorValidar, ASTR sorComparador)
        {
            //Bandera que almacena el valor de la comparación
            bool bFlag = false;
            switch (caso)
            {
                case 1:
                    bFlag = sorValidar.POS_UNO.Equals(sorComparador.POS_UNO);
                    break;
                case 2:
                    bFlag = sorValidar.POS_DOS.Equals(sorComparador.POS_DOS);
                    break;
                case 3:
                    bFlag = sorValidar.POS_TRES.Equals(sorComparador.POS_TRES);
                    break;
                case 4:
                    bFlag = sorValidar.POS_CUATRO.Equals(sorComparador.POS_CUATRO);
                    break;
                case 5:
                    bFlag = sorValidar.SIGN.Equals(sorComparador.SIGN);
                    break;
                default:
                    return false;
            }
            return bFlag;
        }
    }
}
