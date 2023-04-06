using System;
using System.Collections.Generic;
using System.Linq;

namespace Mentat.Domain.Interfaces
{
    public interface IBashTestConfig : ITestConfig
    {
        public int NumberOfTests { get; }
        public bool ColorText { get; }
        public bool HighlightText { get; }
        public bool ApplyTextModifiers { get; }

    }
}
