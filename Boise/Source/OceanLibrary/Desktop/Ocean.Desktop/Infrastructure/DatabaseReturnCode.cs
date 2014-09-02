
namespace Ocean.Infrastructure {

    /// <summary>
    /// Specifies the DatabaseReturnCode
    /// </summary>
    public enum DatabaseReturnCode {
        /// <summary>
        /// Database call sucessful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Database call record not found
        /// </summary>
        RecordNotFound = -1,
        /// <summary>
        /// Database call time stamp mismatch
        /// </summary>
        TimeStampMismatch = -2,
        /// <summary>
        /// Database call operation failed
        /// </summary>
        OperationFailed = -3,
        /// <summary>
        /// Database call record already in databaes
        /// </summary>
        RecordAlreadyInDatabase = -4,
        /// <summary>
        /// Database call connection state error
        /// </summary>
        ConnectionStateError = -500,
        /// <summary>
        /// Database call missing command text
        /// </summary>
        MissingCommandText = -501,
        /// <summary>
        /// Database call missing table name
        /// </summary>
        MissingTableName = -502,
        /// <summary>
        /// Database call missing dataset instance
        /// </summary>
        MissingDataSetInstance = -503,
        /// <summary>
        /// Database call referential integrity violated
        /// </summary>
        ReferentialIntegrityViolated = -504,
        /// <summary>
        /// Database call duplication key violation
        /// </summary>
        DuplicateKeyViolation = -505,
        /// <summary>
        /// Database call value not set
        /// </summary>
        NotSet = -1000
    }
}

//The following is an example of how the stored procedure returns the DatabaseReturnCode
//
// NOTE:  dbo.GetReturnValue is a user defined function.
//
//    1 = success             -  dbo.GetReturnValue('success')
//	-1 = record not found   -  dbo.GetReturnValue('recordnotfound')
//	-2 = timestamp mismatch -  dbo.GetReturnValue('timestampmismatch')
//	-3 = failed             -  dbo.GetReturnValue('failed')
//	-4 = recordalreadyindatabase - dbo.GetReturnValue('recordalreadyindatabase')
//	-100 = unable to translate the name lookup
//CREATE FUNCTION [dbo].[GetReturnValue]
//	(
//	@NAME varchar(25)
//	)
//RETURNS int
//AS
//BEGIN
//*
//	dbo.GetReturnValue return values
//	1 = success             -  dbo.GetReturnValue('success')
//	-1 = record not found   -  dbo.GetReturnValue('recordnotfound')
//	-2 = timestamp mismatch -  dbo.GetReturnValue('timestampmismatch')
//	-3 = failed             -  dbo.GetReturnValue('failed')
//	-4 = recordalreadyindatabase - dbo.GetReturnValue('recordalreadyindatabase')
//	-100 = unable to translate the name lookup
//	Values above 100 are reserved for client side trapped conditions and should never
//	be used in a stored procedure.
//*/
//DECLARE @RETURN int
//DECLARE @KEY varchar(25)
//SET @KEY = LOWER(@NAME)
//IF @KEY = 'success'
//	SET @RETURN = 1
//ELSE IF @KEY = 'recordnotfound' 
//	SET @RETURN = -1
//ELSE IF @KEY = 'timestampmismatch' 
//	SET @RETURN = -2
//ELSE IF @KEY = 'failed'
//	SET @RETURN = -3
//ELSE IF @KEY = 'recordalreadyindatabase'
//	SET @RETURN = -4
//ELSE
//	SET @RETURN = -100
//RETURN @RETURN
//END