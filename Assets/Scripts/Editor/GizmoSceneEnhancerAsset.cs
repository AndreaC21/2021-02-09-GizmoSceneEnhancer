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
    }

}