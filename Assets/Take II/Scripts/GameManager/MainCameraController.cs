using UnityEngine;

namespace Assets.Take_II.Scripts.GameManager {
    public class MainCameraController: MonoBehaviour {
        public Camera MainCamera;
        
        Vector3 MouseStart;
        Vector3 MouseMove;
        public Bounds CameraBounds;
        public Vector3 NewPosition;
        public Character ToTarget;
        private float CameraZoom => MainCamera.orthographicSize;
        private Vector3 CameraPosition => MainCamera.transform.position;

        void Awake() {
            CameraBounds = OrthographicBounds(MainCamera);
        }

        void LateUpdate() {
            HandleZoom(Input.GetAxis("Mouse ScrollWheel"));
            HandleDrag();

            if (ToTarget) {
                TargetCharacter(ToTarget);
            }
        }

        public void TargetCharacter(Character character) {
            var position = character.transform.position;
            
            if (position.x == CameraPosition.x &&
                position.y == CameraPosition.y) {
                    return;
            }

            ToTarget = character;
            MainCamera.orthographicSize = 1.5f;
            var newPosition = new Vector3(position.x, position.y, -10);
            if(PointIsInsideBounds(ref newPosition, CameraBounds)) {
                transform.position = newPosition;
            }
        }
        
        private void HandleZoom(float scrollMovement) {
            if (scrollMovement == 0)
                return;

            ToTarget = null;
            
            var change = CameraZoom + scrollMovement;
            var newValue = Mathf.Clamp(change, 1, 4);

            MainCamera.orthographicSize = newValue;
            
            if (scrollMovement < 0) 
                return;

            if (newValue > 3.9) {
                NewPosition = CameraBounds.center;
                transform.position = NewPosition;
                return;
            }
            
            var percentage = CameraZoom / 4f;
            NewPosition = Vector3.Lerp(CameraPosition, CameraBounds.center, 
                                percentage / (CameraBounds.center - CameraPosition).magnitude);
            transform.position = NewPosition;
        }
       
        private void HandleDrag() {
            if (Input.GetMouseButtonDown (0)) {
                MouseStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                return;
            }
            if (!Input.GetMouseButton (0)) {
                return;
            }
            MouseMove = new Vector2(Input.mousePosition.x - MouseStart.x, 
                                    Input.mousePosition.y - MouseStart.y);
            MouseStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            var newX = transform.position.x + MouseMove.x * Time.deltaTime;
            var newY = transform.position.y + MouseMove.y * Time.deltaTime;
            NewPosition = new Vector3(newX, newY, -10);

            if (PointIsInsideBounds(ref NewPosition, CameraBounds)) {
                transform.position = NewPosition;
            }
        }

        private Bounds OrthographicBounds(Camera camera) {
            float screenAspect = camera.aspect;
            float cameraHeight = CameraZoom * 2;
            return new Bounds( CameraPosition,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        }

        private bool PointIsInsideBounds(ref Vector3 point, Bounds bounds) {
            var extents = OrthographicBounds(MainCamera).extents;
            var left = point.x - extents.x > bounds.center.x - bounds.extents.x;
            var right = point.x + extents.x < bounds.center.x + bounds.extents.x;
            var bottom = point.y - extents.y > bounds.center.y - bounds.extents.y;
            var top = point.y + extents.y < bounds.center.y + bounds.extents.y;

            if (!left || !right) {
                point = new Vector3(CameraPosition.x, point.y, -10);
            }
            if (!top || !bottom) {
                point = new Vector3(point.x, CameraPosition.y, -10);
            }
            
            return left || right || bottom || top;
        }
    }
}