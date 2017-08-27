using System.Windows.Forms;
using DevExpress.XtraEditors;
using Utils.MessageBoxExLib;

namespace Prometheus
{
  public class Dialogs
  {
    public static DialogResult ShowPrompt(string text)
    {
      return ShowPrompt(text, MessageBoxButtons.OKCancel);
    }
    public static DialogResult ShowPrompt(string text, MessageBoxButtons buttons)
    {
      return XtraMessageBox.Show(text, "Prompt", buttons, MessageBoxIcon.Question);
    }
    public static DialogResult ShowError(string text)
    {
      return ShowError(text, MessageBoxButtons.OK);
    }
    public static DialogResult ShowError(string text, MessageBoxButtons buttons)
    {
      return XtraMessageBox.Show(text, "Error", buttons, MessageBoxIcon.Error);
    }
    public static string ShowErrorOld(string text)
    {
      return ShowErrorOld(text, MessageBoxButtons.OK);
    }
    public static string ShowErrorOld(string text, MessageBoxButtons buttons)
    {
      MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox(null);
      messageBox.Text = text;
      messageBox.Caption = "Error";
      messageBox.AddButtons(MessageBoxButtons.OK);
      messageBox.Icon = MessageBoxExIcon.Error;
      return messageBox.Show();
    }
  }
}
