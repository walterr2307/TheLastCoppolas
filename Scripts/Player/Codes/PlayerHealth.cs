using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private int hp = 100, maxHp = 100;
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = "HP: " + hp + "/" + maxHp;
    }

    public void ChangeHealthPoints(int points)
    {
        hp += points;

        if (hp <= 0)
        {
            hp = 0;
            text.text = "HP: " + hp + "/" + maxHp;
            Destroy(gameObject);
        }
        else if (hp > maxHp)
        {
            hp = maxHp;
            text.text = "HP: " + hp + "/" + maxHp;
        }
    }
}
