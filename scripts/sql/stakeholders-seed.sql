DELETE FROM stakeholders."People";
DELETE FROM stakeholders."Followers";
DELETE FROM stakeholders."Users";
DELETE FROM stakeholders."Profiles";
DELETE FROM stakeholders."Memberships";
DELETE FROM stakeholders."Clubs";
DELETE FROM stakeholders."AppRating";

insert into stakeholders."Users" ("Id", "Username", "Password", "Role", "IsActive") values (1, 'admin', 'admin', 0, true);
insert into stakeholders."Users" ("Id", "Username", "Password", "Role", "IsActive") values (2, 'author', 'author', 1, true);
insert into stakeholders."Users" ("Id", "Username", "Password", "Role", "IsActive") values (3, 'tourist', 'tourist', 2, true);
