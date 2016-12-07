using Constantes;
using DTOs;
using Model.DataContextModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    public class AnalisisDatosPorPosicion
    {
        public static Dictionary<string, ObjectInfoDTO> ValidarInfoPorAnalisisAtributos(Dictionary<string, ObjectInfoDTO> dict, AstEntities _astEntities, string fecha)
        {

            List<int> listCONTADORDIASEMANA = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA_SIGN, ConstantesGenerales.CONTADORDIASEMANA, ConstantesGenerales.SIGN_DATOS, fecha);
            List<int> listCONTADORDIAMES = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA_SIGN, ConstantesGenerales.CONTADORDIAMES, ConstantesGenerales.SIGN_DATOS, fecha);
            List<int> listCONTADORDIAMODULO = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA_SIGN, ConstantesGenerales.CONTADORDIAMODULO, ConstantesGenerales.SIGN_DATOS, fecha);
            List<int> listCONTADORMES = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA_SIGN, ConstantesGenerales.CONTADORDIAMES, ConstantesGenerales.SIGN_DATOS, fecha);
            List<int> listCONTADORDESPUESACTUAL = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA_SIGN, ConstantesGenerales.CONTADORDESPUESACTUAL, ConstantesGenerales.SIGN_DATOS, fecha);
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in dict)
            {
                bool cumpleCondiciones = true;
                cumpleCondiciones &= listCONTADORDIASEMANA.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                cumpleCondiciones &= listCONTADORDIAMES.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                cumpleCondiciones &= listCONTADORDIAMODULO.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                cumpleCondiciones &= listCONTADORMES.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                cumpleCondiciones &= listCONTADORDESPUESACTUAL.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                if (!cumpleCondiciones)
                {
                    tempDic.Remove(item.Key);
                }
            }
            return tempDic;
        }

        public static Dictionary<int, ObjectInfoDTO> ValidarInfoPorAnalisisAtributos(Dictionary<int, ObjectInfoDTO> dict, AstEntities _astEntities, string tabla, string fecha)
        {

            List<int> listCONTADORDIASEMANA = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA, ConstantesGenerales.CONTADORDIASEMANA, tabla, fecha);
            List<int> listCONTADORDIAMES = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA, ConstantesGenerales.CONTADORDIAMES, tabla, fecha);
            List<int> listCONTADORDIAMODULO = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA, ConstantesGenerales.CONTADORDIAMODULO, tabla, fecha);
            List<int> listCONTADORMES = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA, ConstantesGenerales.CONTADORDIAMES, tabla, fecha);
            List<int> listCONTADORDESPUESACTUAL = obtenerLista(_astEntities, ConstantesConsultas.QUERY_DATOS_ANALISIS_POSICION_TABLA, ConstantesGenerales.CONTADORDESPUESACTUAL, tabla, fecha);
            var tempDic = dict.ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in dict)
            {
                bool cumpleCondiciones = true;
                cumpleCondiciones &= listCONTADORDIASEMANA.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                cumpleCondiciones &= listCONTADORDIAMES.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                cumpleCondiciones &= listCONTADORDIAMODULO.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                cumpleCondiciones &= listCONTADORMES.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                cumpleCondiciones &= listCONTADORDESPUESACTUAL.IndexOf(item.Value.RankContadorDiaSemana) != -1;
                if (!cumpleCondiciones)
                {
                    tempDic.Remove(item.Key);
                }
            }
            return tempDic;
        }

        private static List<int> obtenerLista(AstEntities _astEntities, string query, string columna, string tabla, string fecha)
        {
            string consulta = string.Format(query, columna, tabla, fecha);
            DbRawSqlQuery<int> data = _astEntities.Database.SqlQuery<int>(consulta);
            return data.AsEnumerable().ToList();
        }

        public static List<int> ValidarIndicadorPosicion(AstEntities _astEntities, string columna, string tabla, string fecha, int datoComparar)
        {
            string consulta = string.Format(ConstantesConsultas.QUERY_CONTADOR_INDICADORES, tabla, columna, datoComparar, fecha);
            DbRawSqlQuery<ContadorValorDTO> data = _astEntities.Database.SqlQuery<ContadorValorDTO>(consulta);
            var listContadorValor = data.AsEnumerable().ToList();
            int min = (from x in listContadorValor select x.Contador).Min();
            int max = (from x in listContadorValor select x.Contador).Max();
            int media = (max + min) / 2;
            return (from x in listContadorValor where x.Contador >= media select x.Valor).ToList();
        }

    }
}
