﻿using PracticaTallerLogin.CapaPresentacion.Forms;
using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}