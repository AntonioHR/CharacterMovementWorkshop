using UnityEngine;

public class ToggleChildrenOnButton: MonoBehaviour
{
    public int index = 0;
    public KeyCode key;

    private void Start()
    {
        UpdateChildren();
    }
    private void Update()
    {
        if(Input.GetKeyDown(key))
        {
            AdvanceIndex();
            UpdateChildren();
        }
    }

    private void AdvanceIndex()
    {
        index++;
        index = index % transform.childCount;
    }

    private void UpdateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }
}
