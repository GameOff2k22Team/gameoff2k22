using System.Collections;
using UnityEngine;

public class GoToReturnFromInventory : Interaction
{
    [Header("Object Inoformations")]
    public Collider[] colliderToDeactivate;

    [Header("Animation Information")]
    public float timeToAnimate;

    private int originalLayer;
    private Transform originalParent;
    private Vector3 originalScale;
    private const string UILayer = "UI"; 
    private const float TIME_TO_WAIT = 0.1f;
    private YieldInstruction yieldInstruction = new WaitForSeconds(TIME_TO_WAIT);

    protected override void Awake()
    {
        base.Awake();

        originalLayer = gameObject.layer;
        originalScale = transform.localScale;
        originalParent = transform.parent;
    }

    public override void Interact()
    {
        InventoryManager.Instance.PlaceObjectInInventory(this);
    }

    public void PutObjectInInventory(Transform inventorySlot)
    {
        ChangeSpaceToUICamera(inventorySlot);

        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        Vector3 scale = Vector3.one;

        HandleCollider(false);

        StartCoroutine(ChangePosition(position, rotation, scale));
    }

    public void RemoveObjectFromInventory(Vector3 position)
    {
        StartCoroutine(RemoveObjectFromInventorySpace(position));
    }

    IEnumerator ChangePosition(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        float time = 0;

        while (time < timeToAnimate)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,
                                                        position, time / timeToAnimate);
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation,
                                                           rotation, time / timeToAnimate);
            this.transform.localScale = Vector3.Lerp(this.transform.localScale,
                                                     scale, time / timeToAnimate);

            yield return yieldInstruction;

            time += TIME_TO_WAIT;
        }
    }

    IEnumerator RemoveObjectFromInventorySpace(Vector3 position)
    {
        this.transform.SetParent(originalParent, true);

        yield return ChangePosition(position, 
                                    transform.rotation, 
                                    originalScale);

        HandleCollider(true);
        ResetLayer();
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
        gameObject.layer = originalLayer;
    }

    private void HandleCollider(bool activate)
    {
        foreach (Collider collider in colliderToDeactivate)
        {
            collider.enabled = activate;
        }
    }
}
