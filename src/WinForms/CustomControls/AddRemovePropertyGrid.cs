// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DarcUI
{
    public class AddRemovePropertyGrid : PropertyGrid
    {
        private readonly ToolStripSeparator _separator;
        private readonly ToolStripButton _btnNew;
        private readonly ToolStripButton _btnDelete;

        public event EventHandler? NewClicked;
        public event EventHandler? DeleteClicked;

        public AddRemovePropertyGrid()
        {
            _btnNew = new("New subscription")
            {
                Image = global::DarcUI.Properties.Resources.add,
                Visible = false
            };
            _btnDelete = new("Delete subscription")
            {
                Image = global::DarcUI.Properties.Resources.delete,
                Visible = false
            };

            _btnNew.Click += (s, e) => NewClicked?.Invoke(this, e);
            _btnDelete.Click += (s, e) => DeleteClicked?.Invoke(this, e);

            ToolStrip toolbar = GetToolbar();
            toolbar.Items.Add(_separator = new ToolStripSeparator { Visible = false });
            toolbar.Items.Add(_btnNew);
            toolbar.Items.Add(_btnDelete);
        }

        [DefaultValue(false)]
        public bool AllowCreate
        {
            get => _btnNew.Visible;
            set
            {
                if (_btnNew.Visible == value)
                {
                    return;
                }

                _btnNew.Visible = value;
                _separator.Visible = _btnNew.Visible && _btnDelete.Visible;
            }
        }

        [DefaultValue(false)]
        public bool AllowDelete
        {
            get => _btnDelete.Visible;
            set
            {
                if (_btnDelete.Visible == value)
                {
                    return;
                }

                _btnDelete.Visible = value;
                _separator.Visible = _btnNew.Visible && _btnDelete.Visible;
            }
        }

        private ToolStrip GetToolbar()
        {
            foreach (Control control in Controls)
            {
                ToolStrip? toolStrip = control as ToolStrip;
                if (toolStrip is not null)
                {
                    return toolStrip;
                }
            }

            throw new MissingMemberException("Unable to find the toolstrip in the PropertyGrid.");
        }
    }
}
