using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core;

/// <summary>
/// the class that handle animated sprites
/// </summary>
public class AnimatedSprite : Sprite
{

  public event Action Completed;
  private readonly HashSet<Action> _handlers = new HashSet<Action>();
  
  private readonly Dictionary<string, AnimationObject> _animations = new();
  private readonly Area2D _frameSize;
  private readonly int _framerate;
  private int _frame;
  private bool _isPlaying;
  private bool _isCompleted;
  private string _currentAnimation;
  private float _animationTimer;
  
  
  public sealed override Rectangle SourceRect { get; protected set; }

  public override int Width
  {
    get
    {
      var scaleX = Math.Abs(Scale.X);
      var result = scaleX * _frameSize.Width;
      return (int)result;
    }
  }

  public override int Height
  {
    get
    {
      var scaleX = Math.Abs(Scale.X);
      var result = scaleX * _frameSize.Height;
      return (int)result;
    }
  }

  public AnimatedSprite(Texture2D texture, Area2D frameSize, int framerate = 8): base(texture)
  {
    _framerate = framerate;
    _frameSize = frameSize;
    SourceRect = new Rectangle(0, 0, _frameSize.Width, frameSize.Height);
    
    _currentAnimation = "";
    _frame = 0;
    _isPlaying = false;
    _animationTimer = 0f;
  }



  /// <summary>
  /// return the numbers of columns the image contains.
  /// </summary>
  /// <returns></returns>
  public int Columns()
  {
    return Texture.Width / _frameSize.Width;
  }

  /// <summary>
  /// return the numbers of rows the image contains.
  /// </summary>
  /// <returns></returns>
  public int Rows()
  {
    return Texture.Height / _frameSize.Height;
  }

  public void AddAnimation(string name, int row, int frameCount, bool loop)
  {
    if(_animations.ContainsKey(name))
      throw new Exception("the key " + name + "already exists!");
    var data = new AnimationObject()
    {
      Row = row,
      FrameCount = frameCount,
      Loop = loop
    };
    _animations.Add(name,data);
    Debug.WriteLine("added " + name);
  }

  private void Reset()
  {
    _frame = 0;
    _currentAnimation = "";
    _isCompleted = false;
    _isPlaying = true;
    _animationTimer = 0;
  }
  public AnimatedSprite Play(string name)
  {
    if (!_animations.ContainsKey(name)) throw new Exception($"The animation {name} does not exist!");
    Reset();
    _currentAnimation = name;
    _isPlaying = true;
    return this;
  }

  public AnimatedSprite OnCompleted(Action completedAction)
  {

    if (_handlers.Add(completedAction))
    {
      Completed += completedAction;
    }
    _isCompleted = true;
    Reset();
    return this;
  }
  
  public void Stop(string name, bool reset = false)
  {
    if (reset)
    {
      _isCompleted = true;
      Completed?.Invoke();
    }
    _isPlaying = false;
  }

  public void Resume()
  {
    _isPlaying = true;
  }

  public bool IsPlaying()
  {
    return _isPlaying;
  }

  public bool IsCompleted()
  {
    return _isCompleted;
  }
  
  public override void Draw(SpriteBatch batch, GameTime delta)
  {

    batch.Draw(Texture,Rect,SourceRect,Color);
  }

 
  public override void Dispose()
  {
    base.Dispose();
    // in this case we're unsubscribing to clear the events
    foreach (var handle in _handlers)
    {
      Completed -= handle;
    }
    _handlers.Clear();
  }


  public override void Update(GameTime delta)
  {
    if (!_isPlaying) return;
    var anim = Animation(_currentAnimation);
    var sequence = BuildFrameSequences(anim);
    if (anim.Loop)
    {
      ProcessLoopingAnimation(sequence, anim, delta);
    }
    else
    {
      ProcessAnimation(sequence,delta);
    }
  }

  // this will process until it is told to stop
  // todo : maybe remove the anim since we dont really need it?
  private void ProcessLoopingAnimation(Point[] sequence, AnimationObject anim, GameTime delta)
  {
    var frameTotalTime = 1f / _framerate;

    _animationTimer += (float)delta.ElapsedGameTime.TotalSeconds;

    if (!(_animationTimer >= frameTotalTime)) return;
    
    _frame = (_frame + 1) % sequence.Length;
    SourceRect = new Rectangle(sequence[_frame].X, sequence[_frame].Y, _frameSize.Width, _frameSize.Height);
    _animationTimer = 0;
  }

  private void ProcessAnimation(Point[] sequences, GameTime delta)
  {
    var frameTotalTime = 1f / _framerate;
    _animationTimer += (float)delta.ElapsedGameTime.TotalSeconds;

    if (!(_animationTimer >= frameTotalTime)) return;
    if (_frame > sequences.Length)
    {
      Completed?.Invoke();
      Reset();
    }
  }

  private AnimationObject Animation(string name)
  {
    if (!_animations.ContainsKey(name)) throw new Exception($"The animation {name} does not exist!");
    return _animations[name];
  }

  private Point[] BuildFrameSequences(AnimationObject anim)
  {
    var sequence = new Point[anim.FrameCount];
    for (int i = 0; i < anim.FrameCount; i++)
    {
      var row = anim.Row * _frameSize.Height;
      var column = i * _frameSize.Width;
      sequence[i] = new Point(column,row);
    }

    return sequence;
  }
}