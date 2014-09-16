using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace QuantTechnologies.Shell.Modules.SettingsManager.Models
{
    public class SerializableColor
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }


        public SerializableColor()
        {

        }
        public SerializableColor(Color pColor)
        {
            A = pColor.A;
            R = pColor.R;
            G = pColor.G;
            B = pColor.B;
        }
        public SerializableColor(SerializableColor pColor)
        {
            A = pColor.A;
            R = pColor.R;
            G = pColor.G;
            B = pColor.B;
        }
        //public static SerializableColor FromColor(Color c)
        //{
        //    return new SerializableColor(c);
        //}

        //public Color ToColor()
        //{
        //    //Color c = Color.FromArgb(A,R,G,B);
        //    //c = Color.FromArgb(A,R,G,B);

        //    return Color.FromArgb(A, R, G, B);
        //}

        public static SerializableColor CreateSubstitute(
              Color original)
        {
            // Create substitute instance,
            // fill its properties from the original object,
            // convert fields of the original object
            // in public properties of the substitute
            return new SerializableColor(original);
        }

        public Color CreateOriginal()
        {
            // create original instance, even without default constructor,
            // copy all important members from the substitute,
            // calculate unimportent ones
            return Color.FromArgb(A, R, G, B);
        }

        public override string ToString()
        {
            return string.Format("RGB: ({0}, {1}, {2})", R.ToString(), G.ToString(), B.ToString());
        }
    }

    public class SerializableFont
    {
        public string FontFamily { get; set; }
        public GraphicsUnit GraphicsUnit { get; set; }
        public float Size { get; set; }
        public FontStyle Style { get; set; }

        /// <summary>
        /// Intended for xml serialization purposes only
        /// </summary>
        public SerializableFont() { }

        public SerializableFont(Font f)
        {
            FontFamily = f.FontFamily.Name;
            GraphicsUnit = f.Unit;
            Size = f.Size;
            Style = f.Style;
        }
        public SerializableFont(SerializableFont f)
        {
            FontFamily = f.FontFamily;
            GraphicsUnit = f.GraphicsUnit;
            Size = f.Size;
            Style = f.Style;
        }
        public override string ToString()
        {
            return string.Format("{0}\t{1}", FontFamily, Size.ToString());
        
        }
        //public static SerializableFont FromFont(Font f)
        //{
        //    return new SerializableFont(f);
        //}

        //public Font ToFont()
        //{
        //    return new Font(FontFamily, Size, Style, GraphicsUnit);
        //}

        public static SerializableFont CreateSubstitute(Font original)
        {
            // Create substitute instance,
            // fill its properties from the original object,
            // convert fields of the original object
            // in public properties of the substitute
            return new SerializableFont(original);
        }

        public Font CreateOriginal()
        {
            // create original instance, even without default constructor,
            // copy all important members from the substitute,
            // calculate unimportent ones
            return new Font(FontFamily, Size, Style, GraphicsUnit);
        }
    }
}
