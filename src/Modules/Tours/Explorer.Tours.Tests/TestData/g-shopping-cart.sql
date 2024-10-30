INSERT INTO tours."ShoppingCarts"(
	"Id", "TotalPrice", "TouristId")
	VALUES (-1, jsonb_build_object('Amount', 20, 'Currency', 0) , -21);

INSERT INTO tours."OrderItems"(
	"Id", "TourId", "TourName", "Price", "ShoppingCartId")
	VALUES (-1, -1, 'Tura1', jsonb_build_object('Amount', 20, 'Currency', 0), -1);