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


INSERT INTO stakeholders."Profiles" ("Id", "UserId", "ProfileImage", "Biography", "Motto")
VALUES 
    (-1, -1, '', 'Administrator of the system', 'Leading by example'),
    (-11, -11, '', 'First Author Biography', 'Creativity is key'),
    (-12, -12, '', 'Second Author Biography', 'Words matter'),
    (-13, -13, '', 'Third Author Biography', 'Inspiration everywhere'),
    (-21, -21, '', 'First Tourist Biography', 'Travel far, learn more'),
    (-22, -22, '', 'Second Tourist Biography', 'Adventure awaits'),
    (-23, -23, '', 'Third Tourist Biography', 'Journey of a lifetime');

INSERT INTO tours."Tours" ("Id", "Name", "Description", "Tags", "Level", "Status", "Price", "AuthorId", "Length", "TransportDurations", "PublishedTime", "ArchivedTime")
VALUES 
    (-1, 'Tura1', 'Opis prve ture', 'tag1, tag2', 0, 1, 
        jsonb_build_object('Amount', 20, 'Currency', 0), 
        -11, 
        150.21, 
        jsonb_build_array(
            jsonb_build_object('Transport', 1, 'Duration', 50.5)
        ), 
        '2024-08-22 09:15:00+02', 
        '2024-12-22 09:15:00+02'),

    (-2, 'Tura2', 'Opis druge ture', 'tag1', 1, 1, 
        jsonb_build_object('Amount', 400, 'Currency', 1), 
        -12, 
        1321.21, 
        jsonb_build_array(
            jsonb_build_object('Transport', 1, 'Duration', 230.5)
        ), 
        '2024-06-12 11:22:00+02', 
        '2024-12-23 08:32:00+02'),

    (-3, 'Tura3', 'Opis treće ture', 'tag1, tag2, tag3', 0, 0,
        jsonb_build_object('Amount', 0, 'Currency', 0), 
        -12, 
        32.321,
        jsonb_build_array(
            jsonb_build_object('Duration', 50.5, 'Transport', 1),
            jsonb_build_object('Duration', 70.0, 'Transport', 2)
        ), 
        '2023-02-12 08:21:00+02', 
        '2024-03-23 08:32:00+02');