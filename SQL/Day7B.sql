-- E-commerce Platform Schema

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    Email VARCHAR(100),
    RegistrationDate DATE
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(200),
    Price DECIMAL(10, 2),
    CategoryID INT
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY,
    CategoryName VARCHAR(100)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    TotalAmount DECIMAL(10, 2),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);




-- Questions

-- 1. List all products with their category names, including products without a category.
	select P.ProductName,P.ProductID,C.CategoryID,C.CategoryName
	from Products P
	left join Categories C ON C.CategoryID=P.CategoryID;
-- 2. Display all customers and their order history, including customers who haven't placed any orders.

	select C.CustomerID,C.CustomerName,O.OrderDate,O.TotalAmount
	from Customers C
	left join Orders O on C.CustomerID = O.CustomerID;
-- 3. Show all categories and the products in each category, including categories without any products.

	select C.CategoryID,C.CategoryName,P.ProductID,P.ProductName
	from Categories C
	left join Products P on P.CategoryID=C.CategoryID;


-- 4. List all possible customer-product combinations, regardless of whether a purchase has occurred.

	select * 
	from Customers 
	cross join Products;

-- 5. Display all orders with customer and product information, including orders where either the customer or product information is missing.

	select *
	from Orders O
	left join Customers C on C.CustomerID = O.CustomerID
	left join OrderDetails OD on OD.OrderID = O.OrderID
	left join Products P on P.ProductID=OD.ProductID 
	order by O.OrderId;



-- 6. Show all products that have never been ordered, along with their category information.

	select P.ProductID,P.ProductName,C.CategoryName
	from Products P
	left join Categories C on C.CategoryID = P.CategoryID
	where not exists (select 1 from OrderDetails O where O.ProductID =P.ProductID  );

-- 7. List all customers who have placed orders in the last week, along with the products they've purchased.

	SELECT
    C.CustomerName,
    P.ProductName,
    O.OrderDate,
    OD.Quantity
	FROM
    Customers C
	INNER JOIN
    Orders O ON C.CustomerID = O.CustomerID
	INNER JOIN
    OrderDetails OD ON O.OrderID = OD.OrderID
	INNER JOIN
    Products P ON OD.ProductID = P.ProductID
	WHERE
    O.OrderDate BETWEEN DATEADD(day, -7, CAST(GETDATE() AS DATE)) AND CAST(GETDATE() AS DATE)
	ORDER BY
    C.CustomerName, O.OrderDate;

-- 8. Display all categories with products priced over $100, including categories without such products.
	select C.CategoryID,C.CategoryName,P.ProductName,P.Price
	from Categories C
	left join Products P on P.CategoryID=C.CategoryID and P.Price>100;

-- 9. Show all orders placed before 2023 and any associated product information.

	SELECT
    O.OrderID,
    O.OrderDate,
    P.ProductName,
    OD.Quantity
	FROM
    Orders O
	LEFT JOIN
    OrderDetails OD ON O.OrderID = OD.OrderID
	LEFT JOIN
    Products P ON OD.ProductID = P.ProductID
	WHERE
    YEAR(O.OrderDate) < 2023;

-- 10. List all possible category-customer combinations, regardless of whether the customer has purchased a product from that category.
		SELECT
			C.CustomerName,
			Cat.CategoryName
		FROM
			Customers C
		CROSS JOIN
			Categories Cat
		ORDER BY
			C.CustomerName, Cat.CategoryName;