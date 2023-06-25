using System;

namespace RiskofRebalance.Buffs
{
    public class Collapse
    {
        public void Awake()
        {

            try
            {
                //PLEASE change this later, does not work right now?
                RoR2.RoR2Application.onLoad += () =>
                {
                    RoR2.DotController.dotDefs[8].damageCoefficient += 2f;
                };
            }

            catch (Exception e)
            {
                Log.Error(e.Message + " - " + e.StackTrace);
            }
        }
    }
}
