using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.dte
{
    public class DayResult
    {
        public bool IsSuccess { get; }
        public DayOfWeek DayOfWeek { get; }

        public DayResult(bool isSuccess, DayOfWeek dayOfWeek)
        {
            IsSuccess = isSuccess;
            DayOfWeek = dayOfWeek;  
        }
    }
}
