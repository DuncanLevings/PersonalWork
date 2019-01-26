using Pathfinding;
using UnityEngine;

public class GenerateNavGraph : MonoBehaviour
{
  public float nodeSize;
  public float Diameter;
  [Tooltip("Any obstacles in the dungeon, ie. walls ect")]
  public int CollisionMaskNum;
  [Tooltip("Ground, Ramps, ect")]
  public int HeightMaskNum;
  public int BufferWidthSize;
  public int BufferDepthSize;
  private int width;
  private int depth;
  private Vector3 centerBounds;

  public GenerateNavGraph()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    if (Object.op_Inequality((Object) DungeonStreaming.Instance, (Object) null))
      return;
    GridGraph gridGraph = AstarPath.active.astarData.AddGraph(typeof (GridGraph)) as GridGraph;
    this.getDimensions();
    gridGraph.width = this.width;
    gridGraph.depth = this.depth;
    gridGraph.nodeSize = this.nodeSize;
    gridGraph.center = new Vector3((float) this.centerBounds.x, -0.1f, (float) this.centerBounds.z);
    gridGraph.UpdateSizeFromWidthDepth();
    gridGraph.collision.diameter = this.Diameter;
    gridGraph.collision.mask = LayerMask.op_Implicit(1 << this.CollisionMaskNum);
    gridGraph.collision.heightMask = LayerMask.op_Implicit(1 << this.HeightMaskNum);
    AstarPath.active.Scan();
  }

  public void CustomStart()
  {
    GridGraph gridGraph = AstarPath.active.astarData.AddGraph(typeof (GridGraph)) as GridGraph;
    this.getDimensions();
    gridGraph.width = this.width;
    gridGraph.depth = this.depth;
    gridGraph.nodeSize = this.nodeSize;
    gridGraph.center = new Vector3((float) this.centerBounds.x, -0.1f, (float) this.centerBounds.z);
    gridGraph.UpdateSizeFromWidthDepth();
    gridGraph.collision.diameter = this.Diameter;
    gridGraph.collision.mask = LayerMask.op_Implicit(1 << this.CollisionMaskNum);
    gridGraph.collision.heightMask = LayerMask.op_Implicit(1 << this.HeightMaskNum);
    AstarPath.active.Scan();
  }

  private void getDimensions()
  {
    Renderer[] objectsOfType = (Renderer[]) Object.FindObjectsOfType<Renderer>();
    if (objectsOfType.Length == 0)
      return;
    Bounds bounds = objectsOfType[0].get_bounds();
    for (int index = 1; index < objectsOfType.Length; ++index)
      ((Bounds) ref bounds).Encapsulate(objectsOfType[index].get_bounds());
    this.width = (int) ((Bounds) ref bounds).get_size().x + this.BufferWidthSize;
    this.depth = (int) ((Bounds) ref bounds).get_size().z + this.BufferDepthSize;
    this.centerBounds = ((Bounds) ref bounds).get_center();
  }
}
