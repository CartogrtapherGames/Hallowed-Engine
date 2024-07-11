using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core;

/// <summary>
/// the class that handle spritesheet animations
/// it is kept seperated of the AnimatedSprite class to for data reason.
/// </summary>
public class SpriteSheet
{
  private readonly Dictionary<string, AnimationObject> _animations = new();
  private readonly Area2D _frameSize;
  private Rectangle _currentFrame;
  private int _framerate;
  private bool _isPlaying;

  private Texture2D _texture;

  public SpriteSheet(Texture2D texture,Area2D frameSize, int framerate = 12)
  {
    _texture = texture;
    _framerate = framerate;
    _frameSize = frameSize;
    _currentFrame = new Rectangle(0, 0, _frameSize.Width, frameSize.Height);
    _isPlaying = false;
  }


  /// <summary>
  /// return the numbers of columns the image contains.
  /// </summary>
  /// <returns></returns>
  public int Columns()
  {
    return _texture.Width / _frameSize.Width;
  }

  /// <summary>
  /// return the numbers of rows the image contains.
  /// </summary>
  /// <returns></returns>
  public int Rows()
  {
    return _texture.Height / _frameSize.Height;
  }

  public void AddAnimations(string name, int column, int frameCount, bool loop)
  {
    if(_animations.ContainsKey(name)) 
      throw new Exception("the key " + name + "already exists!");
    var data = new AnimationObject()
    {
      Row = column,
      FrameCount = frameCount,
      Loop = loop
    };
    _animations.Add(name, data);
  }

  public void Play(string name)
  {
    if (!_animations.ContainsKey(name))
      throw new Exception("The key " + name + "does not exist!");
    // todo : implement the animations flag?
  }

  public void Stop()
  {
    _isPlaying = false;
  }


  public void Update(GameTime delta)
  {
    if (!_isPlaying) return;
    // todo : implement movement
  }


  private int[] CalculateFrame()
  {
    return new int[2];
  }
}