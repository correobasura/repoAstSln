delete from Datos_Temp_Depur where fecha not in (select fecha from astr);commit;
set serveroutput on;
declare
cursor fechas is select distinct(fecha) from Datos_Temp_Depur order by fecha;
var_pos_uno number:=-1;
var_pos_dos number:=-1;
var_pos_tres number:=-1;
var_pos_cuatro number:=-1;
var_sign varchar(20) := '';
var_count_pos_uno number:=0;
var_count_pos_dos number:=0;
var_count_pos_tres number:=0;
var_count_pos_cuatro number:=0;
var_count_sign number:=0;
var_total number := 0;
var_total_cinco number := 0;
begin
	for cur_rec_fechas in fechas
	loop
		--DBMS_OUTPUT.PUT_LINE(cur_rec_fechas.fecha);
		select pos_uno into var_pos_uno from astr where fecha = cur_rec_fechas.fecha;
		select count(1) into var_count_pos_uno from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 1 and clave = var_pos_uno;
		select pos_dos into var_pos_dos from astr where fecha = cur_rec_fechas.fecha;
		select count(1) into var_count_pos_dos from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 2 and clave = var_pos_dos;
		select pos_tres into var_pos_tres from astr where fecha = cur_rec_fechas.fecha;
		select count(1) into var_count_pos_tres from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 3 and clave = var_pos_tres;
		select pos_cuatro into var_pos_cuatro from astr where fecha = cur_rec_fechas.fecha;
		select count(1) into var_count_pos_cuatro from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 4 and clave = var_pos_cuatro;
		select sign into var_sign from astr where fecha = cur_rec_fechas.fecha;
		select count(1) into var_count_sign from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 5 and clavesign like var_sign;
		var_total :=  var_count_pos_uno + var_count_pos_dos + var_count_pos_tres + var_count_pos_cuatro + var_count_sign;
		if var_total = 5
			then var_total_cinco := var_total_cinco+1;
		end if;
		var_pos_uno :=-1;
		var_pos_dos :=-1;
		var_pos_tres :=-1;
		var_pos_cuatro :=-1;
		var_sign := '';
		var_total := 0;
	end loop;
		DBMS_OUTPUT.PUT_LINE(var_total_cinco);
end;
select count(*) from Datos_Temp_Depur;
select count(*), fecha from Datos_Temp_Depur group by fecha;
delete from Datos_Temp_Depur; commit;

/*En algunos rangos de fechas aparecen 2, en otros ninguno, y el máximo revisado fue 4*/
DECLARE
CURSOR cur_pos_uno is SELECT * FROM Pos_Uno_Datos ORDER BY fecha ASC;
var_dato_maximo NUMBER :=0;
var_contador_apariciones NUMBER :=0;
var_min_fecha DATE;
var_MAX_fecha DATE;
var_span_time NUMBER;
BEGIN
  SELECT MIN(fecha) into var_min_fecha from Pos_Uno_Datos;
  SELECT MAX(fecha) into var_MAX_fecha from Pos_Uno_Datos;
	FOR cur_rec in cur_pos_uno
	LOOP		
		SELECT MAX(puntuaciontotal) 
		INTO
		var_dato_maximo
		FROM datos_temp
		WHERE 	posicion = 1
		AND 	fecha = cur_rec.fecha;
    --DBMS_OUTPUT.PUT_LINE(var_dato_maximo||' '||cur_rec.puntuaciontotal);
		IF var_dato_maximo = cur_rec.puntuaciontotal
			THEN var_contador_apariciones := var_contador_apariciones+1;
      var_span_time := cur_rec.fecha - Var_Min_Fecha;
      Var_Min_Fecha := cur_rec.fecha;
			DBMS_OUTPUT.PUT_LINE(var_span_time);
		END IF;
	END LOOP;
  var_span_time := var_MAX_fecha - Var_Min_Fecha;
  DBMS_OUTPUT.PUT_LINE(var_span_time);
	DBMS_OUTPUT.PUT_LINE(var_contador_apariciones);
END;

/*En algunos rangos de fechas aparecen 2, en otros ninguno, y el máximo revisado fue 4
Lo mismo para el mínimo
*/
DECLARE
CURSOR cur_pos_uno is SELECT * FROM Pos_Uno_Datos ORDER BY fecha ASC;
var_dato_maximo NUMBER :=0;
var_contador_apariciones NUMBER :=0;
var_min_fecha DATE;
var_MAX_fecha DATE;
var_span_time NUMBER;
BEGIN
  SELECT MIN(fecha) into var_min_fecha from Pos_Uno_Datos;
  SELECT MAX(fecha) into var_MAX_fecha from Pos_Uno_Datos;
	FOR cur_rec in cur_pos_uno
	LOOP		
		SELECT MIN(puntuaciontotal) 
		INTO
		var_dato_maximo
		FROM datos_temp
		WHERE 	posicion = 1
		AND 	fecha = cur_rec.fecha;
    --DBMS_OUTPUT.PUT_LINE(var_dato_maximo||' '||cur_rec.puntuaciontotal);
		IF var_dato_maximo = cur_rec.puntuaciontotal
			THEN var_contador_apariciones := var_contador_apariciones+1;
      var_span_time := cur_rec.fecha - Var_Min_Fecha;
      Var_Min_Fecha := cur_rec.fecha;
			DBMS_OUTPUT.PUT_LINE(var_span_time);
		END IF;
	END LOOP;
  var_span_time := var_MAX_fecha - Var_Min_Fecha;
  DBMS_OUTPUT.PUT_LINE(var_span_time);
	DBMS_OUTPUT.PUT_LINE(var_contador_apariciones);
END;