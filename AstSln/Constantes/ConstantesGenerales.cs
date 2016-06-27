using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constantes
{
    public class ConstantesGenerales
    {
        public const string ACUARIO = "ACUARIO";
        public const string ARIES = "ARIES";
        public const string CANCER = "CANCER";
        public const string CAPRICORNIO = "CAPRICORNIO";
        public const string ESCORPION = "ESCORPION";
        public const string GEMINIS = "GEMINIS";
        public const string LEO = "LEO";
        public const string LIBRA = "LIBRA";
        public const string PISCIS = "PISCIS";
        public const string SAGITARIO = "SAGITARIO";
        public const string TAURO = "TAURO";
        public const string VIRGO = "VIRGO";
        public const string POS_UNO = "POS_UNO";
        public const string POS_DOS = "POS_DOS";
        public const string POS_TRES = "POS_TRES";
        public const string POS_CUATRO = "POS_CUATRO";
        public const string SIGN = "SIGN";
        public const string QUERY_BASE = "SELECT {0} AS {3}, 10-DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE tipo = {1} {2} GROUP BY {0}";
        public const string QUERY_BASE_STRING = "SELECT {0} AS {3}, 12-DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE tipo = {1} {2} GROUP BY {0}";
        public const string ENCABEZADOS = ";PuntuacionTotal;ContadorGeneral;ContadorDiaSemana;ContadorDiaMes;ContadorDiaModulo;ContadorMes;ContadorDiaAnio;ContadorDiaAnioModulo;ContadorMesModuloDiaModulo;ContadorMesDia;ContadorAnioModulo;ContadorMesModulo;ContadorDespuesActual;UltimasRachas;DictRachasAgrupadasInt;ContadorDespuesSignActual;ContadorUltimoEnRachas";
    }
}
