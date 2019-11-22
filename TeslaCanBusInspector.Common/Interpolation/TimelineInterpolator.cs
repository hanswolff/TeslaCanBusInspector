using System;
using TeslaCanBusInspector.Common.Timeline;

namespace TeslaCanBusInspector.Common.Interpolation
{
    public class TimeLineInterpolator : ITimeLineInterpolator
    {
        public void InterpolateTime(MessageTimeLine timeline)
        {
            if (timeline == null) throw new ArgumentNullException(nameof(timeline));
            
            // TODO:
        }
    }

    public interface ITimeLineInterpolator
    {
        void InterpolateTime(MessageTimeLine timeline);
    }
}
