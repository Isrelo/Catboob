﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.ComponentModel;

namespace CatboobGGStream
{
    public class SettingsManager
    {
        private String working_dir;
        private String saved_overlay_items_path;
        private List<OverlayItem> overlay_items;

        public List<OverlayItem> OverlayItems 
        { 
            get{ return overlay_items; } 
        }

        public SettingsManager(String working_dir_p)
        {
            working_dir = working_dir_p;
            saved_overlay_items_path = working_dir_p + "\\OverlayItems.xml";
            overlay_items = new List<OverlayItem>();            
        }

        public void SaveOverlayItems(BindingList<OverlayListBoxItem> overlay_list_box_items)
        {
            // Read in the overlay items xml into a single string.
            String temp_xml_string = File.ReadAllText(saved_overlay_items_path);

            XmlDocument temp_xml_doc = new XmlDocument();
            temp_xml_doc.LoadXml(temp_xml_string);

            // Get the root node to append to.
            XmlNode document_root = temp_xml_doc.DocumentElement;
            document_root.RemoveAll();

            for (int count = 0; count < overlay_list_box_items.Count; count++)
            {
                OverlayItem temp_item = overlay_list_box_items[count].OverlayItemData;

                // Add a OverlayItem xml child.
                AppendOverlayItemXML(temp_xml_doc, temp_item);
            }

            // Write the changes to the xml file.
            temp_xml_doc.Save(saved_overlay_items_path);
        }

        public void SaveOverlayItem(OverlayItem overlay_item)
        {
            // Read in the overlay items xml into a single string.
            String temp_xml_string = File.ReadAllText(saved_overlay_items_path);

            XmlDocument temp_xml_doc = new XmlDocument();
            temp_xml_doc.LoadXml(temp_xml_string);

            // Add a OverlayItem xml child.
            AppendOverlayItemXML(temp_xml_doc, overlay_item);

            // Write the changes to the xml file.
            temp_xml_doc.Save(saved_overlay_items_path);
        }

        public void LoadOverlayItems()
        {
            overlay_items = new List<OverlayItem>();

            if (File.Exists(saved_overlay_items_path))
            {
                FileStream overlay_items_file = new FileStream(saved_overlay_items_path, FileMode.Open, FileAccess.Read);
                XmlReader overlay_items_xml = XmlReader.Create(overlay_items_file);

                bool overlay_item_parent_found = false;
                String temp_value = "";
                OverlayItem temp_overlay_item = null;

                while (overlay_items_xml.Read())
                {
                    if (overlay_items_xml.IsStartElement() && overlay_items_xml.Name == "OverlayItem")
                    {
                        // Signal that a parent OverlayItem was found.
                        overlay_item_parent_found = true;

                        // Reset for the next OverlayItem.
                        temp_overlay_item = new OverlayItem();
                    }

                    if (overlay_item_parent_found && overlay_items_xml.IsStartElement())
                    {
                        if (overlay_items_xml.Name == "ImagePath")
                        {
                            // Read in the overlay image path.
                            temp_value = overlay_items_xml.ReadElementContentAsString();
                            temp_overlay_item.ImagePath = temp_value;
                        }
                        else if (overlay_items_xml.Name == "SoundPath")
                        {
                            // Read in the overlay sound path.
                            temp_value = overlay_items_xml.ReadElementContentAsString();
                            temp_overlay_item.SoundPath = temp_value;
                        }
                        else if (overlay_items_xml.Name == "HotKey")
                        {
                            // Read in the overlay image path.
                            temp_value = overlay_items_xml.ReadElementContentAsString();
                            temp_overlay_item.HotKey = temp_value;
                        }
                        else if (overlay_items_xml.Name == "SoundVolume")
                        {
                            // Read in the overlay sound volume.
                            temp_value = overlay_items_xml.ReadElementContentAsString();
                            double temp_volume = 0;
                            double.TryParse(temp_value, out temp_volume);
                            temp_overlay_item.SoundVolume = temp_volume;
                        }
                        else if (overlay_items_xml.Name == "DisplayDuration")
                        {
                            temp_value = overlay_items_xml.ReadElementContentAsString();
                            temp_overlay_item.DisplayDuration = temp_value;
                        }
                    }

                    if (overlay_items_xml.NodeType == XmlNodeType.EndElement && overlay_items_xml.Name == "OverlayItem")
                    {
                        // Signal the end of a overlay item.
                        overlay_item_parent_found = false;

                        // Add the loaded OverlayItem to the list.
                        overlay_items.Add(temp_overlay_item);
                    }
                }

                overlay_items_file.Close();
            }
        }

        private void AppendOverlayItemXML(XmlDocument xml_doc, OverlayItem overlay_item)
        {
            // Get the root node to append to.
            XmlNode document_root = xml_doc.DocumentElement;

            // <OverlayItem>
            XmlElement overlay_item_root_element = xml_doc.CreateElement("OverlayItem");

            //   <ImagePath>
            XmlElement image_path = xml_doc.CreateElement("ImagePath");
            image_path.InnerText = overlay_item.ImagePath;
            overlay_item_root_element.AppendChild(image_path);

            //   <SoundPath>
            XmlElement sound_path = xml_doc.CreateElement("SoundPath");
            sound_path.InnerText = overlay_item.SoundPath;
            overlay_item_root_element.AppendChild(sound_path);

            //   <HotKey>
            XmlElement hotkey = xml_doc.CreateElement("HotKey");
            hotkey.InnerText = overlay_item.HotKey;
            overlay_item_root_element.AppendChild(hotkey);

            //   <SoundVolume>
            XmlElement sound_volume = xml_doc.CreateElement("SoundVolume");
            sound_volume.InnerText = overlay_item.SoundVolume.ToString();
            overlay_item_root_element.AppendChild(sound_volume);

            //   <DisplayDuration>
            XmlElement dispaly_duration = xml_doc.CreateElement("DisplayDuration");
            dispaly_duration.InnerText = overlay_item.DisplayDuration;
            overlay_item_root_element.AppendChild(dispaly_duration);

            // Add the new overlay item xml to the end of the document.
            document_root.AppendChild(overlay_item_root_element);
        }
    }
}
