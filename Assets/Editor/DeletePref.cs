using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeletePref : EditorWindow
{
    [MenuItem("Edit/Delete All PlayerPrefs")]
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
