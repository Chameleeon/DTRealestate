USE `realestate`;

-- Insert Users
INSERT INTO `User` (FirstName, LastName, Username, Password, Salt, Email) VALUES
('Admin', 'Admin', 'admin', 'FfvZCO42tNqRMlokeuAx14NssS6GjyyiTbTsy8IcsUk=', 'AA89DB3/eCwIJJyuW5No+A==', 'admin@email.com'),
('Jane', 'Smith', 'janesmith', 'FfvZCO42tNqRMlokeuAx14NssS6GjyyiTbTsy8IcsUk=', 'AA89DB3/eCwIJJyuW5No+A==', 'jane.smith@email.com'),
('Mark', 'Knopfler', 'agentmark', 'FfvZCO42tNqRMlokeuAx14NssS6GjyyiTbTsy8IcsUk=', 'AA89DB3/eCwIJJyuW5No+A==', 'mark.knoplfer@dtrealestate.com');

-- Insert Administrators
INSERT INTO `Administrator` (Korisnik_idKorisnik) VALUES (1);

-- Insert Clients
INSERT INTO `Client` (Korisnik_idKorisnik) VALUES (2);

-- Insert Agents
INSERT INTO `Agent` (Korisnik_idKorisnik, PhoneNumber) VALUES (3, '+123456789');

-- Insert Addresses
INSERT INTO `Address` (Number, Street, City) VALUES
('10A', 'Main Street', 'New York'),
('22B', 'Broadway', 'Los Angeles'),
('78C', 'Sunset Blvd', 'San Francisco');

-- Insert Realestate Types
INSERT INTO `RealestateType` (RealestateType) VALUES
('Apartment'),
('House');

-- Insert Realestate
INSERT INTO `Realestate` (Description, Adresa_idAdresa, TipNekretnine_idTipNekretnine, SquareFootage, ImagePaths, DateAdded) VALUES
('A beautiful apartment in downtown.', 1, 1, 62, '[
  {"path": "Images/1/apt2.jpg", "isPrimary": true, "order": 1},
  {"path": "Images/1/cabinet.png", "isPrimary": true, "order": 2},
  {"path": "Images/1/apt1.webp", "isPrimary": false, "order": 3}
]', '2024-12-01'),
('Spacious house with a garden.', 2, 2, 150, '[
  {"path": "Images/2/house1.jpeg", "isPrimary": true, "order": 1},
  {"path": "Images/2/house2.jpg", "isPrimary": false, "order": 2}
]', '2024-12-05'),
('Modern condo with city view.', 3, 1, 85, '[
  {"path": "Images/3/apt3.jpg", "isPrimary": true, "order": 1},
  {"path": "Images/3/apt4.jpg", "isPrimary": false, "order": 2}
]', '2025-01-20');


-- Insert Offer Types
INSERT INTO `OfferType` (OfferType) VALUES
('Sale'),
('Rent');

-- Insert Offers
INSERT INTO `Offer` (Tip_idTip, Realestate_idRealestate, Agent_Korisnik_idKorisnik, Price, ShortDescription, Title, DateAdded) VALUES
(1, 1, 3, 250000, 'Spatious apartment recently renovated. Two bedrooms, living room, kitchen, bathroom. Has a balcony.', 'Apartment for sale in downtown.', '2025-01-10'),
(2, 2, 3, 1200, 'House for rent. Two stories, 4 bedrooms, 2 bathrooms, kitchen and living room.', 'Suburban house for rent.', '2025-01-12'),
(1, 3, 3, 320000, 'Modern condo with 2 bedrooms, open kitchen, panoramic windows.', 'City view condo for sale.', '2025-01-22'),
(2, 1, 3, 1000, 'Apartment available for monthly rent. Recently renovated, fully furnished.', 'Downtown apartment for rent.', '2025-01-23');


-- Insert Inquiries
INSERT INTO `Inquiry` (Message, Client_Korisnik_idKorisnik, Offer_idOffer) VALUES
('Hi, I\'m interested in the apartment. Is it still available?', 2, 1),
('Can I schedule a visit to see the house next weekend?', 2, 2),
('Is the city view condo still available for a visit this week?', 2, 3),
('What\'s the minimum lease term for the apartment?', 2, 4);

-- Insert Contracts
INSERT INTO `Contract` (Content, SignDate, PeriodVazenja, Offer_idOffer, Client_Korisnik_idKorisnik) VALUES
('This contract confirms the sale of the apartment to Jane Smith. Payment will be completed upon final inspection.', '2025-04-01', 12, 1, 2),
('Rental contract for downtown apartment. Lease term is 6 months, renewable.', '2025-04-05', 6, 4, 2),
('Contract for purchase of city view condo. Down payment received.', '2025-04-10', 12, 3, 2);

