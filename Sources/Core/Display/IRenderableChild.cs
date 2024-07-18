using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core.Display;

public interface IRenderableChild
{
  public void Update(GameTime delta);
  public void Draw(SpriteBatch batch, GameTime delta);
  public void Dispose();
  public bool Enabled { get; set; }
}