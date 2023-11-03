using MyCollection.Application.Worker;
using MyCollection.Data;
using MyCollection.BackgroundTasks;
using MyCollection.MessageBus;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services
        .AddData(hostContext.Configuration)
        .AddBackgroundTasks(hostContext.Configuration)
        .AddMessageBus(hostContext.Configuration);
    })
    .Build();

host.Run();
