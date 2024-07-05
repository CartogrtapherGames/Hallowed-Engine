using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Sources.Core;

/// <summary>
/// Represents a sprite that can be rendered on the screen.
/// </summary>
public class Sprite
{
  public Texture2D Texture
  {
    get => _texture; 
    set=> _texture = value; 
  }
  
  public Vector2 Position
  {
    get => _position;
    set => _position = value;
  }


  public int Width
  {
    get
    {
      var scale = (int)Math.Abs(_scale.X);
      return scale * _texture.Width;

    }
  }

  /// <summary>
  /// the sprite height scaling with the scale value
  /// </summary>
  public int Height
  {
    get
    {
      var scaleY = (int)Math.Abs(_scale.Y);
      return scaleY * _texture.Height;
    }
  }

  /// <summary>
  /// The sprite scale value
  /// </summary>
  public Vector2 Scale
  {
    get => _scale;
    set => _scale = value;
  }

  /// <summary>
  /// Represent the rectangle of the sprite
  /// </summary>
  public Rectangle Rect => new((int)_position.X, (int)_position.Y, Width,Height);

  private Texture2D _texture;
  private Vector2 _position;
  private Vector2 _scale;
  
  public Sprite(Texture2D texture)
  {
    _texture = texture;
    _position = new Vector2();
    _scale = new Vector2();
  }

  /// <summary>
  /// Move the sprite in float coordinates.
  /// Its a shorthand functioon for 
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  public void Move(float x = 0, float y = 0)
  {
    _position.X = x;
    _position.Y = y;
  }

  public virtual void Draw(SpriteBatch batch, GameTime delta)
  {
    batch.Draw(_texture,Rect,Color.White);
  }

  public virtual void Update()
  {
    
  }
}