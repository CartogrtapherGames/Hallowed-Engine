using Hallowed.Core.Objects;
using Hallowed.Objects;
using Microsoft.Xna.Framework;

namespace Hallowed.Core.Display;

/// <summary>
/// The game object class that handle the camera in the world.
/// </summary>
public class Camera
{
  public Matrix Transform { get; private set; }

  private Area2D _screenSize;

  public Camera(int screenWidth, int screenHeight)
  {
    _screenSize = new Area2D(screenWidth, screenHeight);
  }

  public void Follow(CharacterBase target)
  {
    var offset = Matrix.CreateTranslation(
      (float)_screenSize.Width / 2,
      (float)_screenSize.Height / 2,
      0
    );
    var position = Matrix.CreateTranslation(
      -target.Sprite.X - ((float)target.Sprite.Width / 2),
      -target.Sprite.Y + ((float)target.Sprite.Height / 2),
      0);
    Transform = position * offset;
  }
}