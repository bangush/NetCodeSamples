using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace tr.mustaliscl.xml
{
    public class XMLHelper
    {

        public static XmlNode AddElement(string tagName, string textContent, XmlNode parent)
        {
            XmlNode node = parent.OwnerDocument.CreateElement(tagName);
            parent.AppendChild(node);
            if (textContent != null)
            {
                XmlNode content;
                content = parent.OwnerDocument.CreateTextNode(textContent);
                node.AppendChild(content);
            }
            return node;
        }

        public static XmlNode AddAttribute(string attributeName, string textContent, XmlNode parent)
        {

            XmlAttribute attr = parent.OwnerDocument.CreateAttribute(attributeName);
            attr.Value = textContent;
            parent.Attributes.Append(attr);
            return attr;
        }

    }
}
