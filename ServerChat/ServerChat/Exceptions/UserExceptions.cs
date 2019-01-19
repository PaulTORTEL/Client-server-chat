using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat.Exceptions
{
    class UserNotCoException : Exception
    {
        public UserNotCoException()
        {
        }

        public UserNotCoException(String message) : base(message)
        {

        }

        public UserNotCoException(String message, Exception inner) : base(message, inner)
        {

        }
    }

    class TokenExistingException : Exception
    {
        public TokenExistingException()
        {
        }

        public TokenExistingException(String message) : base(message)
        {

        }

        public TokenExistingException(String message, Exception inner) : base(message, inner)
        {

        }
    }

    class UnknownUsernameException : Exception
    {
        public UnknownUsernameException()
        {
        }

        public UnknownUsernameException(String message) : base(message)
        {

        }

        public UnknownUsernameException(String message, Exception inner) : base(message, inner)
        {

        }
    }

    class UsernameTakenException : Exception
    {
        public UsernameTakenException()
        {
        }

        public UsernameTakenException(String message) : base(message)
        {

        }

        public UsernameTakenException(String message, Exception inner) : base(message, inner)
        {

        }
    }

    class WrongCredentialsException : Exception
    {
        public WrongCredentialsException()
        {
        }

        public WrongCredentialsException(String message) : base(message)
        {

        }

        public WrongCredentialsException(String message, Exception inner) : base(message, inner)
        {

        }
    }

    class UnknownTokenException : Exception
    {
        public UnknownTokenException()
        {
        }

        public UnknownTokenException(String message) : base(message)
        {

        }

        public UnknownTokenException(String message, Exception inner) : base(message, inner)
        {

        }
    }

}
