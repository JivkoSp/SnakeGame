using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Models.Exceptions
{
    public class SnakeException : ApplicationException
    {
    
        public SnakeException(string message) : base(message)
        {

        }

    }
}
