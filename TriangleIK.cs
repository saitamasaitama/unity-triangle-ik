using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriangleIK : MonoBehaviour
{
  public Transform bow1;
  public Transform bow2;

  public Transform Target;
  public double Distance;

  public float bow1Length = 10f;
  public float bow2Length = 10f;

  public float minLength => Math.Abs(bow2Length - bow1Length);
  public float maxLength => bow2Length + bow1Length;

  public float Pitch = 0f;
  public double AngleA = 0f;
  public double AngleB = 0f;
  public double AngleC = 0f;


  public Vector3 Tip
  {
    get{
      Vector3 A = this.transform.position;
      Vector3 B = A + bow1.rotation * Vector3.up * bow1Length;
      Vector3 C = B + bow2.rotation * Vector3.up * bow2Length;
      return C;
    }

  }
    


  [Range(0,1f)]
  public float LengthRatio = 0;
  public float Length=10f;





  [ExecuteInEditMode]
  public void Update()
  {
    PanTiltUPdate();
    LengthUpdate();
  }

  private void LengthUpdate()
  {
    //Length = Mathf.Clamp(Length, minLength, maxLength);

    Triangle tri = Triangle.from3edges(bow1Length, bow2Length, Length);

    if (double.IsNaN(tri.angleC.Degree)) return;



    AngleA = tri.angleA.Degree;
    AngleB = tri.angleB.Degree;
    AngleC = tri.angleC.Degree;

    bow1.transform.localRotation = Quaternion.Euler((90f - (float)tri.angleB.Degree)-Pitch , 0, 0);
    bow2.transform.localRotation = Quaternion.Euler(180f - (float)tri.angleC.Degree, 0, 0);


    DebugDraw();
  }

  private void PanTiltUPdate()
  {
    double Distance = Vector3.Distance(this.transform.position, Target.position);
    this.Length = (float)Distance;

    //Yの角度を求める
    float xlength = Target.transform.position.x - this.transform.position.x;
    float ylength = Target.transform.position.z - this.transform.position.z;
    float height = Target.transform.position.y - this.transform.position.y;



    float yr = Mathf.Atan2(
      Target.transform.position.x - this.transform.position.x,
      Target.transform.position.z - this.transform.position.z
      ) / Mathf.PI * 180;

    //斜辺の長さを求める
    float edge = Mathf.Sqrt(
      (xlength * xlength) +
      (ylength * ylength)
      );

    float xr = Mathf.Atan2(
      edge,
      height
      ) / Mathf.PI * 180;

    this.transform.rotation = Quaternion.Euler(0, yr, 0);
    this.Pitch = 90 - xr;


  }

  private void DebugDraw()
  {
    //debug draw
    Vector3 A = this.transform.position;
    Vector3 B = A + bow1.rotation * Vector3.up * bow1Length;
    Vector3 C = B + bow2.rotation * Vector3.up * bow2Length;
    //1
    Debug.DrawLine(A, B,Color.red);
    Debug.DrawLine(B, C, Color.red);
    Debug.DrawLine(A, C, Color.yellow);
  }
}
