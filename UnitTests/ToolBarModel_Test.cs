using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Windows;
using System.Windows.Media;
using CatboobGGStream;
using CatboobGGStream.Model;

namespace UnitTests
{
    [TestClass]
    public class ToolBarModel_Test
    {
        // Expected values;
        int toolbar_height_m = 56;
        string title_text_m = "Catboob";
        SolidColorBrush background_color_m = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF009688");
        SolidColorBrush font_color_m = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
        string main_menu_text_m = "Main menu click delegate function call.";
        string back_text_m = "Back click delegate function call.";
        string cancel_text_m = "Cancel click delegate function call.";
        string delete_text_m = "Delete click delegate function call.";
        string edit_text_m = "Edit click delegate function call.";
        string save_text_m = "Save click delegate function call.";

        // Model to test.
        ToolBarModel model_to_test_m;

        [TestMethod]
        public void ToolBarModleTest()
        {
            model_to_test_m = new ToolBarModel();
            model_to_test_m.ToolBarHeight = 56;
            model_to_test_m.TitleText = "Catboob";
            model_to_test_m.ToolBarBackgroundColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF009688");
            model_to_test_m.ToolBarFontColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");            
            model_to_test_m.MainMenuClick = new DelegateCmdBase((x) => MainMenuClickTest());            
            model_to_test_m.BackClick = new DelegateCmdBase((x) => BackClickTest());            
            model_to_test_m.CancelClick = new DelegateCmdBase((x) => CancelClickTest());            
            model_to_test_m.DeleteClick = new DelegateCmdBase((x) => DeleteClickTest());            
            model_to_test_m.EditClick = new DelegateCmdBase((x) => EditClickTest());            
            model_to_test_m.SaveClick = new DelegateCmdBase((x) => SaveClickTest());

            // Test height.
            Assert.AreEqual(toolbar_height_m, model_to_test_m.ToolBarHeight);

            // Test title text.
            Assert.AreEqual(title_text_m, model_to_test_m.TitleText);

            // Test colors.
            Assert.AreEqual(background_color_m.Color, model_to_test_m.ToolBarBackgroundColor.Color);
            Assert.AreEqual(font_color_m.Color, model_to_test_m.ToolBarFontColor.Color);

            // Test visiblity.
            Assert.AreEqual(Visibility.Collapsed, model_to_test_m.MainMenuVisible);
            Assert.AreEqual(Visibility.Collapsed, model_to_test_m.BackVisible);
            Assert.AreEqual(Visibility.Collapsed, model_to_test_m.CancelVisible);
            Assert.AreEqual(Visibility.Collapsed, model_to_test_m.DeleteVisible);
            Assert.AreEqual(Visibility.Collapsed, model_to_test_m.EditVisible);
            Assert.AreEqual(Visibility.Collapsed, model_to_test_m.SaveVisible);

            model_to_test_m.MainMenuVisible = Visibility.Visible;
            model_to_test_m.BackVisible = Visibility.Visible;
            model_to_test_m.CancelVisible = Visibility.Visible;
            model_to_test_m.DeleteVisible = Visibility.Visible;
            model_to_test_m.EditVisible = Visibility.Visible;
            model_to_test_m.SaveVisible = Visibility.Visible;

            Assert.AreEqual(Visibility.Visible, model_to_test_m.MainMenuVisible);
            Assert.AreEqual(Visibility.Visible, model_to_test_m.BackVisible);
            Assert.AreEqual(Visibility.Visible, model_to_test_m.CancelVisible);
            Assert.AreEqual(Visibility.Visible, model_to_test_m.DeleteVisible);
            Assert.AreEqual(Visibility.Visible, model_to_test_m.EditVisible);
            Assert.AreEqual(Visibility.Visible, model_to_test_m.SaveVisible);

            // Execute click delagate functions.
            model_to_test_m.MainMenuClick.Execute(null);
            model_to_test_m.BackClick.Execute(null);
            model_to_test_m.CancelClick.Execute(null);
            model_to_test_m.DeleteClick.Execute(null);
            model_to_test_m.EditClick.Execute(null);
            model_to_test_m.SaveClick.Execute(null);
        }

        private void MainMenuClickTest()
        {
            string temp_main_menu_text = "Main menu click delegate function call.";
            Console.WriteLine(temp_main_menu_text);
            Assert.AreEqual(main_menu_text_m, temp_main_menu_text);
        }

        private void BackClickTest()
        {
            string temp_back_text = "Back click delegate function call.";
            Console.WriteLine(temp_back_text);
            Assert.AreEqual(back_text_m, temp_back_text);
        }

        private void CancelClickTest()
        {
            string temp_cancel_text = "Cancel click delegate function call.";
            Console.WriteLine(temp_cancel_text);
            Assert.AreEqual(cancel_text_m, temp_cancel_text);
        }

        private void DeleteClickTest()
        {
            string temp_delete_text = "Delete click delegate function call.";
            Console.WriteLine(temp_delete_text);
            Assert.AreEqual(delete_text_m, temp_delete_text);
        }

        private void EditClickTest()
        {
            string temp_edit_text = "Edit click delegate function call.";
            Console.WriteLine(temp_edit_text);
            Assert.AreEqual(edit_text_m, temp_edit_text);
        }

        private void SaveClickTest()
        {
            string temp_save_text = "Save click delegate function call.";
            Console.WriteLine(temp_save_text);
            Assert.AreEqual(save_text_m, temp_save_text);
        }
    }
}
