using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Microsoft.SharePoint.Client;
using System.Net;
using ClientOM = Microsoft.SharePoint.Client;
using System.Xml;
using System.Xml.Linq;
using System.Security;
using MSDN.Samples.ClaimsAuth;


namespace Inventory_Manager
{
    public partial class Form1 : System.Windows.Forms.Form
    {
    

        static public ClientContext ctx = ClaimClientContext.GetAuthenticatedContext(@"https://capstoneconsulting.sharepoint.com");

        static public XDocument loadXMLDocument(string docName)
        {
            List inventoryDocumentList = ctx.Web.Lists.GetByTitle("Inventory");
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml =
                @"<View>
                    <Query>
                      <Where>
                        <Eq>
                          <FieldRef Name='FileLeafRef'/>
                          <Value Type='Text'>" + docName + @"</Value>
                        </Eq>
                      </Where>
                      <RowLimit>1</RowLimit>
                    </Query>
                  </View>";
            ListItemCollection listItems = inventoryDocumentList.GetItems(camlQuery);
            ctx.Load(inventoryDocumentList);
            ctx.Load(listItems);
            ctx.ExecuteQuery();

            if (listItems.Count == 1)
            {
                ClientOM.ListItem item = listItems[0];
                FileInformation fileInformation = ClientOM.File.OpenBinaryDirect(ctx, (string)item["FileRef"]);

                XDocument doc = XDocument.Load(fileInformation.Stream);

                return doc;
            }
            else return new XDocument();
        }

        static public XElement[] loadComputers(XDocument doc)
        {
            List<XElement> returnElements = new List<XElement>();
            foreach (XElement ex in doc.Root.Elements("computer"))
            {
                returnElements.Add(ex);
            }

            return returnElements.ToArray();
        }

       
        public static DataGridView populateDataGrid(XElement[] inputElemnts)
        {
       
            
        }
        public Form1()
        {
            InitializeComponent();

            
                
            XDocument doc = loadXMLDocument("Test.xml");

            XElement[] x = loadComputers(doc);

            DataTable dt = new DataTable();
            dt = doc.


            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = 


          
        }
    }
}
