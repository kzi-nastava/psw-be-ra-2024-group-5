DELETE FROM payments."Coupons";
ALTER SEQUENCE payments."Coupons_Id_seq" RESTART WITH 1;
DELETE FROM payments."Bundles";
ALTER SEQUENCE payments."Bundles_Id_seq" RESTART WITH 1;
DELETE FROM payments."Wallets";
ALTER SEQUENCE payments."Wallets_Id_seq" RESTART WITH 1;
DELETE FROM payments."OrderItems";
ALTER SEQUENCE payments."OrderItems_Id_seq" RESTART WITH 1;
DELETE FROM payments."ShoppingCarts";
ALTER SEQUENCE payments."ShoppingCarts_Id_seq" RESTART WITH 1;
DELETE FROM payments."TourPurchaseTokens";
ALTER SEQUENCE payments."TourPurchaseTokens_Id_seq" RESTART WITH 1;

insert into payments."Wallets"
	("Balance", "TouristId")
values
	('{"Amount":5000,"Currency":0}', 3);

insert into payments."Wallets"
	("Balance", "TouristId")
values
	('{"Amount":5000,"Currency":0}', 4);

insert into payments."Wallets"
	("Balance", "TouristId")
values
	('{"Amount":5000,"Currency":0}', 5);

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