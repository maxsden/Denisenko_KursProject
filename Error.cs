using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denisenko_KursProject
{
    internal class Error
    {
        public string type;
        public string message;
        public int row;

        public Error(string _type, string _message, int _row)
        {
            type= _type;
            message= _message;
            row = _row;
        }
    }
}
