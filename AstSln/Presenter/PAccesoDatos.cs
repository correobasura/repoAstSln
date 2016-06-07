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
using DTOs;

namespace Presenter
{
    public class PAccesoDatos
    {
        private AstEntities _astEntities;
        private IAccesoDatos _iAccesoDatos;
        private ASTR _resultActualLun;
        private ASTR _resultActualSol;
        Dictionary<string, int> dicSign;
        Dictionary<int, int> dictPuntuadorCuatroLun;
        Dictionary<int, int> dictPuntuadorCuatroSol;
        Dictionary<int, int> dictPuntuadorDosLun;
        Dictionary<int, int> dictPuntuadorDosSol;
        Dictionary<string, int> dictPuntuadorSignLun;
        Dictionary<string, int> dictPuntuadorSignSol;
        Dictionary<int, int> dictPuntuadorTresLun;
        Dictionary<int, int> dictPuntuadorTresSol;
        Dictionary<int, int> dictPuntuadorUnoLun;
        Dictionary<int, int> dictPuntuadorUnoSol;
        private List<ASTR> listaDatosGeneral;
        private List<ASTR> listaDatosLun;
        private List<ASTR> listaDatosSol;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="iAccesoDatos"></param>
        public PAccesoDatos(IAccesoDatos iAccesoDatos)
        {
            this._astEntities = new AstEntities();
            this._iAccesoDatos = iAccesoDatos;
            this.InicializarDiccionarioSignosEnteros(dicSign);
            this.dictPuntuadorCuatroSol = this.InicializarDiccionarioEnteros();
            this.dictPuntuadorDosSol = this.InicializarDiccionarioEnteros();
            this.dictPuntuadorTresSol = this.InicializarDiccionarioEnteros();
            this.dictPuntuadorUnoSol = this.InicializarDiccionarioEnteros();
            this.dictPuntuadorSignSol = (Dictionary<string, int>)this.InicializarDiccionarioEnteros();
            this.dictPuntuadorCuatroLun = this.InicializarDiccionarioEnteros();
            this.dictPuntuadorDosLun = this.InicializarDiccionarioEnteros();
            this.dictPuntuadorTresLun = this.InicializarDiccionarioEnteros();
            this.dictPuntuadorUnoLun = this.InicializarDiccionarioEnteros();
            this.dictPuntuadorSignLun = (Dictionary<string, int>)this.InicializarDiccionarioEnteros();

        }

        private void InicializarDiccionarioSignosEnteros(Dictionary<string, int> dict)
        {
            dict = new Dictionary<string, int>();
            dict.Add(ConstantesGenerales.ACUARIO, 0);
            dict.Add(ConstantesGenerales.ARIES, 0);
            dict.Add(ConstantesGenerales.CANCER, 0);
            dict.Add(ConstantesGenerales.CAPRICORNIO, 0);
            dict.Add(ConstantesGenerales.ESCORPION, 0);
            dict.Add(ConstantesGenerales.GEMINIS, 0);
            dict.Add(ConstantesGenerales.LEO, 0);
            dict.Add(ConstantesGenerales.LIBRA, 0);
            dict.Add(ConstantesGenerales.PISCIS, 0);
            dict.Add(ConstantesGenerales.SAGITARIO, 0);
            dict.Add(ConstantesGenerales.TAURO, 0);
            dict.Add(ConstantesGenerales.VIRGO, 0);
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
            this.RealizarPuntuacion();
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

        /// <summary>
        /// Método que Analiza los numeros de la lista, de acuerdo al comparador recibido
        /// </summary>
        /// <param name="listaValidar">Lista que contiene los datos a validar</param>
        /// <param name="sorComparador">Objeto que sirve como comparador de datos</param>
        private void GetNumerosDespuesDelActual(List<ASTR> listaValidar, ASTR sorComparador, string cadSort)
        {
            Dictionary<int, int> contadorPosUno = this.InicializarDiccionarioEnteros();
            Dictionary<int, int> contadorPosDos = this.InicializarDiccionarioEnteros();
            Dictionary<int, int> contadorPosTres = this.InicializarDiccionarioEnteros();
            Dictionary<int, int> contadorPosCuatro = this.InicializarDiccionarioEnteros();
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
        private Dictionary<int, int> InicializarDiccionarioEnteros()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < 10; i++)
            {
                dict.Add(i, 0);
            }
            return dict;
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
            dict.Add(ConstantesGenerales.ACUARIO, new List<int>());
            dict.Add(ConstantesGenerales.ARIES, new List<int>());
            dict.Add(ConstantesGenerales.CANCER, new List<int>());
            dict.Add(ConstantesGenerales.CAPRICORNIO, new List<int>());
            dict.Add(ConstantesGenerales.ESCORPION, new List<int>());
            dict.Add(ConstantesGenerales.GEMINIS, new List<int>());
            dict.Add(ConstantesGenerales.LEO, new List<int>());
            dict.Add(ConstantesGenerales.LIBRA, new List<int>());
            dict.Add(ConstantesGenerales.PISCIS, new List<int>());
            dict.Add(ConstantesGenerales.SAGITARIO, new List<int>());
            dict.Add(ConstantesGenerales.TAURO, new List<int>());
            dict.Add(ConstantesGenerales.VIRGO, new List<int>());
            return dict;
        }

