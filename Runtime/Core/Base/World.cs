using System;
using System.Collections.Generic;
using UnityEngine;

namespace andywiecko.PBD2D.Core
{
    // TODO: check if we can reduce the abstraction
    public interface IWorld
    {
        ISimulationConfiguration Configuration { get; }
        IComponentsRegistry ComponentsRegistry { get; }
        ISystemsRegistry SystemsRegistry { get; }
    }

    public class World : MonoBehaviour, IWorld
    {
        private static List<(Type t, Type t1, Type t2)> tupleTypes = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(ComponentsTuple).IsAssignableFrom(type) && type.IsAbstract == false)
                    {
                        var baseType = type.BaseType;
                        var args = baseType.GetGenericArguments();
                        var tuple = (type, args[0], args[1]);
                        tupleTypes.Add(tuple);
                    }
                }
            }
        }

        public ISimulationConfiguration Configuration { get; set; }
        IComponentsRegistry IWorld.ComponentsRegistry => ComponentsRegistry;
        public ComponentsRegistry ComponentsRegistry { get; } = new();
        ISystemsRegistry IWorld.SystemsRegistry => SystemsRegistry;
        public SystemsRegistry SystemsRegistry { get; } = new();


        private void OnAddComponentItem1(object item1, Type t, Type t2)
        {
            foreach (var item2 in ComponentsRegistry.GetComponents(t2))
            {
                Activator.CreateInstance(t, item1, item2, this);
            }
        }

        private void OnAddComponentItem2(object item2, Type t, Type t1)
        {
            foreach (var item1 in ComponentsRegistry.GetComponents(t1))
            {
                Activator.CreateInstance(t, item1, item2, this);
            }
        }

        private void Awake()
        {
            foreach (var (t, t1, t2) in tupleTypes)
            {
                ComponentsRegistry.SubscribeOnAdd(t1, (object i) => OnAddComponentItem1(i, t, t2));
                ComponentsRegistry.SubscribeOnAdd(t2, (object i) => OnAddComponentItem2(i, t, t1));
            }
        }
    }
}