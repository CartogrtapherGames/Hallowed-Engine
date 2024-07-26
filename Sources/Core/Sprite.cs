using System;
using Hallowed.Core.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core;

/// <summary>
/// Represents a sprite that can be rendered on the screen.
/// </summary>
public class Sprite : IDisposable, IRenderableChild
{
  private Color _color;
  private Vector2 _position;
  private Vector2 _scale;
  private Texture2D _texture;

  private Vector2 _anchor;

  private bool[] _mirror;

  public Sprite()
  {
    _position = new Vector2();
    _scale = new Vector2(1, 1);
    _color = Color.White;
    Rotation = 0;
    _anchor = new Vector2(0, 0);
    // x,y  in this case we're doing an array as its optimized and small and we dont need a big data structure
    _mirror = new bool[2] { false, false };
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
  /// shorthand function to set the sprite anchor.
  /// calling the function without parameters will reset its anchor to (0,0)
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  public void SetAnchor(float x = 0, float y = 0)
  {
    _anchor.X = x;
    _anchor.Y = y;
  }


  /// <summary>
  /// draw the sprite on screen.
  /// Its important to call this in between a spriteBatch in the main class.
  /// </summary>
  /// <param name="batch"></param>
  /// <param name="delta"></param>
  public virtual void Draw(SpriteBatch batch, GameTime delta)
  {
    SpriteEffects effects = GetSpriteEffects();
    batch.Draw(
      texture: _texture,
      destinationRectangle: Rect,
      sourceRectangle: null,
      color: _color,
      rotation: Rotation,
      origin: Origin,
      effects: effects,
      layerDepth: 0f
    );
  }

  /// <summary>
  /// Update the sprite movement and such. 
  /// </summary>
  /// <param name="delta"></param>
  public virtual void Update(GameTime delta)
  {
  }

  public void Flip(bool horizontal = false, bool vertical = false)
  {
    _mirror[0] = horizontal;
    _mirror[1] = vertical;
  }

  /// <summary>
  /// Dispose the sprite and its texture
  /// </summary>
  public virtual void Dispose()
  {
    _texture?.Dispose();
  }

  protected SpriteEffects GetSpriteEffects()
  {
    if (_mirror[0] && _mirror[1])
    {
      return SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
    }

    else if (_mirror[0])
    {
      return SpriteEffects.FlipHorizontally;
    }

    else if (_mirror[1])
    {
      return SpriteEffects.FlipVertically;
    }
    else
    {
      return SpriteEffects.None;
    }
  }

  #region field

  public bool Enabled { get; set; } = true;

  /// <summary>
  /// The sprite texture
  /// </summary>
  public Texture2D Texture
  {
    get => _texture;
    // TODO : maybe make sure to force a refresh on the texture?
    set => _texture = value;
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
      var result = scaleY * _texture.Height;
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
  public Rectangle Rect => new((int)_position.X, (int)_position.Y, Width, Height);

  /// <summary>
  /// Represent the rectangle frame. 
  /// </summary>
  /// <remarks>
  /// Do take note that source rect is meant to be used as the frame rect when doing animated sprite
  /// in the base class Sprite its only added for conveniences.
  /// </remarks>
  public virtual Rectangle SourceRect { get; protected set; }

  /// <summary>
  /// the sprite X coordinates
  /// </summary>
  public float X
  {
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

  /// <summary>
  /// the sprite anchor where the sprite will
  /// base its position and rotation from.
  /// </summary>
  /// <remarks>
  /// the value of the anchor is clamped from 0f to 1f and scales
  /// with the realWidth and RealHeight of the texture
  /// </remarks>
  public Vector2 Anchor
  {
    get => _anchor;
    set
    {
      var x = Math.Clamp(value.X, 0f, 1f);
      var y = Math.Clamp(value.Y, 0f, 1f);
      _anchor = new Vector2(x, y);
    }
  }

  /// <summary>
  /// set the value of the sprite if its flipped or not in the X-axis
  /// false = not flipped
  /// </summary>
  public bool MirrorX
  {
    get => _mirror[0];
    set => _mirror[0] = value;
  }

  /// <summary>
  /// set the value of the sprite if its flipped or not in the Y-axis
  /// false = not flipped
  /// </summary>
  public bool MirrorY
  {
    get => _mirror[1];
    set => _mirror[1] = value;
  }

  /// <summary>
  /// the sprite origin. which is set via the realWidth and anchor of the sprite
  /// </summary>
  protected virtual Vector2 Origin => new(RealWidth * _anchor.X, RealHeight * _anchor.Y);

  public float Rotation { get; set; }

  #endregion
}