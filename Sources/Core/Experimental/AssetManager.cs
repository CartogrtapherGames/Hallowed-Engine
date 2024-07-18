using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core.Experimental;

// TODO : I do not know enough about services yet  but I understand they are important in Monogame.
// a proof of concept for a new Asset database system that would be more similar to how Pixijs or some other framework approach asset loading.
public class AssetManager : IDisposable
{
  private GraphicsDevice _graphicsDevice;

  private Dictionary<string, object> _loadedAsset = new Dictionary<string, object>();

// todo : im not sure how to use the IDisposable component yet so lets not take it in considerations yet.
  private List<IDisposable> _disposablesAssets = new List<IDisposable>();

  private string _rootDirectory = string.Empty;


  public AssetManager(GraphicsDevice graphicsDevice)
  {
    _graphicsDevice = graphicsDevice;
  }

  /// <summary>
  /// Load the texture
  /// </summary>
  /// <example>
  /// <code>
  /// // we keep it similar to the old content pipeline
  /// var texture = AssetManager.Load<Texture2D>("myAsset.png");
  /// // in this case we keep the extension for clarity
  /// </code>
  /// </example>
  /// <param name="assetPath"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public T Load<T>(string assetPath)
  {
    // for the moment we only load png and the path as to be the pure path including the extension.
    if (typeof(T) == typeof(Texture2D) && assetPath.Contains(".png"))
    {
      Texture2D texture = LoadTexture(assetPath);
      var key = assetPath.Split(".")[0];
      _loadedAsset.Add(key, texture);
      return (T)_loadedAsset[key];
    }

    throw new Exception(typeof(T).Name + " is not a valid loadable assets!");
  }

  // if we know the assets is already loaded we dont need to reload and we can just fetch it.
  // TODO : we could also just compact it in the load function to make it easier.
  public T Get<T>(string assetName)
  {
    if (_loadedAsset.ContainsKey(assetName))
    {
      return (T)_loadedAsset[assetName];
    }

    throw new Exception("The asset " + assetName + " does not exist!");
  }

  // here we load  the texture or at least try if theres exceptions
  private Texture2D LoadTexture(string assetPath)
  {
    Texture2D texture;
    try
    {
      texture = Texture2D.FromFile(_graphicsDevice, @$"{_rootDirectory}/{assetPath}");
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }

    // here we premultiply the png.
    Color[] data = new Color[texture.Width * texture.Height];
    texture.GetData(data);

    for (int i = 0; i < data.Length; i++)
    {
      Color c = data[i];
      data[i] = new Color(c.R, c.G, c.B, c.A) * (c.A / 255.0f);
    }

    texture.SetData(data);
    return texture;
  }

  public void Dispose()
  {
    _graphicsDevice?.Dispose();
    GC.SuppressFinalize((object)this);
  }

  // we flush all the assets here that can be disposed of course
  public virtual void FlushAll()
  {
    // here we flush all the assets and their references
  }

  public virtual void Flush(string assetName)
  {
  }
}