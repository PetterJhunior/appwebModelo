﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c3_dominio.entidad;
namespace Modelo.c3_dominio.contrato
{
    public interface IUsuarioDAO
    {
        void crearUsuario(Usuario usuario);
        Usuario buscarUsuario(string cuentaUsuario, string claveUsuario);
    }
}
