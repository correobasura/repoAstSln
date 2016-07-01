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
                dict[cust.ClaveNum].RankContadorAnioModulo = cust.Rank;
                dict[cust.ClaveNum].ContadorAnioModulo = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContMesDia(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorMesDia = cust.Rank;
                dict[cust.ClaveNum].ContadorMesDia = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContMesModuloDiaModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorMesModuloDiaModulo = cust.Rank;
                dict[cust.ClaveNum].ContadorMesModuloDiaModulo = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContMesModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorMesModulo = cust.Rank;
                dict[cust.ClaveNum].ContadorMesModulo = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AdinionarInformacionContadorDiaAnioModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorDiaAnioModulo = cust.Rank;
                dict[cust.ClaveNum].ContadorDiaAnioModulo = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContDiaAnio(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorDiaAnio = cust.Rank;
                dict[cust.ClaveNum].ContadorDiaAnio = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContMes(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorMes = cust.Rank;
                dict[cust.ClaveNum].ContadorMes = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContDiaModulo(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorDiaModulo = cust.Rank;
                dict[cust.ClaveNum].ContadorDiaModulo = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContDiaMes(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorDiaMes = cust.Rank;
                dict[cust.ClaveNum].ContadorDiaMes = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContDiaSemana(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorDiaSemana = cust.Rank;
                dict[cust.ClaveNum].ContadorDiaSemana = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContGeneral(Dictionary<int, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveNum].RankContadorGeneral = cust.Rank;
                dict[cust.ClaveNum].ContadorGeneral = cust.CountRank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveNum].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContAnioModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorAnioModulo = cust.Rank;
                dict[cust.ClaveSign].ContadorAnioModulo = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContMesDia(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorMesDia = cust.Rank;
                dict[cust.ClaveSign].ContadorMesDia = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContMesModuloDiaModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorMesModuloDiaModulo = cust.Rank;
                dict[cust.ClaveSign].ContadorMesModuloDiaModulo = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContMesModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorMesModulo = cust.Rank;
                dict[cust.ClaveSign].ContadorMesModulo = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AdinionarInformacionContadorDiaAnioModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorDiaAnioModulo = cust.Rank;
                dict[cust.ClaveSign].ContadorDiaAnioModulo = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContDiaAnio(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorDiaAnio = cust.Rank;
                dict[cust.ClaveSign].ContadorDiaAnio = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContMes(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorMes = cust.Rank;
                dict[cust.ClaveSign].ContadorMes = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContDiaModulo(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorDiaModulo = cust.Rank;
                dict[cust.ClaveSign].ContadorDiaModulo = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContDiaMes(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorDiaMes = cust.Rank;
                dict[cust.ClaveSign].ContadorDiaMes = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContDiaSemana(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorDiaSemana = cust.Rank;
                dict[cust.ClaveSign].ContadorDiaSemana = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }

        public static void AddInfoContGeneral(Dictionary<string, ObjectInfoDTO> dict, DbRawSqlQuery<QueryInfo> data)
        {
            foreach (var cust in data)
            {
                dict[cust.ClaveSign].RankContadorGeneral = cust.Rank;
                dict[cust.ClaveSign].ContadorGeneral = cust.CountRank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.Rank;
                dict[cust.ClaveSign].PuntuacionTotal += cust.CountRank;
            }
        }
    }
}
