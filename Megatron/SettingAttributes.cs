using QuantTechnologies.Shell.Modules.SettingsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megatron
{
    [AttributeUsage(AttributeTargets.All)]
    public class SettingAttribute : System.Attribute
    {
        Setting mSetting;
        
        object mDefaultValue;

        public object DefaultValue
        {
            get { return mDefaultValue; }
            set { mDefaultValue = value; }
        }

        eScope mScope;

        public eScope Scope
        {
            get { return mScope; }
            set { mScope = value; }
        }

        //public SettingAttribute(string pOwner, string pName, object pDefaultValue, object pValue, eScope pScope = eScope.Application)
        //{
        //    mSetting = new Setting(pOwner, pName, pDefaultValue, pValue, pScope);
        //}

        public SettingAttribute(object pDefaultValue, eScope pScope = eScope.Application)
        {
            DefaultValue = pDefaultValue;
            Scope = pScope;
        }
        //public SettingAttribute()
        //{
        //    //DefaultValue = pDefaultValue;
        //    //Scope = pScope;
        //}
    }
}
