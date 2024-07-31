using System;

namespace Hallowed;

public static class Utils
{
  public static int Clamp(int value, int min, int max)
  {
    if (min > max)
    {
      throw new ArgumentOutOfRangeException("the value of \"min\" is greater than the value of \"max\".");
    }

    if (value < min)
    {
      return min;
    }
    else if (value > max)
    {
      return max;
    }
    else
    {
      return value;
    }
  }
}