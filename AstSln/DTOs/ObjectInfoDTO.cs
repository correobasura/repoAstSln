using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataContextModel;

namespace DTOs
{
    public class ObjectInfoDTO
    {
        private int _puntuacionTotal;
        private int _contadorGeneral;
        private int _contadorDiaSemana;
        private int _contadorDiaMes;
        private int _contadorDiaModulo;
        private int _contadorMes;
        private int _contadorDiaAnio;
        private int _contadorDiaAnioModulo;
        private int _contadorMesModulo;
        private int _contadorMesModuloDiaModulo;
        private int _contadorMesDia;
        private int _contadorAnioModulo;
        private int _contadorDespuesActual;
        private List<int> _rachasAparicion;
        private List<int> _rachasAcumuladas;
        private Dictionary<int, int> _dictRachasAgrupadasInt;
        private int _contadorDespuesSignActual;
        private int _rankContadorGeneral;
        private int _rankContadorDiaSemana;
        private int _rankContadorDiaMes;
        private int _rankContadorDiaModulo;
        private int _rankContadorMes;
        private int _rankContadorDiaAnio;
        private int _rankContadorDiaAnioModulo;
        private int _rankContadorMesModulo;
        private int _rankContadorMesModuloDiaModulo;
        private int _rankContadorMesDia;
        private int _rankContadorAnioModulo;

        public ObjectInfoDTO()
        {
            this.RachasAparicion = new List<int>();
            this.RachasAcumuladas = new List<int>();
            this.DictRachasAgrupadasInt = new Dictionary<int, int>();
        }

        /// <summary>
        /// Almacena el valor de todas las puntuaciones individuales
        /// </summary>
        public int PuntuacionTotal
        {
            get
            {
                return _puntuacionTotal;
            }

            set
            {
                _puntuacionTotal = value;
            }
        }

