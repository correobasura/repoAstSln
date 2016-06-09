using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
