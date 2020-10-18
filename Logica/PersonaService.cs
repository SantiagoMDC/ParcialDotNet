using Datos;
using Entity;
using System;
using System.Collections.Generic;

namespace Logica
{
    public class PersonaService
    {
        private readonly ConnectionManager _conexion;
        private readonly PersonaRepository _repositorio;
        public PersonaService(string connectionString)
        {
            _conexion = new ConnectionManager(connectionString);
            _repositorio = new PersonaRepository(_conexion);
        }
        public GuardarPersonaResponse Guardar(Persona persona)
        {
            try
            {
                _conexion.Open();
                _repositorio.Guardar(persona);
                _conexion.Close();
                return new GuardarPersonaResponse(persona);
            }
            catch (Exception e)
            {
                return new GuardarPersonaResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { _conexion.Close(); }
        }
        public List<Persona> ConsultarTodos()
        {
            _conexion.Open();
            List<Persona> personas = _repositorio.ConsultarTodos();
            _conexion.Close();
            return personas;
        }
    
        public Persona BuscarxIdentificacion(string identificacion)
        {
            _conexion.Open();
            Persona persona = _repositorio.BuscarPorIdentificacion(identificacion);
            _conexion.Close();
            return persona;
        }
        public int Totalizar()
        {
            return _repositorio.Totalizar();
        }
       
    }

    public class GuardarPersonaResponse 
    {
        public GuardarPersonaResponse(Persona persona)
        {
            Error = false;
            Persona = persona;
        }
        public GuardarPersonaResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Persona Persona { get; set; }
    }
}