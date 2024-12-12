INSERT INTO tours."Tours" ("Id", "Name", "Description", "Tags", "Level", "Status", "Price", "AuthorId", "Length", "TransportDurations", "PublishedTime", "ArchivedTime", "NumberOfViews")
VALUES 
    (-1, 'Tura1', 'Opis prve ture', 'tag1, tag2', 0, 1, 
        jsonb_build_object('Amount', 20, 'Currency', 0), 
        -11, 
        150.21, 
        jsonb_build_array(
            jsonb_build_object('Transport', 1, 'Duration', 50.5)
        ), 
        '2024-08-22 09:15:00+02', 
        '2024-12-22 09:15:00+02', 10),

    (-2, 'Tura2', 'Opis druge ture', 'tag1', 1, 1, 
        jsonb_build_object('Amount', 400, 'Currency', 1), 
        -12, 
        1321.21, 
        jsonb_build_array(
            jsonb_build_object('Transport', 1, 'Duration', 230.5)
        ), 
        '2024-06-12 11:22:00+02', 
        '2024-12-23 08:32:00+02', 10),

    (-3, 'Tura3', 'Opis treće ture', 'tag1, tag2, tag3', 0, 0,
        jsonb_build_object('Amount', 0, 'Currency', 0), 
        -12, 
        32.321,
        jsonb_build_array(
            jsonb_build_object('Duration', 50.5, 'Transport', 1),
            jsonb_build_object('Duration', 70.0, 'Transport', 2)
        ), 
        '2023-02-12 08:21:00+02', 
        '2024-03-23 08:32:00+02', 10);
