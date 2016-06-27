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
                + ";" + this.ContadorDiaSemana
                + ";" + this.ContadorDiaMes
                + ";" + this.ContadorDiaModulo
                + ";" + this.ContadorMes
                + ";" + this.ContadorDiaAnio
                + ";" + this.ContadorDiaAnioModulo
                + ";" + this.ContadorMesModuloDiaModulo
                + ";" + this.ContadorMesDia
                + ";" + this.ContadorAnioModulo
                + ";" + this.ContadorMesModulo
                + ";" + this.ContadorDespuesActual
                + ";" + this.ObtenerCadenaLista(RachasAcumuladas)
                + ";" + this.ObtenerCadenaDiccionario(this.DictRachasAgrupadasInt)
                + ";" + this.ContadorDespuesSignActual
                + ";" + this.DictRachasAgrupadasInt.Where(x=>x.Key == RachasAcumuladas.Last()).FirstOrDefault().Value;
            return cad;
        }
    }
}
