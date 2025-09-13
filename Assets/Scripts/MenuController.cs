using UnityEngine;
using UnityEngine.EventSystems; 

public class MenuController : MonoBehaviour
{
    
    public GameObject firstSelectedButton;

   
    private void OnEnable()
    {
        // limpiar selecciones anteriores
        EventSystem.current.SetSelectedGameObject(null);
        
        // Asignamos nuestro botón por defecto como el objeto seleccionado.
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

   
   
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }
}