        /// <summary>
        /// Almacena el valor del contador estadístico de los valores que mas aparecen
        /// </summary>
        public int ContadorGeneral
        {
            get
            {
                return _contadorGeneral;
            }

            set
            {
                _contadorGeneral = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día de la semana
        /// </summary>
        public int ContadorDiaSemana
        {
            get
            {
                return _contadorDiaSemana;
            }

            set
            {
                _contadorDiaSemana = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día del mes
        /// </summary>
        public int ContadorDiaMes
        {
            get
            {
                return _contadorDiaMes;
            }

            set
            {
                _contadorDiaMes = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día par o impar
        /// </summary>
        public int ContadorDiaModulo
        {
            get
            {
                return _contadorDiaModulo;
            }

            set
            {
                _contadorDiaModulo = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al mes
        /// </summary>
        public int ContadorMes
        {
            get
            {
                return _contadorMes;
            }

            set
            {
                _contadorMes = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día del año
        /// </summary>
        public int ContadorDiaAnio
        {
            get
            {
                return _contadorDiaAnio;
            }

            set
            {
                _contadorDiaAnio = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al día del año par o impar
        /// </summary>
        public int ContadorDiaAnioModulo
        {
            get
            {
                return _contadorDiaAnioModulo;
            }

            set
            {
                _contadorDiaAnioModulo = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al mes par o impar y el día par o impar
        /// </summary>
        public int ContadorMesModuloDiaModulo
        {
            get
            {
                return _contadorMesModuloDiaModulo;
            }

            set
            {
                _contadorMesModuloDiaModulo = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al mes y al día
        /// </summary>
        public int ContadorMesDia
        {
            get
            {
                return _contadorMesDia;
            }

            set
            {
                _contadorMesDia = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al año par o impar
        /// </summary>
        public int ContadorAnioModulo
        {
            get
            {
                return _contadorAnioModulo;
            }

            set
            {
                _contadorAnioModulo = value;
            }
        }

        /// <summary>
        /// Agrupa los contadores de acuerdo al mes par o impar
        /// </summary>
        public int ContadorMesModulo
        {
            get
            {
                return _contadorMesModulo;
            }

            set
            {
                _contadorMesModulo = value;
            }
        }

        /// <summary>
        /// Variable que almacena el valor contador para las apariciones después del actual
        /// </summary>
        public int ContadorDespuesActual
        {
            get
            {
                return _contadorDespuesActual;
            }

            set
            {
                _contadorDespuesActual = value;
            }
        }

        /// <summary>
        /// Contiene la lista de las rachas, apariciones del valor en los resultados
        /// </summary>
        public List<int> RachasAparicion
        {
            get
            {
                return _rachasAparicion;
            }

            set
            {
                _rachasAparicion = value;
            }
        }

        /// <summary>
        /// Contiene la lista de rachas agrupadas de acuerdo a las apariciones
        /// </summary>
        public List<int> RachasAcumuladas
        {
            get
            {
                return _rachasAcumuladas;
            }

            set
            {
                _rachasAcumuladas = value;
            }
        }

        /// <summary>
        /// Contiene la información de las rachas agrupadas para los números
        /// </summary>
        public Dictionary<int, int> DictRachasAgrupadasInt
        {
            get
            {
                return _dictRachasAgrupadasInt;
            }

            set
            {
                _dictRachasAgrupadasInt = value;
            }
        }

        /// <summary>
        /// Referencia al contador de las apariciones después el último signo
        /// </summary>
        public int ContadorDespuesSignActual
        {
            get
            {
                return _contadorDespuesSignActual;
            }

            set
            {
                _contadorDespuesSignActual = value;
            }
        }

        public int RankContadorGeneral
        {
            get
            {
                return _rankContadorGeneral;
            }

            set
            {
                _rankContadorGeneral = value;
            }
        }

        public int RankContadorDiaSemana
        {
            get
            {
                return _rankContadorDiaSemana;
            }

            set
            {
                _rankContadorDiaSemana = value;
            }
        }

        public int RankContadorDiaMes
        {
            get
            {
                return _rankContadorDiaMes;
            }

            set
            {
                _rankContadorDiaMes = value;
            }
        }

        public int RankContadorDiaModulo
        {
            get
            {
                return _rankContadorDiaModulo;
            }

            set
            {
                _rankContadorDiaModulo = value;
            }
        }

        public int RankContadorMes
        {
            get
            {
                return _rankContadorMes;
            }

            set
            {
                _rankContadorMes = value;
            }
        }

        public int RankContadorDiaAnio
        {
            get
            {
                return _rankContadorDiaAnio;
            }

            set
            {
                _rankContadorDiaAnio = value;
            }
        }

        public int RankContadorDiaAnioModulo
        {
            get
            {
                return _rankContadorDiaAnioModulo;
            }

            set
            {
                _rankContadorDiaAnioModulo = value;
            }
        }

        public int RankContadorMesModulo
        {
            get
            {
                return _rankContadorMesModulo;
            }

            set
            {
                _rankContadorMesModulo = value;
            }
        }

        public int RankContadorMesModuloDiaModulo
        {
            get
            {
                return _rankContadorMesModuloDiaModulo;
            }

            set
            {
                _rankContadorMesModuloDiaModulo = value;
            }
        }

        public int RankContadorMesDia
        {
            get
            {
                return _rankContadorMesDia;
            }

            set
            {
                _rankContadorMesDia = value;
            }
        }

        public int RankContadorAnioModulo
        {
            get
            {
                return _rankContadorAnioModulo;
            }

            set
            {
                _rankContadorAnioModulo = value;
            }
        }

        private string ObtenerCadenaDiccionario(Dictionary<int, int> dict)
        {
            string cad = "'";
            for (int i = 1; i < dict.Count; i++)
            {
                var item = dict.ElementAt(i);
                cad += item.Key + "=" + item.Value + ",";
            }
            cad += "'";
            return cad;
        }

        private string ObtenerCadenaLista(List<int> lista)
        {
            string cad = "'";
            int contador = 0;
            for (int i = lista.Count - 1; i > 0 && contador < 10; i--)
            {
                cad += lista.ElementAt(i) + ",";
                contador++;
            }
            cad += "'";
            return cad;
        }

        /// <summary>
        /// Método que genera la información en texto de la clase
        /// </summary>
        /// <returns>cadena con información</returns>
        public override string ToString()
        {
            string cad =
                ";" + this.PuntuacionTotal
                + ";" + this.ContadorGeneral
                + ";" + this.RankContadorGeneral
                + ";" + this.ContadorDiaSemana
                + ";" + this.RankContadorDiaSemana
                + ";" + this.ContadorDiaMes
                + ";" + this.RankContadorDiaMes
                + ";" + this.ContadorDiaModulo
                + ";" + this.RankContadorDiaModulo
                + ";" + this.ContadorMes
                + ";" + this.RankContadorMes
                + ";" + this.ContadorDiaAnio
                + ";" + this.RankContadorDiaAnio
                + ";" + this.ContadorDiaAnioModulo
                + ";" + this.RankContadorDiaAnioModulo
                + ";" + this.ContadorMesModuloDiaModulo
                + ";" + this.RankContadorMesModuloDiaModulo
                + ";" + this.ContadorMesDia
                + ";" + this.RankContadorMesDia
                + ";" + this.ContadorAnioModulo
                + ";" + this.RankContadorAnioModulo
                + ";" + this.ContadorMesModulo
                + ";" + this.RankContadorMesModulo
                + ";" + this.ContadorDespuesActual
                + ";" + this.ObtenerCadenaLista(RachasAcumuladas)
                + ";" + this.ObtenerCadenaDiccionario(this.DictRachasAgrupadasInt)
                + ";" + this.ContadorDespuesSignActual
                + ";" + this.DictRachasAgrupadasInt.Where(x => x.Key == RachasAcumuladas.Last()).FirstOrDefault().Value;
            return cad;
        }
    }
}
