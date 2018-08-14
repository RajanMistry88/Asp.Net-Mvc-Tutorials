
--Create Table If it is not exisit 
Create table tblProducts
(
 Id int primary key identity,
 Name nvarchar(25),
 [Description] nvarchar(50),
 Price int
)

--Select Blow Code and Execute  Make Sure You Change You Table Name and Parameter
Declare @Start int 
Set @Start = 1 --Declare the insert value start with 1
--Declare the Parameter of table which we want to add
Declare @Name varchar(25)
Declare @Description varchar(50)

While(@Start <= 1000) -- While lopp start with 1 and end with 1000 (You can chnage End value if you want (@Start <= 50) )
Begin
 Set @Name = 'Product - ' + LTRIM(@Start) -- Asign value to the Delclared Parameter @Name + Declare Value @Start
 Set @Description = 'Product Description - ' + LTRIM(@Start)-- Asign value to the Declared Parameter @Description  + Declare Value @Start
 Insert into tblProducts values (@Name, @Description, @Start * 10) -- Insert Declared Paramete Value into the Table using insert Query
 Set @Start = @Start + 1 -- Increment the Declare Value @Start with existing Value
End
GO

