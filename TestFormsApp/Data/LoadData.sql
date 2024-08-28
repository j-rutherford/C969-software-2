-- Insert Countries (Unique for each Customer)
-- reset if user table is empty, delete everything else too.
DELETE FROM appointment;
DELETE FROM customer;
DELETE FROM address;
delete from city;
delete from country;
INSERT INTO country (CountryId, Country, createDate, createdBy, lastUpdate, lastUpdateBy)
VALUES 
(1, 'USA', NOW(), 'system', NOW(), 'system'),
(2, 'Canada', NOW(), 'system', NOW(), 'system'),
(3, 'UK', NOW(), 'system', NOW(), 'system'),
(4, 'USA', NOW(), 'system', NOW(), 'system'),
(5, 'Canada', NOW(), 'system', NOW(), 'system');

-- Insert Cities (Unique for each Customer)
INSERT INTO city (CityId, City, CountryId, createDate, createdBy, lastUpdate, lastUpdateBy)
VALUES 
(1, 'New York', 1, NOW(), 'system', NOW(), 'system'),
(2, 'Toronto', 2, NOW(), 'system', NOW(), 'system'),
(3, 'London', 3, NOW(), 'system', NOW(), 'system'),
(4, 'New York', 4, NOW(), 'system', NOW(), 'system'),
(5, 'Toronto', 5, NOW(), 'system', NOW(), 'system');

-- Insert Addresses (Unique for each Customer)
INSERT INTO address (AddressId, Address, Address2, PostalCode, Phone, CityId, createDate, createdBy, lastUpdate, lastUpdateBy)
VALUES 
(1, '123 Main St', 'Apt 1', '10001', '123-456-7890', 1, NOW(), 'system', NOW(), 'system'),
(2, '456 Maple Ave', 'Unit 202', 'M4B 1B3', '987-654-3210', 2, NOW(), 'system', NOW(), 'system'),
(3, '789 King St', 'Suite 3', 'SW1A 1AA', '555-123-4567', 3, NOW(), 'system', NOW(), 'system'),
(4, '321 Queen St', 'Apt 4', '10002', '123-111-2222', 4, NOW(), 'system', NOW(), 'system'),
(5, '654 Oak St', 'Unit 305', 'M4B 1C4', '987-999-8888', 5, NOW(), 'system', NOW(), 'system');

-- Insert Customers
INSERT INTO customer (CustomerId, CustomerName, AddressId, Active, CreateDate, CreatedBy, LastUpdate, LastUpdateBy)
VALUES 
(1, 'John Doe', 1, 1, NOW(), 'system', NOW(), 'system'),
(2, 'Jane Smith', 2, 1, NOW(), 'system', NOW(), 'system'),
(3, 'Alice Johnson', 3, 1, NOW(), 'system', NOW(), 'system'),
(4, 'Bob Brown', 4, 1, NOW(), 'system', NOW(), 'system'),
(5, 'Charlie Green', 5, 1, NOW(), 'system', NOW(), 'system');

-- Insert Users (Consultants)
INSERT INTO user (UserId, UserName, Active, Password, CreateDate, CreatedBy, LastUpdate, LastUpdateBy)
VALUES 
(1, 'test', 1,'test', NOW(), 'system', NOW(), 'system'),
(2, 'consultant2', 1,'password123', NOW(), 'system', NOW(), 'system'),
(3, 'consultant3', 1,'password123', NOW(), 'system', NOW(), 'system');

-- Insert Appointments (Ensure no overlapping appointments for users)
-- Appointments for Consultant 1
INSERT INTO appointment (AppointmentId, CustomerId, UserId, Type, Title, Start, End, CreateDate, CreatedBy, LastUpdate, LastUpdateBy, Description, Location, Contact, Url)
VALUES 
(1, 1, 1, 'Consultation', '[N/A]', '2024-08-26 09:00:00', '2024-08-26 10:00:00', NOW(), 'system', NOW(), 'system', '[N/A]', '[N/A]', '[N/A]', '[N/A]'),
(2, 2, 1, 'Consultation', '[N/A]', '2024-08-26 10:15:00', '2024-08-26 11:15:00', NOW(), 'system', NOW(), 'system', '[N/A]', '[N/A]', '[N/A]', '[N/A]'),
(3, 3, 1, 'Follow-up', '[N/A]', '2024-08-26 13:00:00', '2024-08-26 14:00:00', NOW(), 'system', NOW(), 'system', '[N/A]', '[N/A]', '[N/A]', '[N/A]');

-- Appointments for Consultant 2
INSERT INTO appointment (AppointmentId, CustomerId, UserId, Type, Title, Start, End, CreateDate, CreatedBy, LastUpdate, LastUpdateBy, Description, Location, Contact, Url)
VALUES 
(4, 4, 2, 'Consultation', '[N/A]', '2024-08-27 09:00:00', '2024-08-27 10:00:00', NOW(), 'system', NOW(), 'system', '[N/A]', '[N/A]', '[N/A]', '[N/A]'),
(5, 5, 2, 'Strategy Session', '[N/A]', '2024-08-27 10:30:00', '2024-08-27 11:30:00', NOW(), 'system', NOW(), 'system', '[N/A]', '[N/A]', '[N/A]', '[N/A]');

-- Appointments for Consultant 3
INSERT INTO appointment (AppointmentId, CustomerId, UserId, Type, Title, Start, End, CreateDate, CreatedBy, LastUpdate, LastUpdateBy, Description, Location, Contact, Url)
VALUES 
(6, 1, 3, 'Consultation', '[N/A]', '2024-08-28 09:00:00', '2024-08-28 10:00:00', NOW(), 'system', NOW(), 'system', '[N/A]', '[N/A]', '[N/A]', '[N/A]'),
(7, 3, 3, 'Follow-up', '[N/A]', '2024-08-28 10:30:00', '2024-08-28 11:30:00', NOW(), 'system', NOW(), 'system', '[N/A]', '[N/A]', '[N/A]', '[N/A]'),
(8, 5, 3, 'Review', '[N/A]', '2024-08-28 13:00:00', '2024-08-28 14:00:00', NOW(), 'system', NOW(), 'system', '[N/A]', '[N/A]', '[N/A]', '[N/A]');
