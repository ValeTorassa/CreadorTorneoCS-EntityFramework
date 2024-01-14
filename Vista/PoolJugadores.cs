using Controladora;
using Entidades;
using System.Windows.Forms;

namespace Vista
{
    public partial class PoolJugadores : Form
    {
        //Constructor del Form1 que se ejecuta al iniciar el programa
        public PoolJugadores()
        {
            InitializeComponent();
        }
        // Metodo asociado al bot�n de "Agregar" jugador.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(cmbRango.Text))
                {
                    var mensaje = ControladoraJugador.Instancia.CrearJugador(txtNombre.Text, cmbRango.Text);
                    MessageBox.Show(mensaje);
                    ActualizarGrilla();
                }

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al bot�n de "Eliminar" jugador.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvJugadores.Rows.Count > 0)
                {
                    var jugadorSeleccionado = (Jugador)dgvJugadores.CurrentRow.DataBoundItem;

                    if (jugadorSeleccionado != null)
                    {
                        var mensaje = ControladoraJugador.Instancia.EliminarJugador(jugadorSeleccionado);
                        MessageBox.Show(mensaje);
                        ActualizarGrilla();
                    }
                    else
                    {
                        MessageBox.Show("El jugador no se ha podido eliminar", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al bot�n "Crear Torneo Casual"
        private void btnTorneos_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombreTorneo.Text) && numJugadores.Value <= ControladoraTorneoCasual.Instancia.ObtenerJugadoresSinEquipo().Count)
                {
                    var mensaje = ControladoraTorneoCasual.Instancia.CrearTorneoCasual(txtNombreTorneo.Text, Convert.ToInt32(numJugadores.Value));
                    MessageBox.Show(mensaje);

                    ActualizarCMBs();
                    ActualizarGrilla();
                }
                else
                {
                    MessageBox.Show("El torneo no se ha podido agregar", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar Torneo Casual: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al bot�n de "Ver Torneo Casual"
        private void btnVerTorneo_Click(object sender, EventArgs e)
        {
            try
            {
                string torneoSeleccionadoNombre = cmbTorneoCasual.Text;

                if (!string.IsNullOrEmpty(torneoSeleccionadoNombre))
                {
                    Entidades.Torneo torneoSeleccionado = ControladoraTorneoCasual.Instancia.ObtenerTorneoPorNombre(torneoSeleccionadoNombre);

                    Torneo form2 = new Torneo(torneoSeleccionado);
                    form2.Show();
                }
                else
                {
                    MessageBox.Show("Selecciona un torneo antes de continuar.");
                }

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir torneo Casual: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al bot�n de "Eliminar Torneo" (Casual)
        private void btnEliminarTorneo_Click(object sender, EventArgs e)
        {
            try
            {
                string torneoSeleccionadoNombre = cmbTorneoCasual.Text;

                if (!string.IsNullOrEmpty(torneoSeleccionadoNombre))
                {
                    Entidades.Torneo torneoSeleccionado = ControladoraTorneoCasual.Instancia.ObtenerTorneoPorNombre(torneoSeleccionadoNombre);

                    var mensaje = ControladoraTorneoCasual.Instancia.EliminarTorneoCasual(torneoSeleccionado);
                    MessageBox.Show(mensaje);

                    ActualizarCMBs();
                    ActualizarGrilla();
                }
                else
                {
                    MessageBox.Show("Selecciona un torneo antes de eliminarlo.");
                }

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar un torneo casual: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al bot�n de "Ver Torneo Profesional"
        private void btnVerTorneoProfesional_Click(object sender, EventArgs e)
        {
            try
            {
                string torneoSeleccionadoNombre = cmbTorneosProfesionales.Text;

                if (!string.IsNullOrEmpty(torneoSeleccionadoNombre))
                {
                    TorneoProfesional torneoSeleccionado = ControladoraTorneoProfesional.Instancia.ObtenerTorneoProfesionalPorNombre(torneoSeleccionadoNombre);
                    Torneo form2 = new Torneo(torneoSeleccionado);
                    form2.Show();
                }
                else
                {
                    MessageBox.Show("Selecciona un torneo antes de continuar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir torneo profesional: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al bot�n de "Eliminar Torneo Profesional"
        private void btnEliminarProfesional_Click(object sender, EventArgs e)
        {
            try
            {
                string torneoSeleccionadoNombre = cmbTorneosProfesionales.Text;

                if (!string.IsNullOrEmpty(torneoSeleccionadoNombre))
                {
                    TorneoProfesional torneoSeleccionado = ControladoraTorneoProfesional.Instancia.ObtenerTorneoProfesionalPorNombre(torneoSeleccionadoNombre);

                    var mensaje = ControladoraTorneoProfesional.Instancia.EliminarTorneoProfesional(torneoSeleccionado);
                    MessageBox.Show(mensaje);

                    ActualizarCMBs();
                    ActualizarGrilla();
                }
                else
                {
                    MessageBox.Show("Selecciona un torneo profesional antes de eliminarlo.");
                }

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar torneo profesional: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al bot�n "Crear Torneo Profesional"
        private void btnTorneoProfesional_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombreTorneo.Text) && numJugadores.Value <= ControladoraTorneoProfesional.Instancia.ObtenerJugadoresSinEquipoProfesionales().Count && !string.IsNullOrEmpty(txtUbicacion.Text))
                {
                    var mensaje = ControladoraTorneoProfesional.Instancia.CrearTorneoProfesional(txtNombreTorneo.Text, Convert.ToInt32(numJugadores.Value), txtUbicacion.Text, Convert.ToInt32(numPremio.Value));
                    MessageBox.Show(mensaje);

                    ActualizarCMBs();
                    ActualizarGrilla();
                }
                else
                {
                    MessageBox.Show("El torneo profesional no se ha podido crear", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar torneo profesional: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al evento Load del formulario
        private void PoolJugadores_Load(object sender, EventArgs e)
        {
            try
            {
                CreacionRangos();
                JugadoresAleatorios();
                ActualizarCMBs();
                ActualizarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el cargar el formulario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodo asociado al bot�n "Salir"
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // M�todo para actualizar los ComboBoxes con informaci�n de torneos y rangos
        void ActualizarCMBs()
        {
            try
            {
                cmbTorneoCasual.DataSource = null;
                cmbTorneoCasual.DataSource = ControladoraTorneoCasual.Instancia.ObtenerTorneosCasuales();
                cmbTorneoCasual.DisplayMember = "Nombre";

                cmbTorneosProfesionales.DataSource = null;
                cmbTorneosProfesionales.DataSource = ControladoraTorneoProfesional.Instancia.ObtenerTorneosProfesionales();
                cmbTorneosProfesionales.DisplayMember = "Nombre";

                cmbRango.DataSource = null;
                cmbRango.DataSource = ControladoraRango.Instancia.ObtenerRangos();
                cmbRango.DisplayMember = "Nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el actualizar los comboboxs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Metodo para actualizar la grilla de jugadores
        void ActualizarGrilla()
        {

            dgvJugadores.DataSource = null;

            dgvJugadores.DataSource = ControladoraJugador.Instancia.ObtenerJugadoresSinEquipo();
        }

        
       // M�todo para generar jugadores aleatorios
        private void JugadoresAleatorios()
        {
            // Lista de nombres de usuarios
            List<string> nombresDeUsuarios = new List<string>
            {
                "AirWolf","Fessa", "DarkHallow", "NeitOH", "ElMaldito", "ElKaiser", "FJaime11", "SpectralSword", "NeonFury", "RogueWolf", "VortexSlayer", "PixelPirate", "MysticRider", "RapidFalcon", "EchoEnigma", "ChronoWraith"
            };

            Random random = new Random();

            for (int i = 1; i <= 40; i++)
            {

                // Seleccionar un rango aleatoriamente
                List<Rango> rangosDisponibles = ControladoraRango.Instancia.ObtenerRangos();


                Rango rangoSeleccionado = rangosDisponibles[random.Next(rangosDisponibles.Count)];
                string nombreRango = rangoSeleccionado.Nombre; // Aseg�rate de adaptar esto seg�n la estructura de tu clase Rango

                // Seleccionar un nombre de usuario aleatoriamente
                string nombreJugador = nombresDeUsuarios[random.Next(nombresDeUsuarios.Count)] + i;

                ControladoraJugador.Instancia.CrearJugador(nombreJugador, nombreRango);
            }
        }
        
        
        // M�todo para crear rangos
        private void CreacionRangos()
        {
            // CrearRango se encarga de la creaci�n del rango
            ControladoraRango.Instancia.CrearRango("global elite", 10);
            ControladoraRango.Instancia.CrearRango("silver", 1);
            ControladoraRango.Instancia.CrearRango("gold", 3);
            ControladoraRango.Instancia.CrearRango("AK", 7);
        }
       
        //Metodo para limpiar los campos de entradas.
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtNombreTorneo.Text = "";
            txtUbicacion.Text = "";
        }
    }
}