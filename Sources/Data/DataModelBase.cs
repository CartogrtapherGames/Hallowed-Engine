using Microsoft.Xna.Framework;

namespace Hallowed.Data;

[System.Serializable]
public abstract class DataModelBase
{
  public string Id;
  public string GroupId;
  public string Texture;
  public Pivot Pivot;
  public Point StartFrame;
  public FrameSizeModel FrameSize;
  public AnimationModel[] Animations;
}

[System.Serializable]
public struct Pivot
{
  public float X;
  public float Y;
}