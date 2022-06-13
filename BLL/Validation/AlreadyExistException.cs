namespace BLL.Validation
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string mes) : base(mes) { }
    }
}