        private object InicializarPuntuadores(int tipoDiccionario)
        {
            switch (tipoDiccionario)
            {
                case 0:
                    Dictionary<int, int> dictNumeros = this.InicializarDiccionarioEnteros();
                    return dictNumeros;
                case 1:
                    Dictionary<string, int> dictSignTemp = new Dictionary<string, int>();
                    dictSignTemp.Add(ConstantesGenerales.ACUARIO, 0);
                    dictSignTemp.Add(ConstantesGenerales.ARIES, 0);
                    dictSignTemp.Add(ConstantesGenerales.CANCER, 0);
                    dictSignTemp.Add(ConstantesGenerales.CAPRICORNIO, 0);
                    dictSignTemp.Add(ConstantesGenerales.ESCORPION, 0);
                    dictSignTemp.Add(ConstantesGenerales.GEMINIS, 0);
                    dictSignTemp.Add(ConstantesGenerales.LEO, 0);
                    dictSignTemp.Add(ConstantesGenerales.LIBRA, 0);
                    dictSignTemp.Add(ConstantesGenerales.PISCIS, 0);
                    dictSignTemp.Add(ConstantesGenerales.SAGITARIO, 0);
                    dictSignTemp.Add(ConstantesGenerales.TAURO, 0);
                    dictSignTemp.Add(ConstantesGenerales.VIRGO, 0);
                    return dictSignTemp;
                default:
                    return null;
            }
        }

