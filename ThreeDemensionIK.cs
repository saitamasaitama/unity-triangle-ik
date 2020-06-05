using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDemensionIK : MonoBehaviour
{
  public Transform Target;
  public double Distance;
  public TriangleIK ik;

  

  public void Update()
  {
    Distance = Vector3.Distance(this.transform.position, Target.position);

    ik.Length = (float)Distance;

    //Yの角度を求める
    float xlength = Target.transform.position.x - this.transform.position.x;
    float ylength = Target.transform.position.z - this.transform.position.z;
    float height = Target.transform.position.y - this.transform.position.y;



    float yr= Mathf.Atan2(
      Target.transform.position.x- this.transform.position.x ,
      Target.transform.position.z - this.transform.position.z
      ) / Mathf.PI * 180;

    //斜辺の長さを求める
    float edge = Mathf.Sqrt(
      (xlength * xlength) +
      (ylength * ylength)
      ) ;

    float xr = Mathf.Atan2(
      edge,
      height
      ) / Mathf.PI * 180;

    this.transform.rotation = Quaternion.Euler(0, yr, 0);
    this.ik.Pitch = 90 - xr;

    //Debug.Log($"Edge={edge} Y={yr} xr={xr}");

  }


}
