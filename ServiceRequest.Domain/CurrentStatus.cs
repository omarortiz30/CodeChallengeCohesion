using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRequest.Domain
{
    public enum CurrentStatus
    {
        NotApplicable,
        Created,
        InProgress,
        Complete,
        Canceled
    }
}
