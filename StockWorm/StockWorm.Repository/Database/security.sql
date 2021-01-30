create table if not exists Security
(
	SecurityID integer not null primary key AUTOINCREMENT,
    SecurityCode varchar(10) not null,
    SecurityAbbr varchar(128) not null,
    CompanyCode varchar(128) not null,
    CompanyAbbr varchar(128) not null,
    ListingDate DateTime not null,
	ExchangeMarket varchar(10) not null
);

create table if not exists SecurityTask(
	TaskID integer not null primary key AUTOINCREMENT,
	SecurityCode varchar(10) not null,
	ExchangeMarket varchar(10) not null,
	BeginDate DateTime not null,
	EndDate DateTime not null,
	IsFinished int not null
);

create table if not exists SecurityDayQuotation
(
	id integer not null primary key AUTOINCREMENT,
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

