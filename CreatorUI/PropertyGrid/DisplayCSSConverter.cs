using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorUI.PropertyGrid
{
    public class DisplayCSSConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // false - можно вводить вручную
            // true - только выбор из списка
            return false;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // возвращаем список строк из настроек программы
            // (базы данных, интернет и т.д.)
            return new StandardValuesCollection(new List<string> {
                "block",
                "inline",
                "inline-block",
                "inline-table",
                "list-item",
                "none",
                "run-in",
                "table",
                "table-caption",
                "table-cell",
                "table-column-group",
                "table-column",
                "table-footer-group",
                "table-header-group",
                "table-row",
                "table-row-group"
            });
        }
    }
}
