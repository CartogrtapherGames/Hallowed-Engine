using Hallowed.Core;
using Hallowed.Core.Display;
using Hallowed.Core.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Objects;

public abstract class CharacterBase : ObjectBase
{
  public Sprite Sprite;
  protected override Vector2 Origin { get; set; }

  protected CharacterBase()
  {
  }

  public override void Update(GameTime delta)
  {
    // for the moment I will put that there but consider moving it up to the base class
    // if not enabled we do not allow any updates
    if (!Enabled) return;
    Sprite.SetPos(X, Y);
    Sprite.SetAnchor(Pivot.X, Pivot.Y);
  }

  public override void Draw(SpriteBatch batch, GameTime delta)
  {
    Sprite.Draw(batch, delta);
  }
}