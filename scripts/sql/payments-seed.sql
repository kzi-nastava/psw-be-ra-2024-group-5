DELETE FROM payments."OrderItems";
DELETE FROM payments."ShoppingCarts";

insert into payments."ShoppingCarts"
	("TotalPrice", "TouristId")
values
	('{"Amount":0,"Currency":0}', 3);

insert into payments."ShoppingCarts"
	("TotalPrice", "TouristId")
values
	('{"Amount":0,"Currency":0}', 4);

insert into payments."ShoppingCarts"
	("TotalPrice", "TouristId")
values
	('{"Amount":0,"Currency":0}', 5);