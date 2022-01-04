using UnityEngine;

public class ItemRef : MonoBehaviour
{
    private PhysicalItem m_Data_ref = null;

    public void Init(GameObject go, PhysicalItem item)
    {
        m_Data_ref = item;
        go.transform.Rotate(0, -90, 0, Space.Self);

        go.layer = 7;

        Camera cam = go.GetComponentInChildren<Camera>();

        if (item.data.col_size != Vector3.zero)
        {
            BoxCollider bc = go.AddComponent<BoxCollider>();
            bc.size = item.data.col_size;
        }

        if (cam != null)
        {
            cam.gameObject.SetActive(false);
        }
    }
}
