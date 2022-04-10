using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class ClientCard : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private string spriteAddress = "";

    private int id = 0;
    private int rank = 0;
    private int suite = 0;
    private bool isSelected = false;

    public int Id { get => id; }
    public int Rank { get => rank;}
    public int Suite { get => suite;}

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadSprite();
    }
    public void setSortingLayer()
    {
        spriteRenderer.sortingOrder = id;
    }
    private void LoadSprite()
    {
        spriteAddress = SpriteAddress();
        Addressables.LoadAssetAsync<Sprite>(spriteAddress).Completed += SpriteLoaded;
    }
    private string SpriteAddress()
    {
        if(Suite == Suites.Spades)
        {
            return CardSprite.Spades[Rank];
        }
        if(Suite == Suites.Club)
        {
            return CardSprite.Club[Rank];
        }
        if(Suite == Suites.Heart)
        {
            return CardSprite.Heart[Rank];
        }
        if (Suite == Suites.Diamond) 
        { 
            return CardSprite.Diamond[Rank];
        }
        return CardSprite.Back;
    }
    private void SpriteLoaded(AsyncOperationHandle<Sprite> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                spriteRenderer.sprite = obj.Result;
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogError("Sprite load failed.");
                break;
            default: // case AsyncOperationStatus.None:
                break;
        }
    }
    public void Initialize(Card card, int i)
    {
        rank = card.Rank;
        suite = card.Suite;
        id = i;
        setSortingLayer();
        LoadSprite();
        
    }
    public void OnMouseDown()
    {
        if (!isSelected)
        {
            transform.position += Vector3.up * CardSprite.CARD_SELECTED_OFFSET;
            isSelected = true;
        }
        else
        {
            transform.position -= Vector3.up * CardSprite.CARD_SELECTED_OFFSET;
            isSelected = false;
        }
    }
}
