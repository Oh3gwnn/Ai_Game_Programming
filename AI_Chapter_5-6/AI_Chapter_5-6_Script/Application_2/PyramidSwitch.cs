using UnityEngine;

public class PyramidSwitch : MonoBehaviour
{
    public Material onMaterial;
    public Material offMaterial;

    GameObject m_Area;
    PyramidArea m_AreaComponent;
    int m_PyramidIndex;


    void Start()
    {
        m_Area = gameObject.transform.parent.gameObject;
        m_AreaComponent = m_Area.GetComponent<PyramidArea>();
    }

    public void ResetSwitch(int spawnAreaIndex, int pyramidSpawnIndex)
    {
        m_AreaComponent.PlaceObject(gameObject, spawnAreaIndex);
        m_PyramidIndex = pyramidSpawnIndex;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
