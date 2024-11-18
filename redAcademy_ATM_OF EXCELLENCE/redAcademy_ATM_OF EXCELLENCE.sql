USE master
GO

create database Atm

use atm
go

Create table USERS
(fName varchar (30),
sName varchar (30),
userName varchar (20),
encryptedPIN varchar(20),
teachername varchar(20),
maidenName varchar(20),
primary key (UserName)
);


use Atm
GO
create table ACCOUNTS
(accountNumber varchar(10),
userName varchar (20),
balance float,
accountType varchar (10),
accountPin varchar(50),
foreign key (userName) References users(userName),
primary key (accountNumber)
);

use Atm
GO
create table TRANSACTIONS
(transactionalID int IDENTITY(300,5) PRIMARY KEY,
accountNumber varchar(10),
transactionType varchar (10),
amount float,
transactionDateTime DateTime,
foreign key (accountNumber) references accounts(accountNumber)
);

USE Atm
GO
select fName, sName, userName,HASHBYTES('MD2',encryptedPIN) as sensitiveData
from USERS

Select accountNumber, userName, balance, accountType,HASHBYTES('MD2',accountPin) as sensitiveData
from ACCOUNTS

Select * from TRANSACTIONS




drop table USERS
drop table TRANSACTIONS
drop table ACCOUNTS

