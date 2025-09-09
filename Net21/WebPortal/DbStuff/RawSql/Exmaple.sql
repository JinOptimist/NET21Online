-- Read
SELECT *
FROM Users

-- Create
INSERT INTO Users
	(UserName, Password, Money, Role, Language, AvatarUrl)
VALUES 
	('Olga', 'Olg4', 99, 2, 1, '/avatar/default.jpg')

-- Update
UPDATE Users
SET Money = 50
WHERE Id = 2 -- UserName = 'Lera'

-- Delete
DELETE Users
WHERE Id = 3