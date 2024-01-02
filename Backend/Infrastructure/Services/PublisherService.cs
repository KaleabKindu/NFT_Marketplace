using Application.Events;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Application.Contracts.Services;

public class PublisherService: BackgroundService
{
    private readonly Web3 _web3;
    private readonly RabbitMqService _messageQueue;
    private readonly IConfiguration _configuration;

    public PublisherService(RabbitMqService messageQueue, IConfiguration configuration){
        _configuration = configuration;
        _web3 = new Web3();
        _messageQueue = messageQueue;
    }

    public async Task Listen(CancellationToken stoppingToken)
    {
        // var contractAddress = _configuration.GetSection("Ethereum").GetSection("ContractAddress").Value;
        var contractAddress = _configuration["Ethereum:ContractAddress"];
        var contract = _web3.Eth.GetContract(_configuration["Ethereum:Abi"], contractAddress);

        var valueSetEvent = contract.GetEvent<ValueSetEvent>();
        while (!stoppingToken.IsCancellationRequested)
        {
            var eventFilter = await valueSetEvent.CreateFilterAsync();
            FilterLog[] logs1 = await _web3.Eth.Filters.GetFilterChangesForEthNewFilter.SendRequestAsync(eventFilter);
            List<EventLog<ValueSetEvent>> events = valueSetEvent.DecodeAllEventsForEvent(logs1);
            
            // Dispatch events to the message queue
            DispatchEventsToQueue(events.Select(evnt => evnt.Event).ToArray());


            await Task.Delay(5000); // Add a delay to avoid excessive API calls
        }
    }

    private void DispatchEventsToQueue<TEvent>(TEvent[] events)
    {
        foreach(TEvent evt in events){
            _messageQueue.EnqueueAsync(evt);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Listen(stoppingToken);
    }
}
