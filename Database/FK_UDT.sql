
/****** Object:  UserDefinedTableType [dbo].[OrderProductDetailsType]    Script Date: 21-03-2025 12:19:48 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'OrderProductDetailsType' AND ss.name = N'dbo')
CREATE TYPE [dbo].[OrderProductDetailsType] AS TABLE(
	[ProductId] [int] NULL,
	[Quantity] [int] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 21-03-2025 12:19:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[SplitString]
(
    @Input NVARCHAR(MAX),
    @Delimiter CHAR(1)
)
RETURNS @Output TABLE (Value NVARCHAR(MAX))
AS
BEGIN
    DECLARE @Start INT, @End INT
    SELECT @Start = 1, @End = CHARINDEX(@Delimiter, @Input)
   
    WHILE @Start < LEN(@Input) + 1
    BEGIN
        IF @End = 0  
            SET @End = LEN(@Input) + 1
       
        INSERT INTO @Output (Value)
        VALUES(SUBSTRING(@Input, @Start, @End - @Start))
       
        SET @Start = @End + 1
        SET @End = CHARINDEX(@Delimiter, @Input, @Start)
    END
   
    RETURN
END' 
END
GO
