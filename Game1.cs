using System.Diagnostics;
using Hallowed.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hallowed;

public class Game1 : Game
{
  private GraphicsDeviceManager _graphics;
  private SpriteBatch _spriteBatch;
  private Sprite _witch;

  public Game1()
  {
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    // TODO: Add your initialization logic here

    base.Initialize();
  }

  protected override void LoadContent()
  {
    base.LoadContent();
    _spriteBatch = new SpriteBatch(GraphicsDevice);
    var texture = Content.Load<Texture2D>("witch");
    _witch = new Sprite(texture);
    _witch.X = 100;
    _witch.Y = 100;
    // TODO: use this.Content to load your game content here
  }

  protected override void Update(GameTime gameTime)
  {

    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
        Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    if (Keyboard.GetState().IsKeyDown(Keys.Space))
    {
      
      _witch.SetScale(4f,4f);
    }
    // TODO: Add your update logic here

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.CornflowerBlue);
    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
    _witch.Draw(_spriteBatch,gameTime);
    _spriteBatch.End();
    // TODO: Add your drawing code here
    base.Draw(gameTime);
  }
}