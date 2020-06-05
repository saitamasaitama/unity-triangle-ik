using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System;

[CustomEditor(typeof(TriangleIK))]
public class TriangleIKEditor : Editor
{
  

  public override void OnInspectorGUI()
  {
    TriangleIK ik = this.target as TriangleIK;
    base.OnInspectorGUI();
    
    

    LineLayout(() =>
    {
      GUILayout.Label("Range");
      GUILayout.Label($"{ik.MinLength}～{ik.MaxLength}");
    });

    LineLayout(() =>
    {
      GUILayout.Label("TargetDistance");
      GUILayout.Label($"{ik.TargetDistance:0.0000}");
    });


  }

  private void LineLayout(Action act)
  {
    GUILayout.BeginHorizontal();
    act();
    GUILayout.EndHorizontal();
  }

}
