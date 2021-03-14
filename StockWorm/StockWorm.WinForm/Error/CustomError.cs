using System.Windows.Forms;

namespace StockWorm.WinForm.Error
{
    public class CustomError
    {
        public void ShowError(string message)
        {
            MessageBox.Show(message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
    }
}