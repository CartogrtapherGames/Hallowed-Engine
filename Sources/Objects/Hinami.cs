using Hallowed.Core;
using Hallowed.Core.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Objects;

public class Hinami: ObjectBase
{

  
  private AnimatedSprite _sprite;

  public Hinami(): base()
  {
    
  }
  public override void Update(GameTime delta)
  {
    throw new System.NotImplementedException();
  }

  public override void Draw(SpriteBatch batch, GameTime delta)
  {
    base.Draw(batch, delta);
    
  }
}