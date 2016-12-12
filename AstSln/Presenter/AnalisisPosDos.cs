using Constantes;
using DTOs;
using Model.DataContextModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    public class AnalisisPosDos
    {
        /// <summary>
        /// /// Método que realiza el análisis de los indicadores de la posicion del diccionario de datos
        /// </summary>
        /// <param name="_astEntities">Objeto que instancia la bd para la realización de las consultas</param>
        /// <param name="dict">estructura que contiene la información a analizar</param>
        /// <param name="infoPosicion">Objeto que contiene la última información para la posición</param>
        /// <param name="fechaFormat">Cadena que contiene la fecha formateada</param>
        /// <param name="dia">dia a analizar</param>
        /// <returns>Estrucutra con datos depurados</returns>
        public static Dictionary<int, ObjectInfoDTO> ValidarIndicadoresPosicion(AstEntities _astEntities, Dictionary<int, ObjectInfoDTO> dict, InfoPosicionDTO infoPosicion, string fechaFormat, int dia)
        {
            //bool fechaNoSinAparecer = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_SIN_APARECER, 1, fechaFormat, dia);
            //bool fechaNoUltRach = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_ULT_RACH, 1, fechaFormat, dia);
            bool fechaNoComparaUltRachas = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.COMPARA_ULT_RACH, 0, fechaFormat, dia);
            bool fechaNoMinContGeneral = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_CONT_GENERAL, 1, fechaFormat, dia);
            bool fechaNoComparaContGeneral = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.COMPARA_CONT_GENERAL, 0, fechaFormat, dia);
            //bool fechaNoMinContDiaSemana = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_CONT_DIA_SEM, 1, fechaFormat, dia);
            //bool fechaNoComparaContDiaSemana = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.COMPARA_CONT_DIA_SEM, 0, fechaFormat, dia);
            //bool fechaNoIndicaMinDiaMes = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_CONT_DIA_MES, 1, fechaFormat, dia);
            bool fechaNoComparaContDiaMes = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.COMPARA_CONT_DIA_MES, 0, fechaFormat, dia);
            //bool fechaNoIndicaMinDiaMod = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_CONT_DIA_MOD, 1, fechaFormat, dia);
            bool fechaNoComparaContDiaMod = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.COMPARA_CONT_DIA_MOD, 0, fechaFormat, dia);
            //bool fechaNoIndicaMinMes = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_CONT_MES, 1, fechaFormat, dia);
            //bool fechaNoComparaContMes = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.COMPARA_CONT_MES, 0, fechaFormat, dia);
            bool fechaNoIndicaMinDespActual = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_CONT_DESP_ACTUAL, 1, fechaFormat, dia);
            //bool fechaNoComparaContDespActual = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.COMPARA_CONT_DESP_ACTUAL, 0, fechaFormat, dia);
            //bool fechaNoMinPuntuaTotal = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_PUNTUA_TOTAL, 1, fechaFormat, dia);
            bool fechaNoMaxPuntuaTotal = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MAX_PUNTUA_TOTAL, 1, fechaFormat, dia);
            //bool fechaNoMinSumatoria = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MIN_SUMATORIA, 1, fechaFormat, dia);
            //bool fechaNoMaxSumatoria = AnalisisPosicionGeneral.ObtenerBanderaIndicador(_astEntities, ConstantesGenerales.AN_DAT_POS_DOS, ConstantesGenerales.INDICA_MAX_SUMATORIA, 1, fechaFormat, dia);

            List<InfoEliminacionDTO> listaEliminar = new List<InfoEliminacionDTO>();
            List<string> llavesIndicadores = AnalisisPosicionGeneral.ListaIndicadores();

            if (
                //fechaNoSinAparecer
                //||fechaNoUltRach
                fechaNoComparaUltRachas
                || fechaNoMinContGeneral
                || fechaNoComparaContGeneral
                //|| fechaNoMinContDiaSemana
                //|| fechaNoComparaContDiaSemana
                //|| fechaNoIndicaMinDiaMes
                || fechaNoComparaContDiaMes
                //|| fechaNoIndicaMinDiaMod
                || fechaNoComparaContDiaMod
                //|| fechaNoIndicaMinMes
                //|| fechaNoComparaContMes
                || fechaNoIndicaMinDespActual
                //|| fechaNoComparaContDespActual
                //|| fechaNoMinPuntuaTotal
                || fechaNoMaxPuntuaTotal
                //|| fechaNoMinSumatoria
                //|| fechaNoMaxSumatoria
                )
            {
                var menorSinAparecer = dict.First().Value.RachasAcumuladas.Last();
                var menorUltRachas = dict.First().Value.DictRachasAgrupadasInt[dict.First().Value.RachasAcumuladas.Last()];
                var menorContGeneral = dict.First().Value.RankContadorGeneral;
                var menorContDiaSemana = dict.First().Value.RankContadorDiaSemana;
                var menorContDiaMes = dict.First().Value.RankContadorDiaMes;
                var menorDiaMod = dict.First().Value.RankContadorDiaModulo;
                var menorMes = dict.First().Value.RankContadorMes;
                var menorDespActual = dict.First().Value.ContadorDespuesActual;
                var menorPuntuaTotal = dict.First().Value.PuntuacionTotal;
                var mayorPuntuaTotal = dict.First().Value.PuntuacionTotal;
                var menorSumatoria = dict.First().Value.RankContadorDiaSemana +
                    dict.First().Value.RankContadorDiaMes +
                    dict.First().Value.RankContadorDiaModulo +
                    dict.First().Value.RankContadorMes +
                    dict.First().Value.ContadorDespuesActual;
                var mayorSumatoria = dict.First().Value.RankContadorDiaSemana +
                    dict.First().Value.RankContadorDiaMes +
                    dict.First().Value.RankContadorDiaModulo +
                    dict.First().Value.RankContadorMes +
                    dict.First().Value.ContadorDespuesActual;
                foreach (var item in dict)
                {
                    int sumatoria = item.Value.RankContadorDiaSemana +
                        item.Value.RankContadorDiaMes +
                        item.Value.RankContadorDiaModulo +
                        item.Value.RankContadorMes +
                        item.Value.ContadorDespuesActual;
                    int lastUltimoRachas = item.Value.DictRachasAgrupadasInt[item.Value.RachasAcumuladas.Last()];
                    //menorSinAparecer = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoSinAparecer, item.Value.RachasAcumuladas.Last(), menorSinAparecer, ConstantesGenerales.INDICAMINSINAPARECER, item.Key, false, listaEliminar);
                    //menorUltRachas = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoUltRach, item.Value.RachasAcumuladas.Last(), menorUltRachas, ConstantesGenerales.INDICAMINULTRACH, item.Key, false, listaEliminar);
                    AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoComparaUltRachas, item.Value.CONTADORULTIMOENRACHAS, infoPosicion.CONTADORULTIMOENRACHAS, ConstantesGenerales.COMPARAULTRACHAS, item.Key, true, listaEliminar);
                    menorContGeneral = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoMinContGeneral, item.Value.RankContadorGeneral, menorContGeneral, ConstantesGenerales.INDICAMINCONTGENERAL, item.Key, false, listaEliminar);
                    AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoComparaContGeneral, item.Value.RankContadorGeneral, infoPosicion.CONTADORGENERAL, ConstantesGenerales.INDICACOMPARACONTGENERAL, item.Key, true, listaEliminar);
                    //menorContDiaSemana = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoMinContDiaSemana, item.Value.RankContadorDiaSemana, menorContDiaSemana, ConstantesGenerales.INDICAMINCONTDIASEMANA, item.Key, false, listaEliminar);
                    //AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoComparaContDiaSemana, item.Value.RankContadorDiaSemana, infoPosicion.CONTADORDIASEMANA, ConstantesGenerales.INDICACOMPARACONTDIASEMANA, item.Key, true, listaEliminar);
                    //menorContDiaMes = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoIndicaMinDiaMes, item.Value.RankContadorDiaMes, menorContDiaMes, ConstantesGenerales.INDICAMINDIAMES, item.Key, false, listaEliminar);
                    AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoComparaContDiaMes, item.Value.RankContadorDiaMes, infoPosicion.CONTADORDIAMES, ConstantesGenerales.INDICACOMPARACONTDIAMES, item.Key, true, listaEliminar);
                    //menorDiaMod = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoIndicaMinDiaMod, item.Value.RankContadorDiaModulo, menorDiaMod, ConstantesGenerales.INDICAMINDIAMOD, item.Key, false, listaEliminar);
                    AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoComparaContDiaMod, item.Value.RankContadorDiaModulo, infoPosicion.CONTADORDIAMODULO, ConstantesGenerales.INDICACOMPARACONTDIAMOD, item.Key, true, listaEliminar);
                    //menorMes = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoIndicaMinMes, item.Value.RankContadorMes, menorMes, ConstantesGenerales.INDICAMINMES, item.Key, false, listaEliminar);
                    //AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoComparaContMes, item.Value.RankContadorMes, infoPosicion.CONTADORMES, ConstantesGenerales.INDICACOMPARACONTMES, item.Key, true, listaEliminar);
                    menorDespActual = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoIndicaMinDespActual, item.Value.ContadorDespuesActual, menorDespActual, ConstantesGenerales.INDICAMINDESPACTUAL, item.Key, false, listaEliminar);
                    //AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoComparaContDespActual, item.Value.ContadorDespuesActual, infoPosicion.CONTADORDESPUESACTUAL, ConstantesGenerales.INDICACOMPARACONTDESPACTUAL, item.Key, true, listaEliminar);
                    //menorPuntuaTotal = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoMinPuntuaTotal, item.Value.PuntuacionTotal, menorPuntuaTotal, ConstantesGenerales.INDICAMINPUNTUATOTAL, item.Key, false, listaEliminar);
                    mayorPuntuaTotal = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoMaxPuntuaTotal, item.Value.PuntuacionTotal, mayorPuntuaTotal, ConstantesGenerales.INDICAMAXPUNTUATOTAL, item.Key, false, listaEliminar, true);
                    //menorSumatoria = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoMinSumatoria, sumatoria, menorSumatoria, ConstantesGenerales.INDICAMINSUMATORIA, item.Key, false, listaEliminar);
                    //mayorSumatoria = AnalisisPosicionGeneral.AdicionarElementoEliminar(fechaNoMaxSumatoria, sumatoria, mayorSumatoria, ConstantesGenerales.INDICAMAXSUMATORIA, item.Key, false, listaEliminar, true);
                }
            }
            return AnalisisPosicionGeneral.RemoverElementosListaDiccionario(dict, listaEliminar, llavesIndicadores);
        }
    }
}
