create table Security
(
	SecurityID integer not null primary key identity(1,1),
    SecurityCode varchar(10) not null,
    SecurityAbbr varchar(128) not null,
    CompanyCode varchar(128) not null,
    CompanyAbbr varchar(128) not null,
    ListingDate DateTime not null,
	ExchangeMarket varchar(10) not null
);

CREATE UNIQUE INDEX security_code_index on Security (SecurityCode,ExchangeMarket);

create table SecurityTask(
	TaskID integer not null primary key identity(1,1),
	SecurityCode varchar(10) not null,
	ExchangeMarket varchar(10) not null,
	BeginDate DateTime not null,
	EndDate DateTime not null,
	IsFinished int not null
);

create table SecurityDayQuotation
(
	id integer not null primary key Identity(1,1),
	SecurityCode varchar(10) not null,
	TxDate DateTime not null,
	ClosePrice float not null,
	HighPrice float not null,
	LowPrice float not null,
	OpenPrice float not null,
	LastClosePrice float not null,
	PriceChange float not null,
	Change float not null,
	TurnOver float not null,
	VolumeTurnOver float not null,
	PriceTurnOver float not null,
	MarketValue float not null,
	NegoValue float not null
);

create table log
(
	id integer not null primary key identity(1,1),
	logName VARCHAR(100) not null,
	description varchar(4000) not null 
)

