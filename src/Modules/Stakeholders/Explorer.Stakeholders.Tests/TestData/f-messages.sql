INSERT INTO stakeholders."ProfileMessages"(
	"Id", "RecipientId", "SenderId", "SentAt", "Content", "Attachment", "IsRead")
VALUES 
	(-3, -12, -11, '2024-08-22 09:15:00+02', 'Test Message 1', json_build_object('ResourceId',-2,'ResourceType',1), False),
	(-4, -12, -11, '2024-08-22 09:15:00+02', 'Test Message 2', json_build_object('ResourceId',-5,'ResourceType',0), False);