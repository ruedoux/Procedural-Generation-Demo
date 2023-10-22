using Godot;
using System;

namespace ProceduralGeneration;

public partial class MapCamera : Camera2D
{
  float maxZoom;
  float minZoom;

  public float zoomValue = 0.05f;

  public MapCamera(float maxZoom = 8.0f, float minZoom = 0.5f)
  {
    this.maxZoom = maxZoom;
    this.minZoom = minZoom;
  }

  public override void _Input(InputEvent inputEvent)
  {
    if (inputEvent is InputEventMouseMotion mouseMotion)
      if (mouseMotion.ButtonMask == MouseButtonMask.Middle)
        MoveCamera(-mouseMotion.Relative);

    if (inputEvent is InputEventMouseButton mouseButton)
    {
      if (mouseButton.ButtonIndex == MouseButton.WheelDown)
        ZoomCamera(-zoomValue);
      if (mouseButton.ButtonIndex == MouseButton.WheelUp)
        ZoomCamera(zoomValue);
    }
  }

  private void MoveCamera(Vector2 direction)
  {


    Position += direction;
  }

  private void ZoomCamera(float value)
  {
    if (Zoom.X + value < minZoom)
      return;
    if (Zoom.X + value > maxZoom)
      return;
    Zoom = new Vector2(Zoom.X + value, Zoom.Y + value);
  }
}
