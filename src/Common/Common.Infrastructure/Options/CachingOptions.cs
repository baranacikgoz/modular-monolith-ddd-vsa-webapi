using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Options;
public class CachingOptions
{
    public bool UseRedis { get; set; }
    public RedisOptions? Redis { get; set; } = default!;
}

public class RedisOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Host { get; set; } = default!;
    public int Port { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string AppName { get; set; } = default!;
}
