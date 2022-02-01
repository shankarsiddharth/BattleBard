using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboManager : MonoBehaviour
{
    public LaneManager lane_manager;

    public List<Combo> valid_combos;
    public List<char> drumsHit;

    private int _cam_lane = 0;

    private void Start()
    {
        EventManager.OnCameraMove += OnCameraMove;
    }

	void CheckCombo()
    {
        foreach (Combo validCombo in valid_combos)
        {
            List<char> reversedOrder = new List<char>(validCombo.comboOrder);
            reversedOrder.Reverse();

            if (reversedOrder.Count > drumsHit.Count)
                continue;

            List<char> sublist = drumsHit.GetRange(0, reversedOrder.Count);

            // If they aren't equal size, move to the next combo to check
            if (reversedOrder.Count != sublist.Count)
                continue;

            bool equals = true;
            for (int i=0; i<reversedOrder.Count; i++)
            {
                if (reversedOrder[i] != sublist[i])
                {
                    equals = false;
                    break;
                }
            }

            if (equals)
            {
                EventManager.RaiseLaneComboComplete(validCombo.effect, lane_manager.lanes[_cam_lane], validCombo.affectsAllies, validCombo.affectsEnemies);
                print(validCombo);

                char lastDrum = drumsHit[drumsHit.Count - 1];
                drumsHit.Clear();
                drumsHit.Add(lastDrum);
            }
        }
    }

    #region Keyboard Events
    public void LeftThigh(InputAction.CallbackContext context)
    {
        if (context.performed)
            drumsHit.Insert(0, '1');
        CheckCombo();
    }

    public void RightThigh(InputAction.CallbackContext context)
    {
        if (context.performed)
            drumsHit.Insert(0, '3');
        CheckCombo();
    }

    public void Stomach(InputAction.CallbackContext context)
    {
        if (context.performed)
            drumsHit.Insert(0, '5');
        CheckCombo();
    }

    public void LeftShoulder(InputAction.CallbackContext context)
    {
        if (context.performed)
            drumsHit.Insert(0, '7');
        CheckCombo();
    }

    public void RightShoulder(InputAction.CallbackContext context)
    {
        if (context.performed)
            drumsHit.Insert(0, '9');
        CheckCombo();
    }

    // TODO: This input needs to be managed elsewhere...
    public void Pedal(InputAction.CallbackContext context)
    {
        if (context.performed)
            EventManager.RaiseForceCameraMovement(_cam_lane+1);
    }
    #endregion

    #region Events
    private void OnCameraMove(int lane)
    {
        _cam_lane = lane;
    }
    #endregion
}