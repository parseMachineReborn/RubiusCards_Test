using Rubius.Enums;
using System;

namespace Rubius.Models
{
    [Serializable]
    public sealed class LoadTypeConfiguration
    {
        public bool IsSelected;
        public LoadType LoadType;
    }
}