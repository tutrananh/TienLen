using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class ClientPlayer : MonoBehaviour
{
    [SerializeField]
    protected GameObject cardPrefab;

    [SerializeField]
    private int id; 

    private List<ClientCard> hand = new List<ClientCard>();


    void Start()
    {
        for(int i = 0; i < 3;i++)
        {
            GameObject o = GameObject.Instantiate(cardPrefab, transform.position + Vector3.right * i*CardSprite.CARD_ON_HAND_OFFSET, transform.rotation);
            o.transform.SetParent(transform);
            Card test = new Card(i, i);
            ClientCard card = o.GetComponent<ClientCard>();
            card.Initialize(test,i);
            hand.Add(card);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
