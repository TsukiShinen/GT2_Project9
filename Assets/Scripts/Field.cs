using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] GameParameters _parameters;
    [SerializeField] FieldModifier _fieldModifier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) { return; }
        if (!collision.gameObject.CompareTag(_parameters.TagTank)) { return; }
        _fieldModifier.Activate(collision.gameObject.GetComponent<Tank>());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) { return; }
        if (!collision.gameObject.CompareTag(_parameters.TagTank)) { return; }
    }
}
