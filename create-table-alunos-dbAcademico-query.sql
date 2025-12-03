create database dbAcademico;

create table alunos(
prontuario char(9) primary key,
nome varchar(50),
cpf char(11),
rg char(9),
email varchar(50));

select * from alunos;