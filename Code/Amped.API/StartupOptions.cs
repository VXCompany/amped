using System;
using System.Collections.Generic;

namespace Amped.API;

public class StartupOptions
{
    public string Servers { get; set; }
    
    public int? Port { get; set; }

    public string Host { get; set; }

    public IEnumerable<Uri> GetServers()
    {
        if (string.IsNullOrWhiteSpace(Servers))
            yield break;

        var hosts = Servers.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        foreach (var host in hosts)
        {
            if (Uri.IsWellFormedUriString(host, UriKind.Absolute))
                yield return new Uri(host);
        }
    }
}