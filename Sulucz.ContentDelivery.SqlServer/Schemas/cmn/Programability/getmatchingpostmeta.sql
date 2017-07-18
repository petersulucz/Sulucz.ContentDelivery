CREATE PROCEDURE cmn.getmatchingpostmeta
    @errormessage   NVARCHAR(2048)        OUTPUT
   ,@id             INT                 = NULL
   ,@slug           NVARCHAR(256)       = NULL
   ,@top            INT                 = NULL
   ,@skip           INT                 = NULL
AS
    SET NOCOUNT ON

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

    SELECT
        m.id
       ,m.slug
       ,m.title
       ,m.description
       ,m.whencreated
       ,m.whenpublished
       ,m.revision
    FROM  su.postmeta m
    WHERE (@id IS NULL AND @slug IS NULL)
    OR ((@id IS NULL OR id = @id)
      AND (@slug IS NULL OR slug = @slug)
    )
    ORDER BY id
    OFFSET (@skip) ROWS
    FETCH NEXT (@top) ROWS ONLY
RETURN 0
