using System.Collections.Generic;

namespace DTOs
{
    public class ObjectInfoDTO
    {
        public ObjectInfoDTO()
        {
            this.RachasAparicion = new List<int>();
            this.RachasAcumuladas = new List<int>();
            this.DictRachasAgrupadasInt = new Dictionary<int, int>();
            this.RachasAparicionDespActual = new List<int>();
            this.RachasAcumuladasDespActual = new List<int>();
            this.DictRachasAgrupadasIntDespActual = new Dictionary<int, int>();
        }

        /// <summary>
        /// Almacena el valor de todas las puntuaciones individuales
        /// </summary>
        public int PuntuacionTotal { get; set; }

        /// <summary>
        /// Almacena el valor del contador estadístico de los valores que mas aparecen
        /// </summary>
        public int ContadorGeneral { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día de la semana
        /// </summary>
        public int ContadorDiaSemana { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día del mes
        /// </summary>
        public int ContadorDiaMes { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día par o impar
        /// </summary>
        public int ContadorDiaModulo { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al mes
        /// </summary>
        public int ContadorMes { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día del año
        /// </summary>
        //public int ContadorDiaAnio { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día del año par o impar
        /// </summary>
        //public int ContadorDiaAnioModulo { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al mes par o impar y el día par o impar
        /// </summary>
        //public int ContadorMesModuloDiaModulo { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al mes y al día
        /// </summary>
        //public int ContadorMesDia { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al año par o impar
        /// </summary>
        //public int ContadorAnioModulo { get; set; }

        /// <summary>
        /// Agrupa los contadores de acuerdo al mes par o impar
        /// </summary>
        //public int ContadorMesModulo { get; set; }

        /// <summary>
        /// Variable que almacena el valor contador para las apariciones después del actual
        /// </summary>
        public int ContadorDespuesActual { get; set; }

        /// <summary>
        /// Contiene la lista de las rachas, apariciones del valor en los resultados
        /// </summary>
        public List<int> RachasAparicion { get; set; }

        /// <summary>
        /// Contiene la lista de rachas agrupadas de acuerdo a las apariciones
        /// </summary>
        public List<int> RachasAcumuladas { get; set; }

        /// <summary>
        /// Contiene la información de las rachas agrupadas para los números
        /// </summary>
        public Dictionary<int, int> DictRachasAgrupadasInt { get; set; }

        /// <summary>
        /// Referencia al contador de las apariciones después el último signo
        /// </summary>
        public int ContadorDespuesSignActual { get; set; }

        public int RankContadorGeneral { get; set; }

        public int RankContadorDiaSemana { get; set; }

        public int RankContadorDiaMes { get; set; }

        public int RankContadorDiaModulo { get; set; }

        public int RankContadorMes { get; set; }

        public int CONTADORULTIMOENRACHAS { get; set; }

        public int CONTADORULTIMOENRACHASDESACTUA { get; set; }

        //public int RankContadorDiaAnio { get; set; }

        //public int RankContadorDiaAnioModulo { get; set; }

        //public int RankContadorMesModulo { get; set; }

        //public int RankContadorMesModuloDiaModulo { get; set; }

        //public int RankContadorMesDia { get; set; }

        //public int RankContadorAnioModulo { get; set; }

        //public int ContadorDespuesOtroTipo { get; set; }

        public List<int> RachasAparicionDespActual { get; set; }

        public List<int> RachasAcumuladasDespActual { get; set; }

        public Dictionary<int, int> DictRachasAgrupadasIntDespActual { get; set; }

        //private string ObtenerCadenaDiccionario(Dictionary<int, int> dict)
        //{
        //    string cad = "'";
        //    for (int i = 1; i < dict.Count; i++)
        //    {
        //        var item = dict.ElementAt(i);
        //        cad += item.Key + "=" + item.Value + ",";
        //    }
        //    cad += "'";
        //    return cad;
        //}

        //private string ObtenerCadenaLista(List<int> lista)
        //{
        //    string cad = "'";
        //    int contador = 0;
        //    for (int i = lista.Count - 1; i > 0 && contador < 20; i--)
        //    {
        //        cad += lista.ElementAt(i) + ",";
        //        contador++;
        //    }
        //    cad += "'";
        //    return cad;
        //}

        /// <summary>
        /// Método que genera la información en texto de la clase
        /// </summary>
        /// <returns>cadena con información</returns>
        //public override string ToString()
        //{
        //    string cad =
        //        ";" + this.PuntuacionTotal
        //        + ";" + this.ObtenerCadenaLista(RachasAcumuladas)
        //        + ";" + this.ObtenerCadenaDiccionario(this.DictRachasAgrupadasInt)
        //        + ";" + this.DictRachasAgrupadasInt.Where(x => x.Key == RachasAcumuladas.Last()).FirstOrDefault().Value
        //        + ";" + this.ObtenerCadenaLista(RachasAcumuladasDespActual)
        //        + ";" + this.ObtenerCadenaDiccionario(this.DictRachasAgrupadasIntDespActual)
        //        + ";" + this.DictRachasAgrupadasIntDespActual.Where(x => x.Key == RachasAcumuladasDespActual.Last()).FirstOrDefault().Value
        //        + ";" + this.ContadorGeneral
        //        + ";" + this.RankContadorGeneral
        //        + ";" + this.ContadorDiaSemana
        //        + ";" + this.RankContadorDiaSemana
        //        + ";" + this.ContadorDiaMes
        //        + ";" + this.RankContadorDiaMes
        //        + ";" + this.ContadorDiaModulo
        //        + ";" + this.RankContadorDiaModulo
        //        + ";" + this.ContadorMes
        //        + ";" + this.RankContadorMes
        //        + ";" + this.ContadorDiaAnio
        //        + ";" + this.RankContadorDiaAnio
        //        + ";" + this.ContadorDiaAnioModulo
        //        + ";" + this.RankContadorDiaAnioModulo
        //        + ";" + this.ContadorMesModuloDiaModulo
        //        + ";" + this.RankContadorMesModuloDiaModulo
        //        + ";" + this.ContadorMesDia
        //        + ";" + this.RankContadorMesDia
        //        + ";" + this.ContadorAnioModulo
        //        + ";" + this.RankContadorAnioModulo
        //        + ";" + this.ContadorMesModulo
        //        + ";" + this.RankContadorMesModulo
        //        + ";" + this.ContadorDespuesActual
        //        + ";" + this.ContadorDespuesOtroTipo
        //        + ";" + this.ContadorDespuesSignActual;
        //    return cad;
        //}
    }
}