using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace ZooKeeper.ClientSample
{
    public class PessimisticLockV2
    {
        private readonly org.apache.zookeeper.ZooKeeper _zooKeeper;

        public PessimisticLockV2(org.apache.zookeeper.ZooKeeper zooKeeper)
        {
            _zooKeeper = zooKeeper;
            var node = _zooKeeper.existsAsync("/PessimisticLockV2").Result;
            if (node == null)
            {
                Task.WaitAll(_zooKeeper.createAsync("/PessimisticLockV2", null, ZooDefs.Ids.OPEN_ACL_UNSAFE,
                    CreateMode.PERSISTENT));
            }

        }

        /// <summary>
        /// //
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        public async Task<bool> GetLock(string key)
        {
            var node = await _zooKeeper.createAsync($"/PessimisticLockV2/LOCK", Encoding.UTF8.GetBytes("1"),
                ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT_SEQUENTIAL);
            var children = await _zooKeeper.getChildrenAsync("/PessimisticLockV2");
            var minNode = children.Children.OrderBy(t => t).First();

            var res = ("/PessimisticLockV2/" + minNode).Equals(node);
            if (res)
            {
                await _zooKeeper.deleteAsync(node);//releaseLock
                return true;
            }
            return false;
        }
    }
}