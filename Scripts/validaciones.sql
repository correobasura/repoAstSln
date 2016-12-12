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

select count(*) from Datos_Temp_Depur;
set serveroutput on;
declare
cursor fechas is select distinct(fecha) from Datos_Temp_Depur order by fecha;
var_pos_cuatro varchar(20) := '';
var_count_pos_cuatro number:=0;
var_total number := 0;
var_total_cinco number := 0;
begin
    for cur_rec_fechas in fechas
    loop
        select pos_cuatro into var_pos_cuatro from astr where fecha = cur_rec_fechas.fecha;
        select count(1) into var_count_pos_cuatro from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 4 and clave = var_pos_cuatro;
        var_total :=  var_count_pos_cuatro;
        if var_total = 1
            then var_total_cinco := var_total_cinco+1;
        end if;
        var_pos_cuatro := -1;
        var_total := 0;
    end loop;
        DBMS_OUTPUT.PUT_LINE(var_total_cinco);
end;

select count(*) from Datos_Temp_Depur;
set serveroutput on;
declare
cursor fechas is select distinct(fecha) from Datos_Temp_Depur order by fecha;
var_pos_tres varchar(20) := '';
var_count_pos_tres number:=0;
var_total number := 0;
var_total_cinco number := 0;
begin
    for cur_rec_fechas in fechas
    loop
        select pos_tres into var_pos_tres from astr where fecha = cur_rec_fechas.fecha;
        select count(1) into var_count_pos_tres from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 3 and clave = var_pos_tres;
        var_total :=  var_count_pos_tres;
        if var_total = 1
            then var_total_cinco := var_total_cinco+1;
        end if;
        var_pos_tres := -1;
        var_total := 0;
    end loop;
        DBMS_OUTPUT.PUT_LINE(var_total_cinco);
end;

select count(*) from Datos_Temp_Depur;
set serveroutput on;
declare
cursor fechas is select distinct(fecha) from Datos_Temp_Depur order by fecha;
var_pos_dos varchar(20) := '';
var_count_pos_dos number:=0;
var_total number := 0;
var_total_cinco number := 0;
begin
    for cur_rec_fechas in fechas
    loop
        select pos_dos into var_pos_dos from astr where fecha = cur_rec_fechas.fecha;
        select count(1) into var_count_pos_dos from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 2 and clave = var_pos_dos;
        var_total :=  var_count_pos_dos;
        if var_total = 1
            then var_total_cinco := var_total_cinco+1;
        end if;
        var_pos_dos := -1;
        var_total := 0;
    end loop;
        DBMS_OUTPUT.PUT_LINE(var_total_cinco);
end;

select count(*) from Datos_Temp_Depur;
set serveroutput on;
declare
cursor fechas is select distinct(fecha) from Datos_Temp_Depur order by fecha;
var_pos_uno varchar(20) := '';
var_count_pos_uno number:=0;
var_total number := 0;
var_total_cinco number := 0;
begin
    for cur_rec_fechas in fechas
    loop
        select pos_uno into var_pos_uno from astr where fecha = cur_rec_fechas.fecha;
        select count(1) into var_count_pos_uno from Datos_Temp_Depur where fecha = cur_rec_fechas.fecha and posicion = 1 and clave = var_pos_uno;
        var_total :=  var_count_pos_uno;
        if var_total = 1
            then var_total_cinco := var_total_cinco+1;
        end if;
        var_pos_uno := -1;
        var_total := 0;
    end loop;
        DBMS_OUTPUT.PUT_LINE(var_total_cinco);
end;

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
select count(*), posicion, fecha from Datos_Temp_Depur group by posicion, fecha order by 1;