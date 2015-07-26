# Blun.ConfigurationManager
ConfigurationManager for using App.config and Web.config of different assemblys. It have a Wcf.ChannelFactory wich use the ConfigurationManager.

## Getting Started ##
Load the App.config from ClassLibrary 'OtherClassLibrarayWithAppConfig' in the Application 'AppWithWebConfig'.
```C#
using System.Configuration;
using OtherClassLibrarayWithAppConfig = OtherClassLibrarayWithAppConfig.Demo;

namespace AppWithWebConfig 
{
  public class Demo
  {
    public Demo()
    {
      var config = new ConfigurationManager(
                ConfigurationManager.GetAssemblyPath(
                                    typeof(OtherClassLibrarayWithAppConfig)));
                                    
      KeyValueConfigurationElement element = 
                config.AppSettings.Settings["Getting_Started"];
    }
  }
}

```

Now create a WCF Channel from Assembly App.config. MS default Example
```C#
using ChannelFactory = Blun.ConfigurationManager.ServiceModel.ChannelFactory;

  public string GetData(int value)
  {
    // Loads the App.config of the Assembly of the Type 'Contract.IService1'
    var channelFactory = 
        new ChannelFactory<Contract.IService1>(@"Contract_IService1");
    var service = channelFactory.CreateChannel();
    var result = service.GetData(value);
    service.Close()
  }
  
  public string GetDataSecond(int value)
  {
    // Loads the App.config of the Assembly of the Type 'Proxy.Service1Client'
    var channelFactory = 
        new ChannelFactory<Contract.IService1>(@"Proxy_IService1", 
                                              typeof(Proxy.Service1Client));
    var service = channelFactory.CreateChannel();
    var result = service.GetData(value);
    service.Close()
  }
  
   public string GetDataThird(int value)
  {
    // Loads the App.config of the calling Assembly
    var channelFactory = 
        new ChannelFactory<Contract.IService1>(@"Proxy_IService1", 
                                              this.GetType());
    var service = channelFactory.CreateChannel();
    var result = service.GetData(value);
    service.Close()
  }
             
```
