namespace AcademicNetworkUI
{
    partial class EditMessagesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lstUserMessages = new ListBox();
            txtEditMessage = new SiticoneNetCoreUI.SiticoneTextBox();
            btnSaveChanges = new SiticoneNetCoreUI.SiticoneButton();
            btnDeleteMessage = new SiticoneNetCoreUI.SiticoneButton();
            SuspendLayout();
            // 
            // lstUserMessages
            // 
            lstUserMessages.FormattingEnabled = true;
            lstUserMessages.Location = new Point(345, 98);
            lstUserMessages.Name = "lstUserMessages";
            lstUserMessages.Size = new Size(150, 104);
            lstUserMessages.TabIndex = 0;
            // 
            // txtEditMessage
            // 
            txtEditMessage.AccessibleDescription = "A customizable text input field.";
            txtEditMessage.AccessibleName = "Text Box";
            txtEditMessage.AccessibleRole = AccessibleRole.Text;
            txtEditMessage.BackColor = Color.Transparent;
            txtEditMessage.BlinkCount = 3;
            txtEditMessage.BlinkShadow = false;
            txtEditMessage.BorderColor1 = Color.LightSlateGray;
            txtEditMessage.BorderColor2 = Color.LightSlateGray;
            txtEditMessage.BorderFocusColor1 = Color.FromArgb(77, 77, 255);
            txtEditMessage.BorderFocusColor2 = Color.FromArgb(77, 77, 255);
            txtEditMessage.CanShake = true;
            txtEditMessage.ContinuousBlink = false;
            txtEditMessage.CursorBlinkRate = 500;
            txtEditMessage.CursorColor = Color.Black;
            txtEditMessage.CursorHeight = 26;
            txtEditMessage.CursorOffset = 0;
            txtEditMessage.CursorStyle = SiticoneNetCoreUI.Helpers.DrawingStyle.SiticoneDrawingStyle.Solid;
            txtEditMessage.CursorWidth = 1;
            txtEditMessage.DisabledBackColor = Color.WhiteSmoke;
            txtEditMessage.DisabledBorderColor = Color.LightGray;
            txtEditMessage.DisabledTextColor = Color.Gray;
            txtEditMessage.EnableDropShadow = false;
            txtEditMessage.FillColor1 = Color.White;
            txtEditMessage.FillColor2 = Color.White;
            txtEditMessage.Font = new Font("Segoe UI", 9.5F);
            txtEditMessage.ForeColor = Color.DimGray;
            txtEditMessage.HoverBorderColor1 = Color.Gray;
            txtEditMessage.HoverBorderColor2 = Color.Gray;
            txtEditMessage.IsEnabled = true;
            txtEditMessage.Location = new Point(350, 242);
            txtEditMessage.Name = "txtEditMessage";
            txtEditMessage.PlaceholderColor = Color.Gray;
            txtEditMessage.PlaceholderText = "Enter text here...";
            txtEditMessage.ReadOnlyBorderColor1 = Color.LightGray;
            txtEditMessage.ReadOnlyBorderColor2 = Color.LightGray;
            txtEditMessage.ReadOnlyFillColor1 = Color.WhiteSmoke;
            txtEditMessage.ReadOnlyFillColor2 = Color.WhiteSmoke;
            txtEditMessage.ReadOnlyPlaceholderColor = Color.DarkGray;
            txtEditMessage.SelectionBackColor = Color.FromArgb(77, 77, 255);
            txtEditMessage.ShadowAnimationDuration = 1;
            txtEditMessage.ShadowBlur = 10;
            txtEditMessage.ShadowColor = Color.FromArgb(15, 0, 0, 0);
            txtEditMessage.Size = new Size(312, 50);
            txtEditMessage.SolidBorderColor = Color.LightSlateGray;
            txtEditMessage.SolidBorderFocusColor = Color.FromArgb(77, 77, 255);
            txtEditMessage.SolidBorderHoverColor = Color.Gray;
            txtEditMessage.SolidFillColor = Color.White;
            txtEditMessage.TabIndex = 1;
            txtEditMessage.Text = "siticoneTextBox1";
            txtEditMessage.TextPadding = new Padding(16, 0, 6, 0);
            txtEditMessage.ValidationErrorMessage = "Invalid input.";
            txtEditMessage.ValidationFunction = null;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.AccessibleDescription = "The default button control that accept input though the mouse, touch and keyboard";
            btnSaveChanges.AccessibleName = "Save";
            btnSaveChanges.AutoSizeBasedOnText = false;
            btnSaveChanges.BackColor = Color.Transparent;
            btnSaveChanges.BadgeBackColor = Color.Red;
            btnSaveChanges.BadgeFont = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnSaveChanges.BadgeValue = 0;
            btnSaveChanges.BadgeValueForeColor = Color.White;
            btnSaveChanges.BorderColor = Color.FromArgb(94, 148, 255);
            btnSaveChanges.BorderWidth = 2;
            btnSaveChanges.ButtonBackColor = Color.FromArgb(94, 148, 255);
            btnSaveChanges.ButtonImage = null;
            btnSaveChanges.CanBeep = true;
            btnSaveChanges.CanGlow = false;
            btnSaveChanges.CanShake = true;
            btnSaveChanges.ContextMenuStripEx = null;
            btnSaveChanges.CornerRadiusBottomLeft = 0;
            btnSaveChanges.CornerRadiusBottomRight = 0;
            btnSaveChanges.CornerRadiusTopLeft = 0;
            btnSaveChanges.CornerRadiusTopRight = 0;
            btnSaveChanges.CustomCursor = Cursors.Default;
            btnSaveChanges.DisabledTextColor = Color.FromArgb(150, 150, 150);
            btnSaveChanges.EnableLongPress = false;
            btnSaveChanges.EnablePressAnimation = true;
            btnSaveChanges.EnableRippleEffect = true;
            btnSaveChanges.EnableShadow = false;
            btnSaveChanges.EnableTextWrapping = false;
            btnSaveChanges.Font = new Font("Segoe UI", 9F);
            btnSaveChanges.GlowColor = Color.FromArgb(100, 255, 255, 255);
            btnSaveChanges.GlowIntensity = 100;
            btnSaveChanges.GlowRadius = 20F;
            btnSaveChanges.GradientBackground = false;
            btnSaveChanges.GradientColor = Color.FromArgb(114, 168, 255);
            btnSaveChanges.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            btnSaveChanges.HintText = null;
            btnSaveChanges.HoverBackColor = Color.FromArgb(114, 168, 255);
            btnSaveChanges.HoverFontStyle = FontStyle.Regular;
            btnSaveChanges.HoverTextColor = Color.White;
            btnSaveChanges.HoverTransitionDuration = 250;
            btnSaveChanges.ImageAlign = ContentAlignment.MiddleLeft;
            btnSaveChanges.ImagePadding = 5;
            btnSaveChanges.ImageSize = new Size(16, 16);
            btnSaveChanges.IsRadial = false;
            btnSaveChanges.IsReadOnly = false;
            btnSaveChanges.IsToggleButton = false;
            btnSaveChanges.IsToggled = false;
            btnSaveChanges.Location = new Point(439, 323);
            btnSaveChanges.LongPressDurationMS = 1000;
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.NormalFontStyle = FontStyle.Regular;
            btnSaveChanges.ParticleColor = Color.FromArgb(200, 200, 200);
            btnSaveChanges.ParticleCount = 15;
            btnSaveChanges.PressAnimationScale = 0.97F;
            btnSaveChanges.PressedBackColor = Color.FromArgb(74, 128, 235);
            btnSaveChanges.PressedFontStyle = FontStyle.Regular;
            btnSaveChanges.PressTransitionDuration = 150;
            btnSaveChanges.ReadOnlyTextColor = Color.FromArgb(100, 100, 100);
            btnSaveChanges.RippleColor = Color.FromArgb(255, 255, 255);
            btnSaveChanges.RippleOpacity = 0.3F;
            btnSaveChanges.RippleRadiusMultiplier = 0.6F;
            btnSaveChanges.ShadowBlur = 5;
            btnSaveChanges.ShadowColor = Color.FromArgb(100, 0, 0, 0);
            btnSaveChanges.ShadowOffset = new Point(2, 2);
            btnSaveChanges.ShakeDuration = 500;
            btnSaveChanges.ShakeIntensity = 5;
            btnSaveChanges.Size = new Size(225, 62);
            btnSaveChanges.TabIndex = 2;
            btnSaveChanges.Text = "Save";
            btnSaveChanges.TextAlign = ContentAlignment.MiddleCenter;
            btnSaveChanges.TextColor = Color.White;
            btnSaveChanges.TooltipText = null;
            btnSaveChanges.UseAdvancedRendering = true;
            btnSaveChanges.UseParticles = false;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // btnDeleteMessage
            // 
            btnDeleteMessage.AccessibleDescription = "The default button control that accept input though the mouse, touch and keyboard";
            btnDeleteMessage.AccessibleName = "Del";
            btnDeleteMessage.AutoSizeBasedOnText = false;
            btnDeleteMessage.BackColor = Color.Transparent;
            btnDeleteMessage.BadgeBackColor = Color.Red;
            btnDeleteMessage.BadgeFont = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnDeleteMessage.BadgeValue = 0;
            btnDeleteMessage.BadgeValueForeColor = Color.White;
            btnDeleteMessage.BorderColor = Color.FromArgb(94, 148, 255);
            btnDeleteMessage.BorderWidth = 2;
            btnDeleteMessage.ButtonBackColor = Color.FromArgb(94, 148, 255);
            btnDeleteMessage.ButtonImage = null;
            btnDeleteMessage.CanBeep = true;
            btnDeleteMessage.CanGlow = false;
            btnDeleteMessage.CanShake = true;
            btnDeleteMessage.ContextMenuStripEx = null;
            btnDeleteMessage.CornerRadiusBottomLeft = 0;
            btnDeleteMessage.CornerRadiusBottomRight = 0;
            btnDeleteMessage.CornerRadiusTopLeft = 0;
            btnDeleteMessage.CornerRadiusTopRight = 0;
            btnDeleteMessage.CustomCursor = Cursors.Default;
            btnDeleteMessage.DisabledTextColor = Color.FromArgb(150, 150, 150);
            btnDeleteMessage.EnableLongPress = false;
            btnDeleteMessage.EnablePressAnimation = true;
            btnDeleteMessage.EnableRippleEffect = true;
            btnDeleteMessage.EnableShadow = false;
            btnDeleteMessage.EnableTextWrapping = false;
            btnDeleteMessage.Font = new Font("Segoe UI", 9F);
            btnDeleteMessage.GlowColor = Color.FromArgb(100, 255, 255, 255);
            btnDeleteMessage.GlowIntensity = 100;
            btnDeleteMessage.GlowRadius = 20F;
            btnDeleteMessage.GradientBackground = false;
            btnDeleteMessage.GradientColor = Color.FromArgb(114, 168, 255);
            btnDeleteMessage.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            btnDeleteMessage.HintText = null;
            btnDeleteMessage.HoverBackColor = Color.FromArgb(114, 168, 255);
            btnDeleteMessage.HoverFontStyle = FontStyle.Regular;
            btnDeleteMessage.HoverTextColor = Color.White;
            btnDeleteMessage.HoverTransitionDuration = 250;
            btnDeleteMessage.ImageAlign = ContentAlignment.MiddleLeft;
            btnDeleteMessage.ImagePadding = 5;
            btnDeleteMessage.ImageSize = new Size(16, 16);
            btnDeleteMessage.IsRadial = false;
            btnDeleteMessage.IsReadOnly = false;
            btnDeleteMessage.IsToggleButton = false;
            btnDeleteMessage.IsToggled = false;
            btnDeleteMessage.Location = new Point(170, 323);
            btnDeleteMessage.LongPressDurationMS = 1000;
            btnDeleteMessage.Name = "btnDeleteMessage";
            btnDeleteMessage.NormalFontStyle = FontStyle.Regular;
            btnDeleteMessage.ParticleColor = Color.FromArgb(200, 200, 200);
            btnDeleteMessage.ParticleCount = 15;
            btnDeleteMessage.PressAnimationScale = 0.97F;
            btnDeleteMessage.PressedBackColor = Color.FromArgb(74, 128, 235);
            btnDeleteMessage.PressedFontStyle = FontStyle.Regular;
            btnDeleteMessage.PressTransitionDuration = 150;
            btnDeleteMessage.ReadOnlyTextColor = Color.FromArgb(100, 100, 100);
            btnDeleteMessage.RippleColor = Color.FromArgb(255, 255, 255);
            btnDeleteMessage.RippleOpacity = 0.3F;
            btnDeleteMessage.RippleRadiusMultiplier = 0.6F;
            btnDeleteMessage.ShadowBlur = 5;
            btnDeleteMessage.ShadowColor = Color.FromArgb(100, 0, 0, 0);
            btnDeleteMessage.ShadowOffset = new Point(2, 2);
            btnDeleteMessage.ShakeDuration = 500;
            btnDeleteMessage.ShakeIntensity = 5;
            btnDeleteMessage.Size = new Size(225, 62);
            btnDeleteMessage.TabIndex = 3;
            btnDeleteMessage.Text = "Del";
            btnDeleteMessage.TextAlign = ContentAlignment.MiddleCenter;
            btnDeleteMessage.TextColor = Color.White;
            btnDeleteMessage.TooltipText = null;
            btnDeleteMessage.UseAdvancedRendering = true;
            btnDeleteMessage.UseParticles = false;
            btnDeleteMessage.Click += btnDeleteMessage_Click;
            // 
            // EditMessagesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 646);
            Controls.Add(btnDeleteMessage);
            Controls.Add(btnSaveChanges);
            Controls.Add(txtEditMessage);
            Controls.Add(lstUserMessages);
            Name = "EditMessagesForm";
            Text = "EditMessagesForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstUserMessages;
        private SiticoneNetCoreUI.SiticoneTextBox txtEditMessage;
        private SiticoneNetCoreUI.SiticoneButton btnSaveChanges;
        private SiticoneNetCoreUI.SiticoneButton btnDeleteMessage;
    }
}