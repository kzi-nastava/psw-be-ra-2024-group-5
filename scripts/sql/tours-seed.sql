DELETE FROM tours."TouristEquipment";
DELETE FROM tours."TourEquipment";
DELETE FROM tours."Equipment";
ALTER SEQUENCE tours."Equipment_Id_seq" RESTART WITH 1;
DELETE FROM tours."KeyPoint";
ALTER SEQUENCE tours."KeyPoint_Id_seq" RESTART WITH 1;
DELETE FROM tours."Facilities";
ALTER SEQUENCE tours."Facilities_Id_seq" RESTART WITH 1;
DELETE FROM tours."TourReviews";
ALTER SEQUENCE tours."TourReviews_Id_seq" RESTART WITH 1;
DELETE FROM tours."Preferences";
ALTER SEQUENCE tours."Preferences_Id_seq" RESTART WITH 1;
DELETE FROM tours."Tours";
ALTER SEQUENCE tours."Tours_Id_seq" RESTART WITH 1;

insert into tours."Tours"
	("Name", "Description", "Tags", "Level", "Status",
	"Price", "AuthorId", "Length", "TransportDurations", "PublishedTime", "ArchivedTime", "NumberOfViews")
values
	('Draft tura', 'Kul je ova tura', '#najbolje', 0, 0,
	'{"Amount":1000,"Currency":0}', 2, 0, '[{"Duration": 10,"Transport":0}]', '0001-01-01 00:00:00+00', '0001-01-01 00:00:00+00', 0);

insert into tours."Tours"
	("Name", "Description", "Tags", "Level", "Status",
	"Price", "AuthorId", "Length", "TransportDurations", "PublishedTime", "ArchivedTime", "NumberOfViews")
values
	('Faks tura', 'Jej tura', '#bolja', 1, 1,
	'{"Amount":2500,"Currency":0}', 2, 10, '[{"Duration": 5,"Transport":1}]', '2023-01-01 00:00:00+00', '0001-01-01 00:00:00+00', 15);

insert into tours."Tours"
	("Name", "Description", "Tags", "Level", "Status",
	"Price", "AuthorId", "Length", "TransportDurations", "PublishedTime", "ArchivedTime", "NumberOfViews")
values
	('Futoska tura', 'Jos jedna tura', '#zeleno', 1, 1,
	'{"Amount":2500,"Currency":0}', 2, 10, '[{"Duration": 5,"Transport":1}]', '2023-01-01 00:00:00+00', '0001-01-01 00:00:00+00', 105);

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId", "Secret", "SecretImage")
values
	(45.24611507027287, 19.841764521045805, 'Tamo Daleko', 'Ne ididte u ovu kafanu', '', 1, 'Jednom kad udjes, ovde nema izlaska', '');

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId", "Secret", "SecretImage")
values
	(45.24521507027287, 19.831864521045805, 'Becarac', 'U ovu pogotovo', '', 1, 'Tajna su tamburasi petkom', '');

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId", "Secret", "SecretImage")
values
	(45.24641507027287, 19.856964521045805, 'Danka', 'U ovu nikako', '', 1, 'Danka je dobila naziv po istoimenoj ljubavi vlasnika kafane', '');

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId", "Secret", "SecretImage")
values
	(45.24511507027287, 19.851664521045805, 'FTN', 'Ne idite na ovaj faks', '', 2, 'Sta reci o FTN-u a  ne zaplakati...', '');

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId", "Secret", "SecretImage")
values
	(45.2455529844001, 19.842937968777537, 'Promenada', 'Kul soping', '', 2, 'Od Univera do Waltera, ima svega', '');

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId", "Secret", "SecretImage")
values
	(45.24960921800177, 19.828012847247944, 'Futoski park', 'Park', '', 3, 'Podignut u 20. veku, izgradjen za namene Jodne banje', '');

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId", "Secret", "SecretImage")
values
	(45.2526779593883, 19.83754884391624, 'Futoska pijaca', 'Pijaca', '', 3, 'Prostire se na 5000 kv. metara i ima 379 prodajnih mesta', '');