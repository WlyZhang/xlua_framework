using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public class GridMaster : MonoBehaviour
{
    //Setting up the grid
    public int maxX = 20;
    public int maxY = 1;
    public int maxZ = 20;

    public float offsetX = 1f;
    public float offsetY = 1f;
    public float offsetZ = 1f;


    public Node[,,] grid; // our grid
    public List<Node> nodes = new List<Node>();

    [HideInInspector]
    public List<GameObject> notNavList;

    public GameObject gridFloorPrefab;

    [SerializeField]
    [Tooltip("首先需要将【AstarTag】属性的值添加到【Tag】列表中，其次将寻路障碍模型【Tag】设置为此属性值")]
    private string astarTag = "Astar3D";

    void Start()
    {
        InitNodes();
    }

    /// <summary>
    /// 初始化Astar3D寻路寻路网格
    /// </summary>
    private void InitNodes()
    {
        //获取障碍物模型
        this.notNavList = GetTagNodes();

        //The typical way to create a grid
        grid = new Node[maxX, maxY, maxZ];

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int z = 0; z < maxZ; z++)
                {
                    //Apply the offsets and create the world object for each node
                    float posX = x * offsetX;
                    float posY = y * offsetY;
                    float posZ = z * offsetZ;
                    GameObject go = Instantiate(gridFloorPrefab, new Vector3(posX, posY, posZ),
                        Quaternion.identity) as GameObject;
                    //Rename it
                    go.transform.name = x.ToString() + " " + y.ToString() + " " + z.ToString();
                    //and parent it under this transform to be more organized
                    go.transform.parent = transform;

                    //Create a new node and update it's values
                    Node node = new Node();
                    node.x = x;
                    node.y = y;
                    node.z = z;
                    node.worldObject = go;

                    nodes.Add(node);

                    //then place it to the grid
                    grid[x, y, z] = node;

                    AddNodeBehaviour(node);

                    SetNodeRender(node);
                }
            }
        }
    }

    /// <summary>
    /// 添加Node组件
    /// </summary>
    /// <param name="node"></param>
    public void AddNodeBehaviour(Node node)
    {

        AstarBehaviour astarBehaviour = node.worldObject.GetComponent<AstarBehaviour>();

        try
        {
            if (astarBehaviour == null)
            {
                astarBehaviour = node.worldObject.AddComponent<AstarBehaviour>();
            }
            else
            {
                astarBehaviour = node.worldObject.GetComponent<AstarBehaviour>();
            }
        }
        catch
        {
            astarBehaviour = node.worldObject.AddComponent<AstarBehaviour>();
        }
    }

    /// <summary>
    /// 设置不可寻路
    /// </summary>
    public void SetNodesWithNoNavition(Node node)
    {
        if (node != null)
        {
            node.isWalkable = false;

            SetNodeRender(node);
        }
        else
        {
            Debug.Log($"node is null: =============>> {node.worldObject.name}");
        }
    }


    /// <summary>
    /// 设置自身颜色
    /// </summary>
    /// <param name="color"></param>
    public void SetNodeRender(Node node)
    {
        Renderer renderer = node.worldObject.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.color = node.isWalkable ? Color.white : Color.red;
        }
    }


    /// <summary>
    /// 获取Tag模型列表
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetTagNodes()
    {
        if (!string.IsNullOrEmpty(astarTag))
        {
            GameObject[] goArr = GameObject.FindGameObjectsWithTag(astarTag);

            List<GameObject> goList = new List<GameObject>(goArr);

            return goList;
        }

        return null;
    }

    /// <summary>
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="callback"></param>
    public void StartNavition(Vector3 startPos, Vector3 endPos, Pathfinder.PathfindingJobComplete callback = null)
    {
        Node start = GetNodeFromVector3(startPos);
        Node end = GetNodeFromVector3(endPos);

        Pathfinder newJob = new Pathfinder(start, end, callback);
        newJob.FindPath();
    }


    public Node GetNode(int x, int y, int z)
    {
        //Used to get a node from a grid,
        //If it's greater than all the maximum values we have
        //then it's going to return null

        Node retVal = null;

        if (x < maxX && x >= 0 &&
            y >= 0 && y < maxY &&
            z >= 0 && z < maxZ)
        {
            retVal = grid[x, y, z];
        }

        return retVal;
    }

    public Node GetNodeFromVector3(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        int z = Mathf.RoundToInt(pos.z);

        Node retVal = GetNode(x, y, z);
        return retVal;
    }

    //Singleton
    public static GridMaster _instance;
    public static GridMaster Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    void Awake()
    {
        _instance = this;
    }
}
