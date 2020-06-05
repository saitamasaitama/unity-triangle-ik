using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriangleIKFollower : MonoBehaviour
{
  public TriangleIK ik;

  private void Update()
  {
    this.transform.position = ik.Tip;
    
  }

}
