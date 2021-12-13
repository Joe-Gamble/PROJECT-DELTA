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
            //UnityEngine.Debug.Log("Initalising Items of Type: " + factory_type.ToString());

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

    
    public static class Manager
    {
        public static T GetItem<T>(string itemName)
        {
            Type itemType = typeof(T);

            if(itemType.IsSubclassOf(typeof(Weapon)))
            {
                return Weapons.GetItem<T>(itemName);
            }
            throw new Exception("Object of type  " + typeof(T).Name + "is not an Item that is produced by the Factory. ");
        }

        public static List<T> GetAllItemsOfType<T>()
        {
            Type itemType = typeof(T);
            List<T> items = new List<T>();

            if(itemType.IsSubclassOf(typeof(Weapon)) || itemType == typeof(Weapon))
            {
                foreach(Type tp in Weapons.typesByName.Values)
                {
                    if(tp.IsSubclassOf(typeof(T)))
                    {
                        items.Add((T) Activator.CreateInstance(tp));
                    }
                }
                return items;
            }
            else
            {
                throw new Exception("Could not find a factory that supports Type of " + typeof(T).Name);
            }
        }

        private static List<Type> GetAllItemsOfType(Type _type)
        {
            List<Type> items = new List<Type>();

            if(_type.IsSubclassOf(typeof(Weapon)))
            {
                foreach(Type tp in Weapons.typesByName.Values)
                {
                    if(tp.IsSubclassOf((_type)))
                    {
                        items.Add(tp);
                    }
                }
                return items;
            }
            else
            {
                throw new Exception("Could not find a factory that supports Type of" + _type.Name);
            }
        }

        ///<summary> "Returns a list of names from given type filter" </summary>
        public static IEnumerable<string> GetItemNames<T>()
        {
            Type itemType = typeof(T);

            if(itemType == typeof(Weapon))
            {
                return Weapons.typesByName.Keys;
            }
            throw new Exception("Type: " + typeof(T).Name + " is not supported by factories");
        }

        public static List<T> GetCollection<T>(Type[] types, Type[] interfaces)
        {
            List<T> collection = new List<T>();
            List<T> found_types = new List<T>();

            foreach(Type type in types)
            {
                if(type.IsSubclassOf(typeof(T)) || type == typeof(T))
                {
                    foreach(T tp in GetAllItemsOfType<T>())
                    {
                        found_types.Add(tp);
                    }
                }
            }

            foreach(T type in found_types.ToList())
            {
                foreach(Type itf in interfaces)
                {
                    Type tp = type.GetType();
                    if(tp.GetInterfaces().Contains(itf))
                    {
                        continue;
                    }
                    else found_types.Remove(type);
                }
            }

            collection = found_types;
            return collection;
        }
    }

    public static class Weapons
    {
        public static Dictionary<string, Type> typesByName;
        public static bool IsInitialised = typesByName != null;
        
        public static T GetItem<T>(string itemType)
        {
            if(!IsInitialised)
            {
                typesByName = TypeGetter.InitaliseFactoryInfo(typeof(Weapon));
            }

            if(typesByName.ContainsKey(itemType))
            {
                return (T) Activator.CreateInstance(typeof(T));
            }

            throw new Exception("Couldn't find object of type " + typeof(T).Name + " with name " + itemType);
        }
    }
}


