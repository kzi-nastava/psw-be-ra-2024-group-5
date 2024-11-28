DELETE FROM encounters."EncountersExecution";
DELETE FROM encounters."SocialEncounters";
DELETE FROM encounters."HiddenLocationEncounters";
DELETE FROM encounters."Encounters";
DELETE FROM encounters."Participants";

insert into encounters."Encounters"
	("Name", "Description", "Location", "XP", "Status", "Type", "CreatorId")
values
	('Social', 'Social', '{"Latitude": 45.250791539582714, "Longitude": 19.82752553336623}', 100, 1, 1, 1);

insert into encounters."Encounters"
	("Name", "Description", "Location", "XP", "Status", "Type", "CreatorId")
values
	('Misc', 'Misc', '{"Latitude": 45.2484176507088, "Longitude": 19.816437156969418}', 50, 1, 0, 1);

insert into encounters."SocialEncounters"
	("Id", "Radius", "PeopleCount", "UserIds")
values
	(1, 15, 2, '[]');