using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriangleIK : MonoBehaviour
{
  public Transform bow1;
  public Transform bow2;
  public Transform Target;
  //public double Distance;

  public float bow1Length = 10f;
  public float bow2Length = 10f;

  public float MinLength => Math.Abs(bow2Length - bow1Length);
  public float MaxLength => bow2Length + bow1Length;
  public float XLength => Target.transform.position.x - this.transform.position.x;
  public float YLength => Target.transform.position.z - this.transform.position.z;
  public float TargetHeight => Target.transform.position.y - this.transform.position.y;
  public float EdgeLength => Mathf.Sqrt(
    (XLength * XLength) +
    (YLength * YLength)
  );
  public float rotationY => Mathf.Atan2(
    Target.transform.position.x - this.transform.position.x,
    Target.transform.position.z - this.transform.position.z
    ) / Mathf.PI * 180;
  public float rotationX => Mathf.Atan2(
    EdgeLength,
    TargetHeight
  ) / Mathf.PI * 180;


  public double TargetDistance=> Vector3.Distance(this.transform.position, Target.position);
  public float TriangleEdgeLength => Mathf.Clamp((float)TargetDistance, MinLength, MaxLength);

  public Triangle triangle => Triangle.from3edges(bow1Length, bow2Length, TriangleEdgeLength);
  public float Tilt => 90 - this.rotationX;


  //先端の位置
  public Vector3 Tip => this.transform.position
        + (bow1.rotation * Vector3.up * bow1Length)
        + (bow2.rotation * Vector3.up * bow2Length);
    

  [ExecuteInEditMode]
  public void Update()
  {
    PanTiltUPdate();

    DebugDraw();
  }


  private void PanTiltUPdate()
  {
    //斜辺の長さを求める
    bow1.transform.localRotation = Quaternion.Euler((90f - (float)triangle.angleB.Degree) - Tilt, 0, 0);
    bow2.transform.localRotation = Quaternion.Euler(180f - (float)triangle.angleC.Degree, 0, 0);
    this.transform.rotation = Quaternion.Euler(0, rotationY, 0);
    
  }

  private void DebugDraw()
  {
    //debug draw
    Vector3 A = this.transform.position;
    Vector3 B = A + bow1.rotation * Vector3.up * bow1Length;
    Vector3 C = B + bow2.rotation * Vector3.up * bow2Length;
    //1
    Debug.DrawLine(A, B,Color.green);
    Debug.DrawLine(B, C, Color.green);
    Debug.DrawLine(A, C, Color.yellow);

    //最大・最小までを描画する


  }
}
