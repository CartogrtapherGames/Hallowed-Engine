using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core;

public class AnimatedSprite : Sprite
{

  
  private int _frames; 
  public AnimatedSprite(Texture2D texture, int frames): base(texture)
  {
    
  }


  public int CurrentFrame
  {
    get => _frames;
    set => _frames = value;
  }
  
}