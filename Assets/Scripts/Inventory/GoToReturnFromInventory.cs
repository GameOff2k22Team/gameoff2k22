using DG.Tweening;
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

    public Ease ease;
    [Header("Return from Inventory information")]
    public UnityEvent OnRemovedDone;

    private int originalLayer;
    private Vector3 originalScale;
    private const string UILayer = "UI"; 
    private bool hasInteracted;

    protected void Awake()
    {
        originalLayer = gameObject.layer;
        originalScale = gameObject.transform.localScale;
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

            transform.DOLocalMove(inventoryPosition, timeToAnimate).SetEase(ease);
            transform.DOLocalRotate(inventoryRotation, timeToAnimate).SetEase(ease);
            transform.DOScale(inventoryScale, timeToAnimate).SetEase(ease);

            hasInteracted = true;
        }
    }

    public void RemoveObjectFromInventory(Vector3 position,
                                          Vector3 rotation)
    {
        transform.SetParent(null, true);

        transform.DOLocalMove(position, timeToAnimate).SetEase(ease).OnComplete(CompleteRemoval);
        transform.DOLocalRotate(rotation, timeToAnimate).SetEase(ease);
        transform.DOScale(originalScale, timeToAnimate).SetEase(ease);
    }

    void ChangeSpaceToUICamera(Transform inventorySlot)
    {
        this.transform.SetParent(inventorySlot, true);
        ChangeLayerToUI();
    }

    void CompleteRemoval()
    {
        HandleCollider(true);
        ResetLayer();
        OnRemovedDone?.Invoke();
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
