namespace Services.Validation.Exceptions
{
    /// <summary>
    /// Privides basic exception messages.
    /// </summary>
    public static class ExceptionMessages
    {
        public static string EmailUsed = "The email {0} is already used.";
        public static string NicknameTaken = "The nickname {0} is already taken.";
        public static string NotFound = "The object of type {0} with property {1} = {2} is not found";
        public static string AlreadyExists = "The object of type {0} with propery {1} = {2} is already exist.";
        public static string WrongPassword = "The inputed password is incorrect.";
        public static string NicknamesAreEqual = "Choose nickname different from current. ";
        public static string DifferenceEmail = "The email {0} is different from the user's. User id: {1}. ";
    }
}
