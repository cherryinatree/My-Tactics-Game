using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMonobehavior : MonoBehaviour
{
    public void Movement()
    {
        BattleMaster.RemoteChangeBattleState(BattleMaster.BATTLESTATE.MOVE);
    }

    public void TargetNext()
    {
        Targeting.TargetNext();
        UserInterface.ChanceToHitPanel(BattleMechanics.HitChance(Targeting.focusOnMe, Targeting.target, BattleMechanics.AttackType.Attack));
        UserInterface.ShowCharacterInfo(Targeting.target.GetComponent<CharacterController>().myStats);
    }

    public void TargetPrevious()
    {
        Targeting.TargetPrevious();
        UserInterface.ChanceToHitPanel(BattleMechanics.HitChance(Targeting.focusOnMe, Targeting.target, BattleMechanics.AttackType.Attack));
        UserInterface.ShowCharacterInfo(Targeting.target.GetComponent<CharacterController>().myStats);
    }
    public static void IconButton()
    {
        BattleMaster.ActionChosen = EventSystem.current.currentSelectedGameObject.transform.parent.transform.Find("Panel").GetComponent<Image>().sprite.name;
    }

}
