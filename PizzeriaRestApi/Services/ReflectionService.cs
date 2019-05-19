﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PizzeriaRestApi.Services
{
    public class ReflectionService
    {
        public List<string> GetInfoByAssembly()
        {
            List<string> infoes = new List<string>();
            Assembly assembly = GetAssembly("PizzeriaServiceDAL");
            List<Type> interfaces = GetInterfases(assembly);
            foreach (var inter in interfaces)
            {
                CustomAttributeData attr = inter.CustomAttributes.FirstOrDefault(x =>
               x.AttributeType.Name == "UsageInterfaceAttribute");
                if (attr != null)
                {
                    infoes.Add(string.Format("Интерфейс: {0}. {1}", inter.Name,
                   attr.ConstructorArguments[0].Value));
                    infoes.AddRange(GetMethodInfoes(inter));
                }
            }
            return infoes;
        }
        private Assembly GetAssembly(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }
        private List<Type> GetInterfases(Assembly assembly)
        {
            return assembly.GetTypes().Where(x => x.IsInterface).ToList();
        }
        private List<string> GetMethodInfoes(Type inter)
        {
            List<string> infoes = new List<string>();
            MethodInfo[] methods = inter.GetMethods();
            for (int i = 0; i < methods.Length; ++i)
            {
                CustomAttributeData attr = methods[i].CustomAttributes.FirstOrDefault(x
               => x.AttributeType.Name == "UsageMethodAttribute");
                if (attr != null)
                {
                    infoes.Add(string.Format("--> Метод: {0}. {1}", methods[i].Name,
                   attr.ConstructorArguments[0].Value));
                }
            }
            return infoes;
        }
    }
}