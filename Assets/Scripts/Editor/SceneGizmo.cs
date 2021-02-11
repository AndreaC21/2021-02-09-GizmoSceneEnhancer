using System;
using UnityEngine;

namespace technical.test.editor
{
    [Serializable]
    public struct SceneGizmo
    {
        public string Name;
        public Vector3 Position;

        public SceneGizmo(string name, Vector3 position)
        {
            Name = name;
            Position = position;
        }
        public static bool operator ==(SceneGizmo a, SceneGizmo b)
        {
            return a.Name == b.Name && a.Position == b.Position;
        }
        public static bool operator !=(SceneGizmo a, SceneGizmo b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return true;
        }
    }



}