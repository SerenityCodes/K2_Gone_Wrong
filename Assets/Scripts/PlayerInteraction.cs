using DefaultNamespace;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCamera;
    public float playerReach = 3f;
    private Interactable _currentInteractable;
    

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.F) && _currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if (_currentInteractable && newInteractable != _currentInteractable)
                {
                    _currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        _currentInteractable = newInteractable;
        _currentInteractable.EnableOutline();
        HUDController.Instance.EnableInteractionText(_currentInteractable.message, _currentInteractable.showKeyBind);
    }

    void DisableCurrentInteractable()
    {
        if (_currentInteractable)
        {
            _currentInteractable.DisableOutline();
            HUDController.Instance.DisableInteractionText();
            _currentInteractable = null;   
        }
    }
}
