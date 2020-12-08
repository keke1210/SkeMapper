namespace SkeMapper.Exceptions
{
    public class MappingNotExistsException : BaseCustomException
    {
        public MappingNotExistsException()
           : base() { }

        public MappingNotExistsException(string message)
            : base(message) { }

        public MappingNotExistsException(string message, params object[] args)
            : base(message, args) { }
    }
}
