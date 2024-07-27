using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hallowed.Core.Display;

/// <summary>
///  The class that allows to group and transform multiple Renderable Object at the same time.
/// <example>
/// <code>
/// var container = new Container();
/// container.AddChild(sprite);
/// </code>
/// </example>
/// </summary>
/// TODO : add auto Resizing based on the children boundaries
///
public class Container : IRenderableChild
{
  private List<IRenderableChild> _children;

  private float _scale;
  private Vector2 _position;
  public bool Enabled { get; set; } = true;

  public Container()
  {
    _children = new List<IRenderableChild>();
  }


  public void AddChild(IRenderableChild child)
  {
    _children.Add(child);
  }

  public void AddChild(IRenderableChild[] children)
  {
    for (var i = 0; i < children.Length; i++)
    {
      _children.Add(children[i]);
    }
  }

  public void AddChild(List<IRenderableChild> children)
  {
    for (var i = 0; i < children.Count; i++)
    {
      _children.Add(children[i]);
    }
  }

  public void Update(GameTime delta)
  {
    foreach (var child in _children)
    {
      child.Update(delta);
    }
  }

  public void Draw(SpriteBatch batch, GameTime delta)
  {
    foreach (var child in _children)
    {
      if (!child.Enabled) continue;
      child.Draw(batch, delta);
    }
  }

  public void Dispose()
  {
    foreach (var child in _children)
    {
      child.Dispose();
    }
  }

  public int Width { get; set; }

  private void RequestTransformRefresh()
  {
  }
}