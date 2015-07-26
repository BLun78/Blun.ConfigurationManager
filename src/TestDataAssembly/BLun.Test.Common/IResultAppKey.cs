using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blun.Test.Common
{
    public interface IResultAppKey
    {
        string Result { get; }
        string AppSettingsKey { get; }
    }
}
