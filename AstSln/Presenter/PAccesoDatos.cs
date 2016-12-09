using Constantes;
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
        private Dictionary<int, ObjectInfoDTO> dictInfoPosCuatroLun;
        //private Dictionary<int, ObjectInfoDTO> dictInfoPosCuatroSol;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosDosLun;
        //private Dictionary<int, ObjectInfoDTO> dictInfoPosDosSol;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosTresLun;
        //private Dictionary<int, ObjectInfoDTO> dictInfoPosTresSol;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosUnoLun;
        //private Dictionary<int, ObjectInfoDTO> dictInfoPosUnoSol;
        private Dictionary<string, ObjectInfoDTO> dictInfoSignLun;
        //private Dictionary<string, ObjectInfoDTO> dictInfoSignSol;
        private DateTime fecha;
        private List<ASTR> listaDatosGeneral;
        private List<ASTR> listaDatosLun;
        private string path = "";
        private string fechaFormat;
        private List<string> listaConsultas;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="iAccesoDatos"></param>
        public PAccesoDatos(IAccesoDatos iAccesoDatos, DateTime fecha)
        {
            this._astEntities = new AstEntities();
            this._iAccesoDatos = iAccesoDatos;
            this.dictInfoPosCuatroLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            //this.dictInfoPosCuatroSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosDosLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            //this.dictInfoPosDosSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosTresLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            //this.dictInfoPosTresSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosUnoLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            //this.dictInfoPosUnoSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoSignLun = (Dictionary<string, ObjectInfoDTO>)this.InicializarDiccionarioInformacion(1);
            //this.dictInfoSignSol = (Dictionary<string, ObjectInfoDTO>)this.InicializarDiccionarioInformacion(1);
            this.fecha = fecha;
            this.fechaFormat = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            //path = @"C:\temp" + @"\" + fecha.Year + "" + fecha.Month + @"\" + fecha.Day + @"\";
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            listaConsultas = new List<string>();
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
                listaDatosGeneral = objectContex.CreateObjectSet<ASTR>().Where(x => x.FECHA < this.fecha).OrderBy(x => x.FECHA).ToList();
                //listaDatosSol = listaDatosGeneral.Where(x => x.TIPO == 1).ToList();
                listaDatosLun = listaDatosGeneral;
            }
            //this.ValidarDatosRepetidos(listaDatosSol);
            this.ObtenerUltimoResultado();
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, dictInfoPosUnoLun, (int)_resultActualLun.POS_UNO);
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, dictInfoPosDosLun, (int)_resultActualLun.POS_DOS);
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, dictInfoPosTresLun, (int)_resultActualLun.POS_TRES);
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, dictInfoPosCuatroLun, (int)_resultActualLun.POS_CUATRO);
            this.PuntuarInformacion(ConstantesGenerales.SIGN, dictInfoSignLun, _resultActualLun.SIGN);
            //if (fecha.DayOfWeek != DayOfWeek.Sunday)
            //{
            //    this.RecorrerElementosLista(listaDatosSol, _resultActualSol, dictInfoPosUnoSol, dictInfoPosDosSol, dictInfoPosTresSol, dictInfoPosCuatroSol, dictInfoSignSol);
            //    this.RecorrerElementosLista(listaDatosSol, _resultActualLun, dictInfoPosUnoSol, dictInfoPosDosSol, dictInfoPosTresSol, dictInfoPosCuatroSol, dictInfoSignSol);
            //    this.ValidarRachas(listaDatosSol, dictInfoPosUnoSol, dictInfoPosDosSol, dictInfoPosTresSol, dictInfoPosCuatroSol, dictInfoSignSol);
            //    this.EscribirDatosArchivo(dictInfoPosUnoSol, "PosUnoSol");
            //    this.EscribirDatosArchivo(dictInfoPosDosSol, "PosDosSol");
            //    this.EscribirDatosArchivo(dictInfoPosTresSol, "PosTresSol");
            //    this.EscribirDatosArchivo(dictInfoPosCuatroSol, "PosCuatroSol");
            //    this.EscribirDatosArchivo(dictInfoSignSol, "SignSol");
            //}
            this.RecorrerElementosLista(listaDatosLun, _resultActualLun, dictInfoPosUnoLun, dictInfoPosDosLun, dictInfoPosTresLun, dictInfoPosCuatroLun, dictInfoSignLun);
            //this.RecorrerElementosLista(listaDatosLun, _resultActualSol, dictInfoPosUnoLun, dictInfoPosDosLun, dictInfoPosTresLun, dictInfoPosCuatroLun, dictInfoSignLun);
            this.ValidarRachas(listaDatosLun, dictInfoPosUnoLun, dictInfoPosDosLun, dictInfoPosTresLun, dictInfoPosCuatroLun, dictInfoSignLun);

            //this.GuardarDatosTemporales();

            //var dict1 = AnalisisDatosPorPosicion.ValidarInfoPorAnalisisAtributos(dictInfoPosUnoLun, _astEntities, ConstantesGenerales.POS_UNO_DATOS, fechaFormat);
            //var dict2 = AnalisisDatosPorPosicion.ValidarInfoPorAnalisisAtributos(dictInfoPosDosLun, _astEntities, ConstantesGenerales.POS_DOS_DATOS, fechaFormat);
            //var dict3 = AnalisisDatosPorPosicion.ValidarInfoPorAnalisisAtributos(dictInfoPosTresLun, _astEntities, ConstantesGenerales.POS_TRES_DATOS, fechaFormat);
            //var dict4 = AnalisisDatosPorPosicion.ValidarInfoPorAnalisisAtributos(dictInfoPosCuatroLun, _astEntities, ConstantesGenerales.POS_CUATRO_DATOS, fechaFormat);
            //var dictSign = AnalisisDatosPorPosicion.ValidarInfoPorAnalisisAtributos(dictInfoSignLun, _astEntities, fechaFormat);

            //var dict1 = this.ValidarAparicionesDespActual(dictInfoPosUnoLun, ConstantesGenerales.POS_UNO_DATOS);
            //var dict2 = this.ValidarAparicionesDespActual(dictInfoPosDosLun, ConstantesGenerales.POS_DOS_DATOS);
            //var dict3 = this.ValidarAparicionesDespActual(dictInfoPosTresLun, ConstantesGenerales.POS_TRES_DATOS);
            //var dict4 = this.ValidarAparicionesDespActual(dictInfoPosCuatroLun, ConstantesGenerales.POS_CUATRO_DATOS);
            //var dictSign = this.ValidarAparicionesDespActual(dictInfoSignLun, ConstantesGenerales.SIGN_DATOS);

            //dict1 = this.ValidarSinAparecer(dict1, ConstantesGenerales.POS_UNO_DATOS);
            //dict2 = this.ValidarSinAparecer(dict2, ConstantesGenerales.POS_DOS_DATOS);
            //dict3 = this.ValidarSinAparecer(dict3, ConstantesGenerales.POS_TRES_DATOS);
            //dict4 = this.ValidarSinAparecer(dict4, ConstantesGenerales.POS_CUATRO_DATOS);
            //dictSign = this.ValidarSinAparecer(dictSign, ConstantesGenerales.SIGN_DATOS);

            //var dict1 = this.ValidarMaximosMinimosPuntuacionTotal(dictInfoPosUnoLun, ConstantesGenerales.AN_DAT_POS_UNO, 3, 12);
            //var dict2 = this.ValidarMaximosMinimosPuntuacionTotal(dictInfoPosDosLun, ConstantesGenerales.AN_DAT_POS_DOS, 3, 12);
            //var dict3 = this.ValidarMaximosMinimosPuntuacionTotal(dictInfoPosTresLun, ConstantesGenerales.AN_DAT_POS_TRES, 3, 12);
            //var dict4 = this.ValidarMaximosMinimosPuntuacionTotal(dictInfoPosCuatroLun, ConstantesGenerales.AN_DAT_POS_CUATRO, 3, 12);
            //var dictSign = this.ValidarMaximosMinimosPuntuacionTotal(dictInfoSignLun, ConstantesGenerales.AN_DAT_SIGN, 3, 12);

            //dict1 = this.ValidarIndicadores(dict1, ConstantesGenerales.AN_DAT_POS_UNO);
            //dict2 = this.ValidarIndicadores(dict2, ConstantesGenerales.AN_DAT_POS_DOS);
            //dict3 = this.ValidarIndicadores(dict3, ConstantesGenerales.AN_DAT_POS_TRES);
            //dict4 = this.ValidarIndicadores(dict4, ConstantesGenerales.AN_DAT_POS_CUATRO);
            //dictSign = this.ValidarIndicadoresPosicion(dictSign, ConstantesGenerales.AN_DAT_SIGN);            
            var dictSign = this.ValidarIndicadoresPosicion(dictInfoSignLun, ConstantesGenerales.AN_DAT_SIGN);

            //var dict1 = this.EliminarValoresMinimos(dictInfoPosUnoLun, ConstantesGenerales.POS_UNO_DATOS);
            //var dict2 = this.EliminarValoresMinimos(dictInfoPosDosLun, ConstantesGenerales.POS_DOS_DATOS);
            //var dict3 = this.EliminarValoresMinimos(dictInfoPosTresLun, ConstantesGenerales.POS_TRES_DATOS);
            //var dict4 = this.EliminarValoresMinimos(dictInfoPosCuatroLun, ConstantesGenerales.POS_CUATRO_DATOS);
            //var dictSign = this.EliminarValoresMinimos(dictInfoSignLun, ConstantesGenerales.SIGN_DATOS);


            //this.RevisarValoresMinimos(dictInfoPosUnoLun, "PosUnoLunDep", ConstantesGenerales.AN_DAT_POS_UNO, ConstantesTipoSor.POSICION_UNO, ConstantesGenerales.POS_UNO_DATOS);
            //this.RevisarValoresMinimos(dictInfoPosDosLun, "PosDosLunDep", ConstantesGenerales.AN_DAT_POS_DOS, ConstantesTipoSor.POSICION_DOS, ConstantesGenerales.POS_DOS_DATOS);
            //this.RevisarValoresMinimos(, "PosTresLunDep", ConstantesGenerales.AN_DAT_POS_TRES, ConstantesTipoSor.POSICION_TRES, ConstantesGenerales.POS_TRES_DATOS);
            //this.RevisarValoresMinimos(dictInfoPosCuatroLun, "PosCuatroLunDep", ConstantesGenerales.AN_DAT_POS_CUATRO, ConstantesTipoSor.POSICION_CUATRO, ConstantesGenerales.POS_CUATRO_DATOS);
            //this.RevisarValoresMinimos(dictInfoSignLun, "PosSignDep", ConstantesGenerales.AN_DAT_SIGN, ConstantesTipoSor.POSICION_CINCO, ConstantesGenerales.SIGN_DATOS);
            //this.ObtenerUltimosRachasPosicion();
            //this.EliminarMismoDato(dictInfoPosUnoLun, "PosUnoLunDep5", ConstantesGenerales.AN_DAT_POS_UNO, 1);
            //this.EliminarMismoDato(dictInfoPosDosLun, "PosDosLunDep5", ConstantesGenerales.AN_DAT_POS_DOS, 2);
            //this.EliminarMismoDato(dictInfoPosTresLun, "PosTresLunDep5", ConstantesGenerales.AN_DAT_POS_TRES, 3);
            //this.EliminarMismoDato(dictInfoPosCuatroLun, "PosCuatroLunDep5", ConstantesGenerales.AN_DAT_POS_CUATRO, 4);
            //this.EliminarMismoDato(dictInfoSignLun, "PosSignDep5", ConstantesGenerales.AN_DAT_SIGN, 5);
            //this.EliminarDatoSumatoria(dictInfoPosUnoLun, "PosUnoLunDep6",1);
            //this.EliminarDatoSumatoria(dictInfoPosDosLun, "PosDosLunDep6",2);
            //this.EliminarDatoSumatoria(dictInfoPosTresLun, "PosTresLunDep6",3);
            //this.EliminarDatoSumatoria(dictInfoPosCuatroLun, "PosCuatroLunDep6",4);
            ////this.EliminarDatoSumatoria(dictInfoSignLun, "PosSignDep6",5);
            //var dict1 = this.EliminarMenorAparicionDespuesActual(dictInfoPosUnoLun, 1);
            //var dict2 = this.EliminarMenorAparicionDespuesActual(dictInfoPosDosLun, 2);
            //var dict3 = this.EliminarMenorAparicionDespuesActual(dictInfoPosTresLun, 3);
            //var dict4 = this.EliminarMenorAparicionDespuesActual(dictInfoPosCuatroLun, 4);
            //var dict5 = this.EliminarMenorAparicionDespuesActual(dictInfoSignLun, 5);
            //dict1 = this.ValidarSumatoriaDatos(dict1, "APosUnoLunMenores", ConstantesGenerales.POS_UNO_DATOS, "ASC");
            //dict2 = this.ValidarSumatoriaDatos(dict2, "BPosDosLunMenores", ConstantesGenerales.POS_DOS_DATOS, "ASC");
            //dict3 = this.ValidarSumatoriaDatos(dict3, "CPosTresLunMenores", ConstantesGenerales.POS_TRES_DATOS, "ASC");
            //dict4 = this.ValidarSumatoriaDatos(dict4, "DPosCuatroLunMenores", ConstantesGenerales.POS_CUATRO_DATOS, "ASC");
            //dict5 = this.ValidarSumatoriaDatos(dict5, "EPosSignMenores", ConstantesGenerales.SIGN_DATOS, "ASC");
            //this.RevisarValoresMinimos(dict1, "PosUnoLunDep", ConstantesGenerales.AN_DAT_POS_UNO, ConstantesTipoSor.POSICION_UNO, ConstantesGenerales.POS_UNO_DATOS);
            //this.RevisarValoresMinimos(dict2, "PosDosLunDep", ConstantesGenerales.AN_DAT_POS_DOS, ConstantesTipoSor.POSICION_DOS, ConstantesGenerales.POS_DOS_DATOS);
            //this.RevisarValoresMinimos(dict3, "PosTresLunDep", ConstantesGenerales.AN_DAT_POS_TRES, ConstantesTipoSor.POSICION_TRES, ConstantesGenerales.POS_TRES_DATOS);
            //this.RevisarValoresMinimos(dict4, "PosCuatroLunDep", ConstantesGenerales.AN_DAT_POS_CUATRO, ConstantesTipoSor.POSICION_CUATRO, ConstantesGenerales.POS_CUATRO_DATOS);
            //this.RevisarValoresMinimos(dict5, "PosSignDep", ConstantesGenerales.AN_DAT_SIGN, ConstantesTipoSor.POSICION_CINCO, ConstantesGenerales.SIGN_DATOS);
            //this.GuardarDatosTemporalesDepurados(dict1, 1);
            //this.GuardarDatosTemporalesDepurados(dict2, 2);
            //this.GuardarDatosTemporalesDepurados(dict3, 3);
            //this.GuardarDatosTemporalesDepurados(dict4, 4);
            this.GuardarDatosTemporalesDepurados(dictSign, 5);
            //this.EscribirDatosArchivo(dictInfoPosUnoLun, "APosUnoLun");
            //this.EscribirDatosArchivo(dictInfoPosDosLun, "BPosDosLun");
            //this.EscribirDatosArchivo(dictInfoPosTresLun, "CPosTresLun");
            //this.EscribirDatosArchivo(dictInfoPosCuatroLun, "DPosCuatroLun");
            //this.EscribirDatosArchivo(dictInfoSignLun, "ESignLun");
            //this.EscribirConsultas();
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
                item.Value.CONTADORULTIMOENRACHAS = item.Value.DictRachasAgrupadasInt.Where(x => x.Key == item.Value.RachasAcumuladas.Last()).FirstOrDefault().Value;
                var sortedDict = from entry in item.Value.DictRachasAgrupadasInt orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasInt = sortedDict.ToDictionary(x => x.Key, x => x.Value);

                foreach (var itemList in item.Value.RachasAcumuladasDespActual)
                {
                    if (item.Value.DictRachasAgrupadasIntDespActual.ContainsKey(itemList))
                    {
                        item.Value.DictRachasAgrupadasIntDespActual[itemList]++;
                    }
                    else
                    {
                        item.Value.DictRachasAgrupadasIntDespActual.Add(itemList, 1);
                    }
                }
                item.Value.CONTADORULTIMOENRACHASDESACTUA = item.Value.DictRachasAgrupadasIntDespActual.Where(x => x.Key == item.Value.RachasAcumuladasDespActual.Last()).FirstOrDefault().Value;
                sortedDict = from entry in item.Value.DictRachasAgrupadasIntDespActual orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasIntDespActual = sortedDict.ToDictionary(x => x.Key, x => x.Value);
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
                item.Value.CONTADORULTIMOENRACHAS = item.Value.DictRachasAgrupadasInt.Where(x => x.Key == item.Value.RachasAcumuladas.Last()).FirstOrDefault().Value;
                var sortedDict = from entry in item.Value.DictRachasAgrupadasInt orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasInt = sortedDict.ToDictionary(x => x.Key, x => x.Value);

                foreach (var itemList in item.Value.RachasAcumuladasDespActual)
                {
                    if (item.Value.DictRachasAgrupadasIntDespActual.ContainsKey(itemList))
                    {
                        item.Value.DictRachasAgrupadasIntDespActual[itemList]++;
                    }
                    else
                    {
                        item.Value.DictRachasAgrupadasIntDespActual.Add(itemList, 1);
                    }
                }
                item.Value.CONTADORULTIMOENRACHASDESACTUA = item.Value.DictRachasAgrupadasIntDespActual.Where(x => x.Key == item.Value.RachasAcumuladasDespActual.Last()).FirstOrDefault().Value;
                sortedDict = from entry in item.Value.DictRachasAgrupadasIntDespActual orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasIntDespActual = sortedDict.ToDictionary(x => x.Key, x => x.Value);
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
                int contNegativo = 0;
                int contPositivo = 0;
                this.RecorrerListaRachas(dict, item.Key, ref contNegativo, ref contPositivo, item.Value.RachasAparicion, 1);
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
            foreach (var item in dict)
            {
                int contNegativo = 0;
                int contPositivo = 0;
                this.RecorrerListaRachas(dict, item.Key, ref contNegativo, ref contPositivo, item.Value.RachasAparicionDespActual, 2);
                if (contNegativo != 0)
                {
                    dict[item.Key].RachasAcumuladasDespActual.Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dict[item.Key].RachasAcumuladasDespActual.Add(contPositivo);
                }
                dict[item.Key].RachasAparicionDespActual.Clear();
            }
        }

        /// <summary>
        /// Método que realiza el conteo de los valores sucesivos que son iguales
        /// </summary>
        /// <param name="dict">diccionario que contiene la información</param>
        private void ContarRachasPositivasNegativas(Dictionary<string, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                int contNegativo = 0;
                int contPositivo = 0;
                this.RecorrerListaRachas(dict, item.Key, ref contNegativo, ref contPositivo, item.Value.RachasAparicion, 1);
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
            foreach (var item in dict)
            {
                int contNegativo = 0;
                int contPositivo = 0;
                this.RecorrerListaRachas(dict, item.Key, ref contNegativo, ref contPositivo, item.Value.RachasAparicionDespActual, 2);
                if (contNegativo != 0)
                {
                    dict[item.Key].RachasAcumuladasDespActual.Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dict[item.Key].RachasAcumuladasDespActual.Add(contPositivo);
                }
                dict[item.Key].RachasAparicionDespActual.Clear();
            }
        }

        /// <summary>
        /// Método que realiza la escritura de los datos anteriores obtenidos
        /// </summary>
        /// <param name="ultimoResultado">Valor que sirve para comparar y obtener los datos guardados</param>
        private void EscribirDatosAnterior(ASTR ultimoResultado)
        {
            DATOS_TEMP dtPosUno = this.ObtenerResultadoAnalizado(1, (int)ultimoResultado.POS_UNO);
            DATOS_TEMP dtPosDos = this.ObtenerResultadoAnalizado(2, (int)ultimoResultado.POS_DOS);
            DATOS_TEMP dtPosTres = this.ObtenerResultadoAnalizado(3, (int)ultimoResultado.POS_TRES);
            DATOS_TEMP dtPosCuatro = this.ObtenerResultadoAnalizado(4, (int)ultimoResultado.POS_CUATRO);
            DATOS_TEMP dtPosSign = this.ObtenerResultadoAnalizado(5, ultimoResultado.SIGN);
            if (dtPosUno != null)
            {
                POS_UNO_DATOS p = new POS_UNO_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = dtPosUno.PUNTUACIONTOTAL;
                p.SINAPARECER = dtPosUno.SINAPARECER;
                p.CONTADORULTIMOENRACHAS = dtPosUno.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = dtPosUno.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = dtPosUno.CONTADORGENERAL;
                p.CONTADORDIASEMANA = dtPosUno.CONTADORDIASEMANA;
                p.CONTADORDIAMES = dtPosUno.CONTADORDIAMES;
                p.CONTADORDIAMODULO = dtPosUno.CONTADORDIAMODULO;
                p.CONTADORMES = dtPosUno.CONTADORMES;
                //p.CONTADORDIAANIO = dtPosUno.CONTADORDIAANIO;
                //p.CONTADORDIAANIOMODULO = dtPosUno.CONTADORDIAANIOMODULO;
                //p.CONTADORMESMODULODIAMODULO = dtPosUno.CONTADORMESMODULODIAMODULO;
                //p.CONTADORMESDIA = dtPosUno.CONTADORMESDIA;
                //p.CONTADORANIOMODULO = dtPosUno.CONTADORANIOMODULO;
                //p.CONTADORMESMODULO = dtPosUno.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = dtPosUno.CONTADORDESPUESACTUAL;
                //p.CONTADORDESPUESACTUALTOTAL = (dtPosUno.CONTADORGENERAL + dtPosUno.CONTADORDIASEMANA + dtPosUno.CONTADORDIAMES + dtPosUno.CONTADORDIAMODULO + dtPosUno.CONTADORMES + dtPosUno.CONTADORDESPUESACTUAL);
                p.SUMATORIAVALORES = dtPosUno.SUMATORIAVALORES;
                p.CONTADORDESPUESSIGNACTUAL = dtPosUno.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosUno.FECHA;
                this._astEntities.POS_UNO_DATOS.Add(p);
            }
            if (dtPosDos != null)
            {
                POS_DOS_DATOS p = new POS_DOS_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = dtPosDos.PUNTUACIONTOTAL;
                p.SINAPARECER = dtPosDos.SINAPARECER;
                p.CONTADORULTIMOENRACHAS = dtPosDos.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = dtPosDos.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = dtPosDos.CONTADORGENERAL;
                p.CONTADORDIASEMANA = dtPosDos.CONTADORDIASEMANA;
                p.CONTADORDIAMES = dtPosDos.CONTADORDIAMES;
                p.CONTADORDIAMODULO = dtPosDos.CONTADORDIAMODULO;
                p.CONTADORMES = dtPosDos.CONTADORMES;
                //p.CONTADORDIAANIO = dtPosDos.CONTADORDIAANIO;
                //p.CONTADORDIAANIOMODULO = dtPosDos.CONTADORDIAANIOMODULO;
                //p.CONTADORMESMODULODIAMODULO = dtPosDos.CONTADORMESMODULODIAMODULO;
                //p.CONTADORMESDIA = dtPosDos.CONTADORMESDIA;
                //p.CONTADORANIOMODULO = dtPosDos.CONTADORANIOMODULO;
                //p.CONTADORMESMODULO = dtPosDos.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = dtPosDos.CONTADORDESPUESACTUAL;
                //p.CONTADORDESPUESACTUALTOTAL = (dtPosUno.CONTADORGENERAL + dtPosUno.CONTADORDIASEMANA + dtPosUno.CONTADORDIAMES + dtPosUno.CONTADORDIAMODULO + dtPosUno.CONTADORMES + dtPosUno.CONTADORDESPUESACTUAL);
                p.SUMATORIAVALORES = dtPosDos.SUMATORIAVALORES;
                p.CONTADORDESPUESSIGNACTUAL = dtPosDos.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosDos.FECHA;
                this._astEntities.POS_DOS_DATOS.Add(p);
            }
            if (dtPosTres != null)
            {
                POS_TRES_DATOS p = new POS_TRES_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = dtPosTres.PUNTUACIONTOTAL;
                p.SINAPARECER = dtPosTres.SINAPARECER;
                p.CONTADORULTIMOENRACHAS = dtPosTres.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = dtPosTres.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = dtPosTres.CONTADORGENERAL;
                p.CONTADORDIASEMANA = dtPosTres.CONTADORDIASEMANA;
                p.CONTADORDIAMES = dtPosTres.CONTADORDIAMES;
                p.CONTADORDIAMODULO = dtPosTres.CONTADORDIAMODULO;
                p.CONTADORMES = dtPosTres.CONTADORMES;
                //p.CONTADORDIAANIO = dtPosTres.CONTADORDIAANIO;
                //p.CONTADORDIAANIOMODULO = dtPosTres.CONTADORDIAANIOMODULO;
                //p.CONTADORMESMODULODIAMODULO = dtPosTres.CONTADORMESMODULODIAMODULO;
                //p.CONTADORMESDIA = dtPosTres.CONTADORMESDIA;
                //p.CONTADORANIOMODULO = dtPosTres.CONTADORANIOMODULO;
                //p.CONTADORMESMODULO = dtPosTres.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = dtPosTres.CONTADORDESPUESACTUAL;
                //p.CONTADORDESPUESACTUALTOTAL = (dtPosTres.CONTADORGENERAL + dtPosTres.CONTADORDIASEMANA + dtPosTres.CONTADORDIAMES + dtPosTres.CONTADORDIAMODULO + dtPosTres.CONTADORMES + dtPosTres.CONTADORDESPUESACTUAL);
                p.SUMATORIAVALORES = dtPosTres.SUMATORIAVALORES;
                p.CONTADORDESPUESSIGNACTUAL = dtPosTres.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosTres.FECHA;
                this._astEntities.POS_TRES_DATOS.Add(p);
            }
            if (dtPosCuatro != null)
            {
                POS_CUATRO_DATOS p = new POS_CUATRO_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = dtPosCuatro.PUNTUACIONTOTAL;
                p.SINAPARECER = dtPosCuatro.SINAPARECER;
                p.CONTADORULTIMOENRACHAS = dtPosCuatro.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = dtPosCuatro.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = dtPosCuatro.CONTADORGENERAL;
                p.CONTADORDIASEMANA = dtPosCuatro.CONTADORDIASEMANA;
                p.CONTADORDIAMES = dtPosCuatro.CONTADORDIAMES;
                p.CONTADORDIAMODULO = dtPosCuatro.CONTADORDIAMODULO;
                p.CONTADORMES = dtPosCuatro.CONTADORMES;
                //p.CONTADORDIAANIO = dtPosCuatro.CONTADORDIAANIO;
                //p.CONTADORDIAANIOMODULO = dtPosCuatro.CONTADORDIAANIOMODULO;
                //p.CONTADORMESMODULODIAMODULO = dtPosCuatro.CONTADORMESMODULODIAMODULO;
                //p.CONTADORMESDIA = dtPosCuatro.CONTADORMESDIA;
                //p.CONTADORANIOMODULO = dtPosCuatro.CONTADORANIOMODULO;
                //p.CONTADORMESMODULO = dtPosCuatro.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = dtPosCuatro.CONTADORDESPUESACTUAL;
                //p.CONTADORDESPUESACTUALTOTAL = (dtPosCuatro.CONTADORGENERAL + dtPosCuatro.CONTADORDIASEMANA + dtPosCuatro.CONTADORDIAMES + dtPosCuatro.CONTADORDIAMODULO + dtPosCuatro.CONTADORMES + dtPosCuatro.CONTADORDESPUESACTUAL);
                p.SUMATORIAVALORES = dtPosCuatro.SUMATORIAVALORES;
                p.CONTADORDESPUESSIGNACTUAL = dtPosCuatro.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosCuatro.FECHA;
                this._astEntities.POS_CUATRO_DATOS.Add(p);
            }
            if (dtPosSign != null)
            {
                SIGN_DATOS p = new SIGN_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = dtPosSign.PUNTUACIONTOTAL;
                p.SINAPARECER = dtPosSign.SINAPARECER;
                p.CONTADORULTIMOENRACHAS = dtPosSign.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = dtPosSign.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = dtPosSign.CONTADORGENERAL;
                p.CONTADORDIASEMANA = dtPosSign.CONTADORDIASEMANA;
                p.CONTADORDIAMES = dtPosSign.CONTADORDIAMES;
                p.CONTADORDIAMODULO = dtPosSign.CONTADORDIAMODULO;
                p.CONTADORMES = dtPosSign.CONTADORMES;
                //p.CONTADORDIAANIO = dtPosSign.CONTADORDIAANIO;
                //p.CONTADORDIAANIOMODULO = dtPosSign.CONTADORDIAANIOMODULO;
                //p.CONTADORMESMODULODIAMODULO = dtPosSign.CONTADORMESMODULODIAMODULO;
                //p.CONTADORMESDIA = dtPosSign.CONTADORMESDIA;
                //p.CONTADORANIOMODULO = dtPosSign.CONTADORANIOMODULO;
                //p.CONTADORMESMODULO = dtPosSign.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = dtPosSign.CONTADORDESPUESACTUAL;
                //p.CONTADORDESPUESACTUALTOTAL = (dtPosSign.CONTADORGENERAL + dtPosSign.CONTADORDIASEMANA + dtPosSign.CONTADORDIAMES + dtPosSign.CONTADORDIAMODULO + dtPosSign.CONTADORMES + dtPosSign.CONTADORDESPUESACTUAL);
                p.SUMATORIAVALORES = dtPosSign.SUMATORIAVALORES;
                p.CONTADORDESPUESSIGNACTUAL = dtPosSign.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosSign.FECHA;
                this._astEntities.SIGN_DATOS.Add(p);
            }
            this._astEntities.SaveChanges();
        }

        private bool FechaActualEnMenoresApariciones(string tabla, string columna)
        {
            string query = string.Format(ConstantesConsultas.QUERY_AN_DATOS_COUNT_COLUMN, tabla, columna, fechaFormat);
            DbRawSqlQuery<ContadorValorDTO> data = _astEntities.Database.SqlQuery<ContadorValorDTO>(query);
            //var result = (from x in data select x.Contador).ToList().GroupBy(test => test)
            //       .Select(grp => grp.First())
            //       .ToList();
            List<int> listaP = (from x in data where x.Contador == data.ElementAt(0).Contador select x.Valor).ToList();
            //List<int> listaS = (from x in data where x.Contador == result.ElementAt(1) select x.Valor).ToList();
            //List<int> listaT = (from x in data where x.Contador == result.ElementAt(2) select x.Valor).ToList();
            bool indexP = listaP.IndexOf(this.fecha.Day) != -1;
            //bool indexS = listaS.IndexOf(this.fecha.Day) != -1;
            //bool indexT = listaT.IndexOf(this.fecha.Day) != -1;
            //return indexP || indexS || indexT;
            //return indexP || indexS;
            return indexP;
        }

        /// <summary>
        /// Método que realiza el guardado de los datos temporales
        /// </summary>
        private void GuardarDatosTemporales()
        {
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_UNO, dictInfoPosUnoLun);
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_DOS, dictInfoPosDosLun);
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_TRES, dictInfoPosTresLun);
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_CUATRO, dictInfoPosCuatroLun);
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_CINCO, dictInfoSignLun);
            this.EscribirDatosAnterior(this._resultActualLun);
        }

        /// <summary>
        /// Método que realiza el guardado de los datos analizados para la fecha
        /// </summary>
        /// <param name="tipo">Valor que indica el tipo sor</param>
        /// <param name="posicion">Valor que indica la posicion dentro del objeto (pos_uno, pos_dos,...)</param>
        /// <param name="dict">Diccionario de datos para analizar</param>
        private void GuardarDatosTemporales(int tipo, int posicion, Dictionary<int, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                DATOS_TEMP dt = this.ObtenerObjetoGuardar(item.Value);
                dt.CLAVE = item.Key;
                dt.TIPO = tipo;
                dt.POSICION = posicion;
                dt.FECHA = this.fecha;
                dt.ID = this.ObtenerValorSecuencia(dt);
                this._astEntities.DATOS_TEMP.Add(dt);
            }
            this._astEntities.SaveChanges();
        }

        /// <summary>
        /// Método que realiza el guardado de los datos analizados para la fecha
        /// </summary>
        /// <param name="tipo">Valor que indica el tipo sor</param>
        /// <param name="posicion">Valor que indica la posicion dentro del objeto (pos_uno, pos_dos,...)</param>
        /// <param name="dict">Diccionario de datos para analizar</param>
        private void GuardarDatosTemporales(int tipo, int posicion, Dictionary<string, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                DATOS_TEMP dt = this.ObtenerObjetoGuardar(item.Value);
                dt.CLAVESIGN = item.Key;
                dt.TIPO = tipo;
                dt.POSICION = posicion;
                dt.FECHA = this.fecha;
                dt.ID = this.ObtenerValorSecuencia(dt);
                this._astEntities.DATOS_TEMP.Add(dt);
            }
            this._astEntities.SaveChanges();
        }

        /// <summary>
        /// Método que realiza el guardado de los datos depurados
        /// </summary>
        /// <param name="dict">estructura que contiene los datos depurados</param>
        /// <param name="posicion">posicion que referencia en donde se guardan los datos</param>
        private void GuardarDatosTemporalesDepurados(Dictionary<int, ObjectInfoDTO> dict, int posicion)
        {
            foreach (var item in dict)
            {
                DATOS_TEMP_DEPUR dt = new DATOS_TEMP_DEPUR();
                dt.ID = this.ObtenerValorSecuencia(dt);
                dt.PUNTUACIONTOTAL = item.Value.PuntuacionTotal;
                dt.CONTADORULTIMOENRACHAS = item.Value.CONTADORULTIMOENRACHAS;
                dt.CONTADORULTIMOENRACHASDESACTUA = item.Value.CONTADORULTIMOENRACHASDESACTUA;
                dt.CONTADORGENERAL = item.Value.RankContadorGeneral;
                dt.CONTADORDIASEMANA = item.Value.RankContadorDiaSemana;
                dt.CONTADORDIAMES = item.Value.RankContadorDiaMes;
                dt.CONTADORDIAMODULO = item.Value.RankContadorDiaModulo;
                dt.CONTADORMES = item.Value.RankContadorMes;
                //dt.CONTADORDIAANIO = item.Value.RankContadorDiaAnio;
                //dt.CONTADORDIAANIOMODULO = item.Value.RankContadorDiaAnioModulo;
                //dt.CONTADORMESMODULODIAMODULO = item.Value.RankContadorMesModuloDiaModulo;
                //dt.CONTADORMESDIA = item.Value.RankContadorMesDia;
                //dt.CONTADORANIOMODULO = item.Value.RankContadorAnioModulo;
                //dt.CONTADORMESMODULO = item.Value.RankContadorMesModulo;
                dt.CONTADORDESPUESACTUAL = item.Value.ContadorDespuesActual;
                //dt.CONTADORDESPUESACTUALTOTAL = (item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual);
                dt.SUMATORIAVALORES = (item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual);
                dt.CONTADORDESPUESSIGNACTUAL = item.Value.ContadorDespuesSignActual;
                dt.CLAVE = item.Key;
                dt.TIPO = 2;
                dt.POSICION = posicion;
                dt.FECHA = this.fecha;
                this._astEntities.DATOS_TEMP_DEPUR.Add(dt);
            }
            this._astEntities.SaveChanges();
        }

        /// <summary>
        /// Método que realiza el guardado de los datos depurados
        /// </summary>
        /// <param name="dict">estructura que contiene los datos depurados</param>
        /// <param name="posicion">posicion que referencia en donde se guardan los datos</param>
        private void GuardarDatosTemporalesDepurados(Dictionary<string, ObjectInfoDTO> dict, int posicion)
        {
            foreach (var item in dict)
            {
                DATOS_TEMP_DEPUR dt = new DATOS_TEMP_DEPUR();
                dt.ID = this.ObtenerValorSecuencia(dt);
                dt.PUNTUACIONTOTAL = item.Value.PuntuacionTotal;
                dt.CONTADORULTIMOENRACHAS = item.Value.CONTADORULTIMOENRACHAS;
                dt.CONTADORULTIMOENRACHASDESACTUA = item.Value.CONTADORULTIMOENRACHASDESACTUA;
                dt.CONTADORGENERAL = item.Value.RankContadorGeneral;
                dt.CONTADORDIASEMANA = item.Value.RankContadorDiaSemana;
                dt.CONTADORDIAMES = item.Value.RankContadorDiaMes;
                dt.CONTADORDIAMODULO = item.Value.RankContadorDiaModulo;
                dt.CONTADORMES = item.Value.RankContadorMes;
                //dt.CONTADORDIAANIO = item.Value.RankContadorDiaAnio;
                //dt.CONTADORDIAANIOMODULO = item.Value.RankContadorDiaAnioModulo;
                //dt.CONTADORMESMODULODIAMODULO = item.Value.RankContadorMesModuloDiaModulo;
                //dt.CONTADORMESDIA = item.Value.RankContadorMesDia;
                //dt.CONTADORANIOMODULO = item.Value.RankContadorAnioModulo;
                //dt.CONTADORMESMODULO = item.Value.RankContadorMesModulo;
                dt.CONTADORDESPUESACTUAL = item.Value.ContadorDespuesActual;
                //dt.CONTADORDESPUESACTUALTOTAL = (item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual);
                dt.SUMATORIAVALORES = (item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual);
                dt.CONTADORDESPUESSIGNACTUAL = item.Value.ContadorDespuesSignActual;
                dt.CLAVESIGN = item.Key;
                dt.TIPO = 2;
                dt.POSICION = posicion;
                dt.FECHA = this.fecha;
                this._astEntities.DATOS_TEMP_DEPUR.Add(dt);
            }
            this._astEntities.SaveChanges();
        }

        /// <summary>
        /// Método que realiza el incremento para el diccionario, si la bandera recibida es true;
        /// </summary>
        /// <param name="dictIncrementar">Diccionaro que contiene la información sobre la que se realizan los incrementos</param>
        /// <param name="bandera">bandera que realiza la validación, y controla en incremento</param>
        /// <param name="claveDict">clave del diccionario sobre la que se realiza el incremento</param>
        private void IncrementarContador(Dictionary<int, ObjectInfoDTO> dictIncrementar, bool bandera, int claveDict)
        {
            if (bandera)
            {
                foreach (var item in dictIncrementar)
                {
                    if (item.Key.Equals(claveDict))
                    {
                        dictIncrementar[item.Key].RachasAparicionDespActual.Add(1);
                    }
                    else
                    {
                        dictIncrementar[item.Key].RachasAparicionDespActual.Add(0);
                    }
                }
                dictIncrementar[claveDict].PuntuacionTotal++;
            }
        }

        /// <summary>
        /// Método que valida las condiciones para ingreso de elementos a la lista
        /// </summary>
        /// <param name="dict">diccionario que contiene la información a donde se ingresan los datos</param>
        /// <param name="claveDict">clave que sirve para ingresar los datos al diccionario</param>
        /// <param name="contNegativo">Elemento que referencia al contador negativo</param>
        /// <param name="contPositivo">Elemento que referencia al contador positivo</param>
        /// <param name="caso">caso que indica a la lista a la que se agregan los elementos contados</param>
        /// <param name="anterior">Referencia al valor anterior asignado</param>
        /// <param name="comparador">Referencia al dato con el cual se compara</param>
        /// <param name="elementoAdicionar">elemento que se adicionará a la lista</param>
        private void IngresarElementoListaRachas(Dictionary<int, ObjectInfoDTO> dict, int claveDict, ref int contNegativo, ref int contPositivo, int caso, int? anterior, int comparador, int elementoAdicionar)
        {
            if (anterior.Equals(comparador) && caso == 1)
            {
                dict[claveDict].RachasAcumuladas.Add(elementoAdicionar);
                contPositivo = 0;
                contNegativo = 0;
            }
            else if (anterior.Equals(comparador) && caso == 2)
            {
                dict[claveDict].RachasAcumuladasDespActual.Add(elementoAdicionar);
                contPositivo = 0;
                contNegativo = 0;
            }
        }

        /// <summary>
        /// Método que valida las condiciones para ingreso de elementos a la lista
        /// </summary>
        /// <param name="dict">diccionario que contiene la información a donde se ingresan los datos</param>
        /// <param name="claveDict">clave que sirve para ingresar los datos al diccionario</param>
        /// <param name="contNegativo">Elemento que referencia al contador negativo</param>
        /// <param name="contPositivo">Elemento que referencia al contador positivo</param>
        /// <param name="caso">caso que indica a la lista a la que se agregan los elementos contados</param>
        /// <param name="anterior">Referencia al valor anterior asignado</param>
        /// <param name="comparador">Referencia al dato con el cual se compara</param>
        /// <param name="elementoAdicionar">elemento que se adicionará a la lista</param>
        private void IngresarElementoListaRachas(Dictionary<string, ObjectInfoDTO> dict, string claveDict, ref int contNegativo, ref int contPositivo, int caso, int? anterior, int comparador, int elementoAdicionar)
        {
            if (anterior.Equals(comparador) && caso == 1)
            {
                dict[claveDict].RachasAcumuladas.Add(elementoAdicionar);
                contPositivo = 0;
                contNegativo = 0;
            }
            else if (anterior.Equals(comparador) && caso == 2)
            {
                dict[claveDict].RachasAcumuladasDespActual.Add(elementoAdicionar);
                contPositivo = 0;
                contNegativo = 0;
            }
        }

        /// <summary>
        /// Método que retorna una estructura requerida para el caso recibido
        /// </summary>
        /// <param name="caso">opcion que sirve para controlar el tipo de objeto a devolver</param>
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
        /// Método que realiza la construcción del objeto a guardar
        /// </summary>
        /// <param name="objeto">Objeto con información a guardar</param>
        /// <returns>objeto que se va a persistir</returns>
        private DATOS_TEMP ObtenerObjetoGuardar(ObjectInfoDTO objeto)
        {
            DATOS_TEMP dt = new DATOS_TEMP();
            dt.PUNTUACIONTOTAL = objeto.PuntuacionTotal;
            dt.SINAPARECER = objeto.RachasAcumuladas.Last();
            dt.CONTADORULTIMOENRACHAS = objeto.DictRachasAgrupadasInt.Where(x => x.Key == objeto.RachasAcumuladas.Last()).FirstOrDefault().Value;
            dt.CONTADORULTIMOENRACHASDESACTUA = objeto.DictRachasAgrupadasIntDespActual.Where(x => x.Key == objeto.RachasAcumuladasDespActual.Last()).FirstOrDefault().Value;
            dt.CONTADORGENERAL = objeto.RankContadorGeneral;
            dt.CONTADORDIASEMANA = objeto.RankContadorDiaSemana;
            dt.CONTADORDIAMES = objeto.RankContadorDiaMes;
            dt.CONTADORDIAMODULO = objeto.RankContadorDiaModulo;
            dt.CONTADORMES = objeto.RankContadorMes;
            //dt.CONTADORDIAANIO = objeto.RankContadorDiaAnio;
            //dt.CONTADORDIAANIOMODULO = objeto.RankContadorDiaAnioModulo;
            //dt.CONTADORMESMODULODIAMODULO = objeto.RankContadorMesModuloDiaModulo;
            //dt.CONTADORMESDIA = objeto.RankContadorMesDia;
            //dt.CONTADORANIOMODULO = objeto.RankContadorAnioModulo;
            //dt.CONTADORMESMODULO = objeto.RankContadorMesModulo;
            dt.CONTADORDESPUESACTUAL = objeto.ContadorDespuesActual;
            //dt.CONTADORDESPUESACTUALTOTAL = (objeto.RankContadorGeneral + objeto.RankContadorDiaSemana + objeto.RankContadorDiaMes + objeto.RankContadorDiaModulo + objeto.RankContadorMes + objeto.ContadorDespuesActual);
            dt.SUMATORIAVALORES = (objeto.RankContadorGeneral + objeto.RankContadorDiaSemana + objeto.RankContadorDiaMes + objeto.RankContadorDiaModulo + objeto.RankContadorMes + objeto.ContadorDespuesActual);
            dt.CONTADORDESPUESSIGNACTUAL = objeto.ContadorDespuesSignActual;
            return dt;

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
                    int valueDia = (int)fecha.DayOfWeek == 0 ? 7 : (int)fecha.DayOfWeek;
                    return "AND TO_CHAR(fecha, 'D') = " + valueDia;
                ///Agrupa los contadores de acuerdo al día del mes
                case 2:
                    return "AND TO_CHAR(fecha, 'DD') = " + fecha.Day;
                ///Agrupa los contadores de acuerdo al día par o impar
                case 3:
                    return "AND MOD(TO_CHAR(fecha, 'DD'),2) = " + (fecha.Day % 2);
                ///Agrupa los contadores de acuerdo al mes
                case 4:
                    return "AND TO_CHAR(fecha, 'MM') = " + fecha.Month;
                /////Agrupa los contadores de acuerdo al día del año
                //case 5:
                //    return "AND TO_CHAR(fecha, 'DDD') = " + (fecha.DayOfYear);
                /////Agrupa los contadores de acuerdo al día del año par o impar
                //case 6:
                //    return "AND MOD(TO_CHAR(fecha, 'DDD'),2) = " + (fecha.DayOfYear % 2);
                /////Agrupa los contadores de acuerdo al mes par o impar
                //case 7:
                //    return "AND MOD(TO_CHAR(fecha, 'MM'),2) = " + (fecha.Month % 2);
                /////Agrupa los contadores de acuerdo al mes par o impar y el día par o impar
                //case 8:
                //    return "AND MOD(TO_CHAR(fecha, 'MM'),2) = " + (fecha.Month % 2) + " AND MOD(TO_CHAR(fecha, 'DD'),2) = " + (fecha.Day % 2);
                /////Agrupa los contadores de acuerdo al mes y al día
                //case 9:
                //    return "AND TO_CHAR(fecha, 'MM') = " + fecha.Month + " AND TO_CHAR(fecha, 'DD') = " + fecha.Day;
                /////Agrupa los contadores de acuerdo al año par o impar
                //case 10:
                //    return "AND MOD(TO_CHAR(fecha, 'YYYY'),2) = " + (fecha.Year % 2);
                default:
                    return "";
            }
        }

        /// <summary>
        /// Método que obtiene el ultimo resultado de acuerdo a los parámetros recibidos
        /// </summary>
        /// <param name="ultimoResultado"></param>
        /// <param name="posicion"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        private DATOS_TEMP ObtenerResultadoAnalizado(int posicion, int clave)
        {
            var fechaTemp = this.fecha.AddDays(-1);
            return this._astEntities.DATOS_TEMP.Where(x => x.POSICION == posicion && x.FECHA == fechaTemp && x.CLAVE == clave).FirstOrDefault();
        }

        /// <summary>
        /// Método que obtiene el ultimo resultado de acuerdo a los parámetros recibidos
        /// </summary>
        /// <param name="ultimoResultado"></param>
        /// <param name="posicion"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        private DATOS_TEMP ObtenerResultadoAnalizado(int posicion, string clave)
        {
            var fechaTemp = this.fecha.AddDays(-1);
            return this._astEntities.DATOS_TEMP.Where(x => x.POSICION == posicion && x.FECHA == fechaTemp && x.CLAVESIGN == clave).FirstOrDefault();
        }
        /// <summary>
        /// Método que obtiene y asigna los datos de los últimos resultados ingresados
        /// </summary>
        private void ObtenerUltimoResultado()
        {
            //_resultActualSol = listaDatosSol.Last();
            _resultActualLun = listaDatosLun.Last();
        }

        /// <summary>
        /// Método que obtiene el valor de la secuencia a guardar
        /// </summary>
        /// <param name="entidad">objeto que referencia a la secuencia</param>
        /// <returns>Valor obtenido para la secuencia</returns>
        private decimal ObtenerValorSecuencia(object entidad)
        {
            decimal valSecuencia = 0;
            string consultaSecuencia = "SELECT {0} FROM dual";
            string varSecuencia = string.Empty;
            if (entidad is DATOS_TEMP)
            {
                varSecuencia = "SQ_Datos_Temp.NEXTVAL";
            }
            else if (entidad is POS_UNO_DATOS)
            {
                varSecuencia = "SQ_POS_UNO_DATOS.NEXTVAL";
            }
            else if (entidad is POS_DOS_DATOS)
            {
                varSecuencia = "SQ_POS_DOS_DATOS.NEXTVAL";
            }
            else if (entidad is POS_TRES_DATOS)
            {
                varSecuencia = "SQ_POS_TRES_DATOS.NEXTVAL";
            }
            else if (entidad is POS_CUATRO_DATOS)
            {
                varSecuencia = "SQ_POS_CUATRO_DATOS.NEXTVAL";
            }
            else if (entidad is SIGN_DATOS)
            {
                varSecuencia = "SQ_SIGN_DATOS.NEXTVAL";
            }
            else if (entidad is DATOS_TEMP_DEPUR)
            {
                varSecuencia = "SQ_DATOS_TEMP_DEP.NEXTVAL";
            }
            consultaSecuencia = string.Format(consultaSecuencia, varSecuencia);
            valSecuencia = this._astEntities.Database.SqlQuery<decimal>(consultaSecuencia).ToList().Single();
            return valSecuencia;
        }

        /// <summary>
        /// Método que realiza el llamado al método que suma los datos para puntuar la información
        /// </summary>
        /// <param name="posicion">referencia la posición que se evalua dentro del registro (Pos_uno, Pos_dos...)</param>
        /// <param name="tipo">Referencia al tipo de registro que se evalua(Sol-Lun)</param>
        private void PuntuarInformacion(string posicion, Dictionary<int, ObjectInfoDTO> dict, int datoActualPosicion)
        {
            string query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(0), "ClaveNum", fechaFormat, 10);
            this.listaConsultas.Add(query_final);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContGeneral(dict, data);
            query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(1), "ClaveNum", fechaFormat, 10);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaSemana(dict, data);
            query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(2), "ClaveNum", fechaFormat, 10);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaMes(dict, data);
            //Dias pares o impares del mes
            query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(3), "ClaveNum", fechaFormat, 10);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaModulo(dict, data);
            query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(4), "ClaveNum", fechaFormat, 10);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMes(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(5), "ClaveNum", fechaFormat, 10);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContDiaAnio(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(6), "ClaveNum", fechaFormat, 10);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AdinionarInformacionContadorDiaAnioModulo(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(7), "ClaveNum", fechaFormat, 10);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContMesModulo(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(8), "ClaveNum", fechaFormat, 10);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContMesModuloDiaModulo(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(9), "ClaveNum", fechaFormat, 10);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContMesDia(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion,this.ObtenerParametrosQuery(10), "ClaveNum", fechaFormat, 10);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContAnioModulo(dict, data);
            query_final = string.Format(ConstantesConsultas.QUERY_COUNT_DATOS_DESP_ACTUAL, posicion, "ClaveNum", 10, datoActualPosicion, fechaFormat);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDespActual(dict, data);
        }

        /// <summary>
        /// Método que realiza el llamado al método que suma los datos para puntuar la información
        /// </summary>
        /// <param name="posicion">referencia la posición que se evalua dentro del registro (Pos_uno, Pos_dos...)</param>
        /// <param name="tipo">Referencia al tipo de registro que se evalua(Sol-Lun)</param>
        private void PuntuarInformacion(string posicion, Dictionary<string, ObjectInfoDTO> dict, string datoActualPosicion)
        {
            string query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(0), "ClaveSign", fechaFormat, 12);
            this.listaConsultas.Add(query_final);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContGeneral(dict, data);
            query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(1), "ClaveSign", fechaFormat, 12);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaSemana(dict, data);
            query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(2), "ClaveSign", fechaFormat, 12);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaMes(dict, data);
            //Dias pares o impares del mes
            query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(3), "ClaveSign", fechaFormat, 12);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaModulo(dict, data);
            query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(4), "ClaveSign", fechaFormat, 12);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMes(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(5), "ClaveSign", fechaFormat, 12);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContDiaAnio(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(6), "ClaveSign", fechaFormat, 12);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AdinionarInformacionContadorDiaAnioModulo(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(7), "ClaveSign", fechaFormat, 12);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContMesModulo(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(15), "ClaveSign", fechaFormat, 12);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContMesModuloDiaModulo(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(9), "ClaveSign", fechaFormat, 12);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContMesDia(dict, data);
            //query_final = string.Format(ConstantesConsultas.QUERY_BASE, posicion, this.ObtenerParametrosQuery(10), "ClaveSign", fechaFormat, 12);
            //this.listaConsultas.Add(query_final);
            //data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            //ManejoContadores.AddInfoContAnioModulo(dict, data);
            query_final = string.Format(ConstantesConsultas.QUERY_COUNT_DATOS_DESP_ACTUAL_STRING, posicion, "ClaveSign", 12, datoActualPosicion, fechaFormat);
            this.listaConsultas.Add(query_final);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDespActual(dict, data);
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
                this.IncrementarContador(dictPosUno, flagPosUno, (int)item.POS_UNO);
                this.IncrementarContador(dictPosDos, flagPosDos, (int)item.POS_DOS);
                this.IncrementarContador(dictPostres, flagPosTres, (int)item.POS_TRES);
                this.IncrementarContador(dictPosCuatro, flagPosCuatro, (int)item.POS_CUATRO);
                if (flagSign)
                {
                    dictSign[item.SIGN].PuntuacionTotal++;
                    dictPosUno[(int)item.POS_UNO].ContadorDespuesSignActual++;
                    dictPosUno[(int)item.POS_UNO].PuntuacionTotal++;
                    dictPosDos[(int)item.POS_DOS].ContadorDespuesSignActual++;
                    dictPosDos[(int)item.POS_DOS].PuntuacionTotal++;
                    dictPostres[(int)item.POS_TRES].ContadorDespuesSignActual++;
                    dictPostres[(int)item.POS_TRES].PuntuacionTotal++;
                    dictPosCuatro[(int)item.POS_CUATRO].ContadorDespuesSignActual++;
                    dictPosCuatro[(int)item.POS_CUATRO].PuntuacionTotal++;
                    foreach (var itemDict in dictSign)
                    {
                        if (itemDict.Key.Equals(item.SIGN))
                        {
                            dictSign[itemDict.Key].RachasAparicionDespActual.Add(1);
                        }
                        else
                        {
                            dictSign[itemDict.Key].RachasAparicionDespActual.Add(0);
                        }
                    }
                }
                flagPosUno = this.ValidarMismoDato(1, item, sorComparador);
                flagPosDos = this.ValidarMismoDato(2, item, sorComparador);
                flagPosTres = this.ValidarMismoDato(3, item, sorComparador);
                flagPosCuatro = this.ValidarMismoDato(4, item, sorComparador);
                flagSign = this.ValidarMismoDato(5, item, sorComparador);
            }
        }

        /// <summary>
        /// Método que realiza el recorrido de los elementos de la lista para realizar el conteo de apariciones acumuladas
        /// </summary>
        /// <param name="dict">diccionario al que se realiza el ingreso de datos</param>
        /// <param name="claveDict">Clave a la que se ingresara los datos</param>
        /// <param name="contNegativo">Referencia al contador negativo</param>
        /// <param name="contPositivo">Referencia al contador positivo</param>
        /// <param name="listaRecorrer">lista de elementos a recorrer</param>
        /// <param name="caso">caso que permite identificar a cual lista se ingresan los datos 1 lista de apariciones, 2 lista de apariciones después del actual.</param>
        private void RecorrerListaRachas(Dictionary<int, ObjectInfoDTO> dict, int claveDict, ref int contNegativo, ref int contPositivo, List<int> listaRecorrer, int caso)
        {
            int? anterior = null;
            int comparador;
            foreach (var itemList in listaRecorrer)
            {
                ///Si el valor es cero, indica que no ha caido
                if (itemList.Equals(0))
                {
                    comparador = 1;
                    this.IngresarElementoListaRachas(dict, claveDict, ref contNegativo, ref contPositivo, caso, anterior, comparador, contPositivo);
                    contNegativo--;
                }
                else
                {
                    comparador = 0;
                    ///Si el valor es uno, indica que el valor cayó
                    this.IngresarElementoListaRachas(dict, claveDict, ref contNegativo, ref contPositivo, caso, anterior, comparador, contNegativo);
                    contPositivo++;
                }
                anterior = itemList;
            }
        }

        /// <summary>
        /// Método que realiza el recorrido de los elementos de la lista para realizar el conteo de apariciones acumuladas
        /// </summary>
        /// <param name="dict">diccionario al que se realiza el ingreso de datos</param>
        /// <param name="claveDict">Clave a la que se ingresara los datos</param>
        /// <param name="contNegativo">Referencia al contador negativo</param>
        /// <param name="contPositivo">Referencia al contador positivo</param>
        /// <param name="listaRecorrer">lista de elementos a recorrer</param>
        /// <param name="caso">caso que permite identificar a cual lista se ingresan los datos 1 lista de apariciones, 2 lista de apariciones después del actual.</param>
        private void RecorrerListaRachas(Dictionary<string, ObjectInfoDTO> dict, string claveDict, ref int contNegativo, ref int contPositivo, List<int> listaRecorrer, int caso)
        {
            int? anterior = null;
            int comparador;
            foreach (var itemList in listaRecorrer)
            {
                ///Si el valor es cero, indica que no ha caido
                if (itemList.Equals(0))
                {
                    comparador = 1;
                    this.IngresarElementoListaRachas(dict, claveDict, ref contNegativo, ref contPositivo, caso, anterior, comparador, contPositivo);
                    contNegativo--;
                }
                else
                {
                    comparador = 0;
                    ///Si el valor es uno, indica que el valor cayó
                    this.IngresarElementoListaRachas(dict, claveDict, ref contNegativo, ref contPositivo, caso, anterior, comparador, contNegativo);
                    contPositivo++;
                }
                anterior = itemList;
            }
        }

        /// <summary>
        /// Método que realiza la validación de la cantidad de registros en una columna
        /// </summary>
        /// <param name="tabla">tabla de analisis de datos</param>
        /// <param name="columna">columna en la que se analizan los datos</param>
        /// <param name="cantidadValidar">cantidad de registros a validar en el rango de tiempo</param>
        /// <param name="cantidadTomar">cantidad de dias a validar</param>
        /// <returns>bandera que indica si el total de apariciones es mayor a la cantidad a validar</returns>
        private bool ValidarCantidadRegistrosColumna(string tabla, string columna, int cantidadValidar, int cantidadTomar)
        {
            string query = string.Format(ConstantesConsultas.QUERY_CONSULTA_AN_DATOS, columna, tabla, fechaFormat);
            DbRawSqlQuery<int> data = _astEntities.Database.SqlQuery<int>(query);
            List<int> listaDatos = data.AsEnumerable().Take(cantidadTomar).ToList();
            int totalApariciones = (from x in listaDatos where x == 1 select x).Count();
            return totalApariciones >= cantidadValidar;
        }

        /// <summary>
        /// Método que valida para la puntuación total, la aparicion excesiva de valores (1), dentro de la columna
        /// en la tabla de análisis de datos recibida
        /// </summary>
        /// <param name="dict">estructura que contiene la información a depurar</param>
        /// <param name="tabla">tabla en la que se analizan los datos</param>
        /// <param name="cantidadValidar">cantidad de valores que se va a validar</param>
        /// <param name="cantidadTomar">rango de tiempo en el que se valida la cantidad validar</param>
        /// <returns>estructura que contiene el resultado de los datos depurados</returns>
        private Dictionary<int, ObjectInfoDTO> ValidarMaximosMinimosPuntuacionTotal(Dictionary<int, ObjectInfoDTO> dict, string tabla, int cantidadValidar, int cantidadTomar)
        {
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            int minimo = tempDic[0].PuntuacionTotal;
            int keyMinimo = -1;
            int maximo = tempDic[0].PuntuacionTotal;
            int keyMaximo = -1;
            bool excesoDatoMax = ValidarCantidadRegistrosColumna(tabla, ConstantesGenerales.INDICA_MAX_PUNTUA_TOTAL, cantidadValidar, cantidadTomar);
            bool excesoDatoMin = ValidarCantidadRegistrosColumna(tabla, ConstantesGenerales.INDICA_MIN_PUNTUA_TOTAL, cantidadValidar, cantidadTomar);
            bool fechaEnMenoresMax = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MAX_PUNTUA_TOTAL);
            bool fechaEnMenoresMin = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_PUNTUA_TOTAL);
            if (excesoDatoMin || excesoDatoMax || fechaEnMenoresMax || fechaEnMenoresMin)
            {
                foreach (var item in dict)
                {
                    if ((excesoDatoMin && item.Value.PuntuacionTotal < minimo) || fechaEnMenoresMin)
                    {
                        minimo = item.Value.PuntuacionTotal;
                        keyMinimo = item.Key;
                    }
                    if ((excesoDatoMax && item.Value.PuntuacionTotal > maximo) || fechaEnMenoresMax)
                    {
                        maximo = item.Value.PuntuacionTotal;
                        keyMaximo = item.Key;
                    }
                }
            }
            tempDic.Remove(keyMinimo);
            tempDic.Remove(keyMaximo);
            return tempDic;
        }

        /// <summary>
        /// Método que valida para la puntuación total, la aparicion excesiva de valores (1), dentro de la columna
        /// en la tabla de análisis de datos recibida
        /// </summary>
        /// <param name="dict">estructura que contiene la información a depurar</param>
        /// <param name="tabla">tabla en la que se analizan los datos</param>
        /// <param name="cantidadValidar">cantidad de valores que se va a validar</param>
        /// <param name="cantidadTomar">rango de tiempo en el que se valida la cantidad validar</param>
        /// <returns>estructura que contiene el resultado de los datos depurados</returns>
        private Dictionary<string, ObjectInfoDTO> ValidarMaximosMinimosPuntuacionTotal(Dictionary<string, ObjectInfoDTO> dict, string tabla, int cantidadValidar, int cantidadTomar)
        {
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            int minimo = tempDic.ElementAt(0).Value.PuntuacionTotal;
            string keyMinimo = ""; int maximo = tempDic.ElementAt(0).Value.PuntuacionTotal;
            string keyMaximo = "";
            bool excesoDatoMax = ValidarCantidadRegistrosColumna(tabla, ConstantesGenerales.INDICA_MAX_PUNTUA_TOTAL, cantidadValidar, cantidadTomar);
            bool excesoDatoMin = ValidarCantidadRegistrosColumna(tabla, ConstantesGenerales.INDICA_MIN_PUNTUA_TOTAL, cantidadValidar, cantidadTomar);
            bool fechaEnMenoresMax = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MAX_PUNTUA_TOTAL);
            bool fechaEnMenoresMin = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_PUNTUA_TOTAL);
            if (excesoDatoMin || excesoDatoMax || fechaEnMenoresMax || fechaEnMenoresMin)
            {
                foreach (var item in dict)
                {
                    if ((excesoDatoMin && item.Value.PuntuacionTotal < minimo) || fechaEnMenoresMin)
                    {
                        minimo = item.Value.PuntuacionTotal;
                        keyMinimo = item.Key;
                    }
                    if ((excesoDatoMax && item.Value.PuntuacionTotal > maximo) || fechaEnMenoresMax)
                    {
                        maximo = item.Value.PuntuacionTotal;
                        keyMaximo = item.Key;
                    }
                }
            }
            tempDic.Remove(keyMinimo);
            tempDic.Remove(keyMaximo);
            return tempDic;
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
            this.ContarRachasPositivasNegativas(dictSign);
            this.AgruparRachas(dictPosUno);
            this.AgruparRachas(dictPosDos);
            this.AgruparRachas(dictPostres);
            this.AgruparRachas(dictPosCuatro);
            this.AgruparRachas(dictSign);
        }

        //private Dictionary<int, ObjectInfoDTO> ValidarIndicadores(Dictionary<int, ObjectInfoDTO> dict, string tabla)
        //{
        //    var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
        //    bool fechaEnMinUltULT_RACH = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_ULT_RACH);
        //    bool fechaEnMinCon_GENERAL = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_GENERAL);
        //    bool fechaEnMinCon_DIA_SEM = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_DIA_SEM);
        //    bool fechaEnMinCon_DIA_MES = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_DIA_MES);
        //    bool fechaEnMinCon_DIA_MOD = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_DIA_MOD);
        //    bool fechaEnMinConCONT_MES = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_MES);
        //    int indexEnMinUltULT_RACH = 10000;
        //    int indexEnMinCon_GENERAL = 10000;
        //    int indexEnMinCon_DIA_SEM = 10000;
        //    int indexEnMinCon_DIA_MES = 10000;
        //    int indexEnMinCon_DIA_MOD = 10000;
        //    int indexEnMinConCONT_MES = 10000;
        //    int keyEnMinUltULT_RACH = -1;
        //    int keyEnMinCon_GENERAL = -1;
        //    int keyEnMinCon_DIA_SEM = -1;
        //    int keyEnMinCon_DIA_MES = -1;
        //    int keyEnMinCon_DIA_MOD = -1;
        //    int keyEnMinConCONT_MES = -1;
        //    foreach (var item in dict)
        //    {
        //        if (fechaEnMinUltULT_RACH && indexEnMinUltULT_RACH > item.Value.DictRachasAgrupadasInt.Where(x => x.Key == item.Value.RachasAcumuladas.Last()).FirstOrDefault().Value)
        //        {
        //            indexEnMinUltULT_RACH = item.Value.DictRachasAgrupadasInt.Where(x => x.Key == item.Value.RachasAcumuladas.Last()).FirstOrDefault().Value;
        //            keyEnMinUltULT_RACH = item.Key;
        //        }
        //        if (fechaEnMinCon_GENERAL && indexEnMinCon_GENERAL > item.Value.ContadorGeneral)
        //        {
        //            indexEnMinCon_GENERAL = item.Value.ContadorGeneral;
        //            keyEnMinCon_GENERAL = item.Key;
        //        }
        //        if (fechaEnMinCon_DIA_SEM && indexEnMinCon_DIA_SEM > item.Value.ContadorDiaSemana)
        //        {
        //            indexEnMinCon_DIA_SEM = item.Value.ContadorDiaSemana;
        //            keyEnMinCon_DIA_SEM = item.Key;
        //        }
        //        if (fechaEnMinCon_DIA_MES && indexEnMinCon_DIA_MES > item.Value.ContadorDiaMes)
        //        {
        //            indexEnMinCon_DIA_MES = item.Value.ContadorDiaMes;
        //            keyEnMinCon_DIA_MES = item.Key;
        //        }
        //        if (fechaEnMinCon_DIA_MOD && indexEnMinCon_DIA_MOD > item.Value.ContadorDiaModulo)
        //        {
        //            indexEnMinCon_DIA_MOD = item.Value.ContadorDiaModulo;
        //            keyEnMinCon_DIA_MOD = item.Key;
        //        }
        //        if (fechaEnMinConCONT_MES && indexEnMinConCONT_MES > item.Value.ContadorMes)
        //        {
        //            indexEnMinConCONT_MES = item.Value.ContadorMes;
        //            keyEnMinConCONT_MES = item.Key;
        //        }
        //    }
        //    tempDic.Remove(keyEnMinUltULT_RACH);
        //    tempDic.Remove(keyEnMinCon_GENERAL);
        //    tempDic.Remove(keyEnMinCon_DIA_SEM);
        //    tempDic.Remove(keyEnMinCon_DIA_MES);
        //    tempDic.Remove(keyEnMinCon_DIA_MOD);
        //    tempDic.Remove(keyEnMinConCONT_MES);
        //    return tempDic;
        //}

        //private Dictionary<string, ObjectInfoDTO> ValidarIndicadores(Dictionary<string, ObjectInfoDTO> dict, string tabla)
        //{
        //    var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
        //    bool fechaEnMinUltULT_RACH = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_ULT_RACH);
        //    bool fechaEnMinCon_GENERAL = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_GENERAL);
        //    bool fechaEnMinCon_DIA_SEM = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_DIA_SEM);
        //    bool fechaEnMinCon_DIA_MES = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_DIA_MES);
        //    bool fechaEnMinCon_DIA_MOD = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_DIA_MOD);
        //    bool fechaEnMinConCONT_MES = FechaActualEnMenoresApariciones(tabla, ConstantesGenerales.INDICA_MIN_CONT_MES);
        //    int indexEnMinUltULT_RACH = 10000;
        //    int indexEnMinCon_GENERAL = 10000;
        //    int indexEnMinCon_DIA_SEM = 10000;
        //    int indexEnMinCon_DIA_MES = 10000;
        //    int indexEnMinCon_DIA_MOD = 10000;
        //    int indexEnMinConCONT_MES = 10000;
        //    string keyEnMinUltULT_RACH = "";
        //    string keyEnMinCon_GENERAL = "";
        //    string keyEnMinCon_DIA_SEM = "";
        //    string keyEnMinCon_DIA_MES = "";
        //    string keyEnMinCon_DIA_MOD = "";
        //    string keyEnMinConCONT_MES = "";
        //    string keyEnMinConP_ACTUAL = "";
        //    foreach (var item in dict)
        //    {
        //        if (fechaEnMinUltULT_RACH && indexEnMinUltULT_RACH > item.Value.DictRachasAgrupadasInt.Where(x => x.Key == item.Value.RachasAcumuladas.Last()).FirstOrDefault().Value)
        //        {
        //            indexEnMinUltULT_RACH = item.Value.DictRachasAgrupadasInt.Where(x => x.Key == item.Value.RachasAcumuladas.Last()).FirstOrDefault().Value;
        //            keyEnMinUltULT_RACH = item.Key;
        //        }
        //        if (fechaEnMinCon_GENERAL && indexEnMinCon_GENERAL > item.Value.ContadorGeneral)
        //        {
        //            indexEnMinCon_GENERAL = item.Value.ContadorGeneral;
        //            keyEnMinCon_GENERAL = item.Key;
        //        }
        //        if (fechaEnMinCon_DIA_SEM && indexEnMinCon_DIA_SEM > item.Value.ContadorDiaSemana)
        //        {
        //            indexEnMinCon_DIA_SEM = item.Value.ContadorDiaSemana;
        //            keyEnMinCon_DIA_SEM = item.Key;
        //        }
        //        if (fechaEnMinCon_DIA_MES && indexEnMinCon_DIA_MES > item.Value.ContadorDiaMes)
        //        {
        //            indexEnMinCon_DIA_MES = item.Value.ContadorDiaMes;
        //            keyEnMinCon_DIA_MES = item.Key;
        //        }
        //        if (fechaEnMinCon_DIA_MOD && indexEnMinCon_DIA_MOD > item.Value.ContadorDiaModulo)
        //        {
        //            indexEnMinCon_DIA_MOD = item.Value.ContadorDiaModulo;
        //            keyEnMinCon_DIA_MOD = item.Key;
        //        }
        //        if (fechaEnMinConCONT_MES && indexEnMinConCONT_MES > item.Value.ContadorMes)
        //        {
        //            indexEnMinConCONT_MES = item.Value.ContadorMes;
        //            keyEnMinConCONT_MES = item.Key;
        //        }
        //    }
        //    tempDic.Remove(keyEnMinUltULT_RACH);
        //    tempDic.Remove(keyEnMinCon_GENERAL);
        //    tempDic.Remove(keyEnMinCon_DIA_SEM);
        //    tempDic.Remove(keyEnMinCon_DIA_MES);
        //    tempDic.Remove(keyEnMinCon_DIA_MOD);
        //    tempDic.Remove(keyEnMinConCONT_MES);
        //    tempDic.Remove(keyEnMinConP_ACTUAL);
        //    return tempDic;
        //}

        /// <summary>
        /// Método que escribe los datos recibidos en el diccionario en los archivos
        /// </summary>
        /// <param name="dict">diccionario con datos</param>
        /// <param name="cad">cadena que hace parte del nombre del archivo a escribir</param>
        private void EscribirDatosArchivo(Dictionary<int, ObjectInfoDTO> dict, string cad)
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

        private Dictionary<string, ObjectInfoDTO> EliminarValoresMinimos(Dictionary<string, ObjectInfoDTO> dict, string tablaPosicion)
        {
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            InfoPosicionDTO infoPosicion = this.ObtenerUltimoObjetoPosicion(tablaPosicion);
            List<ContadorValorDTO> listContDiaSemana = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORDIASEMANA, infoPosicion.CONTADORDIASEMANA+"");
            List<ContadorValorDTO> listContDiaMes = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORDIAMES, infoPosicion.CONTADORDIAMES + "");
            List<ContadorValorDTO> listContDiaModulo = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORDIAMODULO, infoPosicion.CONTADORDIAMODULO + "");
            List<ContadorValorDTO> listContMes = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORMES, infoPosicion.CONTADORMES + "");
            List<ContadorValorDTO> listContDespActual = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORDESPUESACTUAL, infoPosicion.CONTADORDESPUESACTUAL + "");
            foreach (var item in dict)
            {
                if (!KeyEntreMenores(listContDiaSemana, item.Value.RankContadorDiaSemana))
                {
                    tempDic.Remove(item.Key);
                    continue;
                }
                //if (!KeyEntreMenores(listContDiaMes, item.Value.RankContadorDiaMes))
                //{
                //    tempDic.Remove(item.Key);
                //    continue;
                //}
                //if (!KeyEntreMenores(listContDiaModulo, item.Value.RankContadorDiaModulo))
                //{
                //    tempDic.Remove(item.Key);
                //    continue;
                //}
                //if (!KeyEntreMenores(listContMes, item.Value.RankContadorMes))
                //{
                //    tempDic.Remove(item.Key);
                //    continue;
                //}
                if (!KeyEntreMenores(listContDespActual, item.Value.ContadorDespuesActual))
                {
                    tempDic.Remove(item.Key);
                    continue;
                }
            }
            return tempDic;
        }

        private bool KeyEntreMenores(List<ContadorValorDTO> lista, int valorComparar)
        {
            int index = 0;
            foreach (var item in lista)
            {
                if(item.Valor == valorComparar)
                {
                    return index < 6;
                }
                index++;
            }
            return index < 6;
        }

        private Dictionary<int, ObjectInfoDTO> EliminarValoresMinimos(Dictionary<int, ObjectInfoDTO> dict, string tablaPosicion)
        {
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            InfoPosicionDTO infoPosicion = this.ObtenerUltimoObjetoPosicion(tablaPosicion);
            List<ContadorValorDTO> listContDiaSemana = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORDIASEMANA, infoPosicion.CONTADORDIASEMANA + "");
            List<ContadorValorDTO> listContDiaMes = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORDIAMES, infoPosicion.CONTADORDIAMES + "");
            List<ContadorValorDTO> listContDiaModulo = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORDIAMODULO, infoPosicion.CONTADORDIAMODULO + "");
            List<ContadorValorDTO> listContMes = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORMES, infoPosicion.CONTADORMES + "");
            List<ContadorValorDTO> listContDespActual = this.ObtenerListaContadorDespActualAgrupada(tablaPosicion, ConstantesGenerales.CONTADORDESPUESACTUAL, infoPosicion.CONTADORDESPUESACTUAL + "");
            foreach (var item in dict)
            {
                if (!KeyEntreMenores(listContDiaSemana, item.Value.RankContadorDiaSemana))
                {
                    tempDic.Remove(item.Key);
                    continue;
                }
                //if (!KeyEntreMenores(listContDiaMes, item.Value.RankContadorDiaMes))
                //{
                //    tempDic.Remove(item.Key);
                //    continue;
                //}
                //if (!KeyEntreMenores(listContDiaModulo, item.Value.RankContadorDiaModulo))
                //{
                //    tempDic.Remove(item.Key);
                //    continue;
                //}
                //if (!KeyEntreMenores(listContMes, item.Value.RankContadorMes))
                //{
                //    tempDic.Remove(item.Key);
                //    continue;
                //}
                if (!KeyEntreMenores(listContDespActual, item.Value.ContadorDespuesActual))
                {
                    tempDic.Remove(item.Key);
                    continue;
                }
            }
            return tempDic;
        }

        private List<ContadorValorDTO> ObtenerListaContadorDespActualAgrupada(string tabla, string columna, string datoActual)
        {
            string query = string.Format(ConstantesConsultas.QUERY_COUNT_DATOS_DESP_ACTUAL_COLUMNA, columna, tabla, datoActual, this.fechaFormat);
            DbRawSqlQuery<ContadorValorDTO> data = _astEntities.Database.SqlQuery<ContadorValorDTO>(query);
            return data.AsEnumerable().ToList();
        }
        
        private InfoPosicionDTO ObtenerUltimoObjetoPosicion(string tablaPosicion)
        {
            DateTime fechaAnterior = this.fecha.AddDays(-1);
            string fechaAntFormat = fechaAnterior.Day + "/" + fechaAnterior.Month + "/" + fechaAnterior.Year;
            string query = string.Format(ConstantesConsultas.QUERY_DATOS_ANTERIOR_POSICION, tablaPosicion, fechaAntFormat);
            DbRawSqlQuery<InfoPosicionDTO> data = _astEntities.Database.SqlQuery<InfoPosicionDTO>(query);
            return data.AsEnumerable().ToList().ElementAt(0);
        }

        private void EscribirConsultas()
        {
            string fic = path + fecha.Day+".txt";
            StreamWriter sw = new StreamWriter(fic);
            sw.WriteLine(ConstantesGenerales.ENCABEZADOS);
            foreach (var item in this.listaConsultas)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }

        private List<int> ObtenerValoresSinAparecer(string tablaPosicion)
        {
            string fechaAntFormat = this.fecha.Day + "/" + this.fecha.Month + "/" + this.fecha.Year;
            string query = string.Format(ConstantesConsultas.QUERY_DATOS_SIN_APARECER, tablaPosicion, fechaAntFormat);
            DbRawSqlQuery<ContadorValorDTO> data = _astEntities.Database.SqlQuery<ContadorValorDTO>(query);
            return (from x in data.AsEnumerable() select x.Valor).Take(25).ToList();
        }

        private Dictionary<int, ObjectInfoDTO> ValidarSinAparecer(Dictionary<int, ObjectInfoDTO> dict, string tablaPosicion)
        {
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            List<int> lista = this.ObtenerValoresSinAparecer(tablaPosicion);
            foreach (var item in dict)
            {
                if (lista.IndexOf(item.Value.RachasAcumuladas.Last()) == -1)
                {
                    tempDic.Remove(item.Key);
                }
            }
            return tempDic;
        }

        private Dictionary<string, ObjectInfoDTO> ValidarSinAparecer(Dictionary<string, ObjectInfoDTO> dict, string tablaPosicion)
        {
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            List<int> lista = this.ObtenerValoresSinAparecer(tablaPosicion);
            foreach (var item in dict)
            {
                if (lista.IndexOf(item.Value.RachasAcumuladas.Last()) == -1)
                {
                    tempDic.Remove(item.Key);
                }
            }
            return tempDic;
        }

        private Dictionary<int, ObjectInfoDTO> ValidarAparicionesDespActual(Dictionary<int, ObjectInfoDTO> dict, string tablaPosicion)
        {
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            InfoPosicionDTO infoPosicion = this.ObtenerUltimoObjetoPosicion(tablaPosicion);
            List<QueryInfo> listContadorDiaSemana = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIASEMANA, tablaPosicion, infoPosicion.CONTADORDIASEMANA);
            List<QueryInfo> listContadorDiaMes = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIAMES, tablaPosicion, infoPosicion.CONTADORDIAMES);
            List<QueryInfo> listContadorDiaModulo = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIAMODULO, tablaPosicion, infoPosicion.CONTADORDIAMODULO);
            List<QueryInfo> listContadorMes = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORMES, tablaPosicion, infoPosicion.CONTADORMES);
            List<QueryInfo> listContadorDespActual = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDESPUESACTUAL, tablaPosicion, infoPosicion.CONTADORDESPUESACTUAL);
            List<QueryInfo> listSumatoriaDatos = this.ConsultarSumatoriaDatos(tablaPosicion);
            
            foreach (var item in dict)
            {
                int total = item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual;
                if (!this.ValidarIndex(listContadorDiaSemana, item.Value.RankContadorDiaSemana) ||
                    !this.ValidarIndex(listContadorDiaMes, item.Value.RankContadorDiaMes) ||
                    !this.ValidarIndex(listContadorDiaModulo, item.Value.RankContadorDiaModulo) ||
                    !this.ValidarIndex(listContadorMes, item.Value.RankContadorMes) ||
                    !this.ValidarIndex(listContadorDespActual, item.Value.ContadorDespuesActual) //||
                    //!this.ValidarIndex(listSumatoriaDatos, total)
                    )
                {
                    tempDic.Remove(item.Key);
                }
            }
            return tempDic;
        }

        private bool ValidarIndex(List<QueryInfo> lista, int valueComparar)
        {
            int index = -1;
            for (var i=0; i<lista.Count();i++)
            {
                if(lista.ElementAt(i).ClaveNum == valueComparar)
                {
                    index = i;
                    break;
                }
            }
                return index != -1;
        }

        private Dictionary<string, ObjectInfoDTO> ValidarAparicionesDespActual(Dictionary<string, ObjectInfoDTO> dict, string tablaPosicion)
        {
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            InfoPosicionDTO infoPosicion = this.ObtenerUltimoObjetoPosicion(tablaPosicion);
            List<QueryInfo> listContadorDiaSemana = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIASEMANA, tablaPosicion, infoPosicion.CONTADORDIASEMANA);
            List<QueryInfo> listContadorDiaMes = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIAMES, tablaPosicion, infoPosicion.CONTADORDIAMES);
            List<QueryInfo> listContadorDiaModulo = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIAMODULO, tablaPosicion, infoPosicion.CONTADORDIAMODULO);
            List<QueryInfo> listContadorMes = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORMES, tablaPosicion, infoPosicion.CONTADORMES);
            List<QueryInfo> listContadorDespActual = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDESPUESACTUAL, tablaPosicion, infoPosicion.CONTADORDESPUESACTUAL);
            List<QueryInfo> listSumatoriaDatos = this.ConsultarSumatoriaDatos(tablaPosicion);
            foreach (var item in dict)
            {
                int total = item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual;
                if (!this.ValidarIndex(listContadorDiaSemana, item.Value.RankContadorDiaSemana) ||
                    !this.ValidarIndex(listContadorDiaMes, item.Value.RankContadorDiaMes) ||
                    !this.ValidarIndex(listContadorDiaModulo, item.Value.RankContadorDiaModulo) ||
                    !this.ValidarIndex(listContadorMes, item.Value.RankContadorMes) ||
                    !this.ValidarIndex(listContadorDespActual, item.Value.ContadorDespuesActual) //||
                    //!this.ValidarIndex(listSumatoriaDatos, total)
                    )
                {
                    tempDic.Remove(item.Key);
                }
            }
            return tempDic;
        }

        /// <summary>
        /// Método que realiza la consulta de los valores agrupados para las coincidencias de un valor siguiente
        /// </summary>
        /// <param name="columna">Columna de la tabla sobre la que se realiza la consulta</param>
        /// <param name="tablaValidar">Tabla para validar los datos</param>
        /// <param name="valorComparar">Valor sobre el que se realiza la comparacion</param>
        /// <returns></returns>
        private List<QueryInfo> AgruparContadoresDespuesActual(string columna, string tablaValidar, int valorComparar)
        {
            string fechaFormat = this.fecha.Day + "/" + this.fecha.Month + "/" + this.fecha.Year;
            string consulta = string.Format(ConstantesConsultas.QUERY_AGRUPAR_APARICIONES_DESP_ACTUAL, columna, tablaValidar, valorComparar, fechaFormat);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(consulta);
            List<QueryInfo> lista = data.AsEnumerable().ToList();
            return data.AsEnumerable().Take(7).ToList();
        }

        private List<QueryInfo> ConsultarSumatoriaDatos(string tabla)
        {
            string fechaFormat = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            string consulta = string.Format(ConstantesConsultas.QUERY_SUMATORIA_DATOS, tabla, fechaFormat);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(consulta);
            int totalDatos = (data.AsEnumerable().Count() * 90) / 100;
            return data.AsEnumerable().Take(totalDatos).ToList();
        }

        private Dictionary<string, ObjectInfoDTO> ValidarIndicadoresPosicion(Dictionary<string, ObjectInfoDTO> dict, string tabla)
        {
            string fechaFormat = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            InfoPosicionDTO infoPosicion = this.ObtenerUltimoObjetoPosicion(ConstantesGenerales.SIGN_DATOS);
            bool fechaNoSinAparecer = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_SIN_APARECER, 1);
            bool fechaNoUltRach = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_ULT_RACH, 1);
            bool fechaNoComparaUltRachas = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.COMPARA_ULT_RACH, 0);
            bool fechaNoMinContGeneral = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_CONT_GENERAL, 1);
            bool fechaNoComparaContGeneral = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.COMPARA_CONT_GENERAL, 0);
            bool fechaNoMinContDiaSemana = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_CONT_DIA_SEM, 1);
            bool fechaNoComparaContDiaSemana = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.COMPARA_CONT_DIA_SEM, 0);
            bool fechaNoIndicaMinDiaMes = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_CONT_DIA_MES, 1);
            bool fechaNoComparaContDiaMes = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.COMPARA_CONT_DIA_MES, 0);
            bool fechaNoIndicaMinDiaMod = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_CONT_DIA_MOD, 1);
            bool fechaNoComparaContDiaMod = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.COMPARA_CONT_DIA_MOD, 0);
            bool fechaNoIndicaMinMes = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_CONT_MES, 1);
            bool fechaNoComparaContMes = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.COMPARA_CONT_MES, 0);
            bool fechaNoIndicaMinDespActual = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_CONT_DESP_ACTUAL, 1);
            bool fechaNoComparaContDespActual = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.COMPARA_CONT_DESP_ACTUAL, 0);
            bool fechaNoMinPuntuaTotal = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_PUNTUA_TOTAL, 1);
            bool fechaNoMaxPuntuaTotal = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MAX_PUNTUA_TOTAL, 1);
            bool fechaNoMinSumatoria = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MIN_SUMATORIA, 1);
            bool fechaNoMaxSumatoria = this.ObtenerBanderaIndicador(tabla, fechaFormat, ConstantesGenerales.INDICA_MAX_SUMATORIA, 1);

            List<InfoEliminacionDTO> listaEliminar = new List<InfoEliminacionDTO>();
            List<string> itemsEliminar = new List<string>();

            List<string> llavesIndicadores = new List<string>();
            string indicaMinSinAparecer = "indicaMinSinAparecer";
            string indicaMinUltRach = "indicaMinUltRach";
            string comparaUltRachas = "comparaUltRachas";
            string indicaMinContGeneral = "indicaMinContGeneral";
            string indicaComparaContGeneral = "indicaComparaContGeneral";
            string indicaMinContDiaSemana = "indicaMinContDiaSemana";
            string indicaComparaContDiaSemana = "indicaComparaContDiaSemana";
            string indicaMinDiaMes = "indicaMinDiaMes";
            string indicaComparaContDiaMes = "indicaComparaContDiaMes";
            string indicaMinDiaMod = "indicaMinDiaMod";
            string indicaComparaContDiaMod = "indicaComparaContDiaMod";
            string indicaMinMes = "indicaMinMes";
            string indicaComparaContMes = "indicaComparaContMes";
            string indicaMinDespActual = "indicaMinDespActual";
            string indicaComparaContDespActual = "indicaComparaContDespActual";
            string indicaMinPuntuaTotal = "indicaMinPuntuaTotal";
            string indicaMaxPuntuaTotal = "indicaMaxPuntuaTotal";
            string indicaMinSumatoria = "indicaMinSumatoria";
            string indicaMaxSumatoria = "indicaMaxSumatoria";

            llavesIndicadores.Add(indicaMinSinAparecer);
            llavesIndicadores.Add(indicaMinUltRach);
            llavesIndicadores.Add(comparaUltRachas);
            llavesIndicadores.Add(indicaMinContGeneral);
            llavesIndicadores.Add(indicaComparaContGeneral);
            llavesIndicadores.Add(indicaMinContDiaSemana);
            llavesIndicadores.Add(indicaComparaContDiaSemana);
            llavesIndicadores.Add(indicaMinDiaMes);
            llavesIndicadores.Add(indicaComparaContDiaMes);
            llavesIndicadores.Add(indicaMinDiaMod);
            llavesIndicadores.Add(indicaComparaContDiaMod);
            llavesIndicadores.Add(indicaMinMes);
            llavesIndicadores.Add(indicaComparaContMes);
            llavesIndicadores.Add(indicaMinDespActual);
            llavesIndicadores.Add(indicaComparaContDespActual);
            llavesIndicadores.Add(indicaMinPuntuaTotal);
            llavesIndicadores.Add(indicaMaxPuntuaTotal);
            llavesIndicadores.Add(indicaMinSumatoria);
            llavesIndicadores.Add(indicaMaxSumatoria);

            if (fechaNoSinAparecer 
                || fechaNoUltRach 
                || fechaNoComparaUltRachas 
                || fechaNoMinContGeneral 
                || fechaNoComparaContGeneral
                || fechaNoMinContDiaSemana 
                || fechaNoComparaContDiaSemana 
                || fechaNoIndicaMinDiaMes
                || fechaNoComparaContDiaMes 
                || fechaNoIndicaMinDiaMod  
                || fechaNoComparaContDiaMod
                || fechaNoIndicaMinMes 
                || fechaNoComparaContMes 
                || fechaNoIndicaMinDespActual 
                || fechaNoComparaContDespActual
                || fechaNoMinPuntuaTotal
                || fechaNoMaxPuntuaTotal
                || fechaNoMinSumatoria
                || fechaNoMaxSumatoria)
            {
                var menorSinAparecer = dict.First().Value.RachasAcumuladas.Last();
                var menorUltRachas = dict.First().Value.RachasAcumuladas.Last();
                var menorContGeneral = dict.First().Value.RankContadorGeneral;
                var menorContDiaSemana = dict.First().Value.RankContadorDiaSemana;
                var menorContDiaMes = dict.First().Value.RankContadorDiaMes;
                var menorDiaMod = dict.First().Value.RankContadorDiaModulo;
                var menorMes = dict.First().Value.RankContadorMes;
                var menorDespActual = dict.First().Value.ContadorDespuesActual;
                var menorPuntuaTotal = dict.First().Value.PuntuacionTotal;
                var mayorPuntuaTotal = dict.First().Value.PuntuacionTotal;
                var menorSumatoria = dict.First().Value.RankContadorGeneral +
                    dict.First().Value.RankContadorDiaSemana +
                    dict.First().Value.RankContadorDiaMes +
                    dict.First().Value.RankContadorDiaModulo +
                    dict.First().Value.RankContadorMes +
                    dict.First().Value.ContadorDespuesActual;
                var mayorSumatoria = dict.First().Value.RankContadorGeneral +
                    dict.First().Value.RankContadorDiaSemana +
                    dict.First().Value.RankContadorDiaMes +
                    dict.First().Value.RankContadorDiaModulo +
                    dict.First().Value.RankContadorMes +
                    dict.First().Value.ContadorDespuesActual;
                foreach (var item in dict)
                {
                    int sumatoria = item.Value.RankContadorGeneral + 
                        item.Value.RankContadorDiaSemana +
                        item.Value.RankContadorDiaMes +
                        item.Value.RankContadorDiaModulo +
                        item.Value.RankContadorMes +
                        item.Value.ContadorDespuesActual;
                    menorSinAparecer = this.AdicionarElementoEliminar(fechaNoSinAparecer, item.Value.RachasAcumuladas.Last(), menorSinAparecer, indicaMinSinAparecer, item.Key, false, listaEliminar);
                    menorUltRachas = this.AdicionarElementoEliminar(fechaNoUltRach, item.Value.RachasAcumuladas.Last(), menorUltRachas, indicaMinUltRach, item.Key, false, listaEliminar);
                    this.AdicionarElementoEliminar(fechaNoComparaUltRachas, item.Value.CONTADORULTIMOENRACHAS, infoPosicion.CONTADORULTIMOENRACHAS, comparaUltRachas, item.Key, true, listaEliminar);
                    //menorContGeneral = this.AdicionarElementoEliminar(fechaNoMinContGeneral, item.Value.RankContadorGeneral, menorContGeneral, indicaMinContGeneral, item.Key, false, listaEliminar);
                    //this.AdicionarElementoEliminar(fechaNoComparaContGeneral, item.Value.RankContadorGeneral, infoPosicion.CONTADORGENERAL, indicaComparaContGeneral, item.Key, true, listaEliminar);
                    menorContDiaSemana = this.AdicionarElementoEliminar(fechaNoMinContDiaSemana, item.Value.RankContadorDiaSemana, menorContDiaSemana, indicaMinContDiaSemana, item.Key, false, listaEliminar);
                    this.AdicionarElementoEliminar(fechaNoComparaContDiaSemana, item.Value.RankContadorDiaSemana, infoPosicion.CONTADORDIASEMANA, indicaComparaContDiaSemana, item.Key, true, listaEliminar);
                    menorContDiaMes = this.AdicionarElementoEliminar(fechaNoIndicaMinDiaMes, item.Value.RankContadorDiaMes, menorContDiaMes, indicaMinDiaMes, item.Key, false, listaEliminar);
                    this.AdicionarElementoEliminar(fechaNoComparaContDiaMes, item.Value.RankContadorDiaMes, infoPosicion.CONTADORDIAMES, indicaComparaContDiaMes, item.Key, true, listaEliminar);
                    menorDiaMod = this.AdicionarElementoEliminar(fechaNoIndicaMinDiaMod, item.Value.RankContadorDiaModulo, menorDiaMod, indicaMinDiaMod, item.Key, false, listaEliminar);
                    this.AdicionarElementoEliminar(fechaNoComparaContDiaMod, item.Value.RankContadorDiaModulo, infoPosicion.CONTADORDIAMODULO, indicaComparaContDiaMod, item.Key, true, listaEliminar);
                    menorMes = this.AdicionarElementoEliminar(fechaNoIndicaMinMes, item.Value.RankContadorMes, menorMes, indicaMinMes, item.Key, false, listaEliminar);
                    this.AdicionarElementoEliminar(fechaNoComparaContMes, item.Value.RankContadorMes, infoPosicion.CONTADORMES, indicaComparaContMes, item.Key, true, listaEliminar);
                    menorDespActual = this.AdicionarElementoEliminar(fechaNoIndicaMinDespActual, item.Value.ContadorDespuesActual, menorDespActual, indicaMinDespActual, item.Key, false, listaEliminar);
                    this.AdicionarElementoEliminar(fechaNoComparaContDespActual, item.Value.ContadorDespuesActual, infoPosicion.CONTADORDESPUESACTUAL, indicaComparaContDespActual, item.Key, true, listaEliminar);
                    menorPuntuaTotal = this.AdicionarElementoEliminar(fechaNoMinPuntuaTotal, item.Value.PuntuacionTotal, menorPuntuaTotal, indicaMinPuntuaTotal, item.Key, false, listaEliminar);
                    mayorPuntuaTotal = this.AdicionarElementoEliminar(fechaNoMaxPuntuaTotal, item.Value.PuntuacionTotal, mayorPuntuaTotal, indicaMaxPuntuaTotal, item.Key, false, listaEliminar, true);
                    menorSumatoria = this.AdicionarElementoEliminar(fechaNoMinSumatoria, sumatoria, menorSumatoria, indicaMinSumatoria, item.Key, false, listaEliminar);
                    mayorSumatoria = this.AdicionarElementoEliminar(fechaNoMaxSumatoria, sumatoria, mayorSumatoria, indicaMaxSumatoria, item.Key, false, listaEliminar, true);

                }
            }
            if (listaEliminar.Count() > 0)
            {
                foreach (var i in llavesIndicadores)
                {
                    List<InfoEliminacionDTO> subList = (from x in listaEliminar where x.LlaveAnalisis == i select x).ToList();
                    if (subList.Count() > 0)
                    {
                        int min = (from x in subList select x.Valor).Min();
                        itemsEliminar.AddRange((from x in subList where x.Valor == min select x.LlaveEliminarString).ToList());
                    }
                }
            }
            var keysToInclude = dict.Keys.Except(itemsEliminar).ToList();
            var tempDic = (from entry in dict where keysToInclude.IndexOf(entry.Key) != -1 select entry).ToDictionary(x => x.Key, x => x.Value);
            return tempDic;
        }

        private int AdicionarElementoEliminar(bool banderaCondicion, int valorComparado, int valorComparador, string claveElemento, string claveDict, bool esComparar, List<InfoEliminacionDTO> listaEliminar, bool validarMayor =false)
        {
            if (esComparar)
            {
                if (banderaCondicion && valorComparado == valorComparador)
                {
                    InfoEliminacionDTO obj = new InfoEliminacionDTO();
                    obj.LlaveAnalisis = claveElemento;
                    obj.LlaveEliminarString = claveDict;
                    obj.Valor = valorComparador;
                    listaEliminar.Add(obj);
                }

            }
            else
            {
                if (!validarMayor)
                {
                    if (banderaCondicion && valorComparado < valorComparador)
                    {
                        InfoEliminacionDTO obj = new InfoEliminacionDTO();
                        obj.LlaveAnalisis = claveElemento;
                        obj.LlaveEliminarString = claveDict;
                        valorComparador = valorComparado;
                        obj.Valor = valorComparador;
                        listaEliminar.Add(obj);
                    }
                }
                else
                {
                    if (banderaCondicion && valorComparado > valorComparador)
                    {
                        InfoEliminacionDTO obj = new InfoEliminacionDTO();
                        obj.LlaveAnalisis = claveElemento;
                        obj.LlaveEliminarString = claveDict;
                        valorComparador = valorComparado;
                        obj.Valor = valorComparador;
                        listaEliminar.Add(obj);
                    }
                }
                
            }
            return valorComparador;
        }

        private bool ObtenerBanderaIndicador(string tabla, string fechaFormat, string columna, int valorComparar)
        {
            return AnalisisDatosPorPosicion.ValidarIndicadorPosicion(_astEntities, columna, tabla, fechaFormat, valorComparar).IndexOf(this.fecha.Day) == -1;
        }
    }
}