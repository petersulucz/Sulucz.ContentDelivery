CREATE PROCEDURE su.deletepost
    @errormessage NVARCHAR(2048) OUTPUT
   ,@id           INT
AS
    DECLARE @error INT = 0
    DECLARE @invlidargserror INT = 50002

    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED

    BEGIN TRY
        BEGIN TRANSACTION

            IF(@id IS NULL)
            BEGIN
                SET @error = @invlidargserror
                SET @errormessage = N'The id cannot be null.'
                GOTO ErrorHandler
            END

            DELETE FROM su.postmeta WHERE id = @id
            DELETE FROM su.postcontent WHERE postid = @id
            DELETE FROM su.tags WHERE postid = @id

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        SET @error = ERROR_NUMBER()
        SET @errormessage = ERROR_MESSAGE()
        GOTO ErrorHandler
    END CATCH

ErrorHandler:
ROLLBACK TRANSACTION
RETURN @error
