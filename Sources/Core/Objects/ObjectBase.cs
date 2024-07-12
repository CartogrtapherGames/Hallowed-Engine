using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core.Objects;

public abstract class ObjectBase
{
  
  public bool Enabled { get; set; }


  public abstract void Update(GameTime delta);

  public virtual void Draw(SpriteBatch batch, GameTime delta)
  {
    if (!Enabled) return;
  }
}