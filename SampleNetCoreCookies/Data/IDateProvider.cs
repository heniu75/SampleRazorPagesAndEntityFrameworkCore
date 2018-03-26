using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNetCoreCookies.Data
{
    public interface IDateProvider
    {
        DateTime Now { get; }
    }

    public class DateProvider : IDateProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
