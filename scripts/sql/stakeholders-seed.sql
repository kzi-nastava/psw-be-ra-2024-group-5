DELETE FROM stakeholders."People";
ALTER SEQUENCE stakeholders."People_Id_seq" RESTART WITH 1;
DELETE FROM stakeholders."Followers";
DELETE FROM stakeholders."Profiles";
ALTER SEQUENCE stakeholders."Profiles_Id_seq" RESTART WITH 1;
DELETE FROM stakeholders."Memberships";
DELETE FROM stakeholders."ClubMessages";
ALTER SEQUENCE stakeholders."ClubMessages_Id_seq" RESTART WITH 1;
DELETE FROM stakeholders."Clubs";
ALTER SEQUENCE stakeholders."Clubs_Id_seq" RESTART WITH 1;
DELETE FROM stakeholders."AppRating";
ALTER SEQUENCE stakeholders."AppRating_Id_seq" RESTART WITH 1;
DELETE FROM stakeholders."Users";
ALTER SEQUENCE stakeholders."Users_Id_seq" RESTART WITH 1;
DELETE FROM stakeholders."Followers";
ALTER SEQUENCE stakeholders."Followers_Id_seq" RESTART WITH 1;
DELETE FROM stakeholders."NotificationReadStatuses";
DELETE FROM stakeholders."Notifications";
ALTER SEQUENCE stakeholders."Notifications_Id_seq" RESTART WITH 1;
DELETE FROM stakeholders."ProfileMessages";
ALTER SEQUENCE stakeholders."ProfileMessages_Id_seq" RESTART WITH 1;

-- Users
insert into stakeholders."Users"("Username", "Password", "Role", "IsActive") values ('admin', 'admin', 0, true);
insert into stakeholders."Users"("Username", "Password", "Role", "IsActive") values ('author', 'author', 1, true);
INSERT INTO stakeholders."Users"("Username", "Password", "Role", "IsActive") VALUES ('pera', 'pera', 2, true);
INSERT INTO stakeholders."Users"("Username", "Password", "Role", "IsActive") VALUES ('djura', 'djura', 2, true);
INSERT INTO stakeholders."Users"("Username", "Password", "Role", "IsActive") VALUES ('zika', 'zika', 2, true);

-- People
insert into stakeholders."People"("UserId", "Name", "Surname", "Email") values (1, 'Admin', 'Adminovic', 'admin@gmail.com');
insert into stakeholders."People"("UserId", "Name", "Surname", "Email") values (2, 'Author', 'Authorovic', 'author@gmail.com');
INSERT INTO stakeholders."People"("UserId", "Name", "Surname", "Email") VALUES (3, 'Pera', 'Peric', 'pera@gmail.com');
INSERT INTO stakeholders."People"("UserId", "Name", "Surname", "Email") VALUES (4, 'Djura', 'Djuric', 'djura@gmail.com');
INSERT INTO stakeholders."People"("UserId", "Name", "Surname", "Email") VALUES (5, 'Zika', 'Zikic', 'zika@gmail.com');

-- Profiles
INSERT INTO stakeholders."Profiles"("UserId", "ProfileImage", "Biography", "Motto") VALUES
	(1, '', 'Admin Bio', 'Admin Motto'),
	(2, '', 'Autor Bio', 'Autor Motto'),
	(3, '', 'Turista Bio', 'Turista Motto'),
	(4, '', 'Perina Biografija', 'Perin Motto'),
	(5, '', 'Djurina Biografija', 'Djurin Motto');

INSERT INTO stakeholders."Clubs"("Name", "Description", "ImageDirectory", "OwnerId") VALUES ('Perin klub', 'opis', 'slika', 3);

INSERT INTO stakeholders."Memberships"("ClubId", "UserId") VALUES (1, 4);