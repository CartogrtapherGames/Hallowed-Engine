using System.Collections.Generic;
using Hallowed.Core;
using Hallowed.Core.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Scenes;

public enum PlayerInput
{
  Space
}

public abstract class SceneBase : Game
{
  protected List<IRenderableChild> _children = new List<IRenderableChild>();

  protected GraphicsDeviceManager Graphics;
  protected SpriteBatch SpriteBatch;
  protected InputMap<PlayerInput> InputMap;

  protected SceneBase()
  {
  }

  protected override void Initialize()
  {
    InitializeInput();
    base.Initialize();
  }

  // we initialize our input here  
  protected virtual void InitializeInput()
  {
    InputMap = new InputMap<PlayerInput>();
  }

  protected override void LoadContent()
  {
    SpriteBatch = new SpriteBatch(GraphicsDevice);
    base.LoadContent();
  }

  protected override void Update(GameTime gameTime)
  {
    InputMap.Update();
    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    SpriteBatch.Begin();
    foreach (var child in _children) child.Draw(SpriteBatch, gameTime);
    SpriteBatch.End();
  }

  protected void AddChild(IRenderableChild child)
  {
    _children.Add(child);
  }

  protected void AddChild(IRenderableChild[] children)
  {
    foreach (var child in children)
    {
      _children.Add(child);
    }
  }

  protected void RemoveChild(IRenderableChild child)
  {
    _children.Remove(child);
    child.Dispose();
  }
}