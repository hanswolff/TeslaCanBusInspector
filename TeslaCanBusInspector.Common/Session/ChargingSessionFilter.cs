using System.Collections.Generic;
using TeslaCanBusInspector.Common.Messages;
using IModel3BatteryInfoMessage = TeslaCanBusInspector.Common.Messages.Model3.IBatteryInfoMessage;

namespace TeslaCanBusInspector.Common.Session
{
    public class ChargingSessionFilter : IChargingSessionFilter
    {
        public IEnumerable<MessageTimeline> GetChargingSessions(MessageTimeline timeline)
        {
            var filteredTimeline = new MessageTimeline();

            var currentlyCharging = false;
            foreach (var timedMessage in timeline)
            {
                var chargingActive = IsChargingActive(timedMessage.Value);
                if (chargingActive != null)
                {
                    if (!chargingActive.Value && currentlyCharging)
                    {
                        yield return filteredTimeline;
                        filteredTimeline = new MessageTimeline();
                    }

                    currentlyCharging = chargingActive.Value;
                }

                if (currentlyCharging)
                {
                    filteredTimeline.Add(timedMessage);
                }
            }

            if (currentlyCharging)
            {
                yield return filteredTimeline;
            }
        }

        private static bool? IsChargingActive(ICanBusMessage message)
        {
            if (message is IModel3BatteryInfoMessage batteryInfoMessage)
            {
                return batteryInfoMessage.BmsChargeStatus == 1 ||
                       batteryInfoMessage.BmsState == 2 ||
                       batteryInfoMessage.BmsState == 3;
            }

            return null;
        }
    }

    public interface IChargingSessionFilter
    {
        IEnumerable<MessageTimeline> GetChargingSessions(MessageTimeline timeline);
    }
}
