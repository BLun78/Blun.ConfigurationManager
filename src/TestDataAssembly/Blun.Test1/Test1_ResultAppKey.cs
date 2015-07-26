using System;
using Blun.Test.Common;

namespace Blun.Test1
{
    public class Test1_ResultAppKey : IResultAppKey
    {
        public string Result => "Hallo";
        public string AppSettingsKey => "test1";

    }
}
