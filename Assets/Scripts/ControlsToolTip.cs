using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlsToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  void Start()
  {
    gameObject.transform.parent.GetChild(0).gameObject.SetActive(false);
  }
  
  

  void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
  {
    gameObject.transform.parent.GetChild(0).gameObject.SetActive(true);
    
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    gameObject.transform.parent.GetChild(0).gameObject.SetActive(false);
    
  }

  

  

  

  
}
