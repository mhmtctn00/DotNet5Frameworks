using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.TwoFactorAuth.Google
{
    public class SetupCode
    {
        public string Account { get; internal set; }
        public string AccountSecretKey { get; internal set; }
        public string ManualEntryKey { get; internal set; }
        public string QrCodeSetupImageUrl { get; internal set; }
    }
}
