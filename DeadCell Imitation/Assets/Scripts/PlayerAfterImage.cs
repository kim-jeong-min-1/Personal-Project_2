using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;

    [SerializeField]
    private float setAlpha = 0.8f;
    private float alpha;
    private float alphaMulitply = 0.85f;

    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        SR.sprite = playerSR.sprite;
        alpha = setAlpha;

        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        timeActivated = Time.time;
        StartCoroutine(GrowlessAlpha());
    }
    private void Update()
    {
        if(Time.time >= (timeActivated + activeTime))
        {
            AfterImagePool.Inst.AddPool(gameObject);
        }
    }

    private IEnumerator GrowlessAlpha()
    {
        while(gameObject.activeSelf == true)
        {
            alpha *= alphaMulitply;
            color = new Color(1, 1, 1, alpha);
            SR.color = color;
            yield return new WaitForSeconds(0.015f);
        }    
    }
}
