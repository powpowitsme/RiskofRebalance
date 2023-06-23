using System;

namespace RiskofRebalance.Buffs
{
    public class Collapse
    {
        public void Awake()
        {

            try
            {

                RoR2.RoR2Application.onLoad += () =>
                {
                    RoR2.DotController.dotDefs[8].damageCoefficient += 2f;
                };
            }

            catch (Exception e)
            {
                RiskofRebalance.logger.LogError(e.Message + " - " + e.StackTrace);
            }
        }
    }
}
