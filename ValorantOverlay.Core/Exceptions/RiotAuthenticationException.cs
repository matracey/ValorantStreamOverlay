using System;
using System.Runtime.Serialization;

namespace ValorantOverlay.Core.Exceptions
{

    [Serializable]
    public class RiotAuthenticationException : Exception
    {
        public RiotAuthenticationException() { }
        public RiotAuthenticationException(string message) : base(message) { }
        public RiotAuthenticationException(string message, Exception inner) : base(message, inner) { }
        protected RiotAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class UserLoginInvalidException : RiotAuthenticationException
    {
        public UserLoginInvalidException() { }
        public UserLoginInvalidException(string message) : base(message) { }
        public UserLoginInvalidException(string message, Exception inner) : base(message, inner) { }
        protected UserLoginInvalidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class UserLoginNotSetException : RiotAuthenticationException
    {
        public UserLoginNotSetException() { }
        public UserLoginNotSetException(string message) : base(message) { }
        public UserLoginNotSetException(string message, Exception inner) : base(message, inner) { }
        protected UserLoginNotSetException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class AuthenticationErrorException : RiotAuthenticationException
    {
        public AuthenticationErrorException() { }
        public AuthenticationErrorException(string message) : base(message) { }
        public AuthenticationErrorException(string message, Exception inner) : base(message, inner) { }
        protected AuthenticationErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
