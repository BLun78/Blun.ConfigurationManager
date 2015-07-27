using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using ConfigurationManager = Blun.ConfigurationManager.ConfigurationManager;

namespace Blun.ConfigurationManager.ServiceModel
{
    public class ChannelFactory<TChannel>
        :   IDisposable,
            IChannelFactory<TChannel>,
            IChannelFactory,
            ICommunicationObject
    {

        private readonly IChannelFactory<TChannel> _configurationChannelFactory;
        private readonly string _exePath;

        public static string GetAssemblyPath(Type assemblyType)
        {
            return assemblyType.Assembly.Location;
        }

        public ChannelFactory(string endpointConfigurationName)
            : this(endpointConfigurationName, (EndpointAddress)null, (Configuration)null, ChannelFactory<TChannel>.GetAssemblyPath(typeof(TChannel)))
        {
        }

        public ChannelFactory(string endpointConfigurationName, Type assemblyType)
            : this(endpointConfigurationName, (EndpointAddress)null, (Configuration)null, ChannelFactory<TChannel>.GetAssemblyPath(assemblyType))
        {
        }

        public ChannelFactory(string endpointConfigurationName, string exePath)
            : this(endpointConfigurationName, (EndpointAddress)null, (Configuration)null, exePath)
        {
        }

        public ChannelFactory(string endpointConfigurationName, Configuration configuration)
           : this(endpointConfigurationName, (EndpointAddress)null, configuration, (string)null)
        {
        }

        public ChannelFactory(string endpointConfigurationName, EndpointAddress remoteAddress, Type assemblyType)
            : this(endpointConfigurationName, remoteAddress, (Configuration)null, ChannelFactory<TChannel>.GetAssemblyPath(assemblyType))
        {
        }

        public ChannelFactory(string endpointConfigurationName, EndpointAddress remoteAddress, string exePath)
            : this(endpointConfigurationName, remoteAddress, (Configuration)null, exePath)
        {
        }

        public ChannelFactory(string endpointConfigurationName, EndpointAddress remoteAddress, Configuration configuration)
               : this(endpointConfigurationName, remoteAddress, configuration, (string)null)
        {
        }

        protected ChannelFactory(string endpointConfigurationName, EndpointAddress remoteAddress, Configuration configuration, string exePath)
        {
            var config = default(Configuration);
            if (string.IsNullOrWhiteSpace(exePath) && configuration == null)
            {
                _exePath = ChannelFactory<TChannel>.GetAssemblyPath(typeof(TChannel));
            }
            else
            {
                _exePath = exePath;
            }
            if (!string.IsNullOrWhiteSpace(_exePath) && configuration == null)
            {
                config = new ConfigurationManager(_exePath).Configuration;
            }
            else if (configuration != default(Configuration))
            {
                config = configuration;
            }

            if (config == default(Configuration)) throw new ArgumentNullException(nameof(configuration));

            _configurationChannelFactory = new ConfigurationChannelFactory<TChannel>(endpointConfigurationName,
                                                                                    config,
                                                                                    remoteAddress);

            _configurationChannelFactory.Closed += Closed;
            _configurationChannelFactory.Closing += Closing;
            _configurationChannelFactory.Faulted += Faulted;
            _configurationChannelFactory.Opened += Opened;
            _configurationChannelFactory.Opening += Opening;
        }

        public void Abort()
        {
            _configurationChannelFactory.Abort();
        }

        public void Close()
        {
            _configurationChannelFactory.Close();
        }

        public void Close(TimeSpan timeout)
        {
            _configurationChannelFactory.Close(timeout);
        }

        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            return _configurationChannelFactory.BeginClose(callback, state);
        }

        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return _configurationChannelFactory.BeginClose(timeout, callback, state);
        }

        public void EndClose(IAsyncResult result)
        {
            _configurationChannelFactory.EndClose(result);
        }

        public void Open()
        {
            _configurationChannelFactory.Open();
        }

        public void Open(TimeSpan timeout)
        {
            _configurationChannelFactory.Open(timeout);
        }

        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            return _configurationChannelFactory.BeginOpen(callback, state);
        }

        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return _configurationChannelFactory.BeginOpen(timeout, callback, state);
        }

        public void EndOpen(IAsyncResult result)
        {
            _configurationChannelFactory.EndOpen(result);
        }

        public CommunicationState State => _configurationChannelFactory.State;

        public event EventHandler Closed;
        public event EventHandler Closing;
        public event EventHandler Faulted;
        public event EventHandler Opened;
        public event EventHandler Opening;

        public T GetProperty<T>() where T : class
        {
            return _configurationChannelFactory.GetProperty<T>();
        }

        public TChannel CreateChannel()
        {
            return ((System.ServiceModel.ChannelFactory<TChannel>)_configurationChannelFactory).CreateChannel();
        }

        public TChannel CreateChannel(EndpointAddress to)
        {
            return _configurationChannelFactory.CreateChannel(to);
        }

        public TChannel CreateChannel(EndpointAddress to, Uri via)
        {
            return _configurationChannelFactory.CreateChannel(to, via);
        }

        void IDisposable.Dispose()
        {
            ((IDisposable)_configurationChannelFactory).Dispose();
        }
    }
}
