using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public BoxCollider boxColliderReference;
    public Vector3 virtualBoxSize;
    public float distanceFromTarget = 10F;

    Player player;
    MovementArea area;

    private void Awake ()
    {
        player = FindObjectOfType<Player> ();

        boxColliderReference.transform.position = player.transform.position + player.transform.up.normalized * 2F;

        area = new MovementArea (boxColliderReference.bounds, virtualBoxSize, boxColliderReference);
        area.Update (boxColliderReference, virtualBoxSize);

        started = true;
    }

    private void Update ()
    {
        boxColliderReference.transform.position = player.transform.position + player.transform.up.normalized * 2F;
    }

    private void LateUpdate ()
    {
        area.Update (boxColliderReference, virtualBoxSize);
        transform.position = Vector3.Lerp (transform.position, player.transform.up * distanceFromTarget + area.center, 4F * Time.deltaTime);
        transform.LookAt (transform, area.center);
    }

    bool started = false;

    private void OnDrawGizmos ()
    {
        if (started)
        {
            Gizmos.color = new Color (1, 0, 0, .5F);
            Gizmos.DrawCube (area.center, virtualBoxSize);
        }
    }

    private struct MovementArea
    {
        public Vector3 center;

        private Vector3 bottomLeft, bottomRight, topLeft, topRight;
        private float boundsSizeX, boundsSizeY, boundsSizeZ;
        private float halfSizeY;

        public MovementArea (Bounds targetBounds, Vector3 cubeSize, BoxCollider playerCollider)
        {
            boundsSizeX = targetBounds.size.x;
            boundsSizeY = targetBounds.size.y;
            boundsSizeZ = targetBounds.size.z;
            halfSizeY = cubeSize.y * .5F;

            Vector3 globalPos = playerCollider.transform.TransformDirection (playerCollider.transform.position);
            float bottomY = (globalPos.y - boundsSizeY * .5F) + cubeSize.y * .5F;
            float topY = bottomY + cubeSize.y;

            // Left tem a altura de baixo e Right as de cima
            bottomLeft = new Vector3 (globalPos.x - cubeSize.x * .5F, bottomY, globalPos.z - cubeSize.z * .5F);
            bottomRight = new Vector3 (globalPos.x + cubeSize.x * .5F, topY, globalPos.z - cubeSize.z * .5F);

            topLeft = new Vector3 (globalPos.x - cubeSize.x * .5F, bottomY, globalPos.z + cubeSize.z * .5F);
            topRight = new Vector3 (globalPos.x + cubeSize.x * .5F, topY, globalPos.z + cubeSize.z * .5F);

            center = new Vector3 ((bottomLeft.x + bottomRight.x) * .5F, (bottomY + topY) * .5F, (topRight.z + bottomRight.z) * .5F);
        }

        public void Update (BoxCollider collider, Vector3 size)
        {
            Vector3 globalPos = collider.transform.TransformDirection (collider.bounds.center);

            float x = 0F;

            if (globalPos.x - boundsSizeX * .5F < bottomLeft.x)
                x = (globalPos.x - boundsSizeX * .5F) - bottomLeft.x;
            else if (globalPos.x + boundsSizeX * .5F > bottomRight.x)
                x = (globalPos.x + boundsSizeX * .5F) - bottomRight.x;

            bottomLeft.x += x;
            bottomRight.x += x;

            float y = 0F;

            if (globalPos.y + boundsSizeY * .5F > center.y + halfSizeY)
                y = (globalPos.y + boundsSizeY * .5F) - (center.y + halfSizeY);
            else if (globalPos.y - boundsSizeY * .5F < center.y - halfSizeY)
                y = (globalPos.y - boundsSizeY * .5F) - (center.y - halfSizeY);

            bottomLeft.y += y;
            topLeft.y += y;
            bottomRight.y += y;
            topRight.y += y;

            float z = 0F;

            if (globalPos.z - boundsSizeZ * .5F < bottomRight.z)
                z = (globalPos.z - boundsSizeZ * .5F) - bottomRight.z;

            else if (globalPos.z + boundsSizeZ * .5F > topRight.z)
                z = (globalPos.z + boundsSizeZ * .5F) - topRight.z;

            topRight.z += z;
            bottomRight.z += z;

            center = new Vector3 ((bottomLeft.x + bottomRight.x) * .5F, (bottomLeft.y + topRight.y) * .5F, (topRight.z + bottomRight.z) * .5F);
        }
    }
}