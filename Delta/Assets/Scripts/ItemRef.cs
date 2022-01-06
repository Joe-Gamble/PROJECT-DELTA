using UnityEngine;

public class ItemRef : MonoBehaviour
{
    private Spawner.ItemDetails m_details;

    public void Init(Spawner.ItemDetails details)
    {
        m_details = details;
        // m_details = details;
        gameObject.layer = 7;

        Camera cam = gameObject.GetComponentInChildren<Camera>();

        if (cam != null)
        {
            cam.gameObject.SetActive(false);
        }

        if (details.item.data.col_size != Vector3.zero)
        {
            BoxCollider bc = gameObject.AddComponent<BoxCollider>();
            bc.size = details.item.data.col_size;
            bc.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if other == compatible entity (e.g player, zombie, cow)
        //only should trigger if item is flagged for passive collection (need to implement)
        //m_details.item.Collect(other.gameObject);
    }

    public Spawner.ItemDetails GetDetails()
    {
        return m_details;
    }

    public PhysicalItem GetData()
    {
        return m_details.item;
    }
}
