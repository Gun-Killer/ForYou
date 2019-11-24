using System;
using System.Text;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace ZooKeeper.ClientSample
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {

            org.apache.zookeeper.ZooKeeper zooKeeper = new org.apache.zookeeper.ZooKeeper("127.0.0.1:2181", 5000, new WatcherSample());

            //创建个节点，数据为1111
            var res = await zooKeeper.createAsync("/root", Encoding.UTF8.GetBytes("11111"), ZooDefs.Ids.OPEN_ACL_UNSAFE,
                CreateMode.EPHEMERAL);
            Console.WriteLine(res);

            //获取节点数据
            var midRes = await zooKeeper.getDataAsync("/root");
            Console.WriteLine(Encoding.UTF8.GetString(midRes.Data));

            await Task.CompletedTask;
        }
    }
}
