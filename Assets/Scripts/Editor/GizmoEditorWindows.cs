using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace technical.test.editor
{
    public class GizmoEditorWindows : EditorWindow
    {
        private string path;
        private static GizmoSceneEnhancerAsset data;
        private static List<int> id_gizmo;
        private SceneGizmo seletectedGizmo;
        private SceneGizmo[] resetData;

        int size, index, x, y, z;
        string text = "name";
        Vector2 posResetDeleteButtons;
        private static bool drawOnce;

        void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
            posResetDeleteButtons = new Vector2(-200, -200);

            //loadData
            path = "Assets/Data/Editor/GizmoSceneEnhancer.asset";
            data = AssetDatabase.LoadAssetAtPath(path, typeof(GizmoSceneEnhancerAsset)) as GizmoSceneEnhancerAsset;

            // Save data
            int i = 0;
            resetData = new SceneGizmo[data.getGizmos().Length];
            foreach (SceneGizmo s in data.getGizmos())
            {
                resetData[i] = s;
                ++i;
               
            }

        }
        void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            data = null;
           
            seletectedGizmo = new SceneGizmo("empty", Vector3.zero);
            posResetDeleteButtons = Vector2.zero;
        }


        [MenuItem("Window/Scene Enhancer")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(GizmoEditorWindows));
            id_gizmo = new List<int>();
           
            foreach (SceneGizmo s in data.getGizmos())
            {
                id_gizmo.Add(GUIUtility.GetControlID(FocusType.Passive));
             
            }
        }

        void OnGUI()
        {
            //Init variable
            size = 200;
            index = 0;
            GUILayout.Label("Text", EditorStyles.boldLabel);
            foreach (SceneGizmo s in data.getGizmos())
            {
                if (s == seletectedGizmo) { GUI.backgroundColor = Color.red; }
                else GUI.backgroundColor = Color.white;

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
                {
                    seletectedGizmo = s;
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                if (s == seletectedGizmo) OnValueChange(index, new Vector3(x, y, z));

                ++index;

            }
        }

        void OnSceneGUI(SceneView SceneView)
        {
            //Handles.BeginGUI();

            int index = 0;
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;

            Handles.color = Color.white;
            foreach (SceneGizmo s in data.getGizmos())
            {
                Handles.Label(s.Position + (Vector3.up * 2), s.Name, style);
                Handles.SphereHandleCap(id_gizmo[index], s.Position, Quaternion.Euler(0, 0, 0), 2, EventType.Repaint);

                // Move with 3d arrow from unity
                EditorGUI.BeginChangeCheck();

                Vector3 newTargetPosition = Handles.PositionHandle(s.Position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    data.getGizmos()[index].Position = newTargetPosition;
                }

                // Click on sphere
                if (Handles.Button(s.Position, Quaternion.LookRotation(Camera.current.transform.forward, Camera.current.transform.up), 1, 1, Handles.RectangleHandleCap))
                {
                    seletectedGizmo = s;
                    // Debug.Log("Gizmo selected by click");
                    posResetDeleteButtons = HandleUtility.WorldToGUIPoint(seletectedGizmo.Position);

                }
                index++;
                spawnResetAndDeleteButton((int)posResetDeleteButtons.x, (int)posResetDeleteButtons.y);
            }
        }
            // spawnResetAndDeleteButton();

            /*
            if (Event.current.type == EventType.MouseDown)
            {
                int id = HandleUtility.nearestControl;
                Vector3 mousePos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
                Debug.Log(mousePos);
                for (int i=0; i < id_gizmo.Count; ++i)
                {
                    //if (Vector2.Distance(mousePos, data.getGizmos()[i].Position) <= 0.25f)
                        //Debug.Log(i);
                }
                
            }
            // Handles.EndGUI();
        }

        /*
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

        }*/


        public void spawnResetAndDeleteButton(int x, int y)
        {
            Handles.BeginGUI();

            // Reset and Delete
            GUILayout.BeginArea(new Rect(x, y, 150, 60));

            var rect = EditorGUILayout.BeginVertical();
            
            GUI.Box(rect, GUIContent.none);

            // GUI.color = Color.black;
            //GUI.backgroundColor = Color.white;

            if (GUILayout.Button("Reset Position"))
            {
                int index = FindIndexInData(seletectedGizmo);
                
                OnValueChange(index, resetData[index].Position);
               
            }

            if (GUILayout.Button("Delete Gizmo"))
            {
                data.remove(FindIndexInData(seletectedGizmo));
            }

            GUILayout.EndArea();

            Handles.EndGUI();

        }

        public void OnValueChange(int index, Vector3 newPosition)
        {
           // Debug.Log("Move from " + data.getGizmos()[index].Position + " to " + newPosition);
            if (data.getGizmos()[index].Position == newPosition) return;

            data.getGizmos()[index].Position = newPosition;
            seletectedGizmo = data.getGizmos()[index];

        }

        public int FindIndexInData(SceneGizmo s)
        {
            for (int i = 0; i < data.getGizmos().Length; ++i)
            {
                if (s==data.getGizmos()[i]) return i;
            }
            Debug.LogError("GizmoEditorWindows : FindIndexInData : Scene gizmo not find");
            return -1;
        }

       
    }
   
}