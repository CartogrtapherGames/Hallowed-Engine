using System.Diagnostics;
using Hallowed.Scenes;
using Microsoft.Xna.Framework;

namespace Hallowed.Core;

public static class Graphics
{
  private static GraphicsDeviceManager _graphics;

  //TODO : edit so I can use JSON data to change my setting so its not hard coded etc we can also use that as default
  private static Area2D _screenSize;
  private static bool _fullScreen;

  public static void Init(SceneBase instance, int width = 1920, int height = 1080, bool fullscreen = false)
  {
    _screenSize = new Area2D(width, height);
    _fullScreen = fullscreen;
    _graphics = new GraphicsDeviceManager(instance);
    OnScreenRefresh();
  }

  private static void OnScreenRefresh()
  {
    _graphics.IsFullScreen = _fullScreen;
    _graphics.PreferredBackBufferWidth = Width;
    _graphics.PreferredBackBufferHeight = Height;
    _graphics.ApplyChanges();
  }

  #region Fields

  // in this case, we shouldn't be allowing edits
  public static GraphicsDeviceManager GdManager => _graphics;

  public static Area2D ScreenSize
  {
    get => _screenSize;
    set
    {
      _screenSize = value;
      OnScreenRefresh();
    }
  }

  // in this case, we shouldn't allow editing the screen size?
  public static int Width => _screenSize.Width;
  public static int Height => _screenSize.Height;

  #endregion
}