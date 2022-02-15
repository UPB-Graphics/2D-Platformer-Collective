using UnityEngine;
using UnityEditor;
using SkillTree;

public class EditModeFunctions : EditorWindow
{
    [MenuItem("Window/Skill Tree Edit Mode Functions")]
    public static void ShowWindow()
    {
        GetWindow<EditModeFunctions>("Skill Tree Edit Mode Functions");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Create skill tree from container"))
        {
            FindObjectOfType<SkillTreeUI>().CreateSkillTree();
        }

        if (GUILayout.Button("Destroy skill tree"))
        {
            FindObjectOfType<SkillTreeUI>().DestroySkillTree();
        }

        if (GUILayout.Button("Update skill tree edges"))
        {
            FindObjectOfType<SkillTreeUI>().DrawEdges();
        }
        if (GUILayout.Button("Enable skill tree constraints"))
        {
            FindObjectOfType<SkillTreeUI>().SetConstraints(true);
        }

        if (GUILayout.Button("Disable skill tree constraints"))
        {
            FindObjectOfType<SkillTreeUI>().SetConstraints(false);
        }
    }

}