using Blun.Test.Common;

namespace Blun.Test1
{
    public class Test1_ResultConnectionString : IResultConnectionString
    {
        public string Result => "Hallo";
        public string ConnectionStringName => "test1";
    }
}