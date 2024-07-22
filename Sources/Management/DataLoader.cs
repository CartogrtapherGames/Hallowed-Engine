using System.IO;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Hallowed.Management;

/// <summary>
/// the class that will load any non-ContentManager friendly files.
/// </summary>
public class DataLoader
{
  public string RootDirectory = string.Empty;

  /// <summary>
  /// Load the json file and parse it into an object
  /// </summary>
  /// <param name="assetName"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public T Load<T>(string assetName)
  {
    using var stream = TitleContainer.OpenStream(RootDirectory + "/" + assetName);
    using var reader = new StreamReader(stream);
    var jsonString = reader.ReadToEnd();
    var result = JsonConvert.DeserializeObject<T>(jsonString);
    return result;
  }
}