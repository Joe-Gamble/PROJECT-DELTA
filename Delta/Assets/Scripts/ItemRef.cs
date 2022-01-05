using UnityEngine;

public class ItemRef : MonoBehaviour
{
    private PhysicalItem m_Data_ref = null;

    public void Init(GameObject go, PhysicalItem item)
    {
        m_Data_ref = item;
        go.layer = 7;
        m_Data_ref.runtime_ref = go;

        Camera cam = go.GetComponentInChildren<Camera>();

        if (item.data.col_size != Vector3.zero)
        {
            BoxCollider bc = go.AddComponent<BoxCollider>();
            bc.size = item.data.col_size;
            bc.isTrigger = true;
        }

        if (cam != null)
        {
            cam.gameObject.SetActive(false);
        }
    }

    public PhysicalItem GetData()
    {
        return m_Data_ref;
    }
}
