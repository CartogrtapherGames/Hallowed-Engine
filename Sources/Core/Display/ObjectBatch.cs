using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core.Display;

public sealed class ObjectBatch : IDisposable
{
  private bool _isDisposed;
  private Game _game;
  private SpriteBatch _batch;
  private BasicEffect _effect;

  public ObjectBatch(Game game)
  {
    if (game is null)
    {
      throw new ArgumentException("game");
    }

    _game = game;
    _isDisposed = false;
    _batch = new SpriteBatch(_game.GraphicsDevice);
    _effect = new BasicEffect(_game.GraphicsDevice);
    _effect.FogEnabled = false;
    _effect.TextureEnabled = true;
    _effect.LightingEnabled = false;
    _effect.VertexColorEnabled = true;
    _effect.World = Matrix.Identity;
    _effect.Projection = Matrix.Identity;
    _effect.View = Matrix.Identity;
  }

  public void Dispose()
  {
    if (_isDisposed) return;
    _batch?.Dispose();
    _effect.Dispose();
    _isDisposed = true;
  }

  public void Begin(bool isFilteringEnabled)
  {
    SamplerState sampler = SamplerState.PointClamp;
    if (isFilteringEnabled)
    {
      sampler = SamplerState.LinearClamp;
    }

    Viewport vp = _game.GraphicsDevice.Viewport;
    _effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0f, 1f);

    _batch.Begin(
      blendState: BlendState.AlphaBlend,
      samplerState: sampler,
      rasterizerState: RasterizerState.CullNone,
      effect: _effect
    );
  }

  public void End()
  {
    _batch.End();
  }

  public void Draw(IRenderableChild obj, GameTime delta)
  {
    obj.Draw(_batch, delta);
  }

  public void DrawRenderTarget(Texture2D texture, Rectangle? sourceRect, Rectangle destinationRect, Color color)
  {
    _batch.Draw(texture, destinationRect, sourceRect, color);
  }
}