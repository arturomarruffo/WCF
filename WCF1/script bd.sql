use master
go
create database Arturo
go
use Arturo
go
Create Table [User](
UserID int identity(1,1),
Mail varchar(100),
Password varchar(100),
FirstName varchar(100),
LastName varchar(100),
Birthday varchar(100),
Cellphone varchar(100),
Code varchar(100)
)
go
Create Procedure uspObtenerUserSesion  
@Mail as varchar(100),
@Password as varchar(100)
as
Select UserID,Mail,FirstName,LastName,Birthday,Cellphone,Code
From Arturo.dbo.[User]
Where Mail = @Mail and Password = @Password
go

Create Table Search(
SearchID int identity(1,1),
UserID int,
CareerID int,
AsignatureID int,
SearchStatusID int
)
go
Create Procedure uspInsertarSearch
@UserID as int,
@CareerID as int,
@AsignatureID as int,
@SearchStatusID as int
as
Insert Into Search values(@UserID,@CareerID,@AsignatureID,@SearchStatusID)
go

Create Table Career(
CareerID int identity(1,1),
Name varchar(100),
Description varchar(100)
)
go


go

create procedure uspListarCareer
as
Select CareerID, Name From Career Order By Name
go

Create Table Asignature(
AsignatureID int identity(1,1),
Name varchar(100),
Description varchar(100),
CareerID int
)
go

create procedure uspListarAsignaturePorCareerID 
@CareerID as int
as
Select AsignatureID, Name From Asignature Where CareerID = @CareerID

go

insert into Career values('Ing. de Sistemas','Carrera de Ingeniería de Sistemas')
insert into Career values('Ing. Industrial','Carrera de Ingeniería Industrial')
insert into Career values('Administración','Carrera de Administración')

insert into Asignature values('Cálculo I','Curso de Cálculo I',1)
insert into Asignature values('Diseño de Procesos','Curso de Diseño de Procesos',1)
insert into Asignature values('Calidad de Software','Curso de Calidad de Software',1)

insert into Asignature values('Investigación de Operaciones','Curso de IOp',2)

insert into Asignature values('Administración I','Curso de Administración I',3)

insert into Arturo.dbo.[User] values('r@c.com','123','Raúl','Delgado','01/NOV/1989','999111999','-')
