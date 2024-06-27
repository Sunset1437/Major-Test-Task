using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.DataBase.Entities
{
    public class ApplicationEntity : EntityBase
    {
        public string SenderAddress { get; set; }
        public string ReceiverAddress { get; set;}
        public DateTime TakingDate { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string? Status { get; set; }
        public string? Executor { get; set; }
        public string? WhyIsCanceled { get; set; }
    }
}
