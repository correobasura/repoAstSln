CREATE USER userSort IDENTIFIED BY userSort;
GRANT RESOURCE TO userSort;
GRANT CREATE SESSION TO userSort;
PURGE RECYCLEBIN;

drop TABLE ASTR;
drop SEQUENCE SQ_ASTR;
CREATE SEQUENCE SQ_ASTR  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOCYCLE;

CREATE TABLE ASTR(
	ID NUMBER primary key,
	TIPO NUMBER,
	POS_UNO NUMBER,
	POS_DOS NUMBER,
	POS_TRES NUMBER,
	POS_CUATRO NUMBER, 
	SIGN VARCHAR2(15), 
	NUM_SOR NUMBER,
	FECHA DATE
	);

create or replace trigger TR_INSERT_ASTR
before insert on ASTR
for each row
begin
  select SQ_ASTR.nextval into :new.Id from dual;
end;

drop TABLE Datos_Temp;
drop TABLE POS_UNO_DATOS;
drop TABLE POS_DOS_DATOS;
drop TABLE POS_TRES_DATOS;
drop TABLE POS_CUATRO_DATOS;
drop TABLE SIGN_DATOS;
drop TABLE AN_DAT_POS_UNO;
drop TABLE AN_DAT_POS_DOS;
drop TABLE AN_DAT_POS_TRES;
drop TABLE AN_DAT_POS_CUATRO;
drop TABLE AN_DAT_SIGN;
drop TABLE Datos_Temp_Depur;
drop SEQUENCE SQ_Datos_Temp;
drop SEQUENCE SQ_POS_UNO_DATOS;
drop SEQUENCE SQ_POS_DOS_DATOS;
drop SEQUENCE SQ_POS_TRES_DATOS;
drop SEQUENCE SQ_POS_CUATRO_DATOS;
drop SEQUENCE SQ_SIGN_DATOS;
drop SEQUENCE SQ_AN_DAT_POS_UNO;
drop SEQUENCE SQ_AN_DAT_POS_DOS;
drop SEQUENCE SQ_AN_DAT_POS_TRES;
drop SEQUENCE SQ_AN_DAT_POS_CUATRO;
drop SEQUENCE SQ_AN_DAT_SIGN;
drop SEQUENCE SQ_Datos_Temp_dep;

CREATE SEQUENCE SQ_POS_UNO_DATOS MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_POS_DOS_DATOS MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_POS_TRES_DATOS MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_POS_CUATRO_DATOS MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_SIGN_DATOS MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_Datos_Temp MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_AN_DAT_POS_UNO MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_AN_DAT_POS_DOS MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_AN_DAT_POS_TRES MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_AN_DAT_POS_CUATRO MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_AN_DAT_SIGN MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;
CREATE SEQUENCE SQ_DATOS_TEMP_DEP MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 NOCYCLE;


CREATE TABLE POS_UNO_DATOS(
	ID NUMBER primary key,
	Tipo NUMBER,
	PuntuacionTotal NUMBER,
	SinAparecer NUMBER,
	ContadorUltimoEnRachas NUMBER,
	ContadorUltimoEnRachasDesActua NUMBER,
	ContadorGeneral NUMBER,
	ContadorDiaSemana NUMBER,
	ContadorDiaMes NUMBER,
	ContadorDiaModulo NUMBER,
	ContadorMes NUMBER,
	ContadorDespuesActual NUMBER,
	SumatoriaValores NUMBER,
	ContadorDespuesSignActual NUMBER,
	Fecha DATE
);

CREATE TABLE POS_DOS_DATOS(
	ID NUMBER primary key,
	Tipo NUMBER,
	PuntuacionTotal NUMBER,
	SinAparecer NUMBER,
	ContadorUltimoEnRachas NUMBER,
	ContadorUltimoEnRachasDesActua NUMBER,
	ContadorGeneral NUMBER,
	ContadorDiaSemana NUMBER,
	ContadorDiaMes NUMBER,
	ContadorDiaModulo NUMBER,
	ContadorMes NUMBER,
	ContadorDespuesActual NUMBER,
	SumatoriaValores NUMBER,
	ContadorDespuesSignActual NUMBER,
	Fecha DATE
);

CREATE TABLE POS_TRES_DATOS(
	ID NUMBER primary key,
	Tipo NUMBER,
	PuntuacionTotal NUMBER,
	SinAparecer NUMBER,
	ContadorUltimoEnRachas NUMBER,
	ContadorUltimoEnRachasDesActua NUMBER,
	ContadorGeneral NUMBER,
	ContadorDiaSemana NUMBER,
	ContadorDiaMes NUMBER,
	ContadorDiaModulo NUMBER,
	ContadorMes NUMBER,
	ContadorDespuesActual NUMBER,
	SumatoriaValores NUMBER,
	ContadorDespuesSignActual NUMBER,
	Fecha DATE
);

CREATE TABLE POS_CUATRO_DATOS(
	ID NUMBER primary key,
	Tipo NUMBER,
	PuntuacionTotal NUMBER,
	SinAparecer NUMBER,
	ContadorUltimoEnRachas NUMBER,
	ContadorUltimoEnRachasDesActua NUMBER,
	ContadorGeneral NUMBER,
	ContadorDiaSemana NUMBER,
	ContadorDiaMes NUMBER,
	ContadorDiaModulo NUMBER,
	ContadorMes NUMBER,
	ContadorDespuesActual NUMBER,
	SumatoriaValores NUMBER,
	ContadorDespuesSignActual NUMBER,
	Fecha DATE
);

CREATE TABLE SIGN_DATOS(
	ID NUMBER primary key,
	Tipo NUMBER,
	PuntuacionTotal NUMBER,
	SinAparecer NUMBER,
	ContadorUltimoEnRachas NUMBER,
	ContadorUltimoEnRachasDesActua NUMBER,
	ContadorGeneral NUMBER,
	ContadorDiaSemana NUMBER,
	ContadorDiaMes NUMBER,
	ContadorDiaModulo NUMBER,
	ContadorMes NUMBER,
	ContadorDespuesActual NUMBER,
	SumatoriaValores NUMBER,
	ContadorDespuesSignActual NUMBER,
	Fecha DATE
);

