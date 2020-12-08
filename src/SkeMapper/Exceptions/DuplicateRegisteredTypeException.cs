namespace SkeMapper.Exceptions
{
    public class DuplicateRegisteredTypeException : BaseCustomException
    {
        public DuplicateRegisteredTypeException()
            : base() { }

        public DuplicateRegisteredTypeException(string message)
            : base(message) { }

        public DuplicateRegisteredTypeException(string message, params object[] args)
            : base(message, args) { }
    }
}
