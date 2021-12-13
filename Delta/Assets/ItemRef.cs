using UnityEngine;

public class ItemRef : MonoBehaviour
{
    private PhysicalItem m_Data_ref = null;

    public void Spawn(GameObject parent, PhysicalItem item)
    {
        m_Data_ref = item;
        GameObject obj = GameObject.Instantiate(item.data.prefab, parent.transform.position, this.transform.rotation);
        obj.transform.parent = parent.transform;

        obj.transform.Rotate(0, -90, 0, Space.Self);

        Camera cam = obj.GetComponentInChildren<Camera>();

        if(cam != null)
        {
            cam.gameObject.SetActive(false);
        }
    }

    private void Update() 
    {
        if(m_Data_ref != null)
        {

        }
    }
}
