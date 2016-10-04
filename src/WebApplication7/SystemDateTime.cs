using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Interfaces;
namespace WebApplication7
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
