## Developer setup
This project depends on gRPC. The configuration in the code can be used to run a single host: it will start the host on localhost, port 19796. 

To configure a host that connects to other services, use the AddServer method in the host. The bus will not start until the server connections are established.

```csharp
services.AddMassTransit(x =>
{
    x.AddConsumer<CreateBookmarkCommandHandler>();

    x.UsingGrpc((context, cfg) =>
    {
        var options = context.GetRequiredService<IOptions<StartupOptions>>();
        
        cfg.Host(h =>
        {
            h.Host = options.Value.Host ?? "127.0.0.1";
            h.Port = options.Value.Port ?? 19796;

            foreach (var host in options.Value.GetServers())
                h.AddServer(host);
        });

        cfg.ConfigureEndpoints(context);
    });
});
```

You can run multiple hosts on a single machine by providing the correct startup options:

```bash
#host 1 (19796 and 5001 are the default ports for gRpc and Kestrel, provided for clarity)
dotnet run --port 19796 --urls "https://localhost:5001"

#host 2 (connects to host 1 before the bus starts)
dotnet run --servers http://127.0.0.1:19796/ --port 19797 --urls "https://localhost:5002"
```