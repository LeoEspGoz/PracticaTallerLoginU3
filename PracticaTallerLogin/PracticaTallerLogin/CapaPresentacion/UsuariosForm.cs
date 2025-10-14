using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;

namespace PracticaTallerLogin.CapaPresentacion.Forms
{
    public partial class UsuariosForm : Form
    {
        private readonly UsuarioNegocio negocio = new UsuarioNegocio();

        public UsuariosForm()
        {
            InitializeComponent();
            CargarUsuarios();
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged; // Evento del DataGridView
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = negocio.ObtenerUsuarios();
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtUsuario.Clear();
            txtCorreo.Clear();
            txtPassword.Clear();
            txtRol.Clear();
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                txtId.Text = dgvUsuarios.CurrentRow.Cells["Id"].Value.ToString();
                txtUsuario.Text = dgvUsuarios.CurrentRow.Cells["Nombre"].Value.ToString();
                txtCorreo.Text = dgvUsuarios.CurrentRow.Cells["Correo"].Value.ToString();
                txtRol.Text = dgvUsuarios.CurrentRow.Cells["Rol"].Value.ToString();
                txtPassword.Clear(); // No mostramos la contraseña
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Ingresa nombre y contraseña");
                return;
            }

            bool exito = negocio.Registrar(txtUsuario.Text, txtCorreo.Text, txtPassword.Text);
            if (exito)
            {
                MessageBox.Show("Usuario agregado correctamente");
                CargarUsuarios();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al agregar el usuario");
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["Id"].Value);
                bool exito = negocio.EliminarUsuario(id);
                if (exito)
                {
                    MessageBox.Show("Usuario eliminado correctamente");
                    CargarUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar usuario");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para eliminar");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Selecciona un usuario para actualizar.");
                    return;
                }

                Usuario u = new Usuario
                {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtUsuario.Text,
                    Correo = txtCorreo.Text,
                    Rol = txtRol.Text
                };

                bool exito = negocio.ActualizarUsuario(u);

                if (exito)
                {
                    MessageBox.Show("Usuario actualizado correctamente.");
                    CargarUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar usuario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
    }
}
