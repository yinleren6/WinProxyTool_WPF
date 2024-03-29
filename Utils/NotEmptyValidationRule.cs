﻿using System.Globalization;
using System.Windows.Controls;

namespace WinProxyTool_WPF.Utils
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "此字段为必填项")
                : ValidationResult.ValidResult;
        }
    }
}
