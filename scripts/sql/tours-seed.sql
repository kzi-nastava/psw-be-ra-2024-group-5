DELETE FROM tours."TouristEquipment";
DELETE FROM tours."TourEquipment";
DELETE FROM tours."Equipment";
DELETE FROM tours."KeyPoint";
DELETE FROM tours."Facilities";
DELETE FROM tours."TourReviews";
DELETE FROM tours."Preferences";
DELETE FROM tours."Tours";

insert into tours."Tours"
	("Name", "Description", "Tags", "Level", "Status",
	"Price", "AuthorId", "Length", "TransportDurations", "PublishedTime", "ArchivedTime")
values
	('Draft tura', 'Kul je ova tura', '#najbolje', 0, 0,
	'{"Amount":1000,"Currency":0}', 2, 0, '[{"Duration": 10,"Transport":0}]', '0001-01-01 00:00:00+00', '0001-01-01 00:00:00+00');

insert into tours."Tours"
	("Name", "Description", "Tags", "Level", "Status",
	"Price", "AuthorId", "Length", "TransportDurations", "PublishedTime", "ArchivedTime")
values
	('Faks tura', 'Jej tura', '#bolja', 1, 1,
	'{"Amount":2500,"Currency":0}', 2, 10, '[{"Duration": 5,"Transport":1}]', '2023-01-01 00:00:00+00', '0001-01-01 00:00:00+00');

insert into tours."Tours"
	("Name", "Description", "Tags", "Level", "Status",
	"Price", "AuthorId", "Length", "TransportDurations", "PublishedTime", "ArchivedTime")
values
	('Futoska tura', 'Jos jedna tura', '#zeleno', 1, 1,
	'{"Amount":2500,"Currency":0}', 2, 10, '[{"Duration": 5,"Transport":1}]', '2023-01-01 00:00:00+00', '0001-01-01 00:00:00+00');

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId")
values
	(45.24611507027287, 19.851664521045805, 'FTN', 'Ne idite na ovaj faks', '', 2);

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId")
values
	(45.2455529844001, 19.842937968777537, 'Promenada', 'Kul soping', '', 2);

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId")
values
	(45.24960921800177, 19.828012847247944, 'Futoski park', 'Park', '', 3);

insert into tours."KeyPoint"
	("Latitude", "Longitude", "Name", "Description", "Image", "TourId")
values
	(45.2526779593883, 19.83754884391624, 'Futoska pijaca', 'Pijaca', '', 3);