using System;
using System.Text;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace ZooKeeper.ClientSample
{
    internal class PessimisticLock
    {
        private org.apache.zookeeper.ZooKeeper _zooKeeper;

        public PessimisticLock(org.apache.zookeeper.ZooKeeper zooKeeper)
        {
            this._zooKeeper = zooKeeper;
        }


        public async Task<bool> GetLock(string key)
        {
            try
            {
                var result = await _zooKeeper.createAsync($"/root/{key}", Encoding.UTF8.GetBytes("1"), ZooDefs.Ids.OPEN_ACL_UNSAFE,
                    CreateMode.EPHEMERAL);
                return result == $"/root/{key}";

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }
    }
}
