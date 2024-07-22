using System;
using Hallowed.Data;

namespace Hallowed.Management;

/// <summary>
/// The Record that load the DataModel of the games, so it can access throughout the game
/// </summary>
public record Database
{
  public static HaleyDataModel HaleyDataModel;

  private static DataLoader _dataLoader;

  public static void Init()
  {
    _dataLoader = new DataLoader
    {
      RootDirectory = "Content/Data"
    };
    LoadAllData();
  }

  private static void LoadAllData()
  {
    LoadHaleyData();
  }

  private static void LoadHaleyData()
  {
    HaleyDataModel = _dataLoader.Load<HaleyDataModel>("Player.json");
  }
}