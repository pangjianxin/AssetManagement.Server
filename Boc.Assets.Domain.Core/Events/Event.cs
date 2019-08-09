using MediatR;
using System;

namespace Boc.Assets.Domain.Core.Events
{
    public abstract class Event : INotification
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }

        #region methods
        public string DateTimeFromNow
        {
            get
            {
                TimeSpan span = DateTime.Now - TimeStamp;
                if (span.TotalDays > 60)
                {
                    return "2个月前";
                }

                if (span.TotalDays > 30)
                {
                    return "1个月前";
                }

                if (span.TotalDays > 14)
                {
                    return "2周前";
                }

                if (span.TotalDays > 7)
                {
                    return "1周前";
                }

                if (span.TotalDays > 1)
                {
                    return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
                }

                if (span.TotalHours > 1)
                {
                    return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                }

                if (span.TotalMinutes > 1)
                {
                    return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                }

                if (span.TotalSeconds >= 1)
                {
                    return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                }
                return "1秒前";
            }
        }
        #endregion  
    }
}