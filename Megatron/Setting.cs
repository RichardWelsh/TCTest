using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantTechnologies.Shell.Framework.Services;
using Polenter.Serialization;
using System.Drawing;
using System.IO;
using System.ComponentModel.Composition;
using System.Reflection;
using Megatron;

namespace QuantTechnologies.Shell.Modules.SettingsManager.Models
{
    public enum eScope { Application, User, Host }

    //[Export(typeof(IService))]
    //[Export(typeof(ISettings))]
    public class Setting : ISettings
    {   
        public string Name { get; set; }
        public string Owner { get; set; }
        public object DefaultValue { get; set; }
        public object Value { get; set; }
        public eScope Scope { get; set; }

        public Setting()
        {
            Name = string.Empty;
            Owner = string.Empty;
            DefaultValue = new object();
            Value = new object();
            Scope = eScope.Application;
        }

        public Setting(string pName, string pOwner, object pDefaultValue, object pValue, eScope pScope = eScope.Application)
        {
            Name = pName;
            Owner = pOwner;
            Scope = pScope;
            DefaultValue = pDefaultValue;
            Value = pValue;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", Owner, Name, Scope.ToString(), Value.ToString());
        }

        public Guid ServiceId
        {
            get { return Guid.Parse("{EE7D67E2-89C1-477E-908C-46328B73E526}"); }
        }

        private string FileName(string pOwner)
        {
            return string.Format("{0}.xml", pOwner.ToLower());
        }

        public TSetting ReadSetting<TSetting>(string pOwner, string pName)
        {
            TSetting retVal = default(TSetting);
            if (File.Exists(FileName(pOwner)))
            {
                List<Setting> Settings = (List<Setting>)(new SharpSerializer()).Deserialize(FileName(pOwner));

                Setting result = Settings.FirstOrDefault(s => s.Owner == pOwner && s.Name == pName);

                if (result != null)
                {

                    switch (typeof(TSetting).Name)
                    {
                        case "Font":
                            retVal = (TSetting)(object)(new SerializableFont((SerializableFont)result.Value)).CreateOriginal();
                            break;
                        case "Color":
                            retVal = (TSetting)(object)(new SerializableColor((SerializableColor)result.Value)).CreateOriginal();
                            break;
                        default:
                            retVal = (TSetting)(object)result.Value;
                            break;
                    }
                }
            }

            return retVal;
        }

        public void WriteSetting<TSetting>(string pOwner, string pName, object pDefaultValue, object pValue, eScope pScope)
        {
            string fName = FileName(pOwner);
            List<Setting> Settings = new List<Setting>();

            if (File.Exists(fName))
            {
                Settings = (List<Setting>)(new SharpSerializer()).Deserialize(fName);
                
                int pos = IndexOf(Settings, s => s.Owner == pOwner && s.Name == pName);
                if (pos != -1)
                    Settings.RemoveAt(pos);
            }
            Setting setting = new Setting(pName, pOwner, pDefaultValue, pValue, pScope );

            //if setting exists remove it from list **************  make one liner


            switch (typeof(TSetting).Name)
            {
                case "Font":
                    setting.Value = (new SerializableFont((Font)pValue));
                    setting.DefaultValue = (new SerializableFont((Font)pDefaultValue));
                    break;
                case "Color":
                    setting.Value = (new SerializableColor((Color)pValue));
                    setting.DefaultValue = (new SerializableColor((Color)pDefaultValue));
                    break;
            }

            Settings.Add(setting);

            if (File.Exists(fName))
                File.Delete(fName);

            // adjust for culture
            SharpSerializerXmlSettings xmlSettings = new SharpSerializerXmlSettings();
            xmlSettings.Culture = System.Globalization.CultureInfo.CurrentCulture;
            xmlSettings.Encoding = System.Text.Encoding.Unicode;
            //Polenter.Serialization.Advanced.PropertyProvider pp = new Polenter.Serialization.Advanced.PropertyProvider();

            //serializer.
            (new SharpSerializer(xmlSettings)).Serialize(Settings, fName);            
        }

        public void WriteSetting<TSetting>(ISettings pSetting)
        {
            string fName = FileName(pSetting.Owner);
            List<Setting> Settings = (List<Setting>)(new SharpSerializer()).Deserialize(fName);
            Setting setting = (Setting)pSetting;

            //if setting exists remove it from list **************  make one liner
            int pos = IndexOf(Settings, s => s.Owner == pSetting.Owner && s.Name == pSetting.Name);
            if (pos != -1)
                Settings.RemoveAt(pos);

            switch (typeof(TSetting).Name)
            {
                case "Font":
                    setting.Value = (new SerializableFont((Font)pSetting.Value));
                    setting.DefaultValue = (new SerializableFont((Font)pSetting.DefaultValue));
                    break;
                case "Color":
                    setting.Value = (new SerializableColor((Color)pSetting.Value));
                    setting.DefaultValue = (new SerializableColor((Color)pSetting.DefaultValue));
                    break;
            }

            Settings.Add(setting);

            if (File.Exists(fName))
                File.Delete(fName);

            // adjust for culture
            SharpSerializerXmlSettings xmlSettings = new SharpSerializerXmlSettings();
            xmlSettings.Culture = System.Globalization.CultureInfo.CurrentCulture;
            xmlSettings.Encoding = System.Text.Encoding.Unicode;
            //Polenter.Serialization.Advanced.PropertyProvider pp = new Polenter.Serialization.Advanced.PropertyProvider();

            //serializer.
            (new SharpSerializer(xmlSettings)).Serialize(Settings, fName);
        }

        public void ResetSetting<TSetting>(ISettings pSetting) where TSetting : ISettings
        {
            string fName = FileName(pSetting.Owner);
            List<Setting> Settings = (List<Setting>)(new SharpSerializer()).Deserialize(fName);

            int pos = IndexOf(Settings, s => s.Owner == pSetting.Owner && s.Name == pSetting.Name);
            if (pos != -1)
            {
                Settings[pos].Value = Settings[pos].DefaultValue;
                (new SharpSerializer()).Serialize(Settings, fName);
            }
        }

        public static int IndexOf<T>(IEnumerable<T> list, Predicate<T> condition)
        {
            int i = -1;
            return list.Any(x => { i++; return condition(x); }) ? i : -1;
        }

        public void Save()
        {
            var AsmSetting = Assembly.GetCallingAssembly();//.GetCustomAttribute(typeof( SettingAttribute));
            //var AsmSetting1 = Assembly.GetCallingAssembly().GetCustomAttribute(false);
            var exe = Assembly.GetExecutingAssembly();

            var attrib = exe.GetCustomAttributesData();



            var memberSettings = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.GetCustomAttributes(false).Any(a => a is SettingAttribute)
                    select t;

            foreach (Type t in memberSettings)
            {
                var testMembers =
                    from m in t.GetMembers()
                    where m.GetCustomAttributes(false).Any(a => a is SettingAttribute)
                    select m;

            }
        }
    }
}
