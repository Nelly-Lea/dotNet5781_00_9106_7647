﻿using System;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public Window2()
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
            PasswordUnmask1.Text = PasswordHidden.Password;

        }
        private void HidePasswordFunction1()
        {
            ShowPassword.Text = "SHOW";
            PasswordUnmask.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
        }
        private void Registered_Click(object sender, RoutedEventArgs e)
 {
            string user = UserName.Text;
            string password=PasswordHidden.Password;

            bool DirectorWorkerRegistered = bl.CheckUserWorker(user, password);
            if (DirectorWorkerRegistered)
            {
                Window3 win3 = new Window3();
               
                win3.ShowDialog();
            }
            else
            {
                MessageBox.Show("Input Error");
            }

        }

       
    }
}
