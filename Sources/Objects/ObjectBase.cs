using System;
using Hallowed.Core.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core.Objects;

public abstract class ObjectBase : IRenderableChild, IDisposable
{
  public bool Enabled { get; set; } = true;

  // in this case the *sprite* is the thing that as a rect and transform so we do not draw the object
  // itself and is more or less a container?


  private Vector3 _transform;
  private Vector2 _pivot;


  protected ObjectBase()
  {
    _transform = Vector3.Zero;
    _pivot = new Vector2(0, 0);
  }

  public abstract void Update(GameTime delta);

  public abstract void Draw(SpriteBatch batch, GameTime delta);

  public void Dispose()
  {
    throw new System.NotImplementedException();
  }

  public Vector3 Transform
  {
    get => _transform;
    set => _transform = value;
  }

  public float X
  {
    get => _transform.X;
    set => _transform.X = value;
  }

  public float Y
  {
    get => _transform.Y;
    set => _transform.Y = value;
  }

  public float Z
  {
    get => _transform.Z;
    set => _transform.Z = value;
  }

  public Vector2 Pivot
  {
    get => _pivot;
    set
    {
      var x = Math.Clamp(value.X, 0f, 1f);
      var y = Math.Clamp(value.Y, 0f, 1f);
      _pivot = new Vector2(x, y);
    }
  }

  protected abstract Vector2 Origin { get; set; }
}