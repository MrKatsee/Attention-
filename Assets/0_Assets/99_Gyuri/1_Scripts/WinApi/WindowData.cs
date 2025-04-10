
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowData
{
    public IntPtr hWnd { get; private set; }
    public string title;
    public Texture2D thumbnail;

    public WindowData()
    {
        this.hWnd = IntPtr.Zero;
    }

    public WindowData(IntPtr hWnd, string title = null, Texture2D thumbnail = null)
    {
        this.hWnd = hWnd;
        this.title = title==null?"":title;
        this.thumbnail = thumbnail;
    }

    public override bool Equals(object obj)
    {
        if (obj is WindowData other)
        {
            return this.hWnd == other.hWnd;
        }
        return false;
    }

}
