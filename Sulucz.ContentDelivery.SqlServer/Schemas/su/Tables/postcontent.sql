CREATE TABLE su.postcontent
(
    postid          INT             NOT NULL
   ,orderid         INT             NOT NULL
   ,uniqueid        INT             NOT NULL IDENTITY(1,1)
   ,revision        INT             NOT NULL
   ,contenttype     NVARCHAR(64)    NOT NULL
   ,content         NVARCHAR(MAX)   NOT NULL
)
