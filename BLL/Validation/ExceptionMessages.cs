
namespace BLL.Validation
{
    public static class ExceptionMessages
    {
        public static string EmailIsAlreadyUsed = "The email {0} is already used.";
        public static string InvalidRegistration = "Some of the required fields is not filled properly.";
        public static string NicknameTaken = "The nickname {0} is already taken.";
        public static string NotFound = "The object of type {0} with property {1} = {2} is not found";
    }
}