CREATE TABLE Datos_Temp(
	ID NUMBER primary key,
	posicion NUMBER,
	Tipo NUMBER,
	Clave NUMBER,
	ClaveSign VARCHAR2(15),
	PuntuacionTotal NUMBER,
	SinAparecer NUMBER,
	ContadorUltimoEnRachas NUMBER,
	ContadorUltimoEnRachasDesActua NUMBER,
	ContadorGeneral NUMBER,
	ContadorDiaSemana NUMBER,
	ContadorDiaMes NUMBER,
	ContadorDiaModulo NUMBER,
	ContadorMes NUMBER,
	ContadorDespuesActual NUMBER,
	SumatoriaValores NUMBER,
	ContadorDespuesSignActual NUMBER,
	Fecha DATE
);


create table AN_DAT_POS_UNO(
	ID NUMBER PRIMARY KEY,
	INDICA_MIN_SIN_APARECER NUMBER,
	INDICA_MIN_ULT_RACH NUMBER,
	COMPARA_ULT_RACH NUMBER,
	INDICA_MIN_CONT_GENERAL NUMBER,
	COMPARA_CONT_GENERAL NUMBER,
	INDICA_MIN_CONT_DIA_SEM NUMBER,
	COMPARA_CONT_DIA_SEM NUMBER,
	INDICA_MIN_CONT_DIA_MES NUMBER,
	COMPARA_CONT_DIA_MES NUMBER,
	INDICA_MIN_CONT_DIA_MOD NUMBER,
	COMPARA_CONT_DIA_MOD NUMBER,
	INDICA_MIN_CONT_MES NUMBER,
	COMPARA_CONT_MES NUMBER,
	INDICA_MIN_CONT_DESP_ACTUAL NUMBER,
	COMPARA_CONT_DESP_ACTUAL NUMBER,
	INDICA_MIN_PUNTUA_TOTAL NUMBER,
	INDICA_MAX_PUNTUA_TOTAL NUMBER,
	DIAMES NUMBER,
	FECHA DATE
);

create table AN_DAT_POS_DOS(
	ID NUMBER PRIMARY KEY,
	INDICA_MIN_SIN_APARECER NUMBER,
	INDICA_MIN_ULT_RACH NUMBER,
	COMPARA_ULT_RACH NUMBER,
	INDICA_MIN_CONT_GENERAL NUMBER,
	COMPARA_CONT_GENERAL NUMBER,
	INDICA_MIN_CONT_DIA_SEM NUMBER,
	COMPARA_CONT_DIA_SEM NUMBER,
	INDICA_MIN_CONT_DIA_MES NUMBER,
	COMPARA_CONT_DIA_MES NUMBER,
	INDICA_MIN_CONT_DIA_MOD NUMBER,
	COMPARA_CONT_DIA_MOD NUMBER,
	INDICA_MIN_CONT_MES NUMBER,
	COMPARA_CONT_MES NUMBER,
	INDICA_MIN_CONT_DESP_ACTUAL NUMBER,
	COMPARA_CONT_DESP_ACTUAL NUMBER,
	INDICA_MIN_PUNTUA_TOTAL NUMBER,
	INDICA_MAX_PUNTUA_TOTAL NUMBER,
	DIAMES NUMBER,
	FECHA DATE
);

create table AN_DAT_POS_TRES(
	ID NUMBER PRIMARY KEY,
	INDICA_MIN_SIN_APARECER NUMBER,
	INDICA_MIN_ULT_RACH NUMBER,
	COMPARA_ULT_RACH NUMBER,
	INDICA_MIN_CONT_GENERAL NUMBER,
	COMPARA_CONT_GENERAL NUMBER,
	INDICA_MIN_CONT_DIA_SEM NUMBER,
	COMPARA_CONT_DIA_SEM NUMBER,
	INDICA_MIN_CONT_DIA_MES NUMBER,
	COMPARA_CONT_DIA_MES NUMBER,
	INDICA_MIN_CONT_DIA_MOD NUMBER,
	COMPARA_CONT_DIA_MOD NUMBER,
	INDICA_MIN_CONT_MES NUMBER,
	COMPARA_CONT_MES NUMBER,
	INDICA_MIN_CONT_DESP_ACTUAL NUMBER,
	COMPARA_CONT_DESP_ACTUAL NUMBER,
	INDICA_MIN_PUNTUA_TOTAL NUMBER,
	INDICA_MAX_PUNTUA_TOTAL NUMBER,
	DIAMES NUMBER,
	FECHA DATE
);

create table AN_DAT_POS_CUATRO(
	ID NUMBER PRIMARY KEY,
	INDICA_MIN_SIN_APARECER NUMBER,
	INDICA_MIN_ULT_RACH NUMBER,
	COMPARA_ULT_RACH NUMBER,
	INDICA_MIN_CONT_GENERAL NUMBER,
	COMPARA_CONT_GENERAL NUMBER,
	INDICA_MIN_CONT_DIA_SEM NUMBER,
	COMPARA_CONT_DIA_SEM NUMBER,
	INDICA_MIN_CONT_DIA_MES NUMBER,
	COMPARA_CONT_DIA_MES NUMBER,
	INDICA_MIN_CONT_DIA_MOD NUMBER,
	COMPARA_CONT_DIA_MOD NUMBER,
	INDICA_MIN_CONT_MES NUMBER,
	COMPARA_CONT_MES NUMBER,
	INDICA_MIN_CONT_DESP_ACTUAL NUMBER,
	COMPARA_CONT_DESP_ACTUAL NUMBER,
	INDICA_MIN_PUNTUA_TOTAL NUMBER,
	INDICA_MAX_PUNTUA_TOTAL NUMBER,
	DIAMES NUMBER,
	FECHA DATE
);

