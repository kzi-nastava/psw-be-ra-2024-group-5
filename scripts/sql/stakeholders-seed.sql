DELETE FROM stakeholders."People";
DELETE FROM stakeholders."Followers";
DELETE FROM stakeholders."Users";
DELETE FROM stakeholders."Profiles";
DELETE FROM stakeholders."Memberships";
DELETE FROM stakeholders."Clubs";
DELETE FROM stakeholders."AppRating";

insert into stakeholders."Users" ("Username", "Password", "Role", "IsActive") values ('admin', 'admin', 0, true);
insert into stakeholders."Users" ("Username", "Password", "Role", "IsActive") values ('author', 'author', 1, true);
insert into stakeholders."Users" ("Username", "Password", "Role", "IsActive") values ('tourist', 'tourist', 2, true);
