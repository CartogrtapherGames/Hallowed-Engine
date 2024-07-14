using Hallowed.Core.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core.Objects;

public abstract class ObjectBase : IRenderableChild
{
  public bool Enabled { get; set; } = true;

  public Vector3 Transform { get; set; } = Vector3.Zero;
  public Vector2 Pivot { get; set; }
  public abstract void Update(GameTime delta);

  public virtual void Draw(SpriteBatch batch, GameTime delta)
  {
    if (!Enabled) return;
  }

  public void Dispose()
  {
    throw new System.NotImplementedException();
  }
}