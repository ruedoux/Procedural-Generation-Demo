using Godot;

public partial class MapGenerator
{
  private readonly FastNoiseLite fastNoiseLite = new();

  protected MapGenerator(FastNoiseLite fastNoiseLite)
  {
    this.fastNoiseLite = fastNoiseLite;
  }

  public virtual float GetValueOnPosition(Vector3I vec)
  {
    return fastNoiseLite.GetNoise3D(vec.X, vec.Y, vec.Z);
  }
}