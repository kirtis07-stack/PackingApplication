using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication.Helper
{
    public class CommonMethod
    {
        public static Label InitializeLoadingLabel(Form form, string text = "Loading, please wait...")
        {
            Label lblLoading = new Label
            {
                Text = text,
                AutoSize = true,
                Font = FontManager.GetFont(12F, FontStyle.Italic),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                BackColor = Color.Transparent
            };

            form.Controls.Add(lblLoading);

            // Center in the form
            lblLoading.Location = new Point(
                form.ClientSize.Width / 2 - lblLoading.Width / 2,
                form.ClientSize.Height / 2 - lblLoading.Height / 2
            );

            lblLoading.BringToFront();

            return lblLoading;
        }

        public GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            // Top-left corner
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);

            // Top-right corner
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);

            // Bottom-right corner
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);

            // Bottom-left corner
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }

        public void SetButtonBorderRadius(System.Windows.Forms.Button button, int radius)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.BorderColor = Color.FromArgb(0, 92, 232); // Set to the background color of your form or panel
            button.FlatAppearance.MouseOverBackColor = button.BackColor; // To prevent color change on mouseover
            //button.BackColor = Color.FromArgb(0, 92, 232);

            // Set the border radius
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(0, 0, diameter, diameter, 180, 95); // Top-left corner
            path.AddArc(button.Width - diameter, 0, diameter, diameter, 270, 95); // Top-right corner
            path.AddArc(button.Width - diameter, button.Height - diameter, diameter, diameter, 0, 95); // Bottom-right corner
            path.AddArc(0, button.Height - diameter, diameter, diameter, 90, 95); // Bottom-left corner
            path.CloseFigure();

            button.Region = new Region(path);
        }

        public Image ResizeImage(Image img, int width, int height)
        {
            return new Bitmap(img, new Size(width, height));
        }

        public void DrawRoundedBorder(Control ctrl, PaintEventArgs e, int radius, Color borderColor, int thickness)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(borderColor, thickness))
            {
                Rectangle rect = new Rectangle(
                    thickness / 2,
                    thickness / 2,
                    ctrl.Width - thickness - 1,
                    ctrl.Height - thickness - 1
                );

                using (GraphicsPath path = GetRoundedRect(rect, radius))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        public void DrawBottomBorder(Control ctrl, PaintEventArgs e, Color borderColor, int thickness)
        {
            using (Pen pen = new Pen(borderColor, thickness))
            {
                e.Graphics.DrawLine(
                    pen,
                    0, ctrl.Height - thickness,
                    ctrl.Width, ctrl.Height - thickness
                );
            }
        }

        public void DrawRoundedDashedBorder(Control ctrl, PaintEventArgs e, int radius, Color borderColor, float thickness = 1f)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, ctrl.Width - 1, ctrl.Height - 1);

            using (GraphicsPath path = new GraphicsPath())
            {
                // Build rounded rectangle
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseFigure();

                using (Pen dashedPen = new Pen(borderColor, thickness))
                {
                    dashedPen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawPath(dashedPen, path);
                }
            }
        }

        public void DrawRectangleBorder(Control ctrl, PaintEventArgs e, Color borderColor, float thickness = 1f)
        {
            using (Pen pen = new Pen(borderColor, thickness))
            {
                Rectangle rect = ctrl.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        public void SetTopRoundedRegion(Control ctrl, int radius)
        {
            Rectangle rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);

            using (GraphicsPath path = new GraphicsPath())
            {
                // Top-left rounded
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                // Top edge
                path.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
                // Top-right rounded
                path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                // Rest rectangle
                path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom);
                path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
                path.AddLine(rect.X, rect.Bottom, rect.X, rect.Y + radius);

                path.CloseFigure();
                ctrl.Region = new Region(path);
            }
        }

        public void DrawRightBorder(Control control, PaintEventArgs e, Color borderColor, int thickness)
        {
            using (Pen pen = new Pen(borderColor, thickness))
            {
                int x = control.ClientRectangle.Right - thickness;
                e.Graphics.DrawLine(pen, x, 0, x, control.ClientRectangle.Height);
            }
        }

        //public void ComboBox_Paint(object sender, PaintEventArgs e)
        //{
        //    ComboBox combo = sender as ComboBox;
        //    if (combo.Tag?.ToString() == "error")
        //    {
        //        // Draw red border
        //        ControlPaint.DrawBorder(e.Graphics, combo.ClientRectangle,
        //            Color.Red, 2, ButtonBorderStyle.Solid,
        //            Color.Red, 2, ButtonBorderStyle.Solid,
        //            Color.Red, 2, ButtonBorderStyle.Solid,
        //            Color.Red, 2, ButtonBorderStyle.Solid);
        //    }
        //    else
        //    {
        //        // Draw normal border
        //        ControlPaint.DrawBorder(e.Graphics, combo.ClientRectangle,
        //            SystemColors.WindowFrame, 1, ButtonBorderStyle.Solid,
        //            SystemColors.WindowFrame, 1, ButtonBorderStyle.Solid,
        //            SystemColors.WindowFrame, 1, ButtonBorderStyle.Solid,
        //            SystemColors.WindowFrame, 1, ButtonBorderStyle.Solid);
        //    }
        //}

        public void Combo_DrawItem_Common(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox cb = (ComboBox)sender;
            object item = cb.Items[e.Index];

            // 🔹 Read IsDisabled property dynamically
            bool isDisabled = false;
            var prop = item?.GetType().GetProperty("IsDisabled");
            if (prop != null)
            {
                object propValue = prop.GetValue(item);

                if (propValue is bool b)
                    isDisabled = b == true;   // false = disabled
                else if (propValue is int i)
                    isDisabled = i == 1;       // 0 = disabled
            }

            // 🔹 Background color
            Color backColor = isDisabled
                ? Color.LightGray
                : ((e.State & DrawItemState.Selected) == DrawItemState.Selected
                    ? SystemColors.Highlight
                    : cb.BackColor);

            // 🔹 Text color
            Color textColor = isDisabled
                ? Color.DarkGray
                : ((e.State & DrawItemState.Selected) == DrawItemState.Selected
                    ? SystemColors.HighlightText
                    : cb.ForeColor);

            using (Brush backBrush = new SolidBrush(backColor))
                e.Graphics.FillRectangle(backBrush, e.Bounds);

            string text = cb.GetItemText(item);

            using (Brush textBrush = new SolidBrush(textColor))
                e.Graphics.DrawString(text, e.Font, textBrush, e.Bounds);

            if (!isDisabled)
                e.DrawFocusRectangle();
        }

        private int _lastValidIndex = -1;

        public void Combo_SelectionChangeCommitted_Common(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            object item = cb.SelectedItem;

            bool isDisabled = false;
            var prop = item?.GetType().GetProperty("IsDisabled");
            if (prop != null && prop.GetValue(item) is int value)
                isDisabled = value == 0;

            if (isDisabled)
            {
                MessageBox.Show("This item is disabled.");
                cb.SelectedIndex = _lastValidIndex;
                cb.DroppedDown = false;
            }
            else
            {
                _lastValidIndex = cb.SelectedIndex;
            }
        }

        public void DrawPanelRoundedBorder(Control ctrl, PaintEventArgs e, int radius, Color borderColor, int thickness)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Keep radius safe
            radius = Math.Min(radius, Math.Min(ctrl.Width, ctrl.Height) / 2);

            // Draw INSIDE bounds
            Rectangle rect = new Rectangle(
                thickness,
                thickness,
                ctrl.Width - (thickness * 2) - 1,
                ctrl.Height - (thickness * 2) - 1
            );

            using (GraphicsPath path = GetRoundedRect(rect, radius))
            using (Pen pen = new Pen(borderColor, thickness))
            {
                pen.Alignment = PenAlignment.Inset; 
                e.Graphics.DrawPath(pen, path);
            }
        }


    }
}
