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
        public const string QUERY_GENERAL = "SELECT SUM(A.Total) AS Rank, A.Clave AS {11} FROM ( {0} UNION ALL {1} UNION ALL {2} UNION ALL {3} UNION ALL {4} UNION ALL {5} UNION ALL {6} UNION ALL {7} UNION ALL {8} UNION ALL {9} UNION ALL {10}) A GROUP BY A.Clave";
        public const string QUERY_BASE = "SELECT {0} AS {3}, 10-DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE tipo = {1} {2} GROUP BY {0}";
    }
}
