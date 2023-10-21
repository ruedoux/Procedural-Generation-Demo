using Godot;
using System;

namespace ProceduralGeneration;

public partial class Camera2D : Godot.Camera2D
{
  const float MAX_ZOOM = 8.0f;
  const float MIN_ZOOM = 0.5f;

  public float zoomValue = 0.05f;

  public override void _Input(InputEvent inputEvent)
  {
    if (inputEvent is InputEventMouseMotion mouseMotion)
      if (mouseMotion.ButtonMask == MouseButtonMask.Middle)
        Position -= mouseMotion.Relative;

    if (inputEvent is InputEventMouseButton mouseButton)
    {
      if (mouseButton.ButtonIndex == MouseButton.WheelDown)
        ZoomCamera(-zoomValue);
      if (mouseButton.ButtonIndex == MouseButton.WheelUp)
        ZoomCamera(zoomValue);
    }
  }

  private void ZoomCamera(float value)
  {
    if (Zoom.X + value < MIN_ZOOM)
      return;
    if (Zoom.X + value > MAX_ZOOM)
      return;
    Zoom = new Vector2(Zoom.X + value, Zoom.Y + value);
  }
}
