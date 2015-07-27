using System;
using System.ServiceModel;

namespace Blun.ConfigurationManager.ServiceModel
{
    public class ClientBase<TChannel> :
                                ICommunicationObject,
                                IDisposable
                                where TChannel : class
    {
        public TChannel Channel { get; private set; }

        private readonly ChannelFactory<TChannel> _channelFactory;

        protected ClientBase()
        {
            _channelFactory = new ChannelFactory<TChannel>("*");
            Channel = _channelFactory.CreateChannel();
            InitEvents();
        }

        protected ClientBase(string endpointConfigurationName)
        {
            _channelFactory = new ChannelFactory<TChannel>(endpointConfigurationName);
            Channel = _channelFactory.CreateChannel();
            InitEvents();
        }


        private void InitEvents()
        {
            _channelFactory.Closed += Closed;
            _channelFactory.Closing += Closing;
            _channelFactory.Opened += Opened;
            _channelFactory.Opening += Opening;
            _channelFactory.Faulted += Faulted;
        }

        public void Abort()
        {
            _channelFactory.Abort();
        }

        public void Close()
        {
            _channelFactory.Close();
        }

        public void Close(TimeSpan timeout)
        {
            _channelFactory.Close(timeout);
        }

        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            return _channelFactory.BeginClose(callback, state);
        }

        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return _channelFactory.BeginClose(timeout, callback, state);
        }

        public void EndClose(IAsyncResult result)
        {
            _channelFactory.EndClose(result);
        }

        public void Open()
        {
            _channelFactory.Open();
        }

        public void Open(TimeSpan timeout)
        {
            _channelFactory.Open(timeout);
        }

        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            return _channelFactory.BeginOpen(callback, state);
        }

        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return _channelFactory.BeginOpen(timeout, callback, state);
        }

        public void EndOpen(IAsyncResult result)
        {
            _channelFactory.EndOpen(result);
        }

        public CommunicationState State => _channelFactory.State;

        public event EventHandler Closed;
        public event EventHandler Closing;
        public event EventHandler Faulted;
        public event EventHandler Opened;
        public event EventHandler Opening;

        void IDisposable.Dispose()
        {
            this.Close();
        }
    }
}
