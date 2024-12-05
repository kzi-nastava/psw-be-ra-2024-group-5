INSERT INTO payments."Bundles" ("Id", "Name", "Price", "AuthorId", "Status", "BundleItems")
VALUES 
    (-1, 'Adventure Bundle', jsonb_build_object('Amount', 99.99, 'Currency', 0), -11, 0, jsonb_build_array(-1, -2)),
    (-2, 'Relaxation Bundle', jsonb_build_object('Amount', 129.99, 'Currency', 0), -11, 1, jsonb_build_array(-2, -3)),
    (-3, 'Extreme Sports Bundle', jsonb_build_object('Amount', 199.99, 'Currency', 0), -12, 2, jsonb_build_array(-1, -3)),
    (-4, 'Family Fun Bundle', jsonb_build_object('Amount', 79.99, 'Currency', 0), -13, 0, jsonb_build_array(-1, -2, -3));