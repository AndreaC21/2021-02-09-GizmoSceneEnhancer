using System.Collections;
using UnityEngine;
using UnityEditor;

namespace technical.test.editor
{
    public class GizmoEditorWindows : EditorWindow
    {

        private static GizmoSceneEnhancerAsset data;
        private static GizmoRender rend;
        int size;
        string text = "name";
        int x, y, z;
        private static bool drawOnce;

        void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }
        void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            data = null;
        }


        [MenuItem("Window/Gizmo Editor")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(GizmoEditorWindows));
        }

        public static void DisplayData(GizmoSceneEnhancerAsset so)
        {
            data = so;
            //Debug.Log(data.ToString());
            ShowWindow();
            //Transform t;
            //RenderSphere(t, GizmoType.Selected);

        }

        void OnGUI()
        {
            //Init variable
            size = 200;
            GUILayout.Label("Text", EditorStyles.boldLabel);
            foreach (SceneGizmo s in data.getGizmos())
            {
                GUILayout.BeginHorizontal(); 

                text = EditorGUILayout.TextField(s.Name, GUILayout.Width(size));
                GUILayout.Space(10);
                
                x = EditorGUILayout.IntField("x", (int)s.Position.x, GUILayout.Width(size));
                GUILayout.Space(10);
                y = EditorGUILayout.IntField("y", (int)s.Position.y, GUILayout.Width(size));
                GUILayout.Space(10);
                z = EditorGUILayout.IntField("z", (int)s.Position.z, GUILayout.Width(size));
                GUILayout.Space(10);
                if (GUILayout.Button("Edit", GUILayout.Width(100)))
                    x = 1;

                
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }

        void OnSceneGUI(SceneView SceneView)
        {
            //Handles.BeginGUI();
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;
          
            foreach (SceneGizmo s in data.getGizmos())
            {
                Handles.Label(s.Position + (Vector3.up*2), s.Name,style);
            }
           // Handles.EndGUI();
        }


       

        // With Transform it work but not perfect, call too many time even when the windows is not visible
       [DrawGizmo(GizmoType.Selected | GizmoType.NotInSelectionHierarchy)]
        static void RenderSphere(Transform src, GizmoType gizmoType) //GizmoEditorWindows
        {
            Vector3 position = Vector3.zero;
            // Draw the light icon
            // (A bit above the one drawn by the builtin light gizmo renderer)

           
            //Gizmos.DrawSphere(position, 1);
            if ( data != null)
            {
                Gizmos.color = Color.white;
                foreach (SceneGizmo s in data.getGizmos())
                {
                    Gizmos.DrawSphere(s.Position, 1);

                }
            }

        }


    }
    [ExecuteInEditMode]
    public class GizmoRender : MonoBehaviour
    {
        /*public float explosionRadius = 5.0f;

        void OnDrawGizmos()
        {
            Debug.Log("here");
            // Display the explosion radius when selected
            transform.position = Vector3.zero;
            Gizmos.color = new Color(1, 1, 0, 0.75F);
            Gizmos.DrawSphere(transform.position, explosionRadius);
        }
        */
    }


}