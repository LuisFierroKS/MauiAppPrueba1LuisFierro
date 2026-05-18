namespace MauiAppPrueba1LuisFierro
{
    public partial class MainPage : ContentPage
    {
        private PersonaDatabase db;

        public MainPage()
        {
            InitializeComponent();
            db = new PersonaDatabase();
        }

        private void cmdCrear_Clicked(object sender, EventArgs e)
        {
            try
            {
                var persona = db.Create(
                    CedulaEntry.Text,
                    NombresEntry.Text,
                    ApellidosEntry.Text,
                    CorreoEntry.Text,
                    TelefonoEntry.Text
                );
                DisplayAlert("Éxito", $"Persona registrada: {persona.Nombres} {persona.Apellidos}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudo crear: {ex.Message}", "OK");
            }
        }

        private void cmdLeer_Clicked(object sender, EventArgs e)
        {
            try
            {
                var persona = db.ReadByCedula(CedulaEntry.Text);
                if (persona != null)
                {
                    NombresEntry.Text = persona.Nombres;
                    ApellidosEntry.Text = persona.Apellidos;

                    CorreoEntry.Text = persona.Correo;
                    TelefonoEntry.Text = persona.Telefono;

                    DisplayAlert("Encontrado", "Los datos han sido cargados en los campos.", "OK");
                }
                else
                {
                    DisplayAlert("No Encontrado", "No existe ninguna persona con esa cédula.", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void cmdActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var persona = new Persona
                {
                    Cedula = CedulaEntry.Text,
                    Nombres = NombresEntry.Text,
                    Apellidos = ApellidosEntry.Text,
                    Correo = CorreoEntry.Text,
                    Telefono = TelefonoEntry.Text
                };

                db.Update(persona);
                DisplayAlert("Éxito", "Los datos de la persona han sido actualizados.", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void cmdEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                db.Delete(CedulaEntry.Text);
                DisplayAlert("Eliminado", "Registro eliminado correctamente de la base de datos.", "OK");

                CedulaEntry.Text = string.Empty;
                NombresEntry.Text = string.Empty;
                ApellidosEntry.Text = string.Empty;
                CorreoEntry.Text = string.Empty;
                TelefonoEntry.Text = string.Empty;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
