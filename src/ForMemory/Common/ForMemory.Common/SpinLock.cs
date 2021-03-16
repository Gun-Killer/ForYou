using System.Threading;

namespace ForMemory.Common
{
    public class SpinLock
    {
        private volatile int _lock;

        public void Lock()
        {
            while (Interlocked.CompareExchange(ref _lock, 1, 0) != 0)
            {
                ;
            }
        }

        public void Release()
        {
            _lock = 0;
        }
    }
}
