using DefaultNamespace;
using UnityEngine;

public class GeneratorInteract : MonoBehaviour
{
    private Interactable _interactable;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponent<Interactable>();
    }

    public void ChangeMessage()
    {
        _interactable.message = "Extract CO Canister";
        _interactable.showKeyBind = true;
    }
}
