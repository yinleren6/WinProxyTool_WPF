﻿using Microsoft.Win32;

namespace WinProxyTool_WPF.Utils
{
    public class WinRegTool
    {
        readonly RegistryKey proxy = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
        //代理开关
        public int Get_ProxyEnable() { return (int)proxy.GetValue("ProxyEnable"); }
        public void Set_ProxyEnable(int isEnabled) { proxy.SetValue("ProxyEnable", isEnabled); }

        //代理IP地址
        public string? Get_ProxyServer() { return proxy.GetValue("ProxyServer") as string; }
        public void Set_ProxyServer(string proxyServer) { proxy.SetValue("ProxyServer", proxyServer); }

        //代理绕过的地址
        public string? Get_ProxyOverride() { return proxy.GetValue("ProxyOverride") as string; }
        public void Set_ProxyOverride(string proxyOverride) { proxy.SetValue("ProxyOverride", proxyOverride); }
    }
}
