using UnityEngine;

namespace technical.test.editor
{
    [CreateAssetMenu(fileName = "Gizmo Scene Enhancer Asset", menuName = "Custom/Create Gizmo Scene Enhancer Asset")]
    public class GizmoSceneEnhancerAsset : ScriptableObject
    {
        [SerializeField] private SceneGizmo[] _gizmos = default;

        public override string ToString()
        {
            return "Gizmo count : " + _gizmos.Length;
        }

        public SceneGizmo[] getGizmos()
        {
            return _gizmos;
        }

        public void remove(int index)
        {
            SceneGizmo[] newGizmo = new SceneGizmo[_gizmos.Length - 1];
           // Debug.Log("index: "+index);
            for (int i = 0; i <_gizmos.Length-1; ++i)
            {
                newGizmo[i] = _gizmos[i];
                if (i >= index)
                {
                    if (i >= newGizmo.Length) break; // LastElement
                    newGizmo[i] = _gizmos[i+1];
                }
                else
                {

                }
                
            }
            _gizmos = newGizmo;
        }
    }

}