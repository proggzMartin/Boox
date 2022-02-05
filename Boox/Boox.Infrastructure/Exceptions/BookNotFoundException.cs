﻿namespace Boox.Infrastructure.Exceptions
{
    [Serializable]
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException()
        {
        }

        public BookNotFoundException(string message)
            : base(message)
        {
        }

        public BookNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
