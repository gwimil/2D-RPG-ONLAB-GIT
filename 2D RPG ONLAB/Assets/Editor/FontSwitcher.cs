#if UNITY_EDITOR
using System.Reflection;
using UnityEngine;
using UnityEditor;

public class FontSwitcher : EditorWindow
{
    [MenuItem("Font/Show Window")]
    public static void ShowFontWindow()
    {
        GetWindow<FontSwitcher>();
    }

    public void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        string newFontName = EditorGUILayout.DelayedTextField("Unity Font", GUI.skin.font.fontNames[0]);
        if (EditorGUI.EndChangeCheck())
        {
            ReplaceFont((Font)EditorGUIUtility.LoadRequired("Fonts/Times New Roman.ttf"), newFontName);
            ReplaceFont((Font)EditorGUIUtility.LoadRequired("Fonts/LiberationSerifBold.ttf"), newFontName);
            ReplaceFont((Font)EditorGUIUtility.LoadRequired("Fonts/LiberationSerifRegular.ttf"), newFontName);
           /* ReplaceFont((Font)EditorGUIUtility.LoadRequired("Fonts/Lucida Grande Small Bold.ttf"), newFontName);
            ReplaceFont((Font)EditorGUIUtility.LoadRequired("Fonts/Lucida Grande Big.ttf"), newFontName);*/

            typeof(EditorApplication).GetMethod("RequestRepaintAllViews", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
        }
    }

    private void ReplaceFont(Font font, string fontName)
    {
        if (font.name.Contains("Bold"))
            font.fontNames = new string[] { fontName + " Bold" };
        else
            font.fontNames = new string[] { fontName };
        font.hideFlags = HideFlags.HideAndDontSave;
    }
}
#endif