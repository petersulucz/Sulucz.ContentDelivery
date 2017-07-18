CREATE PROCEDURE cmn.gettagsforpost
    @errormessage NVARCHAR(2048) OUTPUT
   ,@postids      cmn.intlist    READONLY
AS
    SET NOCOUNT ON

    SELECT
       t.postid
      ,t.tag
    FROM su.tags t
    JOIN @postids ids
      ON ids.Id = t.postid

RETURN 0
