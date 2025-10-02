using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.MessageBroker.MessageProducer
{
    public interface IMessageProducer
    {
        public Task SendMessageAsync(string message);
    }
}
