using System.Collections;
using Assets.Utils;
using UnityEngine;

namespace Assets.GameManager {
    public class MainCameraController: MonoBehaviour {
        public Camera MainCamera;
        
        Vector3 MouseStart;
        Vector3 MouseMove;
        public Bounds CameraBounds;
        public Vector3 NewPosition;
        public Character ToTarget;
        private float CameraZoom => MainCamera.orthographicSize;
        private Vector3 CameraPosition => MainCamera.transform.position;
        public float MaxZoom = 7f;
        public float ZoomTime;
        public float ScrollTime;
        public float ScrollSpeed;
        [SerializeField]
        private bool zooming = false;

        void Awake() {
            CameraBounds = OrthographicBounds(MainCamera);
        }

        void LateUpdate() {

            if (ToTarget) {
                TargetCharacter(ToTarget);
                return;

            }
            
            if (zooming) return;
            HandleZoom(Input.GetAxis("Mouse ScrollWheel"));
            HandleDrag();

        }

        public void TargetCharacter(Character character) {
            var zoomTime = 0f;
            var scrollTime = 0f;

            var position = character.transform.position;
            zooming = true;
            ToTarget = null;

            if (position.x == CameraPosition.x &&
                position.y == CameraPosition.y &&
                position.z == CameraPosition.z) {
                    return;
            }
            
            var newPosition = new Vector3(position.x, position.y, position.z);
            // transform.position = newPosition;
            StartCoroutine(ChangeScrollOnSelect(transform, newPosition));
            StartCoroutine(ChangeZoomOnSelect(MainCamera));
            

            IEnumerator ChangeScrollOnSelect(Transform transform, Vector3 finalPosition) {
                while(transform.position != finalPosition) {
                    scrollTime += Time.deltaTime / ScrollTime;
                    transform.position = Vector3.Lerp(transform.position, finalPosition, scrollTime);
                    yield return null;
                }
                yield return null;
            }

            IEnumerator ChangeZoomOnSelect(Camera camera) {
                while(!camera.orthographicSize.Similar(2f, 0.0001f)) {
                    zoomTime += Time.deltaTime / ZoomTime;
                    camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 2f, zoomTime);
                    yield return null;
                }
                zooming = false;
                yield return null;
            }
        }
        
        private void HandleZoom(float scrollMovement) {
            if (scrollMovement == 0)
                return;

            ToTarget = null;
            
            var change = CameraZoom + scrollMovement;
            var newValue = Mathf.Clamp(change, 1, MaxZoom);

            MainCamera.orthographicSize = newValue;
            
            if (scrollMovement < 0) 
                return;

            if (newValue > MaxZoom-0.9) {
                NewPosition = CameraBounds.center;
                transform.position = NewPosition;
                return;
            }
            
            var percentage = CameraZoom / MaxZoom;
            NewPosition = Vector3.Lerp(CameraPosition, CameraBounds.center, 
                                percentage / (CameraBounds.center - CameraPosition).magnitude);
            transform.position = NewPosition;
        }
       
        private void HandleDrag() {
            if (Input.GetMouseButtonDown (0)) {
                MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                return;
            }
            if (!Input.GetMouseButton (0)) {
                return;
            }
            MouseMove = new Vector3(Input.mousePosition.x - MouseStart.x, 
                                    Input.mousePosition.y - MouseStart.y,
                                    Input.mousePosition.z - MouseStart.z);
            MouseStart = new Vector3(Input.mousePosition.x, 
                Input.mousePosition.y, 
                Input.mousePosition.z);

            var newX = transform.position.x + MouseMove.x * (Time.deltaTime / ScrollSpeed);
            var newY = transform.position.y + MouseMove.y * (Time.deltaTime / ScrollSpeed);
            var newZ = transform.position.z + MouseMove.z * (Time.deltaTime / ScrollSpeed);
            NewPosition = new Vector3(newX, newY, newZ);

            if (PointIsInsideBounds(ref NewPosition, CameraBounds)) {
                transform.position = NewPosition;
            }
        }

        private Bounds OrthographicBounds(Camera camera) {
            float cameraHeight = CameraZoom * 2;
            return new Bounds( CameraPosition,
                new Vector3(cameraHeight * camera.aspect, cameraHeight * camera.aspect, cameraHeight));
        }

        private bool PointIsInsideBounds(ref Vector3 point, Bounds bounds) {
            var extents = OrthographicBounds(MainCamera).extents;
            var left = point.x - extents.x > bounds.center.x - bounds.extents.x;
            var right = point.x + extents.x < bounds.center.x + bounds.extents.x;
            
            var bottom = point.y - extents.y > bounds.center.y - bounds.extents.y;
            var top = point.y + extents.y < bounds.center.y + bounds.extents.y;

            if (!left || !right) {
                point = new Vector3(CameraPosition.x, point.y, CameraPosition.z);
            }
            if (!top || !bottom) {
                point = new Vector3(point.x, CameraPosition.y, CameraPosition.z);
            }
            
            return left || right || bottom || top;
        }
    }
}