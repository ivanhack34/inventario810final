/*Para crear tanro la base de datos y la tabla del login*/
CREATE DATABASE DB_ACCESO

USE DB_ACCESO

CREATE TABLE USUARIO (
IdUsuario int primary key identity(1,1),
Correo varchar(100),
Clave varchar(500)
)

/*El procedimiento para el registro del usuario*/
create proc sp_RegistrarUsuario(
@Correo varchar(100),
@Clave varchar(500),
@Registro bit output,
@Mensaje varchar(100) output
)
as
begin
	if(not exists(select * from USUARIO where Correo = @Correo))
	begin
		insert into USUARIO(Correo,Clave) values(@Correo,@Clave)
		set @Registro = 1
		set @Mensaje = 'usuario registrado'
	end
	else
	begin
		set @Registro = 0
		set @Mensaje = 'correo ya existe'
	end
end

/*Procedimiento para validar el usuario*/
create proc sp_ValidarUsuario(
@Correo varchar(100),
@Clave varchar(500)
)
as
begin
	if(exists(select * from USUARIO where Correo = @Correo and Clave = @Clave))
		select IdUsuario from USUARIO where Correo = @Correo and Clave = @Clave
	else
		select '0'
end

/*Ejecutar el procedimiento para registrar y validar usuario*/

declare @registro bit, @mensaje varchar(100)

exec sp_RegistrarUsuario 'ivan@gmail.com', '123456', @registro output, @mensaje output

select @registro
select @mensaje

select * from USUARIO

exec sp_ValidarUsuario 'ivan@gmail.com', '123456'