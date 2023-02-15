using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class)]
public class RequireService : Attribute
{
    public List<Type> requiredServices = new List<Type>();
    
    public RequireService(Type requiredServiceType)
    {
        requiredServices.Add(requiredServiceType);
    }
    
    public RequireService(List<Type> requiredServiceTypes)
    {
        foreach (var element in requiredServiceTypes)
        {
            requiredServices.Add(element);
        }
    }
}
