using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

class PathNode
{
    public int posX;
    public int posZ;
    public PathNode prevNode;
    public int cost;
    public PathNode(int x, int z, PathNode prev, int cost)
    {
        posX = x;
        posZ = z;
        prevNode = prev;
        this.cost = cost;
        
    }
   
}
public class UDEnemyTraceComponent : UDActionComponent
{
    
    private UDGameboard gameBoard;

    public int searchLength = 3;
    public int moveLength = 1;
    private Dictionary<Vector2Int, PathNode> open;
    private List<PathNode> nodes;
    private PathNode resultNode;

    public Vector3 tracePos = Vector3.zero;
    private void Awake()
    {
        open = new Dictionary<Vector2Int, PathNode>();
        nodes = new List<PathNode>();
        actionType = ge.ActionType.TRACE;
        gameBoard = FindObjectOfType<UDGameboard>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Action()
    {
        Debug.Log("FSM_Trace_Action");
        //이동


        
        //Vector3 relativePos = tracePos - transform.position;
        //
        //if (0.5f < relativePos.magnitude)
        //{
        //    Vector3 roundedRelative = new Vector3(Mathf.Round(relativePos.x), 0, Mathf.Round(relativePos.z));
        //    Vector3 largeer = Mathf.Abs(relativePos.x) < Mathf.Abs(relativePos.z) ? new Vector3(0, 0, relativePos.z) : new Vector3(relativePos.x, 0, 0);
        //    largeer.Normalize();
        //    //transform.Translate(largeer);
        //    //if MoveAble
        
        gameBoard.Move(transform.position.x, transform.position.z, resultNode.posX, resultNode.posZ);

        Vector3 pos = transform.position;
        pos.x = resultNode.posX;
        pos.z = resultNode.posZ;

        Vector3 dir = pos - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
        transform.position = pos;
    }

    private void AddNode(Vector2Int pos, PathNode prev)
    {
        int currentCost = prev.cost + 1;
        if (searchLength < currentCost) 
        {
            return;
        }

        if (gameBoard.IsTraceType(pos.x, pos.y))
        {
            if (open.ContainsKey(pos))
            {
                if (currentCost < open[pos].cost)
                {
                    open.Add(pos, new PathNode(pos.x, pos.y, prev, currentCost));
                }
            }
            else
            {
                nodes.Add(new PathNode(pos.x, pos.y, prev, currentCost));
                open.Add(pos, new PathNode(pos.x, pos.y, prev, currentCost));
            }
        }
    }
    private void NodeSearch(PathNode prev)
    {
        {
            Vector2Int left = new Vector2Int(prev.posX - 1, prev.posZ);
            Vector2Int right = new Vector2Int(prev.posX + 1, prev.posZ);
            Vector2Int forward = new Vector2Int(prev.posX, prev.posZ + 1);
            Vector2Int back = new Vector2Int(prev.posX, prev.posZ - 1);

            AddNode(left, prev);
            AddNode(right, prev);
            AddNode(forward, prev);
            AddNode(back, prev);

            
        }
    }

    public bool Check()
    {
        nodes.Clear();
        open.Clear();
        Vector3 pos = transform.position;
        int posX = Mathf.RoundToInt(pos.x);
        int posZ = Mathf.RoundToInt(pos.z);
        PathNode pathNode = new PathNode(posX, posZ, null, 0);

        Debug.Log("FSM_Trace_Check");
        Vector3 playerPos = gameBoard.player.transform.position;
        Vector3 reletivePos = playerPos - pos;
        if (Mathf.Abs(reletivePos.x)+Mathf.Abs(reletivePos.z) <= searchLength )
        {
            //찾기pos, new PathNode(pos.x, pos.y, prev, currentCost)
            nodes.Add(pathNode);
            open.Add(new Vector2Int(posX, posZ), pathNode);
            for (int i = 0; i < nodes.Count; i++)
            {
                NodeSearch(nodes[i]);
            }

            Vector2Int targetPos = new Vector2Int((int)playerPos.x, (int)playerPos.z);
            PathNode retPath;
            if (open.TryGetValue(targetPos,out retPath))
            {
                resultNode = retPath;

                while (moveLength < resultNode.cost)
                {
                    resultNode = resultNode.prevNode;
                }
                if (gameBoard.IsNoneType(resultNode.posX,resultNode.posZ))
                {
                    Action();
                    return true;
                }
            }

        }
        
         return false;
    }
}

