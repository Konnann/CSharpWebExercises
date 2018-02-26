using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

[TestFixture]
public class ProviderControllerTests
{
    [Test]
    public void CheckIfFieldsArePrivate()
    {
        Type type = typeof(ProviderController);

        var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.IsTrue(fields.Length == 4);
       
        Assert.IsTrue(fields.Any(f => f.Name == "providers"));
        Assert.IsTrue(fields.Any(f => f.Name == "energyRepository"));
        Assert.IsTrue(fields.Any(f => f.Name == "factory"));
    }

    [Test]
    public void CheckFieldCount()
    {
        Type type = typeof(ProviderController);

        var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.IsTrue(fields.Length == 4);
    }

    [Test]
    public void CheckIfMethodsExist()
    {
        Type type = typeof(ProviderController);

        MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

        Assert.IsTrue(methods.Any(f => f.Name == "Register"));
        Assert.IsTrue(methods.Any(f => f.Name == "Produce"));
        Assert.IsTrue(methods.Any(f => f.Name == "Repair"));
    }

    [Test]
    public void CheckIfMethodsArePublic()
    {
        Type type = typeof(ProviderController);

        MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

        Assert.IsTrue(methods.Any(f => f.Name == "Register" && f.IsPublic));
        Assert.IsTrue(methods.Any(f => f.Name == "Produce" && f.IsPublic));
        Assert.IsTrue(methods.Any(f => f.Name == "Repair" && f.IsPublic));
    }

    [Test]
    public void CheckIfPropertiesExist()
    {
        Type type = typeof(ProviderController);

        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

        //Assert.IsTrue(methods.Length == 4);

        Assert.IsTrue(methods.Any(f => f.Name == "get_TotalEnergyProduced"));
       Assert.IsTrue(methods.Any(f => f.Name == "get_Entities"));
    }
}

