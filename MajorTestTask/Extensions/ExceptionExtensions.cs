using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.Extensions
{
    public class ExceptionExtensions : Exception
    {
        public ExceptionExtensions() : base() { }
        public ExceptionExtensions(string message) : base(message) { }
        public ExceptionExtensions(string message, Exception innerException) : base(message, innerException) { }
    }
    public class WeightException : ExceptionExtensions
    {
        public WeightException() : base() { }
    }
    public class DimensionsException : ExceptionExtensions
    {
        public DimensionsException() : base() { }
    }
    public class DataEntryException : ExceptionExtensions
    {
        public DataEntryException() : base() { }
    }
}
