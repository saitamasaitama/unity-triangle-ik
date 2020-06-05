using System.Collections;
using System.Collections.Generic;
using System;

public struct Triangle 
{
  public double lengthA, lengthB, lengthC;
  public Angle angleA, angleB, angleC;

  public static Triangle from3edges(double A,double B,double C)
  {

    //b^2 + c^2 + -a^2  / 2bc =  cos
    
    double cosA = (B * B + C * C - A * A) / (2 * B * C);
    double cosB = (A * A + C * C - B * B) / (2 * A * C);
    double cosC = (A * A + B * B - C * C) / (2 * A * B);

    //AにcosAを掛けて

    

    return new Triangle()
    {
      lengthA=A,
      lengthB=B,
      lengthC=C,
      angleA= Angle.fromRadian(Math.Acos(cosA)),
      angleB = Angle.fromRadian(Math.Acos(cosB)),
      angleC = Angle.fromRadian(Math.Acos(cosC)),
    };
  }



  public override string ToString() => $@"
  Length = [{lengthA},{lengthB},{lengthC}]
  Angle  = [
    A={angleA:0.000},
    B={angleB:0.000},
    C={angleC:0.000}
  ]
";

}

public struct Angle
{
  private double radian;

  public double Radian => radian;
  public double Degree => radian * (180/Math.PI);

  public static Angle fromRadian(double radian)
  {
    return new Angle() {
      radian = radian
    };
  }

  public static Angle fromDegree(double degree)
  {
    return new Angle()
    {
      radian = degree * Math.PI * 180
    };
  }

  public override string ToString()
  {
    return $"RAD={Radian} DEG={Degree}";
  }


}