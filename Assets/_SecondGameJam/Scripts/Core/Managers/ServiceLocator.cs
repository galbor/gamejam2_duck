using System;
using System.Collections.Generic;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.Managers
{
    /**
     * Provides a centralized place to register and access services from anywhere in the application.
     * This reduces direct dependencies between classes and makes it easier to swap out implementations.
     * Used mainly for widely used services like logging, analytics, configuration settings, etc.
     * Pros:
     * 1. Decouples classes from their dependencies.
     * Usage:
     * 1. Define an interface for the service:
     * public interface IService
     * {
     *   void DoSomething();
     * }
     * 2. Implement the interface:
     * public class Service : IService
     * {
     *  public void DoSomething() { * do something * }
     * }
     * 3. Register a service:
     * ServiceLocator.RegisterService<IService>(new Service());
     * 4. Get a service and use it
     * void start()
     * {
     *  IService service = ServiceLocator.GetService<IService>();
     *  service.DoSomething();
     * }
     * 5. Unregister a service:
     * ServiceLocator.UnregisterService<IService>();
     */
    public class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        /**
         * Register a service to the service locator.
         */
        public static void RegisterService<T>(T service)
        {
            Type type = typeof(T);
            if (!Services.ContainsKey(type))
            {
                Services[type] = service;
                return;
            }

            Debug.LogWarning($"Service of type {type} is already registered");
        }

        /**
         * Get a service from the service locator.
         */
        public static T GetService<T>()
        {
            Type type = typeof(T);
            if (Services.TryGetValue(type, out var service))
            {
                return (T)service;
            }

            throw new Exception($"Service of type {type} was not found. Make sure to register it first " +
                                $"before trying to access it.");
        }

        /**
         * Unregister a service from the service locator.
         */
        public static void UnregisterService<T>()
        {
            Type type = typeof(T);
            if (!Services.Remove(type))
            {
                Debug.LogWarning($"Service of type {type} was not found. Make sure to register it first " +
                                 $"before trying to unregister it.");
            }
        }
    }
}