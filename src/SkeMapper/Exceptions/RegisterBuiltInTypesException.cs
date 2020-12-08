namespace SkeMapper.Exceptions
{
    public class RegisterBuiltInTypesException : BaseCustomException
    {
        public RegisterBuiltInTypesException()
           : base() { }

        public RegisterBuiltInTypesException(string message)
            : base(message) { }

        public RegisterBuiltInTypesException(string message, params object[] args)
            : base(message, args) { }
    }
}
