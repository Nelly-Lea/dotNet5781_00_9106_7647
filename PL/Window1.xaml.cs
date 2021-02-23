using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public Window1()
        {
            InitializeComponent();
        }
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        private void ShowPassword_MouseLeave(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();

        private void ShowPasswordFunction()
        {
            ShowPassword.Text = "HIDE";
            PasswordUnmask.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;
            PasswordUnmask.Text = PasswordHidden.Password;

        }
        private void HidePasswordFunction()
        {
            ShowPassword.Text = "SHOW";
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
        }

        private void ShowPassword_PreviewMouseUp1(object sender, MouseButtonEventArgs e) => HidePasswordFunction1();
        private void ShowPassword_MouseLeave1(object sender, MouseButtonEventArgs e) => HidePasswordFunction1();
        private void ShowPassword_PreviewMouseDown1(object sender, MouseButtonEventArgs e) => ShowPasswordFunction1();

        private void ShowPasswordFunction1()
        {
            ShowPassword1.Text = "HIDE";
            PasswordUnmask1.Visibility = Visibility.Visible;
            PasswordHidden1.Visibility = Visibility.Hidden;
            PasswordUnmask1.Text = PasswordHidden1.Password;

        }
        private void HidePasswordFunction1()
        {
            ShowPassword1.Text = "SHOW";
            PasswordUnmask1.Visibility = Visibility.Hidden;
            PasswordHidden1.Visibility = Visibility.Visible;
        }
        private void ShowPassword_PreviewMouseUp2(object sender, MouseButtonEventArgs e) => HidePasswordFunction2();
        private void ShowPassword_MouseLeave2(object sender, MouseButtonEventArgs e) => HidePasswordFunction2();
        private void ShowPassword_PreviewMouseDown2(object sender, MouseButtonEventArgs e) => ShowPasswordFunction2();

        private void ShowPasswordFunction2()
        {
            ShowPassword2.Text = "HIDE";
            PasswordUnmask2.Visibility = Visibility.Visible;
            PasswordHidden2.Visibility = Visibility.Hidden;
            PasswordUnmask2.Text = PasswordHidden2.Password;

        }
        private void HidePasswordFunction2()
        {
            ShowPassword2.Text = "SHOW";
            PasswordUnmask2.Visibility = Visibility.Hidden;
            PasswordHidden2.Visibility = Visibility.Visible;
        }
        private void Registered_Click(object sender, RoutedEventArgs e)
        {
            string user = UserName.Text;
            string password = PasswordHidden.Password;

            try
            {
                bl.CheckUserPassenger(user, password);
                Window13 win13 = new Window13();
                win13.CurrentUser = UserName.Text;
                win13.init();

                win13.ShowDialog();
            }
            catch (BO.BadUserNameException ex)
            {
                MessageBox.Show("Input Error");
            }
            catch (BO.BadPasswordUserException ex)
            {
                MessageBox.Show("Error Password");
            }


        }

        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(PasswordHidden1.Password) || (string.IsNullOrEmpty(PasswordHidden2.Password)) || (string.IsNullOrEmpty(NewUserName.Text)))
                    throw new BO.BadInputException("bad input");
                bl.CheckPassword(PasswordHidden1.Password, PasswordHidden2.Password);
                bl.AddUser(NewUserName.Text, PasswordHidden1.Password, false);
                MessageBox.Show("You have been registered");
                this.Close();
            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show("Not all the fields are completed");
            }
            catch (BO.BadPasswordUserException ex)
            {
                MessageBox.Show("Paswords don't match");
            }
           
            catch (BO.BadUserNameException ex)
            {
                MessageBox.Show("The user name already exist");
            }
        }
    }
}
