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
        id      INT
    )

    INSERT INTO @posts
    (
        id
    )
    SELECT
        id
    FROM  su.postmeta m
    WHERE (@id IS NULL AND @slug IS NULL)
    OR ((@id IS NULL OR id = @id)
      AND (@slug IS NULL OR slug = @slug)
    )
    ORDER BY id
    OFFSET (@skip) ROWS
    FETCH NEXT (@top) ROWS ONLY

    SELECT
        pc.postid
       ,pc.orderid
       ,pc.revision
       ,pc.uniqueid
       ,pc.content
       ,pc.contenttype
    FROM su.postcontent pc
    JOIN @posts p
      ON p.id = pc.postid

    SELECT
        pm.id
       ,pm.slug
       ,pm.title
       ,pm.description
       ,pm.whencreated
       ,pm.whenpublished
       ,pm.revision
    FROM su.postmeta pm
    JOIN @posts p
      ON pm.id = p.id

RETURN 0
