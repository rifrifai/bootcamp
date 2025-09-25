using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers;

public class QueryObject
{
    public string? Symbol { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;
    public string? SortBy { get; set; } = string.Empty;
}