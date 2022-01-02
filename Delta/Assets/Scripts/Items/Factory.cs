using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class TypeGetter
{
    public static Dictionary<string, Type> InitaliseFactoryInfo(Type factory_type)
    {
        var factoryTypes = Assembly.GetAssembly(factory_type).GetTypes().Where(
            myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(factory_type));

        Dictionary<string, Type> typesByName = new Dictionary<string, Type>();

        foreach (var type in factoryTypes)
        {
            var tempEffect = Activator.CreateInstance(type) as SMG;

            if (tempEffect.data != null)
            {
                typesByName.Add(tempEffect.data.item_name, type);
            }
        }

        return typesByName;
    }
}

public static class Spawner
{
    //Thinking about Spawn functionality, maybe I need methods later on to support bounds? For mob spawners, loot drops, etc

    /// <summary>
    /// Spawns a factory Item at a location
    /// </summary>
    /// <param name="pos"> The position where it will be spawn </param>
    /// <param name="item"> The ItemData of the object being spawned </param>
    /// <returns> The Gameobject spawned </returns>
    public static GameObject Spawn(Transform pos, PhysicalItem item)
    {
        if (item.data.type != ItemTypes.UNDEFINED)
        {
            GameObject obj = GameObject.Instantiate(item.data.prefab, pos.position, pos.rotation);

            obj.name = item.data.name;
            ItemRef ir = obj.AddComponent<ItemRef>();
            ir.Init(obj, item);

            return obj;
        }
        throw new Exception("Item Undefined, Could not Spawn!");
    }

    /// <summary>
    /// Spawns a factory Item at location <paramref name="pos"/> with data from <paramref name="item"/>. 
    /// </summary>
    /// <param name="pos"> The position where it will be spawn </param>
    /// <param name="item"> The ItemData of the object being spawned </param>
    /// <param name="parent"> Determines if the item will be rooted to transform its spawned on </param>
    /// <param name="isStatic"> Does the item spawn with physics </param>
    /// <returns> The Gameobject spawned </returns>
    public static GameObject Spawn(Transform pos, PhysicalItem item, bool parent, bool isStatic)
    {
        if (item.data.type != ItemTypes.UNDEFINED)
        {
            GameObject obj;

            if (parent)
            {
                obj = GameObject.Instantiate(item.data.prefab, pos);
            }
            else
            {
                obj = GameObject.Instantiate(item.data.prefab, pos.position, pos.rotation);
            }

            if (!isStatic)
            {
                obj.AddComponent<Rigidbody>();
            }

            MeshCollider[] mcs = obj.GetComponentsInChildren<MeshCollider>();

            foreach (MeshCollider mc in mcs)
            {
                if (isStatic)
                {
                    mc.enabled = false;
                }
                else
                {
                    mc.convex = true;
                }
            }

            obj.name = item.data.name;
            ItemRef ir = obj.AddComponent<ItemRef>();
            ir.Init(obj, item);

            return obj;
        }
        throw new Exception("Item Undefined, Could not Spawn!");
    }
}

public static class Factory
{
    /* EXAMPLES
        //GET SPECIFIC TYPE FROM NAME
        SMG smg = Factory.GetItem<SMG>("SMG");

         //FIND ALL TYPES OF A TYPE
        List<Gun> AllGuns = Factory.GetAllItemsOfType<Gun>();

        //FIND TYPES WITH SPECIFIC BEHAVIOUS
        Type[] types = {typeof(Gun), (typeof(Melee))}; 
        Type[] interfaces = {typeof(IReloadable), typeof(IShootable), typeof(ISwingable)};

        //USING DEFINED COLLECTIONS FOR TYPES AND BEHAVIOURS; RETURNS A LIST OF ALL TYPES FOUND
        List<Weapon> FoundWeapons = Factory.GetSpecificCollection<Weapon>(types,interfaces);
    */

    public static T GetItem<T>(string itemName)
    {
        Type itemType = typeof(T);

        if (itemType.IsSubclassOf(typeof(Weapon)))
        {
            return WeaponFactory.GetItem<T>(itemName);
        }
        throw new Exception("Object of type " + typeof(T).Name + " is not an Item that is produced by the Factory. ");
    }

    public static List<T> GetAllItemsOfType<T>()
    {
        Type itemType = typeof(T);
        List<T> items = new List<T>();

        if (itemType.IsSubclassOf(typeof(Weapon)) || itemType == typeof(Weapon))
        {
            foreach (Type tp in WeaponFactory.typesByName.Values)
            {
                if (tp.IsSubclassOf(typeof(T)))
                {
                    items.Add((T)Activator.CreateInstance(tp));
                }
            }
            return items;
        }
        else
        {
            throw new Exception("Could not find a factory that supports Type of " + typeof(T).Name);
        }
    }

    ///<summary> "Returns a list of names from given type filter" </summary>
    public static IEnumerable<string> GetItemNames<T>()
    {
        Type itemType = typeof(T);

        if (itemType == typeof(Weapon))
        {
            return WeaponFactory.typesByName.Keys;
        }
        throw new Exception("Type: " + typeof(T).Name + " is not supported by factories");
    }

    ///<summary> Returns a list of Type T where each instance is a subclass of at least one of the given <paramref name="types"/>.</summary>
    ///<param name="types"> Specified subtypes of T </param>
    public static List<T> GetSpecificCollection<T>(Type[] types)
    {
        List<T> found_types = new List<T>();

        foreach (Type type in types)
        {
            if (type.IsSubclassOf(typeof(T)) || type == typeof(T))
            {
                foreach (T tp in GetAllItemsOfType<T>())
                {
                    found_types.Add(tp);
                }
            }
        }
        return found_types;
    }

    ///<summary> Returns a list of Type T where each instance is a subclass of at least one of the given <paramref name="types"/> and at least one of the given <paramref name="interfaces"/>.</summary>
    ///<param name="types"> Specified subtypes of T </param>
    ///<param name="interfaces"> Specified interfaces of T </param>
    public static List<T> GetSpecificCollection<T>(Type[] types, Type[] interfaces)
    {
        List<T> collection = new List<T>();
        List<T> found_types = new List<T>();

        foreach (Type type in types)
        {
            foreach (Type tp in GetAllItemsOfType(type))
            {
                if (tp.IsSubclassOf(typeof(T)) || type == typeof(T))
                {
                    T t = (T)Activator.CreateInstance(tp);
                    if (!found_types.Contains(t))
                    {
                        found_types.Add((T)Activator.CreateInstance(tp));
                    }
                }
            }
        }

        foreach (T type in found_types)
        {
            foreach (Type itf in interfaces)
            {
                Type tp = type.GetType();
                if (tp.GetInterfaces().Contains(itf))
                {
                    collection.Add(type);
                    break;
                }
            }
        }
        return collection;
    }

    private static List<Type> GetAllItemsOfType(Type _type)
    {
        List<Type> items = new List<Type>();

        if (_type.IsSubclassOf(typeof(Weapon)))
        {
            foreach (Type tp in WeaponFactory.typesByName.Values)
            {
                if (tp.IsSubclassOf((_type)))
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
}

internal static class WeaponFactory
{
    public static Dictionary<string, Type> typesByName;
    public static bool IsInitialised = typesByName != null;

    public static T GetItem<T>(string itemType)
    {
        if (!IsInitialised)
        {
            typesByName = TypeGetter.InitaliseFactoryInfo(typeof(Weapon));
        }

        if (typesByName.ContainsKey(itemType))
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        throw new Exception("Couldn't find object of type " + typeof(T).Name + " with name " + itemType);
    }
}



