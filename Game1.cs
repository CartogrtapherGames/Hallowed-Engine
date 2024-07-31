using System.Diagnostics;
using Hallowed.Core;
using Hallowed.Core.Display;
using Hallowed.Management;
using Hallowed.Objects.Haley;
using Hallowed.Scenes;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;

namespace Hallowed;

public class Game1 : SceneBase
{
  private SpriteBatch _spriteBatch;
  private ObjectHaley _objectHaley;
  private Sprite _background;
  private Camera _camera;
  private bool _toolActive = true;
  protected ObjectBatch ObjectBatch;
  private Screen _screen;

  public static ImGuiRenderer GuiRenderer;

  public Game1()
  {
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    Database.Init();
    _screen = new Screen(this, 1920, 1080);
    ObjectBatch = new ObjectBatch(this);
    GuiRenderer = new ImGuiRenderer(this);
    // _camera = new Camera(Graphics.Width, Graphics.Height);
    _background = new Sprite();
    _background.Anchor = new Vector2(0.5f, 0.5f);
    _background.X = Graphics.Width * 0.5f;
    _background.Y = Graphics.Height * 0.5f;

    _objectHaley = new ObjectHaley(Database.HaleyDataModel, InputMap);
    _objectHaley.X = Graphics.Width * 0.5f;
    _objectHaley.Y = (Graphics.Height * 0.5f);
    _objectHaley.Sprite.SetScale(4f, 4f);
    // TODO: Add your initialization logic here

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

    GuiRenderer.RebuildFontAtlas();
    // TODO: use this.Content to load your game content here
  }

  protected override void Update(GameTime gameTime)
  {
    _objectHaley.Update(gameTime);
    // _background.Update(gameTime);
    //_camera.Follow(_objectHaley);
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
        Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    if (InputMap.IsPressed(Keys.W))
    {
      _background.Y -= 3;
    }

    if (InputMap.IsTriggered(Keys.Space))
    {
    }

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    _screen.Set();
    GraphicsDevice.Clear(Color.CornflowerBlue);

    /*
    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
    _background.Draw(_spriteBatch, gameTime);
    _objectHaley.Draw(_spriteBatch, gameTime);
    _spriteBatch.End();
    */
    ObjectBatch.Begin(false);
    ObjectBatch.Draw(_objectHaley, gameTime);
    ObjectBatch.End();
    DrawDebugTool(gameTime);
    _screen.UnSet();
    _screen.Present(ObjectBatch, false);
    base.Draw(gameTime);
  }

  private void DrawDebugTool(GameTime gameTime)
  {
    var state = _objectHaley.StateMachine.CurrentState.StateKey.ToString();
    GuiRenderer.BeginLayout(gameTime);
    if (_toolActive)
    {
      ImGui.Begin("My First Tool", ref _toolActive, ImGuiWindowFlags.MenuBar);
      ImGui.SetWindowSize(new System.Numerics.Vector2(500, 500));
      if (ImGui.BeginMenuBar())
      {
        if (ImGui.BeginMenu("File"))
        {
          if (ImGui.MenuItem("Open..", "Ctrl+O"))
          {
            /* Do stuff */
          }

          if (ImGui.MenuItem("Save", "Ctrl+S"))
          {
            /* Do stuff */
          }

          if (ImGui.MenuItem("Close", "Ctrl+W"))
          {
            _toolActive = false;
          }

          ImGui.EndMenu();
        }

        ImGui.EndMenuBar();
      }

      ImGui.SeparatorText("Basic Metadata");
      ImGui.BeginGroup();
      ImGui.Text("Entity ID: " + _objectHaley.Id);
      ImGui.Text("Class Name: " + _objectHaley.GetType().Name);
      ImGui.Text("GroupId: " + _objectHaley.GroupId);
      var objectHaleyTransform = _objectHaley.Transform;
      var ObjectHaleyRotation = _objectHaley.Sprite.Rotation;
      ImGui.SliderFloat("X", ref objectHaleyTransform.X, 0, Graphics.Width);

      ImGui.SliderFloat("Y", ref objectHaleyTransform.Y, 0, Graphics.Height);
      _objectHaley.Transform = objectHaleyTransform;

      ImGui.SliderAngle("Rotation", ref ObjectHaleyRotation, 0, 360);
      _objectHaley.Sprite.Rotation = ObjectHaleyRotation;
      if (ImGui.Button("Center Sprite"))
      {
        _objectHaley.X = Graphics.Width * 0.5f;
        _objectHaley.Y = Graphics.Height * 0.5f;
      }

      ImGui.EndGroup();
      ImGui.BeginGroup();
      ImGui.SeparatorText("State Machine");
      ImGui.Text("Current State: ");
      ImGui.SameLine();
      ImGui.Text(state);
      ImGui.Text("Current Direction: " + _objectHaley.Direction.ToString());
      ImGui.EndGroup();
      ImGui.End();
    }

    GuiRenderer.EndLayout();
  }
}