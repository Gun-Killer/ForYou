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

            var middd = await zooKeeper.existsAsync("/root", new WatcherSample());
            if (middd != null)
            {
                await zooKeeper.deleteAsync("/root");
            }
            //创建个节点，数据为1111

            var res = await zooKeeper.createAsync("/root", Encoding.UTF8.GetBytes("11111"), ZooDefs.Ids.OPEN_ACL_UNSAFE,
                CreateMode.PERSISTENT);
            Console.WriteLine(res);

            //获取节点数据
            var midRes = await zooKeeper.getDataAsync("/root", new WatcherSample());
            Console.WriteLine(Encoding.UTF8.GetString(midRes.Data));

            var statts = await zooKeeper.setDataAsync(res, Encoding.UTF8.GetBytes("122"));
            Console.WriteLine(statts.getCversion());

            statts = await zooKeeper.setDataAsync(res, Encoding.UTF8.GetBytes("1223"));//watcher只会执行一次
            Console.WriteLine(statts.getCversion());


            res = await zooKeeper.createAsync("/root", null, ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL);
            Console.WriteLine(res);

            Console.WriteLine(zooKeeper.getChildrenAsync("/root", new WatcherSample()).Result.Children.Count);
            statts = await zooKeeper.setDataAsync(res, Encoding.UTF8.GetBytes("122"));
            Console.WriteLine(statts.getCversion());

            await Task.CompletedTask;
        }
    }
}
