using System.Windows;
namespace StockWorm.Wpf.util
{
    public class CustomMessage
    {
        private static CustomMessage instance;
        private static object obj = new object();

        private CustomMessage()
        {

        }

        public static CustomMessage GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new CustomMessage();
                    }
                }
            }
            return instance;
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message,"错误",MessageBoxButton.OK,MessageBoxImage.Error);
        }
    }
}