using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public static class GameObjectExtensions
    {
        public static T AddUniqueComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();

            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        public static void RemoveComponent<T>(this GameObject gameObject) where T : Object
        {
            Object.Destroy(gameObject.GetComponent<T>());
        }

        public static bool RemoveComponentIfExists<T>(this GameObject gameObject) where T : Object
        {
            T component = gameObject.GetComponent<T>();

            if (component == null)
            {
                return false;
            }

            Object.Destroy(component);
            return true;
        }
    }
}