using UnityEngine;

namespace TheGridMatrix
{
    public class BaseNode : MonoBehaviour
    {
        public int X;
        public int Y;
        public NodeType NodeType = NodeType.Face;
        private BaseNode teleport = null;

        public BaseNode Teleport { get => teleport; set { teleport = value; IsBusy = true; } }
        public bool IsBusy { get; set; } = false;

        public void DisableMesh()
        {
            var meshRenderer = GetComponentInChildren<MeshRenderer>(true);
            if (meshRenderer != null) meshRenderer.enabled = false;
        }
    }

    public enum NodeType
    {
        Vertex,
        Edge,
        Face
    }
}
