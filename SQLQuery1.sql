	select * from Customers cu where cu.CustomerFirstName like '%k%' order by cu.CustomerFirstName asc
	select * from Customers cu where cu.Id between 25 and 28
	select * from Customers cu where cu.id not in(25,29)
	select * from Customers cu where cu.id in(25,29)
	select * from Customers cu where cu.CustomerFirstName not like '%k%'
	select * from Products prod


select * from Orders ord where ord.CustomerId in
	(select cu.Id from Customers cu where cu.CustomerFirstName like '%k%' and ord.ProductId in
	(select prod.Id from Products prod where prod.ProductName like '%k%')
	)

select * from Products prod where prod.Id in
 (select  ord.ProductId from Orders ord where ord.CustomerId in 
 (select cu.Id from Customers cu where cu.CustomerFirstName ='Freeman')
 )

select prod.ProductName, cu.CustomerFirstName, ord.DateOfBuy, ord.QuantityOfProducts from Customers cu inner join Orders ord on cu.Id = ord.CustomerId inner join Products prod on ord.ProductId = prod.Id where cu.CustomerFirstName = 'Kiston'

select prod.ProductName, ord.QuantityOfProducts
 from Products prod left outer join Orders ord
 on  prod.Id = ord.ProductId

select ord.DateOfBuy, prod.ProductName
from Orders ord right outer join Products prod
on ord.ProductId = prod.Id

select COUNT(pr.Id) from Products pr

	select ord.ProductId, COUNT(ord.Id) as ItemsInGroup  from Orders ord
	group by ord.ProductId
	having count(ord.Id)>2

	update Products Set ProductName ='SuitSSSSSSSSS'
	where Id = 17

	update Products set ProductDescription = 'priceHereOver130'
	where ProductPrice>=130

	delete from Customers
	where CustomerFirstName ='AKIn'

select  CONCAT(cus.CustomerFirstName,' ', cus.CustomerLatName) as contactenation from Customers cus
select RTRIM(cus.CustomerFirstName) from Customers cus
select UPPER(pr.ProductName) from Products pr
select LOWER(pr.ProductName) from Products pr
select pr.ProductName, len(pr.ProductName) from Products pr
select * from Products pr

select GETDATE(), GETUTCDATE(), SYSDATETIME(),SYSDATETIMEOFFSET(), SYSUTCDATETIME(), DATEADD(yy,2,getdate()), DATENAME(DAY,GETDATE()), DATENAME(WEEKDAY,GETDATE()), DATEDIFF(HOUR,getdate(),DATEADD(month,2,getdate()))

select pr.ProductName, pr.DateOfAdding, DATEADD(yy,2,pr.DateOfAdding) as added from Products pr

create view sums as
select pr.ProductName, pr.ProductPrice, ord.QuantityOfProducts, pr.ProductPrice*ord.QuantityOfProducts as Suma, ord.DateOfBuy 
from Orders ord
 inner join Products pr on ord.ProductId = pr.Id

 select SUM(Suma) from sums
 select * from sums

select SUM(pr.ProductPrice) as SUMMA, AVG(pr.ProductPrice) as AVG, MAX(pr.ProductPrice), MIN(pr.ProductPrice) from Products pr

select * from Customers cu
insert into  Customers
(CustomerFirstName, CustomerLatName, DateOfRegistration)
values
('testInsert', 'testInsert', GETDATE())

create procedure insIntoCust(@ft nvarchar(max), @lt nvarchar(max), @dt datetime)
as insert into  Customers
(CustomerFirstName, CustomerLatName, DateOfRegistration)
values
(@ft, @lt, @dt)
go

exec insIntoCust 'storedProcedureFt','storedProcedureLt', '2017-05-28 07:52:37.330'
go

declare @date datetime = getdate();
exec insIntoCust 'testStPro','testStorPro', @date
go

drop procedure insIntoCust
go
declare @dater datetime = getdate()

create procedure insIntoCust(@ft nvarchar(max) ='DefaultParStProd', @lt nvarchar(max) ='DefaultParStProd', @dt datetime = '2017-05-28', @em nvarchar(max) = 'defStPrMail')
as insert into  Customers
(CustomerFirstName, CustomerLatName, DateOfRegistration, CustomerEmail)
values
(@ft, @lt, @dt, @em)


declare @dater datetime = getdate();
exec insIntoCust 'test','test',@dater,'test'