using Constantes;
using DTOs;
using Model.DataContextModel;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Presenter
{
    public class AnalisisPosicionGeneral
    {
        /// <summary>
        /// Método que realiza la validación si el día actual se encuentra dentro de los datos analizados
        /// </summary>
        /// <param name="_astEntities">Objeto que instancia la bd para la realización de las consultas</param>
        /// <param name="tabla">Tabla en la que se analizan los datos</param>
        /// <param name="columna">columna de la tabla a analizar</param>
        /// <param name="valorComparar">valor para realizar la comparación</param>
        /// <param name="fecha">cadena que contiene la fecha formateada</param>
        /// <param name="dia">día del mes a analizar</param>
        /// <returns>bandera que indica si el día se encuetra dentro de la lista o no</returns>
        public static bool ObtenerBanderaIndicador(AstEntities _astEntities, string tabla, string columna, int valorComparar, string fecha, int dia)
        {
            return ValidarIndicadorPosicion(_astEntities, columna, tabla, fecha, valorComparar).IndexOf(dia) == -1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_astEntities">Objeto que instancia la bd para la realización de las consultas</param>>
        /// <param name="columna">columna de la tabla a analizar</param>
        /// <param name="tabla">Tabla en la que se analizan los datos</param>
        /// <param name="fecha">cadena que contiene la fecha formateada</param>
        /// <param name="valorComparar">valor para realizar la comparación</param>
        /// <returns>Método que retorna la lista de datos a analizar para los dias</returns>
        public static List<int> ValidarIndicadorPosicion(AstEntities _astEntities, string columna, string tabla, string fecha, int valorComparar)
        {
            string consulta = string.Format(ConstantesConsultas.QUERY_CONTADOR_INDICADORES, tabla, columna, valorComparar, fecha);
            DbRawSqlQuery<ContadorValorDTO> data = _astEntities.Database.SqlQuery<ContadorValorDTO>(consulta);
            var listContadorValor = data.AsEnumerable().ToList();
            int min = (from x in listContadorValor select x.Contador).Min();
            int max = (from x in listContadorValor select x.Contador).Max();
            int media = (max + min) / 2;
            return (from x in listContadorValor where x.Contador >= media select x.Valor).ToList();
        }

        /// <summary>
        /// Método que retorna la lista de cadenas de indicadores a analizar en los datos
        /// </summary>
        /// <returns>Lista de cadenas de indicadores</returns>
        public static List<string> ListaIndicadores()
        {
            List<string> llavesIndicadores = new List<string>(); llavesIndicadores.Add(ConstantesGenerales.INDICAMINSINAPARECER);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINULTRACH);
            llavesIndicadores.Add(ConstantesGenerales.COMPARAULTRACHAS);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMAXULTRACH);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMAXULTRACHDESACT);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINCONTGENERAL);
            llavesIndicadores.Add(ConstantesGenerales.INDICACOMPARACONTGENERAL);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINCONTDIASEMANA);
            llavesIndicadores.Add(ConstantesGenerales.INDICACOMPARACONTDIASEMANA);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINDIAMES);
            llavesIndicadores.Add(ConstantesGenerales.INDICACOMPARACONTDIAMES);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINDIAMOD);
            llavesIndicadores.Add(ConstantesGenerales.INDICACOMPARACONTDIAMOD);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINMES);
            llavesIndicadores.Add(ConstantesGenerales.INDICACOMPARACONTMES);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINDESPACTUAL);
            llavesIndicadores.Add(ConstantesGenerales.INDICACOMPARACONTDESPACTUAL);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINPUNTUATOTAL);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMAXPUNTUATOTAL);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMINSUMATORIA);
            llavesIndicadores.Add(ConstantesGenerales.INDICAMAXSUMATORIA);
            return llavesIndicadores;
        }

        /// <summary>
        /// Método que realiza la adición de elementos a la lista, en caso de cumplir las condiciones
        /// </summary>
        /// <param name="banderaCondicion">bandera que controla si el valor debe andicionarse</param>
        /// <param name="valorComparado">valor que sirve para realizar la comparacion</param>
        /// <param name="valorComparador">Valor que sirve para realizar la comparacion</param>
        /// <param name="claveElemento">Clave asignada al elemento para agrupar al análisis</param>
        /// <param name="claveDict">Clave asociada al diccioanrio</param>
        /// <param name="esComparar">Bandera que controla si el valor se compara por igualdad</param>
        /// <param name="listaEliminar">Lista que contiene los datos que serán adicionados en caso de cumplir las condiciones</param>
        /// <param name="validarMayor">Bandera que control si debe validarse el mayor o el menor</param>
        /// <returns>Valor resultado de la comparación</returns>
        public static int AdicionarElementoEliminar(bool banderaCondicion, int valorComparado, int valorComparador, string claveElemento, string claveDict, bool esComparar, List<InfoEliminacionDTO> listaEliminar, bool validarMayor = false)
        {
            if (esComparar)
            {
                if (banderaCondicion && valorComparado == valorComparador)
                {
                    InfoEliminacionDTO obj = new InfoEliminacionDTO();
                    obj.LlaveAnalisis = claveElemento;
                    obj.LlaveEliminarString = claveDict;
                    obj.Valor = valorComparador;
                    listaEliminar.Add(obj);
                }
            }
            else
            {
                if (!validarMayor)
                {
                    if (banderaCondicion && valorComparado < valorComparador)
                    {
                        InfoEliminacionDTO obj = new InfoEliminacionDTO();
                        obj.LlaveAnalisis = claveElemento;
                        obj.LlaveEliminarString = claveDict;
                        valorComparador = valorComparado;
                        obj.Valor = valorComparador;
                        listaEliminar.Add(obj);
                    }
                }
                else
                {
                    if (banderaCondicion && valorComparado > valorComparador)
                    {
                        InfoEliminacionDTO obj = new InfoEliminacionDTO();
                        obj.LlaveAnalisis = claveElemento;
                        obj.LlaveEliminarString = claveDict;
                        valorComparador = valorComparado;
                        obj.Valor = valorComparador;
                        listaEliminar.Add(obj);
                    }
                }
            }
            return valorComparador;
        }

        /// <summary>
        /// Método que realiza la adición de elementos a la lista, en caso de cumplir las condiciones
        /// </summary>
        /// <param name="banderaCondicion">bandera que controla si el valor debe andicionarse</param>
        /// <param name="valorComparado">valor que sirve para realizar la comparacion</param>
        /// <param name="valorComparador">Valor que sirve para realizar la comparacion</param>
        /// <param name="claveElemento">Clave asignada al elemento para agrupar al análisis</param>
        /// <param name="claveDict">Clave asociada al diccioanrio</param>
        /// <param name="esComparar">Bandera que controla si el valor se compara por igualdad</param>
        /// <param name="listaEliminar">Lista que contiene los datos que serán adicionados en caso de cumplir las condiciones</param>
        /// <param name="validarMayor">Bandera que control si debe validarse el mayor o el menor</param>
        /// <returns>Valor resultado de la comparación</returns>
        public static int AdicionarElementoEliminar(bool banderaCondicion, int valorComparado, int valorComparador, string claveElemento, int claveDict, bool esComparar, List<InfoEliminacionDTO> listaEliminar, bool validarMayor = false)
        {
            if (esComparar)
            {
                if (banderaCondicion && valorComparado == valorComparador)
                {
                    InfoEliminacionDTO obj = new InfoEliminacionDTO();
                    obj.LlaveAnalisis = claveElemento;
                    obj.LlaveElmininarInt = claveDict;
                    obj.Valor = valorComparador;
                    listaEliminar.Add(obj);
                }
            }
            else
            {
                if (!validarMayor)
                {
                    if (banderaCondicion && valorComparado < valorComparador)
                    {
                        InfoEliminacionDTO obj = new InfoEliminacionDTO();
                        obj.LlaveAnalisis = claveElemento;
                        obj.LlaveElmininarInt = claveDict;
                        valorComparador = valorComparado;
                        obj.Valor = valorComparador;
                        listaEliminar.Add(obj);
                    }
                }
                else
                {
                    if (banderaCondicion && valorComparado > valorComparador)
                    {
                        InfoEliminacionDTO obj = new InfoEliminacionDTO();
                        obj.LlaveAnalisis = claveElemento;
                        obj.LlaveElmininarInt = claveDict;
                        valorComparador = valorComparado;
                        obj.Valor = valorComparador;
                        listaEliminar.Add(obj);
                    }
                }
            }
            return valorComparador;
        }

        /// <summary>
        /// Método que contiene la lógica de eliminación de los elementos del diccionario
        /// </summary>
        /// <param name="dict">estrucutra que contiene los datos a analizar</param>
        /// <param name="listaEliminar">Lista que contiene los valores a eliminar</param>
        /// <param name="llavesIndicadores">Lista que contiene los indicadores a analizar</param>
        /// <returns>estructura con datos depurados</returns>
        public static Dictionary<int, ObjectInfoDTO> RemoverElementosListaDiccionario(Dictionary<int, ObjectInfoDTO> dict, List<InfoEliminacionDTO> listaEliminar, List<string> llavesIndicadores)
        {
            List<int> itemsEliminar = new List<int>();
            if (listaEliminar.Count() > 0)
            {
                foreach (var i in llavesIndicadores)
                {
                    List<InfoEliminacionDTO> subList = (from x in listaEliminar where x.LlaveAnalisis == i select x).ToList();
                    if (subList.Count() > 0)
                    {
                        int comparador = i.IndexOf("Max") != -1 ? (from x in subList select x.Valor).Max() : (from x in subList select x.Valor).Min();
                        itemsEliminar.AddRange((from x in subList where x.Valor == comparador select x.LlaveElmininarInt).ToList());
                    }
                }
            }
            var keysToInclude = dict.Keys.Except(itemsEliminar).ToList();
            var tempDic = (from entry in dict where keysToInclude.IndexOf(entry.Key) != -1 select entry).ToDictionary(x => x.Key, x => x.Value);
            return tempDic;
        }

        /// <summary>
        /// Método que realiza la consulta de los valores agrupados para las coincidencias de un valor siguiente
        /// </summary>
        /// <param name="columna">Columna de la tabla sobre la que se realiza la consulta</param>
        /// <param name="tablaValidar">Tabla para validar los datos</param>
        /// <param name="valorComparar">Valor sobre el que se realiza la comparacion</param>
        /// <returns></returns>
        public static List<int> AgruparContadoresDespuesActual(AstEntities _astEntities, string tablaValidar, string columna, int valorComparar, string fechaFormat, int cantidadTomar)
        {
            string consulta = string.Format(ConstantesConsultas.QUERY_AGRUPAR_APARICIONES_DESP_ACTUAL, columna, tablaValidar, valorComparar, fechaFormat);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(consulta);
            List<QueryInfo> lista = data.AsEnumerable().Take(cantidadTomar).ToList();
            return (from x in lista select x.ClaveNum).ToList();
        }

        /// <summary>
        /// Método que realiza la consulta de los valores agrupados para las coincidencias de un valor siguiente
        /// </summary>
        /// <param name="columna">Columna de la tabla sobre la que se realiza la consulta</param>
        /// <param name="tablaValidar">Tabla para validar los datos</param>
        /// <param name="valorComparar">Valor sobre el que se realiza la comparacion</param>
        /// <returns></returns>
        public static List<int> ContarDatosPosicionDiaMes(AstEntities _astEntities, string tablaValidar, string columna, int dia, string fechaFormat)
        {
            string consulta = string.Format(ConstantesConsultas.QUERY_CONTADOR_POSICION_DIA_MES, columna, tablaValidar, dia, fechaFormat);
            DbRawSqlQuery<ContadorValorDTO> data = _astEntities.Database.SqlQuery<ContadorValorDTO>(consulta);
            var listContadorValor = data.AsEnumerable().ToList();
            int min = (from x in listContadorValor select x.Contador).Min();
            int max = (from x in listContadorValor select x.Contador).Max();
            int media = (max + min) / 2;
            return (from x in listContadorValor where x.Contador >= media select x.Valor).ToList();
        }

        /// <summary>
        /// Método que realiza la consulta de los valores agrupados para las coincidencias de un valor siguiente
        /// </summary>
        /// <param name="columna">Columna de la tabla sobre la que se realiza la consulta</param>
        /// <param name="tablaValidar">Tabla para validar los datos</param>
        /// <param name="valorComparar">Valor sobre el que se realiza la comparacion</param>
        /// <returns></returns>
        public static List<int> ContarDatosPosicionDiaSemana(AstEntities _astEntities, string tablaValidar, string columna, int dia, string fechaFormat)
        {
            dia = dia == 0 ? 7 : dia;
            string consulta = string.Format(ConstantesConsultas.QUERY_CONTADOR_POSICION_DIA_SEMANA, columna, tablaValidar, dia, fechaFormat);
            DbRawSqlQuery<ContadorValorDTO> data = _astEntities.Database.SqlQuery<ContadorValorDTO>(consulta);
            var listContadorValor = data.AsEnumerable().Take(3).ToList();
            //int min = (from x in listContadorValor select x.Contador).Min();
            //int max = (from x in listContadorValor select x.Contador).Max();
            //int media = (max + min) / 2;
            return (from x in listContadorValor select x.Valor).ToList();
        }
    }
}