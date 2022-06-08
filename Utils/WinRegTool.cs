using Microsoft.Win32;
using System.Diagnostics;

namespace WinProxyTool_WPF.Utils
{
    public class WinRegTool
    {
        readonly RegistryKey proxy = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
        //代理开关
        public int Get_ProxyEnable()
        {
            if ((int)proxy.GetValue("ProxyEnable") != null) { return (int)proxy.GetValue("ProxyEnable"); }
            return -1;
        }
        public void Set_ProxyEnable(int v)
        {
            proxy.SetValue("ProxyEnable", v);
        }
        //代理IP地址
        public string Get_ProxyServer()
        {
            return (string)proxy.GetValue("ProxyServer");
        }
        public void Set_ProxyServer(string s) { proxy.SetValue("ProxyServer", s); }

        //代理绕过的地址
        public string Get_ProxyOverride()
        {
            string ProxyOverride = (string)proxy.GetValue("ProxyOverride");
            if (ProxyOverride == null) { return "null"; }
            else { return ProxyOverride; }
        }
    }
}
