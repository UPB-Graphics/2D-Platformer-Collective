using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Platformer.Utils
{
    public static class Extensions { }

    public static class GameObjectExtensions
    {
        public static void DestroyAllChildren(this GameObject go)
        {
            foreach (Transform child in go.transform)
            {
                child.gameObject.Destroy();
            }
        }

        public static void DestroyImmediateAllChildren(this GameObject go)
        {
            for (var i = go.transform.childCount - 1; i >= 0; i--)
            {
                go.transform.GetChild(i).gameObject.DestroyImmediate();
            }
        }

        public static void Destroy(this GameObject go)
        {
            UnityEngine.Object.Destroy(go);
        }

        public static void DestroyImmediate(this GameObject go)
        {
            #if UNITY_EDITOR
            UnityEngine.Object.DestroyImmediate(go);
            #else
            UnityEngine.Object.Destroy(go);
            #endif
        }
        
        public static void ShuffleChildren(this GameObject go)
        {
            var goTransform = go.transform;
            var childCount = goTransform.childCount;
            for (var i = 0; i < goTransform.childCount; i++)
            {
                goTransform.GetChild(i).SetSiblingIndex(Random.Range(0, childCount));
            }
        }
        
        public static string Hierarchy(this GameObject go)
        {
            var str = new List<string>();
            str.Add(go.name);
            var tempT = go.transform;
            while (tempT.parent != null)
            {
                var parent = tempT.parent;
                str.Add(parent.name);
                tempT = parent;
            }
            str.Reverse();
            return string.Join(" -> ", str.ToArray());
        }
    }

    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform t, bool destroyImmediately = false)
        {
            foreach (Transform child in t)
            {
                if (destroyImmediately)
                {
                    MonoBehaviour.DestroyImmediate(child.gameObject);
                }
                else
                {
                    MonoBehaviour.Destroy(child.gameObject);
                }
            }
        }
    }
    
    public static class ActionExtensions
    {
        public static void Fire(this Action action)
        {
            if (action != null)
                action();
        }

        public static void Fire<T>(this Action<T> action, T t)
        {
            if (action != null)
                action(t);
        }

        public static void Fire<T, U>(this Action<T, U> action, T t, U u)
        {
            if (action != null)
                action(t, u);
        }

        public static void Fire<T, U, V>(this Action<T, U, V> action, T t, U u, V v)
        {
            if (action != null)
                action(t, u, v);
        }

        public static void Fire<T, U, V, W>(this Action<T, U, V, W> action, T t, U u, V v, W w)
        {
            if (action != null)
                action(t, u, v, w);
        }
    }
}