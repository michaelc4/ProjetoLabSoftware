﻿using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Presenters
{
    public class UsuarioPutDto
    {
        [Required(ErrorMessage = "Id é um campo obrigatório.")]
        public string Id { get; set; }

        public string DesImagem { get; set; }

        [StringLength(20, ErrorMessage = "Senha deve ter no máximo {1} caracteres.")]
        public string DesSenha { get; set; }

        [StringLength(20, ErrorMessage = "Telefone deve ter no máximo {1} caracteres.")]
        public string DesTelefone { get; set; }

        [StringLength(300, ErrorMessage = "Especialidade deve ter no máximo {1} caracteres.")]
        public string DesEspecialidade { get; set; }

        [StringLength(3000, ErrorMessage = "Experiência deve ter no máximo {1} caracteres.")]
        public string DesExperiencia { get; set; }

        public int Admin { get; set; }
    }
}
