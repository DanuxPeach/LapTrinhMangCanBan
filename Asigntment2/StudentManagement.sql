CREATE DATABASE StudentManagement;

CREATE TABLE Student
(
	ID int NOT NULL,
	Name nvarchar(40) NOT NULL,
	Sex nvarchar(10) NOT NULL,
	Birth datetime NOT NULL,
	Faculty nvarchar(100) NOT NULL,
	Address nvarchar(100) NOT NULL,

	PRIMARY KEY(ID)
)
drop table Student
set dateformat DMY
INSERT INTO dbo.Student (ID, Name, Birth, Sex, Faculty, Address) Values (21520682, 'Lam Hai Dang', 05/11/2003, 'Nam', 'Communi	cations and Computer Networks', 'Bien Hoa' )