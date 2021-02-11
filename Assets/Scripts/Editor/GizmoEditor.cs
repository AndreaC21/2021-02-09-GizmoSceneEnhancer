using UnityEngine;
using UnityEditor;

namespace technical.test.editor
{
    [CustomEditor(typeof(GizmoSceneEnhancerAsset))]
    public class GizmoEditor : Editor
    {
        GizmoSceneEnhancerAsset e;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            e = (GizmoSceneEnhancerAsset) target;
            if (GUILayout.Button("Open"))
            {
                GizmoEditorWindows.ShowWindow();
            }
        }

      
    }
}
