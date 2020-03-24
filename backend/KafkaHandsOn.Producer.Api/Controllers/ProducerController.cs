﻿using System;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;


namespace KafkaHandsOn.Producer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducerController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public IActionResult Post([FromQuery] string msg)
        {
            return Created("", SendMessageByKafka(msg));
        }

        private string SendMessageByKafka(string message)
        {
            var config = new ProducerConfig {BootstrapServers = "broker:29092"};
            
            using(var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var sendResult = producer
                                        .ProduceAsync("fila_pedido", new Message<Null,string> {Value = message})
                                            .GetAwaiter()
                                                .GetResult();
                    return $"Mensagem '{sendResult.Value}' de '{sendResult.TopicPartitionOffset}'";
                }
                catch(ProduceException<Null,string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
            return string.Empty;
        }
    }
}
