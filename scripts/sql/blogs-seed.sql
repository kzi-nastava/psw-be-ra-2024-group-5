DELETE FROM blog."Blogs";
DELETE FROM blog."BlogComments";

insert into blog."Blogs"
	("UserId", "Title", "Description", "CreatedDate", "Status", "Votes", "Images")
values
	(1, 'Draft blog', 'Tekst draft bloga', '2023-09-01 00:00:00+00', 0, '[]', '[]');

insert into blog."Blogs"
	("UserId", "Title", "Description", "CreatedDate", "Status", "Votes", "Images")
values
	(1, 'Test blog', 'Neki lep opis bloga', '2023-10-01 00:00:00+00', 1, '[]', '[]');
