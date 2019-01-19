using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerChat
{
    class BlockingConcurrentQueue<T> : IDisposable
    {
        private ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        private Semaphore _semaphore = new Semaphore(0, int.MaxValue);

        public void Enqueue(T data)
        {
            if (data == null) return;

            _queue.Enqueue(data);
            _semaphore.Release();
        }

        public T TryDequeue()
        {
            _semaphore.WaitOne();

            T data;
            _queue.TryDequeue(out data);

            return data;
        }

        public void Dispose()
        {
            if (_semaphore != null)
            {
                _semaphore.Close();
                _semaphore = null;
            }
        }   
    }
}
