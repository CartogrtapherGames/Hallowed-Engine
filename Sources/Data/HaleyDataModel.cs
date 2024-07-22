using Hallowed.Core;

namespace Hallowed.Data;

[System.Serializable]
public class HaleyDataModel : DataModelBase
{
  public string DisplayName;
}

[System.Serializable]
public struct FrameSizeModel
{
  public int Width;
  public int Height;
}

[System.Serializable]
public struct AnimationModel
{
  public string Key;
  public FrameRange FrameRange;
  public int FrameCount;
  public bool Loop;
}