create table AN_DAT_SIGN(
	ID NUMBER PRIMARY KEY,
	INDICA_MIN_SIN_APARECER NUMBER,
	INDICA_MIN_ULT_RACH NUMBER,
	COMPARA_ULT_RACH NUMBER,
	INDICA_MIN_CONT_GENERAL NUMBER,
	COMPARA_CONT_GENERAL NUMBER,
	INDICA_MIN_CONT_DIA_SEM NUMBER,
	COMPARA_CONT_DIA_SEM NUMBER,
	INDICA_MIN_CONT_DIA_MES NUMBER,
	COMPARA_CONT_DIA_MES NUMBER,
	INDICA_MIN_CONT_DIA_MOD NUMBER,
	COMPARA_CONT_DIA_MOD NUMBER,
	INDICA_MIN_CONT_MES NUMBER,
	COMPARA_CONT_MES NUMBER,
	INDICA_MIN_CONT_DESP_ACTUAL NUMBER,
	COMPARA_CONT_DESP_ACTUAL NUMBER,
	INDICA_MIN_PUNTUA_TOTAL NUMBER,
	INDICA_MAX_PUNTUA_TOTAL NUMBER,
	DIAMES NUMBER,
	FECHA DATE
);

CREATE TABLE Datos_Temp_Depur(
	ID NUMBER primary key,
	posicion NUMBER,
	Tipo NUMBER,
	Clave NUMBER,
	ClaveSign VARCHAR2(15),
	PuntuacionTotal NUMBER,
	SinAparecer NUMBER,
	ContadorUltimoEnRachas NUMBER,
	ContadorUltimoEnRachasDesActua NUMBER,
	ContadorGeneral NUMBER,
	ContadorDiaSemana NUMBER,
	ContadorDiaMes NUMBER,
	ContadorDiaModulo NUMBER,
	ContadorMes NUMBER,
	ContadorDespuesActual NUMBER,
	SumatoriaValores NUMBER,
	ContadorDespuesSignActual NUMBER,
	Fecha DATE
);

