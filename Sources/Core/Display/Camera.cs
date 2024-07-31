using Hallowed.Core.Objects;
using Hallowed.Objects;
using Microsoft.Xna.Framework;

namespace Hallowed.Core.Display;

/// <summary>
/// The game object class that handle the camera in the world.
/// </summary>
public sealed class Camera
{
  public static readonly float minZ = 1f;
  public static readonly float MaxZ = 2048f;
  private Vector2 _position;
  private float _z;
  private float _baseZ;

  private float _aspectRatio;
  private float _fieldOfView;

  private Matrix view;
  private Matrix proj;
}