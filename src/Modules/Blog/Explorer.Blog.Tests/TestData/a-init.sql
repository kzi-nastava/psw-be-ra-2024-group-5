INSERT INTO stakeholders."Users" ("Id", "Username", "Password", "Role", "IsActive")
VALUES 
    (-1, 'admin@gmail.com', 'admin', 0, true),
    (-11, 'autor1@gmail.com', 'autor1', 1, true),
    (-12, 'autor2@gmail.com', 'autor2', 1, true),
    (-13, 'autor3@gmail.com', 'autor3', 1, true),
    (-21, 'turista1@gmail.com', 'turista1', 2, true),
    (-22, 'turista2@gmail.com', 'turista2', 2, true),
    (-23, 'turista3@gmail.com', 'turista3', 2, true);


INSERT INTO stakeholders."People" ("Id", "UserId", "Name", "Surname", "Email")
VALUES 
	(-1, -1, 'Admin', 'Adminić', 'admin@gmail.com'),
    (-11, -11, 'Ana', 'Anić', 'autor1@gmail.com'),
    (-12, -12, 'Lena', 'Lenić', 'autor2@gmail.com'),
    (-13, -13, 'Sara', 'Sarić', 'autor3@gmail.com'),
    (-21, -21, 'Pera', 'Perić', 'turista1@gmail.com'),
    (-22, -22, 'Mika', 'Mikić', 'turista2@gmail.com'),
    (-23, -23, 'Steva', 'Stević', 'turista3@gmail.com');


INSERT INTO stakeholders."Profiles" ("Id", "UserId", "ProfilePictureUrl", "Biography", "Motto")
VALUES 
    (-1, -1, '', 'Administrator of the system', 'Leading by example'),
    (-11, -11, '', 'First Author Biography', 'Creativity is key'),
    (-12, -12, '', 'Second Author Biography', 'Words matter'),
    (-13, -13, '', 'Third Author Biography', 'Inspiration everywhere'),
    (-21, -21, '', 'First Tourist Biography', 'Travel far, learn more'),
    (-22, -22, '', 'Second Tourist Biography', 'Adventure awaits'),
    (-23, -23, '', 'Third Tourist Biography', 'Journey of a lifetime');

INSERT INTO blog."blogs" ("Id", "userId", title, description, "createdDate", status, votes, images)
VALUES 
    (-1, -11, 'Exploring the Art of Writing', 'A deep dive into the nuances of creative writing.', '2024-11-07', 1, '[]', '[]'),
    (-2, -12, 'The Wonders of Language', 'Discovering how language shapes our world.', '2024-11-07', 1, '[]', '[]'),
    (-3, -13, 'Inspiration in Daily Life', 'Finding motivation and beauty in everyday moments.', '2024-11-07', 1, '[]', '[]'),
    (-4, -11, 'A Journey through Europe', 'Experiencing culture, history, and adventure across Europe.', '2024-11-07', 1, '[]', '[]');