create or replace trigger TR_INSERT_POS_UNO_DATOS_AFT
before insert on POS_UNO_DATOS
for each row
declare
var_min_dato_sin_aparecer NUMBER;
var_ind_min_sin_aparecer NUMBER;
var_min_dato_cont_ult_rach NUMBER;
var_ind_min_cont_ult_rach NUMBER;
var_anterior_cont_ult_rach NUMBER;
var_comparador_ult_rach NUMBER;
var_min_dato_cont_general NUMBER;
var_ind_min_cont_general NUMBER;
var_anterior_cont_general NUMBER;
var_comparador_general NUMBER;
var_min_dato_cont_dia_semana NUMBER;
var_ind_min_cont_dia_semana NUMBER;
var_anterior_cont_dia_semana NUMBER;
var_comparador_dia_semana NUMBER;
var_min_dato_cont_dia_mes NUMBER;
var_ind_min_cont_dia_mes NUMBER;
var_anterior_cont_dia_mes NUMBER;
var_comparador_dia_mes NUMBER;
var_min_dato_cont_dia_modulo NUMBER;
var_ind_min_cont_dia_modulo NUMBER;
var_anterior_cont_dia_modulo NUMBER;
var_comparador_dia_modulo NUMBER;
var_min_dato_cont_mes NUMBER;
var_ind_min_cont_mes NUMBER;
var_anterior_cont_mes NUMBER;
var_comparador_mes NUMBER;
var_min_dato_cont_desp_actual NUMBER;
var_ind_min_cont_desp_actual NUMBER;
var_anterior_cont_desp_actual NUMBER;
var_comparador_desp_actual NUMBER;
var_min_dato_puntua_total NUMBER;
var_indica_min_puntua_total NUMBER;
var_max_dato_puntua_total NUMBER;
var_indica_max_puntua_total NUMBER;
begin
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
	WHERE fecha = :new.fecha
	AND posicion = 1;
	SELECT contadorultimoenrachas, 
		contadorgeneral, 
		contadordiasemana, 
		contadordiames, 
		contadordiamodulo, 
		contadormes, 
		ContadorDespuesActual
	INTO var_anterior_cont_ult_rach, 
		var_anterior_cont_general, 
		var_anterior_cont_dia_semana, 
		var_anterior_cont_dia_mes, 
		var_anterior_cont_dia_modulo, 
		var_anterior_cont_mes, 
		var_anterior_cont_desp_actual
	FROM POS_UNO_DATOS 
	WHERE fecha = :new.fecha-1;
	IF :new.sinaparecer = var_min_dato_sin_aparecer
		THEN var_ind_min_sin_aparecer := 1;
	ELSE var_ind_min_sin_aparecer := 0;
	END IF;
    IF :new.contadorultimoenrachas = var_min_dato_cont_ult_rach 
    	THEN var_ind_min_cont_ult_rach := 1;
    ELSE var_ind_min_cont_ult_rach := 0;
    END IF;
    IF :new.contadorultimoenrachas = var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := 0;
    ELSIF :new.contadorultimoenrachas < var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := -1;
    ELSE var_comparador_ult_rach := 1;
    END IF;
    IF :new.contadorgeneral = var_min_dato_cont_general 
		THEN var_ind_min_cont_general := 1;
	ELSE var_ind_min_cont_general := 0;
	END IF;
	IF :new.contadorgeneral = var_anterior_cont_general 
		THEN var_comparador_general := 0;
	ELSIF :new.contadorgeneral < var_anterior_cont_general 
		THEN var_comparador_general := -1;
	ELSE var_comparador_general := 1;
	END IF;
	IF :new.contadordiasemana = var_min_dato_cont_dia_semana 
		THEN var_ind_min_cont_dia_semana := 1;
	ELSE var_ind_min_cont_dia_semana := 0;
	END IF;
	IF :new.contadordiasemana = var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := 0;
	ELSIF :new.contadordiasemana < var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := -1;
	ELSE var_comparador_dia_semana := 1;
	END IF;
	IF :new.contadordiames = var_min_dato_cont_dia_mes 
		THEN var_ind_min_cont_dia_mes := 1;
	ELSE var_ind_min_cont_dia_mes := 0;
	END IF;
	IF :new.contadordiames = var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := 0;
	ELSIF :new.contadordiames < var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := -1;
	ELSE var_comparador_dia_mes := 1;
	END IF;
	IF :new.contadordiamodulo = var_min_dato_cont_dia_modulo 
		THEN var_ind_min_cont_dia_modulo := 1;
	ELSE var_ind_min_cont_dia_modulo := 0;
	END IF;
	IF :new.contadordiamodulo = var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := 0;
	ELSIF :new.contadordiamodulo < var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := -1;
	ELSE var_comparador_dia_modulo := 1;
	END IF;
	IF :new.contadormes = var_min_dato_cont_mes 
		THEN var_ind_min_cont_mes := 1;
	ELSE var_ind_min_cont_mes := 0;
	END IF;
	IF :new.contadormes = var_anterior_cont_mes 
		THEN var_comparador_mes := 0;
	ELSIF :new.contadormes < var_anterior_cont_mes 
		THEN var_comparador_mes := -1;
	ELSE var_comparador_mes := 1;
	END IF;
	IF :new.ContadorDespuesActual = var_min_dato_cont_desp_actual 
		THEN var_ind_min_cont_desp_actual := 1;
	ELSE var_ind_min_cont_desp_actual := 0;
	END IF;
	IF :new.ContadorDespuesActual = var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := 0;
	ELSIF :new.ContadorDespuesActual < var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := -1;
	ELSE var_comparador_desp_actual := 1;
	END IF;
	IF :new.PuntuacionTotal = var_min_dato_puntua_total 
		THEN var_indica_min_puntua_total := 1;
	ELSE var_indica_min_puntua_total := 0;
	END IF;
	IF :new.PuntuacionTotal = var_max_dato_puntua_total 
		THEN var_indica_max_puntua_total := 1;
	ELSE var_indica_max_puntua_total := 0;
	END IF;
	INSERT INTO AN_DAT_POS_UNO(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
		INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, 
		COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, 
		INDICA_MIN_PUNTUA_TOTAL, INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
	 VALUES (SQ_AN_DAT_POS_UNO.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, 
	 	var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, 
	 	var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, 
	 	var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
	 	var_indica_max_puntua_total, EXTRACT(DAY FROM :new.fecha), :new.FECHA);
end;

create or replace trigger TR_INSERT_POS_DOS_DATOS_AFT
before insert on POS_DOS_DATOS
for each row
declare
var_min_dato_sin_aparecer NUMBER;
var_ind_min_sin_aparecer NUMBER;
var_min_dato_cont_ult_rach NUMBER;
var_ind_min_cont_ult_rach NUMBER;
var_anterior_cont_ult_rach NUMBER;
var_comparador_ult_rach NUMBER;
var_min_dato_cont_general NUMBER;
var_ind_min_cont_general NUMBER;
var_anterior_cont_general NUMBER;
var_comparador_general NUMBER;
var_min_dato_cont_dia_semana NUMBER;
var_ind_min_cont_dia_semana NUMBER;
var_anterior_cont_dia_semana NUMBER;
var_comparador_dia_semana NUMBER;
var_min_dato_cont_dia_mes NUMBER;
var_ind_min_cont_dia_mes NUMBER;
var_anterior_cont_dia_mes NUMBER;
var_comparador_dia_mes NUMBER;
var_min_dato_cont_dia_modulo NUMBER;
var_ind_min_cont_dia_modulo NUMBER;
var_anterior_cont_dia_modulo NUMBER;
var_comparador_dia_modulo NUMBER;
var_min_dato_cont_mes NUMBER;
var_ind_min_cont_mes NUMBER;
var_anterior_cont_mes NUMBER;
var_comparador_mes NUMBER;
var_min_dato_cont_desp_actual NUMBER;
var_ind_min_cont_desp_actual NUMBER;
var_anterior_cont_desp_actual NUMBER;
var_comparador_desp_actual NUMBER;
var_min_dato_puntua_total NUMBER;
var_indica_min_puntua_total NUMBER;
var_max_dato_puntua_total NUMBER;
var_indica_max_puntua_total NUMBER;
begin
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
	WHERE fecha = :new.fecha
	AND posicion = 1;
	SELECT contadorultimoenrachas, 
		contadorgeneral, 
		contadordiasemana, 
		contadordiames, 
		contadordiamodulo, 
		contadormes, 
		ContadorDespuesActual
	INTO var_anterior_cont_ult_rach, 
		var_anterior_cont_general, 
		var_anterior_cont_dia_semana, 
		var_anterior_cont_dia_mes, 
		var_anterior_cont_dia_modulo, 
		var_anterior_cont_mes, 
		var_anterior_cont_desp_actual
	FROM POS_DOS_DATOS 
	WHERE fecha = :new.fecha-1;
	IF :new.sinaparecer = var_min_dato_sin_aparecer
		THEN var_ind_min_sin_aparecer := 1;
	ELSE var_ind_min_sin_aparecer := 0;
	END IF;
    IF :new.contadorultimoenrachas = var_min_dato_cont_ult_rach 
    	THEN var_ind_min_cont_ult_rach := 1;
    ELSE var_ind_min_cont_ult_rach := 0;
    END IF;
    IF :new.contadorultimoenrachas = var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := 0;
    ELSIF :new.contadorultimoenrachas < var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := -1;
    ELSE var_comparador_ult_rach := 1;
    END IF;
    IF :new.contadorgeneral = var_min_dato_cont_general 
		THEN var_ind_min_cont_general := 1;
	ELSE var_ind_min_cont_general := 0;
	END IF;
	IF :new.contadorgeneral = var_anterior_cont_general 
		THEN var_comparador_general := 0;
	ELSIF :new.contadorgeneral < var_anterior_cont_general 
		THEN var_comparador_general := -1;
	ELSE var_comparador_general := 1;
	END IF;
	IF :new.contadordiasemana = var_min_dato_cont_dia_semana 
		THEN var_ind_min_cont_dia_semana := 1;
	ELSE var_ind_min_cont_dia_semana := 0;
	END IF;
	IF :new.contadordiasemana = var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := 0;
	ELSIF :new.contadordiasemana < var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := -1;
	ELSE var_comparador_dia_semana := 1;
	END IF;
	IF :new.contadordiames = var_min_dato_cont_dia_mes 
		THEN var_ind_min_cont_dia_mes := 1;
	ELSE var_ind_min_cont_dia_mes := 0;
	END IF;
	IF :new.contadordiames = var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := 0;
	ELSIF :new.contadordiames < var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := -1;
	ELSE var_comparador_dia_mes := 1;
	END IF;
	IF :new.contadordiamodulo = var_min_dato_cont_dia_modulo 
		THEN var_ind_min_cont_dia_modulo := 1;
	ELSE var_ind_min_cont_dia_modulo := 0;
	END IF;
	IF :new.contadordiamodulo = var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := 0;
	ELSIF :new.contadordiamodulo < var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := -1;
	ELSE var_comparador_dia_modulo := 1;
	END IF;
	IF :new.contadormes = var_min_dato_cont_mes 
		THEN var_ind_min_cont_mes := 1;
	ELSE var_ind_min_cont_mes := 0;
	END IF;
	IF :new.contadormes = var_anterior_cont_mes 
		THEN var_comparador_mes := 0;
	ELSIF :new.contadormes < var_anterior_cont_mes 
		THEN var_comparador_mes := -1;
	ELSE var_comparador_mes := 1;
	END IF;
	IF :new.ContadorDespuesActual = var_min_dato_cont_desp_actual 
		THEN var_ind_min_cont_desp_actual := 1;
	ELSE var_ind_min_cont_desp_actual := 0;
	END IF;
	IF :new.ContadorDespuesActual = var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := 0;
	ELSIF :new.ContadorDespuesActual < var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := -1;
	ELSE var_comparador_desp_actual := 1;
	END IF;
	IF :new.PuntuacionTotal = var_min_dato_puntua_total 
		THEN var_indica_min_puntua_total := 1;
	ELSE var_indica_min_puntua_total := 0;
	END IF;
	IF :new.PuntuacionTotal = var_max_dato_puntua_total 
		THEN var_indica_max_puntua_total := 1;
	ELSE var_indica_max_puntua_total := 0;
	END IF;
	INSERT INTO AN_DAT_POS_DOS(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
		INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, 
		COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, 
		INDICA_MIN_PUNTUA_TOTAL, INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
	 VALUES (SQ_AN_DAT_POS_DOS.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, 
	 	var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, 
	 	var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, 
	 	var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
	 	var_indica_max_puntua_total, EXTRACT(DAY FROM :new.fecha), :new.FECHA);
end;

create or replace trigger TR_INSERT_POS_TRES_DATOS_AFT
before insert on POS_TRES_DATOS
for each row
declare
var_min_dato_sin_aparecer NUMBER;
var_ind_min_sin_aparecer NUMBER;
var_min_dato_cont_ult_rach NUMBER;
var_ind_min_cont_ult_rach NUMBER;
var_anterior_cont_ult_rach NUMBER;
var_comparador_ult_rach NUMBER;
var_min_dato_cont_general NUMBER;
var_ind_min_cont_general NUMBER;
var_anterior_cont_general NUMBER;
var_comparador_general NUMBER;
var_min_dato_cont_dia_semana NUMBER;
var_ind_min_cont_dia_semana NUMBER;
var_anterior_cont_dia_semana NUMBER;
var_comparador_dia_semana NUMBER;
var_min_dato_cont_dia_mes NUMBER;
var_ind_min_cont_dia_mes NUMBER;
var_anterior_cont_dia_mes NUMBER;
var_comparador_dia_mes NUMBER;
var_min_dato_cont_dia_modulo NUMBER;
var_ind_min_cont_dia_modulo NUMBER;
var_anterior_cont_dia_modulo NUMBER;
var_comparador_dia_modulo NUMBER;
var_min_dato_cont_mes NUMBER;
var_ind_min_cont_mes NUMBER;
var_anterior_cont_mes NUMBER;
var_comparador_mes NUMBER;
var_min_dato_cont_desp_actual NUMBER;
var_ind_min_cont_desp_actual NUMBER;
var_anterior_cont_desp_actual NUMBER;
var_comparador_desp_actual NUMBER;
var_min_dato_puntua_total NUMBER;
var_indica_min_puntua_total NUMBER;
var_max_dato_puntua_total NUMBER;
var_indica_max_puntua_total NUMBER;
begin
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
	WHERE fecha = :new.fecha
	AND posicion = 1;
	SELECT contadorultimoenrachas, 
		contadorgeneral, 
		contadordiasemana, 
		contadordiames, 
		contadordiamodulo, 
		contadormes, 
		ContadorDespuesActual
	INTO var_anterior_cont_ult_rach, 
		var_anterior_cont_general, 
		var_anterior_cont_dia_semana, 
		var_anterior_cont_dia_mes, 
		var_anterior_cont_dia_modulo, 
		var_anterior_cont_mes, 
		var_anterior_cont_desp_actual
	FROM POS_TRES_DATOS 
	WHERE fecha = :new.fecha-1;
	IF :new.sinaparecer = var_min_dato_sin_aparecer
		THEN var_ind_min_sin_aparecer := 1;
	ELSE var_ind_min_sin_aparecer := 0;
	END IF;
    IF :new.contadorultimoenrachas = var_min_dato_cont_ult_rach 
    	THEN var_ind_min_cont_ult_rach := 1;
    ELSE var_ind_min_cont_ult_rach := 0;
    END IF;
    IF :new.contadorultimoenrachas = var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := 0;
    ELSIF :new.contadorultimoenrachas < var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := -1;
    ELSE var_comparador_ult_rach := 1;
    END IF;
    IF :new.contadorgeneral = var_min_dato_cont_general 
		THEN var_ind_min_cont_general := 1;
	ELSE var_ind_min_cont_general := 0;
	END IF;
	IF :new.contadorgeneral = var_anterior_cont_general 
		THEN var_comparador_general := 0;
	ELSIF :new.contadorgeneral < var_anterior_cont_general 
		THEN var_comparador_general := -1;
	ELSE var_comparador_general := 1;
	END IF;
	IF :new.contadordiasemana = var_min_dato_cont_dia_semana 
		THEN var_ind_min_cont_dia_semana := 1;
	ELSE var_ind_min_cont_dia_semana := 0;
	END IF;
	IF :new.contadordiasemana = var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := 0;
	ELSIF :new.contadordiasemana < var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := -1;
	ELSE var_comparador_dia_semana := 1;
	END IF;
	IF :new.contadordiames = var_min_dato_cont_dia_mes 
		THEN var_ind_min_cont_dia_mes := 1;
	ELSE var_ind_min_cont_dia_mes := 0;
	END IF;
	IF :new.contadordiames = var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := 0;
	ELSIF :new.contadordiames < var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := -1;
	ELSE var_comparador_dia_mes := 1;
	END IF;
	IF :new.contadordiamodulo = var_min_dato_cont_dia_modulo 
		THEN var_ind_min_cont_dia_modulo := 1;
	ELSE var_ind_min_cont_dia_modulo := 0;
	END IF;
	IF :new.contadordiamodulo = var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := 0;
	ELSIF :new.contadordiamodulo < var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := -1;
	ELSE var_comparador_dia_modulo := 1;
	END IF;
	IF :new.contadormes = var_min_dato_cont_mes 
		THEN var_ind_min_cont_mes := 1;
	ELSE var_ind_min_cont_mes := 0;
	END IF;
	IF :new.contadormes = var_anterior_cont_mes 
		THEN var_comparador_mes := 0;
	ELSIF :new.contadormes < var_anterior_cont_mes 
		THEN var_comparador_mes := -1;
	ELSE var_comparador_mes := 1;
	END IF;
	IF :new.ContadorDespuesActual = var_min_dato_cont_desp_actual 
		THEN var_ind_min_cont_desp_actual := 1;
	ELSE var_ind_min_cont_desp_actual := 0;
	END IF;
	IF :new.ContadorDespuesActual = var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := 0;
	ELSIF :new.ContadorDespuesActual < var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := -1;
	ELSE var_comparador_desp_actual := 1;
	END IF;
	IF :new.PuntuacionTotal = var_min_dato_puntua_total 
		THEN var_indica_min_puntua_total := 1;
	ELSE var_indica_min_puntua_total := 0;
	END IF;
	IF :new.PuntuacionTotal = var_max_dato_puntua_total 
		THEN var_indica_max_puntua_total := 1;
	ELSE var_indica_max_puntua_total := 0;
	END IF;
	INSERT INTO AN_DAT_POS_TRES(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
		INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, 
		COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, 
		INDICA_MIN_PUNTUA_TOTAL, INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
	 VALUES (SQ_AN_DAT_POS_TRES.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, 
	 	var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, 
	 	var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, 
	 	var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
	 	var_indica_max_puntua_total, EXTRACT(DAY FROM :new.fecha), :new.FECHA);
end;

create or replace trigger TR_INSERT_POS_CUATRO_DATOS_AFT
before insert on POS_CUATRO_DATOS
for each row
declare
var_min_dato_sin_aparecer NUMBER;
var_ind_min_sin_aparecer NUMBER;
var_min_dato_cont_ult_rach NUMBER;
var_ind_min_cont_ult_rach NUMBER;
var_anterior_cont_ult_rach NUMBER;
var_comparador_ult_rach NUMBER;
var_min_dato_cont_general NUMBER;
var_ind_min_cont_general NUMBER;
var_anterior_cont_general NUMBER;
var_comparador_general NUMBER;
var_min_dato_cont_dia_semana NUMBER;
var_ind_min_cont_dia_semana NUMBER;
var_anterior_cont_dia_semana NUMBER;
var_comparador_dia_semana NUMBER;
var_min_dato_cont_dia_mes NUMBER;
var_ind_min_cont_dia_mes NUMBER;
var_anterior_cont_dia_mes NUMBER;
var_comparador_dia_mes NUMBER;
var_min_dato_cont_dia_modulo NUMBER;
var_ind_min_cont_dia_modulo NUMBER;
var_anterior_cont_dia_modulo NUMBER;
var_comparador_dia_modulo NUMBER;
var_min_dato_cont_mes NUMBER;
var_ind_min_cont_mes NUMBER;
var_anterior_cont_mes NUMBER;
var_comparador_mes NUMBER;
var_min_dato_cont_desp_actual NUMBER;
var_ind_min_cont_desp_actual NUMBER;
var_anterior_cont_desp_actual NUMBER;
var_comparador_desp_actual NUMBER;
var_min_dato_puntua_total NUMBER;
var_indica_min_puntua_total NUMBER;
var_max_dato_puntua_total NUMBER;
var_indica_max_puntua_total NUMBER;
begin
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
	WHERE fecha = :new.fecha
	AND posicion = 1;
	SELECT contadorultimoenrachas, 
		contadorgeneral, 
		contadordiasemana, 
		contadordiames, 
		contadordiamodulo, 
		contadormes, 
		ContadorDespuesActual
	INTO var_anterior_cont_ult_rach, 
		var_anterior_cont_general, 
		var_anterior_cont_dia_semana, 
		var_anterior_cont_dia_mes, 
		var_anterior_cont_dia_modulo, 
		var_anterior_cont_mes, 
		var_anterior_cont_desp_actual
	FROM POS_CUATRO_DATOS 
	WHERE fecha = :new.fecha-1;
	IF :new.sinaparecer = var_min_dato_sin_aparecer
		THEN var_ind_min_sin_aparecer := 1;
	ELSE var_ind_min_sin_aparecer := 0;
	END IF;
    IF :new.contadorultimoenrachas = var_min_dato_cont_ult_rach 
    	THEN var_ind_min_cont_ult_rach := 1;
    ELSE var_ind_min_cont_ult_rach := 0;
    END IF;
    IF :new.contadorultimoenrachas = var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := 0;
    ELSIF :new.contadorultimoenrachas < var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := -1;
    ELSE var_comparador_ult_rach := 1;
    END IF;
    IF :new.contadorgeneral = var_min_dato_cont_general 
		THEN var_ind_min_cont_general := 1;
	ELSE var_ind_min_cont_general := 0;
	END IF;
	IF :new.contadorgeneral = var_anterior_cont_general 
		THEN var_comparador_general := 0;
	ELSIF :new.contadorgeneral < var_anterior_cont_general 
		THEN var_comparador_general := -1;
	ELSE var_comparador_general := 1;
	END IF;
	IF :new.contadordiasemana = var_min_dato_cont_dia_semana 
		THEN var_ind_min_cont_dia_semana := 1;
	ELSE var_ind_min_cont_dia_semana := 0;
	END IF;
	IF :new.contadordiasemana = var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := 0;
	ELSIF :new.contadordiasemana < var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := -1;
	ELSE var_comparador_dia_semana := 1;
	END IF;
	IF :new.contadordiames = var_min_dato_cont_dia_mes 
		THEN var_ind_min_cont_dia_mes := 1;
	ELSE var_ind_min_cont_dia_mes := 0;
	END IF;
	IF :new.contadordiames = var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := 0;
	ELSIF :new.contadordiames < var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := -1;
	ELSE var_comparador_dia_mes := 1;
	END IF;
	IF :new.contadordiamodulo = var_min_dato_cont_dia_modulo 
		THEN var_ind_min_cont_dia_modulo := 1;
	ELSE var_ind_min_cont_dia_modulo := 0;
	END IF;
	IF :new.contadordiamodulo = var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := 0;
	ELSIF :new.contadordiamodulo < var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := -1;
	ELSE var_comparador_dia_modulo := 1;
	END IF;
	IF :new.contadormes = var_min_dato_cont_mes 
		THEN var_ind_min_cont_mes := 1;
	ELSE var_ind_min_cont_mes := 0;
	END IF;
	IF :new.contadormes = var_anterior_cont_mes 
		THEN var_comparador_mes := 0;
	ELSIF :new.contadormes < var_anterior_cont_mes 
		THEN var_comparador_mes := -1;
	ELSE var_comparador_mes := 1;
	END IF;
	IF :new.ContadorDespuesActual = var_min_dato_cont_desp_actual 
		THEN var_ind_min_cont_desp_actual := 1;
	ELSE var_ind_min_cont_desp_actual := 0;
	END IF;
	IF :new.ContadorDespuesActual = var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := 0;
	ELSIF :new.ContadorDespuesActual < var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := -1;
	ELSE var_comparador_desp_actual := 1;
	END IF;
	IF :new.PuntuacionTotal = var_min_dato_puntua_total 
		THEN var_indica_min_puntua_total := 1;
	ELSE var_indica_min_puntua_total := 0;
	END IF;
	IF :new.PuntuacionTotal = var_max_dato_puntua_total 
		THEN var_indica_max_puntua_total := 1;
	ELSE var_indica_max_puntua_total := 0;
	END IF;
	INSERT INTO AN_DAT_POS_CUATRO(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
		INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, 
		COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, 
		INDICA_MIN_PUNTUA_TOTAL, INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
	 VALUES (SQ_AN_DAT_POS_CUATRO.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, 
	 	var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, 
	 	var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, 
	 	var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
	 	var_indica_max_puntua_total, EXTRACT(DAY FROM :new.fecha), :new.FECHA);
end;

create or replace trigger TR_INSERT_SIGN_DATOS_AFT
before insert on SIGN_DATOS
for each row
declare
var_min_dato_sin_aparecer NUMBER;
var_ind_min_sin_aparecer NUMBER;
var_min_dato_cont_ult_rach NUMBER;
var_ind_min_cont_ult_rach NUMBER;
var_anterior_cont_ult_rach NUMBER;
var_comparador_ult_rach NUMBER;
var_min_dato_cont_general NUMBER;
var_ind_min_cont_general NUMBER;
var_anterior_cont_general NUMBER;
var_comparador_general NUMBER;
var_min_dato_cont_dia_semana NUMBER;
var_ind_min_cont_dia_semana NUMBER;
var_anterior_cont_dia_semana NUMBER;
var_comparador_dia_semana NUMBER;
var_min_dato_cont_dia_mes NUMBER;
var_ind_min_cont_dia_mes NUMBER;
var_anterior_cont_dia_mes NUMBER;
var_comparador_dia_mes NUMBER;
var_min_dato_cont_dia_modulo NUMBER;
var_ind_min_cont_dia_modulo NUMBER;
var_anterior_cont_dia_modulo NUMBER;
var_comparador_dia_modulo NUMBER;
var_min_dato_cont_mes NUMBER;
var_ind_min_cont_mes NUMBER;
var_anterior_cont_mes NUMBER;
var_comparador_mes NUMBER;
var_min_dato_cont_desp_actual NUMBER;
var_ind_min_cont_desp_actual NUMBER;
var_anterior_cont_desp_actual NUMBER;
var_comparador_desp_actual NUMBER;
var_min_dato_puntua_total NUMBER;
var_indica_min_puntua_total NUMBER;
var_max_dato_puntua_total NUMBER;
var_indica_max_puntua_total NUMBER;
begin
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
	WHERE fecha = :new.fecha
	AND posicion = 5;
	SELECT contadorultimoenrachas, 
		contadorgeneral, 
		contadordiasemana, 
		contadordiames, 
		contadordiamodulo, 
		contadormes, 
		ContadorDespuesActual
	INTO var_anterior_cont_ult_rach, 
		var_anterior_cont_general, 
		var_anterior_cont_dia_semana, 
		var_anterior_cont_dia_mes, 
		var_anterior_cont_dia_modulo, 
		var_anterior_cont_mes, 
		var_anterior_cont_desp_actual
	FROM SIGN_DATOS 
	WHERE fecha = :new.fecha-1;
	IF :new.sinaparecer = var_min_dato_sin_aparecer
		THEN var_ind_min_sin_aparecer := 1;
	ELSE var_ind_min_sin_aparecer := 0;
	END IF;
    IF :new.contadorultimoenrachas = var_min_dato_cont_ult_rach 
    	THEN var_ind_min_cont_ult_rach := 1;
    ELSE var_ind_min_cont_ult_rach := 0;
    END IF;
    IF :new.contadorultimoenrachas = var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := 0;
    ELSIF :new.contadorultimoenrachas < var_anterior_cont_ult_rach 
    	THEN var_comparador_ult_rach := -1;
    ELSE var_comparador_ult_rach := 1;
    END IF;
    IF :new.contadorgeneral = var_min_dato_cont_general 
		THEN var_ind_min_cont_general := 1;
	ELSE var_ind_min_cont_general := 0;
	END IF;
	IF :new.contadorgeneral = var_anterior_cont_general 
		THEN var_comparador_general := 0;
	ELSIF :new.contadorgeneral < var_anterior_cont_general 
		THEN var_comparador_general := -1;
	ELSE var_comparador_general := 1;
	END IF;
	IF :new.contadordiasemana = var_min_dato_cont_dia_semana 
		THEN var_ind_min_cont_dia_semana := 1;
	ELSE var_ind_min_cont_dia_semana := 0;
	END IF;
	IF :new.contadordiasemana = var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := 0;
	ELSIF :new.contadordiasemana < var_anterior_cont_dia_semana 
		THEN var_comparador_dia_semana := -1;
	ELSE var_comparador_dia_semana := 1;
	END IF;
	IF :new.contadordiames = var_min_dato_cont_dia_mes 
		THEN var_ind_min_cont_dia_mes := 1;
	ELSE var_ind_min_cont_dia_mes := 0;
	END IF;
	IF :new.contadordiames = var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := 0;
	ELSIF :new.contadordiames < var_anterior_cont_dia_mes 
		THEN var_comparador_dia_mes := -1;
	ELSE var_comparador_dia_mes := 1;
	END IF;
	IF :new.contadordiamodulo = var_min_dato_cont_dia_modulo 
		THEN var_ind_min_cont_dia_modulo := 1;
	ELSE var_ind_min_cont_dia_modulo := 0;
	END IF;
	IF :new.contadordiamodulo = var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := 0;
	ELSIF :new.contadordiamodulo < var_anterior_cont_dia_modulo 
		THEN var_comparador_dia_modulo := -1;
	ELSE var_comparador_dia_modulo := 1;
	END IF;
	IF :new.contadormes = var_min_dato_cont_mes 
		THEN var_ind_min_cont_mes := 1;
	ELSE var_ind_min_cont_mes := 0;
	END IF;
	IF :new.contadormes = var_anterior_cont_mes 
		THEN var_comparador_mes := 0;
	ELSIF :new.contadormes < var_anterior_cont_mes 
		THEN var_comparador_mes := -1;
	ELSE var_comparador_mes := 1;
	END IF;
	IF :new.ContadorDespuesActual = var_min_dato_cont_desp_actual 
		THEN var_ind_min_cont_desp_actual := 1;
	ELSE var_ind_min_cont_desp_actual := 0;
	END IF;
	IF :new.ContadorDespuesActual = var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := 0;
	ELSIF :new.ContadorDespuesActual < var_anterior_cont_desp_actual 
		THEN var_comparador_desp_actual := -1;
	ELSE var_comparador_desp_actual := 1;
	END IF;
	IF :new.PuntuacionTotal = var_min_dato_puntua_total 
		THEN var_indica_min_puntua_total := 1;
	ELSE var_indica_min_puntua_total := 0;
	END IF;
	IF :new.PuntuacionTotal = var_max_dato_puntua_total 
		THEN var_indica_max_puntua_total := 1;
	ELSE var_indica_max_puntua_total := 0;
	END IF;
	INSERT INTO AN_DAT_SIGN(ID, INDICA_MIN_SIN_APARECER, INDICA_MIN_ULT_RACH, COMPARA_ULT_RACH, INDICA_MIN_CONT_GENERAL, COMPARA_CONT_GENERAL, 
		INDICA_MIN_CONT_DIA_SEM, COMPARA_CONT_DIA_SEM, INDICA_MIN_CONT_DIA_MES, COMPARA_CONT_DIA_MES, INDICA_MIN_CONT_DIA_MOD, 
		COMPARA_CONT_DIA_MOD, INDICA_MIN_CONT_MES, COMPARA_CONT_MES, INDICA_MIN_CONT_DESP_ACTUAL, COMPARA_CONT_DESP_ACTUAL, 
		INDICA_MIN_PUNTUA_TOTAL, INDICA_MAX_PUNTUA_TOTAL, DIAMES, FECHA)
	 VALUES (SQ_AN_DAT_SIGN.nextval, var_ind_min_sin_aparecer, var_ind_min_cont_ult_rach, var_comparador_ult_rach, var_ind_min_cont_general, 
	 	var_comparador_general, var_ind_min_cont_dia_semana, var_comparador_dia_semana, var_ind_min_cont_dia_mes, 
	 	var_comparador_dia_mes, var_ind_min_cont_dia_modulo, var_comparador_dia_modulo, var_ind_min_cont_mes, 
	 	var_comparador_mes, var_ind_min_cont_desp_actual, var_comparador_desp_actual, var_indica_min_puntua_total, 
	 	var_indica_max_puntua_total, EXTRACT(DAY FROM :new.fecha), :new.FECHA);
end;


select * from POS_UNO_DATOS order by fecha desc;
select * from AN_DAT_POS_UNO order by fecha desc;
select * from POS_DOS_DATOS order by fecha desc;
select * from AN_DAT_POS_DOS order by fecha desc;
select * from POS_TRES_DATOS order by fecha desc;
select * from AN_DAT_POS_TRES order by fecha desc;
select * from POS_CUATRO_DATOS order by fecha desc;
select * from AN_DAT_POS_CUATRO order by fecha desc;
select * from SIGN_DATOS order by fecha desc;
select * from AN_DAT_SIGN order by fecha desc;

