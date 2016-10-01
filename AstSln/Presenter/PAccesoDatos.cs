using Constantes;
using IView;
using Model.DataContextModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using DTOs;

namespace Presenter
{
    public class PAccesoDatos
    {
        private AstEntities _astEntities;
        private IAccesoDatos _iAccesoDatos;
        private ASTR _resultActualLun;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosCuatroLun;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosCuatroSol;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosDosLun;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosDosSol;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosTresLun;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosTresSol;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosUnoLun;
        private Dictionary<int, ObjectInfoDTO> dictInfoPosUnoSol;
        private Dictionary<string, ObjectInfoDTO> dictInfoSignLun;
        private Dictionary<string, ObjectInfoDTO> dictInfoSignSol;
        private DateTime fecha;
        private List<ASTR> listaDatosGeneral;
        private List<ASTR> listaDatosLun;
        private string path = "";

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="iAccesoDatos"></param>
        public PAccesoDatos(IAccesoDatos iAccesoDatos, DateTime fecha)
        {
            this._astEntities = new AstEntities();
            this._iAccesoDatos = iAccesoDatos;
            this.dictInfoPosCuatroLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            //this.dictInfoPosCuatroSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosDosLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            //this.dictInfoPosDosSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosTresLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            //this.dictInfoPosTresSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoPosUnoLun = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            //this.dictInfoPosUnoSol = (Dictionary<int, ObjectInfoDTO>)this.InicializarDiccionarioInformacion();
            this.dictInfoSignLun = (Dictionary<string, ObjectInfoDTO>)this.InicializarDiccionarioInformacion(1);
            //this.dictInfoSignSol = (Dictionary<string, ObjectInfoDTO>)this.InicializarDiccionarioInformacion(1);
            this.fecha = fecha;
            path = @"C:\temp" + @"\" + fecha.Year + "" + fecha.Month + @"\" + fecha.Day + @"\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Método que obtiene la lista de resultados de acuerdo a los parámetros ingresados
        /// </summary>
        public void ObtenerResultados()
        {
            using (var context = new AstEntities())
            {
                var objectContex = ((IObjectContextAdapter)context).ObjectContext;
                //Si no se pagina la lista, se obtienen todos los resultados, de lo contrario, se traen los resultados solicitados
                listaDatosGeneral = objectContex.CreateObjectSet<ASTR>().Where(x => x.FECHA < this.fecha).OrderBy(x => x.FECHA).ToList();
                //listaDatosSol = listaDatosGeneral.Where(x => x.TIPO == 1).ToList();
                listaDatosLun = listaDatosGeneral;
            }
            //this.ValidarDatosRepetidos(listaDatosSol);
            this.ObtenerUltimoResultado();
            this.PuntuarInformacion(ConstantesGenerales.POS_UNO, ConstantesTipoSor.TIPO_LUN, dictInfoPosUnoLun, (int)_resultActualLun.POS_UNO);
            this.PuntuarInformacion(ConstantesGenerales.POS_DOS, ConstantesTipoSor.TIPO_LUN, dictInfoPosDosLun, (int)_resultActualLun.POS_DOS);
            this.PuntuarInformacion(ConstantesGenerales.POS_TRES, ConstantesTipoSor.TIPO_LUN, dictInfoPosTresLun, (int)_resultActualLun.POS_TRES);
            this.PuntuarInformacion(ConstantesGenerales.POS_CUATRO, ConstantesTipoSor.TIPO_LUN, dictInfoPosCuatroLun, (int)_resultActualLun.POS_CUATRO);
            this.PuntuarInformacion(ConstantesGenerales.SIGN, ConstantesTipoSor.TIPO_LUN, dictInfoSignLun, _resultActualLun.SIGN);
            //if (fecha.DayOfWeek != DayOfWeek.Sunday)
            //{
            //    this.RecorrerElementosLista(listaDatosSol, _resultActualSol, dictInfoPosUnoSol, dictInfoPosDosSol, dictInfoPosTresSol, dictInfoPosCuatroSol, dictInfoSignSol);
            //    this.RecorrerElementosLista(listaDatosSol, _resultActualLun, dictInfoPosUnoSol, dictInfoPosDosSol, dictInfoPosTresSol, dictInfoPosCuatroSol, dictInfoSignSol);
            //    this.ValidarRachas(listaDatosSol, dictInfoPosUnoSol, dictInfoPosDosSol, dictInfoPosTresSol, dictInfoPosCuatroSol, dictInfoSignSol);
            //    this.EscribirDatosArchivo(dictInfoPosUnoSol, "PosUnoSol");
            //    this.EscribirDatosArchivo(dictInfoPosDosSol, "PosDosSol");
            //    this.EscribirDatosArchivo(dictInfoPosTresSol, "PosTresSol");
            //    this.EscribirDatosArchivo(dictInfoPosCuatroSol, "PosCuatroSol");
            //    this.EscribirDatosArchivo(dictInfoSignSol, "SignSol");
            //}
            this.RecorrerElementosLista(listaDatosLun, _resultActualLun, dictInfoPosUnoLun, dictInfoPosDosLun, dictInfoPosTresLun, dictInfoPosCuatroLun, dictInfoSignLun);
            //this.RecorrerElementosLista(listaDatosLun, _resultActualSol, dictInfoPosUnoLun, dictInfoPosDosLun, dictInfoPosTresLun, dictInfoPosCuatroLun, dictInfoSignLun);
            this.ValidarRachas(listaDatosLun, dictInfoPosUnoLun, dictInfoPosDosLun, dictInfoPosTresLun, dictInfoPosCuatroLun, dictInfoSignLun);


            //this.GuardarDatosTemporales();


            //this.EscribirDatosArchivo(dictInfoPosUnoLun, "PosUnoLun");
            //this.EscribirDatosArchivo(dictInfoPosDosLun, "PosDosLun");
            //this.EscribirDatosArchivo(dictInfoPosTresLun, "PosTresLun");
            //this.EscribirDatosArchivo(dictInfoPosCuatroLun, "PosCuatroLun");
            //this.EscribirDatosArchivo(dictInfoSignLun, "SignLun");
            //this.RevisarValoresMinimos(dictInfoPosUnoLun, "PosUnoLunDep", ConstantesGenerales.AN_DAT_POS_UNO, ConstantesTipoSor.POSICION_UNO, ConstantesGenerales.POS_UNO_DATOS);
            //this.RevisarValoresMinimos(dictInfoPosDosLun, "PosDosLunDep", ConstantesGenerales.AN_DAT_POS_DOS, ConstantesTipoSor.POSICION_DOS, ConstantesGenerales.POS_DOS_DATOS);
            //this.RevisarValoresMinimos(dictInfoPosTresLun, "PosTresLunDep", ConstantesGenerales.AN_DAT_POS_TRES, ConstantesTipoSor.POSICION_TRES, ConstantesGenerales.POS_TRES_DATOS);
            //this.RevisarValoresMinimos(dictInfoPosCuatroLun, "PosCuatroLunDep", ConstantesGenerales.AN_DAT_POS_CUATRO, ConstantesTipoSor.POSICION_CUATRO, ConstantesGenerales.POS_CUATRO_DATOS);
            //this.RevisarValoresMinimos(dictInfoSignLun, "PosSignDep", ConstantesGenerales.AN_DAT_SIGN, ConstantesTipoSor.POSICION_CINCO, ConstantesGenerales.SIGN_DATOS);
            //this.ObtenerUltimosRachasPosicion();
            //this.EliminarMismoDato(dictInfoPosUnoLun, "PosUnoLunDep5", ConstantesGenerales.AN_DAT_POS_UNO, 1);
            //this.EliminarMismoDato(dictInfoPosDosLun, "PosDosLunDep5", ConstantesGenerales.AN_DAT_POS_DOS, 2);
            //this.EliminarMismoDato(dictInfoPosTresLun, "PosTresLunDep5", ConstantesGenerales.AN_DAT_POS_TRES, 3);
            //this.EliminarMismoDato(dictInfoPosCuatroLun, "PosCuatroLunDep5", ConstantesGenerales.AN_DAT_POS_CUATRO, 4);
            //this.EliminarMismoDato(dictInfoSignLun, "PosSignDep5", ConstantesGenerales.AN_DAT_SIGN, 5);
            //this.EliminarDatoSumatoria(dictInfoPosUnoLun, "PosUnoLunDep6",1);
            //this.EliminarDatoSumatoria(dictInfoPosDosLun, "PosDosLunDep6",2);
            //this.EliminarDatoSumatoria(dictInfoPosTresLun, "PosTresLunDep6",3);
            //this.EliminarDatoSumatoria(dictInfoPosCuatroLun, "PosCuatroLunDep6",4);
            //this.EliminarDatoSumatoria(dictInfoSignLun, "PosSignDep6",5);
            var dict1 = this.ValidarSumatoriaDatos(dictInfoPosUnoLun, "APosUnoLunMenores", ConstantesGenerales.POS_UNO_DATOS, "ASC");
            var dict2 = this.ValidarSumatoriaDatos(dictInfoPosDosLun, "BPosDosLunMenores", ConstantesGenerales.POS_DOS_DATOS, "ASC");
            var dict3 = this.ValidarSumatoriaDatos(dictInfoPosTresLun, "CPosTresLunMenores", ConstantesGenerales.POS_TRES_DATOS, "ASC");
            var dict4 = this.ValidarSumatoriaDatos(dictInfoPosCuatroLun, "DPosCuatroLunMenores", ConstantesGenerales.POS_CUATRO_DATOS, "ASC");
            var dict5 = this.ValidarSumatoriaDatos(dictInfoSignLun, "EPosSignMenores", ConstantesGenerales.SIGN_DATOS, "ASC");
            this.RevisarValoresMinimos(dict1, "PosUnoLunDep", ConstantesGenerales.AN_DAT_POS_UNO, ConstantesTipoSor.POSICION_UNO, ConstantesGenerales.POS_UNO_DATOS);
            this.RevisarValoresMinimos(dict2, "PosDosLunDep", ConstantesGenerales.AN_DAT_POS_DOS, ConstantesTipoSor.POSICION_DOS, ConstantesGenerales.POS_DOS_DATOS);
            this.RevisarValoresMinimos(dict3, "PosTresLunDep", ConstantesGenerales.AN_DAT_POS_TRES, ConstantesTipoSor.POSICION_TRES, ConstantesGenerales.POS_TRES_DATOS);
            this.RevisarValoresMinimos(dict4, "PosCuatroLunDep", ConstantesGenerales.AN_DAT_POS_CUATRO, ConstantesTipoSor.POSICION_CUATRO, ConstantesGenerales.POS_CUATRO_DATOS);
            this.RevisarValoresMinimos(dict5, "PosSignDep", ConstantesGenerales.AN_DAT_SIGN, ConstantesTipoSor.POSICION_CINCO, ConstantesGenerales.SIGN_DATOS);
        }

        private void GuardarDatosTemporales()
        {
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_UNO, dictInfoPosUnoLun);
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_DOS, dictInfoPosDosLun);
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_TRES, dictInfoPosTresLun);
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_CUATRO, dictInfoPosCuatroLun);
            this.GuardarDatosTemporales(ConstantesTipoSor.TIPO_LUN, ConstantesTipoSor.POSICION_CINCO, dictInfoSignLun);
            this.EscribirDatosAnterior(this._resultActualLun);
        }

        private void EliminarMismoDato(Dictionary<int,ObjectInfoDTO> dict, string nombreArchivo, string tabla, int casoObjetoAnalisis)
        {
            List<QueryInfoMismoDato> listCOMPARA_CONT_GENERAL = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_GENERAL);
            List<QueryInfoMismoDato> listCOMPARA_CONT_DIA_SEM = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_DIA_SEM);
            List<QueryInfoMismoDato> listCOMPARA_CONT_DIA_MES = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_DIA_MES);
            List<QueryInfoMismoDato> listCOMPARA_CONT_DIA_MOD = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_DIA_MOD);
            List<QueryInfoMismoDato> listCOMPARA_CONT_MES = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_MES);
            var dictRevisar = dict.ToDictionary(x => x.Key, x => x.Value);
            int mes = this.fecha.Month;
            bool comparaConGene = this.ObtenerIndexElemento(listCOMPARA_CONT_GENERAL, mes);
            bool comparaContDiaSem= this.ObtenerIndexElemento(listCOMPARA_CONT_DIA_SEM, mes);
            bool comparaContDiaMes = this.ObtenerIndexElemento(listCOMPARA_CONT_DIA_MES, mes);
            bool comparaContDiaMod = this.ObtenerIndexElemento(listCOMPARA_CONT_DIA_MOD, mes);
            bool comparaConMes = this.ObtenerIndexElemento(listCOMPARA_CONT_MES, mes);
            int conGeneral = 0;
            int conDiaSemana = 0;
            int conDiaMes = 0;
            int conDiaMod = 0;
            int conMes = 0;

            switch (casoObjetoAnalisis)
            {
                case 1:
                    POS_UNO_DATOS p1 = (POS_UNO_DATOS)this.ObtenerUltimoObjetoPosicion(casoObjetoAnalisis);
                    conGeneral = (int)p1.CONTADORGENERAL;
                    conDiaSemana = (int)p1.CONTADORDIASEMANA;
                    conDiaMes = (int)p1.CONTADORDIAMES;
                    conDiaMod = (int)p1.CONTADORDIAMODULO;
                    conMes = (int)p1.CONTADORMES;
                    break;
                case 2:
                    POS_DOS_DATOS p2 = (POS_DOS_DATOS)this.ObtenerUltimoObjetoPosicion(casoObjetoAnalisis);
                    conGeneral = (int)p2.CONTADORGENERAL;
                    conDiaSemana = (int)p2.CONTADORDIASEMANA;
                    conDiaMes = (int)p2.CONTADORDIAMES;
                    conDiaMod = (int)p2.CONTADORDIAMODULO;
                    conMes = (int)p2.CONTADORMES;
                    break;
                case 3:
                    POS_TRES_DATOS p3 = (POS_TRES_DATOS)this.ObtenerUltimoObjetoPosicion(casoObjetoAnalisis);
                    conGeneral = (int)p3.CONTADORGENERAL;
                    conDiaSemana = (int)p3.CONTADORDIASEMANA;
                    conDiaMes = (int)p3.CONTADORDIAMES;
                    conDiaMod = (int)p3.CONTADORDIAMODULO;
                    conMes = (int)p3.CONTADORMES;
                    break;
                case 4:
                    POS_CUATRO_DATOS p4 = (POS_CUATRO_DATOS)this.ObtenerUltimoObjetoPosicion(casoObjetoAnalisis);
                    conGeneral = (int)p4.CONTADORGENERAL;
                    conDiaSemana = (int)p4.CONTADORDIASEMANA;
                    conDiaMes = (int)p4.CONTADORDIAMES;
                    conDiaMod = (int)p4.CONTADORDIAMODULO;
                    conMes = (int)p4.CONTADORMES;
                    break;
                default:
                    break;
            }
            foreach (var item in dict)
            {
                if (comparaConGene && item.Value.RankContadorGeneral.Equals(conGeneral)) {
                    dictRevisar.Remove(item.Key);
                }
                if (comparaContDiaSem && item.Value.RankContadorDiaSemana.Equals(conDiaSemana)) {
                    dictRevisar.Remove(item.Key);
                }
                if (comparaContDiaMes && item.Value.RankContadorDiaMes.Equals(conDiaMes))
                {
                    dictRevisar.Remove(item.Key);
                }
                if (comparaContDiaMod && item.Value.RankContadorDiaModulo.Equals(conDiaMod)) {
                    dictRevisar.Remove(item.Key);
                }
                if (comparaConMes && item.Value.RankContadorMes.Equals(conMes)) {
                    dictRevisar.Remove(item.Key);
                }
            }
            this.EscribirDatosArchivo(dictRevisar, nombreArchivo);
            this.EliminarDatoSumatoria(dict, nombreArchivo + "11", casoObjetoAnalisis);
        }

        private void EliminarDatoSumatoria(Dictionary<int, ObjectInfoDTO> dict, string nombreArchivo, int tablaPosicion)
        {
            var dictRevisar = dict.ToDictionary(x => x.Key, x => x.Value);
            int mes = this.fecha.Month;
            int total = 0;
            int comparadorMinimo = this.ObtenerDatoComparar(tablaPosicion, false);
            int comparadorMaximo = this.ObtenerDatoComparar(tablaPosicion, true);
            foreach (var item in dict)
            {
                total = item.Value.RankContadorGeneral + item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes 
                    + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.RankContadorDiaAnio 
                    + item.Value.RankContadorMesModuloDiaModulo
                    + item.Value.RankContadorAnioModulo + item.Value.RankContadorMesModulo;
                
                if (total> comparadorMaximo || total< comparadorMinimo)
                {
                    dictRevisar.Remove(item.Key);
                }
            }
            this.EscribirDatosArchivo(dictRevisar, nombreArchivo);
        }

        private Dictionary<int, ObjectInfoDTO> ValidarSumatoriaDatos(Dictionary<int, ObjectInfoDTO> dict, string nombreArchivo, string nombreTabla, string orden)
        {
            var dictRevisar = dict.ToDictionary(x => x.Key, x => x.Value);
            int total = 0;
            List<SumatoriaDatosDTO> lista = this.ConsultarSumatoriaDatos(nombreTabla, orden);
            List<int> enterosLista = (from x in lista select x.Valor).ToList();
            if (orden.Equals("ASC"))
            {
                foreach (var item in dict)
                {
                    total = item.Value.RankContadorGeneral + item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes
                        + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual;

                    if (enterosLista.IndexOf(total) != -1)
                    {
                        dictRevisar.Remove(item.Key);
                    }
                }
            }
            else
            {
                foreach (var item in dict)
                {
                    total = item.Value.RankContadorGeneral + item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes
                        + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual;

                    if (enterosLista.IndexOf(total) == -1)
                    {
                        dictRevisar.Remove(item.Key);
                    }
                }
            }
            this.EscribirDatosArchivo(dictRevisar, nombreArchivo);
            //this.ValidarSumatoriaDatos(dictRevisar, nombreArchivo + "Mayores", ConstantesGenerales.SIGN_DATOS, "DESC",12);
            return dictRevisar;
        }

        private Dictionary<string, ObjectInfoDTO> ValidarSumatoriaDatos(Dictionary<string, ObjectInfoDTO> dict, string nombreArchivo, string nombreTabla, string orden)
        {
            var dictRevisar = dict.ToDictionary(x => x.Key, x => x.Value);
            int total = 0;
            List<SumatoriaDatosDTO> lista = this.ConsultarSumatoriaDatos(nombreTabla, orden);
            List<int> enterosLista = (from x in lista select x.Valor).ToList();
            if (orden.Equals("ASC"))
            {
                foreach (var item in dict)
                {
                    total = item.Value.RankContadorGeneral + item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes
                        + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual;

                    if (enterosLista.IndexOf(total) != -1)
                    {
                        dictRevisar.Remove(item.Key);
                    }
                }
            }
            else
            {
                foreach (var item in dict)
                {
                    total = item.Value.RankContadorGeneral + item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes
                        + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.ContadorDespuesActual;

                    if (enterosLista.IndexOf(total) == -1)
                    {
                        dictRevisar.Remove(item.Key);
                    }
                }
            }
            this.EscribirDatosArchivo(dictRevisar, nombreArchivo);
            //this.ValidarSumatoriaDatos(dictRevisar, nombreArchivo + "Mayores", ConstantesGenerales.SIGN_DATOS, "DESC", 12);
            return dictRevisar;
        }

        private int ObtenerDatoComparar(int tablaPosicion, bool maximo)
        {
            if (maximo)
            {
                switch (tablaPosicion)
                {
                    case 1:
                        return ConstantesMaxMin.MAX_MEDIDOR_POS_UNO;
                    case 2:
                        return ConstantesMaxMin.MAX_MEDIDOR_POS_DOS;
                    case 3:
                        return ConstantesMaxMin.MAX_MEDIDOR_POS_TRES;
                    case 4:
                        return ConstantesMaxMin.MAX_MEDIDOR_POS_CUATRO;
                    case 5:
                        return ConstantesMaxMin.MAX_MEDIDOR_SIGN;
                }
            }else
            {
                switch (tablaPosicion)
                {
                    case 1:
                        return ConstantesMaxMin.MIN_MEDIDOR_POS_UNO;
                    case 2:
                        return ConstantesMaxMin.MIN_MEDIDOR_POS_DOS;
                    case 3:
                        return ConstantesMaxMin.MIN_MEDIDOR_POS_TRES;
                    case 4:
                        return ConstantesMaxMin.MIN_MEDIDOR_POS_CUATRO;
                    case 5:
                        return ConstantesMaxMin.MIN_MEDIDOR_SIGN;
                }

            }
            return 0;
        }

        private void EliminarDatoSumatoria(Dictionary<string, ObjectInfoDTO> dict, string nombreArchivo, int tablaPosicion)
        {
            var dictRevisar = dict.ToDictionary(x => x.Key, x => x.Value);
            int mes = this.fecha.Month;
            int total = 0;
            int comparadorMinimo = this.ObtenerDatoComparar(tablaPosicion, false);
            int comparadorMaximo = this.ObtenerDatoComparar(tablaPosicion, true);
            foreach (var item in dict)
            {
                total = item.Value.RankContadorGeneral + item.Value.RankContadorDiaSemana + item.Value.RankContadorDiaMes
                    + item.Value.RankContadorDiaModulo + item.Value.RankContadorMes + item.Value.RankContadorDiaAnio
                    + item.Value.RankContadorMesModuloDiaModulo
                    + item.Value.RankContadorAnioModulo + item.Value.RankContadorMesModulo;

                if (total > comparadorMaximo || total < comparadorMinimo)
                {
                    dictRevisar.Remove(item.Key);
                }
            }
            this.EscribirDatosArchivo(dictRevisar, nombreArchivo);
        }

        private void EliminarMismoDato(Dictionary<string, ObjectInfoDTO> dict, string nombreArchivo, string tabla, int casoObjetoAnalisis)
        {
            List<QueryInfoMismoDato> listCOMPARA_CONT_GENERAL = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_GENERAL);
            List<QueryInfoMismoDato> listCOMPARA_CONT_DIA_SEM = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_DIA_SEM);
            List<QueryInfoMismoDato> listCOMPARA_CONT_DIA_MES = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_DIA_MES);
            List<QueryInfoMismoDato> listCOMPARA_CONT_DIA_MOD = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_DIA_MOD);
            List<QueryInfoMismoDato> listCOMPARA_CONT_MES = this.ConsultarDatosAgrupados(tabla, ConstantesGenerales.COMPARA_CONT_MES);
            var dictRevisar = dict.ToDictionary(x => x.Key, x => x.Value);
            int mes = this.fecha.Month;
            bool comparaConGene = this.ObtenerIndexElemento(listCOMPARA_CONT_GENERAL, mes);
            bool comparaContDiaSem = this.ObtenerIndexElemento(listCOMPARA_CONT_DIA_SEM, mes);
            bool comparaContDiaMes = this.ObtenerIndexElemento(listCOMPARA_CONT_DIA_MES, mes);
            bool comparaContDiaMod = this.ObtenerIndexElemento(listCOMPARA_CONT_DIA_MOD, mes);
            bool comparaConMes = this.ObtenerIndexElemento(listCOMPARA_CONT_MES, mes);
            SIGN_DATOS p4 = (SIGN_DATOS)this.ObtenerUltimoObjetoPosicion(casoObjetoAnalisis);
            int conGeneral = (int)p4.CONTADORGENERAL;
            int conDiaSemana = (int)p4.CONTADORDIASEMANA;
            int conDiaMes = (int)p4.CONTADORDIAMES;
            int conDiaMod = (int)p4.CONTADORDIAMODULO;
            int conMes = (int)p4.CONTADORMES;

            foreach (var item in dict)
            {
                if (comparaConGene && item.Value.RankContadorGeneral.Equals(conGeneral))
                {
                    dictRevisar.Remove(item.Key);
                }
                if (comparaContDiaSem && item.Value.RankContadorDiaSemana.Equals(conDiaSemana))
                {
                    dictRevisar.Remove(item.Key);
                }
                if (comparaContDiaMes && item.Value.RankContadorDiaMes.Equals(conDiaMes))
                {
                    dictRevisar.Remove(item.Key);
                }
                if (comparaContDiaMod && item.Value.RankContadorDiaModulo.Equals(conDiaMod))
                {
                    dictRevisar.Remove(item.Key);
                }
                if (comparaConMes && item.Value.RankContadorMes.Equals(conMes))
                {
                    dictRevisar.Remove(item.Key);
                }
            }
            this.EscribirDatosArchivo(dictRevisar, nombreArchivo);
            this.EliminarDatoSumatoria(dict, nombreArchivo + "11", casoObjetoAnalisis);
        }

        private bool ObtenerIndexElemento(List<QueryInfoMismoDato> lista, int datoComparar)
        {
            for (var i=0; i<lista.Count(); i++)
            {
                int elMes = Convert.ToInt32(lista.ElementAt(i).Mes);
                if (elMes.Equals(datoComparar))
                {
                    return true;
                }
            }
            return false;
        }

        private List<SumatoriaDatosDTO> ConsultarSumatoriaDatos(string tabla, string orden)
        {
            string fechaFormat = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            string consulta = string.Format(ConstantesGenerales.QUERY_SUMATORIA_DATOS, tabla, orden, fechaFormat);
            DbRawSqlQuery<SumatoriaDatosDTO> data = _astEntities.Database.SqlQuery<SumatoriaDatosDTO>(consulta);
            int totalDatos = (data.AsEnumerable().Count()*40) / 100;
            return data.AsEnumerable().Take(totalDatos).ToList();
        }

        private List<QueryInfoMismoDato> ConsultarDatosAgrupados(string tabla, string columna)
        {
            string consulta = string.Format(ConstantesGenerales.QUERY_MISMO_DATO_ANDATOS, tabla, columna);
            DbRawSqlQuery<QueryInfoMismoDato> data = _astEntities.Database.SqlQuery<QueryInfoMismoDato>(consulta);
            return data.AsEnumerable().Take(3).ToList();
        }

        /// <summary>
        /// Método que marca una posición, cero o uno, de acuerdo al valor recibido en el dato
        /// si coincide con la clave, marca la posición como uno, de lo contrario marca cero
        /// </summary>
        /// <param name="dict">Diccionario con valos valores de las posiciones</param>
        /// <param name="dato">dato que se buscará en la clave del diccionario</param>
        private void AdicionarElementoDiccionarioRachas(Dictionary<int, ObjectInfoDTO> dict, int dato)
        {
            foreach (var item in dict)
            {
                if (item.Key.Equals(dato))
                {
                    item.Value.RachasAparicion.Add(1);
                }
                else
                {
                    item.Value.RachasAparicion.Add(0);
                }
            }
        }

        /// <summary>
        /// Método que marca una posición, cero o uno, de acuerdo al valor recibido en el dato
        /// si coincide con la clave, marca la posición como uno, de lo contrario marca cero
        /// </summary>
        /// <param name="dict">Diccionario con valos valores de las posiciones</param>
        /// <param name="dato">dato que se buscará en la clave del diccionario</param>
        private void AdicionarElementoDiccionarioRachasSign(Dictionary<string, ObjectInfoDTO> dict, string sign)
        {
            foreach (var item in dict)
            {
                if (item.Key.Equals(sign))
                {
                    item.Value.RachasAparicion.Add(1);
                }
                else
                {
                    item.Value.RachasAparicion.Add(0);
                }
            }
        }

        /// <summary>
        /// Método que realiza la consulta de los valores agrupados para las coincidencias de un valor siguiente
        /// </summary>
        /// <param name="columna">Columna de la tabla sobre la que se realiza la consulta</param>
        /// <param name="tablaValidar">Tabla para validar los datos</param>
        /// <param name="valorComparar">Valor sobre el que se realiza la comparacion</param>
        /// <returns></returns>
        private List<QueryInfo> AgruparContadoresDespuesActual(string columna, string tablaValidar, int valorComparar)
        {
            string fechaFormat = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            string consulta = string.Format(ConstantesGenerales.QUERY_AGRUPAR_APARICIONES_DESP_ACTUAL, columna, tablaValidar, valorComparar, fechaFormat);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(consulta);
            return data.AsEnumerable().ToList();
        }

        /// <summary>
        /// Método encargado de contar y agrupar las rachas para cada valor
        /// </summary>
        /// <param name="dict">diccionario con información a ser agrupada</param>
        private void AgruparRachas(Dictionary<int, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                foreach (var itemList in item.Value.RachasAcumuladas)
                {
                    if (item.Value.DictRachasAgrupadasInt.ContainsKey(itemList))
                    {
                        item.Value.DictRachasAgrupadasInt[itemList]++;
                    }
                    else
                    {
                        item.Value.DictRachasAgrupadasInt.Add(itemList, 1);
                    }
                }
                var sortedDict = from entry in item.Value.DictRachasAgrupadasInt orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasInt = sortedDict.ToDictionary(x => x.Key, x => x.Value);

                foreach (var itemList in item.Value.RachasAcumuladasDespActual)
                {
                    if (item.Value.DictRachasAgrupadasIntDespActual.ContainsKey(itemList))
                    {
                        item.Value.DictRachasAgrupadasIntDespActual[itemList]++;
                    }
                    else
                    {
                        item.Value.DictRachasAgrupadasIntDespActual.Add(itemList, 1);
                    }
                }
                sortedDict = from entry in item.Value.DictRachasAgrupadasIntDespActual orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasIntDespActual = sortedDict.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        /// Método encargado de contar y agrupar las rachas para cada valor
        /// </summary>
        /// <param name="dict">diccionario con información a ser agrupada</param>
        private void AgruparRachas(Dictionary<string, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                foreach (var itemList in item.Value.RachasAcumuladas)
                {
                    if (item.Value.DictRachasAgrupadasInt.ContainsKey(itemList))
                    {
                        item.Value.DictRachasAgrupadasInt[itemList]++;
                    }
                    else
                    {
                        item.Value.DictRachasAgrupadasInt.Add(itemList, 1);
                    }
                }
                var sortedDict = from entry in item.Value.DictRachasAgrupadasInt orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasInt = sortedDict.ToDictionary(x => x.Key, x => x.Value);

                foreach (var itemList in item.Value.RachasAcumuladasDespActual)
                {
                    if (item.Value.DictRachasAgrupadasIntDespActual.ContainsKey(itemList))
                    {
                        item.Value.DictRachasAgrupadasIntDespActual[itemList]++;
                    }
                    else
                    {
                        item.Value.DictRachasAgrupadasIntDespActual.Add(itemList, 1);
                    }
                }
                sortedDict = from entry in item.Value.DictRachasAgrupadasIntDespActual orderby entry.Value descending select entry;
                item.Value.DictRachasAgrupadasIntDespActual = sortedDict.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        /// <summary>
        /// Método que realiza el conteo en la lista de datos recibidos, de las coincidencias encontradas
        /// </summary>
        /// <param name="valorComparar"></param>
        /// <param name="listaDatos"></param>
        /// <returns></returns>
        private int ContadorValoresUltimasApariciones(int valorComparar, List<int> listaDatos)
        {
            return listaDatos.Where(x=>x == valorComparar).ToList().Count;
        }

        /// <summary>
        /// Método que realiza el conteo de los valores sucesivos que son iguales
        /// </summary>
        /// <param name="dict">diccionario que contiene la información</param>
        private void ContarRachasPositivasNegativas(Dictionary<int, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                int contNegativo = 0;
                int contPositivo = 0;
                this.RecorrerListaRachas(dict, item.Key, ref contNegativo, ref contPositivo, item.Value.RachasAparicion, 1);
                if (contNegativo != 0)
                {
                    dict[item.Key].RachasAcumuladas.Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dict[item.Key].RachasAcumuladas.Add(contPositivo);
                }
                dict[item.Key].RachasAparicion.Clear();
            }
            foreach (var item in dict)
            {
                int contNegativo = 0;
                int contPositivo = 0;
                this.RecorrerListaRachas(dict, item.Key, ref contNegativo, ref contPositivo, item.Value.RachasAparicionDespActual, 2);
                if (contNegativo != 0)
                {
                    dict[item.Key].RachasAcumuladasDespActual.Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dict[item.Key].RachasAcumuladasDespActual.Add(contPositivo);
                }
                dict[item.Key].RachasAparicionDespActual.Clear();
            }
        }

        /// <summary>
        /// Método que realiza el conteo de los valores sucesivos que son iguales
        /// </summary>
        /// <param name="dict">diccionario que contiene la información</param>
        private void ContarRachasPositivasNegativas(Dictionary<string, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                int contNegativo = 0;
                int contPositivo = 0;
                this.RecorrerListaRachas(dict, item.Key, ref contNegativo, ref contPositivo, item.Value.RachasAparicion, 1);
                if (contNegativo != 0)
                {
                    dict[item.Key].RachasAcumuladas.Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dict[item.Key].RachasAcumuladas.Add(contPositivo);
                }
                dict[item.Key].RachasAparicion.Clear();
            }
            foreach (var item in dict)
            {
                int contNegativo = 0;
                int contPositivo = 0;
                this.RecorrerListaRachas(dict, item.Key, ref contNegativo, ref contPositivo, item.Value.RachasAparicionDespActual, 2);
                if (contNegativo != 0)
                {
                    dict[item.Key].RachasAcumuladasDespActual.Add(contNegativo);
                }
                else if (contPositivo != 0)
                {
                    dict[item.Key].RachasAcumuladasDespActual.Add(contPositivo);
                }
                dict[item.Key].RachasAparicionDespActual.Clear();
            }
        }

        /// <summary>
        /// Método que realiza la escritura de los datos anteriores obtenidos
        /// </summary>
        /// <param name="ultimoResultado">Valor que sirve para comparar y obtener los datos guardados</param>
        private void EscribirDatosAnterior(ASTR ultimoResultado)
        {
            DATOS_TEMP dtPosUno = this._astEntities.DATOS_TEMP.Where(x => x.POSICION == 1 && x.TIPO == ultimoResultado.TIPO && x.FECHA < this.fecha && x.CLAVE == ultimoResultado.POS_UNO).OrderByDescending(x => x.FECHA).ToList().FirstOrDefault();
            DATOS_TEMP dtPosDos = this._astEntities.DATOS_TEMP.Where(x => x.POSICION == 2 && x.TIPO == ultimoResultado.TIPO && x.FECHA < this.fecha && x.CLAVE == ultimoResultado.POS_DOS).OrderByDescending(x => x.FECHA).ToList().FirstOrDefault();
            DATOS_TEMP dtPosTres = this._astEntities.DATOS_TEMP.Where(x => x.POSICION == 3 && x.TIPO == ultimoResultado.TIPO && x.FECHA < this.fecha && x.CLAVE == ultimoResultado.POS_TRES).OrderByDescending(x => x.FECHA).ToList().FirstOrDefault();
            DATOS_TEMP dtPosCuatro = this._astEntities.DATOS_TEMP.Where(x => x.POSICION == 4 && x.TIPO == ultimoResultado.TIPO && x.FECHA < this.fecha && x.CLAVE == ultimoResultado.POS_CUATRO).OrderByDescending(x => x.FECHA).ToList().FirstOrDefault();
            DATOS_TEMP dtPosSign = this._astEntities.DATOS_TEMP.Where(x => x.POSICION == 5 && x.TIPO == ultimoResultado.TIPO && x.FECHA < this.fecha && x.CLAVESIGN == ultimoResultado.SIGN).OrderByDescending(x => x.FECHA).ToList().FirstOrDefault();
            if (dtPosUno != null)
            {
                POS_UNO_DATOS p = new POS_UNO_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = (int)dtPosUno.PUNTUACIONTOTAL;
                p.CONTADORULTIMOENRACHAS = (int)dtPosUno.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = (int)dtPosUno.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = (int)dtPosUno.CONTADORGENERAL;
                p.CONTADORDIASEMANA = (int)dtPosUno.CONTADORDIASEMANA;
                p.CONTADORDIAMES = (int)dtPosUno.CONTADORDIAMES;
                p.CONTADORDIAMODULO = (int)dtPosUno.CONTADORDIAMODULO;
                p.CONTADORMES = (int)dtPosUno.CONTADORMES;
                p.CONTADORDIAANIO = (int)dtPosUno.CONTADORDIAANIO;
                p.CONTADORDIAANIOMODULO = (int)dtPosUno.CONTADORDIAANIOMODULO;
                p.CONTADORMESMODULODIAMODULO = (int)dtPosUno.CONTADORMESMODULODIAMODULO;
                p.CONTADORMESDIA = (int)dtPosUno.CONTADORMESDIA;
                p.CONTADORANIOMODULO = (int)dtPosUno.CONTADORANIOMODULO;
                p.CONTADORMESMODULO = (int)dtPosUno.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = (int)dtPosUno.CONTADORDESPUESACTUAL;
                p.CONTADORDESPUESOTROTIPO = (int)dtPosUno.CONTADORDESPUESOTROTIPO;
                p.CONTADORDESPUESSIGNACTUAL = (int)dtPosUno.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosUno.FECHA;
                this._astEntities.POS_UNO_DATOS.Add(p);
            }
            if (dtPosDos != null)
            {
                POS_DOS_DATOS p = new POS_DOS_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = (int)dtPosDos.PUNTUACIONTOTAL;
                p.CONTADORULTIMOENRACHAS = (int)dtPosDos.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = (int)dtPosDos.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = (int)dtPosDos.CONTADORGENERAL;
                p.CONTADORDIASEMANA = (int)dtPosDos.CONTADORDIASEMANA;
                p.CONTADORDIAMES = (int)dtPosDos.CONTADORDIAMES;
                p.CONTADORDIAMODULO = (int)dtPosDos.CONTADORDIAMODULO;
                p.CONTADORMES = (int)dtPosDos.CONTADORMES;
                p.CONTADORDIAANIO = (int)dtPosDos.CONTADORDIAANIO;
                p.CONTADORDIAANIOMODULO = (int)dtPosDos.CONTADORDIAANIOMODULO;
                p.CONTADORMESMODULODIAMODULO = (int)dtPosDos.CONTADORMESMODULODIAMODULO;
                p.CONTADORMESDIA = (int)dtPosDos.CONTADORMESDIA;
                p.CONTADORANIOMODULO = (int)dtPosDos.CONTADORANIOMODULO;
                p.CONTADORMESMODULO = (int)dtPosDos.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = (int)dtPosDos.CONTADORDESPUESACTUAL;
                p.CONTADORDESPUESOTROTIPO = (int)dtPosDos.CONTADORDESPUESOTROTIPO;
                p.CONTADORDESPUESSIGNACTUAL = (int)dtPosDos.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosDos.FECHA;
                this._astEntities.POS_DOS_DATOS.Add(p);
            }
            if (dtPosTres != null)
            {
                POS_TRES_DATOS p = new POS_TRES_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = (int)dtPosTres.PUNTUACIONTOTAL;
                p.CONTADORULTIMOENRACHAS = (int)dtPosTres.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = (int)dtPosTres.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = (int)dtPosTres.CONTADORGENERAL;
                p.CONTADORDIASEMANA = (int)dtPosTres.CONTADORDIASEMANA;
                p.CONTADORDIAMES = (int)dtPosTres.CONTADORDIAMES;
                p.CONTADORDIAMODULO = (int)dtPosTres.CONTADORDIAMODULO;
                p.CONTADORMES = (int)dtPosTres.CONTADORMES;
                p.CONTADORDIAANIO = (int)dtPosTres.CONTADORDIAANIO;
                p.CONTADORDIAANIOMODULO = (int)dtPosTres.CONTADORDIAANIOMODULO;
                p.CONTADORMESMODULODIAMODULO = (int)dtPosTres.CONTADORMESMODULODIAMODULO;
                p.CONTADORMESDIA = (int)dtPosTres.CONTADORMESDIA;
                p.CONTADORANIOMODULO = (int)dtPosTres.CONTADORANIOMODULO;
                p.CONTADORMESMODULO = (int)dtPosTres.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = (int)dtPosTres.CONTADORDESPUESACTUAL;
                p.CONTADORDESPUESOTROTIPO = (int)dtPosTres.CONTADORDESPUESOTROTIPO;
                p.CONTADORDESPUESSIGNACTUAL = (int)dtPosTres.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosTres.FECHA;
                this._astEntities.POS_TRES_DATOS.Add(p);
            }
            if (dtPosCuatro != null)
            {
                POS_CUATRO_DATOS p = new POS_CUATRO_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = (int)dtPosCuatro.PUNTUACIONTOTAL;
                p.CONTADORULTIMOENRACHAS = (int)dtPosCuatro.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = (int)dtPosCuatro.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = (int)dtPosCuatro.CONTADORGENERAL;
                p.CONTADORDIASEMANA = (int)dtPosCuatro.CONTADORDIASEMANA;
                p.CONTADORDIAMES = (int)dtPosCuatro.CONTADORDIAMES;
                p.CONTADORDIAMODULO = (int)dtPosCuatro.CONTADORDIAMODULO;
                p.CONTADORMES = (int)dtPosCuatro.CONTADORMES;
                p.CONTADORDIAANIO = (int)dtPosCuatro.CONTADORDIAANIO;
                p.CONTADORDIAANIOMODULO = (int)dtPosCuatro.CONTADORDIAANIOMODULO;
                p.CONTADORMESMODULODIAMODULO = (int)dtPosCuatro.CONTADORMESMODULODIAMODULO;
                p.CONTADORMESDIA = (int)dtPosCuatro.CONTADORMESDIA;
                p.CONTADORANIOMODULO = (int)dtPosCuatro.CONTADORANIOMODULO;
                p.CONTADORMESMODULO = (int)dtPosCuatro.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = (int)dtPosCuatro.CONTADORDESPUESACTUAL;
                p.CONTADORDESPUESOTROTIPO = (int)dtPosCuatro.CONTADORDESPUESOTROTIPO;
                p.CONTADORDESPUESSIGNACTUAL = (int)dtPosCuatro.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosCuatro.FECHA;
                this._astEntities.POS_CUATRO_DATOS.Add(p);
            }
            if (dtPosSign != null)
            {
                SIGN_DATOS p = new SIGN_DATOS();
                p.TIPO = ultimoResultado.TIPO;
                p.ID = this.ObtenerValorSecuencia(p);
                p.PUNTUACIONTOTAL = (int)dtPosSign.PUNTUACIONTOTAL;
                p.CONTADORULTIMOENRACHAS = (int)dtPosSign.CONTADORULTIMOENRACHAS;
                p.CONTADORULTIMOENRACHASDESACTUA = (int)dtPosSign.CONTADORULTIMOENRACHASDESACTUA;
                p.CONTADORGENERAL = (int)dtPosSign.CONTADORGENERAL;
                p.CONTADORDIASEMANA = (int)dtPosSign.CONTADORDIASEMANA;
                p.CONTADORDIAMES = (int)dtPosSign.CONTADORDIAMES;
                p.CONTADORDIAMODULO = (int)dtPosSign.CONTADORDIAMODULO;
                p.CONTADORMES = (int)dtPosSign.CONTADORMES;
                p.CONTADORDIAANIO = (int)dtPosSign.CONTADORDIAANIO;
                p.CONTADORDIAANIOMODULO = (int)dtPosSign.CONTADORDIAANIOMODULO;
                p.CONTADORMESMODULODIAMODULO = (int)dtPosSign.CONTADORMESMODULODIAMODULO;
                p.CONTADORMESDIA = (int)dtPosSign.CONTADORMESDIA;
                p.CONTADORANIOMODULO = (int)dtPosSign.CONTADORANIOMODULO;
                p.CONTADORMESMODULO = (int)dtPosSign.CONTADORMESMODULO;
                p.CONTADORDESPUESACTUAL = (int)dtPosSign.CONTADORDESPUESACTUAL;
                p.CONTADORDESPUESOTROTIPO = (int)dtPosSign.CONTADORDESPUESOTROTIPO;
                p.CONTADORDESPUESSIGNACTUAL = (int)dtPosSign.CONTADORDESPUESSIGNACTUAL;
                p.FECHA = dtPosSign.FECHA;
                this._astEntities.SIGN_DATOS.Add(p);
            }
            this._astEntities.SaveChanges();
        }

        /// <summary>
        /// Método que escribe los datos recibidos en el diccionario en los archivos
        /// </summary>
        /// <param name="dict">diccionario con datos</param>
        /// <param name="cad">cadena que hace parte del nombre del archivo a escribir</param>
        private void EscribirDatosArchivo(Dictionary<int, ObjectInfoDTO> dict, string cad)
        {
            string fic = path + fecha.Day + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            sw.WriteLine(ConstantesGenerales.ENCABEZADOS);
            foreach (var item in dict)
            {
                sw.Write(item.Key);
                sw.WriteLine(item.Value.ToString());
            }
            sw.Close();
        }

        /// <summary>
        /// Método que escribe los datos recibidos en el diccionario en los archivos
        /// </summary>
        /// <param name="dict">diccionario con datos</param>
        /// <param name="cad">cadena que hace parte del nombre del archivo a escribir</param>
        private void EscribirDatosArchivo(Dictionary<string, ObjectInfoDTO> dict, string cad)
        {
            string fic = path + fecha.Day + cad + ".csv";
            StreamWriter sw = new StreamWriter(fic);
            sw.WriteLine(ConstantesGenerales.ENCABEZADOS);
            foreach (var item in dict)
            {
                sw.Write(item.Key);
                sw.WriteLine(item.Value.ToString());
            }
            sw.Close();
        }

        /// <summary>
        /// Método que realiza el guardado de los datos analizados para la fecha
        /// </summary>
        /// <param name="tipo">Valor que indica el tipo sor</param>
        /// <param name="posicion">Valor que indica la posicion dentro del objeto (pos_uno, pos_dos,...)</param>
        /// <param name="dict">Diccionario de datos para analizar</param>
        private void GuardarDatosTemporales(int tipo, int posicion, Dictionary<int, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                DATOS_TEMP dt = this.ObtenerObjetoGuardar(item.Value);
                dt.CLAVE = item.Key;
                dt.TIPO = tipo;
                dt.POSICION = posicion;
                dt.FECHA = this.fecha;
                dt.ID = this.ObtenerValorSecuencia(dt);
                this._astEntities.DATOS_TEMP.Add(dt);
            }
            this._astEntities.SaveChanges();
        }

        /// <summary>
        /// Método que realiza el guardado de los datos analizados para la fecha
        /// </summary>
        /// <param name="tipo">Valor que indica el tipo sor</param>
        /// <param name="posicion">Valor que indica la posicion dentro del objeto (pos_uno, pos_dos,...)</param>
        /// <param name="dict">Diccionario de datos para analizar</param>
        private void GuardarDatosTemporales(int tipo, int posicion, Dictionary<string, ObjectInfoDTO> dict)
        {
            foreach (var item in dict)
            {
                DATOS_TEMP dt = this.ObtenerObjetoGuardar(item.Value);
                dt.CLAVESIGN = item.Key;
                dt.TIPO = tipo;
                dt.POSICION = posicion;
                dt.FECHA = this.fecha;
                dt.ID = this.ObtenerValorSecuencia(dt);
                this._astEntities.DATOS_TEMP.Add(dt);
            }
            this._astEntities.SaveChanges();
        }

        /// <summary>
        /// Método que realiza el incremento para el diccionario, si la bandera recibida es true;
        /// </summary>
        /// <param name="dictIncrementar">Diccionaro que contiene la información sobre la que se realizan los incrementos</param>
        /// <param name="bandera">bandera que realiza la validación, y controla en incremento</param>
        /// <param name="claveDict">clave del diccionario sobre la que se realiza el incremento</param>
        private void IncrementarContador(Dictionary<int, ObjectInfoDTO> dictIncrementar, bool bandera, int claveDict, bool mismoTipo)
        {
            if (bandera)
            {
                if (mismoTipo)
                {
                    foreach (var item in dictIncrementar)
                    {
                        if (item.Key.Equals(claveDict))
                        {
                            dictIncrementar[item.Key].RachasAparicionDespActual.Add(1);
                        }
                        else
                        {
                            dictIncrementar[item.Key].RachasAparicionDespActual.Add(0);
                        }
                    }
                }
                else
                {
                    dictIncrementar[claveDict].ContadorDespuesOtroTipo++;
                }
                dictIncrementar[claveDict].PuntuacionTotal++;
            }
        }

        /// <summary>
        /// Método que valida las condiciones para ingreso de elementos a la lista
        /// </summary>
        /// <param name="dict">diccionario que contiene la información a donde se ingresan los datos</param>
        /// <param name="claveDict">clave que sirve para ingresar los datos al diccionario</param>
        /// <param name="contNegativo">Elemento que referencia al contador negativo</param>
        /// <param name="contPositivo">Elemento que referencia al contador positivo</param>
        /// <param name="caso">caso que indica a la lista a la que se agregan los elementos contados</param>
        /// <param name="anterior">Referencia al valor anterior asignado</param>
        /// <param name="comparador">Referencia al dato con el cual se compara</param>
        /// <param name="elementoAdicionar">elemento que se adicionará a la lista</param>
        private void IngresarElementoListaRachas(Dictionary<int, ObjectInfoDTO> dict, int claveDict, ref int contNegativo, ref int contPositivo, int caso, int? anterior, int comparador, int elementoAdicionar)
        {
            if (anterior.Equals(comparador) && caso == 1)
            {
                dict[claveDict].RachasAcumuladas.Add(elementoAdicionar);
                contPositivo = 0;
                contNegativo = 0;
            }
            else if (anterior.Equals(comparador) && caso == 2)
            {
                dict[claveDict].RachasAcumuladasDespActual.Add(elementoAdicionar);
                contPositivo = 0;
                contNegativo = 0;
            }
        }

        /// <summary>
        /// Método que valida las condiciones para ingreso de elementos a la lista
        /// </summary>
        /// <param name="dict">diccionario que contiene la información a donde se ingresan los datos</param>
        /// <param name="claveDict">clave que sirve para ingresar los datos al diccionario</param>
        /// <param name="contNegativo">Elemento que referencia al contador negativo</param>
        /// <param name="contPositivo">Elemento que referencia al contador positivo</param>
        /// <param name="caso">caso que indica a la lista a la que se agregan los elementos contados</param>
        /// <param name="anterior">Referencia al valor anterior asignado</param>
        /// <param name="comparador">Referencia al dato con el cual se compara</param>
        /// <param name="elementoAdicionar">elemento que se adicionará a la lista</param>
        private void IngresarElementoListaRachas(Dictionary<string, ObjectInfoDTO> dict, string claveDict, ref int contNegativo, ref int contPositivo, int caso, int? anterior, int comparador, int elementoAdicionar)
        {
            if (anterior.Equals(comparador) && caso == 1)
            {
                dict[claveDict].RachasAcumuladas.Add(elementoAdicionar);
                contPositivo = 0;
                contNegativo = 0;
            }
            else if (anterior.Equals(comparador) && caso == 2)
            {
                dict[claveDict].RachasAcumuladasDespActual.Add(elementoAdicionar);
                contPositivo = 0;
                contNegativo = 0;
            }
        }

        /// <summary>
        /// Método que retorna una estructura requerida para el caso recibido
        /// </summary>
        /// <param name="caso">opcion que sirve para controlar el tipo de objeto a devolver</param>
        /// <returns></returns>
        private object InicializarDiccionarioInformacion(int caso = 0)
        {
            if (caso == 0)
            {
                Dictionary<int, ObjectInfoDTO> dict = new Dictionary<int, ObjectInfoDTO>();
                for (int i = 0; i < 10; i++)
                {
                    dict.Add(i, new ObjectInfoDTO());
                }
                return dict;
            }
            else
            {
                Dictionary<string, ObjectInfoDTO> dict = new Dictionary<string, ObjectInfoDTO>();
                dict.Add(ConstantesGenerales.ACUARIO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.ARIES, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.CANCER, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.CAPRICORNIO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.ESCORPION, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.GEMINIS, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.LEO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.LIBRA, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.PISCIS, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.SAGITARIO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.TAURO, new ObjectInfoDTO());
                dict.Add(ConstantesGenerales.VIRGO, new ObjectInfoDTO());
                return dict;
            }

        }

        /// <summary>
        /// Método que realiza la construcción del objeto a guardar
        /// </summary>
        /// <param name="objeto">Objeto con información a guardar</param>
        /// <returns>objeto que se va a persistir</returns>
        private DATOS_TEMP ObtenerObjetoGuardar(ObjectInfoDTO objeto)
        {
            DATOS_TEMP dt = new DATOS_TEMP();
            dt.PUNTUACIONTOTAL = objeto.PuntuacionTotal;
            dt.CONTADORULTIMOENRACHAS = objeto.DictRachasAgrupadasInt.Where(x => x.Key == objeto.RachasAcumuladas.Last()).FirstOrDefault().Value;
            dt.CONTADORULTIMOENRACHASDESACTUA = objeto.DictRachasAgrupadasIntDespActual.Where(x => x.Key == objeto.RachasAcumuladasDespActual.Last()).FirstOrDefault().Value;
            dt.CONTADORGENERAL = objeto.RankContadorGeneral;
            dt.CONTADORDIASEMANA = objeto.RankContadorDiaSemana;
            dt.CONTADORDIAMES = objeto.RankContadorDiaMes;
            dt.CONTADORDIAMODULO = objeto.RankContadorDiaModulo;
            dt.CONTADORMES = objeto.RankContadorMes;
            dt.CONTADORDIAANIO = objeto.RankContadorDiaAnio;
            dt.CONTADORDIAANIOMODULO = objeto.RankContadorDiaAnioModulo;
            dt.CONTADORMESMODULODIAMODULO = objeto.RankContadorMesModuloDiaModulo;
            dt.CONTADORMESDIA = objeto.RankContadorMesDia;
            dt.CONTADORANIOMODULO = objeto.RankContadorAnioModulo;
            dt.CONTADORMESMODULO = objeto.RankContadorMesModulo;
            dt.CONTADORDESPUESACTUAL = objeto.ContadorDespuesActual;
            dt.CONTADORDESPUESOTROTIPO = objeto.ContadorDespuesOtroTipo;
            dt.CONTADORDESPUESSIGNACTUAL = objeto.ContadorDespuesSignActual;
            return dt;

        }

        /// <summary>
        /// Método que obtiene la consulta que se realiza
        /// </summary>
        /// <param name="caso">Caso que valida cual es la consulta que se debe retornar</param>
        /// <returns>Consulta generada</returns>
        private string ObtenerParametrosQuery(int caso)
        {
            switch (caso)
            {
                ///Agrupa los contadores de acuerdo al día de la semana
                case 1:
                    return "AND TO_CHAR(fecha, 'D') = " + ((int)fecha.DayOfWeek + 1);
                ///Agrupa los contadores de acuerdo al día del mes
                case 2:
                    return "AND TO_CHAR(fecha, 'DD') = " + fecha.Day;
                ///Agrupa los contadores de acuerdo al día par o impar
                case 3:
                    return "AND MOD(TO_CHAR(fecha, 'DD'),2) = " + (fecha.Day % 2);
                ///Agrupa los contadores de acuerdo al mes
                case 4:
                    return "AND TO_CHAR(fecha, 'MM') = " + fecha.Month;
                ///Agrupa los contadores de acuerdo al día del año
                case 5:
                    return "AND TO_CHAR(fecha, 'DDD') = " + (fecha.DayOfYear);
                ///Agrupa los contadores de acuerdo al día del año par o impar
                case 6:
                    return "AND MOD(TO_CHAR(fecha, 'DDD'),2) = " + (fecha.DayOfYear % 2);
                ///Agrupa los contadores de acuerdo al mes par o impar
                case 7:
                    return "AND MOD(TO_CHAR(fecha, 'MM'),2) = " + (fecha.Month % 2);
                ///Agrupa los contadores de acuerdo al mes par o impar y el día par o impar
                case 8:
                    return "AND MOD(TO_CHAR(fecha, 'MM'),2) = " + (fecha.Month % 2) + " AND MOD(TO_CHAR(fecha, 'DD'),2) = " + (fecha.Day % 2);
                ///Agrupa los contadores de acuerdo al mes y al día
                case 9:
                    return "AND TO_CHAR(fecha, 'MM') = " + fecha.Month + " AND TO_CHAR(fecha, 'DD') = " + fecha.Day;
                ///Agrupa los contadores de acuerdo al año par o impar
                case 10:
                    return "AND MOD(TO_CHAR(fecha, 'YYYY'),2) = " + (fecha.Year % 2);
                default:
                    return "";
            }
        }

        /// <summary>
        /// Método que permite 
        /// </summary>
        /// <param name="caso">variable que controla el tipo de objeto que debe devolverse</param>
        /// <returns></returns>
        private object ObtenerUltimoObjetoPosicion(int caso)
        {
            DateTime fechaAnterior = this.fecha.AddDays(-1);
            switch (caso)
            {
                case 1:
                    POS_UNO_DATOS pud = this._astEntities.POS_UNO_DATOS.Where(x => x.FECHA == fechaAnterior).ToList().FirstOrDefault();
                    return pud;
                case 2:
                    POS_DOS_DATOS pdd = this._astEntities.POS_DOS_DATOS.Where(x => x.FECHA == fechaAnterior).ToList().FirstOrDefault();
                    return pdd;
                case 3:
                    POS_TRES_DATOS ptd = this._astEntities.POS_TRES_DATOS.Where(x => x.FECHA == fechaAnterior).ToList().FirstOrDefault();
                    return ptd;
                case 4:
                    POS_CUATRO_DATOS pcd = this._astEntities.POS_CUATRO_DATOS.Where(x => x.FECHA == fechaAnterior).ToList().FirstOrDefault();
                    return pcd;
                case 5:
                    SIGN_DATOS sd = this._astEntities.SIGN_DATOS.Where(x => x.FECHA == fechaAnterior).ToList().FirstOrDefault();
                    return sd;
            }
            return null;
        }

        /// <summary>
        /// Método que obtiene y asigna los datos de los últimos resultados ingresados
        /// </summary>
        private void ObtenerUltimoResultado()
        {
           //_resultActualSol = listaDatosSol.Last();
            _resultActualLun = listaDatosLun.Last();
        }

        /// <summary>
        /// Método que retornar la lista de las últimas aapariciones de una columna y una tabla
        /// </summary>
        /// <param name="columna">columna de la que se requieren los datos</param>
        /// <param name="tablaValidar">tabla a la que se consulta</param>
        /// <returns>Lista de datos encontrados</returns>
        private List<int> ObtenerValoresActuales(string columna, string tablaValidar)
        {
            string fechaqFormato = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            string consulta = string.Format(ConstantesGenerales.QUERY_VER_APARICIONES_ACTUAL, columna, tablaValidar, fechaqFormato);
            DbRawSqlQuery<int> data = _astEntities.Database.SqlQuery<int>(consulta);
            return data.AsEnumerable().Take(20).ToList();
        }

        /// <summary>
        /// Método que permite obtener el valor minimo de un conjunto de datos validado, para indicar si es el menor del grupo
        /// </summary>
        /// <param name="tablaValidar">Tabla sobre la que se realiza la validación</param>
        /// <param name="columna">columna sobre la que se realiza la validación</param>
        /// <param name="posicion">variable que indica la posición que se va a validar</param>
        /// <returns>Valor consultado para los datos ingresados</returns>
        private int ObtenerValorMinimo(string tablaValidar, string columna, int posicion)
        {
            string fechaFormat = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            string consulta = string.Format(ConstantesGenerales.QUERY_MIN_DATO_DATA_TEMP, columna, tablaValidar, fechaFormat, ConstantesTipoSor.TIPO_LUN, posicion);
            DbRawSqlQuery<int> data = _astEntities.Database.SqlQuery<int>(consulta);
            return data.AsEnumerable().FirstOrDefault();
        }

        /// <summary>
        /// Método que obtiene el valor de la secuencia a guardar
        /// </summary>
        /// <param name="entidad">objeto que referencia a la secuencia</param>
        /// <returns>Valor obtenido para la secuencia</returns>
        private int ObtenerValorSecuencia(object entidad)
        {
            int valSecuencia = 0;
            string consultaSecuencia = "SELECT {0} FROM dual";
            string varSecuencia = string.Empty;
            if (entidad is DATOS_TEMP)
            {
                varSecuencia = "SQ_Datos_Temp.NEXTVAL";
            }
            else if (entidad is POS_UNO_DATOS)
            {
                varSecuencia = "SQ_POS_UNO_DATOS.NEXTVAL";
            }
            else if (entidad is POS_DOS_DATOS)
            {
                varSecuencia = "SQ_POS_DOS_DATOS.NEXTVAL";
            }
            else if (entidad is POS_TRES_DATOS)
            {
                varSecuencia = "SQ_POS_TRES_DATOS.NEXTVAL";
            }
            else if (entidad is POS_CUATRO_DATOS)
            {
                varSecuencia = "SQ_POS_CUATRO_DATOS.NEXTVAL";
            }
            else if (entidad is SIGN_DATOS)
            {
                varSecuencia = "SQ_SIGN_DATOS.NEXTVAL";
            }
            consultaSecuencia = string.Format(consultaSecuencia, varSecuencia);
            valSecuencia = (int)this._astEntities.Database.SqlQuery<decimal>(consultaSecuencia).ToList().Single();
            return valSecuencia;
        }

        /// <summary>
        /// Método que realiza el llamado al método que suma los datos para puntuar la información
        /// </summary>
        /// <param name="posicion">referencia la posición que se evalua dentro del registro (Pos_uno, Pos_dos...)</param>
        /// <param name="tipo">Referencia al tipo de registro que se evalua(Sol-Lun)</param>
        private void PuntuarInformacion(string posicion, int tipo, Dictionary<int, ObjectInfoDTO> dict, int datoActualPosicion)
        {
            string paramDate = " AND fecha < to_date('" + this.fecha.Day + "/" + this.fecha.Month + "/" + this.fecha.Year + "','dd/MM/yyyy') ";
            string query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(0), "ClaveNum", paramDate, 10);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContGeneral(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(1), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaSemana(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(2), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaMes(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(3), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(4), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMes(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(5), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaAnio(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(6), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AdinionarInformacionContadorDiaAnioModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(7), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(8), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesModuloDiaModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(9), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesDia(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(10), "ClaveNum", paramDate, 10);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContAnioModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_COUNT_DATOS_DESP_ACTUAL, posicion, "ClaveNum", 10, datoActualPosicion, paramDate);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDespActual(dict, data);
        }

        /// <summary>
        /// Método que realiza el llamado al método que suma los datos para puntuar la información
        /// </summary>
        /// <param name="posicion">referencia la posición que se evalua dentro del registro (Pos_uno, Pos_dos...)</param>
        /// <param name="tipo">Referencia al tipo de registro que se evalua(Sol-Lun)</param>
        private void PuntuarInformacion(string posicion, int tipo, Dictionary<string, ObjectInfoDTO> dict, string datoActualPosicion)
        {
            string paramDate = " AND fecha < to_date('" + this.fecha.Day + "/" + this.fecha.Month + "/" + this.fecha.Year + "','dd/MM/yyyy') ";
            string query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(0), "ClaveSign", paramDate, 12);
            DbRawSqlQuery<QueryInfo> data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContGeneral(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(1), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaSemana(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(2), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaMes(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(3), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(4), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMes(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(5), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDiaAnio(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(6), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AdinionarInformacionContadorDiaAnioModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(7), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(8), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesModuloDiaModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(9), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContMesDia(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_BASE, posicion, tipo, this.ObtenerParametrosQuery(10), "ClaveSign", paramDate, 12);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContAnioModulo(dict, data);
            query_final = string.Format(ConstantesGenerales.QUERY_COUNT_DATOS_DESP_ACTUAL_STRING, posicion, "ClaveSign", 12, datoActualPosicion, paramDate);
            data = _astEntities.Database.SqlQuery<QueryInfo>(query_final);
            ManejoContadores.AddInfoContDespActual(dict, data);
        }

        /// <summary>
        /// Método que recorre los elementos de la lista, y valida las apariciones siguientes
        /// </summary>
        /// <param name="listaValidar">lista que contiene los datos a validar</param>
        /// <param name="sorComparador">Elemento que sirve como comparador</param>
        /// <param name="dictPosUno">diccionario al que se realiza el incremento para la posición uno</param>
        /// <param name="dictPosDos">diccionario al que se realiza el incremento para la posición dos</param>
        /// <param name="dictPostres">diccionario al que se realiza el incremento para la posición tres</param>
        /// <param name="dictPosCuatro">diccionario al que se realiza el incremento para la posición cuatro</param>
        /// <param name="dictSign">diccionario al que se realiza el incremento para sign</param>
        private void RecorrerElementosLista(List<ASTR> listaValidar, ASTR sorComparador, Dictionary<int, ObjectInfoDTO> dictPosUno,
            Dictionary<int, ObjectInfoDTO> dictPosDos, Dictionary<int, ObjectInfoDTO> dictPostres, Dictionary<int, ObjectInfoDTO> dictPosCuatro,
            Dictionary<string, ObjectInfoDTO> dictSign)
        {
            bool flagPosUno = false;
            bool flagPosDos = false;
            bool flagPosTres = false;
            bool flagPosCuatro = false;
            bool flagSign = false;
            if (sorComparador.TIPO.Equals(listaValidar.ElementAt(0).TIPO))
            {
                foreach (var item in listaValidar)
                {
                    this.IncrementarContador(dictPosUno, flagPosUno, (int)item.POS_UNO, true);
                    this.IncrementarContador(dictPosDos, flagPosDos, (int)item.POS_DOS, true);
                    this.IncrementarContador(dictPostres, flagPosTres, (int)item.POS_TRES, true);
                    this.IncrementarContador(dictPosCuatro, flagPosCuatro, (int)item.POS_CUATRO, true);
                    if (flagSign)
                    {
                        dictSign[item.SIGN].PuntuacionTotal++;
                        dictPosUno[(int)item.POS_UNO].ContadorDespuesSignActual++;
                        dictPosUno[(int)item.POS_UNO].PuntuacionTotal++;
                        dictPosDos[(int)item.POS_DOS].ContadorDespuesSignActual++;
                        dictPosDos[(int)item.POS_DOS].PuntuacionTotal++;
                        dictPostres[(int)item.POS_TRES].ContadorDespuesSignActual++;
                        dictPostres[(int)item.POS_TRES].PuntuacionTotal++;
                        dictPosCuatro[(int)item.POS_CUATRO].ContadorDespuesSignActual++;
                        dictPosCuatro[(int)item.POS_CUATRO].PuntuacionTotal++;
                        foreach (var itemDict in dictSign)
                        {
                            if (itemDict.Key.Equals(item.SIGN))
                            {
                                dictSign[itemDict.Key].RachasAparicionDespActual.Add(1);
                            }
                            else
                            {
                                dictSign[itemDict.Key].RachasAparicionDespActual.Add(0);
                            }
                        }
                    }
                    flagPosUno = this.ValidarMismoDato(1, item, sorComparador);
                    flagPosDos = this.ValidarMismoDato(2, item, sorComparador);
                    flagPosTres = this.ValidarMismoDato(3, item, sorComparador);
                    flagPosCuatro = this.ValidarMismoDato(4, item, sorComparador);
                    flagSign = this.ValidarMismoDato(5, item, sorComparador);
                }
            }
            else
            {
                foreach (var item in listaValidar)
                {
                    this.IncrementarContador(dictPosUno, flagPosUno, (int)item.POS_UNO, false);
                    this.IncrementarContador(dictPosDos, flagPosDos, (int)item.POS_DOS, false);
                    this.IncrementarContador(dictPostres, flagPosTres, (int)item.POS_TRES, false);
                    this.IncrementarContador(dictPosCuatro, flagPosCuatro, (int)item.POS_CUATRO, false);
                    if (flagSign)
                    {
                        dictSign[item.SIGN].ContadorDespuesOtroTipo++;
                        dictSign[item.SIGN].PuntuacionTotal++;
                    }
                    flagPosUno = this.ValidarMismoDato(1, item, sorComparador);
                    flagPosDos = this.ValidarMismoDato(2, item, sorComparador);
                    flagPosTres = this.ValidarMismoDato(3, item, sorComparador);
                    flagPosCuatro = this.ValidarMismoDato(4, item, sorComparador);
                    flagSign = this.ValidarMismoDato(5, item, sorComparador);
                }
            }
        }

        /// <summary>
        /// Método que realiza el recorrido de los elementos de la lista para realizar el conteo de apariciones acumuladas
        /// </summary>
        /// <param name="dict">diccionario al que se realiza el ingreso de datos</param>
        /// <param name="claveDict">Clave a la que se ingresara los datos</param>
        /// <param name="contNegativo">Referencia al contador negativo</param>
        /// <param name="contPositivo">Referencia al contador positivo</param>
        /// <param name="listaRecorrer">lista de elementos a recorrer</param>
        /// <param name="caso">caso que permite identificar a cual lista se ingresan los datos 1 lista de apariciones, 2 lista de apariciones después del actual.</param>
        private void RecorrerListaRachas(Dictionary<int, ObjectInfoDTO> dict, int claveDict, ref int contNegativo, ref int contPositivo, List<int> listaRecorrer, int caso)
        {
            int? anterior = null;
            int comparador;
            foreach (var itemList in listaRecorrer)
            {
                ///Si el valor es cero, indica que no ha caido
                if (itemList.Equals(0))
                {
                    comparador = 1;
                    this.IngresarElementoListaRachas(dict, claveDict, ref contNegativo, ref contPositivo, caso, anterior, comparador, contPositivo);
                    contNegativo--;
                }
                else
                {
                    comparador = 0;
                    ///Si el valor es uno, indica que el valor cayó
                    this.IngresarElementoListaRachas(dict, claveDict, ref contNegativo, ref contPositivo, caso, anterior, comparador, contNegativo);
                    contPositivo++;
                }
                anterior = itemList;
            }
        }

        /// <summary>
        /// Método que realiza el recorrido de los elementos de la lista para realizar el conteo de apariciones acumuladas
        /// </summary>
        /// <param name="dict">diccionario al que se realiza el ingreso de datos</param>
        /// <param name="claveDict">Clave a la que se ingresara los datos</param>
        /// <param name="contNegativo">Referencia al contador negativo</param>
        /// <param name="contPositivo">Referencia al contador positivo</param>
        /// <param name="listaRecorrer">lista de elementos a recorrer</param>
        /// <param name="caso">caso que permite identificar a cual lista se ingresan los datos 1 lista de apariciones, 2 lista de apariciones después del actual.</param>
        private void RecorrerListaRachas(Dictionary<string, ObjectInfoDTO> dict, string claveDict, ref int contNegativo, ref int contPositivo, List<int> listaRecorrer, int caso)
        {
            int? anterior = null;
            int comparador;
            foreach (var itemList in listaRecorrer)
            {
                ///Si el valor es cero, indica que no ha caido
                if (itemList.Equals(0))
                {
                    comparador = 1;
                    this.IngresarElementoListaRachas(dict, claveDict, ref contNegativo, ref contPositivo, caso, anterior, comparador, contPositivo);
                    contNegativo--;
                }
                else
                {
                    comparador = 0;
                    ///Si el valor es uno, indica que el valor cayó
                    this.IngresarElementoListaRachas(dict, claveDict, ref contNegativo, ref contPositivo, caso, anterior, comparador, contNegativo);
                    contPositivo++;
                }
                anterior = itemList;
            }
        }

        /// <summary>
        /// Método que realiza la revisión de los valores mínimos y su verificación para limpiarlos del diccionario recibido
        /// </summary>
        /// <param name="dict">diccionario en el que se revisan los valores minimos</param>
        /// <param name="nombreArchivo">nombre del archivo de salida para la escritura de los datos</param>
        /// <param name="nombreTablaAnalisis">Nombre de la tabla donde se realiza el análisis de los datos</param>
        /// <param name="posicion">posición para la carga de datos</param>
        /// <param name="nombreTablaDatos">nombre de la tabla que contiene los datos a validar</param>
        private void RevisarValoresMinimos(Dictionary<string, ObjectInfoDTO> dict, string nombreArchivo, string nombreTablaAnalisis, int posicion, string nombreTablaDatos)
        {
            var temDict2 = dict.ToDictionary(x => x.Key, x => x.Value);
            int valorMinimoConUltimoRachas = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORULTIMOENRACHAS, posicion);
            bool espacioUltimoRachasMayor = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_ULT_RACH);
            int valorMinimoConGeneral = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORGENERAL, posicion);
            bool espacioGeneral = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_GENERAL);
            int valorMinimoConDiaSemana = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORDIASEMANA, posicion);
            bool espacioDiaSemana = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_DIA_SEM);
            int valorMinimoConDiaMes = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORDIAMES, posicion);
            bool espacioDiaMes = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_DIA_MES);
            int valorMinimoConDiaMod = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORDIAMODULO, posicion);
            bool espacioDiaMod = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_DIA_MOD);
            int valorMinimoConMes = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORMES, posicion);
            bool espacioMes = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_MES);
            foreach (var item in dict)
            {
                int valorUltimoRachas = item.Value.DictRachasAgrupadasInt.Where(x => x.Key == item.Value.RachasAcumuladas.Last()).FirstOrDefault().Value;
                int valorContadorGeneral = item.Value.RankContadorGeneral;
                int valorContadorDiaSem = item.Value.RankContadorDiaSemana;
                int valorContadorDiaMes = item.Value.RankContadorDiaMes;
                int valorContadorDiaMod = item.Value.RankContadorDiaModulo;
                int valorContadorMes = item.Value.RankContadorMes;
                if ((espacioUltimoRachasMayor && valorUltimoRachas.Equals(valorMinimoConUltimoRachas)) || valorUltimoRachas.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioGeneral && valorContadorGeneral.Equals(valorMinimoConGeneral)) || valorContadorGeneral.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioDiaSemana && valorContadorDiaSem.Equals(valorMinimoConDiaSemana)) || valorContadorDiaSem.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioDiaMes && valorContadorDiaMes.Equals(valorMinimoConDiaMes)) || valorContadorDiaMes.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioDiaMod && valorContadorDiaMod.Equals(valorMinimoConDiaMod)) || valorContadorDiaMod.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioMes && valorContadorMes.Equals(valorMinimoConMes)) || valorContadorMes.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
            }
            this.EscribirDatosArchivo(temDict2, nombreArchivo);
            //this.ValidarDatosDespActual(dict, nombreArchivo + "lvl2", posicion, nombreTablaDatos);
            //this.ValidarDatosDespActual(temDict2, nombreArchivo + "lvl3", posicion, nombreTablaDatos);
        }

        /// <summary>
        /// Método que realiza la revisión de los valores mínimos y su verificación para limpiarlos del diccionario recibido
        /// </summary>
        /// <param name="dict">diccionario en el que se revisan los valores minimos</param>
        /// <param name="nombreArchivo">nombre del archivo de salida para la escritura de los datos</param>
        /// <param name="nombreTablaAnalisis">Nombre de la tabla donde se realiza el análisis de los datos</param>
        /// <param name="posicion">posición para la carga de datos</param>
        /// <param name="nombreTablaDatos">nombre de la tabla que contiene los datos a validar</param>
        private void RevisarValoresMinimos(Dictionary<int, ObjectInfoDTO> dict, string nombreArchivo, string nombreTablaAnalisis, int posicion, string nombreTablaDatos)
        {
            var temDict2 = dict.ToDictionary(x => x.Key, x => x.Value);
            int valorMinimoConUltimoRachas = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORULTIMOENRACHAS, posicion);
            bool espacioUltimoRachasMayor = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_ULT_RACH);
            int valorMinimoConGeneral = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORGENERAL, posicion);
            bool espacioGeneral = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_GENERAL);
            int valorMinimoConDiaSemana = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORDIASEMANA, posicion);
            bool espacioDiaSemana = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_DIA_SEM);
            int valorMinimoConDiaMes = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORDIAMES, posicion);
            bool espacioDiaMes = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_DIA_MES);
            int valorMinimoConDiaMod = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORDIAMODULO, posicion);
            bool espacioDiaMod = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_DIA_MOD);
            int valorMinimoConMes = this.ObtenerValorMinimo(ConstantesGenerales.DATOS_TEMP, ConstantesGenerales.CONTADORMES, posicion);
            bool espacioMes = this.ValidarEspacioTiempo(15, nombreTablaAnalisis, ConstantesGenerales.INDICA_MIN_CONT_MES);
            foreach (var item in dict)
            {
                int valorUltimoRachas = item.Value.DictRachasAgrupadasInt.Where(x => x.Key == item.Value.RachasAcumuladas.Last()).FirstOrDefault().Value;
                int valorContadorGeneral = item.Value.RankContadorGeneral;
                int valorContadorDiaSem = item.Value.RankContadorDiaSemana;
                int valorContadorDiaMes = item.Value.RankContadorDiaMes;
                int valorContadorDiaMod = item.Value.RankContadorDiaModulo;
                int valorContadorMes = item.Value.RankContadorMes;
                if ((espacioUltimoRachasMayor && valorUltimoRachas.Equals(valorMinimoConUltimoRachas)) || valorUltimoRachas.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioGeneral && valorContadorGeneral.Equals(valorMinimoConGeneral)) || valorContadorGeneral.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioDiaSemana && valorContadorDiaSem.Equals(valorMinimoConDiaSemana)) || valorContadorDiaSem.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioDiaMes && valorContadorDiaMes.Equals(valorMinimoConDiaMes)) || valorContadorDiaMes.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioDiaMod && valorContadorDiaMod.Equals(valorMinimoConDiaMod)) || valorContadorDiaMod.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
                if ((espacioMes && valorContadorMes.Equals(valorMinimoConMes)) || valorContadorMes.Equals(0))
                {
                    temDict2.Remove(item.Key);
                }
            }
            this.EscribirDatosArchivo(temDict2, nombreArchivo);
            //this.ValidarDatosDespActual(dict, nombreArchivo+"lvl2", posicion, nombreTablaDatos);
            //this.ValidarDatosDespActual(temDict2, nombreArchivo + "lvl3", posicion, nombreTablaDatos);
        }

        /// <summary>
        /// Método que realiza la validación de los datos que están despues del actual en el analisis de datos
        /// </summary>
        /// <param name="dict">diccionario del que se validan los datos</param>
        /// <param name="nombreArchivo">nombre del archivo donde se guardarán los datos de salida</param>
        /// <param name="posicion">posición para consultar los datos</param>
        /// <param name="nombreTablaDatos">nombre de la tabla que contiene los datos a consutar</param>
        private void ValidarDatosDespActual(Dictionary<string, ObjectInfoDTO> dict, string nombreArchivo, int posicion, string nombreTablaDatos)
        {
            var temDict2 = dict.ToDictionary(x => x.Key, x => x.Value);
            object objInfo = this.ObtenerUltimoObjetoPosicion(posicion);
            List<QueryInfo> lstContadoresAgrupadosGeneral = new List<QueryInfo>();
            List<QueryInfo> lstContadoresAgrupadosDiaSemana = new List<QueryInfo>();
            List<QueryInfo> lstContadoresAgrupadosDiaMes = new List<QueryInfo>();
            List<QueryInfo> lstContadoresAgrupadosDiaModulo = new List<QueryInfo>();
            List<QueryInfo> lstContadoresAgrupadosMes = new List<QueryInfo>();
            List<int> lstDatosDespActualGeneral = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORGENERAL, nombreTablaDatos);
            List<int> lstDatosDespActualDiaSemana = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORDIASEMANA, nombreTablaDatos);
            List<int> lstDatosDespActualDiaMes = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORDIAMES, nombreTablaDatos);
            List<int> lstDatosDespActualDiaModulo = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORDIAMODULO, nombreTablaDatos);
            List<int> lstDatosDespActualMes = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORMES, nombreTablaDatos);
            SIGN_DATOS p = (SIGN_DATOS)objInfo;
            lstContadoresAgrupadosGeneral = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORGENERAL, nombreTablaDatos, (int)p.CONTADORGENERAL);
            lstContadoresAgrupadosDiaSemana = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIASEMANA, nombreTablaDatos, (int)p.CONTADORDIASEMANA);
            lstContadoresAgrupadosDiaMes = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIAMES, nombreTablaDatos, (int)p.CONTADORDIAMES);
            lstContadoresAgrupadosDiaModulo = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIAMODULO, nombreTablaDatos, (int)p.CONTADORDIAMODULO);
            lstContadoresAgrupadosMes = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORMES, nombreTablaDatos, (int)p.CONTADORMES);
            foreach (var item in dict)
            {
                KeyValuePair<string, ObjectInfoDTO> itemRef = item;
                if (this.ValidarBorradoDato(lstContadoresAgrupadosGeneral, lstContadoresAgrupadosDiaSemana, lstContadoresAgrupadosDiaMes, 
                    lstContadoresAgrupadosDiaModulo, lstContadoresAgrupadosMes, lstDatosDespActualGeneral, 
                    lstDatosDespActualDiaSemana, lstDatosDespActualDiaMes, lstDatosDespActualDiaModulo, lstDatosDespActualMes, itemRef, p))
                {
                    temDict2.Remove(item.Key);
                }
            }

            this.EscribirDatosArchivo(temDict2, nombreArchivo);
        }

        private bool EstaEntreTresMayores(List<QueryInfo> datosValidar, int datoComparar)
        {
            bool esMayores = false;
            List<QueryInfo> datosValidarTemp = datosValidar.GetRange(datosValidar.Count-2, 2);
            for (int i = 0; i < datosValidarTemp.Count; i++)
            {
                if (datosValidarTemp.ElementAt(i).ClaveNum.Equals(datoComparar))
                {
                    esMayores = true;
                    break;
                }
            }
            return esMayores;
        }

        private bool ValidarBorradoDato(List<QueryInfo> lstContadoresAgrupadosGeneral, List<QueryInfo> lstContadoresAgrupadosDiaSemana, 
            List<QueryInfo> lstContadoresAgrupadosDiaMes, List<QueryInfo> lstContadoresAgrupadosDiaModulo, 
            List<QueryInfo> lstContadoresAgrupadosMes, List<int> lstDatosDespActualGeneral, 
            List<int> lstDatosDespActualDiaSemana, List<int> lstDatosDespActualDiaMes, List<int> lstDatosDespActualDiaModulo, 
            List<int> lstDatosDespActualMes, KeyValuePair<string, ObjectInfoDTO> item, object datoComparador)
        {
            bool eliminarDato = false;
            int contadorGeneral;
            int contadorDiaSemana;
            int contadorDiaMes;
            int contadorDiaModulo;
            int contadorMes;
            if (datoComparador is SIGN_DATOS)
            {
                SIGN_DATOS p = (SIGN_DATOS)datoComparador;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;
                eliminarDato = (this.EstaEntreTresMayores(lstContadoresAgrupadosGeneral, item.Value.RankContadorGeneral) && lstDatosDespActualGeneral.Where(x=>x == contadorGeneral).ToList().Count>5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaSemana, item.Value.RankContadorDiaSemana) && lstDatosDespActualDiaSemana.Where(x => x == contadorDiaSemana).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaMes, item.Value.RankContadorDiaMes) && lstDatosDespActualDiaMes.Where(x => x == contadorDiaMes).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaModulo, item.Value.RankContadorDiaModulo) && lstDatosDespActualDiaModulo.Where(x => x == contadorDiaModulo).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosMes, item.Value.RankContadorMes) && lstDatosDespActualMes.Where(x => x == contadorMes).ToList().Count > 5);
            }
            return eliminarDato || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorGeneral, lstDatosDespActualGeneral), item.Value.RankContadorGeneral, lstContadoresAgrupadosGeneral, 4)
                                || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorDiaSemana, lstDatosDespActualDiaSemana), item.Value.RankContadorDiaSemana, lstContadoresAgrupadosDiaSemana, 4)
                                || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorDiaMes, lstDatosDespActualDiaMes), item.Value.RankContadorDiaMes, lstContadoresAgrupadosDiaMes, 4)
                                || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorDiaModulo, lstDatosDespActualDiaModulo), item.Value.RankContadorDiaModulo, lstContadoresAgrupadosDiaModulo, 4)
                                || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorMes, lstDatosDespActualMes), item.Value.RankContadorMes, lstContadoresAgrupadosMes, 4);
        }

        private bool ValidarBorradoDato(List<QueryInfo> lstContadoresAgrupadosGeneral, List<QueryInfo> lstContadoresAgrupadosDiaSemana,
            List<QueryInfo> lstContadoresAgrupadosDiaMes, List<QueryInfo> lstContadoresAgrupadosDiaModulo,
            List<QueryInfo> lstContadoresAgrupadosMes, List<int> lstDatosDespActualGeneral,
            List<int> lstDatosDespActualDiaSemana, List<int> lstDatosDespActualDiaMes, List<int> lstDatosDespActualDiaModulo,
            List<int> lstDatosDespActualMes, KeyValuePair<int, ObjectInfoDTO> item, object datoComparador)
        {
            bool eliminarDato = false;
            int contadorGeneral;
            int contadorDiaSemana;
            int contadorDiaMes;
            int contadorDiaModulo;
            int contadorMes;
            if (datoComparador is POS_UNO_DATOS)
            {
                POS_UNO_DATOS p = (POS_UNO_DATOS)datoComparador;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;
                eliminarDato = (this.EstaEntreTresMayores(lstContadoresAgrupadosGeneral, item.Value.RankContadorGeneral) && lstDatosDespActualGeneral.Where(x => x == contadorGeneral).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaSemana, item.Value.RankContadorDiaSemana) && lstDatosDespActualDiaSemana.Where(x => x == contadorDiaSemana).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaMes, item.Value.RankContadorDiaMes) && lstDatosDespActualDiaMes.Where(x => x == contadorDiaMes).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaModulo, item.Value.RankContadorDiaModulo) && lstDatosDespActualDiaModulo.Where(x => x == contadorDiaModulo).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosMes, item.Value.RankContadorMes) && lstDatosDespActualMes.Where(x => x == contadorMes).ToList().Count > 5);
            }
            else if (datoComparador is POS_DOS_DATOS)
            {
                POS_DOS_DATOS p = (POS_DOS_DATOS)datoComparador;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;
                eliminarDato = (this.EstaEntreTresMayores(lstContadoresAgrupadosGeneral, item.Value.RankContadorGeneral) && lstDatosDespActualGeneral.Where(x => x == contadorGeneral).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaSemana, item.Value.RankContadorDiaSemana) && lstDatosDespActualDiaSemana.Where(x => x == contadorDiaSemana).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaMes, item.Value.RankContadorDiaMes) && lstDatosDespActualDiaMes.Where(x => x == contadorDiaMes).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaModulo, item.Value.RankContadorDiaModulo) && lstDatosDespActualDiaModulo.Where(x => x == contadorDiaModulo).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosMes, item.Value.RankContadorMes) && lstDatosDespActualMes.Where(x => x == contadorMes).ToList().Count > 5);
            }
            else if (datoComparador is POS_TRES_DATOS)
            {
                POS_TRES_DATOS p = (POS_TRES_DATOS)datoComparador;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;
                eliminarDato = (this.EstaEntreTresMayores(lstContadoresAgrupadosGeneral, item.Value.RankContadorGeneral) && lstDatosDespActualGeneral.Where(x => x == contadorGeneral).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaSemana, item.Value.RankContadorDiaSemana) && lstDatosDespActualDiaSemana.Where(x => x == contadorDiaSemana).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaMes, item.Value.RankContadorDiaMes) && lstDatosDespActualDiaMes.Where(x => x == contadorDiaMes).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaModulo, item.Value.RankContadorDiaModulo) && lstDatosDespActualDiaModulo.Where(x => x == contadorDiaModulo).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosMes, item.Value.RankContadorMes) && lstDatosDespActualMes.Where(x => x == contadorMes).ToList().Count > 5);
            }
            else
            {
                POS_CUATRO_DATOS p = (POS_CUATRO_DATOS)datoComparador;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;
                eliminarDato = (this.EstaEntreTresMayores(lstContadoresAgrupadosGeneral, item.Value.RankContadorGeneral) && lstDatosDespActualGeneral.Where(x => x == contadorGeneral).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaSemana, item.Value.RankContadorDiaSemana) && lstDatosDespActualDiaSemana.Where(x => x == contadorDiaSemana).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaMes, item.Value.RankContadorDiaMes) && lstDatosDespActualDiaMes.Where(x => x == contadorDiaMes).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosDiaModulo, item.Value.RankContadorDiaModulo) && lstDatosDespActualDiaModulo.Where(x => x == contadorDiaModulo).ToList().Count > 5)
                    || (this.EstaEntreTresMayores(lstContadoresAgrupadosMes, item.Value.RankContadorMes) && lstDatosDespActualMes.Where(x => x == contadorMes).ToList().Count > 5);
            }
            return eliminarDato || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorGeneral, lstDatosDespActualGeneral), item.Value.RankContadorGeneral, lstContadoresAgrupadosGeneral, 4)
                                || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorDiaSemana, lstDatosDespActualDiaSemana), item.Value.RankContadorDiaSemana, lstContadoresAgrupadosDiaSemana, 4)
                                || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorDiaMes, lstDatosDespActualDiaMes), item.Value.RankContadorDiaMes, lstContadoresAgrupadosDiaMes, 4)
                                || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorDiaModulo, lstDatosDespActualDiaModulo), item.Value.RankContadorDiaModulo, lstContadoresAgrupadosDiaModulo, 4)
                                || this.ValidarExcesoFrecuencia(this.ContadorValoresUltimasApariciones(item.Value.RankContadorMes, lstDatosDespActualMes), item.Value.RankContadorMes, lstContadoresAgrupadosMes, 4);
        }

        /// <summary>
        /// Método que realiza la validación de los datos que están despues del actual en el analisis de datos
        /// </summary>
        /// <param name="dict">diccionario del que se validan los datos</param>
        /// <param name="nombreArchivo">nombre del archivo donde se guardarán los datos de salida</param>
        /// <param name="posicion">posición para consultar los datos</param>
        /// <param name="nombreTablaDatos">nombre de la tabla que contiene los datos a consutar</param>
        private void ValidarDatosDespActual(Dictionary<int, ObjectInfoDTO> dict, string nombreArchivo, int posicion, string nombreTablaDatos)
        {
            var temDict2 = dict.ToDictionary(x => x.Key, x => x.Value);
            object objInfo = this.ObtenerUltimoObjetoPosicion(posicion);
            List<QueryInfo> lstContadoresAgrupadosGeneral = new List<QueryInfo>();
            List<QueryInfo> lstContadoresAgrupadosDiaSemana = new List<QueryInfo>();
            List<QueryInfo> lstContadoresAgrupadosDiaMes = new List<QueryInfo>();
            List<QueryInfo> lstContadoresAgrupadosDiaModulo = new List<QueryInfo>();
            List<QueryInfo> lstContadoresAgrupadosMes = new List<QueryInfo>();
            List<int> lstDatosDespActualGeneral = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORGENERAL, nombreTablaDatos);
            List<int> lstDatosDespActualDiaSemana = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORDIASEMANA, nombreTablaDatos);
            List<int> lstDatosDespActualDiaMes = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORDIAMES, nombreTablaDatos);
            List<int> lstDatosDespActualDiaModulo = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORDIAMODULO, nombreTablaDatos);
            List<int> lstDatosDespActualMes = this.ObtenerValoresActuales(ConstantesGenerales.CONTADORMES, nombreTablaDatos);
            int contadorGeneral = 0;
            int contadorDiaSemana = 0;
            int contadorDiaMes = 0;
            int contadorDiaModulo = 0;
            int contadorMes = 0;
            if (posicion == 1)
            {
                POS_UNO_DATOS p = (POS_UNO_DATOS)objInfo;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;

            }
            else if (posicion == 2)
            {
                POS_DOS_DATOS p = (POS_DOS_DATOS)objInfo;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;

            }
            else if (posicion == 3)
            {
                POS_TRES_DATOS p = (POS_TRES_DATOS)objInfo;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;

            }
            else
            {
                POS_CUATRO_DATOS p = (POS_CUATRO_DATOS)objInfo;
                contadorGeneral = (int)p.CONTADORGENERAL;
                contadorDiaSemana = (int)p.CONTADORDIASEMANA;
                contadorDiaMes = (int)p.CONTADORDIAMES;
                contadorDiaModulo = (int)p.CONTADORDIAMODULO;
                contadorMes = (int)p.CONTADORMES;

            }

            lstContadoresAgrupadosGeneral = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORGENERAL, nombreTablaDatos, contadorGeneral);
            lstContadoresAgrupadosDiaSemana = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIASEMANA, nombreTablaDatos, contadorDiaSemana);
            lstContadoresAgrupadosDiaMes = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIAMES, nombreTablaDatos, contadorDiaMes);
            lstContadoresAgrupadosDiaModulo = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORDIAMODULO, nombreTablaDatos, contadorDiaModulo);
            lstContadoresAgrupadosMes = this.AgruparContadoresDespuesActual(ConstantesGenerales.CONTADORMES, nombreTablaDatos, contadorMes);
            foreach (var item in dict)
            {
                KeyValuePair<int, ObjectInfoDTO> itemRef = item;
                if (this.ValidarBorradoDato(lstContadoresAgrupadosGeneral, lstContadoresAgrupadosDiaSemana, lstContadoresAgrupadosDiaMes,
                    lstContadoresAgrupadosDiaModulo, lstContadoresAgrupadosMes, lstDatosDespActualGeneral,
                    lstDatosDespActualDiaSemana, lstDatosDespActualDiaMes, lstDatosDespActualDiaModulo, lstDatosDespActualMes, itemRef, objInfo))
                {
                    temDict2.Remove(item.Key);
                }
            }

            this.EscribirDatosArchivo(temDict2, nombreArchivo);

        }

        /// <summary>
        /// Método que realiza la validación de datos existentes
        /// </summary>
        /// <param name="listica">lista de datos a validar</param>
        private void ValidarDatosRepetidos(List<ASTR> listica)
        {
            var fecha = ((DateTime)listica.ElementAt(0).FECHA).AddDays(1);
            List<DateTime> fechasNo = new List<DateTime>();
            for (int i = 1; i < listica.Count; i++)
            {
                var fecha2 = ((DateTime)listica.ElementAt(i).FECHA);
                if(fecha != fecha2)
                {
                    fechasNo.Add(fecha);
                }
                fecha = fecha2.AddDays(1);
            }
            //List<decimal> listaIdentificadores = new List<decimal>();
            //DateTime fechaInicial = (DateTime)listica.ElementAt(0).FECHA;
            //for (int i = 1; i < listica.Count; i++)
            //{
            //    ASTR astUno = listica.ElementAt(i);
            //    for (int j = i + 1; j < listica.Count; j++)
            //    {
            //        ASTR astTemp = listica.ElementAt(j);
            //        if (astTemp.POS_UNO == astUno.POS_UNO
            //            && astTemp.POS_DOS == astUno.POS_DOS
            //            && astTemp.POS_TRES == astUno.POS_TRES
            //            && astTemp.POS_CUATRO == astUno.POS_CUATRO
            //            && astTemp.SIGN == astUno.SIGN)
            //        {
            //            if (!listaIdentificadores.Contains(astUno.ID)) listaIdentificadores.Add(astUno.ID);
            //            if (!listaIdentificadores.Contains(astTemp.ID)) listaIdentificadores.Add(astTemp.ID);
            //        }
            //        if (astUno.FECHA == astTemp.FECHA)
            //        {
            //            if (!listaIdentificadores.Contains(astUno.ID)) listaIdentificadores.Add(astUno.ID);
            //            if (!listaIdentificadores.Contains(astTemp.ID)) listaIdentificadores.Add(astTemp.ID);
            //        }
            //    }
            //    DateTime fechaSor = (DateTime)listica.ElementAt(i).FECHA;
            //    if (!fechaSor.AddDays(-1).Equals(fechaInicial))
            //    {
            //        listaIdentificadores.Add(fechaInicial);
            //    }
            //    fechaInicial = fechaSor;
            //}
            //List<ASTR> listaTemp = new List<ASTR>();
            ////Si no se pagina la lista, se obtienen todos los resultados, de lo contrario, se traen los resultados solicitados
            //listaTemp = (from ast in listica
            //             where listaIdentificadores.Contains(ast.ID)
            //             select ast).ToList();
            //string fic = path + "repetidos.txt";
            //StreamWriter sw = new StreamWriter(fic);
            //foreach (var item in listaTemp)
            //{
            //    sw.WriteLine(item.ToString());
            //}
            //sw.Close();
            string fic = path + "repetidos.txt";
            StreamWriter sw = new StreamWriter(fic);
            foreach (var item in fechasNo)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }

        /// <summary>
        /// Método que permite validar si el espacio de tiempo recibido, para una columna a validar en una tabla, es mayor
        /// </summary>
        /// <param name="espacio">valor en cantidad que permite verificar si un valor se encuentra dentro del tiempo</param>
        /// <param name="tablaValidar">Tabla sobre la que se realiza la validación</param>
        /// <param name="columna">columna para la consulta de los datos</param>
        /// <returns></returns>
        private bool ValidarEspacioTiempo(int espacio, string tablaValidar, string columna)
        {
            string consulta = string.Format(ConstantesGenerales.QUERY_MAX_FECHA_CAMPO, tablaValidar, columna);
            DbRawSqlQuery<DateTime> data = _astEntities.Database.SqlQuery<DateTime>(consulta);
            DateTime fechaUltima = data.AsEnumerable().FirstOrDefault();
            TimeSpan ts = this.fecha - fechaUltima;
            return ts.Days < espacio;
        }

        /// <summary>
        /// Método que permite verificar si el valor tiene una frecuencia mayor a la que se admite
        /// </summary>
        /// <param name="contadorApariciones"></param>
        /// <param name="datoValidar"></param>
        /// <param name="datosAgrupados"></param>
        /// <param name="contadorCiclo"></param>
        /// <returns></returns>
        private bool ValidarExcesoFrecuencia(int contadorApariciones, int datoValidar, List<QueryInfo> datosAgrupados, int contadorCiclo)
        {
            for (int i = 0; i < contadorCiclo && i < datosAgrupados.Count; i++)
            {
                if (datoValidar.Equals(datosAgrupados.ElementAt(i).ClaveNum))
                {
                    return (i == 0 && contadorApariciones > 1) || (contadorApariciones > 2);
                }
            }
            return false;
        }

        /// <summary>
        /// Método encargado de validar de acuerdo al caso, el resultado de comparar los dos resultados
        /// </summary>
        /// <param name="caso">caso que indica que se debe validar</param>
        /// <param name="sorValidar">Variable que indica el elemento que se quiere validar</param>
        /// <param name="sorComparador">Objeto que referencia al resultado que sirve como comparador</param>
        /// <returns>bFlag: Bandera con resultado de compararación</returns>
        private bool ValidarMismoDato(int caso, ASTR sorValidar, ASTR sorComparador)
        {
            //Bandera que almacena el valor de la comparación
            bool bFlag = false;
            switch (caso)
            {
                case 1:
                    bFlag = sorValidar.POS_UNO.Equals(sorComparador.POS_UNO);
                    break;
                case 2:
                    bFlag = sorValidar.POS_DOS.Equals(sorComparador.POS_DOS);
                    break;
                case 3:
                    bFlag = sorValidar.POS_TRES.Equals(sorComparador.POS_TRES);
                    break;
                case 4:
                    bFlag = sorValidar.POS_CUATRO.Equals(sorComparador.POS_CUATRO);
                    break;
                case 5:
                    bFlag = sorValidar.SIGN.Equals(sorComparador.SIGN);
                    break;
                default:
                    return false;
            }
            return bFlag;
        }

        /// <summary>
        /// Método que obtiene las listas de las rachas para cada valor
        /// </summary>
        /// <param name="listaValidar">lista con elementos a validar</param>
        /// <param name="dictPosUno">diccionario para validacion de posicion 1</param>
        /// <param name="dictPosDos">diccionario para validacion de posicion 2</param>
        /// <param name="dictPostres">diccionario para validacion de posicion 3</param>
        /// <param name="dictPosCuatro">diccionario para validacion de posicion 4</param>
        /// <param name="dictSign">diccionario para validacion de sign</param>
        private void ValidarRachas(List<ASTR> listaValidar, Dictionary<int, ObjectInfoDTO> dictPosUno,
            Dictionary<int, ObjectInfoDTO> dictPosDos, Dictionary<int, ObjectInfoDTO> dictPostres, Dictionary<int, ObjectInfoDTO> dictPosCuatro,
            Dictionary<string, ObjectInfoDTO> dictSign)
        {
            foreach (var item in listaValidar)
            {
                this.AdicionarElementoDiccionarioRachas(dictPosUno, (int)item.POS_UNO);
                this.AdicionarElementoDiccionarioRachas(dictPosDos, (int)item.POS_DOS);
                this.AdicionarElementoDiccionarioRachas(dictPostres, (int)item.POS_TRES);
                this.AdicionarElementoDiccionarioRachas(dictPosCuatro, (int)item.POS_CUATRO);
                this.AdicionarElementoDiccionarioRachasSign(dictSign, item.SIGN);
            }
            this.ContarRachasPositivasNegativas(dictPosUno);
            this.ContarRachasPositivasNegativas(dictPosDos);
            this.ContarRachasPositivasNegativas(dictPostres);
            this.ContarRachasPositivasNegativas(dictPosCuatro);
            this.ContarRachasPositivasNegativas(dictSign);
            this.AgruparRachas(dictPosUno);
            this.AgruparRachas(dictPosDos);
            this.AgruparRachas(dictPostres);
            this.AgruparRachas(dictPosCuatro);
            this.AgruparRachas(dictSign);
        }

        private void EliminarUltimoRachaRepetido()
        {

        }

        private void ObtenerUltimosRachasPosicion()
        {
            DateTime fecha = this.fecha.AddDays(-1);
            string fechaFormat = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            string consulta = string.Format(ConstantesGenerales.QUERY_VALIDAR_ULTIMOS_RACHAS, fechaFormat);
            DbRawSqlQuery<UltimosRachasDTO> data = _astEntities.Database.SqlQuery<UltimosRachasDTO>(consulta);
            List<UltimosRachasDTO> listDatos = data.AsEnumerable().ToList();
        }
    }
}
