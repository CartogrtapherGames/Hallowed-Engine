using Hallowed.Core;
using Hallowed.Management;
using Hallowed.Objects;
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
  private AnimatedSprite _king;
  private ObjectHaley _objectHaley;


  public Game1()
  {
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    Database.Init();

    _objectHaley = new ObjectHaley(Database.HaleyDataModel, InputMap);
    _objectHaley.Pivot = new Vector2(0.5f, 0f);
    _objectHaley.X = Graphics.Width * 0.5f;
    _objectHaley.Y = Graphics.Height * 0.5f;
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

    var haleyTexture = Content.Load<Texture2D>("Yula");
    _objectHaley.SetTexture(haleyTexture);

    var texture = Content.Load<Texture2D>("witch");
    _witch.Texture = texture;


    var kingTexture = Content.Load<Texture2D>("king1");
    var frameSize = new Area2D(32, 32);

    _king = new AnimatedSprite(kingTexture, frameSize, new Point(0, 1), 8)
    {
      X = Graphics.Width * 0.5f,
      Y = Graphics.Height * 0.5f,
    };
    _king.SetAnchor(0.5f);
    _king.Rotation = 0;


    _king.SetScale(4f, 4f);
    _king.AddAnimation("idle", new FrameRange(1, 0), 4, true);
    _king.AddAnimation("walk", new FrameRange(1, 5), 4, true);
    // TODO: use this.Content to load your game content here
  }

  protected override void Update(GameTime gameTime)
  {
    _witch.Update(gameTime);
    _objectHaley.Update(gameTime);
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
        Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    if (InputMap.IsTriggered(PlayerInput.Space))
    {
      _witch.SetScale(4f, 4f);
      _witch.Rotation += 45;
      _king.Play("idle");
    }

    if (InputMap.IsPressed(Keys.D))
    {
      _witch.X += 10;
    }

    if (InputMap.IsTriggered(Keys.LeftShift))
    {
      _king.Play("walk");
    }

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.CornflowerBlue);
    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
    _witch.Draw(_spriteBatch, gameTime);
    //  _king.Draw(_spriteBatch, gameTime);
    _objectHaley.Draw(_spriteBatch, gameTime);
    _spriteBatch.End();
    base.Draw(gameTime);
  }


  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
  }
}