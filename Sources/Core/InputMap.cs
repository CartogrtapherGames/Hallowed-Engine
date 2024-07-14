using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hallowed.Core;

#nullable disable
// TODO : make it not a static class later? since it would make senses that the keyboard is bound to the scene class and passed along their object
public class InputMap<T> where T : System.Enum
{
// for the moment it works as a basic class im to tired blegh

  private readonly Dictionary<T, List<AbstractKey>> _actions = new();

  private KeyboardState _oldState;
  private KeyboardState _newState;


  public void BindAction(T name, Keys key)
  {
    var input = new AbstractKey()
    {
      Type = InputType.Keyboard,
      Index = (int)key
    };

    if (_actions.ContainsKey(name))
    {
      _actions[name].Add(input);
    }
    else
    {
      var list = new List<AbstractKey>() { input };
      _actions.Add(name, list);
    }
  }

  public void BindAction(T name, Keys[] keys)
  {
    if (_actions.ContainsKey(name))
    {
      foreach (var t in keys)
      {
        var input = new AbstractKey()
        {
          Type = InputType.Keyboard,
          Index = (int)t
        };
        _actions[name].Add(input);
      }
    }
    else
    {
      var list = ConvertToList(keys, InputType.Keyboard);
      _actions.Add(name, list);
    }
  }

  public void BindAction(T name, List<Keys> keys)
  {
    if (_actions.ContainsKey(name))
    {
      var list = ConvertToList(keys, InputType.Keyboard);
      var result = list.Concat(_actions[name]).ToList();
      _actions[name] = result;
    }
    else
    {
      var list = ConvertToList(keys, InputType.Keyboard);
      _actions.Add(name, list);
    }
  }

  public void BindAction(T name, Buttons button)
  {
    var input = new AbstractKey()
    {
      Type = InputType.Gamepad,
      Index = (int)button
    };
    if (_actions.ContainsKey(name))
    {
      _actions[name].Add(input);
    }
    else
    {
      var list = new List<AbstractKey>() { input };
      _actions.Add(name, list);
    }
  }

  public void BindAction(T name, Buttons[] buttons)
  {
    if (_actions.ContainsKey(name))
    {
      foreach (var button in buttons)
      {
        var input = new AbstractKey()
        {
          Type = InputType.Gamepad,
          Index = (int)button
        };
        _actions[name].Add(input);
      }
    }
    else
    {
      var list = ConvertToList(buttons, InputType.Gamepad);
      _actions.TryAdd(name, list);
    }
  }

  public void BindAction(T name, List<Buttons> buttons)
  {
    if (_actions.ContainsKey(name))
    {
      var list = ConvertToList(buttons, InputType.Gamepad);
      var result = list.Concat(_actions[name]).ToList();
      _actions[name] = result;
    }
    else
    {
      var list = ConvertToList(buttons, InputType.Gamepad);
      _actions.Add(name, list);
    }
  }

  public bool IsPressed(T name)
  {
    if (!_actions.ContainsKey(name)) throw new Exception($"the action {name} does not exists!");
    var action = _actions[name];
    foreach (var input in action)
    {
      switch (input.Type)
      {
        case InputType.Keyboard:
        {
          var key = (Keys)input.Index;
          if (GetState().IsKeyDown(key))
          {
            return true;
          }

          break;
        }
        case InputType.Gamepad:
        {
          var button = (Buttons)input.Index;
          if (GetGamepadState().IsButtonDown(button))
          {
            return true;
          }

          break;
        }
      }
    }

    return false;
  }

  public bool IsPressed(Keys key)
  {
    return GetState().IsKeyDown(key);
  }

  public bool IsUp(Keys key)
  {
    return GetState().IsKeyUp(key);
  }

  public bool IsTriggered(Keys key)
  {
    return GetState().IsKeyDown(key) && _oldState.IsKeyUp(key);
  }

  public void Update()
  {
    _newState = GetState();
    _oldState = _newState;
  }

  private static KeyboardState GetState() => PlatformGetState();
  private GamePadState GetGamepadState() => GamePad.GetState(PlayerIndex.One);

  private static KeyboardState PlatformGetState()
  {
    return Keyboard.GetState();
  }

  #region ListConverter

  private static List<AbstractKey> ConvertToList(List<Keys> keys, InputType type)
  {
    var list = new List<AbstractKey>();
    foreach (var key in keys)
    {
      var input = new AbstractKey()
      {
        Type = type,
        Index = (int)key
      };
    }

    return list;
  }

  private static List<AbstractKey> ConvertToList(Keys[] keys, InputType type)
  {
    var list = new List<AbstractKey>();
    foreach (var key in keys)
    {
      var input = new AbstractKey()
      {
        Type = type,
        Index = (int)key
      };
    }

    return list;
  }

  private static List<AbstractKey> ConvertToList(List<Buttons> buttons, InputType type)
  {
    var list = new List<AbstractKey>();
    foreach (var button in buttons)
    {
      var input = new AbstractKey()
      {
        Type = type,
        Index = (int)button
      };
    }

    return list;
  }

  private static List<AbstractKey> ConvertToList(Buttons[] buttons, InputType type)
  {
    var list = new List<AbstractKey>();
    foreach (var button in buttons)
    {
      var input = new AbstractKey()
      {
        Type = type,
        Index = (int)button
      };
    }

    return list;
  }

  #endregion
}

internal struct AbstractKey
{
  public InputType Type;
  public int Index;
}

internal enum InputType
{
  Keyboard,
  Gamepad
}