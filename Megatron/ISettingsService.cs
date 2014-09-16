using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using QuantTechnologies.Shell.Modules.Workspace;
using System.Collections.ObjectModel;
using QuantTechnologies.Shell.Modules.SettingsManager.Models;

namespace QuantTechnologies.Shell.Framework.Services
{

    public interface ISettings 
    {
        string Name { get; set; }
        string Owner { get; set; }
        object DefaultValue { get; set; }
        object Value { get; set; }
        eScope Scope { get; set; }

        TSetting ReadSetting<TSetting>(string pOwner, string pName);// where TSetting : ISettings;
        void WriteSetting<TSetting>(ISettings pSetting);// where TSetting : ISettings;
        void WriteSetting<TSetting>(string pOwner, string pName, object pDefaultValue, object pValue, eScope pScope);// where TSetting : ISettings;
        void ResetSetting<TSetting>(ISettings pSetting) where TSetting : ISettings;

    }

}
