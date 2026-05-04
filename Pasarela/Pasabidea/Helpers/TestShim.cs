using System;
using System.Collections;
using System.Diagnostics;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    // Shim mínimo para compilar en entornos sin MSTest.
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestMethodAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestInitializeAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestCleanupAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AssemblyInitializeAttribute : Attribute { }

    public sealed class TestContext
    {
        public IDictionary Properties { get; } = new Hashtable();
        public string FullyQualifiedTestClassName { get; set; }
        public string TestName { get; set; }

        public void WriteLine(string message) => Debug.WriteLine(message);
        public void WriteLine(string format, params object[] args) => Debug.WriteLine(string.Format(format, args));
    }
}