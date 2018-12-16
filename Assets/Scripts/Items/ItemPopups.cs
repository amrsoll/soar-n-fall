using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopups : MonoBehaviour {
    public Transform container;
    public GameObject popupPrefab;
	
	void Update () {
        foreach (Transform t in container)
        {
            Destroy(t.gameObject);
        }

        Collider[] near = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider col in near)
        {
            ItemController it = col.GetComponent<ItemController>();
            if (it == null) continue;
            GameObject newPopup = Instantiate<GameObject>(popupPrefab, container);
            newPopup.transform.position = Camera.main.WorldToScreenPoint(col.transform.position + Vector3.up);
            newPopup.GetComponent<TMPro.TextMeshProUGUI>().text = it.type.ToString();
        }
	}
}
