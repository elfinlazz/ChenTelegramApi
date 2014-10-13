using System;
using Ninject;

namespace TelegramApi.TLCore.Kernel
{
    public class CoreKernel : IDisposable
    {
        private static CoreKernel _instance;
        private IKernel _kernel;

        private CoreKernel()
        {
            _instance = this;
            _kernel = new StandardKernel();
            _kernel.Load<SerializerModule>();
        }

        private static CoreKernel Instance
        {
            get { return _instance ?? (_instance = new CoreKernel()); }
        }

        public static T Get<T>()
        {
            return Instance._kernel.Get<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_kernel != null)
                {
                    _kernel.Dispose();
                    _kernel = null;
                }
            }
        }
    }
}