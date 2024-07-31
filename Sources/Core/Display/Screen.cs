using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core.Display;

public sealed class Screen : IDisposable
{
  private static readonly int MinDim = 64;
  private static readonly int MaxDim = 4096;
  private bool _isDisposed;
  private Game _game;
  private RenderTarget2D _target;

  private bool _isSet;

  public int Width => _target.Width;
  public int Height => _target.Height;

  public Screen(Game game, int width, int height)
  {
    width = Utils.Clamp(width, Screen.MinDim, Screen.MaxDim);
    height = Utils.Clamp(height, Screen.MinDim, Screen.MaxDim);

    _game = game ?? throw new ArgumentNullException("game");
    _target = new RenderTarget2D(_game.GraphicsDevice, width, height);
    _isDisposed = false;
    _isSet = false;
  }

  public void Dispose()
  {
    if (_isDisposed) return;
    _target?.Dispose();
    _target = null;
    _isDisposed = true;
    GC.SuppressFinalize(this);
  }

  public void Set()
  {
    if (_isSet) throw new Exception("Render target already set");
    _game.GraphicsDevice.SetRenderTarget(_target);
    _isSet = true;
  }

  public void UnSet()
  {
    if (!_isSet) throw new Exception("Render target is not set");
    _game.GraphicsDevice.SetRenderTarget(null);
    _isSet = false;
  }

  public void Present(ObjectBatch objectBatch, bool textureFiltering = true)
  {
    ArgumentNullException.ThrowIfNull(objectBatch);

#if DEBUG
    _game.GraphicsDevice.Clear(Color.HotPink);
#else
    _game.GraphicsDevice.Clear(Color.Black);
#endif

    var destinationRect = CalculateDestRect();
    objectBatch.Begin(textureFiltering);
    objectBatch.DrawRenderTarget(_target, null, destinationRect, Color.White);
    objectBatch.End();
  }

  private Rectangle CalculateDestRect()
  {
    var backBufferBounds = _game.GraphicsDevice.PresentationParameters.Bounds;
    var backBufferAspectRatio = (float)backBufferBounds.Width / backBufferBounds.Height;
    var screenAspectRatio = (float)Width / Height;

    float rx = 0f;
    float ry = 0f;
    float rw = backBufferBounds.Width;
    float rh = backBufferBounds.Height;

    if (backBufferAspectRatio > screenAspectRatio)
    {
      rw = rh * screenAspectRatio;
      rx = ((float)backBufferBounds.Width - rw) / 2f;
    }
    else if (backBufferAspectRatio < screenAspectRatio)
    {
      rh = rw / screenAspectRatio;
      ry = ((float)backBufferBounds.Height - rh) / 2f;
    }

    var result = new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
    return result;
  }
}