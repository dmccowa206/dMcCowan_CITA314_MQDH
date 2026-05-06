using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    public void ToggleUI(bool toggle)
    {
        if (canvas != null)
        {
            canvas.enabled = toggle;
        }
    }
}
