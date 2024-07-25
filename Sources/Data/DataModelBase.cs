using Microsoft.Xna.Framework;

namespace Hallowed.Data;

[System.Serializable]
public abstract class DataModelBase
{
  public string Id;
  public string GroupId;
  public string Texture;
  public Vector2 Pivot;
  public Point StartFrame;
  public FrameSizeModel FrameSize;
  public AnimationModel[] Animations;
}