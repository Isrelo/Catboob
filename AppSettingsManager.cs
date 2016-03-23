using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.ComponentModel;

namespace CatboobGGStream
{
    public class AppSettingsManager
    {
        private String working_dir;
        private String saved_app_items_path;
        BindingList<AppListBoxItem> app_list_box_items;
        private List<AppItem> app_items;

        public List<AppItem> AppItems 
        { 
            get{ return app_items; } 
        }

        public AppSettingsManager(String working_dir_p, BindingList<AppListBoxItem> app_list_box_items_p)
        {
            working_dir = working_dir_p;
            saved_app_items_path = working_dir_p + "\\AppItems.xml";

            app_list_box_items = app_list_box_items_p;

            app_items = new List<AppItem>();            
        }

        public void SaveAppItems()
        {
            // Read in the app items xml into a single string.
            String temp_xml_string = File.ReadAllText(saved_app_items_path);

            XmlDocument temp_xml_doc = new XmlDocument();
            temp_xml_doc.LoadXml(temp_xml_string);

            // Get the root node to append to.
            XmlNode document_root = temp_xml_doc.DocumentElement;
            document_root.RemoveAll();

            for (int count = 0; count < app_list_box_items.Count; count++)
            {
                AppItem temp_item = app_list_box_items[count].AppItemData;

                // Add a AppItem xml child.
                AppendAppItemXML(temp_xml_doc, temp_item);
            }

            // Write the changes to the xml file.
            temp_xml_doc.Save(saved_app_items_path);
        }

        public void SaveAppItem(AppItem app_item)
        {
            // Read in the app items xml into a single string.
            String temp_xml_string = File.ReadAllText(saved_app_items_path);

            XmlDocument temp_xml_doc = new XmlDocument();
            temp_xml_doc.LoadXml(temp_xml_string);

            // Add a AppItem xml child.
            AppendAppItemXML(temp_xml_doc, app_item);

            // Write the changes to the xml file.
            temp_xml_doc.Save(saved_app_items_path);
        }

        public void LoadAppItems()
        {
            app_items = new List<AppItem>();

            if (File.Exists(saved_app_items_path))
            {
                FileStream app_items_file = new FileStream(saved_app_items_path, FileMode.Open, FileAccess.Read);
                XmlReader app_items_xml = XmlReader.Create(app_items_file);

                bool app_item_parent_found = false;
                String temp_value = "";
                AppItem temp_app_item = null;

                while (app_items_xml.Read())
                {
                    if (app_items_xml.IsStartElement() && app_items_xml.Name == "AppItem")
                    {
                        // Signal that a parent AppItem was found.
                        app_item_parent_found = true;

                        // Reset for the next AppItem.
                        temp_app_item = new AppItem();
                    }

                    if (app_item_parent_found && app_items_xml.IsStartElement())
                    {
                        if (app_items_xml.Name == "AppPath")
                        {
                            // Read in the application executable path.
                            temp_value = app_items_xml.ReadElementContentAsString();
                            temp_app_item.AppPath = temp_value;
                        }
                        else if (app_items_xml.Name == "AppTitle")
                        {
                            // Read in the application title.
                            temp_value = app_items_xml.ReadElementContentAsString();
                            temp_app_item.AppTitle = temp_value;
                        }
                        else if (app_items_xml.Name == "AppArgs")
                        {
                            // Read in the application arguments.
                            temp_value = app_items_xml.ReadElementContentAsString();

                            // Bug: Blank arguments being passed to application.
                            temp_app_item.AppArgs = temp_value.Trim();
                        }
                    }

                    if (app_items_xml.NodeType == XmlNodeType.EndElement && app_items_xml.Name == "AppItem")
                    {
                        // Signal the end of a app item.
                        app_item_parent_found = false;

                        // Add the loaded AppItem to the list.
                        app_items.Add(temp_app_item);
                    }
                }

                app_items_file.Close();
            }
        }

        private void AppendAppItemXML(XmlDocument xml_doc, AppItem app_item)
        {
            // Get the root node to append to.
            XmlNode document_root = xml_doc.DocumentElement;

            // <AppPath>
            XmlElement app_item_root_element = xml_doc.CreateElement("AppItem");

            //   <ImagePath>
            XmlElement image_path = xml_doc.CreateElement("AppPath");
            image_path.InnerText = app_item.AppPath;
            app_item_root_element.AppendChild(image_path);

            //   <AppTitle>
            XmlElement sound_path = xml_doc.CreateElement("AppTitle");
            sound_path.InnerText = app_item.AppTitle;
            app_item_root_element.AppendChild(sound_path);

            //   <AppArgs>
            XmlElement hotkey = xml_doc.CreateElement("AppArgs");
            hotkey.InnerText = app_item.AppArgs;
            app_item_root_element.AppendChild(hotkey);

            // Add the new app item xml to the end of the document.
            document_root.AppendChild(app_item_root_element);
        }
    }
}
