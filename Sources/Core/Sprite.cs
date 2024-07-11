using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core;

/// <summary>
/// Represents a sprite that can be rendered on the screen.
/// </summary>
public class Sprite : IDisposable
{
  private Color _color;
  private Vector2 _position;
  private Vector2 _scale;
  private Texture2D _texture;
  

  public Sprite(Texture2D texture)
  {
    _texture = texture;
    _position = new Vector2();
    _scale = new Vector2(1,1);
    _color = Color.White;
  }

  /// <summary>
  /// Move the sprite in float coordinates.
  /// Its a shorthand function to sprite.x / sprite.y
  /// </summary>
  /// <example>
  /// <code>
  /// var sprite = new Sprite(myTexture);
  /// sprite.SetPos(10,10);
  /// //same as
  /// sprite.x = 10;
  /// sprite.y = 10;
  /// </code>
  /// </example>
  /// <param name="x"></param>
  /// <param name="y"></param>
  public void SetPos(float x = 0, float y = 0)
  {
    _position.X = x;
    _position.Y = y;
  }

  /// <summary>
  /// shorthand function to set the sprite scale
  /// calling the function without parameters will reset its scale
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  public void SetScale(float x = 1, float y = 1)
  {
    _scale.X = x;
    _scale.Y = y;
  }

  /// <summary>
  /// draw the sprite on screen.
  /// Its important to call this in between a spriteBatch in the main class.
  /// </summary>
  /// <param name="batch"></param>
  /// <param name="delta"></param>
  public virtual void Draw(SpriteBatch batch, GameTime delta)
  {
    batch.Draw(_texture,Rect,_color);
  }

  public virtual void Update(GameTime delta)
  {
    
  }

  #region field

  /// <summary>
  /// The sprite texture
  /// </summary>
  public Texture2D Texture
  {
    get => _texture; 
    // TODO : maybe make sure to force a refresh on the texture?
    set=> _texture = value; 
  }

  /// <summary>
  /// the sprite coordinates
  /// </summary>
  public Vector2 Position
  {
    get => _position;
    set => _position = value;
  }


  /// <summary>
  /// The sprite height scaling with the scale value
  /// </summary>
  public virtual int Width
  {
    get
    {
      var scaleX = Math.Abs(_scale.X);
      var result = scaleX * _texture.Width;
      return (int)result;

    }
  }

  /// <summary>
  /// the sprite height scaling with the scale value
  /// </summary>
  public virtual int Height
  {
    get
    {
      var scaleY = Math.Abs(_scale.Y);
      var result = scaleY * _texture.Width;
      return (int)result;
    }
  }

  /// <summary>
  /// The real width of the sprite without being affected by the scaling factor
  /// </summary>
  public int RealWidth => _texture.Width;

  /// <summary>
  /// The real height of the sprite without being affected by the scaling factor
  /// </summary>
  public int RealHeight => _texture.Height;

  /// <summary>
  /// The sprite scale value
  /// </summary>
  public Vector2 Scale
  {
    get => _scale;
    set => _scale = value;
  }

  /// <summary>
  /// The sprite color
  /// </summary>
  public Color Color
  {
    get => _color;
    set => _color = value;
  }

  /// <summary>
  /// Represent the rectangle of the sprite
  /// </summary>
  public Rectangle Rect => new((int)_position.X, (int)_position.Y, Width,Height);

  /// <summary>
  /// Represent the rectangle frame. 
  /// </summary>
  /// <remarks>
  /// Do take note that source rect is meant to be used as the frame rect when doing animated sprite
  /// in the base class Sprite its only added for conveniences.
  /// </remarks>
  public virtual Rectangle SourceRect
  {
    get;
    protected set;
  }

  /// <summary>
  /// the sprite X coordinates
  /// </summary>
  public float X {
    get => _position.X;
    set => _position.X = value;
  }

  /// <summary>
  /// the sprite Y coordinates
  /// </summary>
  public float Y
  {
    get => _position.Y;
    set => _position.Y = value;
  }
  
  #endregion

  public virtual void Dispose()
  {
    _texture?.Dispose();
  }
}