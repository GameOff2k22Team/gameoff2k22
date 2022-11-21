using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GoToReturnFromInventory : MonoBehaviour
{
    [Header("Object Inoformations")]
    public Collider[] colliderToDeactivate;
    public bool interactOnlyOnce;

    [Header("Animation Information")]
    public float timeToAnimate;

    [Header("Go To Inventory information")]
    public Vector3 inventoryPosition = Vector3.zero;
    public Vector3 inventoryRotation = Vector3.zero;
    public Vector3 inventoryScale = Vector3.one;

    [Header("Return from Inventory information")]
    public UnityEvent OnRemovedDone;

    private int originalLayer;
    private Transform originalParent;
    private Vector3 originalScale;
    private const string UILayer = "UI"; 
    private const float TIME_TO_WAIT = 0.1f;
    private YieldInstruction yieldInstruction = new WaitForSeconds(TIME_TO_WAIT);
    private bool hasInteracted;

    protected void Awake()
    {
        originalLayer = gameObject.layer;
        originalScale = transform.localScale;
        originalParent = transform.parent;
    }

    public void GoToInventory()
    {
        InventoryManager.Instance.PlaceObjectInInventory(this);
    }

    public void PutObjectInInventory(Transform inventorySlot)
    {
        if (!interactOnlyOnce || (interactOnlyOnce && !hasInteracted))
        {
            ChangeSpaceToUICamera(inventorySlot);

            HandleCollider(false);

            StartCoroutine(ChangePosition(inventoryPosition, 
                                          Quaternion.Euler(inventoryRotation), 
                                          inventoryScale));

            hasInteracted = true;
        }
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

        OnRemovedDone?.Invoke();
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
