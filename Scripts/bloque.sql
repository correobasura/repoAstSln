delete from datos_temp where fecha not in(select fecha from astr);commit;
declare
cursor curposuno is SELECT * from pos_uno_datos order by fecha asc;
cursor curposdos is SELECT * from pos_dos_datos order by fecha asc;
cursor curpostres is SELECT * from pos_tres_datos order by fecha asc;
cursor curposcuatro is SELECT * from pos_cuatro_datos order by fecha asc;
cursor curpossign is SELECT * from sign_datos order by fecha asc;
var_min_dato_sin_aparecer NUMBER := 0;
var_ind_min_sin_aparecer NUMBER := 0;
var_min_dato_cont_ult_rach NUMBER := 0;
var_ind_min_cont_ult_rach NUMBER := 0;
var_anterior_cont_ult_rach NUMBER := 0;
var_comparador_ult_rach NUMBER := 0;
var_min_dato_cont_general NUMBER := 0;
var_ind_min_cont_general NUMBER := 0;
var_anterior_cont_general NUMBER := 0;
var_comparador_general NUMBER := 0;
var_min_dato_cont_dia_semana NUMBER := 0;
var_ind_min_cont_dia_semana NUMBER := 0;
var_anterior_cont_dia_semana NUMBER := 0;
var_comparador_dia_semana NUMBER := 0;
var_min_dato_cont_dia_mes NUMBER := 0;
var_ind_min_cont_dia_mes NUMBER := 0;
var_anterior_cont_dia_mes NUMBER := 0;
var_comparador_dia_mes NUMBER := 0;
var_min_dato_cont_dia_modulo NUMBER := 0;
var_ind_min_cont_dia_modulo NUMBER := 0;
var_anterior_cont_dia_modulo NUMBER := 0;
var_comparador_dia_modulo NUMBER := 0;
var_min_dato_cont_mes NUMBER := 0;
var_ind_min_cont_mes NUMBER := 0;
var_anterior_cont_mes NUMBER := 0;
var_comparador_mes NUMBER := 0;
var_min_dato_cont_desp_actual NUMBER := 0;
var_ind_min_cont_desp_actual NUMBER := 0;
var_anterior_cont_desp_actual NUMBER := 0;
var_comparador_desp_actual NUMBER := 0;
var_min_dato_puntua_total NUMBER := 0;
var_indica_min_puntua_total NUMBER := 0;
var_max_dato_puntua_total NUMBER := 0;
var_indica_max_puntua_total NUMBER := 0;
begin
	for currec in curposuno
	loop
		SELECT MIN(sinaparecer),
			MIN(contadorultimoenrachas), 
			MIN(contadorgeneral), 
			MIN(contadordiasemana), 
			MIN(contadordiames), 
			MIN(contadordiamodulo), 
			MIN(contadormes), 
			MIN(ContadorDespuesActual), 
			MIN(PuntuacionTotal), 
			MAX(PuntuacionTotal)
		INTO var_min_dato_sin_aparecer,
			var_min_dato_cont_ult_rach, 
			var_min_dato_cont_general, 
			var_min_dato_cont_dia_semana, 
			var_min_dato_cont_dia_mes, 
			var_min_dato_cont_dia_modulo, 
			var_min_dato_cont_mes, 
			var_min_dato_cont_desp_actual, 
			var_min_dato_puntua_total, 
			var_max_dato_puntua_total
		FROM Datos_Temp
		WHERE fecha = currec.fecha
		AND posicion = 1;
		IF currec.sinaparecer = var_min_dato_sin_aparecer
			THEN var_ind_min_sin_aparecer := 1;
		ELSE var_ind_min_sin_aparecer :=0;
		END IF;
		/*Si el dato es igual al mínimo entonces el indicador es 1, sino es cero*/
	    IF currec.contadorultimoenrachas = var_min_dato_cont_ult_rach 
	    	THEN var_ind_min_cont_ult_rach := 1;
	    ELSE var_ind_min_cont_ult_rach := 0;
	    END IF;
	    /*Si el dato anterior es =, el comparador es cero, si es mayor al anterior, cerá 1, de lo contrario será -1*/
	    IF currec.contadorultimoenrachas = var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := 0;
	    ELSIF currec.contadorultimoenrachas < var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := -1;
	    ELSE var_comparador_ult_rach := 1;
	    END IF;
	    /*Si el dato es igual al mínimo entonces el indicador es 1, sino es cero*/
	    IF currec.contadorgeneral = var_min_dato_cont_general 
			THEN var_ind_min_cont_general := 1;
		ELSE var_ind_min_cont_general := 0;
		END IF;
		/*Si el dato anterior es =, el comparador es cero, si es mayor al anterior, cerá 1, de lo contrario será -1*/
		IF currec.contadorgeneral = var_anterior_cont_general 
			THEN var_comparador_general := 0;
		ELSIF currec.contadorgeneral < var_anterior_cont_general 
			THEN var_comparador_general := -1;
		ELSE var_comparador_general := 1;
		END IF;
		/*Si el dato es igual al mínimo entonces el indicador es 1, sino es cero*/
		IF currec.contadordiasemana = var_min_dato_cont_dia_semana 
			THEN var_ind_min_cont_dia_semana := 1;
		ELSE var_ind_min_cont_dia_semana := 0;
		END IF;
		/*Si el dato anterior es =, el comparador es cero, si es mayor al anterior, cerá 1, de lo contrario será -1*/
		IF currec.contadordiasemana = var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := 0;
		ELSIF currec.contadordiasemana < var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := -1;
		ELSE var_comparador_dia_semana := 1;
		END IF;
		/*Si el dato es igual al mínimo entonces el indicador es 1, sino es cero*/
		IF currec.contadordiames = var_min_dato_cont_dia_mes 
			THEN var_ind_min_cont_dia_mes := 1;
		ELSE var_ind_min_cont_dia_mes := 0;
		END IF;
		/*Si el dato anterior es =, el comparador es cero, si es mayor al anterior, cerá 1, de lo contrario será -1*/
		IF currec.contadordiames = var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := 0;
		ELSIF currec.contadordiames < var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := -1;
		ELSE var_comparador_dia_mes := 1;
		END IF;
		/*Si el dato es igual al mínimo entonces el indicador es 1, sino es cero*/
		IF currec.contadordiamodulo = var_min_dato_cont_dia_modulo 
			THEN var_ind_min_cont_dia_modulo := 1;
		ELSE var_ind_min_cont_dia_modulo := 0;
		END IF;
		/*Si el dato anterior es =, el comparador es cero, si es mayor al anterior, cerá 1, de lo contrario será -1*/
		IF currec.contadordiamodulo = var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := 0;
		ELSIF currec.contadordiamodulo < var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := -1;
		ELSE var_comparador_dia_modulo := 1;
		END IF;
		/*Si el dato es igual al mínimo entonces el indicador es 1, sino es cero*/
		IF currec.contadormes = var_min_dato_cont_mes 
			THEN var_ind_min_cont_mes := 1;
		ELSE var_ind_min_cont_mes := 0;
		END IF;
		/*Si el dato anterior es =, el comparador es cero, si es mayor al anterior, cerá 1, de lo contrario será -1*/
		IF currec.contadormes = var_anterior_cont_mes 
			THEN var_comparador_mes := 0;
		ELSIF currec.contadormes < var_anterior_cont_mes 
			THEN var_comparador_mes := -1;
		ELSE var_comparador_mes := 1;
		END IF;
		IF currec.ContadorDespuesActual = var_min_dato_cont_desp_actual 
			THEN var_ind_min_cont_desp_actual := 1;
		ELSE var_ind_min_cont_desp_actual := 0;
		END IF;
		IF currec.ContadorDespuesActual = var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := 0;
		ELSIF currec.ContadorDespuesActual < var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := -1;
		ELSE var_comparador_desp_actual := 1;
		END IF;
		IF currec.PuntuacionTotal = var_min_dato_puntua_total 
			THEN var_indica_min_puntua_total := 1;
		ELSE var_indica_min_puntua_total := 0;
		END IF;
		IF currec.PuntuacionTotal = var_max_dato_puntua_total 
			THEN var_indica_max_puntua_total := 1;
		ELSE var_indica_max_puntua_total := 0;
		END IF;
		INSERT INTO AN_DAT_POS_UNO(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
								INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, 
								INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, 
								INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, INDICA_MIN_PUNTUA_TOTAL, 
								INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
	 		VALUES (SQ_AN_DAT_POS_UNO.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, 
	 				var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, 
	 				var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, 
	 				var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
	 				var_indica_max_puntua_total, EXTRACT(DAY FROM currec.fecha), currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_mes := currec.contadormes;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
	end loop;
	var_ind_min_sin_aparecer := 0;
	var_ind_min_cont_ult_rach := 0; 
	var_comparador_ult_rach := 0; 
	var_ind_min_cont_general := 0; 
	var_comparador_general := 0; 
	var_ind_min_cont_dia_semana := 0; 
	var_comparador_dia_semana := 0; 
	var_ind_min_cont_dia_mes := 0; 
	var_comparador_dia_mes := 0; 
	var_ind_min_cont_dia_modulo := 0; 
	var_comparador_dia_modulo := 0; 
	var_ind_min_cont_mes := 0; 
	var_ind_min_cont_desp_actual := 0;
	var_comparador_desp_actual :=0;
	var_comparador_mes := 0;
	for currec in curposdos
	loop
		SELECT MIN(sinaparecer),
			MIN(contadorultimoenrachas), 
			MIN(contadorgeneral), 
			MIN(contadordiasemana), 
			MIN(contadordiames), 
			MIN(contadordiamodulo), 
			MIN(contadormes), 
			MIN(ContadorDespuesActual), 
			MIN(PuntuacionTotal), 
			MAX(PuntuacionTotal)
		INTO var_min_dato_sin_aparecer,
			var_min_dato_cont_ult_rach, 
			var_min_dato_cont_general, 
			var_min_dato_cont_dia_semana, 
			var_min_dato_cont_dia_mes, 
			var_min_dato_cont_dia_modulo, 
			var_min_dato_cont_mes, 
			var_min_dato_cont_desp_actual, 
			var_min_dato_puntua_total, 
			var_max_dato_puntua_total
		FROM Datos_Temp
		WHERE fecha = currec.fecha
		AND posicion = 2;
	    IF currec.contadorultimoenrachas = var_min_dato_cont_ult_rach 
	    	THEN var_ind_min_cont_ult_rach := 1;
	    ELSE var_ind_min_cont_ult_rach := 0;
	    END IF;
	    IF currec.contadorultimoenrachas = var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := 0;
	    ELSIF currec.contadorultimoenrachas < var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := -1;
	    ELSE var_comparador_ult_rach := 1;
	    END IF;
	    IF currec.contadorgeneral = var_min_dato_cont_general 
			THEN var_ind_min_cont_general := 1;
		ELSE var_ind_min_cont_general := 0;
		END IF;
		IF currec.contadorgeneral = var_anterior_cont_general 
			THEN var_comparador_general := 0;
		ELSIF currec.contadorgeneral < var_anterior_cont_general 
			THEN var_comparador_general := -1;
		ELSE var_comparador_general := 1;
		END IF;
		IF currec.contadordiasemana = var_min_dato_cont_dia_semana 
			THEN var_ind_min_cont_dia_semana := 1;
		ELSE var_ind_min_cont_dia_semana := 0;
		END IF;
		IF currec.contadordiasemana = var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := 0;
		ELSIF currec.contadordiasemana < var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := -1;
		ELSE var_comparador_dia_semana := 1;
		END IF;
		IF currec.contadordiames = var_min_dato_cont_dia_mes 
			THEN var_ind_min_cont_dia_mes := 1;
		ELSE var_ind_min_cont_dia_mes := 0;
		END IF;
		IF currec.contadordiames = var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := 0;
		ELSIF currec.contadordiames < var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := -1;
		ELSE var_comparador_dia_mes := 1;
		END IF;
		IF currec.contadordiamodulo = var_min_dato_cont_dia_modulo 
			THEN var_ind_min_cont_dia_modulo := 1;
		ELSE var_ind_min_cont_dia_modulo := 0;
		END IF;
		IF currec.contadordiamodulo = var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := 0;
		ELSIF currec.contadordiamodulo < var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := -1;
		ELSE var_comparador_dia_modulo := 1;
		END IF;
		IF currec.contadormes = var_min_dato_cont_mes 
			THEN var_ind_min_cont_mes := 1;
		ELSE var_ind_min_cont_mes := 0;
		END IF;
		IF currec.contadormes = var_anterior_cont_mes 
			THEN var_comparador_mes := 0;
		ELSIF currec.contadormes < var_anterior_cont_mes 
			THEN var_comparador_mes := -1;
		ELSE var_comparador_mes := 1;
		END IF;
		IF currec.ContadorDespuesActual = var_min_dato_cont_desp_actual 
			THEN var_ind_min_cont_desp_actual := 1;
		ELSE var_ind_min_cont_desp_actual := 0;
		END IF;
		IF currec.ContadorDespuesActual = var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := 0;
		ELSIF currec.ContadorDespuesActual < var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := -1;
		ELSE var_comparador_desp_actual := 1;
		END IF;
		IF currec.PuntuacionTotal = var_min_dato_puntua_total 
			THEN var_indica_min_puntua_total := 1;
		ELSE var_indica_min_puntua_total := 0;
		END IF;
		IF currec.PuntuacionTotal = var_max_dato_puntua_total 
			THEN var_indica_max_puntua_total := 1;
		ELSE var_indica_max_puntua_total := 0;
		END IF;
		INSERT INTO AN_DAT_POS_DOS(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
								INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, 
								INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, 
								INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, INDICA_MIN_PUNTUA_TOTAL, 
								INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
			VALUES (SQ_AN_DAT_POS_DOS.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, 
					var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, 
					var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, 
					var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
					var_indica_max_puntua_total, EXTRACT(DAY FROM currec.fecha), currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_mes := currec.contadormes;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
	end loop;
	var_ind_min_sin_aparecer := 0;
	var_ind_min_cont_ult_rach := 0; 
	var_comparador_ult_rach := 0; 
	var_ind_min_cont_general := 0; 
	var_comparador_general := 0; 
	var_ind_min_cont_dia_semana := 0; 
	var_comparador_dia_semana := 0; 
	var_ind_min_cont_dia_mes := 0; 
	var_comparador_dia_mes := 0; 
	var_ind_min_cont_dia_modulo := 0; 
	var_comparador_dia_modulo := 0; 
	var_ind_min_cont_mes := 0; 
	var_comparador_mes := 0;
	var_ind_min_cont_desp_actual := 0;
	var_comparador_desp_actual :=0;
	for currec in curpostres
	loop
		SELECT MIN(sinaparecer),
			MIN(contadorultimoenrachas), 
			MIN(contadorgeneral), 
			MIN(contadordiasemana), 
			MIN(contadordiames), 
			MIN(contadordiamodulo), 
			MIN(contadormes), 
			MIN(ContadorDespuesActual), 
			MIN(PuntuacionTotal), 
			MAX(PuntuacionTotal)
		INTO var_min_dato_sin_aparecer,
			var_min_dato_cont_ult_rach, 
			var_min_dato_cont_general, 
			var_min_dato_cont_dia_semana, 
			var_min_dato_cont_dia_mes, 
			var_min_dato_cont_dia_modulo, 
			var_min_dato_cont_mes, 
			var_min_dato_cont_desp_actual, 
			var_min_dato_puntua_total, 
			var_max_dato_puntua_total
		FROM Datos_Temp
		WHERE fecha = currec.fecha
		AND posicion = 3;
	    IF currec.contadorultimoenrachas = var_min_dato_cont_ult_rach 
	    	THEN var_ind_min_cont_ult_rach := 1;
	    ELSE var_ind_min_cont_ult_rach := 0;
	    END IF;
	    IF currec.contadorultimoenrachas = var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := 0;
	    ELSIF currec.contadorultimoenrachas < var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := -1;
	    ELSE var_comparador_ult_rach := 1;
	    END IF;
	    IF currec.contadorgeneral = var_min_dato_cont_general 
			THEN var_ind_min_cont_general := 1;
		ELSE var_ind_min_cont_general := 0;
		END IF;
		IF currec.contadorgeneral = var_anterior_cont_general 
			THEN var_comparador_general := 0;
		ELSIF currec.contadorgeneral < var_anterior_cont_general 
			THEN var_comparador_general := -1;
		ELSE var_comparador_general := 1;
		END IF;
		IF currec.contadordiasemana = var_min_dato_cont_dia_semana 
			THEN var_ind_min_cont_dia_semana := 1;
		ELSE var_ind_min_cont_dia_semana := 0;
		END IF;
		IF currec.contadordiasemana = var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := 0;
		ELSIF currec.contadordiasemana < var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := -1;
		ELSE var_comparador_dia_semana := 1;
		END IF;
		IF currec.contadordiames = var_min_dato_cont_dia_mes 
			THEN var_ind_min_cont_dia_mes := 1;
		ELSE var_ind_min_cont_dia_mes := 0;
		END IF;
		IF currec.contadordiames = var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := 0;
		ELSIF currec.contadordiames < var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := -1;
		ELSE var_comparador_dia_mes := 1;
		END IF;
		IF currec.contadordiamodulo = var_min_dato_cont_dia_modulo 
			THEN var_ind_min_cont_dia_modulo := 1;
		ELSE var_ind_min_cont_dia_modulo := 0;
		END IF;
		IF currec.contadordiamodulo = var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := 0;
		ELSIF currec.contadordiamodulo < var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := -1;
		ELSE var_comparador_dia_modulo := 1;
		END IF;
		IF currec.contadormes = var_min_dato_cont_mes 
			THEN var_ind_min_cont_mes := 1;
		ELSE var_ind_min_cont_mes := 0;
		END IF;
		IF currec.contadormes = var_anterior_cont_mes 
			THEN var_comparador_mes := 0;
		ELSIF currec.contadormes < var_anterior_cont_mes 
			THEN var_comparador_mes := -1;
		ELSE var_comparador_mes := 1;
		END IF;
		IF currec.ContadorDespuesActual = var_min_dato_cont_desp_actual 
			THEN var_ind_min_cont_desp_actual := 1;
		ELSE var_ind_min_cont_desp_actual := 0;
		END IF;
		IF currec.ContadorDespuesActual = var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := 0;
		ELSIF currec.ContadorDespuesActual < var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := -1;
		ELSE var_comparador_desp_actual := 1;
		END IF;
		IF currec.PuntuacionTotal = var_min_dato_puntua_total 
			THEN var_indica_min_puntua_total := 1;
		ELSE var_indica_min_puntua_total := 0;
		END IF;
		IF currec.PuntuacionTotal = var_max_dato_puntua_total 
			THEN var_indica_max_puntua_total := 1;
		ELSE var_indica_max_puntua_total := 0;
		END IF;
		INSERT INTO AN_DAT_POS_TRES(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
								INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, 
								INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, 
								INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, INDICA_MIN_PUNTUA_TOTAL, 
								INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
			VALUES (SQ_AN_DAT_POS_TRES.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, 
					var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, 
					var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, 
					var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
					var_indica_max_puntua_total, EXTRACT(DAY FROM currec.fecha), currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
		var_anterior_cont_mes := currec.contadormes;
	end loop;
	var_ind_min_sin_aparecer := 0;
	var_ind_min_cont_ult_rach := 0; 
	var_comparador_ult_rach := 0; 
	var_ind_min_cont_general := 0; 
	var_comparador_general := 0; 
	var_ind_min_cont_dia_semana := 0; 
	var_comparador_dia_semana := 0; 
	var_ind_min_cont_dia_mes := 0; 
	var_comparador_dia_mes := 0; 
	var_ind_min_cont_dia_modulo := 0; 
	var_comparador_dia_modulo := 0; 
	var_ind_min_cont_mes := 0; 
	var_comparador_mes := 0;
	var_ind_min_cont_desp_actual := 0;
	var_comparador_desp_actual :=0;
	for currec in curposcuatro
	loop
		SELECT MIN(sinaparecer),
			MIN(contadorultimoenrachas), 
			MIN(contadorgeneral), 
			MIN(contadordiasemana), 
			MIN(contadordiames), 
			MIN(contadordiamodulo), 
			MIN(contadormes), 
			MIN(ContadorDespuesActual), 
			MIN(PuntuacionTotal), 
			MAX(PuntuacionTotal)
		INTO var_min_dato_sin_aparecer,
			var_min_dato_cont_ult_rach, 
			var_min_dato_cont_general, 
			var_min_dato_cont_dia_semana, 
			var_min_dato_cont_dia_mes, 
			var_min_dato_cont_dia_modulo, 
			var_min_dato_cont_mes, 
			var_min_dato_cont_desp_actual, 
			var_min_dato_puntua_total, 
			var_max_dato_puntua_total
		FROM Datos_Temp
		WHERE fecha = currec.fecha
		AND posicion = 4;
	    IF currec.contadorultimoenrachas = var_min_dato_cont_ult_rach 
	    	THEN var_ind_min_cont_ult_rach := 1;
	    ELSE var_ind_min_cont_ult_rach := 0;
	    END IF;
	    IF currec.contadorultimoenrachas = var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := 0;
	    ELSIF currec.contadorultimoenrachas < var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := -1;
	    ELSE var_comparador_ult_rach := 1;
	    END IF;
	    IF currec.contadorgeneral = var_min_dato_cont_general 
			THEN var_ind_min_cont_general := 1;
		ELSE var_ind_min_cont_general := 0;
		END IF;
		IF currec.contadorgeneral = var_anterior_cont_general 
			THEN var_comparador_general := 0;
		ELSIF currec.contadorgeneral < var_anterior_cont_general 
			THEN var_comparador_general := -1;
		ELSE var_comparador_general := 1;
		END IF;
		IF currec.contadordiasemana = var_min_dato_cont_dia_semana 
			THEN var_ind_min_cont_dia_semana := 1;
		ELSE var_ind_min_cont_dia_semana := 0;
		END IF;
		IF currec.contadordiasemana = var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := 0;
		ELSIF currec.contadordiasemana < var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := -1;
		ELSE var_comparador_dia_semana := 1;
		END IF;
		IF currec.contadordiames = var_min_dato_cont_dia_mes 
			THEN var_ind_min_cont_dia_mes := 1;
		ELSE var_ind_min_cont_dia_mes := 0;
		END IF;
		IF currec.contadordiames = var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := 0;
		ELSIF currec.contadordiames < var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := -1;
		ELSE var_comparador_dia_mes := 1;
		END IF;
		IF currec.contadordiamodulo = var_min_dato_cont_dia_modulo 
			THEN var_ind_min_cont_dia_modulo := 1;
		ELSE var_ind_min_cont_dia_modulo := 0;
		END IF;
		IF currec.contadordiamodulo = var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := 0;
		ELSIF currec.contadordiamodulo < var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := -1;
		ELSE var_comparador_dia_modulo := 1;
		END IF;
		IF currec.contadormes = var_min_dato_cont_mes 
			THEN var_ind_min_cont_mes := 1;
		ELSE var_ind_min_cont_mes := 0;
		END IF;
		IF currec.contadormes = var_anterior_cont_mes 
			THEN var_comparador_mes := 0;
		ELSIF currec.contadormes < var_anterior_cont_mes 
			THEN var_comparador_mes := -1;
		ELSE var_comparador_mes := 1;
		END IF;
		IF currec.ContadorDespuesActual = var_min_dato_cont_desp_actual 
			THEN var_ind_min_cont_desp_actual := 1;
		ELSE var_ind_min_cont_desp_actual := 0;
		END IF;
		IF currec.ContadorDespuesActual = var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := 0;
		ELSIF currec.ContadorDespuesActual < var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := -1;
		ELSE var_comparador_desp_actual := 1;
		END IF;
		IF currec.PuntuacionTotal = var_min_dato_puntua_total 
			THEN var_indica_min_puntua_total := 1;
		ELSE var_indica_min_puntua_total := 0;
		END IF;
		IF currec.PuntuacionTotal = var_max_dato_puntua_total 
			THEN var_indica_max_puntua_total := 1;
		ELSE var_indica_max_puntua_total := 0;
		END IF;
		INSERT INTO AN_DAT_POS_CUATRO(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
								INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, 
								INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, 
								INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, INDICA_MIN_PUNTUA_TOTAL, 
								INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
			VALUES (SQ_AN_DAT_POS_CUATRO.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, 
					var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, 
					var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, 
					var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
					var_indica_max_puntua_total, EXTRACT(DAY FROM currec.fecha), currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_mes := currec.contadormes;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
	end loop;
	var_ind_min_sin_aparecer := 0;
	var_ind_min_cont_ult_rach := 0; 
	var_comparador_ult_rach := 0; 
	var_ind_min_cont_general := 0; 
	var_comparador_general := 0; 
	var_ind_min_cont_dia_semana := 0; 
	var_comparador_dia_semana := 0; 
	var_ind_min_cont_dia_mes := 0; 
	var_comparador_dia_mes := 0; 
	var_ind_min_cont_dia_modulo := 0; 
	var_comparador_dia_modulo := 0; 
	var_ind_min_cont_mes := 0; 
	var_comparador_mes := 0;
	var_ind_min_cont_desp_actual := 0;
	var_comparador_desp_actual :=0;
	for currec in curpossign
	loop
		SELECT MIN(sinaparecer),
			MIN(contadorultimoenrachas), 
			MIN(contadorgeneral), 
			MIN(contadordiasemana), 
			MIN(contadordiames), 
			MIN(contadordiamodulo), 
			MIN(contadormes), 
			MIN(ContadorDespuesActual), 
			MIN(PuntuacionTotal), 
			MAX(PuntuacionTotal)
		INTO var_min_dato_sin_aparecer,
			var_min_dato_cont_ult_rach, 
			var_min_dato_cont_general, 
			var_min_dato_cont_dia_semana, 
			var_min_dato_cont_dia_mes, 
			var_min_dato_cont_dia_modulo, 
			var_min_dato_cont_mes, 
			var_min_dato_cont_desp_actual, 
			var_min_dato_puntua_total, 
			var_max_dato_puntua_total
		FROM Datos_Temp
		WHERE fecha = currec.fecha
		AND posicion = 5;
	    IF currec.contadorultimoenrachas = var_min_dato_cont_ult_rach 
	    	THEN var_ind_min_cont_ult_rach := 1;
	    ELSE var_ind_min_cont_ult_rach := 0;
	    END IF;
	    IF currec.contadorultimoenrachas = var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := 0;
	    ELSIF currec.contadorultimoenrachas < var_anterior_cont_ult_rach 
	    	THEN var_comparador_ult_rach := -1;
	    ELSE var_comparador_ult_rach := 1;
	    END IF;
	    IF currec.contadorgeneral = var_min_dato_cont_general 
			THEN var_ind_min_cont_general := 1;
		ELSE var_ind_min_cont_general := 0;
		END IF;
		IF currec.contadorgeneral = var_anterior_cont_general 
			THEN var_comparador_general := 0;
		ELSIF currec.contadorgeneral < var_anterior_cont_general 
			THEN var_comparador_general := -1;
		ELSE var_comparador_general := 1;
		END IF;
		IF currec.contadordiasemana = var_min_dato_cont_dia_semana 
			THEN var_ind_min_cont_dia_semana := 1;
		ELSE var_ind_min_cont_dia_semana := 0;
		END IF;
		IF currec.contadordiasemana = var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := 0;
		ELSIF currec.contadordiasemana < var_anterior_cont_dia_semana 
			THEN var_comparador_dia_semana := -1;
		ELSE var_comparador_dia_semana := 1;
		END IF;
		IF currec.contadordiames = var_min_dato_cont_dia_mes 
			THEN var_ind_min_cont_dia_mes := 1;
		ELSE var_ind_min_cont_dia_mes := 0;
		END IF;
		IF currec.contadordiames = var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := 0;
		ELSIF currec.contadordiames < var_anterior_cont_dia_mes 
			THEN var_comparador_dia_mes := -1;
		ELSE var_comparador_dia_mes := 1;
		END IF;
		IF currec.contadordiamodulo = var_min_dato_cont_dia_modulo 
			THEN var_ind_min_cont_dia_modulo := 1;
		ELSE var_ind_min_cont_dia_modulo := 0;
		END IF;
		IF currec.contadordiamodulo = var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := 0;
		ELSIF currec.contadordiamodulo < var_anterior_cont_dia_modulo 
			THEN var_comparador_dia_modulo := -1;
		ELSE var_comparador_dia_modulo := 1;
		END IF;
		IF currec.contadormes = var_min_dato_cont_mes 
			THEN var_ind_min_cont_mes := 1;
		ELSE var_ind_min_cont_mes := 0;
		END IF;
		IF currec.contadormes = var_anterior_cont_mes 
			THEN var_comparador_mes := 0;
		ELSIF currec.contadormes < var_anterior_cont_mes 
			THEN var_comparador_mes := -1;
		ELSE var_comparador_mes := 1;
		END IF;
		IF currec.ContadorDespuesActual = var_min_dato_cont_desp_actual 
			THEN var_ind_min_cont_desp_actual := 1;
		ELSE var_ind_min_cont_desp_actual := 0;
		END IF;
		IF currec.ContadorDespuesActual = var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := 0;
		ELSIF currec.ContadorDespuesActual < var_anterior_cont_desp_actual 
			THEN var_comparador_desp_actual := -1;
		ELSE var_comparador_desp_actual := 1;
		END IF;
		IF currec.PuntuacionTotal = var_min_dato_puntua_total 
			THEN var_indica_min_puntua_total := 1;
		ELSE var_indica_min_puntua_total := 0;
		END IF;
		IF currec.PuntuacionTotal = var_max_dato_puntua_total 
			THEN var_indica_max_puntua_total := 1;
		ELSE var_indica_max_puntua_total := 0;
		END IF;
		INSERT INTO AN_DAT_SIGN(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
								INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, 
								INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, 
								INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, INDICA_MIN_PUNTUA_TOTAL, 
								INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
			VALUES (SQ_AN_DAT_SIGN.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, 
					var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, 
					var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, 
					var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, 
					var_indica_min_puntua_total, var_indica_max_puntua_total, EXTRACT(DAY FROM currec.fecha), currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_mes := currec.contadormes;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
	end loop;
end;