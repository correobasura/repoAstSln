﻿using Constantes;
using IView;
using Model.DataContextModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using DTOs;

namespace Presenter
{
    public class PAccesoDatos
    {
        private AstEntities _astEntities;
        private IAccesoDatos _iAccesoDatos;
        private ASTR _resultActualLun;
        private ASTR _resultActualSol;
        Dictionary<int, ObjectInfoDTO> dictInfoPosCuatroLun;
        Dictionary<int, ObjectInfoDTO> dictInfoPosDosLun;
        Dictionary<int, ObjectInfoDTO> dictInfoPosTresLun;
        Dictionary<int, ObjectInfoDTO> dictInfoPosUnoLun;
        Dictionary<string, ObjectInfoDTO> dictInfoSignLun;
        Dictionary<int, ObjectInfoDTO> dictInfoPosCuatroSol;
        Dictionary<int, ObjectInfoDTO> dictInfoPosDosSol;
        Dictionary<int, ObjectInfoDTO> dictInfoPosTresSol;
        Dictionary<int, ObjectInfoDTO> dictInfoPosUnoSol;
        Dictionary<string, ObjectInfoDTO> dictInfoSignSol;
        private List<ASTR> listaDatosGeneral;
        private List<ASTR> listaDatosLun;
        private List<ASTR> listaDatosSol;
        private string path = "";
        private DateTime fecha;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="iAccesoDatos"></param>
        public PAccesoDatos(IAccesoDatos iAccesoDatos)
        {
            this._astEntities = new AstEntities();
            this._iAccesoDatos = iAccesoDatos;
            this.dictInfoPosUnoLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosDosLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosTresLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosCuatroLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoSignLun = (Dictionary<string, ObjectInfoDTO>)this.InicializarDiccionarioInformacion(1);
            this.dictInfoPosUnoSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosDosSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosTresSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosCuatroSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoSignSol = (Dictionary<string, ObjectInfoDTO>)this.InicializarDiccionarioInformacion(1);
            fecha = DateTime.Today;
            path = @"C:\temp" + @"\" + fecha.Year + "" + fecha.Month + @"\" + fecha.Day + @"\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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
            //this.ValidarDatosRepetidos(listaDatosLun);
            this.ObtenerUltimoResultado();
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, dictInfoPosUnoLun);
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, dictInfoPosDosLun);
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, dictInfoPosTresLun);
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, dictInfoPosCuatroLun);
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, dictInfoSignLun);
            if (fecha.DayOfWeek != DayOfWeek.Sunday)
            {
                this.RecorrerElementosLista(listaDatosSol, _resultActualSol, dictInfoPosUnoSol, dictInfoPosDosSol, dictInfoPosTresSol, dictInfoPosCuatroSol, dictInfoSignSol);
                this.ValidarRachas(listaDatosSol, dictInfoPosUnoSol, dictInfoPosDosSol, dictInfoPosTresSol, dictInfoPosCuatroSol, dictInfoSignSol);
                this.EscribirDatosArchivo(dictInfoPosUnoSol, "PosUnoSol");
                this.EscribirDatosArchivo(dictInfoPosDosSol, "PosDosSol");
                this.EscribirDatosArchivo(dictInfoPosTresSol, "PosTresSol");
                this.EscribirDatosArchivo(dictInfoPosCuatroSol, "PosCuatroSol");
                this.EscribirDatosArchivo(dictInfoSignSol, "SignSol");
            }
            this.RecorrerElementosLista(listaDatosLun, _resultActualLun, dictInfoPosUnoLun, dictInfoPosDosLun, dictInfoPosTresLun, dictInfoPosCuatroLun, dictInfoSignLun);
            this.ValidarRachas(listaDatosLun, dictInfoPosUnoLun, dictInfoPosDosLun, dictInfoPosTresLun, dictInfoPosCuatroLun, dictInfoSignLun);
            this.EscribirDatosArchivo(dictInfoPosUnoLun, "PosUnoLun");
            this.EscribirDatosArchivo(dictInfoPosDosLun, "PosDosLun");
            this.EscribirDatosArchivo(dictInfoPosTresLun, "PosTresLun");
            this.EscribirDatosArchivo(dictInfoPosCuatroLun, "PosCuatroLun");
            this.EscribirDatosArchivo(dictInfoSignLun, "SignLun");
        }

        private void ValidarDatosRepetidos(List<ASTR> listica)
        {
            List<decimal> listaIdentificadores = new List<decimal>();
            DateTime fechaInicial = (DateTime)listica.ElementAt(0).FECHA;
            for (int i = 1; i < listica.Count; i++)
            {
                ASTR astUno = listica.ElementAt(i);
                for (int j = i + 1; j < listica.Count; j++)
                {
                    ASTR astTemp = listica.ElementAt(j);
                    //if (astTemp.POS_UNO == astUno.POS_UNO
                    //    && astTemp.POS_DOS == astUno.POS_DOS
                    //    && astTemp.POS_TRES == astUno.POS_TRES
                    //    && astTemp.POS_CUATRO == astUno.POS_CUATRO
                    //    && astTemp.SIGN == astUno.SIGN)
                    //{
                    //    if (!listaIdentificadores.Contains(astUno.ID)) listaIdentificadores.Add(astUno.ID);
                    //    if (!listaIdentificadores.Contains(astTemp.ID)) listaIdentificadores.Add(astTemp.ID);
                    //}
                    if (astUno.FECHA == astTemp.FECHA)
                    {
                        if (!listaIdentificadores.Contains(astUno.ID)) listaIdentificadores.Add(astUno.ID);
                        if (!listaIdentificadores.Contains(astTemp.ID)) listaIdentificadores.Add(astTemp.ID);
                    }
                }
                //DateTime fechaSor = (DateTime)listica.ElementAt(i).FECHA;
                //if (!fechaSor.AddDays(-1).Equals(fechaInicial))
                //{
                //    listaIdentificadores.Add(fechaInicial);
                //}
                //fechaInicial = fechaSor;
            }
            List<ASTR> listaTemp = new List<ASTR>();
            //Si no se pagina la lista, se obtienen todos los resultados, de lo contrario, se traen los resultados solicitados
            listaTemp = (from ast in listica
                         where listaIdentificadores.Contains(ast.ID)
                         select ast).ToList();
            string fic = path + "repetidos.txt";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in listaTemp)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }

        /// <summary>
        /// Método que marca una posición, cero o uno, de acuerdo al valor recibido en el dato
        /// si coincide con la clave, marca la posición como uno, de lo contrario marca cero
        /// </summary>
        /// <param name="dict">Diccionario con valos valores de las posiciones</param>
        /// <param name="dato">dato que se buscará en la clave del diccionario</param>
        private void AdicionarElementoDiccionarioRachas(Dictionary<int, ObjectInfoDTO> dict, int dato)
        {
            foreach (var item in dict)
            {
                if (item.Key.Equals(dato))
                {
                    item.Value.RachasAparicion.Add(1);
                }
                else
                {
                    item.Value.RachasAparicion.Add(0);
                }
            }
        }

        /// <summary>
        /// Método que marca una posición, cero o uno, de acuerdo al valor recibido en el dato
        /// si coincide con la clave, marca la posición como uno, de lo contrario marca cero
        /// </summary>
        /// <param name="dict">Diccionario con valos valores de las posiciones</param>
        /// <param name="dato">dato que se buscará en la clave del diccionario</param>
        private void AdicionarElementoDiccionarioRachasSign(Dictionary<string, ObjectInfoDTO> dict, string sign)
        {
            foreach (var item in dict)
            {
                if (item.Key.Equals(sign))
                {
                    item.Value.RachasAparicion.Add(1);
                }
                else
                {
                    item.Value.RachasAparicion.Add(0);
                }
            }
        }

        /// <summary>
        /// Método encargado de contar y agrupar las rachas para cada valor
        /// </summary>
        /// <param name="dict">diccionario con información a ser agrupada</param>
        private void AgruparRachas(Dictionary<int, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                foreach (var itemList in item.Value.RachasAcumuladas)
                {
                    if (item.Value.DictRachasAgrupadasInt.ContainsKey(itemList))
                    {
                        item.Value.DictRachasAgrupadasInt[itemList]++;
                    }
                    else
                    {
                        item.Value.DictRachasAgrupadasInt.Add(itemList, 1);
                    }
                }
                var sortedDict = from entry in item.Value.DictRachasAgrupadasInt orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasInt = sortedDict.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        /// Método encargado de contar y agrupar las rachas para cada valor
        /// </summary>
        /// <param name="dict">diccionario con información a ser agrupada</param>
        private void AgruparRachas(Dictionary<string, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                foreach (var itemList in item.Value.RachasAcumuladas)
                {
                    if (item.Value.DictRachasAgrupadasInt.ContainsKey(itemList))
                    {
                        item.Value.DictRachasAgrupadasInt[itemList]++;
                    }
                    else
                    {
                        item.Value.DictRachasAgrupadasInt.Add(itemList, 1);
                    }
                }
                var sortedDict = from entry in item.Value.DictRachasAgrupadasInt orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasInt = sortedDict.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        /// Método que realiza el conteo de los valores sucesivos que son iguales
        /// </summary>
        /// <param name="dict">diccionario que contiene la información</param>
        private void ContarRachasPositivasNegativas(Dictionary<int, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                int? anterior = null;
                int contNegativo = 0;
                int contPositivo = 0;
                foreach (var itemList in item.Value.RachasAparicion)
                {
                    ///Si el valor es cero, indica que no ha caido
                    if (itemList.Equals(0))
                    {
                        if (anterior.Equals(1))
                        {
                            dict[item.Key].RachasAcumuladas.Add(contPositivo);
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
                            dict[item.Key].RachasAcumuladas.Add(contNegativo);
                            contNegativo = 0;
                            contPositivo = 0;
                        }
                        contPositivo++;
                    }
                    anterior = itemList;
                }
                if (contNegativo != 0)
                {
                    dict[item.Key].RachasAcumuladas.Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dict[item.Key].RachasAcumuladas.Add(contPositivo);
                }
                dict[item.Key].RachasAparicion.Clear();
            }
        }

        /// <summary>
        /// Método que realiza el conteo de los valores sucesivos que son iguales
        /// </summary>
        /// <param name="dict">diccionario que contiene la información</param>
        private void ContarRachasPositivasNegativasSign(Dictionary<string, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                int? anterior = null;
                int contNegativo = -1;
                int contPositivo = 1;
                foreach (var itemList in item.Value.RachasAparicion)
                {
                    ///Si el valor es cero, indica que no ha caido
                    if (itemList.Equals(0))
                    {
                        if (anterior.Equals(1))
                        {
                            dict[item.Key].RachasAcumuladas.Add(contPositivo);
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
                            dict[item.Key].RachasAcumuladas.Add(contNegativo);
                            contNegativo = 0;
                            contPositivo = 0;
                        }
                        contPositivo++;
                    }
                    anterior = itemList;
                }
                if (contNegativo != 0)
                {
                    dict[item.Key].RachasAcumuladas.Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dict[item.Key].RachasAcumuladas.Add(contPositivo);
                }
                dict[item.Key].RachasAparicion.Clear();
            }
        }

        /// <summary>
        /// Método que retorna una estructura requerida para el caso recibido
        /// </summary>
        /// <param name="caso"></param>
        /// <returns></returns>
        private object InicializarDiccionarioInformacion(int caso = 0)
        {
            if (caso == 0)
            {
                Dictionary<int, ObjectInfoDTO> dict = new Dictionary<int, ObjectInfoDTO>();
                for (int i = 0; i < 10; i++)
                {
                    dict.Add(i, new ObjectInfoDTO());
                }
                return dict;
            }
            else
            {
                Dictionary<string, ObjectInfoDTO> dict = new Dictionary<string, ObjectInfoDTO>();
                dict.Add(ConstantesGenerales.ACUARIO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.ARIES, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.CANCER, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.CAPRICORNIO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.ESCORPION, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.GEMINIS, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.LEO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.LIBRA, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.PISCIS, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.SAGITARIO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.TAURO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.VIRGO, new ObjectInfoDTO());
                return dict;
            }

        }

        /// <summary>
        /// Método que obtiene la consulta que se realiza
        /// </summary>
        /// <param name="caso">Caso que valida cual es la consulta que se debe retornar</param>
        /// <returns>Consulta generada</returns>
        private string ObtenerParametrosQuery(int caso)
        {
            switch (caso)
            {
                ///Agrupa los contadores de acuerdo al día de la semana
                case 1:
                    return "AND TO_CHAR(fecha, 'D') = " + ((int)fecha.DayOfWeek + 1);
                ///Agrupa los contadores de acuerdo al día del mes
                case 2:
                    return "AND TO_CHAR(fecha, 'DD') = " + fecha.Day;
                ///Agrupa los contadores de acuerdo al día par o impar
                case 3:
                    return "AND MOD(TO_CHAR(fecha, 'DD'),2) = " + (fecha.Day % 2);
                ///Agrupa los contadores de acuerdo al mes
                case 4:
                    return "AND TO_CHAR(fecha, 'MM') = " + fecha.Month;
                ///Agrupa los contadores de acuerdo al día del año
                case 5:
                    return "AND TO_CHAR(fecha, 'DDD') = " + (fecha.DayOfYear);
                ///Agrupa los contadores de acuerdo al día del año par o impar
                case 6:
                    return "AND MOD(TO_CHAR(fecha, 'DDD'),2) = " + (fecha.DayOfYear % 2);
                ///Agrupa los contadores de acuerdo al mes par o impar
                case 7:
                    return "AND MOD(TO_CHAR(fecha, 'MM'),2) = " + (fecha.Month % 2);
                ///Agrupa los contadores de acuerdo al mes par o impar y el día par o impar
                case 8:
                    return "AND MOD(TO_CHAR(fecha, 'MM'),2) = " + (fecha.Month % 2) + " AND MOD(TO_CHAR(fecha, 'DD'),2) = " + (fecha.Day % 2);
                ///Agrupa los contadores de acuerdo al mes y al día
                case 9:
                    return "AND TO_CHAR(fecha, 'MM') = " + fecha.Month + " AND TO_CHAR(fecha, 'DD') = " + fecha.Day;
                ///Agrupa los contadores de acuerdo al año par o impar
                case 10:
                    return "AND MOD(TO_CHAR(fecha, 'YYYY'),2) = " + (fecha.Year % 2);
                default:
                    return "";
            }
        }

        /// <summary>
        /// Método que escribe los datos recibidos en el diccionario en los archivos
        /// </summary>
        /// <param name="dict">diccionario con datos</param>
        /// <param name="cad">cadena que hace parte del nombre del archivo a escribir</param>
        private void EscribirDatosArchivo(Dictionary<int, ObjectInfoDTO> dict, string cad)
        {
            string fic = path + fecha.Day +cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            sw.WriteLine(ConstantesGenerales.ENCABEZADOS);
            foreach (var item in dict)
            {
                sw.Write(item.Key);
                sw.WriteLine(item.Value.ToString());
            }
            sw.Close();
        }

        /// <summary>
        /// Método que escribe los datos recibidos en el diccionario en los archivos
        /// </summary>
        /// <param name="dict">diccionario con datos</param>
        /// <param name="cad">cadena que hace parte del nombre del archivo a escribir</param>
        private void EscribirDatosArchivo(Dictionary<string, ObjectInfoDTO> dict, string cad)
        {
            string fic = path + fecha.Day + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            sw.WriteLine(ConstantesGenerales.ENCABEZADOS);
            foreach (var item in dict)
            {
                sw.Write(item.Key);
                sw.WriteLine(item.Value.ToString());
            }
            sw.Close();
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
        /// Método que realiza el llamado al método que suma los datos para puntuar la información
        /// </summary>
        /// <param name="posicion">referencia la posición que se evalua dentro del registro (Pos_uno, Pos_dos...)</param>
        /// <param name="tipo">Referencia al tipo de registro que se evalua(Sol-Lun)</param>
        private void PuntuarInformacion(string posicion, int tipo, Dictionary<int, ObjectInfoDTO> dict)
        {
            string query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(0), "ClaveNum");
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContGeneral(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(1), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaSemana(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(2), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaMes(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(3), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(4), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMes(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(5), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaAnio(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(6), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AdinionarInformacionContadorDiaAnioModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(7), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(8), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesModuloDiaModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(9), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesDia(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(10), "ClaveNum");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContAnioModulo(dict, data);
        }

        /// <summary>
        /// Método que realiza el llamado al método que suma los datos para puntuar la información
        /// </summary>
        /// <param name="posicion">referencia la posición que se evalua dentro del registro (Pos_uno, Pos_dos...)</param>
        /// <param name="tipo">Referencia al tipo de registro que se evalua(Sol-Lun)</param>
        private void PuntuarInformacion(string posicion, int tipo, Dictionary<string, ObjectInfoDTO> dict)
        {
            string query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(0), "ClaveSign");
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContGeneral(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(1), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaSemana(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(2), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaMes(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(3), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(4), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMes(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(5), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaAnio(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(6), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AdinionarInformacionContadorDiaAnioModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(7), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(8), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesModuloDiaModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(9), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesDia(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE_STRING, posicion, tipo, this.ObtenerParametrosQuery(10), "ClaveSign");
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContAnioModulo(dict, data);
        }

        /// <summary>
        /// Método que recorre los elementos de la lista, y valida las apariciones siguientes
        /// </summary>
        /// <param name="listaValidar">lista que contiene los datos a validar</param>
        /// <param name="sorComparador">Elemento que sirve como comparador</param>
        /// <param name="dictPosUno">diccionario al que se realiza el incremento para la posición uno</param>
        /// <param name="dictPosDos">diccionario al que se realiza el incremento para la posición dos</param>
        /// <param name="dictPostres">diccionario al que se realiza el incremento para la posición tres</param>
        /// <param name="dictPosCuatro">diccionario al que se realiza el incremento para la posición cuatro</param>
        /// <param name="dictSign">diccionario al que se realiza el incremento para sign</param>
        private void RecorrerElementosLista(List<ASTR> listaValidar, ASTR sorComparador, Dictionary<int, ObjectInfoDTO> dictPosUno,
            Dictionary<int, ObjectInfoDTO> dictPosDos, Dictionary<int, ObjectInfoDTO> dictPostres, Dictionary<int, ObjectInfoDTO> dictPosCuatro,
            Dictionary<string, ObjectInfoDTO> dictSign)
        {
            bool flagPosUno = false;
            bool flagPosDos = false;
            bool flagPosTres = false;
            bool flagPosCuatro = false;
            bool flagSign = false;
            foreach (var item in listaValidar)
            {
                if (flagPosUno)
                {
                    dictPosUno[(int)item.POS_UNO].ContadorDespuesActual++;
                }
                if (flagPosDos)
                {
                    dictPosDos[(int)item.POS_DOS].ContadorDespuesActual++;
                }
                if (flagPosTres)
                {
                    dictPostres[(int)item.POS_TRES].ContadorDespuesActual++;
                }
                if (flagPosCuatro)
                {
                    dictPosCuatro[(int)item.POS_CUATRO].ContadorDespuesActual++;
                }
                if (flagSign)
                {
                    dictSign[item.SIGN].ContadorDespuesActual++;
                    dictPosUno[(int)item.POS_UNO].ContadorDespuesSignActual++;
                    dictPosDos[(int)item.POS_DOS].ContadorDespuesSignActual++;
                    dictPostres[(int)item.POS_TRES].ContadorDespuesSignActual++;
                    dictPosCuatro[(int)item.POS_CUATRO].ContadorDespuesSignActual++;
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

        /// <summary>
        /// Método que obtiene las listas de las rachas para cada valor
        /// </summary>
        /// <param name="listaValidar">lista con elementos a validar</param>
        /// <param name="dictPosUno">diccionario para validacion de posicion 1</param>
        /// <param name="dictPosDos">diccionario para validacion de posicion 2</param>
        /// <param name="dictPostres">diccionario para validacion de posicion 3</param>
        /// <param name="dictPosCuatro">diccionario para validacion de posicion 4</param>
        /// <param name="dictSign">diccionario para validacion de sign</param>
        private void ValidarRachas(List<ASTR> listaValidar, Dictionary<int, ObjectInfoDTO> dictPosUno,
            Dictionary<int, ObjectInfoDTO> dictPosDos, Dictionary<int, ObjectInfoDTO> dictPostres, Dictionary<int, ObjectInfoDTO> dictPosCuatro,
            Dictionary<string, ObjectInfoDTO> dictSign)
        {
            foreach (var item in listaValidar)
            {
                this.AdicionarElementoDiccionarioRachas(dictPosUno, (int)item.POS_UNO);
                this.AdicionarElementoDiccionarioRachas(dictPosDos, (int)item.POS_DOS);
                this.AdicionarElementoDiccionarioRachas(dictPostres, (int)item.POS_TRES);
                this.AdicionarElementoDiccionarioRachas(dictPosCuatro, (int)item.POS_CUATRO);
                this.AdicionarElementoDiccionarioRachasSign(dictSign, item.SIGN);
            }
            this.ContarRachasPositivasNegativas(dictPosUno);
            this.ContarRachasPositivasNegativas(dictPosDos);
            this.ContarRachasPositivasNegativas(dictPostres);
            this.ContarRachasPositivasNegativas(dictPosCuatro);
            this.ContarRachasPositivasNegativasSign(dictSign);
            this.AgruparRachas(dictPosUno);
            this.AgruparRachas(dictPosDos);
            this.AgruparRachas(dictPostres);
            this.AgruparRachas(dictPosCuatro);
            this.AgruparRachas(dictSign);
        }
    }
}
