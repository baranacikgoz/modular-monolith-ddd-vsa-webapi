using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Options;
public class BackgroundJobsOptions
{
    public int PollingFrequencyInSeconds { get; set; } = 2;

    [Required(AllowEmptyStrings = false)]
    public string DashboardPath { get; set; } = "/hangfire";
}
