using UnityEngine;

public class ItemRef : MonoBehaviour
{
    private PhysicalItem m_Data_ref = null;

    public void Init(GameObject go, PhysicalItem item)
    {
        m_Data_ref = item;
        go.transform.Rotate(0, -90, 0, Space.Self);

        Camera cam = go.GetComponentInChildren<Camera>();

        if(cam != null)
        {
            cam.gameObject.SetActive(false);
        }
    }
}
