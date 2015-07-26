using System;
using System.Globalization;
using Blun.ConfigurationManager.ServiceModel;
using Blun.Test.Common;
using Blun.Test1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blun.ConfigurationManager.Test
{
    [TestClass]
    public class TestConfigurationManager
    {
        [TestMethod]
        public void Test1_Test1Result_Ok()
        {
            IResultAppKey expected = new Test1_ResultAppKey();

            var conf = new ConfigurationManager(ConfigurationManager.GetAssemblyPath(typeof(Test1_ResultAppKey)));
            var result = conf.AppSettings.Settings[expected.AppSettingsKey];

            Assert.AreEqual(expected.Result, result.Value);
        }

        [TestMethod]
        public void Test1_Test1Result_Null()
        {
            IResultAppKey expected = new Test1_ResultAppKey();

            var conf = new ConfigurationManager(ConfigurationManager.GetAssemblyPath(typeof(TestConfigurationManager)));
            var result = conf.AppSettings.Settings[expected.AppSettingsKey];

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test1_Test2Result_Ok()
        {
            IResultConnectionString expected = new Test1_ResultConnectionString();

            var conf = new ConfigurationManager(ConfigurationManager.GetAssemblyPath(typeof(Test1_ResultAppKey)));
            var result = conf.ConnectionStrings.ConnectionStrings[expected.ConnectionStringName];

            Assert.AreEqual(expected.Result, result.ConnectionString);
        }

        [TestMethod]
        public void Test1_Test2Result_Null()
        {
            IResultConnectionString expected = new Test1_ResultConnectionString();

            var conf = new ConfigurationManager(ConfigurationManager.GetAssemblyPath(typeof(TestConfigurationManager)));
            var result = conf.ConnectionStrings.ConnectionStrings[expected.ConnectionStringName];

            Assert.IsNull(result);
        }

        private static string ExpectedFormation(int value)
        {
            return string.Format(CultureInfo.InvariantCulture, "You entered: {0}", value);
        }

        [TestMethod]
        public void Test2_Common_IServiceContract_Ok()
        {
            var value = 5;
            var expeced = ExpectedFormation(value);

            var channelFactory = new Blun.ConfigurationManager.ServiceModel.ChannelFactory<Blun.Test.Common.IService1>(@"Test_Common_IService1");
            var service = channelFactory.CreateChannel();
            var result = service.GetData(value);

            Assert.AreEqual(expeced, result);
        }

        [TestMethod]
        public void Test2_Common_IServiceContract_NO_Endpoint()
        {
            var value = 5;
            var expeced = ExpectedFormation(value);
            var thrownException = false;
            string result = null;
            string source = null;

            try
            {
                var channelFactory = new Blun.ConfigurationManager.ServiceModel.ChannelFactory<Blun.Test.Common.IService1>(@"Test_Common_IService1_Fail");
                var service = channelFactory.CreateChannel();
                result = service.GetData(value);
            }
            catch (InvalidOperationException ex)
            {
                thrownException = true;
                source = ex.Source;
            }

            Assert.AreEqual("System.ServiceModel", source);
            Assert.IsNull(result);
            Assert.IsTrue(thrownException);
        }

        [TestMethod]
        public void Test2_Test1_IServiceProxy_Ok()
        {
            var value = 3;
            var expeced = ExpectedFormation(value);

            var channelFactory = new Blun.ConfigurationManager.ServiceModel.ChannelFactory<Blun.Test1.Service1Proxy.IService1>(@"Test_Proxy_IService1");
            var service = channelFactory.CreateChannel();
            var result = service.GetData(value);

            Assert.AreEqual(expeced, result);
        }

        [TestMethod]
        public void Test2_Test1_IServiceProxy_NO_Endpoint()
        {
            var value = 3;
            var expeced = ExpectedFormation(value);
            var thrownException = false;
            string result = null;
            string source = null;

            try
            {
                var channelFactory = new Blun.ConfigurationManager.ServiceModel.ChannelFactory<Blun.Test1.Service1Proxy.IService1>(@"Test_Common_IService1_Fail");
                var service = channelFactory.CreateChannel();
                result = service.GetData(value);
            }
            catch (InvalidOperationException ex)
            {
                thrownException = true;
                source = ex.Source;
            }

            Assert.AreEqual("System.ServiceModel", source);
            Assert.IsNull(result);
            Assert.IsTrue(thrownException);
        }

        [TestMethod]
        public void Test2_ConfigurationManager_IServiceContract_Ok()
        {
            var value = 5;
            var expeced = ExpectedFormation(value);

            var channelFactory = new Blun.ConfigurationManager.ServiceModel.ChannelFactory<Blun.Test.Common.IService1>(@"Test_ConfigurationManager_IService1", this.GetType());
            var service = channelFactory.CreateChannel();
            var result = service.GetData(value);

            Assert.AreEqual(expeced, result);
        }

        [TestMethod]
        public void Test2_ConfigurationManager_IServiceContract_NO_Endpoint()
        {
            var value = 5;
            var expeced = ExpectedFormation(value);
            var thrownException = false;
            string result = null;
            string source = null;

            try
            {
                var channelFactory = new Blun.ConfigurationManager.ServiceModel.ChannelFactory<Blun.Test.Common.IService1>(@"Test_ConfigurationManager_IService1_Fail", this.GetType());
                var service = channelFactory.CreateChannel();
                result = service.GetData(value);
            }
            catch (InvalidOperationException ex)
            {
                thrownException = true;
                source = ex.Source;
            }

            Assert.AreEqual("System.ServiceModel", source);
            Assert.IsNull(result);
            Assert.IsTrue(thrownException);
        }
    }
}
