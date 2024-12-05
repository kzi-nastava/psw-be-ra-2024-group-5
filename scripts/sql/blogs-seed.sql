DELETE FROM blog."Blogs";
ALTER SEQUENCE blog."Blogs_Id_seq" RESTART WITH 1;
DELETE FROM blog."BlogComments";
ALTER SEQUENCE blog."BlogComments_Id_seq" RESTART WITH 1;

insert into blog."Blogs"
	("UserId", "Title", "Description", "CreatedDate", "Status", "Votes", "Images")
values
	(1, 'Draft blog', 'Tekst draft bloga', '2023-09-01 00:00:00+00', 0, '[]', '[]');

insert into blog."Blogs"
	("UserId", "Title", "Description", "CreatedDate", "Status", "Votes", "Images")
values
	(1, 'Test blog', 'Neki lep opis bloga', '2023-10-01 00:00:00+00', 1, '[]', '[]');
