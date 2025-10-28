Create Database  BookMyShow;

use BookMyShow;

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100),
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(15) NOT NULL UNIQUE,
    HashedPassword NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE()
);

insert into Users(FirstName,LastName,Email,PhoneNumber,HashedPassword) values('Bimal','Devasia','bimaldevasia@gmail.com','9934126790','sajfnsdhgibaf34323#43'),
																			('John', 'Doe', 'john.doe@example.com', '+14155550101', 'HASHED_pw_123'),
																			('Jane', 'Smith', 'jane.smith@example.com', '+14155550102', 'HASHED_pw_456'),
																			('Amit', 'Kumar', 'amit.kumar@example.com', '+919876543210', 'HASHED_pw_789'),
																			('Sara', 'Ali', 'sara.ali@example.com', '+441234567890', 'HASHED_pw_abc'),
																			('David', 'Lee', 'david.lee@example.com', '+61234567890', 'HASHED_pw_def');


CREATE TABLE Cities (
    CityID INT PRIMARY KEY IDENTITY(1,1),
    CityName NVARCHAR(100) NOT NULL,
    [State] NVARCHAR(100) NOT NULL,
    Country NVARCHAR(100) DEFAULT 'India'
);

insert into Cities (CityName,[State]) values('Mumbai', 'Maharashtra'),
										  ('Delhi', 'Delhi'),
										  ('Bengaluru', 'Karnataka'),
										  ('Chennai', 'Tamil Nadu'),
										  ('Kolkata', 'West Bengal'),
										  ('Hyderabad', 'Telangana'),
										  ('Pune', 'Maharashtra'),
										  ('Ahmedabad', 'Gujarat'),
										  ('Jaipur', 'Rajasthan'),
										  ('Kochi', 'Kerala');

select * from Cities;


CREATE TABLE Theaters (
    TheaterID INT PRIMARY KEY IDENTITY(1,1),
    TheaterName NVARCHAR(255) NOT NULL,
    [Address] NVARCHAR(MAX) NOT NULL,
    CityID INT NOT NULL,
    FOREIGN KEY (CityID) REFERENCES Cities(CityID)
);

insert into Theaters (TheaterName,[Address],CityID) values('PVR Cinemas - Phoenix Mall', 'Phoenix Market City, Kurla, Mumbai', 1),
('INOX - R City Mall', 'R City Mall, Ghatkopar, Mumbai', 1),
('Cinepolis - DLF Mall', 'DLF Mall of India, Noida, Delhi NCR', 2),
('INOX - Garuda Mall', 'Garuda Mall, Magrath Road, Bengaluru', 3),
('SPI Cinemas - Sathyam', 'Royapettah, Chennai', 4),
('INOX - Quest Mall', 'Quest Mall, Ballygunge, Kolkata', 5),
('PVR - Nexus Mall', 'Kukatpally, Hyderabad', 6),
('E-Square', 'University Road, Pune', 7),
('Cinepolis - AlphaOne Mall', 'Vastrapur, Ahmedabad', 8),
('INOX - Pink Square Mall', 'Govind Marg, Jaipur', 9);

select * from Theaters;


CREATE TABLE Screens (
    ScreenID INT PRIMARY KEY IDENTITY(1,1),
    TheaterID INT NOT NULL,
    ScreenName NVARCHAR(50) NOT NULL, 
    TotalSeats INT NOT NULL,
    FOREIGN KEY (TheaterID) REFERENCES Theaters(TheaterID)
);

insert into Screens (TheaterID,ScreenName,TotalSeats) values(1, 'Kairali', 180),
(1, 'Nila', 150),
(2, 'Sree Padmanabha', 200),
(3, 'Surabhi', 160),
(4, 'Navarang', 140),
(5, 'Chithram Hall', 190),
(6, 'Sree Screen', 170),
(7, 'Tharang', 220),
(8, 'Sangeeth', 130),
(9, 'New Galaxy', 150),
(10, 'Kairali Deluxe', 100);


select * from Screens;


