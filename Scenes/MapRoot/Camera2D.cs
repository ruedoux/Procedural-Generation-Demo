using Godot;
using System;

namespace ProceduralGeneration;

public partial class MapCamera : Camera2D
{
  Control UI;
  float maxZoom;
  float minZoom;

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

  //static func is_mouse_on_ui(element:Control) -> bool:
  //  return element.get_global_rect().has_point(element.get_global_mouse_position())

  private static bool IsMouseOnUI(Control element)
    => element.GetGlobalRect().HasPoint(element.GetGlobalMousePosition());

}
