
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowData
{
    public IntPtr hWnd { get; private set; }
    public string title;
    public Texture2D thumbnail;
    public string exePath;

    public WindowData()
    {
        this.hWnd = IntPtr.Zero;
    }

    public WindowData(IntPtr hWnd, string title = null, Texture2D thumbnail = null, string exePath = null)
    {
        this.hWnd = hWnd;
        this.title = title == null ? "" : title;
        this.thumbnail = thumbnail;
        this.exePath = exePath;
    }


    public override bool Equals(object obj)
    {
        if (obj is WindowData other)
        {
            return this.hWnd == other.hWnd;
        }
        return false;
    }

    public bool isExePath(string exePath)
    {
        return this.exePath.Equals(exePath);
    }

}
