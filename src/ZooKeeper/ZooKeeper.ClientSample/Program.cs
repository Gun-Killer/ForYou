using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace ZooKeeper.ClientSample
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {

            org.apache.zookeeper.ZooKeeper zooKeeper = new org.apache.zookeeper.ZooKeeper("127.0.0.1:2181", 5000, new WatcherSample());

            var res = await zooKeeper.createAsync("/node1", Encoding.UTF8.GetBytes("1"), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL);
            Console.WriteLine(res);

            res = await zooKeeper.createAsync("/node2", Encoding.UTF8.GetBytes("1"), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL);
            Console.WriteLine(res);

            res = await zooKeeper.createAsync("/PessimisticLock", Encoding.UTF8.GetBytes("1"), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
            Console.WriteLine(res);

            PessimisticLock lLock = new PessimisticLock(zooKeeper);
            Console.WriteLine(await lLock.GetLock("4"));

            Console.WriteLine(await lLock.ReleaseLock("4"));

            List<Task> tasks = new List<Task>(5);
            for (int i = 0; i < 5; i++)
            {
                tasks.Add(new Task(() =>
              {
                  var lockRes = lLock.GetLock("4").Result;
                  Console.WriteLine($"{Thread.CurrentThread.Name} get  lock {lockRes}");

              }));
            }

            foreach (var task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks.ToArray());

            await Task.CompletedTask;
        }
    }
}