CREATE TABLE Movies (
    MovieID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX),
    ReleaseDate DATE,
    DurationMinutes INT,
    PosterURL NVARCHAR(512),
    CensorRating NVARCHAR(10) 
);

INSERT INTO Movies (Title, Description, ReleaseDate, DurationMinutes, PosterURL, CensorRating)
VALUES
('Aavesham', 'A college student befriends a local gangster, leading to chaos and comedy.', '2024-04-11', 155, 'https://example.com/posters/aavesham.jpg', 'U/A'),
('Manjummel Boys', 'A group of friends from Kochi go on a trip that turns into a daring rescue mission.', '2024-02-22', 135, 'https://example.com/posters/manjummel_boys.jpg', 'U/A'),
('Premalu', 'A lighthearted romantic comedy set in Hyderabad, following the love story of a Malayali youth.', '2024-02-09', 150, 'https://example.com/posters/premalu.jpg', 'U'),
('Aadujeevitham', 'Based on the true story of a Malayali worker stranded in the Saudi desert.', '2024-03-28', 172, 'https://example.com/posters/aadujeevitham.jpg', 'U/A'),
('The Goat Life', 'A man’s survival journey through hardship and faith.', '2024-03-28', 170, 'https://example.com/posters/goat_life.jpg', 'U/A'),
('King of Kotha', 'A gangster’s rise and fall in a crime-filled town.', '2023-08-24', 160, 'https://example.com/posters/king_of_kotha.jpg', 'A'),
('Bramayugam', 'A dark horror thriller blending folklore, power, and greed.', '2024-02-15', 140, 'https://example.com/posters/bramayugam.jpg', 'U/A'),
('RDX', 'Three friends take revenge after being humiliated by local goons.', '2023-08-25', 155, 'https://example.com/posters/rdx.jpg', 'U/A'),
('2018', 'Based on the Kerala floods of 2018, showing human resilience and unity.', '2023-05-05', 156, 'https://example.com/posters/2018.jpg', 'U'),
('Hridayam', 'A coming-of-age drama tracing a man’s emotional journey through love and life.', '2022-01-21', 172, 'https://example.com/posters/hridayam.jpg', 'U');


select * from Movies;

