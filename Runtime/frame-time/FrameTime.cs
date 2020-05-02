using UnityEngine;

namespace BeatThat.FrameTime
{
    public static class FrameTimeUtils
    {

        /// <summary>
        /// The beat for a given frame number and frames per beat
        /// </summary>
        /// <returns>The time.</returns>
        /// <param name="frame">The frame number.</param>
        /// <param name="framesPerTimeUnit">Frames per second.</param>
        public static float FrameToTime(this int frame, int framesPerTimeUnit)
        {
            return SnapTimeToFrame(frame / (float)framesPerTimeUnit, framesPerTimeUnit);
        }

		public static float Loop(this float beat, float loopLen)
		{
			if(beat < loopLen) {
				return beat;
			}
			return beat - (loopLen * Mathf.Floor(beat / loopLen));
		}

        /// <summary>
        /// Snaps a time to a standardized frame time.
        /// The exact times of sub beats are fixed so that they land on the same floating value regardless of the base beat, e.g.
        /// for 16 beats/frame would be something like 
        /// 
        /// (
        /// 1.0, 1.0625, 1.125, ..., 
        /// 2.0, 2.0625, 2.125, ..., 
        /// 9999999999.0, 9999999999.0625, 9999999999.125, ...
        /// )
        /// </summary>
        /// <returns>The given time snapped to a frame</returns>
        /// <param name="time">the time</param>
        /// <param name="framesPerTimeUnit">Frames per second.</param>
        public static float SnapTimeToFrame(this float time, int framesPerTimeUnit)
        {
            var baseTime = Mathf.FloorToInt(time);
            var framesAfterTime = TimeToFrame(time, framesPerTimeUnit) - TimeToFrame(baseTime, framesPerTimeUnit); // use float arithmetic only on 'framesAfterBeat' for consistent precision
            return baseTime + ((1f / framesPerTimeUnit) * (float)framesAfterTime);
        }
        
        /// <summary>
        /// The frame number for a given time and frames/sec
        /// </summary>
        /// <returns>the frame number</returns>
        /// <param name="time">time.</param>
        /// <param name="framesPerTimeUnit">Frames per second.</param>
        public static int TimeToFrame(this float time, int framesPerTimeUnit)
        {
            return Mathf.RoundToInt(time * framesPerTimeUnit);
        }

        public static int TimeToFrame(this int time, int framesPerTimeUnit)
        {
            return Mathf.RoundToInt(time * framesPerTimeUnit);
        }
    }
}
