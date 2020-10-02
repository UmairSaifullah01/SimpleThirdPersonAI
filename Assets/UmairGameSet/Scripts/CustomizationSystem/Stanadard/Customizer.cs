public abstract class Customizer
{
    public int currentIndex;
    public int Selected=0;
    protected int maxIndex;

    public bool MoveNext()
    {
        currentIndex++;
        if (currentIndex >= maxIndex)
            currentIndex = maxIndex - 1;
        Show();
        return currentIndex < maxIndex;
    }

    public bool MoveBack()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = 0;
        Show();

        return currentIndex > 0;
    }

    public abstract CustomizationInfluence CurrentInfluence();
    public abstract CustomizeItem CurrentItem();
    public abstract void LoadData();
    public abstract void Select();

    protected virtual void Show()
    {
    }
}


// [System.Serializable]
// public class StickerCustomizer : Customizer
// {
//     public Transform SideStikerTransfrom;
//     private StickerCustomizeItem[] stickerCustomizeItems;
//
//     public override CustomizationInfluence CurrentInfluence()
//     {
//         return stickerCustomizeItems[currentIndex].influence;
//     }
//
//     public override void LoadData()
//     {
//         stickerCustomizeItems = CustomizeDataContainer.instance.stickerData.Elements;
//         maxIndex = stickerCustomizeItems.Length;
//     }
//
//     protected override void Show()
//     {
//         var StickerOject = stickerCustomizeItems[currentIndex].StickerObject;
//         if (SideStikerTransfrom.childCount > 0)
//         {
//             foreach (Transform item in SideStikerTransfrom)
//             {
//                 Object.Destroy(item.gameObject);
//             }
//         }
//
//         Object.Instantiate(StickerOject, SideStikerTransfrom.position, SideStikerTransfrom.rotation,
//             SideStikerTransfrom);
//     }
// }