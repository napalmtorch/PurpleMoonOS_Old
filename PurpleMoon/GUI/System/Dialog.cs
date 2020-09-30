// system libraries
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

// os libraries
using PurpleMoon.Core;
using PurpleMoon.Hardware;
using PurpleMoon.Math;
using PurpleMoon.Types;
using PurpleMoon.GUI;

namespace PurpleMoon.GUI
{
    public class Dialog : Window
    {
        public DialogResult result = DialogResult.none;
        public DialogResult resultType = DialogResult.ok;

        public Button btnOK, btnCancel, btnYes, btnNo;
        public string msg;
        private int textW;

        public Dialog(string title, string msg, DialogResult type) : base("Shutdown", 0, 0, 160, 128)
        {
            this.msg = msg;
            this.resultType = type;
            this.title = title;
            this.minimizeBox = false;
            this.maximizeBox = false;
            this.dialog = true;

            if (type == DialogResult.ok)
            {
                btnCancel = null;
                btnYes = null;
                btnNo = null;
                btnOK = new Button(x + (width / 2) - (btnOK.width / 2), y + height - btnOK.height - 8, "OK");
            }
            else if (type == DialogResult.okCancel)
            {
                btnCancel = new Button(x + (width / 2) + 8, y + height - btnOK.height - 8, "Cancel");
                btnYes = null;
                btnNo = null;
                btnOK = new Button(x + (width / 2) - btnOK.width - 8, y + height - btnOK.height - 8, "OK");
            }
            else if (type == DialogResult.yesNo)
            {
                btnCancel = null;
                btnYes = new Button(x + (width / 2) - btnYes.width - 8, y + height - btnYes.height - 8, "Yes");
                btnNo = new Button(x + (width / 2) + 8, y + height - btnNo.height - 8, "No");
                btnOK = null;
            }

            textW = Fonts.FONT_MONO.StringWidth(msg);
            this.width = textW + 24;
            if (this.width < 212 && resultType == DialogResult.yesNo || this.width < 212 && resultType == DialogResult.okCancel) { this.width = 212; }
            else if (this.width < 128 && resultType == DialogResult.ok) { this.width = 128; }
            this.x = (SVGA.width / 2) - (width / 2);
            this.y = (SVGA.height / 2) - (height / 2) + 32;
        }

        public override void Update()
        {
            // update btn positions
            if (resultType == DialogResult.okCancel)
            {
                btnOK.x = x + (width / 2) - btnOK.width - 8;
                btnOK.y = y + height - btnOK.height - 8;
            }
            else
            {
                btnOK.x = x + (width / 2) - (btnOK.width / 2);
                btnOK.y = y + height - btnOK.height - 8;
            }
            btnCancel.x = x + (width / 2) + 8;
            btnCancel.y = y + height - btnOK.height - 8;
            btnYes.x = x + (width / 2) - btnYes.width - 8;
            btnYes.y = y + height - btnYes.height - 8;
            btnNo.x = x + (width / 2) + 8;
            btnNo.y = y + height - btnNo.height - 8;

            if (resultType == DialogResult.okCancel) { btnOK.Update(); btnCancel.Update(); }
            if (resultType == DialogResult.ok) { btnOK.Update(); }
            if (resultType == DialogResult.yesNo) { btnYes.Update(); btnNo.Update(); }

            if (btnOK.down) { result = DialogResult.ok; }
            if (btnCancel.down) { result = DialogResult.cancel; }
            if (btnYes.down) { result = DialogResult.yes; }
            if (btnNo.down) { result = DialogResult.no; }

            // update base
            UpdateWindow();
            base.Update();

            // fix position
            this.x = (SVGA.width / 2) - (this.width / 2);
            this.y = (SVGA.height / 2) - (height / 2) + 32;
            this.moving = false;
        }

        public override void Draw()
        {
            // draw base
            DrawWindow();

            // draw msg
            if (msg.Length > 0)
            {
                Graphics2D.DrawString(x + (width / 2) - (textW / 2), this.y + (height / 2) - 5, msg, Color.white, Fonts.FONT_MONO);
            }

            if (resultType == DialogResult.okCancel) { btnOK.Draw(); btnCancel.Draw(); }
            if (resultType == DialogResult.ok) { btnOK.Draw(); }
            if (resultType == DialogResult.yesNo) { btnYes.Draw(); btnNo.Draw(); }
        }
    }
}
