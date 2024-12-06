using System.Runtime.InteropServices;

namespace AutoCompleteTextBox
{
    public class CustomAutocompleteTextBox4 : TextBox
    {
        private IList<string> _autoCompleteSource = null;
        private string _startString = null;
        private ToolStripDropDown _dropDown = null;
        private ListBox _box = null;
        Point point;

        //mouse events
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;

        [DllImport("user32.dll")]
        static extern bool GetCaretPos(out Point lpPoint);
        [DllImport("user32.dll")]
        static extern bool SetCaretPos(int X, int Y);

        //cursor coordinates
        public int X_Coor { get; set; }
        public int Y_Coor { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string selected { get; set; }
        public bool IsKeydownSelected { get; set; }
        public bool IsBackspaceSelected { get; set; }

        public IList<string> MyAutoCompleteSource
        {
            get { return _autoCompleteSource; }
            set
            {
                if (value != null)
                {
                    _autoCompleteSource = value;
                }
            }
        }

        public string StartString
        {
            get { return _startString; }
            set { _startString = value; }
        }

        public bool IsValueInSource
        {
            get
            {
                if (_autoCompleteSource == null)
                    return true;
                else
                {
                    var data = from item in _autoCompleteSource where item == this.Text.Trim() select item;

                    if (data.Count<string>() > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CustomAutocompleteTextBox4()
        {
            //Todo
            this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (this.Text.Trim() != "")
            {
                if (_autoCompleteSource == null)
                    return;

                if (_dropDown.Visible)
                    _dropDown.Visible = false;

                if (IsKeydownSelected)
                {
                    var legalStrings = from item in _autoCompleteSource where item.ToLower().Contains(this.Text.ToLower().Trim()) select item;

                    if (legalStrings.Count<string>() > 0)
                    {
                        selected = this.Text + legalStrings.First();

                        _box.Items.Clear();
                        if (string.IsNullOrEmpty(_startString) == false)
                            _box.Items.Add(_startString);
                        foreach (string str in legalStrings)
                            _box.Items.Add(str);

                        _dropDown.Show(this, new Point(0, this.Height));
                    }
                }

                IsKeydownSelected = false;
                IsBackspaceSelected = false;
            }
            else
            {
                _dropDown.Close();
            }
            //mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToUInt32(X_Coor), Convert.ToUInt32(Y_Coor), 0, 0);
        }

        private void SetCaretPosition()
        {
            start = this.SelectionStart;
            point = new Point();
            GetCaretPos(out point);
            this.Text = selected;
            length = selected.Length;
            this.Select(start, length);
            SetCaretPos(point.X, point.Y);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            IsKeydownSelected = true;

            if (e.KeyCode == Keys.Down)
            {
                if (_box.Visible)
                    _box.Focus();
            }

            if (e.KeyCode == Keys.Back)
                IsBackspaceSelected = true;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            _dropDown = new ToolStripDropDown();
            _box = new ListBox();
            _box.Width = this.Width - 2;
            _box.Height += 25;
            _box.TabStop = false;

            _box.SelectedIndexChanged += (EventHandler)((sender, arg) =>
            {
                if (_box.SelectedIndex == 0)
                    _box.SelectedIndex = 0;
            });

            _box.KeyDown += (KeyEventHandler)((sender, k) =>
            {
                if (k.KeyCode == Keys.Enter && _box.SelectedIndex >= 0)
                {
                    this.Text = _box.SelectedItem.ToString();
                    _dropDown.Close();
                }
            });

            _box.Click += (EventHandler)((sender, arg) =>
            {
                this.Text = _box.SelectedItem.ToString();
                _dropDown.Close();
            });

            //add Listbox to control host
            ToolStripControlHost host = new ToolStripControlHost(_box);
            host.AutoSize = false;
            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;

            //add host to toolstrip dropdown
            _dropDown.Items.Add(host);
            _dropDown.Height = _box.Height;
            _dropDown.AutoSize = true;
            _dropDown.Margin = Padding.Empty;
            _dropDown.Padding = Padding.Empty;
            _dropDown.Size = host.Size = _box.Size;
            _dropDown.TabStop = false;
            _dropDown.AutoClose = false;

            if (this.Parent != null)
            {
                this.Parent.Click += new EventHandler(Parent_Click);
                this.Parent.MouseClick += new MouseEventHandler(Parent_Click);
                this.Parent.Move += new EventHandler(Parent_Click);
            }

            if (this.FindForm() != null)
            {
                this.FindForm().Click += new EventHandler(Parent_Click);
                this.FindForm().MouseClick += new MouseEventHandler(Parent_Click);
                this.FindForm().Move += new EventHandler(Parent_Click);
            }
        }

        private void Parent_Click(object sender, EventArgs e)
        {
            _dropDown.Close();
        }
    }
}