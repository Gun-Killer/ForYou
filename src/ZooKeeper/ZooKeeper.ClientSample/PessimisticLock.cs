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
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                var result = await _zooKeeper.createAsync($"/PessimisticLock/{key}", Encoding.UTF8.GetBytes(key), ZooDefs.Ids.OPEN_ACL_UNSAFE,
                    CreateMode.EPHEMERAL);//临时node 不能有children node
                return result == $"/PessimisticLock/{key}";

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        public async Task<bool> ReleaseLock(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var sta = await _zooKeeper.existsAsync($"/PessimisticLock/{key}", new WatcherSample());
            if (sta == null) return true;

            var lockData = await _zooKeeper.getDataAsync($"/PessimisticLock/{key}", new WatcherSample());

            var mark = Encoding.UTF8.GetString(lockData.Data) == key;
            if (mark)
            {
                await _zooKeeper.deleteAsync($"/PessimisticLock/{key}", sta.getVersion());
            }

            return true;
        }
    }
}