CREATE TABLE Genres (
    GenreID INT PRIMARY KEY IDENTITY(1,1),
    GenreName NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO Genres (GenreName)
VALUES
('Action'),
('Drama'),
('Comedy'),
('Romance'),
('Thriller'),
('Horror'),
('Adventure'),
('Family'),
('Historical'),
('Fantasy'),
('Science Fiction'),
('Biography'),
('Crime'),
('Mystery'),
('Musical');


select * from Genres ;


CREATE TABLE MovieGenres (
    MovieID INT NOT NULL,
    GenreID INT NOT NULL,
    PRIMARY KEY (MovieID, GenreID),
    FOREIGN KEY (MovieID) REFERENCES Movies(MovieID),
    FOREIGN KEY (GenreID) REFERENCES Genres(GenreID)
);

INSERT INTO MovieGenres (MovieID, GenreID) VALUES (1, 1), (1, 3);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (2, 5), (2, 2), (2, 7);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (3, 4), (3, 3);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (4, 2), (4, 12), (4, 7);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (5, 2), (5, 12);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (6, 1), (6, 13), (6, 2);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (7, 6), (7, 5);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (8, 1), (8, 2);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (9, 2), (9, 1), (9, 9);
INSERT INTO MovieGenres (MovieID, GenreID) VALUES (10, 4), (10, 2), (10, 15);


select M.Title,G.GenreName from Movies M join MovieGenres MG on M.MovieID=MG.MovieID join Genres G on G.GenreID = MG.GenreID;

CREATE TABLE Languages (
    LanguageID INT PRIMARY KEY IDENTITY(1,1),
    LanguageName NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO Languages (LanguageName)
VALUES
('Malayalam'),
('Tamil'),
('Telugu'),
('Hindi'),
('Kannada'),
('English'),
('Marathi'),
('Gujarati'),
('Bengali'),
('Punjabi');

select * from Languages;



CREATE TABLE MovieLanguages (
    MovieID INT NOT NULL,
    LanguageID INT NOT NULL,
    PRIMARY KEY (MovieID, LanguageID),
    FOREIGN KEY (MovieID) REFERENCES Movies(MovieID),
    FOREIGN KEY (LanguageID) REFERENCES Languages(LanguageID)
);


INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (1, 1);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (2, 1);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (3, 1), (3, 3);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (4, 1), (4, 4), (4, 2);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (5, 1), (5, 6);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (6, 1), (6, 2), (6, 3), (6, 4);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (7, 1);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (8, 1), (8, 2);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (9, 1), (9, 2), (9, 3), (9, 4);
INSERT INTO MovieLanguages (MovieID, LanguageID) VALUES (10, 1);


select * from MovieLanguages;



CREATE TABLE Shows (
    ShowID INT PRIMARY KEY IDENTITY(1,1),
    MovieID INT NOT NULL,
    ScreenID INT NOT NULL,
    ShowStartTime DATETIME2 NOT NULL,
    [Format] NVARCHAR(20) DEFAULT '2D', 
    FOREIGN KEY (MovieID) REFERENCES Movies(MovieID),
    FOREIGN KEY (ScreenID) REFERENCES Screens(ScreenID)
);

INSERT INTO Shows (MovieID, ScreenID, ShowStartTime, [Format])
VALUES
(1, 1, '2025-10-28 10:00:00', '2D'),
(1, 2, '2025-10-28 14:00:00', '2D'),
(2, 3, '2025-10-28 11:00:00', '2D'),
(2, 4, '2025-10-28 18:30:00', '2D'),
(3, 5, '2025-10-28 09:30:00', '2D'),
(3, 6, '2025-10-28 16:00:00', '2D'),
(4, 7, '2025-10-28 13:00:00', 'IMAX 3D'),
(4, 7, '2025-10-28 19:30:00', 'IMAX 3D'),
(5, 8, '2025-10-28 10:30:00', '2D'),
(5, 8, '2025-10-28 15:45:00', '2D'),
(6, 9, '2025-10-28 12:00:00', '2D'),
(6, 10, '2025-10-28 20:00:00', '2D'),
(7, 1, '2025-10-28 21:15:00', '2D'),
(8, 2, '2025-10-28 17:30:00', '2D'),
(9, 3, '2025-10-28 09:45:00', '2D'),
(9, 4, '2025-10-28 18:00:00', '2D'),
(10, 5, '2025-10-28 13:15:00', '2D'),
(10, 6, '2025-10-28 19:00:00', '2D');


select * from Shows;

CREATE TABLE SeatCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50) NOT NULL UNIQUE 
)

INSERT INTO SeatCategories (CategoryName)
VALUES
('Silver'),
('Gold'),
('Platinum'),
('Recliner'),
('Balcony');

select * from SeatCategories;



CREATE TABLE Seats (
    SeatID INT PRIMARY KEY IDENTITY(1,1),
    ScreenID INT NOT NULL,
    RowNum NVARCHAR(3) NOT NULL,
    SeatNum INT NOT NULL,
    CategoryID INT NOT NULL,
    CONSTRAINT UQ_Screen_Seat UNIQUE(ScreenID, RowNum, SeatNum),
    FOREIGN KEY (ScreenID) REFERENCES Screens(ScreenID),
    FOREIGN KEY (CategoryID) REFERENCES SeatCategories(CategoryID)
);

INSERT INTO Seats (ScreenID, RowNum, SeatNum, CategoryID)
VALUES
-- Silver (Rows A–C)
(1, 'A', 1, 1), (1, 'A', 2, 1), (1, 'A', 3, 1), (1, 'A', 4, 1), (1, 'A', 5, 1),
(1, 'B', 1, 1), (1, 'B', 2, 1), (1, 'B', 3, 1), (1, 'B', 4, 1), (1, 'B', 5, 1),
(1, 'C', 1, 1), (1, 'C', 2, 1), (1, 'C', 3, 1), (1, 'C', 4, 1), (1, 'C', 5, 1),

