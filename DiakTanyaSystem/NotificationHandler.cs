using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiakTanyaSystem
{
    class NotificationHandler
    {
        public static void sendToast(string title, string text)
        {
            System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
            nIcon.Icon = SystemIcons.Error;
            nIcon.Visible = true;
            nIcon.ShowBalloonTip(10000, title, text, System.Windows.Forms.ToolTipIcon.Error);

        }
    }
}
