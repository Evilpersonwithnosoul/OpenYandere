using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.NPC.Prefabs;
using OpenYandere.Characters.NPC;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(NPC))]
public class StudentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Desenha o Inspetor padrão
        DrawDefaultInspector();

        NPC student = (NPC)target;

        // Aqui você pode adicionar controles personalizados para editar sua rotina, atividades, etc.
        if (GUILayout.Button("Add New Activity"))
        {
            // Adicionar nova atividade ao Student aqui...
        }
    }
}
#endif
