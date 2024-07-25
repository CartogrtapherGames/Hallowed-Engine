using Hallowed.Core;
using Hallowed.Core.Objects;
using Hallowed.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Objects;

public enum Direction
{
  Top,
  Left,
  Right,
  Bottom
}

public abstract class CharacterBase : ObjectBase
{
  public AnimatedSprite Sprite;

  public Direction Direction = Direction.Left;

  protected CharacterBase(DataModelBase data)
  {
    var frameSize = new Area2D(data.FrameSize.Width, data.FrameSize.Height);
    var firstFrame = new Point(data.StartFrame.X, data.StartFrame.Y);
    Sprite = new AnimatedSprite(frameSize, firstFrame, 8);
    Pivot = new Vector2(data.Pivot.X, data.StartFrame.Y);
    Setup(data);
  }

  public void SetTexture(Texture2D texture2D)
  {
    Sprite.Texture = texture2D;
  }

  protected virtual void Setup(DataModelBase data)
  {
    SetAnimations(data.Animations);
  }

  protected void SetAnimations(AnimationModel[] animations)
  {
    foreach (var animation in animations)
    {
      Sprite.AddAnimation(animation.Key, animation.FrameRange, animation.FrameCount, animation.Loop);
    }
  }


  protected override void RefreshTransform()
  {
    Sprite.SetPos(X, Y);
    Sprite.SetAnchor(Pivot.X, Pivot.Y);
  }

  public override void Update(GameTime delta)
  {
    Sprite.Update(delta);
  }

  public override void Draw(SpriteBatch batch, GameTime delta)
  {
    Sprite.Draw(batch, delta);
  }

  public override void Dispose()
  {
    Sprite.Dispose();
  }
}