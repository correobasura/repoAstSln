using Constantes;
using IView;
using Model.DataContextModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
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
            var date = DateTime.Today;
            if (date.DayOfWeek != DayOfWeek.Sunday)
            {
                this.GetNumerosDespuesDelActual(listaDatosSol, _resultActualSol, "SOL");
                this.ValidarRachas(listaDatosSol, "SOL");
            }
            this.GetNumerosDespuesDelActual(listaDatosLun, _resultActualLun, "LUN");
            this.ValidarRachas(listaDatosLun, "LUN");
        }

        /// <summary>
        /// Método que obtiene y asigna los datos de los últimos resultados ingresados
        /// </summary>
        private void ObtenerUltimoResultado()
        {
            _resultActualSol = listaDatosSol.Last();
            _resultActualLun = listaDatosLun.Last();
        }

        /// <summary>
        /// Método que Analiza los numeros de la lista, de acuerdo al comparador recibido
        /// </summary>
        /// <param name="listaValidar">Lista que contiene los datos a validar</param>
        /// <param name="sorComparador">Objeto que sirve como comparador de datos</param>
        private void GetNumerosDespuesDelActual(List<ASTR> listaValidar, ASTR sorComparador, string cadSort)
        {
            Dictionary<int, int> contadorPosUno = this.InicializarDiccionarioNumDespuesActual();
            Dictionary<int, int> contadorPosDos = this.InicializarDiccionarioNumDespuesActual();
            Dictionary<int, int> contadorPosTres = this.InicializarDiccionarioNumDespuesActual();
            Dictionary<int, int> contadorPosCuatro = this.InicializarDiccionarioNumDespuesActual();
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

            this.EscribirNumerosDespuesActual(contadorPosUno, cadSort + "PosUnoDespActual");
            this.EscribirNumerosDespuesActual(contadorPosDos, cadSort + "PosDosDespActual");
            this.EscribirNumerosDespuesActual(contadorPosTres, cadSort + "PosTresDespActual");
            this.EscribirNumerosDespuesActual(contadorPosCuatro, cadSort + "PosCuatroDespActual");
            this.EscribirNumerosDespuesActualSign(clonedDictionary, cadSort + "SignDespActual");
        }

        /// <summary>
        /// Método que inicializa los diccioanros contadores para validar los que caen 
        /// después del actual
        /// </summary>
        /// <param name="dict"></param>
        private Dictionary<int, int> InicializarDiccionarioNumDespuesActual()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < 10; i++)
            {
                dict.Add(i, 0);
            }
            return dict;
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

        /// <summary>
        /// Método que obtiene las listas de las rachas para cada valor
        /// </summary>
        /// <param name="listaValidar"></param>
        private void ValidarRachas(List<ASTR> listaValidar, string sort)
        {
            Dictionary<int, List<int>> dicPosUno = this.InicializarDiccionarioRachas();
            Dictionary<int, List<int>> dicPosDos = this.InicializarDiccionarioRachas();
            Dictionary<int, List<int>> dicPosTres = this.InicializarDiccionarioRachas();
            Dictionary<int, List<int>> dicPosCuatro = this.InicializarDiccionarioRachas();
            Dictionary<string, List<int>> dicSign = this.InicializarDiccionarioRachasSign();
            foreach (var item in listaValidar)
            {
                this.AdicionarElementoDiccionarioRachas(dicPosUno, (int)item.POS_UNO);
                this.AdicionarElementoDiccionarioRachas(dicPosDos, (int)item.POS_DOS);
                this.AdicionarElementoDiccionarioRachas(dicPosTres, (int)item.POS_TRES);
                this.AdicionarElementoDiccionarioRachas(dicPosCuatro, (int)item.POS_CUATRO);
                this.AdicionarElementoDiccionarioRachasSign(dicSign, item.SIGN);
            }
            Dictionary<int, List<int>> dicRachasPosUno = this.ContarRachasPositivasNegativas(dicPosUno);
            Dictionary<int, List<int>> dicRachasPosDos = this.ContarRachasPositivasNegativas(dicPosDos);
            Dictionary<int, List<int>> dicRachasPosTres = this.ContarRachasPositivasNegativas(dicPosTres);
            Dictionary<int, List<int>> dicRachasPosCuatro = this.ContarRachasPositivasNegativas(dicPosCuatro);
            Dictionary<string, List<int>> dicRachasSign = this.ContarRachasPositivasNegativasSign(dicSign);
            this.EscribirDatosArchivo(dicRachasPosUno, sort + "Pos_uno_file");
            this.EscribirDatosArchivo(dicRachasPosDos, sort + "Pos_dos_file");
            this.EscribirDatosArchivo(dicRachasPosTres, sort + "Pos_tres_file");
            this.EscribirDatosArchivo(dicRachasPosCuatro, sort + "Pos_cuatro_file");
            this.EscribirDatosArchivoSign(dicRachasSign, sort + "Sign_file");
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

        /// <summary>
        /// Método que inicializa la estructura que lleva los diccionarios de rachas para los signos
        /// </summary>
        private Dictionary<string, List<int>> InicializarDiccionarioRachasSign()
        {
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            dict.Add(ACUARIO, new List<int>());
            dict.Add(ARIES, new List<int>());
            dict.Add(CANCER, new List<int>());
            dict.Add(CAPRICORNIO, new List<int>());
            dict.Add(ESCORPION, new List<int>());
            dict.Add(GEMINIS, new List<int>());
            dict.Add(LEO, new List<int>());
            dict.Add(LIBRA, new List<int>());
            dict.Add(PISCIS, new List<int>());
            dict.Add(SAGITARIO, new List<int>());
            dict.Add(TAURO, new List<int>());
            dict.Add(VIRGO, new List<int>());
            return dict;
        }

        /// <summary>
        /// Método que inicializa la estructura que lleva los diccionarios de rachas para los signos
        /// </summary>
        /// <param name="dict">diccionario para llenar información</param>
        /// <param name="sign">cadena para asignar</param>
        private void AdicionarElementoDiccionarioRachasSign(Dictionary<string, List<int>> dict, string sign)
        {
            foreach (var item in dict)
            {
                if (item.Key.Equals(sign))
                {
                    item.Value.Add(1);
                }
                else
                {
                    item.Value.Add(0);
                }
            }
        }

        /// <summary>
        /// Método que marca una posición, cero o uno, de acuerdo al valor recibido en el dato
        /// si coincide con la clave, marca la posición como uno, de lo contrario marca cero
        /// </summary>
        /// <param name="dict">Diccionario con vlos valores de las posiciones</param>
        /// <param name="dato">dato que se buscará en la clave del diccioanrio</param>
        private void AdicionarElementoDiccionarioRachas(Dictionary<int, List<int>> dict, int dato)
        {
            foreach (var item in dict)
            {
                if (item.Key.Equals(dato))
                {
                    item.Value.Add(1);
                }
                else
                {
                    item.Value.Add(0);
                }
            }
        }

        /// <summary>
        /// Método que retorna un diccionario para cada clave, indicando las veces seguidas que un valor está presente, mostrando un valor positivo, 
        /// y un valor negativo, para indicar que deja de estar presente
        /// </summary>
        /// <param name="dict">diccionario que contiene la lista con valores 1 o 0 para cada coincidencia</param>
        /// <returns>Diccionario con valores de que está presente</returns>
        private Dictionary<int, List<int>> ContarRachasPositivasNegativas(Dictionary<int, List<int>> dict)
        {
            Dictionary<int, List<int>> dicRachas = new Dictionary<int, List<int>>();
            foreach (var item in dict)
            {

                if (!dicRachas.ContainsKey(item.Key))
                {
                    dicRachas.Add(item.Key, new List<int>());
                }

                int? anterior = null;
                int contNegativo = 0;
                int contPositivo = 0;
                foreach (var itemList in item.Value)
                {
                    ///Si el valor es cero, indica que no ha caido
                    if (itemList.Equals(0))
                    {
                        if (anterior.Equals(1))
                        {
                            dicRachas[item.Key].Add(contPositivo);
                            contPositivo = 0;
                            contNegativo = 0;
                        }
                        contNegativo--;
                    }
                    else
                    {
                        ///Si el valor es uno, indica que el valor cayó
                        if (anterior.Equals(0))
                        {
                            dicRachas[item.Key].Add(contNegativo);
                            contNegativo = 0;
                            contPositivo = 0;
                        }
                        contPositivo++;
                    }
                    anterior = itemList;
                }
                if (contNegativo != 0)
                {
                    dicRachas[item.Key].Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dicRachas[item.Key].Add(contPositivo);
                }
            }
            return dicRachas;
        }

        /// <summary>
        /// Método que retorna un diccionario para cada clave, indicando las veces seguidas que un valor está presente, mostrando un valor positivo, 
        /// y un valor negativo, para indicar que deja de estar presente
        /// </summary>
        /// <param name="dict">diccionario que contiene la lista con valores 1 o 0 para cada coincidencia</param>
        /// <returns>Diccionario con valores de que está presente</returns>
        private Dictionary<string, List<int>> ContarRachasPositivasNegativasSign(Dictionary<string, List<int>> dict)
        {
            Dictionary<string, List<int>> dicRachas = new Dictionary<string, List<int>>();
            foreach (var item in dict)
            {
                if (!dicRachas.ContainsKey(item.Key))
                {
                    dicRachas.Add(item.Key, new List<int>());
                }

                int? anterior = null;
                int contNegativo = -1;
                int contPositivo = 1;
                foreach (var itemList in item.Value)
                {
                    ///Si el valor es cero, indica que no ha caido
                    if (itemList.Equals(0))
                    {
                        if (anterior.Equals(1))
                        {
                            dicRachas[item.Key].Add(contPositivo);
                            contPositivo = 0;
                            contNegativo = 0;
                        }
                        contNegativo--;
                    }
                    else
                    {
                        ///Si el valor es uno, indica que el valor cayó
                        if (anterior.Equals(0))
                        {
                            dicRachas[item.Key].Add(contNegativo);
                            contNegativo = 0;
                            contPositivo = 0;
                        }
                        contPositivo++;
                    }
                    anterior = itemList;
                }
                if (contNegativo != 0)
                {
                    dicRachas[item.Key].Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dicRachas[item.Key].Add(contPositivo);
                }
            }
            return dicRachas;
        }

        /// <summary>
        /// Método que escribe los datos recibidos en el diccionario en los archivos
        /// </summary>
        /// <param name="dict">diccionario con datos</param>
        /// <param name="cad">cadena que hace parte del nombre del archivo a escribir</param>
        private void EscribirDatosArchivo(Dictionary<int, List<int>> dict, string cad)
        {
            string fic = @"C:\temp\" + cad + ".txt";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Key + ":");
                List<int> listTempNegativa = (from x in item.Value
                                              where x < 0
                                              select x).ToList();
                List<int> listTempPositiva = (from x in item.Value
                                              where x > 0
                                              select x).ToList();
                listTempNegativa.Sort();
                listTempPositiva.Sort();
                Dictionary<int, int> dictContRachaNegativa = new Dictionary<int, int>();
                this.AgruparRachas(listTempNegativa, dictContRachaNegativa);
                Dictionary<int, int> dictContRachaPositiva = new Dictionary<int, int>();
                this.AgruparRachas(listTempPositiva, dictContRachaPositiva);
                sw.WriteLine("U\tP\tA\tTA Racha Item");
                int ultimo = item.Value.Last();
                sw.WriteLine(ultimo + "\t" + item.Value.ElementAt(item.Value.Count - 3)
                    + "\t" + item.Value.ElementAt(item.Value.Count - 5)
                    + "\t" + item.Value.ElementAt(item.Value.Count - 7)
                    + "\t" + item.Value.ElementAt(item.Value.Count - 9)
                    + "\t" + item.Value.ElementAt(item.Value.Count - 11));
                if (dictContRachaNegativa.ContainsKey(ultimo))
                {
                    sw.WriteLine("Ultimo dentro de histórico");
                    sw.WriteLine(ultimo + "=" + dictContRachaNegativa[ultimo]);
                }
                //sw.WriteLine("Positiva agrupada");
                //this.EscribirDataDiccionario(sw, dictContRachaPositiva);
                //sw.WriteLine("");
                //sw.WriteLine("Negativa agrupada");
                //this.EscribirDataDiccionario(sw, dictContRachaNegativa);
                //sw.WriteLine("");
                this.EscribirDiezMayores(sw, dictContRachaNegativa);
                sw.WriteLine("");
            }
            sw.Close();
        }

        /// <summary>
        /// Método que escribe los datos recibidos en el diccionario en los archivos
        /// </summary>
        /// <param name="dict">diccionario con datos</param>
        /// <param name="cad">cadena que hace parte del nombre del archivo a escribir</param>
        private void EscribirDatosArchivoSign(Dictionary<string, List<int>> dict, string cad)
        {
            string fic = @"C:\temp\" + cad + ".txt";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Key + ":");
                List<int> listTempNegativa = (from x in item.Value
                                              where x < 0
                                              select x).ToList();
                List<int> listTempPositiva = (from x in item.Value
                                              where x > 0
                                              select x).ToList();
                listTempNegativa.Sort();
                listTempPositiva.Sort();
                Dictionary<int, int> dictContRachaNegativa = new Dictionary<int, int>();
                this.AgruparRachas(listTempNegativa, dictContRachaNegativa);
                Dictionary<int, int> dictContRachaPositiva = new Dictionary<int, int>();
                this.AgruparRachas(listTempPositiva, dictContRachaPositiva);
                sw.WriteLine("U\tP\tA\tTA, Racha Item");
                int ultimo = item.Value.Last();
                sw.WriteLine(ultimo + "\t" + item.Value.ElementAt(item.Value.Count - 3)
                    + "\t" + item.Value.ElementAt(item.Value.Count - 5)
                    + "\t" + item.Value.ElementAt(item.Value.Count - 7)
                    + "\t" + item.Value.ElementAt(item.Value.Count - 9)
                    + "\t" + item.Value.ElementAt(item.Value.Count - 11));
                if (dictContRachaNegativa.ContainsKey(ultimo))
                {
                    sw.WriteLine("Ultimo dentro de histórico");
                    sw.WriteLine(ultimo + "=" + dictContRachaNegativa[ultimo]);
                }
                //sw.WriteLine("Positiva agrupada");
                //this.EscribirDataDiccionario(sw, dictContRachaPositiva);
                //sw.WriteLine("");
                //this.EscribirDataDiccionario(sw, dictContRachaNegativa);
                this.EscribirDiezMayores(sw, dictContRachaNegativa);
                sw.WriteLine("");
            }
            sw.Close();
        }

        /// <summary>
        /// Método que escribe los datos de los diez mayores contadores en el diccionario
        /// para facilitar la revisión de datos
        /// </summary>
        /// <param name="sw">Objeto que escribe los datos</param>
        /// <param name="dict">diccionario de numeros</param>
        /// <param name="dictSign">diccionario de signos</param>
        /// <param name="caso">caso que indica si se escribe el de numeros(1) o signos(2)</param>
        private void EscribirDiezMayores(StreamWriter sw, Dictionary<int, int> dict)
        {
            sw.WriteLine("Rachas");
            var sortedDict = from entry in dict orderby entry.Value descending select entry;
            int i = 0;
            foreach (var itemDic in sortedDict.ToDictionary(x => x.Key, x => x.Value).OrderByDescending(x => x.Value))
            {
                sw.Write(itemDic.Key + "=" + itemDic.Value + ",");
                i++;
                if (i == 10)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Método que escribe los datos
        /// </summary>
        /// <param name="sw">Objeto usado para escribir</param>
        /// <param name="dictContadorRachas">objeto con contador de rachas organizadas</param>
        private void EscribirDataDiccionario(StreamWriter sw, Dictionary<int, int> dictContadorRachas)
        {
            foreach (var itemList in dictContadorRachas)
            {
                sw.Write(itemList.Key + "=" + itemList.Value + ",");
            }
        }

        /// <summary>
        /// Método encargado de contar y agrupar las rachas para cada valor
        /// </summary>
        /// <param name="listaRecorrer"></param>
        /// <param name="diccionarioAgrupador">diccionario para agrupar los datos</param>
        private void AgruparRachas(List<int> listaRecorrer, Dictionary<int, int> diccionarioAgrupador)
        {
            foreach (var item in listaRecorrer)
            {
                if (diccionarioAgrupador.ContainsKey(item))
                {
                    diccionarioAgrupador[item]++;
                }
                else
                {
                    diccionarioAgrupador.Add(item, 1);
                }
            }
        }

        /// <summary>
        /// Método que escribe los datos correspondientes a las coincidencias después de la actual
        /// </summary>
        /// <param name="dict">diccionario con datos obtenidos</param>
        /// <param name="cad">nombre asignado al archivo</param>
        private void EscribirNumerosDespuesActual(Dictionary<int, int> dict, string cad)
        {
            string fic = @"C:\temp\" + cad + ".txt";
            StreamWriter sw = new StreamWriter(fic);
            var sortedDict = from entry in dict orderby entry.Value descending select entry;
            dict = sortedDict.ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Key + "=" + item.Value);
            }
            sw.Close();
        }

        /// <summary>
        /// Método que escribe los datos correspondientes a las coincidencias después de la actual
        /// </summary>
        /// <param name="dict">diccionario con datos obtenidos</param>
        /// <param name="cad">nombre asignado al archivo</param>
        private void EscribirNumerosDespuesActualSign(Dictionary<string, int> dict, string cad)
        {
            string fic = @"C:\temp\" + cad + ".txt";
            StreamWriter sw = new StreamWriter(fic);
            var sortedDict = from entry in dict orderby entry.Value descending select entry;
            dict = sortedDict.ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in dict)
            {
                sw.WriteLine(item.Key + "=" + item.Value);
            }
            sw.Close();
        }
    }
}
