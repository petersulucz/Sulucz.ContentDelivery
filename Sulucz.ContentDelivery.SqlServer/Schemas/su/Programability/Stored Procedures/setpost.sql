CREATE PROCEDURE su.setpost
    @errormessage   NVARCHAR(2048)        OUTPUT
   ,@id             INT                 = NULL
   ,@slug           NVARCHAR(256)
   ,@title          NVARCHAR(256)
   ,@description    NVARCHAR(512)
   ,@whenpublished  DATETIME2    
   ,@revision       INT          
   ,@postcontent    su.postcontentlist    READONLY
AS
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED

    DECLARE @error        INT = 0
    DECLARE @itemnotfound INT = 50001
    DECLARE @invalidargs  INT = 50002

    BEGIN TRY
        BEGIN TRANSACTION

        IF(@id IS NOT NULL AND NOT EXISTS (SELECT TOP 1 1 FROM su.postmeta WHERE id = @id))
        BEGIN
            SET @error = @itemnotfound
            SET @errormessage = N'The post with id ''' + CAST(@id AS NVARCHAR(10)) + ''' was not found.'
            GOTO ErrorHandler
        END

        IF(EXISTS (SELECT TOP 1 1 FROM su.postmeta WHERE slug = @slug AND (id <> @id OR @id IS NULL)))
        BEGIN
            SET @error = @invalidargs
            SET @errormessage = N'The slug ''' + @slug + ''' is already taken.'
            GOTO ErrorHandler
        END

        IF(@id IS NOT NULL AND EXISTS (SELECT TOP 1 1 FROM su.postmeta WHERE id = @id AND revision <> @revision))
        BEGIN
            SET @error = @invalidargs
            SET @errormessage = N'The revision ''' + CAST(@revision AS NVARCHAR(4)) + ''' does not match.'
            GOTO ErrorHandler
        END

        IF (@id IS NULL)
        BEGIN
            INSERT INTO su.postmeta
            (
                 slug
                ,title
                ,description
                ,whencreated
                ,whenpublished
                ,revision
            )
            VALUES
            (
                @slug
               ,@title
               ,@description
               ,GETUTCDATE()
               ,@whenpublished
               ,0
            )

            SET @id = SCOPE_IDENTITY()
        END
        ELSE
        BEGIN
            UPDATE su.postmeta
            SET
                 slug = @slug
                ,title = @title
                ,description = @description
                ,whenpublished = @whenpublished
                ,revision = revision + 1
            WHERE id = @id

            IF (@@ROWCOUNT <> 1)
            BEGIN
                SET @error = @itemnotfound
                SEt @errormessage = N'The post with id ''' + @id + ''' was not found.'
                GOTO ErrorHandler
            END
        END

        DELETE FROM su.postcontent
        WHERE postid = @id

        INSERT INTO su.postcontent
        (
             postid
            ,orderid
            ,revision
            ,contenttype
            ,content
        )
        SELECT
            @id
           ,orderid
           ,revision
           ,contenttype
           ,content
        FROM @postcontent

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        SET @error = ERROR_NUMBER()
        SET @errormessage = ERROR_MESSAGE()
        GOTO ErrorHandler
    END CATCH
RETURN @error

ErrorHandler:
    ROLLBACK TRANSACTION
    RETURN @error
