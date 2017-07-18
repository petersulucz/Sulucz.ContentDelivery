--
--  Gets post details
--      id and slug are mutually exclusive.
--
CREATE PROCEDURE su.getpost
    @errormessage   NVARCHAR(2048)        OUTPUT
   ,@id             INT                 = NULL
   ,@slug           NVARCHAR(256)       = NULL
   ,@top            INT                 = NULL
   ,@skip           INT                 = NULL
AS
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    DECLARE @error        INT = 0
    DECLARE @itemnotfound INT = 50001
    DECLARE @invalidargs  INT = 50002

    SET @top = ISNULL(@top, 1)
    SET @skip = ISNULL(@skip, 0)

    IF(@top > 20)
    BEGIN
        SET @top = 20
    END

    DECLARE @posts TABLE
    (
        id              INT             NOT NULL
       ,slug            NVARCHAR(256)   NOT NULL
       ,title           NVARCHAR(256)   NOT NULL
       ,description     NVARCHAR(512)   NOT NULL
       ,whencreated     DATETIME2       NOT NULL
       ,whenpublished   DATETIME2       NOT NULL
       ,revision        INT             NOT NULL
    )

    INSERT INTO @posts
    EXECUTE @error = cmn.getmatchingpostmeta @errormessage = @errormessage
                                            ,@id = @id
                                            ,@slug = @slug
                                            ,@skip = @skip
                                            ,@top = @top

    IF(@error <> 0)
    BEGIN
        GOTO ErrorHandler
    END

    DECLARE @ids cmn.intlist

    INSERT INTO @ids
    SELECT id
    FROM @posts

    -- Select the tag info
    EXECUTE @error = cmn.gettagsforpost @errormessage = @errormessage
                                       ,@postids = @ids

    IF(@error <> 0)
    BEGIN
        GOTO ErrorHandler
    END

    -- Get the post content
    SELECT
        pc.postid
       ,pc.orderid
       ,pc.revision
       ,pc.uniqueid
       ,pc.content
       ,pc.contenttype
    FROM su.postcontent pc
    JOIN @ids p
      ON p.id = pc.postid

    -- Get the post metadata.
    SELECT * FROM @posts

ErrorHandler:
RETURN @error
