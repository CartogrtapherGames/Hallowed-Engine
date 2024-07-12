namespace Hallowed.Core;

internal struct AnimationObject
{
  public int Row;
  public int Column = 0;
  public int FrameCount;
  public bool Loop;

  public AnimationObject()
  {
    Row = 0;
    FrameCount = 0;
    Loop = false;
  }
}