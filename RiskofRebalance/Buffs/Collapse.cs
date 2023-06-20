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
#pragma warning disable Publicizer001
                    RoR2.DotController.dotDefs[8].damageCoefficient += 2f;
#pragma warning restore Publicizer001
                };
            }

            catch (Exception e)
            {
                MainFile.logger.LogError(e.Message + " - " + e.StackTrace);
            }
        }
    }
}
