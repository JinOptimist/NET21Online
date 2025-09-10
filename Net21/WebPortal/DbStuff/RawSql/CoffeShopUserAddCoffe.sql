 SELECT 
    U.UserName AS AuthorName,
    CF.Name AS CoffeeName,
    CF.Cell AS Price
 FROM CoffeeProducts CF
    LEFT JOIN Users U ON U.Id = CF.AuthorId
 ORDER BY U.UserName, CF.Name;

SELECT 
    U.UserName AS AuthorName,
    COUNT(*) AS TotalCoffees
FROM CoffeeProducts CF
    LEFT JOIN Users U ON U.Id = CF.AuthorId
GROUP BY U.UserName
ORDER BY U.UserName;