using UnityEngine.EventSystems;
using UnityEngine;

public class buttonhover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    private Vector3 original_pos;
    SoundPlayer sp;

    public void Start()
    {
        sp = FindObjectOfType<SoundPlayer>();
    }

    public void OnPointerEnter(PointerEventData eventData)

    {
        original_pos = transform.position;
        mouse_over = true;
        transform.position += Vector3.forward * -5;
        sp.PlaySound(0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        transform.position = original_pos;
    }

    public void hoveropt()

    {
        mouse_over = false;
        transform.position = original_pos;

    }
}

