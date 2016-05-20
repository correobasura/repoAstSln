using Constantes;
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
        private ASTR _resultActualSol;
        private ASTR _resultActualLun;
        private List<ASTR> listaDatosSol;
        private List<ASTR> listaDatosLun;
        private List<ASTR> listaDatosGeneral;
        Dictionary<string, int> dicSign = new Dictionary<string, int>();
        private const string ACUARIO = "ACUARIO";
        private const string ARIES = "ARIES";
        private const string CANCER = "CANCER";
        private const string CAPRICORNIO = "CAPRICORNIO";
        private const string ESCORPION = "ESCORPION";
        private const string GEMINIS = "GEMINIS";
        private const string LEO = "LEO";
        private const string LIBRA = "LIBRA";
        private const string PISCIS = "PISCIS";
        private const string SAGITARIO = "SAGITARIO";
        private const string TAURO = "TAURO";
        private const string VIRGO = "VIRGO";


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="iAccesoDatos"></param>
        public PAccesoDatos(IAccesoDatos iAccesoDatos)
        {
            this._astEntities = new AstEntities();
            this._iAccesoDatos = iAccesoDatos;
            this.dicSign.Add(ACUARIO, 0);
            this.dicSign.Add(ARIES, 0);
            this.dicSign.Add(CANCER, 0);
            this.dicSign.Add(CAPRICORNIO, 0);
            this.dicSign.Add(ESCORPION, 0);
            this.dicSign.Add(GEMINIS, 0);
            this.dicSign.Add(LEO, 0);
            this.dicSign.Add(LIBRA, 0);
            this.dicSign.Add(PISCIS, 0);
            this.dicSign.Add(SAGITARIO, 0);
            this.dicSign.Add(TAURO, 0);
            this.dicSign.Add(VIRGO, 0);
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
            this.ValidarRachas(listaDatosSol);
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
            bool flagPosUno = false;
            bool flagPosDos = false;
            bool flagPosTres = false;
            bool flagPosCuatro = false;
            bool flagSign = false;
            var clonedDictionary = dicSign.ToDictionary(
                x => x.Key, // Typically no cloning necessary (immuable)
                x => (int)x.Value  // Do the copy how you want
            );
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
                if (flagSign)
                {
                    clonedDictionary[item.SIGN]++;
                }
                flagPosUno = this.ValidarMismoDato(1, item, sorComparador);
                flagPosDos = this.ValidarMismoDato(2, item, sorComparador);
                flagPosTres = this.ValidarMismoDato(3, item, sorComparador);
                flagPosCuatro = this.ValidarMismoDato(4, item, sorComparador);
                flagSign = this.ValidarMismoDato(5, item, sorComparador);
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

        private void ValidarRachas(List<ASTR> listaValidar)
        {
            Dictionary<int, List<int>> dicPosUno = this.InicializarDiccionarioRachas();
            Dictionary < int, List < int >> dicPosDos = this.InicializarDiccionarioRachas();
            Dictionary < int, List < int >> dicPosTres = this.InicializarDiccionarioRachas();
            Dictionary < int, List < int >> dicPosCuatro = this.InicializarDiccionarioRachas();
            //Dictionary < int, List < int >> dicSign = this.InicializarDiccionarioRachas();
            foreach (var item in listaValidar)
            {
                this.AdicionarElementoDiccionarioRachas(dicPosUno, (int)item.POS_UNO);
            }
        }

        /// <summary>
        /// Método que inicializa la estructura que lleva los diccionarios de rachas
        /// </summary>
        private Dictionary<int, List<int>> InicializarDiccionarioRachas()
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < 10; i++)
            {
                List<int> lista = new List<int>();
                dict.Add(i, lista);
            }
            return dict;
        }

        private void AdicionarElementoDiccionarioRachas(Dictionary<int, List<int>> dict, int dato)
        {
            foreach (var item in dict)
            {
                if(item.Key.Equals(dato))
                {
                    item.Value.Add(1);
                }
                else
                {
                    item.Value.Add(0);
                }
            }
        }
    }
}
