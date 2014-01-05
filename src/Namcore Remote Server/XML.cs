using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Text;
using System.Xml;
using Namcore_Remote_Server.Configuration;

namespace Namcore_Remote_Server
{
    internal class XML
    {
        public void CreateAccountEntry(Account account)
        {
            var encoder = new UnicodeEncoding();
            var xmLobj = new XmlTextWriter(MyConfig.XmlLocation, encoder)
            {
                Formatting = Formatting.Indented,
                Indentation = 3
            };
            xmLobj.WriteStartDocument();
            xmLobj.WriteStartElement("User" + account.Id.ToString(CultureInfo.InvariantCulture));
            xmLobj.WriteAttributeString("Id", account.Id.ToString(CultureInfo.InvariantCulture));
            xmLobj.WriteAttributeString("Name", account.Name);
            xmLobj.WriteAttributeString("PassHash", account.PassHash);
            xmLobj.WriteAttributeString("Rank", account.Rights.GetHashCode().ToString(CultureInfo.InvariantCulture));
            xmLobj.WriteEndElement();
            xmLobj.Close();
        }

        public static void RetrieveAccounts()
        {
            Globals.Accounts = new List<Account>();
            var xmLobj = new XmlTextReader(MyConfig.XmlLocation);
            while (xmLobj.Read())
            {
                switch (xmLobj.NodeType)
                {
                    case XmlNodeType.Element:
                        var newAccount = new Account();
                        if (xmLobj.AttributeCount <= 0) continue;
                        while (xmLobj.MoveToNextAttribute())
                        {
                            switch (xmLobj.Name)
                            {
                                case "Id":
                                    newAccount.Id = Convert.ToInt32(xmLobj.Value);
                                    break;
                                case "Name":
                                    newAccount.Name = xmLobj.Value;
                                    break;
                                case "PassHash":
                                    newAccount.PassHash = xmLobj.Value;
                                    break;
                                case "Rank":
                                    newAccount.Rights = (AccountRights)Convert.ToInt32(xmLobj.Value);
                                    break;
                            }
                        }
                        Globals.Accounts.Add(newAccount);
                        break;
                }
            }
        }

        public void RemoveAccount(Account account)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(MyConfig.XmlLocation);

            // find a node - here the one with name='abc'
            var node = doc.SelectSingleNode("/User" + account.Id.ToString(CultureInfo.InvariantCulture) + "[@Id='"+ account.Id +"']");

            // if found....
            if (node != null)
            {
                // get its parent node
                XmlNode parent = node.ParentNode;

                // remove the child node
                parent.RemoveChild(node);

                // verify the new XML structure
                string newXML = doc.OuterXml;

                // save to file or whatever....
                doc.Save(MyConfig.XmlLocation);
            }
        }
    }
    
}