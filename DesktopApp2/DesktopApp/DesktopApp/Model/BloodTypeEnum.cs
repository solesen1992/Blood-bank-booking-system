using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// BloodTypeEnum defines a set of possible values that represent different blood types.
// Each member of the enum corresponds to a specific blood type (e.g., `APositive`, `ANegative`, etc.).
// Enums are useful by limiting the possible values that a variable can take.

namespace DesktopApp.Model
{
    public enum BloodTypeEnum
    {
        APositive, ANegative, BPositive, BNegative, ABPositive, ABNegative, OPositive, ONegative
    }
}
