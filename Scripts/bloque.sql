delete from Datos_Temp where fecha not in (select fecha from astr where tipo = 2);

declare
cursor curposuno is SELECT * from pos_uno_datos order by fecha asc;
cursor curposdos is SELECT * from pos_dos_datos order by fecha asc;
cursor curpostres is SELECT * from pos_tres_datos order by fecha asc;
cursor curposcuatro is SELECT * from pos_cuatro_datos order by fecha asc;
cursor curpossign is SELECT * from sign_datos order by fecha asc;
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
begin
	for currec in curposuno
	loop
		SELECT MIN(contadorultimoenrachas), MIN(contadorgeneral), MIN(contadordiasemana), MIN(contadordiames), MIN(contadordiamodulo), MIN(contadormes), MIN(ContadorDespuesActual)
		INTO var_min_dato_cont_ult_rach, var_min_dato_cont_general, var_min_dato_cont_dia_semana, var_min_dato_cont_dia_mes, var_min_dato_cont_dia_modulo, var_min_dato_cont_mes, var_min_dato_cont_desp_actual
		FROM Datos_Temp
		WHERE fecha = currec.fecha
		AND posicion = 1;
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
		INSERT INTO AN_DAT_POS_UNO(ID, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, FECHA)
	 		VALUES (SQ_AN_DAT_POS_UNO.nextval, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_mes := currec.contadormes;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
	end loop;
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
		SELECT MIN(contadorultimoenrachas), MIN(contadorgeneral), MIN(contadordiasemana), MIN(contadordiames), MIN(contadordiamodulo), MIN(contadormes), MIN(ContadorDespuesActual)
		INTO var_min_dato_cont_ult_rach, var_min_dato_cont_general, var_min_dato_cont_dia_semana, var_min_dato_cont_dia_mes, var_min_dato_cont_dia_modulo, var_min_dato_cont_mes, var_min_dato_cont_desp_actual
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
		INSERT INTO AN_DAT_POS_DOS(ID, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, FECHA)
			VALUES (SQ_AN_DAT_POS_DOS.nextval, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_mes := currec.contadormes;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
	end loop;
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
		SELECT MIN(contadorultimoenrachas), MIN(contadorgeneral), MIN(contadordiasemana), MIN(contadordiames), MIN(contadordiamodulo), MIN(contadormes), MIN(ContadorDespuesActual)
		INTO var_min_dato_cont_ult_rach, var_min_dato_cont_general, var_min_dato_cont_dia_semana, var_min_dato_cont_dia_mes, var_min_dato_cont_dia_modulo, var_min_dato_cont_mes, var_min_dato_cont_desp_actual
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
		INSERT INTO AN_DAT_POS_TRES(ID, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, FECHA)
			VALUES (SQ_AN_DAT_POS_TRES.nextval, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
		var_anterior_cont_mes := currec.contadormes;
	end loop;
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
		SELECT MIN(contadorultimoenrachas), MIN(contadorgeneral), MIN(contadordiasemana), MIN(contadordiames), MIN(contadordiamodulo), MIN(contadormes), MIN(ContadorDespuesActual)
		INTO var_min_dato_cont_ult_rach, var_min_dato_cont_general, var_min_dato_cont_dia_semana, var_min_dato_cont_dia_mes, var_min_dato_cont_dia_modulo, var_min_dato_cont_mes, var_min_dato_cont_desp_actual
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
		INSERT INTO AN_DAT_POS_CUATRO(ID, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, FECHA)
			VALUES (SQ_AN_DAT_POS_CUATRO.nextval, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_mes := currec.contadormes;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
	end loop;
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
		SELECT MIN(contadorultimoenrachas), MIN(contadorgeneral), MIN(contadordiasemana), MIN(contadordiames), MIN(contadordiamodulo), MIN(contadormes), MIN(ContadorDespuesActual)
		INTO var_min_dato_cont_ult_rach, var_min_dato_cont_general, var_min_dato_cont_dia_semana, var_min_dato_cont_dia_mes, var_min_dato_cont_dia_modulo, var_min_dato_cont_mes, var_min_dato_cont_desp_actual
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
		INSERT INTO AN_DAT_SIGN(ID, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, FECHA)
			VALUES (SQ_AN_DAT_SIGN.nextval, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, currec.FECHA);
		var_anterior_cont_ult_rach := currec.contadorultimoenrachas;
		var_anterior_cont_general := currec.contadorgeneral;
		var_anterior_cont_dia_semana := currec.contadordiasemana;
		var_anterior_cont_dia_mes := currec.contadordiames;
		var_anterior_cont_dia_modulo := currec.contadordiamodulo;
		var_anterior_cont_mes := currec.contadormes;
		var_anterior_cont_desp_actual := currec.ContadorDespuesActual;
	end loop;
end;

select * from AN_DAT_POS_UNO order by fecha desc;
select * from AN_DAT_POS_DOS order by fecha desc;
select * from AN_DAT_POS_TRES order by fecha desc;
select * from AN_DAT_POS_CUATRO order by fecha desc;
select * from AN_DAT_SIGN order by fecha desc;