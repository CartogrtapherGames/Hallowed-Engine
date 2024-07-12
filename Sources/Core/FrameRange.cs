namespace Hallowed.Core;

/// <summary>
/// the public record that handles the range of an animations
/// </summary>
/// <param name="Row"> the row the animations is positioned</param>
/// <param name="Column"> the column the animations is positioned</param>
public record FrameRange(int Row = 0, int Column = 0);