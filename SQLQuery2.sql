select * from Customers
select * from Products
insert into Products values
('test',23,'testDesc',GETDATE()),
('test',23,'testDesc',GETDATE()),
('test',23,'testDesc',GETDATE()),
('test',23,'testDesc',GETDATE()),
('test',23,'testDesc',GETDATE()),
('test',23,'testDesc',GETDATE()),
('test',23,'testDesc',GETDATE()),
('test',23,'testDesc',GETDATE()),
('test',23,'testDesc',GETDATE());

insert into Customers values
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com'),
('test','tst',GETDATE(),'test@mail.com');



INSERT INTO Customers (CustomerName, ContactName, Address, City, PostalCode, Country)
VALUES ('Cardinal', 'Tom B. Erichsen', 'Skagen 21', 'Stavanger', '4006', 'Norway');