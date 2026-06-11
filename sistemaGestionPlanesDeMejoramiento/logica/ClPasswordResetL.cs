using SendGrid;
using SendGrid.Helpers.Mail;
using sistemaGestionPlanesDeMejoramiento.datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClPasswordResetL
    {
        private ClPasswordResetD datos = new ClPasswordResetD();
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpKey;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public ClPasswordResetL()
        {
            int puerto;
            _smtpHost = ConfigurationManager.AppSettings["BrevoSmtpHost"];
            _smtpPort = int.TryParse(ConfigurationManager.AppSettings["BrevoSmtpPort"], out puerto) ? puerto : 587;
            _smtpUser = ConfigurationManager.AppSettings["BrevoSmtpUser"];
            _smtpKey = ConfigurationManager.AppSettings["BrevoSmtpKey"];
            _fromEmail = ConfigurationManager.AppSettings["BrevoFromEmail"];
            _fromName = ConfigurationManager.AppSettings["BrevoFromName"];
        }

        // Solicitar código de verificación
        public async Task<bool> SolicitarCodigo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo es obligatorio.");

            var usuario = datos.obtenerUsuarioPorCorreo(correo);
            if (usuario == null) return false;

            string codigo = GenerarCodigoAlfanumerico(8);
            DateTime expiracion = DateTime.Now.AddMinutes(15);

            datos.guardarCodigo(usuario.idUsuario, codigo, expiracion);
            await EnviarCorreoConCodigo(correo, codigo);
            return true;
        }

        // Validar código
        public bool ValidarCodigo(string codigo, out int idUsuario)
        {
            return datos.validarCodigo(NormalizarCodigo(codigo), out idUsuario);
        }

        // Restablecer contraseña usando código
        public bool RestablecerPassword(string codigo, string nuevaPassword)
        {
            if (string.IsNullOrWhiteSpace(nuevaPassword))
                throw new ArgumentException("La nueva contraseña es obligatoria.");
            if (nuevaPassword.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.");

            codigo = NormalizarCodigo(codigo);
            if (datos.validarCodigo(codigo, out int idUsuario))
            {
                string nuevoHash = ClUsuarioD.HashPassword(nuevaPassword);
                if (datos.ActualizarPassword(idUsuario, nuevoHash))
                {
                    datos.MarcarCodigoComoUsado(codigo);
                    return true;
                }
            }
            return false;
        }

        // Generar código alfanumérico (evita caracteres confusos: I, O, 0, 1)
        private string GenerarCodigoAlfanumerico(int longitud)
        {
            const string caracteres = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            char[] codigo = new char[longitud];
            byte[] buffer = new byte[longitud];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(buffer);
            }

            for (int i = 0; i < longitud; i++)
                codigo[i] = caracteres[buffer[i] % caracteres.Length];

            return new string(codigo);
        }

        private string NormalizarCodigo(string codigo)
        {
            return string.IsNullOrWhiteSpace(codigo) ? "" : codigo.Trim().ToUpperInvariant();
        }

        // Enviar correo con SendGrid
        private async Task EnviarCorreoConCodigo(string destinatario, string codigo)
        {
            if (string.IsNullOrWhiteSpace(_smtpHost) ||
                string.IsNullOrWhiteSpace(_smtpUser) ||
                string.IsNullOrWhiteSpace(_smtpKey) ||
                string.IsNullOrWhiteSpace(_fromEmail))
                throw new InvalidOperationException("La configuración SMTP de recuperación no está completa.");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Configurar el cliente SMTP con los parámetros de Brevo
            using (SmtpClient client = new SmtpClient(_smtpHost, _smtpPort))
            {
                // Configurar las credenciales y la seguridad de la conexión
                client.EnableSsl = true; // Necesario para el puerto 587 (TLS)
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpUser, _smtpKey);

                // Crear el mensaje de correo
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_fromEmail, _fromName);
                mail.To.Add(destinatario);
                mail.Subject = "Código de recuperación de contraseña";
                mail.IsBodyHtml = true;
                mail.Body = $@"
                    <h2>Recuperación de contraseña</h2>
                    <p>Has solicitado restablecer tu contraseña. Usa el siguiente código:</p>
                    <h3 style='font-size:32px; letter-spacing:5px;'>{codigo}</h3>
                    <p>Este código expira en <strong>15 minutos</strong>.</p>
                    <p>Si no solicitaste este cambio, ignora este correo.</p>";

                // Enviar el correo de forma asíncrona
                await client.SendMailAsync(mail);
            }
        }
    }
}
