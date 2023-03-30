using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;//Döngü sayısını belirler.
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = gameObject.transform.position;
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {//0 a bölünme hatasını engellemek için. Period 0 olursa nesne durur.
            return;
        }

        float cycles = Time.time / period; //Time.time nekadar zaman geçtiğini verir. Burda devamlı zaman büyür.

        const float tau = Mathf.PI * 2; // değişmez değer 6.283'ün.
        float rawSinWave = Mathf.Sin(cycles * tau);// -1 den 1 e gider.

        movementFactor = (rawSinWave + 1f) / 2f;// 0 dan 1 e giderken tekrardan hesaplanır.

        Vector3 offset = movementVector * movementFactor;
        gameObject.transform.position = startingPosition + offset;
    }
}
