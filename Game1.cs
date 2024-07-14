using System.Diagnostics;
using Hallowed.Core;
using Hallowed.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hallowed;

public class Game1 : SceneBase
{
  private GraphicsDeviceManager _graphics;
  private SpriteBatch _spriteBatch;
  private Sprite _witch;
  private AnimatedSprite _king;

  public Game1()
  {
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    InputMap.BindAction("action", Keys.Space);
    // TODO: Add your initialization logic here
    _witch = new Sprite()
    {
      X = 100,
      Y = 100,
    };
    base.Initialize();
  }

  protected override void LoadContent()
  {
    base.LoadContent();

    _spriteBatch = new SpriteBatch(GraphicsDevice);
    var texture = Content.Load<Texture2D>("witch");
    _witch.Texture = texture;

    var kingTexture = Content.Load<Texture2D>("king");
    var frameSize = new Area2D(32, 32);

    _king = new AnimatedSprite(kingTexture, frameSize, new Point(0, 1), 8)
    {
      X = 0,
      Y = 0
    };

    _king.SetScale(4f, 4f);
    _king.AddAnimation("idle", new FrameRange(1, 0), 4, true);
    _king.AddAnimation("walk", new FrameRange(1, 5), 4, true);
    // TODO: use this.Content to load your game content here
  }

  protected override void Update(GameTime gameTime)
  {
    var newKeyboardState = Keyboard.GetState();


    _king.Update(gameTime);
    _witch.Update(gameTime);
    //  _witch.Rotation += 0.01f;
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
        Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    if (InputMap.IsPressed("action"))
    {
      _witch.SetScale(4f, 4f);
      _king.Play("idle");
    }

    if (InputMap.IsTriggered(Keys.LeftShift)) // TODO : fix that shit because jesus christ its way to much conditions
    {
      _king.Play("walk");
      Debug.WriteLine("Ping");
      /*
      if (_king.IsPlaying())
      {
        Debug.WriteLine("ping!");
        _king.Stop();
      }
      else
      {
        _king.Resume();
      }
      */
    }

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.CornflowerBlue);
    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
    _witch.Draw(_spriteBatch, gameTime);
    _king.Draw(_spriteBatch, gameTime);
    _spriteBatch.End();
    // TODO: Add your drawing code here
    base.Draw(gameTime);
  }
}