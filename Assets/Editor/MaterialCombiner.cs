using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class MaterialCombiner : EditorWindow
{
    Material mat;
    List<Material> materials = new List<Material>();
    Material placeholder;
    string message;
    bool delete = false;

    [MenuItem("Tools/Material Combiner")]
    public static void showwindow()
    {
        GetWindow<MaterialCombiner>("Material Combiner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Material To Use");
        mat = (Material)EditorGUILayout.ObjectField(mat,typeof(Material),true);
        GUILayout.Label("Materials To Replace");

        placeholder = (Material)EditorGUILayout.ObjectField(placeholder, typeof(Material), true);

        if (materials.Count > 0)
        {
            foreach (Material m in materials)
            {
                GUILayout.Label(m.name);
            }
        }
        


        if (GUILayout.Button("Push"))
        {
            if(placeholder!=null)
            {
                if(!materials.Contains(placeholder))
                {
                    materials.Add(placeholder);
                }
                else
                {
                    message = "Already in List !!!";
                }
            }
        }

        if (GUILayout.Button("Pop"))
        {
            if (materials.Count > 0)
            {
                materials.Remove(materials[materials.Count-1]);
            }
            else
            {
                message = "List is Empty!!";
            }
        }

        delete = GUILayout.Toggle(delete,"Delete Replaced Materials?");

        if (GUILayout.Button("Replace"))
        {
            process();
        }

        GUILayout.Label(message);
    }

    void process()
    {
        if (mat == null || materials.Count == 0)
        {
            message = "Both material fields must contain material(s)";
            return;
        }
        foreach (GameObject g in FindObjectsOfType<GameObject>())
        {
            if(g.GetComponent<MeshRenderer>() == null)
            {
                continue;
            }

            else
            {
                MeshRenderer rend = g.GetComponent<MeshRenderer>();
                int i = 0;
                Material[] list = rend.sharedMaterials;
                foreach (Material m1 in list)
                {
                    if(materials.Contains(m1))
                    {
                        list[i] = mat;
                        i++;
                    }
                }

                rend.sharedMaterials = list;
            }

        }

        if (delete)
        {
            foreach (Material m in materials)
            {
                string path = AssetDatabase.GetAssetPath(m);
                FileUtil.DeleteFileOrDirectory(path);
                FileUtil.DeleteFileOrDirectory(path +".meta");
            }
            materials.Clear();
        }
    }

}
