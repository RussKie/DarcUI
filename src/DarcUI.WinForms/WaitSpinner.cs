// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// Copyright (c) Git Extensions. All rights reserved.
// Borrowed from https://github.com/gitextensions/gitextensions

using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace DarcUI;

public sealed class WaitSpinner : Control
{
    private readonly int _dotCount = 11;
    private readonly int _dotRadius = 8;
    private readonly int _circleRadius = 18;

    private readonly Color _color = SystemColors.ControlDarkDark;
    private readonly Timer _timer;
    private readonly IReadOnlyList<Brush> _brushes;
    private readonly (float sin, float cos)[] _angles;

    private bool _isAnimating;
    private PointF _centre;
    private Control? _host;
    private int _progress;

    public bool IsAnimating
    {
        get => _isAnimating;
        set
        {
            _isAnimating = value;

            if (_isAnimating)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }
    }

    public Control? Host
    {
        get => _host;
        set
        {
            if (_host == value)
            {
                return;
            }

            if (_host is not null)
            {
                _host.Resize -= host_Resize;
            }

            _host = value;

            if (_host is not null)
            {
                SetLocation(_host);
                _host.Resize += host_Resize;
            }

            void SetLocation(Control host)
            {
                Location = new Point(host.Left + (host.Width - Width) / 2,
                                     host.Top + (host.Height - Height) / 2);
            }

            void host_Resize(object? sender, EventArgs e) => SetLocation((Control)sender!);
        }
    }

    public WaitSpinner()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);

        _brushes = GetBrushes();
        _angles = GetAngles();
        UpdateCentre();

        _timer = new Timer { Interval = 1000 / 30 }; // 30 fps
        _timer.Tick += delegate
        {
            _progress = (_progress + 1) % _dotCount;
            Invalidate();
        };
        IsAnimating = true;

        Resize += delegate
        { UpdateCentre(); };

        return;

        void UpdateCentre()
        {
            _centre = new PointF(Width / 2, (Height / 2) - 1);
        }

        IReadOnlyList<Brush> GetBrushes()
        {
            var alphaDelta = (byte.MaxValue / 2) / _dotCount;

            var brushes = new Brush[_dotCount];
            var alpha = 0;

            for (var i = 0; i < _dotCount; i++)
            {
                if (alpha > 255)
                {
                    alpha = 255;
                }

                brushes[i] = new SolidBrush(Color.FromArgb((byte)alpha, _color));
                alpha += alphaDelta;
            }

            return brushes;
        }

        (float sin, float cos)[] GetAngles()
        {
            var angleDelta = Math.PI * 2.0 / _dotCount;

            var angles = new (float sin, float cos)[_dotCount];

            var angle = 0.0;

            for (var i = 0; i < _dotCount; i++)
            {
                angles[i] = (
                    sin: (float)Math.Sin(angle),
                    cos: (float)Math.Cos(angle));
                angle += angleDelta;
            }

            return angles;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _timer.Dispose();
        }

        base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

        var p = _progress;
        for (var i = 0; i < _dotCount; i++)
        {
            p %= _dotCount;
            ref var angle = ref _angles[p];
            e.Graphics.FillEllipse(
                _brushes[i],
                (_centre.X - (_dotRadius / 2f)) + (_circleRadius * angle.cos),
                (_centre.Y - (_dotRadius / 2f)) + (_circleRadius * angle.sin),
                _dotRadius,
                _dotRadius);
            p++;
        }

        base.OnPaint(e);
    }
}
