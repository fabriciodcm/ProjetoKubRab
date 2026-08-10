using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectKubRab.Worker.Infrastructure.Messaging
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; } = string.Empty;
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool AutomaticRecoveryEnabled { get; set; } = true;
    }
}
