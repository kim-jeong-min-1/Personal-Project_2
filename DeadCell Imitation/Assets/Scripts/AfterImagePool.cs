using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImage;

    private Queue<GameObject> afterObjects = new Queue<GameObject>();
    public static AfterImagePool Inst { get; private set; }
    private void Awake()
    {
        Inst = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(afterImage);
            obj.transform.SetParent(transform);
            AddPool(obj);
        }
    }

    public void AddPool(GameObject obj)
    {
        obj.SetActive(false);
        afterObjects.Enqueue(obj);
    }

    public GameObject ReturnPool()
    {
        if(afterObjects.Count == 0)
        {
            GrowPool();
        }
        GameObject obj = afterObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }
}
