﻿namespace Assets.Scripts.Manager
{
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public float PanSpeed = 30f;
        public float PanborderThickness = 10f;
        public float ScrollSpeed = 5f;

        private bool canMove = false;
        private float minY = 15f;
        private float maxY = 200f;
        private float minAngle = 25f;
        private float maxAngle = 60f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.canMove = !canMove;
            }

            if (!canMove)
            {
                return;
            }

            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.UpArrow) ||
                Input.mousePosition.y >= Screen.height - PanborderThickness)
            {
                this.MoveCamera(Vector3.forward);
            }
            if (Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.DownArrow) ||
                Input.mousePosition.y <= PanborderThickness)
            {
                this.MoveCamera(Vector3.back);
            }
            if (Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.RightArrow) ||
                Input.mousePosition.x >= Screen.width - PanborderThickness)
            {
                this.MoveCamera(Vector3.right);
            }
            if (Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.LeftArrow) ||
                Input.mousePosition.x <= PanborderThickness)
            {
                this.MoveCamera(Vector3.left);
            }

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            var pos = this.transform.position;

            pos.y -= scroll * this.ScrollSpeed * Time.deltaTime * 1000;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            this.transform.position = pos;

            this.transform.rotation = Quaternion.Euler(this.GetAngle(pos.y), 0, 0);
        }

        private void MoveCamera(Vector3 direction)
        {
            transform.Translate(direction * this.PanSpeed * Time.deltaTime, Space.World);
        }

        private float GetAngle(float y)
        {
            var porcentage = (y - minY) / (maxY - minY);
            var angle = ((maxAngle - minAngle) * porcentage) + minAngle;
            Debug.Log(angle);
            return angle;
        }
    }
}