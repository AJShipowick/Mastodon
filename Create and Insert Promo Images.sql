CREATE TABLE PromoImages
(
    Id int,
    Name varchar(50) not null,
    Photo varbinary(max) not null
)

INSERT INTO PromoImages (Id, Name, Photo) 
SELECT 10, 'Image1', BulkColumn 
FROM Openrowset( Bulk 'C:\Users\ajshi\Downloads\BulkUpload\sky.svg', Single_Blob) as PromoImage;