using DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    public class ManejoContadores
    {
        public static void AddInfoContAnioModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorAnioModulo = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContMesDia(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorMesDia = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContMesModuloDiaModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorMesModuloDiaModulo = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContMesModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorMesModulo = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AdinionarInformacionContadorDiaAnioModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorDiaAnioModulo = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContDiaAnio(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorDiaAnio = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContMes(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorMes = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContDiaModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorDiaModulo = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContDiaMes(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorDiaMes = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContDiaSemana(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorDiaSemana = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContGeneral(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].ContadorGeneral = cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContAnioModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorAnioModulo = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContMesDia(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorMesDia = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContMesModuloDiaModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorMesModuloDiaModulo = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContMesModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorMesModulo = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AdinionarInformacionContadorDiaAnioModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorDiaAnioModulo = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContDiaAnio(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorDiaAnio = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContMes(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorMes = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContDiaModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorDiaModulo = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContDiaMes(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorDiaMes = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContDiaSemana(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorDiaSemana = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }

        public static void AddInfoContGeneral(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].ContadorGeneral = cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
            }
        }
    }
}
