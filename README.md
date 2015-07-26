# Blun.ConfigurationManager
Blun.ConfigurationManager help for libray configurations app.config, etc.

## Getting Started ##

Load the App.config from ClassLibrary 'OtherClassLibrarayWithAppConfig' in the Application 'AppWithWebConfig'.
```C#
using OtherClassLibrarayWithAppConfig = OtherClassLibrarayWithAppConfig.Demo;

namespace AppWithWebConfig 
{
  public class Demo
  {
    public Demo()
    {
      var config = new ConfigurationManager(ConfigurationManager.GetAssemblyPath(typeof(OtherClassLibrarayWithAppConfig)));
      KeyValueConfigurationElement element = config.AppSettings.Settings["Getting_Started"];
    }
  }
}

```
