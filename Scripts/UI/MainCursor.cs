using UnityEngine;

public class MainCursor : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x + 40, Input.mousePosition.y - 40, Input.mousePosition.z);
    }
}
