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
                var span = DateTime.Now - TimeStamp;
                return $"{span.Days}天,{span.Hours}小时,{span.Minutes}分钟,{span.Seconds}秒";
            }
        }
        #endregion  
    }
}