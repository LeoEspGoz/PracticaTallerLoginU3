using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaTallerLogin.CapaPresentacion.Forms
{
    public partial class LoginForm : Form

    {
        private readonly UsuarioNegocio negocio = new UsuarioNegocio();
        public LoginForm()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string hash = CapaUtilidades.Seguridad.CalcularSHA256(txtContrasena.Text);
                MessageBox.Show("Hash calculado: " + hash);

                var usuario = negocio.IniciarSesion(txtCorreo.Text, txtContrasena.Text);

                if (usuario != null)
                {
                    MessageBox.Show($"Bienvenido {usuario.Nombre}");
                    UsuariosForm form = new UsuariosForm();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Correo o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
