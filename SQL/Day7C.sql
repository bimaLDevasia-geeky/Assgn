-- Library Management Schema

CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY,
    AuthorName VARCHAR(100),
    BirthYear INT
);

CREATE TABLE Books (
    BookID INT PRIMARY KEY,
    Title VARCHAR(200),
    AuthorID INT,
    PublicationYear INT,
    FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID)
);

CREATE TABLE Patrons (
    PatronID INT PRIMARY KEY,
    PatronName VARCHAR(100),
    MembershipDate DATE
);

CREATE TABLE Loans (
    LoanID INT PRIMARY KEY,
    BookID INT,
    PatronID INT,
    LoanDate DATE,
    ReturnDate DATE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID),
    FOREIGN KEY (PatronID) REFERENCES Patrons(PatronID)
);





-- Questions

	-- 1. List all books along with their authors, including books without assigned authors.
	select B.BookID,B.Title,A.AuthorName
	from 
		Books B
	left join Authors A on B.AuthorID = A.AuthorID;

	-- 2. Display all patrons and their loan history, including patrons who have never borrowed a book.
	
		Select P.PatronID,P.PatronName,L.LoanID,L.LoanDate,L.BookID
		from Patrons P
		left join Loans L on L.PatronID =P.PatronID;
	

-- 3. Show all authors and the books they've written, including authors who haven't written any books in our collection.

	select A.AuthorID,A.AuthorName,B.Title
	from Authors A 
	left join Books B on B.AuthorID = A.AuthorID;

-- 4. List all possible book-patron combinations, regardless of whether a loan has occurred.

	select * from Books cross join Patrons ;

-- 5. Display all loans with book and patron information, including loans where either the book or patron information is missing.

		select L.LoanID,L.LoanDate,B.Title,P.PatronName
		from
		Loans L
		left join Patrons P on L.PatronID=P.PatronID
		left join Books B on L.BookID = B.BookID;




-- 6. Show all books that have never been loaned, along with their author information.
SELECT
    B.Title,
    A.AuthorName
FROM
    Books B
LEFT JOIN
    Authors A ON B.AuthorID = A.AuthorID
LEFT JOIN
    Loans L ON B.BookID = L.BookID
WHERE
    L.LoanID IS NULL;

-- 7. List all patrons who have borrowed books in the last month, along with the books they've borrowed.

SELECT
    P.PatronName,
    B.Title AS BookBorrowed,
    L.LoanDate
FROM
    Patrons P
INNER JOIN
    Loans L ON P.PatronID = L.PatronID
INNER JOIN
    Books B ON L.BookID = B.BookID
WHERE
    L.LoanDate >= DATEADD(day, -30, GETDATE());

-- 8. Display all authors born after 1970 and their books, including those without any books in our collection.
		SELECT
			A.AuthorName,
			A.BirthYear,
			B.Title
		FROM
			Authors A
		LEFT JOIN

			Books B ON A.AuthorID = B.AuthorID
		WHERe
			A.BirthYear > 1900;

-- 9. Show all books published before 2000 and any associated loan information.

		SELECT
		B.Title,
		B.PublicationYear,
		L.LoanDate,
		P.PatronName
		FROM
		Books B
		LEFT JOIN
		Loans L ON B.BookID = L.BookID
		LEFT JOIN
		Patrons P ON L.PatronID = P.PatronID
		WHERE
	    B.PublicationYear < 2000;

-- 10. List all possible author-patron combinations, regardless of whether the patron has borrowed a book by that author.
		SELECT
			A.AuthorName,
			P.PatronName
		FROM
			Authors A
		CROSS JOIN
			Patrons P
		ORDER BY
			A.AuthorName, P.PatronName;