-- Gold (Rows D–E)
(1, 'D', 1, 2), (1, 'D', 2, 2), (1, 'D', 3, 2), (1, 'D', 4, 2), (1, 'D', 5, 2),
(1, 'E', 1, 2), (1, 'E', 2, 2), (1, 'E', 3, 2), (1, 'E', 4, 2), (1, 'E', 5, 2);


--Screen 2 (Nila) - Platinum and Recliner categories
INSERT INTO Seats (ScreenID, RowNum, SeatNum, CategoryID)
VALUES
-- Platinum (Rows A–B)
(2, 'A', 1, 3), (2, 'A', 2, 3), (2, 'A', 3, 3), (2, 'A', 4, 3),
(2, 'B', 1, 3), (2, 'B', 2, 3), (2, 'B', 3, 3), (2, 'B', 4, 3),

-- Recliner (Row C)
(2, 'C', 1, 4), (2, 'C', 2, 4), (2, 'C', 3, 4);


select * from seats;

CREATE TABLE ShowPrices (
    ShowPriceID INT PRIMARY KEY IDENTITY(1,1),
    ShowID INT NOT NULL,
    CategoryID INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    CONSTRAINT UQ_Show_Category_Price UNIQUE(ShowID, CategoryID),
    FOREIGN KEY (ShowID) REFERENCES Shows(ShowID),
    FOREIGN KEY (CategoryID) REFERENCES SeatCategories(CategoryID)
);


INSERT INTO ShowPrices (ShowID, CategoryID, Price)
VALUES
(1, 1, 150.00),
(1, 2, 200.00),
(1, 4, 300.00),
(2, 1, 160.00),
(2, 2, 220.00);


INSERT INTO ShowPrices (ShowID, CategoryID, Price)
VALUES
(3, 1, 180.00),
(3, 2, 250.00),
(3, 4, 320.00),
(4, 1, 170.00),
(4, 2, 240.00);


INSERT INTO ShowPrices (ShowID, CategoryID, Price)
VALUES
(5, 1, 140.00),
(5, 2, 190.00),
(5, 3, 250.00),
(6, 1, 150.00),
(6, 2, 200.00);


INSERT INTO ShowPrices (ShowID, CategoryID, Price)
VALUES
(7, 2, 300.00),
(7, 3, 400.00),
(7, 4, 500.00),
(8, 2, 320.00),
(8, 3, 420.00);


INSERT INTO ShowPrices (ShowID, CategoryID, Price)
VALUES
(9, 1, 180.00),
(9, 2, 250.00),
(10, 1, 190.00),
(10, 2, 260.00);


INSERT INTO ShowPrices (ShowID, CategoryID, Price)
VALUES
(11, 1, 160.00),
(11, 2, 220.00),
(11, 4, 300.00),
(12, 1, 180.00),
(12, 2, 240.00);

INSERT INTO ShowPrices (ShowID, CategoryID, Price)
VALUES
(13, 1, 170.00),
(13, 2, 230.00),
(14, 1, 150.00),
(14, 2, 200.00);

CREATE TABLE Offers (
    OfferID INT PRIMARY KEY IDENTITY(1,1),
    OfferCode NVARCHAR(50) NOT NULL UNIQUE,
    [Description] NVARCHAR(MAX),
    DiscountPercentage DECIMAL(5, 2),
    MaxDiscount DECIMAL(10, 2),
    ValidUntil DATE
);

INSERT INTO Offers (OfferCode, Description, DiscountPercentage, MaxDiscount, ValidUntil)
VALUES
('FIRST50', 'Get 50% off on your first booking (up to ₹150).', 50.00, 150.00, '2025-12-31'),
('MOVIE20', 'Flat 20% off on all movie tickets (max ₹100).', 20.00, 100.00, '2025-11-30'),
('WEEKEND30', 'Enjoy 30% off on weekend shows (max ₹200).', 30.00, 200.00, '2026-01-31'),
('FESTIVE10', '10% off during festival weeks.', 10.00, 75.00, '2025-12-25'),
('RECLINER25', '25% discount on recliner seats (max ₹250).', 25.00, 250.00, '2026-03-31');

select * from Offers;

CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    ShowID INT NOT NULL,
    BookingTime DATETIME2 DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    OfferID INT NULL, 
    BookingStatus NVARCHAR(20) NOT NULL,
    CONSTRAINT CHK_BookingStatus CHECK (BookingStatus IN ('Pending', 'Confirmed', 'Cancelled')),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ShowID) REFERENCES Shows(ShowID),
    FOREIGN KEY (OfferID) REFERENCES Offers(OfferID)
);

INSERT INTO Bookings (UserID, ShowID, TotalAmount, OfferID, BookingStatus)
VALUES
(5, 2, 275.00, NULL, 'Confirmed'),
(1, 5, 600.00, 3, 'Confirmed'),
(2, 6, 340.00, NULL, 'Cancelled'),
(3, 7, 410.00, NULL, 'Confirmed'),
(4, 8, 500.00, 2, 'Pending'),
(5, 9, 620.00, 1, 'Confirmed'),
(1, 10, 450.00, NULL, 'Confirmed'),
(2, 11, 300.00, NULL, 'Pending'),
(3, 12, 700.00, 3, 'Confirmed'),
(4, 13, 280.00, NULL, 'Cancelled'),
(5, 14, 350.00, 2, 'Confirmed'),
(1, 15, 390.00, NULL, 'Confirmed'),
(2, 16, 520.00, NULL, 'Pending'),
(3, 17, 310.00, 1, 'Confirmed'),
(4, 18, 450.00, NULL, 'Confirmed');


select * from Bookings order by BookingID;

CREATE TABLE BookingSeats (
    BookingID INT NOT NULL,
    SeatID INT NOT NULL,
    PricePaid DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (BookingID, SeatID),
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID),
    FOREIGN KEY (SeatID) REFERENCES Seats(SeatID)
);

INSERT INTO BookingSeats (BookingID, SeatID, PricePaid)
VALUES
(11, 34, 175.00),
(11, 35, 175.00);

INSERT INTO BookingSeats (BookingID, SeatID, PricePaid)
VALUES
(1, 26, 137.50),
(1, 27, 137.50);


CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT NOT NULL,
    PaymentMethod NVARCHAR(50), -- e.g., 'Credit Card', 'UPI'
    TransactionID NVARCHAR(255),
    PaymentStatus NVARCHAR(20) NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentTime DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT CHK_PaymentStatus CHECK (PaymentStatus IN ('Pending', 'Successful', 'Failed')),
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID)
);


INSERT INTO Payments (BookingID, PaymentMethod, TransactionID, PaymentStatus, Amount)
VALUES
(1, 'Credit Card', 'TXN_112233_A', 'Successful', 275.00),
(2, 'UPI', 'UPI_B445566', 'Successful', 600.00),
(3, 'Credit Card', 'TXN_778899_C', 'Failed', 340.00),
(4, 'Debit Card', 'TXN_101010_D', 'Successful', 410.00),
(6, 'Net Banking', 'NET_E202020', 'Successful', 620.00),
(5, 'UPI', 'UPI_F303030', 'Pending', 500.00),
(8, 'Credit Card', 'TXN_404040_G', 'Pending', 300.00),
(7, 'UPI', 'UPI_H505050', 'Successful', 450.00),
(9, 'Credit Card', 'TXN_606060_I', 'Successful', 700.00);



CREATE TABLE Reviews (
    ReviewID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    MovieID INT NOT NULL,
    Rating DECIMAL(2, 1) NOT NULL, 
    Comment NVARCHAR(MAX),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (MovieID) REFERENCES Movies(MovieID)
);

INSERT INTO Reviews (UserID, MovieID, Rating, Comment)
VALUES
(1, 1, 9.0, 'Absolutely fantastic! Fahadh Faasil was brilliant.'),
(2, 2, 9.5, 'One of the best survival thrillers ever. A must-watch experience.'),
(3, 3, 8.0, 'Such a fun and lighthearted romantic comedy. Great chemistry!'),
(4, 6, 4.5, 'All style, no substance. The story was very weak.'),
(5, 4, 8.5, NULL);
