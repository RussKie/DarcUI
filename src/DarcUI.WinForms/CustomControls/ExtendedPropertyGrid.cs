// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace DarcUI;

public class ExtendedPropertyGrid : PropertyGrid
{
    private readonly ToolStripSeparator _separator1;
    private readonly ToolStripSeparator _separator2;
    private readonly ToolStripSeparator _separator3;
    private readonly ToolStripButton _btnNew;
    private readonly ToolStripButton _btnDelete;
    private readonly ToolStripButton _btnTrigger;
    private readonly ToolStripButton _btnViewChannels;

    public event EventHandler? NewClicked;
    public event EventHandler? DeleteClicked;
    public event EventHandler? TriggerClicked;
    public event EventHandler? ViewChannelsClicked;

    public ExtendedPropertyGrid()
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
        _btnTrigger = new("Trigger subscription")
        {
            Image = global::DarcUI.Properties.Resources.trigger,
            Visible = false
        };
        _btnViewChannels = new("View channels")
        {
            Image = global::DarcUI.Properties.Resources.channels,
            Visible = false
        };

        _btnNew.Click += (s, e) => NewClicked?.Invoke(this, e);
        _btnDelete.Click += (s, e) => DeleteClicked?.Invoke(this, e);
        _btnTrigger.Click += (s, e) => TriggerClicked?.Invoke(this, e);
        _btnViewChannels.Click += (s, e) => ViewChannelsClicked?.Invoke(this, e);

        ToolStrip toolbar = GetToolbar();
        toolbar.Items.Add(_separator1 = new ToolStripSeparator { Visible = false });
        toolbar.Items.Add(_btnNew);
        toolbar.Items.Add(_separator2 = new ToolStripSeparator { Visible = false });
        toolbar.Items.Add(_btnTrigger);
        toolbar.Items.Add(_btnDelete);
        toolbar.Items.Add(_separator3 = new ToolStripSeparator { Visible = false });
        toolbar.Items.Add(_btnViewChannels);
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
            _separator1.Visible = _btnNew.Visible;
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
            _separator2.Visible = _btnTrigger.Visible || _btnDelete.Visible;
        }
    }

    [DefaultValue(false)]
    public bool AllowTrigger
    {
        get => _btnTrigger.Visible;
        set
        {
            if (_btnTrigger.Visible == value)
            {
                return;
            }

            _btnTrigger.Visible = value;
            _separator2.Visible = _btnTrigger.Visible || _btnDelete.Visible;
        }
    }

    [DefaultValue(false)]
    public bool AllowViewChannels
    {
        get => _btnViewChannels.Visible;
        set
        {
            if (_btnViewChannels.Visible == value)
            {
                return;
            }

            _btnViewChannels.Visible = value;
            _separator3.Visible = _btnViewChannels.Visible;
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
