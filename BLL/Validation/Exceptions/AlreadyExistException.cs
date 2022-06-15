namespace Services.Validation.Exceptions
{
    public class AlreadyExistException : CustomException
    {
        public AlreadyExistException(string mes) : base(mes) { }
    }
}
