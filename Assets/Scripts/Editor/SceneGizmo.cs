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

        
    }

}