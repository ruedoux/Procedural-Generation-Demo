using System;
using Godot;

namespace ProceduralGeneration;

public partial class MapCamera : Camera2D
{
  private Control UI;
  private float maxZoom;
  private float minZoom;

  public Vector2 maxCameraPosition = Vector2.Zero;
  public float zoomValue = 0.05f;

  public MapCamera(Control UI, float maxZoom = 8.0f, float minZoom = 0.5f)
  {
    this.UI = UI;
    this.maxZoom = maxZoom;
    this.minZoom = minZoom;
  }


  public override void _Input(InputEvent inputEvent)
  {
    if (IsMouseOnUI(UI))
      return;

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
    Vector2 finalPosition = GlobalPosition + direction;

    finalPosition.X = Math.Clamp(finalPosition.X, 0, maxCameraPosition.X);
    finalPosition.Y = Math.Clamp(finalPosition.Y, 0, maxCameraPosition.Y);

    GlobalPosition = finalPosition;
  }

  private void ZoomCamera(float value)
  {
    if (Zoom.X + value < minZoom)
      return;
    if (Zoom.X + value > maxZoom)
      return;
    Zoom = new Vector2(Zoom.X + value, Zoom.Y + value);
  }

  private static bool IsMouseOnUI(Control element)
    => element.GetGlobalRect().HasPoint(element.GetGlobalMousePosition());

}