        private string ObtenerConsulta(int caso, string aliasColumna)
        {
            string query = "";
            DateTime today = DateTime.Today;
            switch (caso)
            {
                ///Agrupa los contadores generales de los numeros sin condiciones
                case 0:
                    query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE tipo = {1} GROUP BY {0}";
                    break;

                ///Agrupa los contadores de acuerdo al día de la semana
                case 1:
                    query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE TO_CHAR(fecha, 'D') = " + ((int)today.DayOfWeek + 1) + " AND tipo = {1} GROUP BY {0}";
                    break;

                ///Agrupa los contadores de acuerdo al día del mes
                case 2:
                    query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE TO_CHAR(fecha, 'DD') = " + today.Day + " AND tipo = {1} GROUP BY {0}";
                    break;

                ///Agrupa los contadores de acuerdo al día par o impar
                case 3:
                    //Día par
                    if (today.Day % 2 == 0)
                    {
                        query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE MOD(TO_CHAR(fecha, 'DD'),2) = 0 AND tipo = {1} GROUP BY {0}";
                    }
                    //Día Impar
                    else
                    {
                        query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE MOD(TO_CHAR(fecha, 'DD'),2) = 1 AND tipo = {1} GROUP BY {0}";
                    }
                    break;
                ///Agrupa los contadores de acuerdo al mes
                case 4:
                    query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE TO_CHAR(fecha, 'MM') = " + today.Month + " AND tipo = {1} GROUP BY {0}";
                    break;
                ///Agrupa los contadores de acuerdo al día del año
                case 5:
                    query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE TO_CHAR(fecha, 'DDD') = " + today.DayOfYear + " AND tipo = {1} GROUP BY {0}";
                    break;
                ///Agrupa los contadores de acuerdo al día del año par o impar
                case 6:
                    //Día del año par
                    if (today.DayOfYear % 2 == 0)
                    {
                        query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE MOD(TO_CHAR(fecha, 'DDD'),2) = 0 AND tipo = {1} GROUP BY {0}";
                    }
                    //Día del año impar
                    else
                    {
                        query = "SELECT {0} AS " + aliasColumna + ", DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE MOD(TO_CHAR(fecha, 'DDD'),2) = 1 AND tipo = {1} GROUP BY {0}";
                    }
                    break;
            }
            return query;
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
        /// 
        /// </summary>
        /// <param name="posicion"></param>
        /// <param name="tipo"></param>
        private void PuntuarInformacion(string posicion, int tipo, int casoConsulta, int casoDiccionario, string aliasColumna)
        {
            string query = string.Format(this.ObtenerConsulta(casoConsulta, aliasColumna), posicion, tipo);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(query);
            switch (casoDiccionario)
            {
                case 1:
                    if (tipo.Equals(ConstantesTipoSor.TIPO_SOL))
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorUnoSol);
                    }
                    else
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorUnoLun);
                    }
                    break;
                case 2:
                    if (tipo.Equals(ConstantesTipoSor.TIPO_SOL))
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorUnoSol);
                    }
                    else
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorUnoLun);
                    }
                    break;
                case 3:
                    if (tipo.Equals(ConstantesTipoSor.TIPO_SOL))
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorUnoSol);
                    }
                    else
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorUnoLun);
                    }
                    break;
                case 4:
                    if (tipo.Equals(ConstantesTipoSor.TIPO_SOL))
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorUnoSol);
                    }
                    else
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorUnoLun);
                    }
                    break;
                case 5:
                    if (tipo.Equals(ConstantesTipoSor.TIPO_SOL))
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorSignSol);
                    }
                    else
                    {
                        this.SumarDatosDiccionario(data, dictPuntuadorSignLun);
                    }
                    break;
            }

        }

        private void RealizarPuntuacion()
        {
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_SOL, 0, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_SOL, 1, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_SOL, 2, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_SOL, 3, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_SOL, 4, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_SOL, 5, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_SOL, 6, 1, "ClaveNum");

            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_SOL, 0, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_SOL, 1, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_SOL, 2, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_SOL, 3, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_SOL, 4, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_SOL, 5, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_SOL, 6, 2, "ClaveNum");

            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_SOL, 0, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_SOL, 1, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_SOL, 2, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_SOL, 3, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_SOL, 4, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_SOL, 5, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_SOL, 6, 3, "ClaveNum");

            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_SOL, 0, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_SOL, 1, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_SOL, 2, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_SOL, 3, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_SOL, 4, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_SOL, 5, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_SOL, 6, 4, "ClaveNum");

            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_SOL, 0, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_SOL, 1, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_SOL, 2, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_SOL, 3, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_SOL, 4, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_SOL, 5, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_SOL, 6, 2, "ClaveSign");

            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, 0, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, 1, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, 2, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, 3, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, 4, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, 5, 1, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, 6, 1, "ClaveNum");

            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, 0, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, 1, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, 2, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, 3, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, 4, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, 5, 2, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, 6, 2, "ClaveNum");

            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, 0, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, 1, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, 2, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, 3, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, 4, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, 5, 3, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, 6, 3, "ClaveNum");

            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, 0, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, 1, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, 2, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, 3, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, 4, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, 5, 4, "ClaveNum");
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, 6, 4, "ClaveNum");

            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, 0, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, 1, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, 2, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, 3, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, 4, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, 5, 2, "ClaveSign");
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, 6, 2, "ClaveSign");
        }
        private void SumarDatosDiccionario(DbRawSqlQuery<QueryInfo> data, Dictionary<int, int> dict)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum] += (10 - cust.Rank);
            }
        }

        private void SumarDatosDiccionario(DbRawSqlQuery<QueryInfo> data, Dictionary<string, int> dict)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign] += (12 - cust.Rank);
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
    }
}
