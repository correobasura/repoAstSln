﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UltimosRachasDTO
    {
        private int DatoConsulta;
        private int Posicion;

        public int DatoConsulta1
        {
            get
            {
                return DatoConsulta;
            }

            set
            {
                DatoConsulta = value;
            }
        }

        public int Posicion1
        {
            get
            {
                return Posicion;
            }

            set
            {
                Posicion = value;
            }
        }
    }
}
