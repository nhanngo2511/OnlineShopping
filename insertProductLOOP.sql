
GO
	DECLARE @i int
	Set @i = 440883

	declare @Name nvarchar(500)
	

	declare @brandId int
	set @brandId = 1

	declare @Categoryid int
	set @Categoryid = 3

	declare @age int
	set @age = 1

	declare @country int
	set @country = 1

	declare @mode int
	set @mode = 1

	declare @image nvarchar
	set @image = 1

	declare @id int
	

WHILE @i < 1000000
BEGIN 

	set @Name = (N'Product Name ' + CAST(@i AS nvarchar(10)))

	IF @brandId > 10 BEGIN SET @brandId = 1 END

	IF @Categoryid > 13 BEGIN SET @Categoryid = 3 END

	IF @age > 4 BEGIN SET @age = 1 END

	IF @country > 5 BEGIN SET @country = 1 END

	IF @mode > 4 BEGIN SET @mode = 1 END

	IF @image > 7 BEGIN SET @image = 1 END


	exec spCreateProduct @Name, 100000, 
	90, 
	0, 
	0, 
	N'This is short discription', 
	N'This is long discription',
	N'This is wholesale discription', 
	N'This is brand details', 
	1, 
	1, 
	0,
	NULL,
	@brandId,
	@Categoryid,
	@age,
	@country,
	@mode,
	@image,
	@id output

	Set @i = @i + 1

	IF @brandId = 7 BEGIN set @brandId = @brandId + 3 END
	ELSE BEGIN set @brandId = @brandId + 1 END
	
	IF @Categoryid = 8 BEGIN set @Categoryid = @Categoryid + 4 END
	ELSE BEGIN set @Categoryid = @Categoryid + 1 END	   


		   set @age = @age + 1

		   set @country = @country + 1

		   set @mode = @mode + 1

		   set @image = @image + 1
END


select * from Products
