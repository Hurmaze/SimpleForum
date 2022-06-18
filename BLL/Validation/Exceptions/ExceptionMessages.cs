namespace Services.Validation.Exceptions
{
    public static class ExceptionMessages
    {
        public static string EmailIsAlreadyUsed = "The email {0} is already used.";
        public static string NicknameTaken = "The nickname {0} is already taken.";
        public static string NotFound = "The object of type {0} with property {1} = {2} is not found";
        public static string AlreadyExists = "The object of type {0} with propery {1} = {2} is already exist.";
        public static string WrongPassword = "The inputed password is incorrect.";
        public static string NicknamesAreEqual = "Choose nickname different from current. ";
    }
}
