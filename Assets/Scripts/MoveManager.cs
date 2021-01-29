using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class Node{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class MoveManager : MonoBehaviour
{
    //타일맵 전체 사이즈
    [SerializeField] private Vector2Int bottomLeft, topRight;
    private Vector2Int startPos, targetPos;
    public List<Node> FinalNodeList = new List<Node>();

    int sizeX, sizeY;
    private Node[,] NodeArray;
    private Node StartNode, TargetNode, CurNode;
    private List<Node> OpenList, ClosedList;

    public void PathFinding(Transform startPos, Vector2Int targetPos)
    {
        FinalNodeList.Clear();
        this.targetPos = targetPos;
        this.startPos = new Vector2Int((int) startPos.position.x, (int) startPos.position.y);
        PathFinding();
    }

    public void PathFinding()
    {
        // NodeArray의 크기 정해주고, isWall, x, y 대입
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
                        isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }


        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() {StartNode};
        ClosedList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
                    CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 마지막
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }

                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                for (int i = 0; i < FinalNodeList.Count; i++)
                {
                    //Debug.Log(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                }

                return;
            }

            //랜덤성 부여하면 끝
            // ↑ → ↓ ←

            List<int[]> random = new List<int[]>();
            random.Add(new int[2]{0, 1});
            random.Add(new int[2]{0, -1});
            random.Add(new int[2]{1, 0});
            random.Add(new int[2]{-1, 0});
            Shuffle(random);
            for (int i = 0; i < 4; i++)
            {
                OpenListAdd(CurNode.x + random[0][0], CurNode.y + random[0][1]);
                random.RemoveAt(0);
            }

            // OpenListAdd(CurNode.x, CurNode.y + 1);
            // OpenListAdd(CurNode.x + 1, CurNode.y);
            // OpenListAdd(CurNode.x, CurNode.y - 1);
            // OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }
    
    public void Shuffle<T>(List<T> list)
    {
        int size = list.Count;
        for (int index = 0; index < size; index++)
        {
            int randIndex = Random.Range(index, size);  // index ~ size -1
            T temp = list[index];
            list[index] = list[randIndex];
            list[randIndex] = temp;
        }
    }


    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 &&
            !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall &&
            !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) *
                                 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }
    
    //해당 칸으로 이동하는 함수
    public void Move(Transform startPos, Node tagetNode){
        startPos.position = Vector2.MoveTowards(startPos.position, new Vector2(tagetNode.x, tagetNode.y), 1f);
        //애니메이션 추가
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0)
            for (int i = 1; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y),
                    new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
}