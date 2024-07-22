using System.Collections.Generic;
using System.Linq;
using Hallowed.Core;
using Hallowed.Core.Display;
using Hallowed.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Scenes;

/// <summary>
/// The abstract class that shape a scene in the game
/// it offers many abstractions and functionality to help scene rendering.
/// It also offers a child tree rendering system
/// </summary>
public abstract class SceneBase : Game
{
  protected static SceneBase Instance = (SceneBase)null;

  protected readonly List<IRenderableChild> Children = new();

  protected SpriteBatch SpriteBatch;

  // TODO fix that for later
  protected InputMap<PlayerInput> InputMap;

  protected SceneBase()
  {
    Instance = this;
    Graphics.Init(Instance);
    InitializeInput();
  }

  protected override void Initialize()
  {
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
    foreach (var child in Children.Where(child => child.Enabled))
    {
      child.Update(gameTime);
    }

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    SpriteBatch.Begin();
    foreach (var child in Children.Where(child => child.Enabled))
    {
      child.Draw(SpriteBatch, gameTime);
    }

    SpriteBatch.End();
  }

  protected override void Dispose(bool disposing)
  {
    foreach (var child in Children)
    {
      child.Dispose();
    }

    base.Dispose(disposing);
  }

  protected void AddChild(IRenderableChild child)
  {
    Children.Add(child);
  }

  protected void AddChild(IRenderableChild[] children)
  {
    foreach (var child in children)
    {
      Children.Add(child);
    }
  }

  protected void RemoveChild(IRenderableChild child)
  {
    Children.Remove(child);
    child.Dispose();
  }
}