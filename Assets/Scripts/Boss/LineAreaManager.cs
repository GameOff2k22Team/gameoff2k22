using System.Collections.Generic;
using UnityEngine;

public class LineAreaManager : Singleton<LineAreaManager>
{
    public List<GameObject> LineAreaDamageHorizontal = new List<GameObject>();
    public List<GameObject> LineAreaDamageVertical = new List<GameObject>();

    private List<GameObject> tempListHori = new List<GameObject>();
    private List<GameObject> tempListVert = new List<GameObject>();


    public void ToggleLines(List<int> VertLines, List<int> HorLines, float timeBeforeDamage, float timeWithDamage,bool enable = true)
    {
        
        for(int i=0; i < VertLines.Count; i++)
        {
            LineAreaDamageVertical[VertLines[i]].GetComponent<LineAreaDamage>().Initialize(timeBeforeDamage, timeWithDamage);
            LineAreaDamageVertical[VertLines[i]].SetActive(enable);
        }
        for(int j=0; j<HorLines.Count; j++)
        {
            LineAreaDamageHorizontal[HorLines[j]].GetComponent<LineAreaDamage>().Initialize(timeBeforeDamage, timeWithDamage);
            LineAreaDamageHorizontal[HorLines[j]].SetActive(enable);
        }
    }

}
