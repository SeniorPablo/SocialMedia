using System;

namespace SocialMedia.Api.Core.Exceptions
{
    public class BussinesException : Exception
    {
        public BussinesException()
        {

        }

        public BussinesException(string message) :  base(message)
        {

        }
    }
}
