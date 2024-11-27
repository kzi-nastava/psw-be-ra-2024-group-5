DELETE FROM stakeholders."People";
DELETE FROM stakeholders."Followers";
DELETE FROM stakeholders."Users";
DELETE FROM stakeholders."Profiles";
DELETE FROM stakeholders."Memberships";
DELETE FROM stakeholders."Clubs";
DELETE FROM stakeholders."AppRating";

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
INSERT INTO stakeholders."Profiles"("Id", "UserId", "ProfileImage", "Biography", "Motto") VALUES 
	(1, 1, '', 'Admin Bio', 'Admin Motto'),
	(2, 2, '', 'Autor Bio', 'Autor Motto'),
	(3, 3, '', 'Turista Bio', 'Turista Motto'),
	(4, 4, '', 'Perina Biografija', 'Perin Motto'),
	(5, 5, '', 'Djurina Biografija', 'Djurin Motto');

INSERT INTO stakeholders."Clubs"("Name", "Description", "ImageDirectory", "OwnerId") VALUES ('Perin klub', 'opis', 'slika', 3);

INSERT INTO stakeholders."Memberships"("ClubId", "UserId") VALUES (1, 4);