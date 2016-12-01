using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constantes
{
    public class ConstantesConsultas
    {
        public const string QUERY_BASE = "SELECT {0} AS {2}, {4}-DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank, COUNT(*) AS CountRank FROM astr WHERE 1=1 {1} AND fecha < TO_DATE('{3}','dd/MM/yyyy') GROUP BY {0}";
        public const string QUERY_BASE_CONTADOR_GENERAL = "SELECT {0} AS {2}, {4} AS Rank, COUNT(*) AS CountRank FROM astr WHERE 1=1 {1} AND fecha < TO_DATE('{3}','dd/MM/yyyy') GROUP BY {0}";
        public const string QUERY_MAX_FECHA_CAMPO = "SELECT MAX(fecha) FROM {0} WHERE {1} = 1 AND fecha < TO_DATE('{2}','dd/MM/yyyy')";
        public const string QUERY_MIN_DATO_DATA_TEMP = "SELECT MIN({0}) FROM DATOS_TEMP WHERE fecha = to_date('{2}','dd/MM/yyyy') AND posicion = {4}";
        public const string QUERY_VER_APARICIONES_ACTUAL = "SELECT {0} FROM {1} WHERE fecha < to_date('{2}','dd/MM/yyyy') ORDER BY fecha DESC";
        public const string QUERY_VER_APARICIONES_DESP_ACTUAL = "SELECT {0} FROM {1} WHERE fecha-1 IN (SELECT fecha FROM {1} WHERE {0} = {2}) ORDER BY fecha DESC";
        public const string QUERY_AGRUPAR_APARICIONES_DESP_ACTUAL = "SELECT count(*) AS CountRank, {0} AS ClaveNum FROM {1} WHERE fecha-1 IN (SELECT fecha FROM {1} WHERE {0} = {2} AND fecha < to_date('{3}','dd/MM/yyyy')) GROUP BY {0} ORDER BY 1 DESC";
        public const string QUERY_CONSULTAR_MAX_DATA = "SELECT {0} FROM {1} WHERE fecha = (SELECT MAX(fecha) FROM {1})";
        public const string QUERY_TOTAL_DATOS_DESP_ACTUAL = "SELECT COUNT(*) FROM {0} WHERE fecha-1 IN (SELECT fecha FROM {0} WHERE CONTADORGENERAL = {1})";
        public const string QUERY_VALIDAR_ULTIMOS_RACHAS = "SELECT ContadorUltimoEnRachas AS DatoConsulta, 1 AS posicion FROM pos_uno_datos WHERE fecha = TO_DATE('{0}','dd/MM/yyyy') UNION ALL SELECT ContadorUltimoEnRachas AS DatoConsulta, 2 AS posicion FROM pos_dos_datos WHERE fecha = TO_DATE('{0}','dd/MM/yyyy') UNION ALL SELECT ContadorUltimoEnRachas AS DatoConsulta, 3 AS posicion FROM pos_tres_datos WHERE fecha = TO_DATE('{0}','dd/MM/yyyy') UNION ALL SELECT ContadorUltimoEnRachas AS DatoConsulta, 4 AS posicion FROM pos_cuatro_datos WHERE fecha = TO_DATE('{0}','dd/MM/yyyy') UNION ALL SELECT ContadorUltimoEnRachas AS DatoConsulta, 5 AS posicion FROM sign_datos WHERE fecha = TO_DATE('{0}','dd/MM/yyyy')";
        public const string QUERY_MISMO_DATO_ANDATOS = "SELECT COUNT(*) AS Contador, TO_CHAR(fecha,'MM') AS Mes FROM {0} WHERE {1} = 0 GROUP BY TO_CHAR(fecha,'MM') ORDER BY 1";
        public const string QUERY_SUMATORIA_DATOS = "SELECT COUNT(*) AS CountRank, SUMATORIAVALORES AS ClaveNum FROM {0} WHERE fecha < TO_DATE('{1}','dd/MM/yyyy') GROUP BY SUMATORIAVALORES ORDER BY 1 DESC";
        public const string QUERY_COUNT_DATOS_DESP_ACTUAL = "SELECT {0} AS {1}, {2}-DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank, COUNT(*) FROM astr WHERE fecha IN (SELECT fecha+1 FROM astr WHERE {0} = {3}) AND fecha < TO_DATE('{4}','dd/MM/yyyy') GROUP BY {0}";
        public const string QUERY_COUNT_DATOS_DESP_ACTUAL_STRING = "SELECT {0} AS {1}, {2}-DENSE_RANK () OVER (ORDER BY COUNT(*) DESC) AS Rank FROM astr WHERE fecha IN (SELECT fecha+1 FROM astr WHERE {0} LIKE '{3}') AND fecha < TO_DATE('{4}','dd/MM/yyyy') GROUP BY {0}";
        public const string QUERY_CONSULTA_AN_DATOS = "SELECT {0} FROM {1} WHERE fecha < TO_DATE('{2}','dd/MM/yyyy') ORDER BY FECHA DESC";
        public const string QUERY_AN_DATOS_COUNT_COLUMN = "SELECT COUNT(*) AS CONTADOR, DIAMES AS VALOR FROM {0} WHERE {1} = 1 AND fecha < TO_DATE('{2}','dd/MM/yyyy') GROUP BY DIAMES ORDER BY 1";
        public const string QUERY_COUNT_DATOS_DESP_ACTUAL_COLUMNA = "SELECT COUNT(*) AS Contador, {0} AS Valor FROM {1} WHERE fecha IN (SELECT fecha+1 FROM {1} WHERE {0} = {2}) AND fecha < TO_DATE('{3}','dd/MM/yyyy') GROUP BY {0} ORDER BY 1";
        public const string QUERY_DATOS_ANTERIOR_POSICION = "SELECT CONTADORDIASEMANA AS CONTADORDIASEMANA, CONTADORDIAMES AS CONTADORDIAMES, CONTADORDIAMODULO AS CONTADORDIAMODULO, CONTADORMES AS CONTADORMES, CONTADORDESPUESACTUAL AS CONTADORDESPUESACTUAL FROM {0} WHERE fecha = TO_DATE('{1}','dd/MM/yyyy')";
        public const string QUERY_DATOS_SIN_APARECER = "SELECT COUNT(*) AS Contador, sinaparecer AS Valor FROM {0} WHERE fecha < TO_DATE('{1}','dd/MM/yyyy') GROUP BY sinaparecer ORDER BY 1 DESC, 2 DESC";
    }
}
