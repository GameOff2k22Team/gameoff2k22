using System.Collections;
using UnityEngine;

public class FromWorldToInventorySpace : MonoBehaviour, Interaction
{
    [Header("Cameras")]
    public Camera baseCamera;
    public Camera UICamera;

    [Header("Animation Information")]
    public float timeToAnimate;

    private string originalLayer;
    private const string UILayer = "UI"; 
    private const float TIME_TO_WAIT = 0.1f;
    private YieldInstruction yieldInstruction = new WaitForSeconds(TIME_TO_WAIT);

    private void Awake()
    {
        originalLayer = gameObject.layer.ToString();
    }

    public void Interact()
    {
        InventoryManager.Instance.PlaceObjectInInventory(this);
    }

    public void PutObjectInInventory(Transform inventorySlot)
    {
        StartCoroutine(ChangeTransformToInventorySlot(inventorySlot));
    }

    IEnumerator ChangeTransformToInventorySlot(Transform inventorySlot)
    {
        ChangeSpaceToUICamera(inventorySlot);

        float time = 0;

        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        Vector3 scale = Vector3.one;

        while (time < timeToAnimate)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, 
                                                        position, time/timeToAnimate);
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation,
                                                           rotation, time / timeToAnimate);
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, 
                                                     scale, time/timeToAnimate);
            
            yield return yieldInstruction;
            
            time += TIME_TO_WAIT;
        }
    }

    void ChangeSpaceToUICamera(Transform inventorySlot)
    {
        this.transform.SetParent(inventorySlot, true);
        ChangeLayerToUI();
    }

    void ChangeLayerToUI()
    {
        gameObject.layer = LayerMask.NameToLayer(UILayer);
    }

    private void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(originalLayer);
    }
}
