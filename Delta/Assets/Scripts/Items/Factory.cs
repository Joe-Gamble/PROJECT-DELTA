using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Factory
{
    public struct FactoryInfo
    {
        public Dictionary<string, Type> typesByName;
        public bool IsInitialised => typesByName != null;
    }

    public static class TypeGetter
    {
        public static Dictionary<string, Type> InitaliseFactoryInfo(Type factory_type)
        {
            UnityEngine.Debug.Log("Initalising Items of Type: " + factory_type.ToString());

            var factoryTypes = Assembly.GetAssembly(factory_type).GetTypes().Where(
                myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(factory_type));

            Dictionary<string, Type> typesByName = new Dictionary<string, Type>();

            foreach(var type in factoryTypes)
            {
                var tempEffect = Activator.CreateInstance(type) as SMG;

                if(tempEffect.data != null)
                {
                    typesByName.Add(tempEffect.data.item_name, type);
                }
            }

            return typesByName;
        }
    }

    public static class WeaponFactory
    {
        public static Dictionary<string, Type> typesByName;
        public static bool IsInitialised => typesByName != null;

        public static dynamic GetWeapon<T>(string weaponType)
        {
            if(!IsInitialised)
            {
                typesByName = TypeGetter.InitaliseFactoryInfo(typeof(Weapon));
            }

            if(typesByName.ContainsKey(weaponType))
            {
                return (T) Activator.CreateInstance(typeof(T));
            }

            throw new Exception("Couldn't find object of type " + typeof(T).Name + " with name " + weaponType);
        }

        internal static IEnumerable<string> GetWeaponNames()
        {
            typesByName = TypeGetter.InitaliseFactoryInfo(typeof(Weapon));
            return WeaponFactory.typesByName.Keys;
        }

    }
}


