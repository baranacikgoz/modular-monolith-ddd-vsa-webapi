using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Options;

public class OutboxOptions
{
    [Required]
    public int BackgroundJobPeriodInMilliSeconds { get; set; }

    [Required]
    public int BatchSizePerExecution { get; set; }

    [Required]
    public int MaxFailCountBeforeSentToDeadLetter { get; set; }
}
