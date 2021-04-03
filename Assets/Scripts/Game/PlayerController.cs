using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGridMatrix
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private Rigidbody rb = null;
        [SerializeField] private Vector3 direction = Vector3.right;
        [SerializeField] private Vector3 newDirection = Vector3.zero;
        [SerializeField] private bool changeDirection = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            GameManager.Pause = false;
        }

        private void FixedUpdate()
        {
            if (GameManager.Pause) return;
            rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * moveSpeed);
        }

        private void Update()
        {
            if (GameManager.Pause) return;
            KeyboardControl();
        }

        private void KeyboardControl()
        {
            if (GameManager.Pause) return;

            if (!changeDirection)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    changeDirection = true;
                    newDirection = Vector3.up;
                }
                else if (direction != Vector3.down && Input.GetKeyDown(KeyCode.DownArrow))
                {
                    changeDirection = true;
                    newDirection = Vector3.down;
                }
                else if (direction != Vector3.right && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    changeDirection = true;
                    newDirection = Vector3.right;
                }
                else if (direction != Vector3.left && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    changeDirection = true;
                    newDirection = Vector3.left;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                var node = other.GetComponent<BaseNode>();
                
                if (node != null)
                {
                    if (node.Teleport != null && node.NodeType == NodeType.Edge)
                    {
                        transform.position = new Vector3(node.Teleport.transform.position.x, node.Teleport.transform.position.y, transform.position.z) + direction / 2f;
                    }
                    else
                    {
                        if (changeDirection)
                        {
                            changeDirection = false;
                            direction = newDirection;
                            transform.position = new Vector3(node.transform.position.x, node.transform.position.y, transform.position.z);
                        }

                        GameManager.Score++;
                    }

                }

                var spawnData = other.GetComponent<GameSpawnObject>();
                if (spawnData != null) spawnData.OnCollideSpawn();
            }
        }
    }
}