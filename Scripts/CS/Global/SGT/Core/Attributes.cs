using System;

namespace SGT;

[AttributeUsage(AttributeTargets.Class)]
public class TestClass : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class TestMethod : Attribute { }