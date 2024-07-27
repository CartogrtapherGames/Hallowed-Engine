using System.Diagnostics;
using Hallowed.Core;
using Hallowed.Core.Display;
using Hallowed.Management;
using Hallowed.Objects.Haley;
using Hallowed.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hallowed;

public class Game1 : SceneBase
{
  private SpriteBatch _spriteBatch;
  private Sprite _witch;
  private ObjectHaley _objectHaley;
  private Sprite _background;
  private Camera _camera;

  public Game1()
  {
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    Database.Init();

    _camera = new Camera(Graphics.Width, Graphics.Height);
    _background = new Sprite();
    _background.Anchor = new Vector2(0.5f, 0.5f);
    _background.X = Graphics.Width * 0.5f;
    _background.Y = Graphics.Height * 0.5f;

    _objectHaley = new ObjectHaley(Database.HaleyDataModel, InputMap);
    _objectHaley.X = Graphics.Width * 0.5f;
    _objectHaley.Y = (Graphics.Height * 0.5f);
    _objectHaley.Sprite.SetScale(4f, 4f);
    // TODO: Add your initialization logic here
    _witch = new Sprite()
    {
      X = 100,
      Y = 100,
      Anchor = new Vector2(0.5f, 0.5f)
    };
    base.Initialize();
  }

  protected override void InitializeInput()
  {
    base.InitializeInput();
    InputMap.BindAction(PlayerInput.Space, Keys.Space);
    InputMap.BindAction(PlayerInput.Left, Keys.A);
    InputMap.BindAction(PlayerInput.Right, Keys.D);
  }

  protected override void LoadContent()
  {
    base.LoadContent();
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    var backgroundTexture = Content.Load<Texture2D>("Background");
    _background.Texture = backgroundTexture;

    var haleyTexture = Content.Load<Texture2D>("Yula");
    _objectHaley.SetTexture(haleyTexture);

    var texture = Content.Load<Texture2D>("witch");
    _witch.Texture = texture;

    // TODO: use this.Content to load your game content here
  }

  protected override void Update(GameTime gameTime)
  {
    _objectHaley.Update(gameTime);
    _background.Update(gameTime);
    _camera.Follow(_objectHaley);
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
        Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    if (InputMap.IsPressed(Keys.W))
    {
      _background.Y -= 3;
    }

    if (InputMap.IsTriggered(Keys.Space))
    {
      Debug.WriteLine("Haley_X: " + _objectHaley.X + "," + " Haley_Y " + _objectHaley.Y);
      Debug.WriteLine("Width: " + Graphics.Width / 2 + "," + " Height: " + Graphics.Height / 2);
    }

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.CornflowerBlue);
    _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.Transform);
    _background.Draw(_spriteBatch, gameTime);
    _objectHaley.Draw(_spriteBatch, gameTime);
    _spriteBatch.End();
    base.Draw(gameTime);
  }
}