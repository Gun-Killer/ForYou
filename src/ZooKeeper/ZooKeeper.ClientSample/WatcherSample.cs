using System;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace ZooKeeper.ClientSample
{
    internal class WatcherSample : Watcher
    {
        /// <inheritdoc />
        public override Task process(WatchedEvent @event)
        {
            Console.WriteLine(@event.getState());
            return Task.CompletedTask;
        }
    }
}
