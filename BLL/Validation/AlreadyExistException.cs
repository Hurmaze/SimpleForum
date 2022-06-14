namespace BLL.Validation
{
    public class AlreadyExistException : CustomException
    {
        public AlreadyExistException(string mes) : base(mes) { }
    }
}
