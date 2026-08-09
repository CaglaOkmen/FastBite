using UnityEngine;
using System.Collections.Generic;

public class MouseInteraction : MonoBehaviour
{
    Vector3 mousePosition;
    RaycastHit2D raycastHit2D;
    Transform clickObject;
    
    Camera cam;
    GameObject selectedItem;
    Vector3 offset;
    bool isDragging = false;
    ItemMotion itemMotion;
    public Transform dropZone;

    public Vector2 dropZoneSize = new Vector2(2f, 3f);
    List<GameObject> placedItems = new List<GameObject>();
    

    void Start()
    {
        cam = Camera.main;
        itemMotion = FindObjectOfType<ItemMotion>();
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown(mousePosition, mouseRay);
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            HandleDrag();
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            HandleMouseUp();
        }
    }

    void HandleMouseDown(Vector3 mousePosition, Ray mouseRay)
    {
        raycastHit2D = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
        clickObject = raycastHit2D ? raycastHit2D.collider.transform : null;

        if (clickObject)
        {
            selectedItem = clickObject.gameObject;
            isDragging = true;
            itemMotion.SetSelectedItem(selectedItem);
            if (placedItems.Contains(selectedItem))
            {
                placedItems.Remove(selectedItem);
                Rigidbody2D rb = selectedItem.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
    
    void HandleDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectedItem.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
    
    void HandleMouseUp()
    {
        if (IsInDropZone(selectedItem.transform.position))
        {
            PlaceItem(selectedItem);
        }
        
        selectedItem = null;
        isDragging = false;
        itemMotion.SetSelectedItem(null);
    }

    bool IsInDropZone(Vector3 position)
    {
        if (dropZone == null) return false;
        
        Vector3 zonePos = dropZone.position;
        float halfWidth = dropZoneSize.x / 2f;
        float halfHeight = dropZoneSize.y / 2f;
        
        return (position.x >= zonePos.x - halfWidth && position.x <= zonePos.x + halfWidth &&
                position.y >= zonePos.y - halfHeight && position.y <= zonePos.y + halfHeight);
    }

    // Siralama yapilacak. Son giren ilk cikacak sekilde duzenlenecek.
    void PlaceItem(GameObject item)
    {
        if (!placedItems.Contains(item))
        {
            placedItems.Add(item);
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}