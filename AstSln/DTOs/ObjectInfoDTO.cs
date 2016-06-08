using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ObjectInfoDTO
    {
        private int _puntuacion;

        public int Puntuacion
        {
            get
            {
                return _puntuacion;
            }

            set
            {
                _puntuacion = value;
            }
        }
    }
}
