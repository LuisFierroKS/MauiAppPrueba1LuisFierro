using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;

namespace MauiAppPrueba1LuisFierro
{
    internal class PersonaDatabase
    {
        private string connectionString;
        private SqliteConnection connection;

        public PersonaDatabase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "personas.db");
            connectionString = $"Data Source={dbPath}";
            connection = new SqliteConnection(connectionString);
            connection.Open();

            connection.Execute(@"CREATE TABLE IF NOT EXISTS Personas (
                                    Cedula TEXT PRIMARY KEY,
                                    Nombres TEXT NOT NULL,
                                    Apellidos TEXT NOT NULL,
                                    Correo TEXT,
                                    Telefono TEXT
                                )");
        }

        public Persona Create(string cedula, string nombres, string apellidos, string correo, string telefono)
        {
            var nuevaPersona = new Persona
            {
                Cedula = cedula,
                Nombres = nombres,
                Apellidos = apellidos,
                Correo = correo,
                Telefono = telefono
            };

            var affected = connection.Execute(@"INSERT INTO Personas (Cedula, Nombres, Apellidos, Correo, Telefono) 
                                                VALUES (@Cedula, @Nombres, @Apellidos, @Correo, @Telefono)", nuevaPersona);

            if (affected == 0) throw new Exception("No se pudo registrar a la persona.");
            return nuevaPersona;
        }

        public Persona ReadByCedula(string cedula)
        {
            return connection.Query<Persona>("SELECT * FROM Personas WHERE Cedula = @Cedula", new { Cedula = cedula }).FirstOrDefault();
        }

        public List<Persona> ReadAll()
        {
            return connection.Query<Persona>("SELECT * FROM Personas").ToList();
        }

        public void Update(Persona persona)
        {
            var affected = connection.Execute(@"UPDATE Personas 
                                                SET Nombres = @Nombres, Apellidos = @Apellidos, Correo = @Correo, Telefono = @Telefono 
                                                WHERE Cedula = @Cedula", persona);
            if (affected == 0) throw new Exception($"No se pudo actualizar el registro de la cédula {persona.Cedula}");
        }

        public void Delete(string cedula)
        {
            var affected = connection.Execute("DELETE FROM Personas WHERE Cedula = @Cedula", new { Cedula = cedula });
            if (affected == 0) throw new Exception($"No se pudo eliminar el registro con cédula {cedula}");
        }

        
    }
}
