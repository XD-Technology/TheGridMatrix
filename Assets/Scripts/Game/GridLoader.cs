using System.Collections.Generic;
using UnityEngine;

namespace TheGridMatrix
{
    public class GridLoader : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] [Range(5, 10)] private int x;
        [SerializeField] [Range(8, 16)] private int y;

        [Header("Spawn Settings")]
        [SerializeField] private Vector2 spawnInterval = new Vector2(3, 6);
        [SerializeField] private Vector2 spawnCountInTick = new Vector2(1, 2);
        [SerializeField] private int maxAllowed = 5;
        [SerializeField] private float timer = 0f;

        [Header("Spawn Objects Settings")]
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject score;
        [SerializeField] private GameObject endGame;

        [Header("Reference")]
        [SerializeField] private GameObject grid;

        private List<BaseNode> faceNodes = new List<BaseNode>();
        [SerializeField] private List<GameSpawnObject> busyNodes = new List<GameSpawnObject>();

        private void Start()
        {
            BaseNode[,] gridArray = new BaseNode[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var obj = Instantiate(grid, transform);
                    obj.transform.localPosition = new Vector3(i, j);
                    var node = obj.AddComponent<BaseNode>();
                    node.X = i;
                    node.Y = j;

                    if (i != 0 && i != x - 1 && j != 0 && j != y - 1) obj.name = "FaceNode_";
                    else
                    {
                        obj.name = "EdgeNode_";

                        if ((i != 0 || j != 0) && (i != 0 || j != y - 1) && (i != x - 1 || j != y - 1) && (i != x - 1 || j != 0)) node.NodeType = NodeType.Edge;
                        else node.NodeType = NodeType.Vertex;
                    }

                    obj.name += "(" + i + "-" + j + ")";
                    gridArray[i, j] = node;
                }
            }

            var camera = Camera.main;
            camera.transform.position = new Vector3((x / 2f) - 0.5f, (y / 2f) - 0.5f, -10f);
            faceNodes = new List<BaseNode>();
            busyNodes = new List<GameSpawnObject>();

            foreach (var node in gridArray)
            {
                int i = node.X;
                int j = node.Y;

                if (node.NodeType == NodeType.Face)
                {
                    faceNodes.Add(node);
                }
                else if (node.NodeType == NodeType.Edge)
                {
                    if (i == 0) node.Teleport = gridArray[x - 1, j]; //left
                    else if (i == x - 1) node.Teleport = gridArray[0, j]; //right
                    else if (j == 0) node.Teleport = gridArray[i, y - 1]; //bottom
                    else if (j == y - 1) node.Teleport = gridArray[i, 0]; //top

                    node.DisableMesh();
                }
                else
                {
                    Destroy(node.gameObject);
                }
            }
        }

        private void FixedUpdate()
        {
            if (GameManager.Pause) return;

            timer += Time.fixedDeltaTime;

            if (timer >= spawnInterval.x)
            {
                bool canSpawn = Random.Range(0, 100) > 30 || timer >= spawnInterval.y;
                if (canSpawn)
                {
                    int spawnCount = (int)Random.Range(spawnCountInTick.x - 1, spawnCountInTick.y + 1);
                    spawnCount = spawnCount <= 0 ? 1 : spawnCount;
                    for (int i = 0; i < spawnCount; i++)
                    {
                        var freeNode = GetRandomFreeNode();
                        freeNode.IsBusy = true;
                        var obj = Random.Range(0, 100) > 31f ? score : endGame;
                        var objData = Instantiate(obj, parent).GetComponent<GameSpawnObject>();
                        objData.transform.position = freeNode.transform.position;
                        objData.OnDestroySpawn += () => { busyNodes.Remove(objData); freeNode.IsBusy = false; };
                        busyNodes.Add(objData);
                    }
                    timer = 0;
                }
            }

            DestroyOld();
        }

        private void DestroyOld()
        {
            foreach (var data in busyNodes)
            {
                if (data != null) data.Age += Time.fixedDeltaTime;
            }
        }

        private BaseNode GetRandomFreeNode()
        {
            var freeNodes = new List<BaseNode>();

            foreach (var node in faceNodes)
            {
                if (node != null && !node.IsBusy) freeNodes.Add(node);
            }

            var index = Random.Range(-1, freeNodes.Count);
            return freeNodes[index];
        }
    }
}
