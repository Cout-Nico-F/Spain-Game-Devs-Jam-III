using UnityEngine;

public abstract class Potion : MonoBehaviour
{
    protected int canCombineWith;
    protected int resultPotionId;

    public abstract void Combine();
    public abstract void OnMouseEnter();
    public abstract void OnMouseDown();

    public void OnHouseTouch()
    {
        Destroy(this.gameObject); //TODO: if we use object pooling, remember to change here too.
    }
}