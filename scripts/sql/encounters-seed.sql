DELETE FROM encounters."EncountersExecution";
DELETE FROM encounters."SocialEncounters";
DELETE FROM encounters."HiddenLocationEncounters";
DELETE FROM encounters."Encounters";
ALTER SEQUENCE encounters."Encounters_Id_seq" RESTART WITH 1;
DELETE FROM encounters."Participants";
ALTER SEQUENCE encounters."Participants_Id_seq" RESTART WITH 1;

insert into encounters."Encounters"
	("Name", "Description", "Location", "XP", "Status", "Type", "CreatorId")
values
	('Friendship place', 'This is the place to find new friends', '{"Latitude": 45.250791539582714, "Longitude": 19.82752553336623}', 100, 1, 1, 1);

insert into encounters."Encounters"
	("Name", "Description", "Location", "XP", "Status", "Type", "CreatorId")
values
	('Open gym', 'Complete 200 push-ups', '{"Latitude": 45.2484176507088, "Longitude": 19.816437156969418}', 50, 1, 0, 1);

insert into encounters."Encounters"
	("Name", "Description", "Location", "XP", "Status", "Type", "CreatorId")
values
	('Sremski Karlovci most famous building', 'Find the place from which this photo was taken.', '{"Latitude": 45.207545, "Longitude": 19.930066}', 50, 1, 2, 1);

insert into encounters."SocialEncounters"
	("Id", "Radius", "PeopleCount", "UserIds")
values
	(1, 15, 2, '[]');

insert into encounters."HiddenLocationEncounters"
	("Id", "ImageLocation", "Image")
values
	(3, '{"Latitude": 45.202264, "Longitude": 19.934194}', '');