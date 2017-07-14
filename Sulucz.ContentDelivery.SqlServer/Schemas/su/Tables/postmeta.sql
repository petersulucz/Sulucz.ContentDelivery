CREATE TABLE su.postmeta
(
    id              INT             NOT NULL IDENTITY(1, 1)
   ,slug            NVARCHAR(256)   NOT NULL
   ,title           NVARCHAR(256)   NOT NULL
   ,description     NVARCHAR(512)   NOT NULL
   ,whencreated     DATETIME2       NOT NULL
   ,whenpublished   DATETIME2       NOT NULL
   ,revision        INT             NOT NULL
)
