CREATE TYPE su.postcontentlist AS TABLE
(
    orderid         INT             NOT NULL
   ,uniqueid        INT             NULL
   ,revision        INT             NOT NULL
   ,contenttype     NVARCHAR(64)    NOT NULL
   ,content         NVARCHAR(MAX)   NOT NULL
)
