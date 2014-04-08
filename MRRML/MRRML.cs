using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRML_Lib
{
    public class MRRML
    {

        protected String name = "";

        protected List<MRRML> children = new List<MRRML>();
        protected List<MRRML_Att> attributes = new List<MRRML_Att>();

        public MRRML()
        {

        }

        public static MRRML parse(String text)
        {

            int len = 0;

            MRRML tmp;

            text = removeLength(text);

            len = pullLength(text);
            text = removeLength(text);
            tmp = new MRRML(text.Substring(0, len));
            text = text.Substring(len, text.Length - len);


            while (text.IndexOf("::") != 0)
            {
                len = pullLength(text);
                text = removeLength(text);
                tmp.attributes.Add(MRRML_Att.parse(text.Substring(0, len)));
                text = text.Substring(len, text.Length - len);
            }

            text = text.Substring(2, text.Length - 2);

            while (text.Length > 0)
            {
                len = pullLength(text);
                tmp.children.Add(MRRML.parse(text.Substring(0, text.IndexOf(":") + 1 + len)));
                text = removeLength(text);
                //tmp.children.Add(MRRML.parse(text.Substring(0, len)));
                text = text.Substring(len, text.Length - len);
            }

            return tmp;
        }

        public MRRML(String name)
        {
            this.name = name;
        }

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public void addChild(MRRML child)
        {
            addChild(children.Count, child);
        }

        public void addChild(int index, MRRML child)
        {
            children.Insert(index, child);
        }

        public MRRML getChild(String name)
        {
            for (int g = 0; g < children.Count; g++)
            {
                if (children[g].Name == name)
                {
                    return children[g];
                }
            }

            return null;
        }

        public void removeChildrenByName(String name)
        {
            for (int g = children.Count - 1; g > -1; g--)
            {
                if (children[g].Name == name)
                {
                    children.RemoveAt(g);
                }
            }
        }

        public void removeChild(MRRML child)
        {
            children.Remove(child);
        }

        public void removeChild(int index)
        {
            children.RemoveAt(index);
        }

        public void addAttribute(MRRML_Att attribute)
        {
            addAttribute(attributes.Count, attribute);
        }

        public void addAttribute(int index, MRRML_Att attribute)
        {
            attributes.Insert(index, attribute);
        }

        public MRRML_Att getAttribute(String name)
        {
            for (int g = 0; g < attributes.Count; g++)
            {
                if (attributes[g].Name == name)
                {
                    return attributes[g];
                }
            }

            return null;
        }

        public void removeAttributesByName(String name)
        {
            for (int g = attributes.Count - 1; g > -1; g--)
            {
                if (attributes[g].Name == name)
                {
                    attributes.RemoveAt(g);
                }
            }
        }

        public void removeAttribute(MRRML_Att attribute)
        {
            attributes.Remove(attribute);
        }

        public void removeAttribute(int index)
        {
            attributes.RemoveAt(index);
        }

        public List<MRRML_Att> Attributes
        {
            get
            {
                return attributes;
            }
            set
            {
                attributes = value;
            }
        }

        public List<MRRML> Children
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
            }
        }

        public static int pullLength(String text)
        {
            return int.Parse(text.Substring(0, text.IndexOf(":")));
        }

        public static String removeLength(String text)
        {
            return text.Substring(text.IndexOf(":") + 1, text.Length - (text.IndexOf(":") + 1));
        }

        public override String ToString()
        {
            String s = "";

            String tmp = "";

            tmp += name.Length + ":" + name;

            for (int g = 0; g < attributes.Count; g++)
            {
                tmp += attributes[g].ToString();
            }
            tmp += "::";
            for (int g = 0; g < children.Count; g++)
            {
                tmp += children[g].ToString();
            }

            tmp += "";

            s += tmp.Length + ":" + tmp;

            return s;
        }

    }

    public class MRRML_Att
    {
        protected String name = "";
        protected String value = "";

        public MRRML_Att(String name)
        {
            this.name = name;
            this.value = "";
        }

        public MRRML_Att(String name, String value)
        {
            this.name = name;
            this.value = value;
        }

        public static MRRML_Att parse(String text)
        {

            int len = MRRML.pullLength(text);
            text = MRRML.removeLength(text);
            String name = text.Substring(0, len);
            text = text.Substring(len, text.Length - len);
            len = MRRML.pullLength(text);
            text = MRRML.removeLength(text);
            String value = text.Substring(0, len);

            return new MRRML_Att(name, value);
        }

        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public String Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        public override String ToString()
        {
            String s = name.Length + ":" + name + value.Length + ":" + value;
            return s.Length + ":" + s;
        }
    }
}
