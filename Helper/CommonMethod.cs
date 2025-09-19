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
    }
}
