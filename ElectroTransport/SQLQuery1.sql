CREATE TABLE [User](
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
[Login] VARCHAR(20) not NULL,
[Password] VARCHAR(20) NOT NULL,
[Role] VARCHAR(5) NOT NULL
);

create table [Category](
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
[Name] NVARCHAR(MAX) Collate Cyrillic_General_CI_AS null,
);

create table [Manufacturer](
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
[Name] NVARCHAR(MAX) Collate Cyrillic_General_CI_AS null,
);

create table Transport(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
[Name] NVARCHAR(MAX) Collate Cyrillic_General_CI_AS null,
CategoryId int not null,
Speed decimal not null,
WheelDiameter decimal not null,
[Weight] decimal not null,
EnginePower decimal not null,
ManufacturerId int not null,
Price decimal not null,
FOREIGN KEY(CategoryId) references Category(Id),
FOREIGN KEY(ManufacturerId) references Manufacturer(Id)
);

create table TransportAmount(
Id int not null,
TransportId int not null,
[Count] int not null,
FOREIGN KEY(TransportId) references Transport(Id)
);

CREATE TABLE [Order](
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
OrderDate DATETIME NOT NULL);

CREATE TABLE [ListOrder](
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
OrderId int not null,
TransportId int not null,
[Count] int not null,
FOREIGN KEY(TransportId) references Transport(Id),
FOREIGN KEY(OrderId) references [Order](Id)
);