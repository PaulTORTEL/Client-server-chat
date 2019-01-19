using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat.Exceptions
{
    class UnknownChannelException : Exception
    {
        public UnknownChannelException()
        {
        }

        public UnknownChannelException(String message) : base(message)
        {

        }

        public UnknownChannelException(String message, Exception inner) : base(message, inner)
        {

        }
    }

    class ChannelNameAlreadyTaken : Exception
    {
        public ChannelNameAlreadyTaken()
        {
        }

        public ChannelNameAlreadyTaken(String message) : base(message)
        {

        }

        public ChannelNameAlreadyTaken(String message, Exception inner) : base(message, inner)
        {

        }
    }

    class TooFewChannelsException : Exception
    {
        public TooFewChannelsException()
        {
        }

        public TooFewChannelsException(String message) : base(message)
        {

        }

        public TooFewChannelsException(String message, Exception inner) : base(message, inner)
        {

        }
    }

}
