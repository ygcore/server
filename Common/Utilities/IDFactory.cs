using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Common.Utilities
{
    public class IDFactory
    {
        private List<long> Ids = new List<long>();

        private long lastId = 0;

        public IDFactory(long lid)
        {
            lastId = lid;
            Ids.Add(lastId);
        }

        public long Register(long uid)
        {
            lock (Ids)
            {
                lastId = uid;

                while (!Ids.Contains<long>(lastId))
                {
                    if (Ids.Contains<long>(lastId))
                    {
                        Interlocked.Increment(ref lastId);
                    }
                    else
                    {
                        Ids.Add(lastId);
                        break;
                    }
                }
            }

            return lastId;
        }

        public long GetNext()
        {
            lock (Ids)
            {
                Interlocked.Increment(ref lastId);

                while (!Ids.Contains<long>(lastId))
                {
                    if (Ids.Contains<long>(lastId))
                    {
                        Interlocked.Increment(ref lastId);
                    }
                    else
                    {
                        Ids.Add(lastId);
                        break;
                    }
                }
            }

            return lastId;
        }

        public void Release(long val)
        {
            lock (Ids)
            {
                if (Ids.Contains(val))
                {
                    Ids.Remove(val);
                    lastId = val;
                }
            }
        }
    }
}
